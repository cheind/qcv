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
      si.BundleName = "source";

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(v);
      f.Add(si);

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.FPS = 30.0;

      Dictionary<string, object> env = new Dictionary<string, object>()
      {
        {"interactor", new QCV.Base.ConsoleDataInteractor(runtime)}
      };

      runtime.Run(f, env, 5);
      runtime.Shutdown();
    }
  }
}
