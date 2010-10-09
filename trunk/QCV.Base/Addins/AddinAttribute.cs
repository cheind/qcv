// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;

namespace QCV.Base.Addins {

  /// <summary>
  /// Flags addins
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class AddinAttribute : System.Attribute {

    /// <summary>
    /// Initializes a new instance of the AddinAttribute class.
    /// </summary>
    public AddinAttribute() {
    }
  }
}
