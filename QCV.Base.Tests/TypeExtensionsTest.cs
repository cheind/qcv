using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QCV.Base.Extensions;

namespace QCV.Base.Tests {

  [TestFixture]
  class TypeExtensionsTest {

    [Base.Addin]
    class CarriesAddin {
    }

    class CarriesNoAddin {
    }

    [Test]
    public void IsAddin() {
      Assert.True(typeof(CarriesAddin).IsAddin());
      Assert.False(typeof(CarriesNoAddin).IsAddin());
    }
  }
}
