using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using System.IO;
using log4net;

namespace QCV {
  public class CommandLine {
    private static readonly ILog _logger = LogManager.GetLogger(typeof(CommandLine));

    public class CLIArgs {
      public bool help = false;
      public bool immediate_execute = false;
      public bool auto_shutdown = false;
      public bool enable_debugger = false;
      public double target_fps = 30;
      public List<string> filterlist_providers = new List<string>();
      public List<string> load_filterlist_paths = new List<string>();
      public List<string> source_paths = new List<string>();
      public List<string> addin_paths = new List<string>();
      public List<string> compile_references = new List<string>();
    };

    private CLIArgs _args = new CLIArgs();
    private OptionSet _opts = null;

    public CommandLine() {
     _opts = new OptionSet() {
        { "a|addin-path=", "load addins from assembly located at {PATH}", 
          v => _args.addin_paths.AddRange(Base.Globbing.Glob(v))},
        { "r|reference=", "provide {ASSEMBLY} as reference for compilation", 
          v => _args.compile_references.Add(v)},
        { "l|load=", "load persisted FilterList from {PATH}", 
          v => _args.load_filterlist_paths.AddRange(Base.Globbing.Glob(v)) },
        { "s|source=", "compile {PATH} and load as possible addin", 
          v => _args.source_paths.AddRange(Base.Globbing.Glob(v)) },
        { "run", "immediately start executing", 
          v => _args.immediate_execute = v != null },
        { "shutdown", "automatically exit application when runtime stops", 
          v => _args.auto_shutdown = v != null },
        { "debug", "enable attaching a debugger", 
          v => _args.enable_debugger = v != null },
        { "fps=", "target FPS to achieve", 
          v => _args.target_fps = Double.Parse(v) },
        { "nofps", "disable FPS control", 
          v => _args.target_fps = 0.0 },
        { "h|help", "show this help", 
          v => _args.help = v != null }
      };

      Parse();
      _args.load_filterlist_paths = new List<string>(_args.load_filterlist_paths.Distinct());
      _args.addin_paths = new List<string>(_args.addin_paths.Distinct());
      _args.source_paths = new List<string>(_args.source_paths.Distinct());
      _args.compile_references = new List<string>(_args.compile_references.Distinct());
      _args.target_fps = Math.Max(0.1, _args.target_fps);
    }

    public CLIArgs Args {
      get { return _args; }
    }

    public string GetHelp() {
      MemoryStream memoryStream = new MemoryStream();
      TextWriter tw = new StreamWriter(memoryStream);
      tw.WriteLine("qcv.exe - Quick Computer Vision");
      tw.WriteLine("");
      tw.WriteLine("Usage: qcv.exe [OPTIONS] IFilterListProvider [,IFilterListProvider]*");
      tw.WriteLine("Options:");
      _opts.WriteOptionDescriptions(tw);
      tw.Flush();
      memoryStream.Position = 0;
      StreamReader r = new StreamReader(memoryStream);
      return r.ReadToEnd();
    }

    private void Parse() {
      try {
        List<string> cli = Environment.GetCommandLineArgs().ToList();
        cli.RemoveAt(0);
        _args.filterlist_providers = _opts.Parse(cli);
      } catch (Exception err) {
        _logger.Error(String.Format("Failed to parse command line arguments - {0}", err.Message));
        _logger.Info("Use qcv.exe --help or browse to http://code.google.com/p/qcv/");
        _args = new CLIArgs();
      }
    }
  }
}
