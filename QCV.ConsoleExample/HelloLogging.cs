using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using QCV.Base.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;
using log4net;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloLogging : IExample {

    public void Run(string[] args) {

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(
        new QCV.Base.AnonymousFilter(
          (b) => {
            ILog l = b.GetDefaultLogger();
            l.Debug("A debugging greeting message!");
            b["cancel"] = true;
          })
      );

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.CycleTime.FPS = 30.0;
      runtime.Run(f, 10);
    }
  }
}
