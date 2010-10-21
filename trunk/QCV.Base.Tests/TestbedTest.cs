using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using QCV.Base.Extensions;

namespace QCV.Base.Tests {

  [TestFixture]
  class TestbedTest {

    class MyFilter : IFilter {
      public void Execute(Dictionary<string, object> b) {
        b["result"] = 14.0;
      }
    }

    class MyFilter2 : IFilter {
      public void Execute(Dictionary<string, object> b) {
        b["result"] = 14.0;
        b.GetRuntime().RequestStop();
      }
    }

    [Test]
    public void SingleStep() {
      Testing.Testbed tb = new Testing.Testbed();
      Assert.True(tb.Runtime.RequestStart(new FilterList() { new MyFilter() }, tb.Bundle));
      Assert.False(tb.Runtime.RequestStart(new FilterList() { new MyFilter() }, tb.Bundle));

      tb.Runtime.SingleStep();
      Assert.AreEqual(14.0, tb.Bundle.Get<double>("result"));
      Assert.False(tb.Runtime.StopRequested);
      Assert.True(tb.Runtime.RequestStop());
      Assert.True(tb.Runtime.StopRequested);
      Assert.Throws<InvalidOperationException>(new TestDelegate(() => tb.Runtime.SingleStep()));
    }

    [Test]
    public void Run() {
      Testing.Testbed tb = new Testing.Testbed();
      Assert.True(tb.Runtime.RequestStart(new FilterList() { new MyFilter2() }, tb.Bundle));
      
      tb.Runtime.Run(); 
      Assert.AreEqual(14.0, tb.Bundle.Get<double>("result"));
      Assert.True(tb.Runtime.StopRequested);
      Assert.False(tb.Runtime.Running);
    }


  }
}
