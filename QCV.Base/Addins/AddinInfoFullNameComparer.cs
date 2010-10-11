using System.Collections.Generic;

namespace QCV.Base.Addins {

  /// <summary>
  /// Compare two instances of AddinInfo by their types full name.
  /// </summary>
  public class AddinInfoFullNameComparer : EqualityComparer<AddinInfo> {

    public override bool Equals(AddinInfo a, AddinInfo b) {
      return a.FullName == b.FullName;
    }

    public override int GetHashCode(AddinInfo a) {
      return a.FullName.GetHashCode();
    }
  }
}
