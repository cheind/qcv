using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace QCV.Toolbox {

  public static class Draw {
    public static void OrderedPointList(
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

    /// <summary>
    /// Draw a visual indication of the pattern coordinate frame
    /// </summary>
    /// <param name="img">Image to draw to</param>
    /// <param name="ecp">Extrinsic calibration</param>
    /// <param name="icp">Intrinsic calibration</param>
    public static void DrawExtrinsicFrame(
                       Emgu.CV.Image<Bgr, Byte> img,
                       Emgu.CV.ExtrinsicCameraParameters ecp,
                       Emgu.CV.IntrinsicCameraParameters icp) 
    {
      float extension = img.Width / 10;
      PointF[] coords = Emgu.CV.CameraCalibration.ProjectPoints(
        new MCvPoint3D32f[] { 
          new MCvPoint3D32f(0, 0, 0),
          new MCvPoint3D32f(extension, 0, 0),
          new MCvPoint3D32f(0, extension, 0),
          new MCvPoint3D32f(0, 0, extension),
        },
        ecp, icp);

      img.Draw(new LineSegment2DF(coords[0], coords[1]), new Bgr(Color.Red), 2);
      img.Draw(new LineSegment2DF(coords[0], coords[2]), new Bgr(Color.Green), 2);
      img.Draw(new LineSegment2DF(coords[0], coords[3]), new Bgr(Color.Blue), 2);
    }

  }
}
