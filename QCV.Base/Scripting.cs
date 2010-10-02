using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using Microsoft.VisualC;
using System.Reflection;
using log4net;

namespace QCV.Base {
  [Serializable]
  public class Scripting {
    private static readonly ILog _logger = LogManager.GetLogger(typeof(Scripting));
    private List<CompilerResults> _results = new List<CompilerResults>();

    public IList<CompilerResults> CompilerResults {
      get { return _results; }
    }

    public IEnumerable<Assembly> CompiledAssemblies {
      get { return _results.Select((cr) => { return cr.CompiledAssembly; }); }
    }

    public bool Compile(IEnumerable<string> sources, IEnumerable<string> refs) {

      // Split sources into various languages
      IEnumerable<string> csharp = sources.Where(
        (s) => { return s.EndsWith(".cs", StringComparison.InvariantCultureIgnoreCase); }
      );

      IEnumerable<string> vb = sources.Where(
        (s) => { return s.EndsWith(".vb", StringComparison.InvariantCultureIgnoreCase); }
      );

      IEnumerable<string> cpp = sources.Where(
        (s) => { return s.EndsWith(".cpp", StringComparison.InvariantCultureIgnoreCase); }
      );

      CompilerParameters cp = new CompilerParameters(refs.ToArray());
      cp.GenerateExecutable = false;
      cp.GenerateInMemory = true;
      
      Dictionary<string, string> pp = new Dictionary<string, string>() {{"CompilerVersion", "v3.5"}};

      _results = new List<CompilerResults>();
      if (csharp.Count() > 0) {
        CSharpCodeProvider p = new CSharpCodeProvider();
        _results.Add(new CSharpCodeProvider(pp).CompileAssemblyFromFile(cp, csharp.ToArray()));
      }
      if (vb.Count() > 0) {
        _results.Add(new VBCodeProvider(pp).CompileAssemblyFromFile(cp, vb.ToArray()));
      }
      if (cpp.Count() > 0) {
        _results.Add(new CppCodeProvider().CompileAssemblyFromFile(cp, cpp.ToArray()));
      }

      bool success = _results.All((cr) => { return !cr.Errors.HasErrors; });

      if (success) {
        _logger.Info(FormatCompilerResults());
      } else {
        _logger.Error(FormatCompilerResults());
      }

      return success;
    }

    public String FormatCompilerResults() {
      StringBuilder sb = new StringBuilder();

      bool success = _results.All((cr) => { return !cr.Errors.HasErrors; });
      if (success) {
        sb.Append("Compilation succeeded");
      } else {
        sb.AppendLine("Compilation failed");
        foreach (CompilerResults cr in _results) {
          sb.Append(FormatErrors(cr));
        }
      }
      return sb.ToString();
    }

    public String FormatErrors(CompilerResults cr) {
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < cr.Errors.Count; i++)
        sb.AppendLine(i.ToString() + ": " + cr.Errors[i].ToString());

      return sb.ToString();
    }
  }
}

