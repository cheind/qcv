using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using log4net;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloScriptingExchange : IExample {
    public void Run(string[] args) {

      QCV.Base.Scripting s = new QCV.Base.Scripting();

      s.Compile(
        new string[] { @"..\..\etc\scripts\say_hello.cs" },
        new string[] { "QCV.Base.dll", "System.dll" });

      QCV.Base.Addins.AddinHost h0 = new QCV.Base.Addins.AddinHost();
      h0.DiscoverInAssembly(s.CompiledAssemblies);

      Console.ReadKey();

      s.Compile(
        new string[] { @"..\..\etc\scripts\say_hello.cs" },
        new string[] { "QCV.Base.dll", "System.dll" });

      QCV.Base.Addins.AddinHost h1 = new QCV.Base.Addins.AddinHost();
      h1.DiscoverInAssembly(s.CompiledAssemblies);

      QCV.Base.IFilter a = h0.FindAndCreateInstance(
        typeof(QCV.Base.IFilter),
        "Scripts.SayHello") as QCV.Base.IFilter;


      QCV.Base.IFilter b = h1.FindAndCreateInstance(
        typeof(QCV.Base.IFilter),
        "Scripts.SayHello") as QCV.Base.IFilter;

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(a);
      f.Add(b);

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.FPS = 1.0;
      runtime.Run(f, new QCV.Base.ConsoleInteraction(), -1);
    }
  }
}

