using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using QCV.Base.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloFPS : IExample {

    public void Run(string[] args) {
      QCV.Toolbox.Camera c = new QCV.Toolbox.Camera();
      c.Name = "input 1";
      c.DeviceIndex = 0;

      QCV.Toolbox.ShowFPS fps = new QCV.Toolbox.ShowFPS();

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(c);
      f.Add(
        new QCV.Base.AnonymousFilter(
          (b) => {
            Image<Bgr, byte> i = b.GetImage("input 1");
            i.Draw(new Rectangle(0, 0, i.Width, i.Height), new Bgr(Color.Green), 4);
            b.GetInteractor().Show("from input 1", i);
          })
      );
      f.Add(fps);

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();

      Dictionary<string, object> env = new Dictionary<string,object>() {
        {"interactor", new QCV.Base.ConsoleDataInteractor(runtime)}
      };

      runtime.CycleTime.FPS = 30.0;
      runtime.Run(f, env, 10);
      runtime.Shutdown();
    }
  }
}
