using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
