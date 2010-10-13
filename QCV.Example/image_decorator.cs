// qcv.exe -s image_decorator.cs Example.ImageDecorator --run

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
  public class ImageDecorator : IFilter, IFilterListProvider {

    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        new QCV.Toolbox.Camera(0, 640, 480, "source"),
        this,
      };
    }

    private int _thickness = 10;
    [Description("Specifies the thickness of the border drawn.")]
    public int Thickness {
      get { return _thickness; }
      set { _thickness = value; }
    }

    private Color _color = Color.Red;
    [Description("Specifies the fill color of the border.")]
    public Color Color {
      get { return _color; }
      set { _color = value; }
    }

    public void Execute(Dictionary<string, object> bundle) {
      Image<Bgr, byte> image = bundle.GetImage("source");
      image.Draw(new Rectangle(Point.Empty, image.Size), new Bgr(_color), _thickness);

      IDataInteractor idi = bundle.GetInteractor();
      idi.Show("camera input", image);
    }
  }
}