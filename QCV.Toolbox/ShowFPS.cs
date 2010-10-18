using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using QCV.Base.Extensions;

namespace QCV.Toolbox {

  [Base.Addin]
  [Serializable]
  public class ShowFPS : Base.IFilter, ISerializable {
    private int _iterations = 0;
    private DateTime _last_update = DateTime.Now;
    private double _update_frequency = 1.0;

    public ShowFPS() {}

    public ShowFPS(SerializationInfo info, StreamingContext context)
    {
      _iterations = 0;
      _last_update = DateTime.Now;
      this.UpdateFrequency = info.GetDouble("update_frequency");
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("update_frequency", _update_frequency);
    }

    public double UpdateFrequency {
      get { return _update_frequency;}
      set {_update_frequency = value;}
    }

    public void Execute(Dictionary<string, object> b) {
      _iterations += 1;
      DateTime now = DateTime.Now;
      double elapsed = (now - _last_update).TotalSeconds;
      if (elapsed > _update_frequency) {
        b.GetInteractor().Show("FPS", _iterations / elapsed );
        _last_update = now;
        _iterations = 0;
      }

    }

    
  }
}
