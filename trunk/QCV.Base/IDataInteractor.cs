using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Reflection;

namespace QCV.Base {
  public interface IDataInteractor {
    void Show(string id, object o);
    bool Query(string text, object o);
    void CacheEvent(object instance, MethodInfo mi);
    void ExecutePendingEvents(object instance, Dictionary<string, object> bundle);
  }
}
