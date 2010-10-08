using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloImageList : IExample {

    public void Run(string[] args) {

      QCV.Toolbox.ImageList il = new QCV.Toolbox.ImageList();
      il.DirectoryPath = "../../etc/images";
      il.FilePattern = "*.png";
      il.Name = "source";
      il.Loop = false;

      QCV.Toolbox.ShowImage si = new QCV.Toolbox.ShowImage();
      si.BundleName = "source";
 
      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(il);
      f.Add(si);

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();

      Dictionary<string, object> env = new Dictionary<string,object>() {
        {"interactor", new QCV.Base.ConsoleDataInteractor(runtime)}
      };

      runtime.CycleTime.FPS = 0.7;
      runtime.Run(f, env, -1);
      runtime.Shutdown();
    }
  }
}
