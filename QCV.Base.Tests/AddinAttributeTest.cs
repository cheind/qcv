using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace QCV.Base.Tests {

  [TestFixture]
  class AddinAttributeTest {

    [Base.Addin]
    class CarriesAddin {
    }

    [Test]
    public void HasAddin() {
      Type t = typeof(CarriesAddin);
      Assert.True(Attribute.IsDefined(t, typeof(QCV.Base.AddinAttribute)));
    }

  }
}
