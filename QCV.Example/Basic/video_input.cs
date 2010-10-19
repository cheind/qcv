// qcv.exe -s video_input.cs Example.Basic.VideoInput --run

using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Extensions;

namespace Example.Basic {

  [Addin]
  public class VideoInput : IFilter, IFilterListProvider {

    public FilterList CreateFilterList(AddinHost host) {

      QCV.Toolbox.Camera c = new QCV.Toolbox.Camera();
      c.DeviceIndex = 0;
      c.Name = "source";

      return new FilterList() {
        new QCV.Toolbox.Video(@"..\..\etc\videos\a.avi", "source"),
        this
      };
    }

    public void Execute(Dictionary<string, object> bundle) {
      bundle.GetInteractor().Show("video", bundle.GetImage("source"));
    }

  }
}