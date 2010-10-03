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
      public List<string> filterlist_providers = new List<string>();
      public List<string> load_paths = new List<string>();
      public List<string> script_paths = new List<string>();
      public List<string> assembly_paths = new List<string>();
      public List<string> references = new List<string>();
    };

    private CLIArgs _args = new CLIArgs();
    private OptionSet _opts = null;

    public CommandLine() {
     _opts = new OptionSet() {
        { "a|assembly-path=", "load assembly from {PATH}", 
          v => _args.assembly_paths.AddRange(Base.Globbing.Glob(v))},
        { "r|reference=", "add the named {ASSEMBLY} as reference", 
          v => _args.references.Add(v)},
        { "l|load=", "load persisted FilterList from {PATH}", 
          v => _args.load_paths.AddRange(Base.Globbing.Glob(v)) },
        { "s|script=", "compile the file pointed to by {PATH} and load as possible addin.", 
          v => _args.script_paths.AddRange(Base.Globbing.Glob(v)) },
        { "run", "immediately start executing", 
          v => _args.immediate_execute = v != null },
      };

      Parse();
      _args.load_paths = new List<string>(_args.load_paths.Distinct());
      _args.assembly_paths = new List<string>(_args.assembly_paths.Distinct());
      _args.script_paths = new List<string>(_args.script_paths.Distinct());
      _args.references = new List<string>(_args.references.Distinct());
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
      } catch (OptionException) {}
    }
  }
}
