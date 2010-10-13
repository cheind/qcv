﻿// qcv.exe -s image_decorator.cs Example.ImageDecorator --run

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

    public void Execute(Dictionary<string, object> bundle) {
      Image<Bgr, byte> image = bundle.GetImage("source");
      image.Draw(new Rectangle(Point.Empty, image.Size), new Bgr(Color.Red), 10);

      IDataInteractor idi = bundle.GetInteractor();
      idi.Show("camera input", image);
    }
  }
}