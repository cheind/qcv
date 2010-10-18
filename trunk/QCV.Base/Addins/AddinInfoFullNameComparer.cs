// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;

namespace QCV.Base.Addins {

  /// <summary>
  /// Compare two instances of AddinInfo by their types full name.
  /// </summary>
  public class AddinInfoFullNameComparer : EqualityComparer<AddinInfo> {

    /// <summary>
    /// Test if two addin infos are equal.
    /// </summary>
    /// <param name="a">First addin</param>
    /// <param name="b">Second addin</param>
    /// <returns>True if addin infos are equal, false otherwise</returns>
    public override bool Equals(AddinInfo a, AddinInfo b) {
      return a.FullName == b.FullName;
    }

    /// <summary>
    /// Calculate the hashcode for the addin info.
    /// </summary>
    /// <param name="a">Addin info</param>
    /// <returns>The calculated hashcode</returns>
    public override int GetHashCode(AddinInfo a) {
      return a.FullName.GetHashCode();
    }
  }
}
