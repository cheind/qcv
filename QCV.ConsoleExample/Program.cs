using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using QCV.Base;
using QCV.Toolbox;
using QCV.Base.Extensions;
using System.Drawing;
using System.Diagnostics;
using QCV.Toolbox.Sources;

namespace QCV.ConsoleExample {
  [Serializable]
  class Program {

    static void Main(string[] args) {
      Camera c = new Camera();
      c.Name = "input 1";
      c.DeviceIndex = 0;
      c.FrameWidth = 320;

      Video v = new Video();
      v.Name = "input 2";
      v.VideoPath = "Wildlife.wmv";
      v.Loop = true;
      c.FrameWidth = 320;
      c.FrameHeight = 200;

      FilterList f = new FilterList();
      f.Add(c);
      f.Add(v);
      f.Add(
        new AnonymousFilter(
          (b, ev) => {
            Image<Bgr, byte> i = b.FetchImage("input 1");
            b.FetchInteraction().ShowImage("a", i);
        })
      );

      f.Add(
        new AnonymousFilter(
          (b, ev) => {
            Image<Bgr, byte> i = b.FetchImage("input 2");
            b.FetchInteraction().ShowImage("b", i);
          })
      );

      Runtime runtime = new Runtime();
      runtime.FPS = 25.0;
      runtime.Run(f, new ConsoleInteraction(), 10);

    }
  }
}
