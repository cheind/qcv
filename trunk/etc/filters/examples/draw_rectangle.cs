// qcv.exe -s draw_border.cs Example.DrawBorder --run

using System;
using System.Collections.Generic;
using System.Drawing

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions; 

using Emgu.CV;
using Emgu.CV.Structure;

namespace Example {

  [Addin]
  public class DrawBorder : IFilter, IFilterListProvider {
    
    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        host.FindAndCreate<IFilter>("QCV.Toolbox.Camera", new object[]{"source", 0, 320, 200}),
        this,
        host.FindAndCreate<IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"})
      };
    }
    
    private int _thickness = 5;
    [Description("Defines the width of the border.")]
    public int Thickness {
      get { return _thickness;}
      set { _thickness = value; }
    }
    
    private Color _color = Color.Red;
    [Description("Defines the color of the border.")]
    public Color Color {
      get { return _color;}
      set { _color = value;}
    }
    
    public void Execute(Dictionary<string, object> b) {
      Image<Bgr, byte> i = b.FetchImage("source");
      i.Draw(new Rectangle(0, 0, i.Size.Width, i.Size.Height), new Bgr(_color), _thickness);
    }
  }
}