using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {
  
  [Base.Addins.Addin]
  public class HelloWorld : IExample {

    public void Run(string[] args) {
      QCV.Toolbox.Sources.Camera c = new QCV.Toolbox.Sources.Camera();
      c.Name = "input 1";
      c.DeviceIndex = 0;

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(c);
      f.Add(
        new QCV.Base.AnonymousFilter(
          (b, ev) => {
            Console.WriteLine("Frame received");            
          })
      );

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.FPS = 1.0;
      runtime.Run(f, new QCV.Base.ConsoleInteraction(), 10);
    }
  }
}
