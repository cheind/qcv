using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloPersistedState : IExample {

    public void Run(string[] args) {

      // Part one, assembly the filter list

      QCV.Toolbox.Sources.Camera c = new QCV.Toolbox.Sources.Camera();
      c.Name = "input 1";
      c.DeviceIndex = 0;

      QCV.Toolbox.ShowImage si = new QCV.Toolbox.ShowImage();
      si.BagName = "input 1";
      si.ShowName = "image";
      
      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(c);
      f.Add(si);

      QCV.Base.FilterList.Save("filterlist.fl", f);

      f = null;
      f = QCV.Base.FilterList.Load("filterlist.fl");

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      runtime.FPS = 30.0;
      runtime.Run(f, new QCV.Base.ConsoleInteraction(), 10);
    }
  }
}
