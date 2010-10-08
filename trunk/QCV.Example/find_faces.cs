// qcv.exe -s find_faces.cs Example.FindFaces --run

using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions; 

using Emgu.CV;
using Emgu.CV.Structure;

namespace Example {

  [Addin]
  public class FindFaces : IFilter, IFilterListProvider {
    
    private HaarCascade _hc = null;
    private string _cascade = "face_cascade.xml";
    
    [Description("Stored classifier description.")]
    [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
    public string CascadePath {
      get { return _cascade;}
      set {
        if (File.Exists(value)) {
          _cascade = value; 
          _hc = new HaarCascade(value);
        }
      }
    }
    
    public void Execute(Dictionary<string, object> b) {
      while (_hc == null) {
        if (!b.GetInteractor().Query("Please specify a correct cascade file", this)) {
          b["cancel"] = true;
        }
      }
      
      Image<Bgr, byte> i = b.GetImage("source");
      Image<Gray, byte> gray = i.Convert<Gray, byte>();
      
      foreach(MCvAvgComp comp in gray.DetectHaarCascade(_hc)[0]) {
        i.Draw(comp.rect, new Bgr(Color.Red), 4);
      }
    }
    
    
    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        host.CreateInstance<IFilter>("QCV.Toolbox.Camera", new object[]{0, 320, 200, "source"}),
        this,
        host.CreateInstance<IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"})
      };
    }
    
  }
}