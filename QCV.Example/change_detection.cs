// qcv.exe -s change_detection.cs Example.DetectChanges --run

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;

using QCV.Base;
using QCV.Base.Extensions;

using Emgu.CV;
using Emgu.CV.VideoSurveillance;
using Emgu.CV.Structure;

using Emgu.CV.CvEnum;
using log4net;

namespace Example {

  [Addin]
  public class DetectChanges : IFilter, IFilterListProvider {
    private static readonly ILog _logger = LogManager.GetLogger(typeof(DetectChanges));
    private FGDetector<Bgr> _detector = new FGDetector<Bgr>(FORGROUND_DETECTOR_TYPE.FGD_SIMPLE);
    private List<Rectangle> _blobs = new List<Rectangle>();

    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        host.CreateInstance<IFilter>("QCV.Toolbox.Camera", new object[]{0, 640, 480, "source"}),
        this,
        host.CreateInstance<IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"})
      };
    }

    public void OnUpdate(Dictionary<string, object> bundle) {
      Image<Bgr, byte> image = bundle.GetImage("source");
      Image<Bgr, byte> smoothed = image.SmoothGaussian(3);
      _detector.Update(smoothed);
      
      Image<Gray, Byte> fg_mask = _detector.ForgroundMask;
      Image<Gray, Byte> fg_mask_morph = fg_mask.Erode(1).Dilate(1);

      // Fetch the interactor from the bundle
      IDataInteractor idi = bundle.GetInteractor();
      idi.Show("forground", fg_mask_morph);

      // Find contours
      _blobs.Clear();
      for (Contour<Point> c = fg_mask_morph.FindContours(); c != null; c = c.HNext) {
        Rectangle r = c.BoundingRectangle;
        int area = r.Width * r.Height;
        if (area > 20) {
          _blobs.Add(r);
        }
      }

      while (MergeTwoRectangles()) {
        ;
      }

    }

    private bool MergeTwoRectangles() {
      for (int i = 0; i < _blobs.Count - 1; ++i) {
        for (int j = i + 1; j < _blobs.Count; ++j) {
          if (_blobs[i].IntersectsWith(_blobs[j]) ||
              _blobs[i].Contains(_blobs[j]) ||
              _blobs[j].Contains(_blobs[i])) 
          {
            Rectangle res = new Rectangle();
            res.X = Math.Min(_blobs[i].X, _blobs[j].X);
            res.Y = Math.Min(_blobs[i].Y, _blobs[j].Y);
            res.Width = Math.Max(_blobs[i].X + _blobs[i].Width, _blobs[j].X + _blobs[j].Width) - res.X;
            res.Height = Math.Max(_blobs[i].Y + _blobs[i].Height, _blobs[j].Y + _blobs[j].Height) - res.Y;
            _blobs[i] = res;
            _blobs.RemoveAt(j);
            return true;
          }
        }
      }
      return false;
    }

    public void Execute(Dictionary<string, object> b) {
      IDataInteractor idi = b.GetInteractor();
      // Process all pending events, supplying them with the current bundle information
      idi.ExecutePendingEvents(this, b);

      Image<Bgr, byte> image = b.GetImage("source");
      foreach (Rectangle r in _blobs) {
        image.Draw(r, new Bgr(Color.Green), 2);
      }

    }
  }
}