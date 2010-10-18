// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using QCV.Base.Extensions;

namespace QCV.Toolbox {

  /// <summary>
  /// A filter that shows an image.
  /// </summary>
  [Base.Addin]
  [Serializable]
  public class ShowImage : Base.IFilter {

    /// <summary>
    /// Key name of image data in bundle.
    /// </summary>
    private string _bundle_name = "source";

    /// <summary>
    /// Identifier to use with show request.
    /// </summary>
    private string _show_name = "image";

    /// <summary>
    /// Initializes a new instance of the ShowImage class.
    /// </summary>
    public ShowImage() {
    }

    /// <summary>
    /// Initializes a new instance of the ShowImage class.
    /// </summary>
    /// <param name="bundle_name">Key name of image data in bundle</param>
    public ShowImage(string bundle_name) {
      this.BundleName = bundle_name;
    }

    /// <summary>
    /// Initializes a new instance of the ShowImage class.
    /// </summary>
    /// <param name="bundle_name">Key name of image data in bundle</param>
    /// <param name="show_name">Identifier to use with show request</param>
    public ShowImage(string bundle_name, string show_name) {
      this.BundleName = bundle_name;
      this.ShowName = show_name;
    }

    /// <summary>
    /// Gets or sets the name of image data in bundle.
    /// </summary>
    public string BundleName {
      get { return _bundle_name; }
      set { _bundle_name = value; }
    }

    /// <summary>
    /// Gets or sets the identifier used for showing the image.
    /// </summary>
    public string ShowName {
      get { return _show_name; }
      set { _show_name = value; }
    }

    /// <summary>
    /// Execute the filter.
    /// </summary>
    /// <param name="b">Bundle of information</param>
    public void Execute(Dictionary<string, object> b) {
      Image<Bgr, byte> i = b.GetImage(_bundle_name);
      Base.IDataInteractor ii = b.GetInteractor();
      ii.Show(_show_name, i);
    }
  }
}
