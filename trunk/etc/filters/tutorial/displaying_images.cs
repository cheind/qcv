// qcv.exe -s displaying_images.cs Tutorial.DisplayingImages

using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions; // ease access of bundle

// Image types are Emgu types
using Emgu.CV;
using Emgu.CV.Structure;

namespace Tutorial {

  [Addin]
  public class DisplayingImages : IFilter, IFilterListProvider {
    
    public FilterList CreateFilterList(AddinHost host) {
      
      return new FilterList() {
        // Camera with requested frame dimensions
        new QCV.Toolbox.Camera(0, 320, 200, "camera"),
        // Display
        this
      };
      
    }
    
    public void Execute(Dictionary<string, object> bundle) {
      // Fetch the interactor from the bundle
      IDataInteractor idi = bundle.FetchInteractor();
      // Fetch the current image from camera
      Image<Bgr, byte> image = bundle.FetchImage("camera");
      // Request showing the image.
      idi.Show("camera live feed", image);
    }
    
  }
}