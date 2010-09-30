using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.Base {

  public interface IRuntime {
    void Run(FilterList filters, double frame_rate, int wait);
    void Show(string id, Image<Bgr, byte> image, bool copy_image);
  };

  public class ConsoleRuntime : IRuntime {
    private HashSet<string> _known_windows = new HashSet<string>();

    public void Show(string name, Image<Bgr, byte> image, bool copy_image) {
      if (!_known_windows.Contains(name)) {
        _known_windows.Add(name);
        CvInvoke.cvNamedWindow(name);
      }

      CvInvoke.cvShowImage(name, copy_image ? image.Copy() : image);
      CvInvoke.cvWaitKey(1);
    }

    public void Run(FilterList filters, double frame_rate, int wait) {
      Pipeline p = new Pipeline();
      p.FrameRate = frame_rate;
      p.Run(filters, this, wait);
    }
  }
}
