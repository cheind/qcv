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

      QCV.Base.Compiler s = new QCV.Base.Compiler(
        new string[] { "QCV.Base.dll", "System.dll" },
        false
      );

      bool success = s.CompileFromFile(@"..\..\etc\scripts\say_hello.cs");
      Console.WriteLine(s.FormatCompilerResults());

      if (!success) {
        return;
      }

      QCV.Base.Addins.AddinHost h = new QCV.Base.Addins.AddinHost();
      h.DiscoverInAssembly(s.CompiledAssemblies);
      QCV.Base.IFilter say_hello = h.CreateInstance<QCV.Base.IFilter>("Scripts.SayHello");
      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(say_hello);
      
      QCV.Base.Runtime runtime = new QCV.Base.Runtime();

      Dictionary<string, object> env = new Dictionary<string,object>() {
        {"interactor", new QCV.Base.ConsoleDataInteractor(runtime)}
      };
      runtime.CycleTime.FPS = 1.0;
      runtime.Start(f, env, -1);
      runtime.Shutdown();
    }
  }
}

