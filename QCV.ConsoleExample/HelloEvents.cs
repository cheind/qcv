using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using QCV.Base.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Reflection;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  class HelloEventFilter : QCV.Base.IFilter {

    public void OnCancel(Dictionary<string, object> b) {
      b.GetRuntime().RequestStop();
    }

    public void Execute(Dictionary<string, object> b) {
      b.GetInteractor().ExecutePendingEvents(this, b);
    }
  }


  [Base.Addins.Addin]
  public class HelloEvents : IExample {

    public void Run(string[] args) {

      QCV.Base.FilterList fl = new QCV.Base.FilterList()
      {
        new QCV.Toolbox.Camera(0),
        new HelloEventFilter()
      };

      
      
      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      QCV.Base.ConsoleDataInteractor i = new QCV.Base.ConsoleDataInteractor(runtime);

      Dictionary<string, object> env = new Dictionary<string, object>() {
        {"interactor", i}
      };

      runtime.CycleTime.FPS = 30.0;
      runtime.Start(fl, env, 0);

      Console.WriteLine("Press any key to trigger event");
      Console.ReadKey();

      MethodInfo[] mi = QCV.Base.MethodInfoScanner.FindEventMethods(fl[1]);
      i.CacheEvent(fl[1], mi[0]);
      
      runtime.Shutdown();

    }
  }
}
