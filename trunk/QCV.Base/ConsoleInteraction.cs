using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;

namespace QCV.Base {
  public class ConsoleInteraction : IInteraction {
    private HashSet<string> _known_windows = new HashSet<string>();

    public void ShowImage(string id, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image) {
       if (!_known_windows.Contains(id)) {
        _known_windows.Add(id);
        CvInvoke.cvNamedWindow(id);
      }

      CvInvoke.cvShowImage(id, image.Copy());
      CvInvoke.cvWaitKey(1);
    }
  }
}
