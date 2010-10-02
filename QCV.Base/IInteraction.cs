using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.Base {
  public interface IInteraction {
    void RuntimeStarted();
    void RuntimeStopped();
    void RuntimeShutdown();
    void ShowImage(string id, Image<Bgr, byte> image);
  }
}
