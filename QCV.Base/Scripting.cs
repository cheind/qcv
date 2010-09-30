using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace QCV.Base {
  public class Scripting {

    public static CompilerResults Compile(IEnumerable<string> sources, IEnumerable<string> refs) {

      CSharpCodeProvider provider = new CSharpCodeProvider();

      CompilerParameters props = new CompilerParameters(refs.ToArray());
      props.GenerateExecutable = false;
      props.GenerateInMemory = true;
      
      return provider.CompileAssemblyFromFile(props, sources.ToArray());
    }


  }
}
