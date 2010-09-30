using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace QCV {

  [Base.Addins.Addin]
  public class ShowFPS : Base.IFilter {
    private Stopwatch _watch = new Stopwatch();

    public delegate void FPSUpdateEventHandler(object sender, double fps);
    public event FPSUpdateEventHandler FPSUpdateEvent;

    public void Execute(QCV.Base.Bundle b, System.ComponentModel.CancelEventArgs e) {
      if (FPSUpdateEvent != null) {
        if (_watch.IsRunning) {
          _watch.Stop();
          FPSUpdateEvent(this, 1.0 / _watch.Elapsed.TotalSeconds);
          _watch.Reset();
        }
        _watch.Start();
      }
    }
  }
}
