using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using log4net;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloScripting : IExample {
    public void Run(string[] args) {

      QCV.Base.Scripting s = new QCV.Base.Scripting(QCV.Base.Scripting.Language.CSharp);

      CompilerResults results = s.Compile(
        new string[]{ @"..\..\etc\scripts\say_hello.cs" },
        new string[]{"QCV.Base.dll", "System.dll"});

      Console.WriteLine(s.FormatCompilerResults(results));

      if (results.Errors.HasErrors) {
        return;
      }
          
      QCV.Base.Addins.AddinStore.DiscoverInAssembly(results.CompiledAssembly);
      QCV.Base.IFilter say_hello = QCV.Base.Addins.AddinStore.FindAndCreateInstance(
        typeof(QCV.Base.IFilter),
        "Scripts.SayHello") as QCV.Base.IFilter;

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(say_hello);
      
      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.FPS = 1.0;
      runtime.Run(f, new QCV.Base.ConsoleInteraction(), -1);
    }
  }
}

