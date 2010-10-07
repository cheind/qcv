// qcv.exe -s multiple_image_input.cs Tutorial.MultipleImageInput
using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Addins;

namespace Tutorial {

  [Addin]
  public class MultipleImageInput : IFilterListProvider {
    
    // Create a new FilterList containing multiple image sources
    public FilterList CreateFilterList(AddinHost host) {
      
      return new FilterList() {
        // Camera with requested frame dimensions
        new QCV.Toolbox.Camera(0, 320, 200, "camera"),
        // Video from disk - looping enabled
        new QCV.Toolbox.Video("../../etc/videos/a.avi", "video", true),
        // Image list from disk - looping enabled
        new QCV.Toolbox.ImageList("../../etc/images", "*.png", "images", true)
      };
      
    }
    
  }
}