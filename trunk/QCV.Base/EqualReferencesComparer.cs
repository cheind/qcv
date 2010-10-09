// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;

namespace QCV.Base {
  public class EqualReferencesComparer : EqualityComparer<object> {
    public override int GetHashCode(object obj) {
      return obj.GetHashCode();
    }

    public override bool Equals(object a, object b) {
      return Object.ReferenceEquals(a, b);
    }
  }
}
