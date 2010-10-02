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
  public class HelloVisualization : IExample {

    public void Run(string[] args) {
      QCV.Toolbox.Sources.Camera c = new QCV.Toolbox.Sources.Camera();
      c.Name = "input 1";
      c.DeviceIndex = 0;

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(c);
      f.Add(
        new QCV.Base.AnonymousFilter(
          (b, ev) => {
            Image<Bgr, byte> i = b.FetchImage("input 1");
            i.Draw(new Rectangle(0, 0, i.Width, i.Height), new Bgr(Color.Green), 4);
            b.FetchInteraction().ShowImage("from input 1", i);
          })
      );

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();

      Dictionary<string, object> env = new Dictionary<string,object>() {
        {"interaction", new QCV.Base.ConsoleInteraction(runtime)}
      };
      runtime.FPS = 30.0;
      runtime.Run(f, env, 10);
    }
  }
}
