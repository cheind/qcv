using System;
using System.Collections.Generic;
using QCV.Base.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace Scripts {

  [QCV.Base.Addins.Addin]
  public class DrawRectangleFilterList : QCV.Base.IFilterListProvider {

    public QCV.Base.FilterList CreateFilterList(QCV.Base.Addins.AddinHost h) {
      return new QCV.Base.FilterList() {
        h.CreateInstance<QCV.Base.IFilter>("QCV.Toolbox.Camera", new object[]{0, 320, 200, "source"}),
        h.CreateInstance<QCV.Base.IFilter>("Scripts.DrawRectangle"),
        h.CreateInstance<QCV.Base.IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"}),
        h.CreateInstance<QCV.Base.IFilter>("QCV.Toolbox.ShowFPS")
      };
    }
  }
}