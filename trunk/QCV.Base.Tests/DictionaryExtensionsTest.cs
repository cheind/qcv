using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QCV.Base.Extensions;

namespace QCV.Base.Tests {

  [TestFixture]
  class DictionaryExtensionsTest {

    [Test]
    public void Get() {
      Dictionary<string, object> d = new Dictionary<string, object>();
      d["foo"] = (double)1.0;
      d["bar"] = "test";

      Assert.DoesNotThrow(new TestDelegate(() => d.Get<double>("foo")));
      Assert.DoesNotThrow(new TestDelegate(() => d.Get<string>("bar")));
      Assert.Throws<KeyNotFoundException>(new TestDelegate(() => d.Get<string>("not here")));
      Assert.Throws<InvalidCastException>(new TestDelegate(() => d.Get<int>("foo")));
      Assert.Throws<InvalidCastException>(new TestDelegate(() => d.Get<int>("bar")));

      Assert.AreEqual(1.0, d.Get<double>("foo"));
      Assert.AreEqual("test", d.Get<string>("bar"));
    }

    [Test]
    public void TryGet() {
      Dictionary<string, object> d = new Dictionary<string, object>();
      d["foo"] = (double)1.0;
      d["bar"] = "test";

      Assert.DoesNotThrow(new TestDelegate(() => { double v; d.Get<double>("foo", out v); }));
      Assert.DoesNotThrow(new TestDelegate(() => { string v; d.Get<string>("bar", out v); }));
      Assert.DoesNotThrow(new TestDelegate(() => { string v; d.Get<string>("not here", out v); }));
      Assert.Throws<InvalidCastException>(new TestDelegate(() => { int v; d.Get<int>("foo", out v); }));
      Assert.Throws<InvalidCastException>(new TestDelegate(() => { int v; d.Get<int>("bar", out v); }));

      double value;
      string str;
      Assert.True(d.Get<double>("foo", out value));
      Assert.AreEqual(1.0, value);
      Assert.False(d.Get<double>("nothere", out value));

      Assert.True(d.Get<string>("bar", out str));
      Assert.AreEqual("test", str);
    }

    [Test]
    public void GetRuntime() {
      Dictionary<string, object> d = new Dictionary<string, object>();
      d["runtime"] = new Base.Runtime();

      Assert.DoesNotThrow(new TestDelegate(() => d.GetRuntime()));
    }

    class NullInteractor : Base.IDataInteractor {
      public void Show(string id, object o) {        
      }

      public bool Query(string text, object o) {
        return false;
      }

      public void ExecutePendingEvents(IFilter instance, Dictionary<string, object> bundle) {
      }
    }

    [Test]
    public void GetInteractor() {
      Dictionary<string, object> d = new Dictionary<string, object>();
      d["interactor"] = new NullInteractor();

      Assert.DoesNotThrow(new TestDelegate(() => d.GetInteractor()));
    }

    [Test]
    public void GetFilterlist() {
      Dictionary<string, object> d = new Dictionary<string, object>();
      d["filterlist"] = new Base.FilterList();

      Assert.DoesNotThrow(new TestDelegate(() => d.GetFilterList()));
    }
  }
}
