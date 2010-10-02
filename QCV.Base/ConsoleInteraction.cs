using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.Base {
  public class ConsoleInteraction : IInteraction {
    private HashSet<string> _known_windows = new HashSet<string>();
    private ThreadedWorker _w = new ThreadedWorker();

    public void ShowImage(string id, Image<Bgr, byte> image) {

      _w.Invoke(() => {
        if (!_known_windows.Contains(id)) {
          _known_windows.Add(id);
          CvInvoke.cvNamedWindow(id);
        }

        CvInvoke.cvShowImage(id, image.Copy());
        CvInvoke.cvWaitKey(1);
        return null;
      });
    }

    public void RuntimeStarted() {
      _w.Start();
    }

    public void RuntimeShutdown() {
      _w.Stop();
    }

    public void RuntimeStopped() {}
  }
}
