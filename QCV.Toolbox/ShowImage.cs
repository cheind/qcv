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

    public ShowImage() {
    }

    public ShowImage(string bundle_name) {
      this.BundleName = bundle_name;
    }

    public ShowImage(string bundle_name, string show_name) {
      this.BundleName = bundle_name;
      this.ShowName = show_name;
    }

    private string _bundle_name = "source";
    public string BundleName {
      get { return _bundle_name; }
      set { _bundle_name = value; }
    }

    private string _show_name = "image";
    public string ShowName {
      get { return _show_name; }
      set { _show_name = value; }
    }

    public void Execute(Dictionary<string, object> b, System.ComponentModel.CancelEventArgs e) {
      Image<Bgr, byte> i = b.FetchImage(_bundle_name);
      Base.IDataInteractor ii = b.FetchInteractor();
      ii.Show(_show_name, i);
    }
  }
}
