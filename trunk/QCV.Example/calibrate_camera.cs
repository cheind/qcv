// qcv.exe -s calibrate_camera.cs Example.CalibrateCamera --run

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions;

using Emgu.CV;
using Emgu.CV.Structure;

namespace Example {

  [Addin]
  public class CalibrateCamera : IFilter, IFilterListProvider {
    private Size _nr_corners = new Size(9, 6);
    private float _field_size = 25;
    List<PointF[]> _image_points = new List<PointF[]>();
    List<MCvPoint3D32f[]> _object_points = new List<MCvPoint3D32f[]>();

    /// <summary>
    /// Get/set the number of inner cornser per row and column
    /// </summary>
    [Description("Number of inner corners per row and column")]
    public System.Drawing.Size Size {
      get { return _nr_corners; }
      set {
        _nr_corners = value;
      }
    }

    /// <summary>
    /// Get/set the size of single square in the checkerboard pattern
    /// </summary>
    [Description("The size of single square in the checkerboard pattern in units of your choice.")]
    public float SizeOfSquare {
      get { return _field_size; }
      set {_field_size = value; }
    }

    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        host.CreateInstance<IFilter>("QCV.Toolbox.Camera", new object[]{0, 640, 480, "source"}),
        this,
        host.CreateInstance<IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"})
      };
    }

    public void OnTakeImage(Dictionary<string, object> bundle) {
      if (bundle.Get<bool>("pattern_found")) {
        _image_points.Add(bundle.Get<PointF[]>("pattern_corners"));
        _object_points.Add(GenerateObjectCorners());
        bundle.GetInteractor().Show("Number of correspondences", _image_points.Count);
      }
    }

    public void OnCalibrate(Dictionary<string, object> bundle) {
      if (_image_points.Count >= 3) {
        IntrinsicCameraParameters icp = new IntrinsicCameraParameters();
        ExtrinsicCameraParameters[] ecp;

        Emgu.CV.CameraCalibration.CalibrateCamera(
          _object_points.ToArray(),
          _image_points.ToArray(),
          bundle.GetImage("source").Size,
          icp,
          Emgu.CV.CvEnum.CALIB_TYPE.DEFAULT,
          out ecp);

        bundle["intrinsics"] = icp;
      }
    }

    public void Execute(Dictionary<string, object> bundle) {
      // For illustration purposes, we detect the chessboard in each frame
      Image<Gray, byte> image = bundle.GetImage("source").Convert<Gray, byte>();
      PointF[] corners = null;

      bool found = CameraCalibration.FindChessboardCorners(
        image, _nr_corners,
        Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH |
        Emgu.CV.CvEnum.CALIB_CB_TYPE.FILTER_QUADS |
        Emgu.CV.CvEnum.CALIB_CB_TYPE.NORMALIZE_IMAGE,
        out corners
      );
      if (found) {
        image.FindCornerSubPix(
          new System.Drawing.PointF[][] { corners },
          new System.Drawing.Size(5, 5),
          new System.Drawing.Size(-1, -1),
          new MCvTermCriteria(0.001));
      }

      bundle["pattern_found"] = found;
      bundle["pattern_corners"] = corners;

      bundle.GetInteractor().ExecutePendingEvents(this, bundle);

      QCV.Toolbox.Draw.OrderedPointList(bundle.GetImage("source"), corners, found ? Color.Green : Color.Red);
      if (found && bundle.ContainsKey("intrinsics")) {
        IntrinsicCameraParameters icp = bundle.Get<IntrinsicCameraParameters>("intrinsics");

        ExtrinsicCameraParameters ecp = CameraCalibration.FindExtrinsicCameraParams2(
          GenerateObjectCorners(),
          corners,
          icp);

        QCV.Toolbox.Draw.DrawExtrinsicFrame(bundle.GetImage("source"), ecp, icp);
      }
    }

    /// <summary>
    /// Generate reference points
    /// </summary>
    private MCvPoint3D32f[] GenerateObjectCorners() {
      MCvPoint3D32f[] corners = new MCvPoint3D32f[_nr_corners.Width * _nr_corners.Height];
      for (int y = 0; y < _nr_corners.Height; ++y) {
        for (int x = 0; x < _nr_corners.Width; x++) {
          int id = y * _nr_corners.Width + x;
          corners[id] = new MCvPoint3D32f(x * _field_size, y * _field_size, 0);
        }
      }
      return corners;
    }
  }
}