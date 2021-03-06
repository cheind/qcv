﻿// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

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
using QCV.Base.Extensions;
using System.Reflection;

namespace QCV {
  public partial class Main : Form {

    private static readonly ILog _logger = LogManager.GetLogger(typeof(Main));
    private HookableTextWriter _console_hook = new HookableTextWriter();
    private Base.MultipleFileWatcher _watcher = new Base.MultipleFileWatcher();
    private Base.Compilation.AggregateCompiler _aggregate_compiler = null;
    private Base.AddinHost _ah = null;
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

      _fl_settings.EventCache = _ev_cache;

      string qcv_path = Path.GetDirectoryName(Application.ExecutablePath);

      // Redirect console output
      _console_hook.StringAppendedEvent += new HookableTextWriter.StringAppendedEventHandler(ConsoleStringAppendedEvent);
      Console.SetOut(_console_hook);

      // Configure logging
      XmlConfigurator.Configure(new System.IO.FileInfo(Path.Combine(qcv_path, "QCV.log4net")));

      // Write informative startup info
      DisplayStartupMessage();

      // Parse command line
      CommandLine cl = new CommandLine();
      _args = cl.Args;

      if (_args.help) {
        Console.WriteLine(cl.GetHelp());
      }

      // Register all script paths for change events
      _watcher.Changed += new FileSystemEventHandler(SourceFileChanged);
      _watcher.AddFilePaths(_args.source_paths);

      // Setup the aggregate compiler
      Base.Compilation.CompilerSettings cs = new Base.Compilation.CompilerSettings();
      cs.AssemblyReferences = _args.compile_references.Union(new string[] { 
            "mscorlib.dll", "System.dll", 
            "System.Drawing.dll", "System.Design.dll", 
            "System.Xml.dll",
            Path.Combine(qcv_path, "QCV.Base.dll"), 
            Path.Combine(qcv_path, "QCV.Toolbox.dll"), 
            Path.Combine(qcv_path, "Emgu.CV.dll"), 
            Path.Combine(qcv_path, "Emgu.Util.dll"), 
            Path.Combine(qcv_path, "log4net.dll")}).Distinct();

      cs.FrameworkVersion = Base.TargetFramework.Version;
      cs.DebugInformation = _args.enable_debugger;

      _aggregate_compiler = new Base.Compilation.AggregateCompiler(cs);
      _aggregate_compiler.AddCompiler(typeof(Base.Compilation.CSharpCompiler));
      _aggregate_compiler.AddCompiler(typeof(Base.Compilation.VBCompiler));

      IEnumerable<string> cannot_compile = _args.source_paths.Where((s) => !_aggregate_compiler.CanCompileFile(s));
      if (cannot_compile.Any()) {
        _logger.Warn(String.Format("The following files cannot be compiled {0}", String.Join(",", cannot_compile)));
      }

      // Setup runtime

      _env = new Dictionary<string, object>() {
        {"interactor", this}
      };

      _runtime = new QCV.Base.Runtime();
      _runtime.RuntimeStartingEvent += new EventHandler(RuntimeStartingEvent);
      _runtime.RuntimeStoppedEvent += new EventHandler(RuntimeStoppedEvent);
      
      _nrc_fps.Value = (Decimal)_args.target_fps;
      SetCycleTime(_args.target_fps);

      // Initial compilation
      CompileAndUpdate();
    }

    private void DisplayStartupMessage() {
      Version qcv_version = Assembly.GetExecutingAssembly().GetName().Version;
      Version net_version = Base.TargetFramework.Version;
      _logger.Info(
        String.Format(
          "QCV v{0}.{1} built for .NET v{2}.{3} is starting",
          new object[] { qcv_version.Major, qcv_version.Minor, net_version.Major, net_version.Minor }
      ));
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
      } else if (text.StartsWith("ERROR") || text.StartsWith("FATAL")) {
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

    private void SourceFileChanged(object sender, FileSystemEventArgs e) {
      CompileAndUpdate();
    }

    private void CompileAndUpdate() {
      lock (_aggregate_compiler) {
        Base.Compilation.ICompilerResults results = _aggregate_compiler.CompileFiles(_args.source_paths);
        if (results.Success) {
          try {
            UpdateFilterList(results);
          } catch (Exception ex) {
            _logger.Error(String.Format("Failed to update/create filter list - {0}", ex.Message));
          }
        }
      }
    }

    private void UpdateFilterList(QCV.Base.Compilation.ICompilerResults results) {
      bool running = _runtime.Running;
      if (running) {
        _query_form.Cancel();
        _runtime.Stop(true);
      }

      QCV.Base.AddinHost tmp = new QCV.Base.AddinHost();
      tmp.DiscoverInAssembly(results.GetCompiledAssemblies());


      if (_fl == null) {

        _ah = new QCV.Base.AddinHost();
        _ah.DiscoverInDomain();
        _ah.DiscoverInDirectory(Path.GetDirectoryName(Application.ExecutablePath));
        _ah.DiscoverInDirectory(Environment.CurrentDirectory);
        _ah.MergeByFullName(tmp);

        // First run
        _fl = new QCV.Base.FilterList();
        IEnumerable<Type> providers = _ah.FindAddins(
            typeof(Base.IFilterListProvider),
            (ai) => (ai.IsDefaultConstructible() && _args.filterlist_providers.Contains(ai.FullName)));

        int target_prov_count = _args.filterlist_providers.Count;
        int performance_prov_count = providers.Count();
        if (target_prov_count > performance_prov_count) {
          _logger.Warn("Not all specified IFilterListProvider were found.");
        }

        foreach (Type addin in providers) {
          Base.IFilterListProvider p = _ah.CreateInstance(addin) as Base.IFilterListProvider;
          try {
            _fl.AddRange(p.CreateFilterList(_ah));
          } catch (Exception ex) {
            _logger.Error(String.Format("Error raised while creating filter list {0} - {1}", p.GetType().FullName, ex.Message));
          }
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
          _runtime.Start(_fl, _env, 0);
        }

      } else {
        _ah.MergeByFullName(tmp);
        // Subsequent runs
        QCV.Base.Reconfiguration r = new QCV.Base.Reconfiguration();
        QCV.Base.FilterList fl_new = r.Update(_fl, _ah);
        r.CopyPropertyValues(_fl, fl_new);
        _fl = fl_new;
      }

      _fl_settings.GenerateUI(_fl);

      if (running) {
        _runtime.Start(_fl, _env, 0);
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
        _runtime.Start(_fl, _env, 0);
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
      } else {
        _runtime.CycleTime.Enabled = true;
        _runtime.CycleTime.FPS = fps;
      }
    }
  }
}
