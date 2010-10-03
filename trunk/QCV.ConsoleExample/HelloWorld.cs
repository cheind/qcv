using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {
  
  [Base.Addins.Addin]
  public class HelloWorld : IExample {

    public void Run(string[] args) {
      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(
        new QCV.Base.AnonymousFilter(
          (b, ev) => {
            Console.WriteLine("Frame received");
          })
      );

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.FPS = 1.0;
      runtime.Run(f, 10);
      runtime.Shutdown();
    }
  }
}
