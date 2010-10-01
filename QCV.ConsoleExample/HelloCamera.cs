using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloCamera : IExample {

    public void Run(string[] args) {

      QCV.Toolbox.Sources.Camera c = new QCV.Toolbox.Sources.Camera();
      c.DeviceIndex = 0;
      c.FrameWidth = 320;
      c.FrameHeight = 200;
      c.Name = "source";

      QCV.Toolbox.ShowImage si = new QCV.Toolbox.ShowImage();
      si.BagName = "source";

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(c);
      f.Add(si);

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.FPS = 30.0;
      runtime.Run(f, new QCV.Base.ConsoleInteraction(), 5);
    }
  }
}
