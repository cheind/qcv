using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QCV.Base {
  public class EventInvocationCache {
    private Dictionary<object, List<MethodInfo>> _cache;

    public EventInvocationCache() {
      _cache = new Dictionary<object, List<MethodInfo>>(new EqualReferencesComparer());
    }

    public void Add(object instance, MethodInfo mi) {
      this.AddRange(instance, new MethodInfo[] { mi });
    }

    public void AddRange(object instance, MethodInfo[] mi) {
      lock (_cache) {
        List<MethodInfo> li;
        if (!_cache.ContainsKey(instance)) {
          li = new List<MethodInfo>();
          _cache.Add(instance, li);
        } else {
          li = _cache[instance];
        }
        li.AddRange(mi);
      }
    }

    public void InvokeEvents(object instance, Dictionary<string, object> bundle) {
      lock (_cache) {
        if (!_cache.ContainsKey(instance)) {
          return;
        } else {
          object[] param = new object[]{bundle};
          List<MethodInfo> li = _cache[instance];
          foreach (MethodInfo mi in li.Distinct()) {
            mi.Invoke(instance, param);
          }
          _cache.Remove(instance);
        }
      }
    }

  }
}
