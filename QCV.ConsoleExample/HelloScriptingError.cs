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

      QCV.Base.Scripting s = new QCV.Base.Scripting();

      bool success = s.Compile(
        new string[] { @"..\..\etc\scripts\error.cs" },
        new string[] { "System.dll"});

      if (!success) {
        Console.WriteLine("Errors during compilation!");
      }

      Console.WriteLine(s.FormatCompilerResults());

    }
  }
}

