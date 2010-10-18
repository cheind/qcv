using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using QCV.Base.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.ConsoleExample {

  [Base.Addin]
  public class HelloRecording : IExample {

    public void Run(string[] args) {
      QCV.Toolbox.Camera c = new QCV.Toolbox.Camera();
      c.Name = "camera";
      c.DeviceIndex = 0;
      c.FrameWidth = 640;
      c.FrameHeight = 480;

      QCV.Toolbox.ShowImage si = new QCV.Toolbox.ShowImage();
      si.BundleName = "camera";

      QCV.Toolbox.RecordVideo rv = new QCV.Toolbox.RecordVideo();
      rv.VideoPath = "myvideo.avi";
      rv.BagName = "camera";
      rv.FPS = 30;
      rv.FrameWidth = 160;
      rv.FrameHeight = 100;

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(c);
      f.Add(si);
      f.Add(rv);
        
      QCV.Base.Runtime runtime = new QCV.Base.Runtime();

      Dictionary<string, object> env = new Dictionary<string,object>() {
        {"interactor", new QCV.Base.ConsoleDataInteractor(runtime)}
      };

      runtime.CycleTime.FPS = 30.0;
      runtime.Start(f, env, 3);
    }
  }
}
