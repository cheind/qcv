using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloImageList : IExample {

    public void Run(string[] args) {

      QCV.Toolbox.Sources.ImageList il = new QCV.Toolbox.Sources.ImageList();
      il.DirectoryPath = "../../etc/images";
      il.FilePattern = "*.png";
      il.Name = "source";
      il.Loop = false;

      QCV.Toolbox.ShowImage si = new QCV.Toolbox.ShowImage();
      si.BagName = "source";
 
      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(il);
      f.Add(si);

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();

      Dictionary<string, object> env = new Dictionary<string,object>() {
        {"interaction", new QCV.Base.ConsoleInteraction(runtime)}
      };

      runtime.FPS = 0.7;
      runtime.Run(f, env, -1);
      runtime.Shutdown();
    }
  }
}
