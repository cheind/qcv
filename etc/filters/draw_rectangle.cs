using System;
using System.Collections.Generic;
using QCV.Base.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace Scripts {

  [QCV.Base.Addins.Addin]
  public class DrawRectangle : QCV.Base.IFilter {
    private int _thickness = 5;
    private Color _color = Color.Red;
    
    public int Thickness {
      get { return _thickness;}
      set { _thickness = value; }
    }
    
    public Color Color {
      get { return _color;}
      set { _color = value;}
    }
    
    public void Execute(Dictionary<string, object> b) {
      Image<Bgr, byte> i = b.FetchImage("source");
      i.Draw(new Rectangle(0, 0, i.Size.Width, i.Size.Height), new Bgr(_color), Thickness);
    }
  }
}