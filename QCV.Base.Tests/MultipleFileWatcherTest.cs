using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Threading;

namespace QCV.Base.Tests {

  [TestFixture]
  class MultipleFileWatcherTest {

    [Test]
    public void WatchSingleFile() {

      string tmp_a = Path.GetTempFileName();
      try {
        using (MultipleFileWatcher mfw = new MultipleFileWatcher()) {
          FileInfo fi = new FileInfo(tmp_a);

          // Will apply two distinct file changes
          CountdownLatch cl = new CountdownLatch(2);

          // Each time the change event is triggered we count one.
          mfw.Changed += new FileSystemEventHandler((o, e) => { cl.Signal(); });
          mfw.AddFilePath(tmp_a);
          Thread.Sleep(500);
        
          using (StreamWriter sw_a = fi.CreateText()) {
            sw_a.AutoFlush = true;
            sw_a.WriteLine("abc");
            sw_a.Flush();
          }
          Thread.Sleep(700);

          using (StreamWriter sw_a = fi.CreateText()) {
            sw_a.AutoFlush = true;
            sw_a.WriteLine("abcd");
            sw_a.Flush();
          }

          // Wait a second to complete.
          Assert.True(cl.Wait(new TimeSpan(0, 0, 1)));            
        }
        
      } finally {
        File.Delete(tmp_a);
      }

      
    }
  }
}
