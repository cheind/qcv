using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using log4net;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloScriptingError : IExample {
    public void Run(string[] args) {

      QCV.Base.Scripting s = new QCV.Base.Scripting(QCV.Base.Scripting.Language.CSharp);

      CompilerResults results = s.Compile(
        new string[] { @"..\..\etc\scripts\error.cs" },
        new string[] {});

      Console.WriteLine(s.FormatCompilerResults(results));
    }
  }
}

