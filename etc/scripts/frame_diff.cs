using System;
using System.Collections.Generic;
using System.Drawing;

using Emgu.CV;
using Emgu.CV.Structure;
using QCV.Base.Extensions;

namespace Scripts {

  [QCV.Base.Addins.Addin]
  public class SimpleFrameDiff : QCV.Base.IFilter {
    
    Emgu.CV.Image<Gray, byte> prev = null;
    Emgu.CV.Image<Gray, byte> last_stable = null;
    Emgu.CV.Image<Gray, byte> current_stable = null;
    DateTime stable_start = DateTime.Now;
    bool is_in_change = true;
    
    
    public void Execute(Dictionary<string, object> b, System.ComponentModel.CancelEventArgs e) {
      Image<Bgr, byte> image = b.FetchImage("source");
      Image<Gray, byte> g = image.Convert<Gray, byte>();

      if (prev != null) {
        Image<Gray, byte> d = DiffImage(prev, g);
        double dp = DiffPercent(d);
        if (dp > 0.0) {

          if (!is_in_change)
            last_stable = current_stable;

          is_in_change = true;
          image.Draw(new Rectangle(0, 0, image.Size.Width, image.Size.Height), new Bgr(Color.Red), 2);
        } else {
          if (is_in_change)
            stable_start = DateTime.Now;
          is_in_change = false;

          if ((DateTime.Now - stable_start).TotalSeconds > 1.0) {
            image.Draw(new Rectangle(0, 0, image.Size.Width, image.Size.Height), new Bgr(Color.Green), 2);
            if (last_stable != null) {
              d = DiffImage(last_stable, g);
              Emgu.CV.Contour<Point> c = d.FindContours();
              while (c != null) {
                Rectangle r = c.BoundingRectangle;
                image.Draw(r, new Bgr(Color.Green), 2);
                c = c.HNext;
              }
            }
            current_stable = g;
          }
        }
      }
      prev = g;
    }
    
    double DiffPercent(Image<Gray, byte> d) {
      return d.CountNonzero()[0] / (1.0 * d.Size.Height * d.Size.Width);
    }

    Image<Gray, byte> DiffImage(Image<Gray, byte> a, Image<Gray, byte> b) {
      Image<Gray, byte> d = a.AbsDiff(b);
      d._ThresholdBinary(new Gray(30.0), new Gray(255.0));
      d._Erode(2);
      d._Dilate(4);
      return d;
    }
  }
}