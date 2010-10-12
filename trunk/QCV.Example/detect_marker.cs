// qcv.exe -s detect_marker.cs Example.DetectMarker --run

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Design;
using Emgu.CV.CvEnum;

namespace Example {

  [Addin]
  public class DetectMarker : IFilter, IFilterListProvider {
    private Image<Bgr, byte> _marker = null;
    private Image<Gray, byte> _binary_marker = null;
    private Image<Gray, byte> _warped = null, _tmp = null;
    private Matrix<double> _warp_matrix = new Matrix<double>(3, 3);
    private PointF[] _warp_dest = null;
    private int _binary_threshold = 40;
    private double _max_error_normed = 0.4;

    public DetectMarker() {
    }

    /// <summary>
    /// Get or set the marker image to detect.
    /// </summary>
    [Description("Get or set the marker image to detect.")]
    [Editor(typeof(QCV.Toolbox.ImageTypeEditor), typeof(UITypeEditor))]
    public Image<Bgr, byte> MarkerImage {
      get { return _marker; }
      set {
        _marker = value; UpdateMarker();
      }
    }

    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        host.CreateInstance<IFilter>("QCV.Toolbox.Camera", new object[]{0, 640, 480, "source"}),
        this,
        host.CreateInstance<IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"})
      };
    }

    public void Execute(Dictionary<string, object> b) {
      // As long as no marker image has been set
      if (_marker == null) {
        return;
      }

      Image<Bgr, byte> image = b.GetImage("source");
      Image<Gray, byte> gray_input = image.Convert<Gray, byte>();
      // 1. Find contours
      Contour<Point> contour_points;
      using (Image<Gray, byte> binary = new Image<Gray, byte>(image.Size)) {
        CvInvoke.cvThreshold(gray_input, binary, _binary_threshold, 255, THRESH.CV_THRESH_BINARY | THRESH.CV_THRESH_OTSU);
        binary._Not(); // Contour is searched on white points, marker envelope is black.
        contour_points = binary.FindContours();
      }

      // 2. Analyse content of contours to detect marker
      PointF[] marker_corners = FindMarker(contour_points, gray_input);
      b["marker_corners"] = marker_corners;

      // Attribute image to show detection result
      if (marker_corners != null) {
        QCV.Toolbox.Drawing.Points.OrderedList(image, marker_corners, Color.Green);
      }
    }

    private PointF[] FindMarker(Contour<Point> contour_points, Image<Gray, byte> image) {
      bool marker_found = false;
      double best_error = _max_error_normed;
      PointF[] corners = new PointF[4];

      lock (_marker) {
        MemStorage contour_storage = new MemStorage();
        while (contour_points != null) {
          // Approximate contour points by poly-lines.
          Contour<Point> c = contour_points.ApproxPoly(contour_points.Perimeter * 0.05, contour_storage);
          if (c.Total == 4 && c.Perimeter > 200) {

            // Warp content of poly-line as if looking at it from the top
            PointF[] warp_source = new PointF[] { 
              new PointF(c[0].X, c[0].Y),
              new PointF(c[1].X, c[1].Y),
              new PointF(c[2].X, c[2].Y),
              new PointF(c[3].X, c[3].Y)
            };

            CvInvoke.cvGetPerspectiveTransform(warp_source, _warp_dest, _warp_matrix);
            CvInvoke.cvWarpPerspective(
              image, _warped, _warp_matrix,
              (int)INTER.CV_INTER_CUBIC + (int)WARP.CV_WARP_FILL_OUTLIERS,
              new MCvScalar(0)
            );
            CvInvoke.cvThreshold(_warped, _warped, _binary_threshold, 255, THRESH.CV_THRESH_BINARY | THRESH.CV_THRESH_OTSU);

            // Perform a template matching with the stored pattern in order to
            // determine if content of the envelope matches the stored pattern and
            // determine the orientation of the pattern in the image.
            // Orientation is encoded
            // 0: 0°, 1: 90°, 2: 180°, 3: 270°
            double error;
            int orientation;
            MatchPattern(out error, out orientation);

            if (error < best_error) {
              best_error = error;
              int id_0 = orientation;
              int id_1 = (orientation + 1) % 4;
              int id_2 = (orientation + 2) % 4;
              int id_3 = (orientation + 3) % 4;

              // ids above are still counterclockwise ordered, we need to permute them
              // 0   3    0   1
              // +---+    +---+
              // |   | -> |   |
              // +---+    +---+
              // 1   2    2   3

              corners[0] = c[id_0];
              corners[1] = c[id_3];
              corners[2] = c[id_1];
              corners[3] = c[id_2];

              marker_found = true;
            }
          }
          contour_points = contour_points.HNext;
        }
        
        if (marker_found) {
          return corners;
        } else {
          return null;
        }

      }
    }

    private void MatchPattern(out double error, out int orientation) {
      // 0 degrees
      orientation = 0;
      error = _warped.MatchTemplate(_binary_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;

      // 90 degrees
      CvInvoke.cvTranspose(_warped, _tmp);
      CvInvoke.cvFlip(_tmp, IntPtr.Zero, 1); // y-axis 
      double err = _tmp.MatchTemplate(_binary_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (err < error) {
        error = err;
        orientation = 1;
      }

      // 180 degrees
      CvInvoke.cvFlip(_warped, _tmp, -1);
      err = _tmp.MatchTemplate(_binary_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (err < error) {
        error = err;
        orientation = 2;
      }

      // 270 degrees
      CvInvoke.cvTranspose(_warped, _tmp);
      CvInvoke.cvFlip(_tmp, IntPtr.Zero, 0); // x-axis 
      err = _tmp.MatchTemplate(_binary_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (err < error) {
        error = err;
        orientation = 3;
      }
    }

    /// <summary>
    /// Update internal memory structure
    /// </summary>
    private void UpdateMarker() {
      if (_marker == null)
        return;
      lock (_marker) {
        _binary_marker = _marker.Convert<Gray, byte>();
        int width = _binary_marker.Width;

        // Warp points are specified in counter-clockwise order
        // since cvApproxpoly seems to return poly-points in counter-clockwise order.

        _warp_dest = new PointF[] { 
          new PointF(0, 0),
          new PointF(0, width),
          new PointF(width, width),
          new PointF(width, 0)
        };

        // Storage to hold warped image
        _warped = new Image<Gray, byte>(width, width);
        // Storage to test orientations
        _tmp = new Image<Gray, byte>(width, width);
      }
    }
  }
}