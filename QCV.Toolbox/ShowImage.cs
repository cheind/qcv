using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using QCV.Base.Extensions;

namespace QCV.Toolbox {

  [Base.Addins.Addin]
  [Serializable]
  public class ShowImage : Base.IFilter {

    private string _bag_name = "source";
    public string BagName {
      get { return _bag_name; }
      set { _bag_name = value; }
    }

    private string _show_name = "image";
    public string ShowName {
      get { return _show_name; }
      set { _show_name = value; }
    }

    public void Execute(QCV.Base.Bundle b, System.ComponentModel.CancelEventArgs e) {
      Image<Bgr, byte> i = b.FetchImage(_bag_name);
      Base.Runtime r = b.FetchRuntime();
      r.Show(_show_name, i);
    }
  }
}
