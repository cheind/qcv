using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using Microsoft.VisualC;
using System.Reflection;

namespace QCV.Base {
  public class Scripting {

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

      _results = new List<CompilerResults>();
      if (csharp.Count() > 0) {
        _results.Add(new CSharpCodeProvider().CompileAssemblyFromFile(cp, csharp.ToArray()));
      }
      if (vb.Count() > 0) {
        _results.Add(new VBCodeProvider().CompileAssemblyFromFile(cp, vb.ToArray()));
      }
      if (cpp.Count() > 0) {
        _results.Add(new CppCodeProvider().CompileAssemblyFromFile(cp, cpp.ToArray()));
      }

      return _results.All((cr) => { return !cr.Errors.HasErrors; });
    }

    public String FormatCompilerResults(IEnumerable<CompilerResults> results) {
      StringBuilder sb = new StringBuilder();
      foreach (CompilerResults cr in results) {
        sb.Append(FormatCompilerResults(cr));
      }
      return sb.ToString();
    }

    public String FormatCompilerResults(CompilerResults cr) {
      StringBuilder sb = new StringBuilder();

      if (cr.Errors.Count > 0) {
        sb.AppendLine("Build failed");
        for (int i = 0; i < cr.Errors.Count; i++)
          sb.AppendLine(i.ToString() + ": " + cr.Errors[i].ToString());
      } else {
        sb.AppendLine("Build succeeded");
        sb.AppendLine("Compiler returned with result code: " + cr.NativeCompilerReturnValue.ToString());
      }

      return sb.ToString();
    }
  }
}

