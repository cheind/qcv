using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using System.IO;

namespace QCV {
  public class CommandLine {
    public class CLIArgs {
      public bool parsed_ok = true;
      public bool help = false;
      public bool immediate_execute = false;
      public List<string> filter_names = new List<string>();
      public List<string> load_paths = new List<string>();
      public List<string> script_paths = new List<string>();
      public List<string> assembly_paths = new List<string>();
    };

    private CLIArgs _args = new CLIArgs();
    private OptionSet _opts = null;

    public CommandLine() {
     _opts = new OptionSet() {
        { "a|assembly-path", "{PATH} containing additional filters", var => _args.assembly_paths.Add(var)},
        { "l|load=", "the {PATH} to load the filter list from", v => _args.load_paths.Add(v) },
        { "s|script=", "the {PATH} to a csharp script", v => _args.script_paths.Add(v) },
        { "r|run", "immediately start executing", v => _args.immediate_execute = v != null },
      };

      Parse();
    }

    public CLIArgs Args {
      get { return _args; }
    }

    public string GetHelp() {
      MemoryStream memoryStream = new MemoryStream();
      TextWriter tw = new StreamWriter(memoryStream);
      tw.WriteLine("qcv.exe - Quick OpenCV");
      tw.WriteLine("");
      tw.WriteLine("Usage: qcv.exe [OPTIONS] [FILTER, ...]");
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
        _args.filter_names = _opts.Parse(cli);
      } catch (OptionException) {}
    }
  }
}
