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

    public Main() {
      InitializeComponent();

      // Redirect console output
      _console_hook.StringAppendedEvent += new HookableTextWriter.StringAppendedEventHandler(ConsoleStringAppendedEvent);
      Console.SetOut(_console_hook);

      // Configure logging
      XmlConfigurator.Configure(new System.IO.FileInfo("QCV.log4net"));

      // Parse command line
      CommandLine cl = new CommandLine();
      _args = cl.Args;

      // Compile all scripts
      _ic = new QCV.Base.InstantCompiler(
        _args.script_paths,
        _args.references.Union(new string[] { 
            "mscorlib.dll", "System.dll", "System.Drawing.dll", "System.Xml.dll",
            "QCV.Base.dll", "QCV.Toolbox.dll", "Emgu.CV.dll", "Emgu.Util.dll"}).Distinct()
      );
      _ic.BuildSucceededEvent += new QCV.Base.InstantCompiler.BuildEventHandler(BuildSucceededEvent);

      _ah = new QCV.Base.Addins.AddinHost();
      _ah.DiscoverInDomain();
      _ah.DiscoverInDirectory(Environment.CurrentDirectory);

      _env = new Dictionary<string, object>() {
        {"interactor", this}
      };

      _runtime = new QCV.Base.Runtime();
      _runtime.RuntimeStartingEvent += new EventHandler(RuntimeStartingEvent);
      _runtime.RuntimeStoppedEvent += new EventHandler(RuntimeStoppedEvent);

      _ic.Compile();

      if (_args.immediate_execute) {
        _runtime.Run(_fl, _env, 0);
      }
    }

    void ConsoleStringAppendedEvent(object sender, string text) {
      _rtb_console.InvokeIfRequired(() => {
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

    void RuntimeStoppedEvent(object sender, EventArgs e) {
      _btn_run.InvokeIfRequired(() => {
        _btn_run.Text = "Run";
        _btn_run.BackColor = Control.DefaultBackColor;
      });
    }

    void RuntimeStartingEvent(object sender, EventArgs e) {
      _btn_run.InvokeIfRequired(() => {
        _btn_run.Text = "Stop";
        _btn_run.BackColor = Color.LightGreen;
      });
    }

    void BuildSucceededEvent(object sender, QCV.Base.Compiler compiler) {
      bool running = _runtime.Running;
      if (running) {
        _runtime.Stop(true);
      }

      QCV.Base.Addins.AddinHost tmp = new QCV.Base.Addins.AddinHost();
      tmp.DiscoverInAssembly(compiler.CompiledAssemblies);
      _ah.MergeByFullName(tmp);

      if (_fl == null) {
        _fl = new QCV.Base.FilterList();
        IEnumerable<Base.Addins.AddinInfo> providers = _ah.FindAddins(
            typeof(Base.IFilterListProvider),
            (ai) => (ai.DefaultConstructible && _args.filterlist_providers.Contains(ai.FullName)));

        foreach (Base.Addins.AddinInfo ai in providers) {
          Base.IFilterListProvider p = _ah.CreateInstance(ai) as Base.IFilterListProvider;
          _fl.AddRange(p.CreateFilterList(_ah));
        }

        _logger.Info(String.Format("Created {0} filters.", _fl.Count));
      } else {
        QCV.Base.Reconfiguration r = new QCV.Base.Reconfiguration();
        QCV.Base.FilterList fl_new;
        r.Update(_fl, _ah, out fl_new);
        r.CopyPropertyValues(_fl, fl_new);
        _fl = fl_new;
      }

      _filter_properties.Filters = _fl;

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
        _runtime.Stop(false);
      } else {
        _runtime.Run(_fl, _env, 0);
      }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      e.Cancel = Shutdown();
    }

    private bool Shutdown() {
      _runtime.Stop(false);
      return _runtime.Running;
    }

    private void _mnu_help_arguments_Click(object sender, EventArgs e) {
      CommandLine cl = new CommandLine();
      MessageBox.Show(cl.GetHelp(), "qcv.exe", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void _mnu_save_filter_list_Click(object sender, EventArgs e) {
      if (this.saveFileDialog1.ShowDialog() == DialogResult.OK) {
        Base.FilterList.Save(this.saveFileDialog1.FileName, _fl);
      }
    }
  }
}
