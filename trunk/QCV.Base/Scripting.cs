using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using Microsoft.VisualC;

namespace QCV.Base {
  public class Scripting {
    
    public enum Language {
      CSharp,
      VisualBasic,
      Cpp
    }

    private CodeDomProvider _provider;

    public Scripting(Language language) {
      switch (language) {
        case Language.CSharp :
          _provider = new CSharpCodeProvider();
          break;
        case Language.VisualBasic:
          _provider = new VBCodeProvider();
          break;
        case Language.Cpp:
          _provider = new CppCodeProvider();
          break;
      }
    }

    public CompilerResults Compile(IEnumerable<string> sources, IEnumerable<string> refs) {

      CompilerParameters props = new CompilerParameters(refs.ToArray());
      props.GenerateExecutable = false;
      props.GenerateInMemory = true;
      
      return _provider.CompileAssemblyFromFile(props, sources.ToArray());
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
