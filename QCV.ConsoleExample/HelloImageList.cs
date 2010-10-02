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

      QCV.Base.Runtime runtime = new QCV.Base.Runtime(
        new QCV.Base.ConsoleInteraction()
      );
      runtime.FPS = 0.7;
      runtime.Run(f, -1);
    }
  }
}
