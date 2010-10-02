using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace QCV.Toolbox {

  [Base.Addins.Addin]
  [Serializable]
  public class ShowFPS : Base.IFilter, ISerializable {
    private Stopwatch _watch = new Stopwatch();

    public delegate void FPSUpdateEventHandler(object sender, double fps);
    public event FPSUpdateEventHandler FPSUpdateEvent;

    public ShowFPS() {
    }

    public ShowFPS(SerializationInfo info, StreamingContext context)
    {
      _watch = new Stopwatch();
    }

    public void Execute(Dictionary<string, object> b, System.ComponentModel.CancelEventArgs e) {
      if (FPSUpdateEvent != null) {
        if (_watch.IsRunning) {
          _watch.Stop();
          FPSUpdateEvent(this, 1.0 / _watch.Elapsed.TotalSeconds);
          _watch.Reset();
        }
        _watch.Start();
      }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {}
  }
}
