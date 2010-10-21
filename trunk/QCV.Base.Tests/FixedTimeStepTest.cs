using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;

namespace QCV.Base.Tests {

  [TestFixture]
  class FixedTimeStepTest {

    [Test]
    public void Fixed() {
      Stopwatch sw = new Stopwatch();
      Base.FixedTimeStep fts = new FixedTimeStep();
      fts.FPS = 30;
      fts.Enabled = true;

      sw.Start();
      int count = 0;
      while (sw.ElapsedMilliseconds < 1000) {
        fts.UpdateAndWait();
        count += 1;
      }
      sw.Stop();

      Assert.AreEqual(30.0, count * 1.0, 2.0); 
    }

    [Test]
    public void Disabled() {
      Stopwatch sw = new Stopwatch();
      Base.FixedTimeStep fts = new FixedTimeStep();
      fts.FPS = 30;
      fts.Enabled = false;

      sw.Start();
      int count = 0;
      while (sw.ElapsedMilliseconds < 100) {
        fts.UpdateAndWait();
        count += 1;
      }
      sw.Stop();

      Assert.Less(100.0, count);
    }
  }
}
