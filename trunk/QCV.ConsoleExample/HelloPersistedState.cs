using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {

  [Base.Addin]
  public class HelloPersistedState : IExample {

    public void Run(string[] args) {

      // Part one, assembly the filter list

      QCV.Toolbox.Camera c = new QCV.Toolbox.Camera();
      c.Name = "input 1";
      c.DeviceIndex = 0;
      c.FrameWidth = 320;

      QCV.Toolbox.ShowImage si = new QCV.Toolbox.ShowImage();
      si.BundleName = "input 1";
      si.ShowName = "image";
      
      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(c);
      f.Add(si);

      QCV.Base.FilterList.Save("filterlist.fl", f);

      f = null;
      f = QCV.Base.FilterList.Load("filterlist.fl");

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();

      Dictionary<string, object> env = new Dictionary<string,object>() {
        {"interactor", new QCV.Base.ConsoleDataInteractor(runtime)}
      };
      
      runtime.CycleTime.FPS = 30.0;
      runtime.Start(f, env, 10);
      runtime.Shutdown();
    }
  }
}
