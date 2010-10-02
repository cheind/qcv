using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloVideo : IExample {

    public void Run(string[] args) {
      QCV.Toolbox.Sources.Video v = new QCV.Toolbox.Sources.Video();
      v.VideoPath = "../../etc/videos/a.avi";
      v.Name = "source";

      QCV.Toolbox.ShowImage si = new QCV.Toolbox.ShowImage();
      si.BagName = "source";

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(v);
      f.Add(si);

      QCV.Base.Runtime runtime = new QCV.Base.Runtime(
        new QCV.Base.ConsoleInteraction()
      );
      runtime.FPS = 30.0;
      runtime.Run(f, 5);
    }
  }
}
