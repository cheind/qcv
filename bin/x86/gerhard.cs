using System;
using System.Collections.Generic;
using QCV.Base.Extensions;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace Scripts {

  [QCV.Base.Addins.Addin]
  public class Gerhard : QCV.Base.IFilter, QCV.Base.IFilterListProvider {
    
    private int _thickness = 10;
    private Color _color = Color.Red;
    
    public int Thickness {
      get { return _thickness;}
      set {_thickness = value;}
    }
    
    public Color Color {
      get { return _color;}
      set {_color = value;}
    }
    
    public QCV.Base.FilterList CreateFilterList(QCV.Base.Addins.AddinHost h) {
      return new QCV.Base.FilterList() {
        h.CreateInstance<QCV.Base.IFilter>("QCV.Toolbox.Camera", new object[]{0, 320, 200, "source"}),
        h.CreateInstance<QCV.Base.IFilter>("Scripts.Gerhard"),
        h.CreateInstance<QCV.Base.IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"}),
        h.CreateInstance<QCV.Base.IFilter>("QCV.Toolbox.ShowFPS")
      };
    }
    
    public void Execute(Dictionary<string, object> b, System.ComponentModel.CancelEventArgs e) {
      Image<Bgr, byte> img = b.FetchImage("source");
      img.Draw(new Rectangle(0,0,img.Width, img.Height), new Bgr(Color), Thickness); 
    }
  }
}