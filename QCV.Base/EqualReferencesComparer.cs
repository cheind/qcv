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
  /// Compare two filter by reference.
  /// </summary>
  public class EqualReferencesComparer : EqualityComparer<IFilter> {

    /// <summary>
    /// Get the hash code for a filter.
    /// </summary>
    /// <param name="obj">The filter to calculate the hash code for</param>
    /// <returns>The hash code</returns>
    public override int GetHashCode(IFilter obj) {
      return obj.GetHashCode();
    }

    /// <summary>
    /// Determine if two filter equal by reference.
    /// </summary>
    /// <param name="a">First filter</param>
    /// <param name="b">Second filter</param>
    /// <returns>True if the two filter equal, false otherwise</returns>
    public override bool Equals(IFilter a, IFilter b) {
      return Object.ReferenceEquals(a, b);
    }
  }
}
