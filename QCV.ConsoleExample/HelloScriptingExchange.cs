using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using log4net;
using Emgu.CV;

namespace QCV.ConsoleExample {

  class ScriptingFilterList : Base.IFilterListProvider {

    public QCV.Base.FilterList CreateFilterList(QCV.Base.Addins.AddinHost h) {
      return new QCV.Base.FilterList() {
        h.CreateInstance<Base.IFilter>("QCV.Toolbox.Camera", new object[]{0, 320, 200, "source"}),
        h.CreateInstance<Base.IFilter>("Scripts.DrawRectangle"),
        h.CreateInstance<Base.IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"})
      };
    }
  }

  [Base.Addins.Addin]
  public class HelloScriptingExchange : IExample {
    public void Run(string[] args) {
      Console.WriteLine("Press any key to quit");

      string[] scripts = new string[]{
        @"..\..\etc\scripts\draw_rectangle.cs"
      };

      QCV.Base.InstantCompiler ic = new QCV.Base.InstantCompiler(
        scripts, new string[] {
        "QCV.Base.dll", "Emgu.CV.dll", "Emgu.Util.dll", 
        "System.dll", "System.Drawing.dll", "System.Xml.dll"},
        false);

      QCV.Base.Addins.AddinHost h = new QCV.Base.Addins.AddinHost();
      h.DiscoverInDomain();
      h.DiscoverInDirectory(Environment.CurrentDirectory);

      QCV.Base.FilterList fl = null;
      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.CycleTime.FPS = 30.0;

      Dictionary<string, object> env = new Dictionary<string, object>() {
        {"interactor", new QCV.Base.ConsoleDataInteractor(runtime)}
      };

      ic.BuildSucceededEvent += (s, ev) => {
        
        QCV.Base.Addins.AddinHost tmp = new QCV.Base.Addins.AddinHost();
        tmp.DiscoverInAssembly(ev.CompiledAssemblies);

        h.MergeByFullName(tmp);

        if (fl == null) {
          Base.IFilterListProvider provider = new ScriptingFilterList();
          fl = provider.CreateFilterList(h);
        } else {
          runtime.Stop(true);
          QCV.Base.Reconfiguration r = new QCV.Base.Reconfiguration();
          QCV.Base.FilterList fl_new = r.Update(fl, h);
          r.CopyPropertyValues(fl, fl_new);
          fl = fl_new;
        }

        runtime.Run(fl, env, 0);
      };

      ic.Compile();

      Console.ReadKey();

      runtime.Stop(true);
      runtime.Shutdown();
    }
  }
}

