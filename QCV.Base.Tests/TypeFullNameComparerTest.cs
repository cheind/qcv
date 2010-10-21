using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace QCV.Base.Tests {

  [TestFixture]
  class TypeFullNameComparerTest {

    [Test]
    public void Compare() {
      Type a = typeof(string);
      Type b = typeof(string);

      Base.TypeFullNameComparer c = new Base.TypeFullNameComparer();
      Assert.True(c.Equals(a, b));
      Assert.False(c.Equals(a, typeof(int)));

      Assert.AreEqual(a.FullName.GetHashCode(), c.GetHashCode(a));
    }
  }
}
