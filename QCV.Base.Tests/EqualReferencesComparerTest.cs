using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace QCV.Base.Tests {

  [TestFixture]
  class EqualReferencesComparerTest {

    class TestFilter : Base.IFilter {
      public void Execute(Dictionary<string, object> b) {
        throw new NotImplementedException();
      }
    }

    [Test]
    public void Equals() {

      Base.EqualReferencesComparer e = new EqualReferencesComparer();

      TestFilter a = new TestFilter();
      TestFilter b = new TestFilter();

      Assert.True(e.Equals(a, a));
      Assert.True(e.Equals(b, b));
      Assert.False(e.Equals(a, b));
    }
  }
}
