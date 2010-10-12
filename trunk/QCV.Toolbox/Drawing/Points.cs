using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace QCV.Toolbox.Drawing {
  public static class Points {
    public static void OrderedList(
                       Image<Bgr, byte> image, 
                       IEnumerable<PointF> points, 
                       System.Drawing.Color color) 
    {
      Bgr bgr = new Bgr(color);
      MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN, 0.8, 0.8);
      int count = 1;
      foreach (PointF point in points) {
        image.Draw(new CircleF(point, 4), bgr, 2);
        Point p = new Point((int)Math.Round(point.X), (int)Math.Round(point.Y));
        image.Draw(count.ToString(), ref f, new System.Drawing.Point(p.X + 5, p.Y - 5), bgr);
        count++;
      }
    }

  }
}
