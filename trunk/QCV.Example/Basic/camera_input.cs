// qcv.exe -s camera_input.cs Example.Basic.CameraInput --run

using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Addins;

namespace Example.Basic {

  [Addin]
  public class CameraInput : IFilterListProvider {
    
    // Create a new FilterList containing a camera filter
    public FilterList CreateFilterList(AddinHost host) {
      
      QCV.Toolbox.Camera c = new QCV.Toolbox.Camera();
      c.DeviceIndex = 0;
      c.Name = "source";
      
      return new FilterList() {c};
    }

  }
}