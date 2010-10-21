using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace QCV.Base.Tests {
  
  [TestFixture]
  class MethodInfoScannerTest {

    class MyFilter : Base.IFilter {
      public void OnClickMe(Dictionary<string, object> bundle) { }
      public void OnClickMe2(Dictionary<string, object> bundle) { }
      public void OnNotAnEvent(int wrong_type) { }
      private void OnNotAnEvent2(Dictionary<string, object> bundle) { }

      public void Execute(Dictionary<string, object> b) {
        throw new NotImplementedException();
      }
    }

    [Test]
    public void FindEvents() {
      MethodInfo[] mi = Base.MethodInfoScanner.FindEventMethods(new MyFilter());
      mi = mi.OrderBy((m) => m.Name).ToArray();

      Assert.AreEqual(2, mi.Length);
      Assert.AreEqual("OnClickMe", mi[0].Name);
      Assert.AreEqual("OnClickMe2", mi[1].Name);
    }
  }
}
