using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using NDesk.Options;
using System.CodeDom.Compiler;
using log4net.Config;
using log4net;

using QCV.Extensions;

namespace QCV {
  public partial class Main : Form {

    private static readonly ILog _logger = LogManager.GetLogger(typeof(Main));
    private HookableTextWriter _console_hook = new HookableTextWriter();
    private Base.InstantCompiler _ic = null;
    private Base.Addins.AddinHost _ah = null;
    private Dictionary<string, object> _env = null;
    private Base.FilterList _fl = null;
    private Base.Runtime _runtime = null;
    private CommandLine.CLIArgs _args = null;
    private bool _appexit_requested = false;
    private ShowQueryForm _query_form;

    public Main() {
      InitializeComponent();

      _query_form = new ShowQueryForm(this);
      _query_form.FormClosing += new FormClosingEventHandler(AnyFormClosing);

      _fl_settings.DataInteractor = this;

      // Redirect console output
      _console_hook.StringAppendedEvent += new HookableTextWriter.StringAppendedEventHandler(ConsoleStringAppendedEvent);
      Console.SetOut(_console_hook);

      // Configure logging
      XmlConfigurator.Configure(new System.IO.FileInfo("QCV.log4net"));

      // Parse command line
      CommandLine cl = new CommandLine();
      _args = cl.Args;

      if (_args.help) {
        Console.WriteLine(cl.GetHelp());
      }

      // Compile all scripts
      _ic = new QCV.Base.InstantCompiler(
        _args.source_paths,
        _args.compile_references.Union(new string[] { 
            "mscorlib.dll", "System.dll", "System.Drawing.dll", "System.Xml.dll",
            "QCV.Base.dll", "QCV.Toolbox.dll", "Emgu.CV.dll", "Emgu.Util.dll"}).Distinct(),
        _args.enable_debugger
      );
      _ic.BuildSucceededEvent += new QCV.Base.InstantCompiler.BuildEventHandler(BuildSucceededEvent);

      _env = new Dictionary<string, object>() {
        {"interactor", this}
      };

      _runtime = new QCV.Base.Runtime();
      _runtime.RuntimeStartingEvent += new EventHandler(RuntimeStartingEvent);
      _runtime.RuntimeStoppedEvent += new EventHandler(RuntimeStoppedEvent);
      
      _nrc_fps.Value = (Decimal)_args.target_fps;

      _ic.Compile();
    }

    void ConsoleStringAppendedEvent(object sender, string text) {
      _rtb_console.InvokeIfRequired(() => {
        _rtb_console.Select(_rtb_console.TextLength, 0);
        _rtb_console.SelectionColor = ColorFromText(text);
        _rtb_console.AppendText(text);
        _rtb_console.ScrollToCaret();
      });
    }

    private Color ColorFromText(string text) {
      if (text.StartsWith("INFO")) {
        return Color.DarkGreen;
      } else if (text.StartsWith("ERROR")) {
        return Color.DarkRed;
      } else if (text.StartsWith("WARN")) {
        return Color.DarkOrange;
      } else {
        return Color.Black;
      }
    }

    void RuntimeStartingEvent(object sender, EventArgs e) {
      _btn_run.InvokeIfRequired(() => {
        _btn_run.Text = "Stop";
        _btn_run.BackColor = Color.LightGreen;
      });
    }

    void RuntimeStoppedEvent(object sender, EventArgs e) {
      _btn_run.InvokeIfRequired(() => {
        if (_appexit_requested || _args.auto_shutdown) {
          this.Close();
        } else {
          _btn_run.Text = "Run";
          _btn_run.BackColor = Control.DefaultBackColor;
        }
      });
    }

    void BuildSucceededEvent(object sender, QCV.Base.Compiler compiler) {
      bool running = _runtime.Running;
      if (running) {
        _query_form.Cancel();
        _runtime.Stop(true);
      }

      QCV.Base.Addins.AddinHost tmp = new QCV.Base.Addins.AddinHost();
      tmp.DiscoverInAssembly(compiler.CompiledAssemblies);
      

      if (_fl == null) {

        _ah = new QCV.Base.Addins.AddinHost();
        _ah.DiscoverInDomain();
        _ah.DiscoverInDirectory(Environment.CurrentDirectory);
        _ah.MergeByFullName(tmp);

        // First run
        _fl = new QCV.Base.FilterList();
        IEnumerable<Base.Addins.AddinInfo> providers = _ah.FindAddins(
            typeof(Base.IFilterListProvider),
            (ai) => (ai.DefaultConstructible && _args.filterlist_providers.Contains(ai.FullName)));

        int target_prov_count = _args.filterlist_providers.Count;
        int performance_prov_count = providers.Count();
        if (target_prov_count > performance_prov_count) {
          _logger.Warn("Not all specified IFilterListProvider were found.");
        }

        foreach (Base.Addins.AddinInfo ai in providers) {
          Base.IFilterListProvider p = _ah.CreateInstance(ai) as Base.IFilterListProvider;
          _fl.AddRange(p.CreateFilterList(_ah));
        }

        foreach (string lpath in _args.load_filterlist_paths) {
          string path = Path.GetFullPath(lpath);
          if (File.Exists(path)) {
            try {
              Base.FilterList tmp_fl = Base.FilterList.Load(path);
              _fl.AddRange(tmp_fl);
            } catch (Exception err) {
              _logger.Error(
                String.Format("Failed to load FilterList from '{0}' {1}", 
                path, err.Message)
              );
            }
               
          } else {
            _logger.Error(String.Format("Path '{0}' does not exist.", lpath));
          }
        }
        _logger.Info(String.Format("Created {0} filters.", _fl.Count));

        if (_args.immediate_execute) {
          _runtime.Run(_fl, _env, 0);
        }

      } else {
        _ah.MergeByFullName(tmp);
        // Subsequent runs
        QCV.Base.Reconfiguration r = new QCV.Base.Reconfiguration();
        QCV.Base.FilterList fl_new;
        r.Update(_fl, _ah, out fl_new);
        r.CopyPropertyValues(_fl, fl_new);
        _fl = fl_new;
      }

      _fl_settings.GenerateUI(_fl);

      if (running) {
        _runtime.Run(_fl, _env, 0);
      }
    }

    private Base.FilterList LoadAndCombineFilterLists(List<string> list) {
      Base.FilterList fl = new QCV.Base.FilterList();
      foreach (string path in list) {
        fl.AddRange(Base.FilterList.Load(path));
      }
      return fl;
    }

    void AnyFormClosing(object sender, FormClosingEventArgs e) {
      if (e.CloseReason != CloseReason.FormOwnerClosing) {
        e.Cancel = true;
        (sender as Form).Hide();
      }
    }

    private void _btn_play_Click(object sender, EventArgs e) {
      if (_runtime.Running) {
        _query_form.Cancel();
        _runtime.Stop(false);
      } else {
        _runtime.Run(_fl, _env, 0);
      }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      _appexit_requested = true;
      e.Cancel = Shutdown();
    }

    private bool Shutdown() {
      _query_form.Cancel();
      _runtime.Stop(false);
      return _runtime.Running;
    }

    private void _mnu_help_arguments_Click(object sender, EventArgs e) {
      Console.WriteLine(new CommandLine().GetHelp());
    }

    private void _mnu_save_filter_list_Click(object sender, EventArgs e) {
      if (this.saveFileDialog1.ShowDialog() == DialogResult.OK) {
        try {
          Base.FilterList.Save(this.saveFileDialog1.FileName, _fl);
        } catch (Exception err) {
          _logger.Error(
            String.Format("Failed to save FilterList to '{0}' {1}",
            this.saveFileDialog1.FileName, err.Message)
          );
        }
      }
    }

    private void ConsoleLinkClicked(object sender, LinkClickedEventArgs e) {
      System.Diagnostics.Process.Start(e.LinkText);
    }

    private void _nrc_fps_ValueChanged(object sender, EventArgs e) {
      SetCycleTime((double)_nrc_fps.Value);
    }

    private void SetCycleTime(double fps) {
      if (fps == 0) {
        _runtime.CycleTime.Enabled = false;
        _logger.Info("Cycle time control disabled");
      } else {
        _logger.Info("Cycle time control enabled");
        _runtime.CycleTime.Enabled = true;
        _runtime.CycleTime.FPS = fps;
      }
    }
  }
}
