using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using log4net;

namespace QCV.ConsoleExample {

  [Base.Addin]
  public class HelloScriptingError : IExample {
    public void Run(string[] args) {

      QCV.Base.Compiler s = new QCV.Base.Compiler();

      bool success = s.CompileFromFile(@"..\..\etc\scripts\error.cs");
        
      if (!success) {
        Console.WriteLine("Errors during compilation!");
      }

      Console.WriteLine(s.FormatCompilerResults());

    }
  }
}

