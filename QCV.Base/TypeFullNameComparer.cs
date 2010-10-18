// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;

namespace QCV.Base {

  /// <summary>
  /// Compare two instances of AddinInfo by their types full name.
  /// </summary>
  public class TypeFullNameComparer : EqualityComparer<Type> {

    /// <summary>
    /// Test if two type names are equal.
    /// </summary>
    /// <param name="a">First type</param>
    /// <param name="b">Second type</param>
    /// <returns>True if addin infos are equal, false otherwise</returns>
    public override bool Equals(Type a, Type b) {
      return a.FullName == b.FullName;
    }

    /// <summary>
    /// Calculate the hashcode for the type.
    /// </summary>
    /// <param name="a">Addin info</param>
    /// <returns>The calculated hashcode</returns>
    public override int GetHashCode(Type a) {
      return a.FullName.GetHashCode();
    }
  }
}
