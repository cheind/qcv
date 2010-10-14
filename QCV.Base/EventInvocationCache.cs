// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QCV.Base {

  /// <summary>
  /// Caches event notifcations for filter.
  /// </summary>
  /// <remarks>Events are cached to provide a thread safe mechanism to
  /// synchronize events with the filters execute method.</remarks>
  public class EventInvocationCache {

    /// <summary>
    /// A lookup from filter to list of cached invocations.
    /// </summary>
    private Dictionary<IFilter, List<MethodInfo>> _cache;

    /// <summary>
    /// Initializes a new instance of the EventInvocationCache class.
    /// </summary>
    public EventInvocationCache() {
      _cache = new Dictionary<IFilter, List<MethodInfo>>(new EqualReferencesComparer());
    }

    /// <summary>
    /// Cache an event.
    /// </summary>
    /// <param name="instance">Target filter</param>
    /// <param name="mi">Reflected method info</param>
    public void Add(QCV.Base.IFilter instance, MethodInfo mi) {
      this.AddRange(instance, new MethodInfo[] { mi });
    }

    /// <summary>
    /// Add a range of cached events.
    /// </summary>
    /// <param name="instance">Target filter</param>
    /// <param name="mi">Reflected method infos</param>
    public void AddRange(IFilter instance, MethodInfo[] mi) {
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

    /// <summary>
    /// Invoke cached events
    /// </summary>
    /// <remarks>After events are executed the target instance is removed
    /// from the internal lookup.</remarks>
    /// <param name="instance">Instance to invoke cached events for.</param>
    /// <param name="bundle">Bundle of information to pass to instance</param>
    public void InvokeEvents(IFilter instance, Dictionary<string, object> bundle) {
      lock (_cache) {
        if (!_cache.ContainsKey(instance)) {
          return;
        } else {
          object[] param = new object[] { bundle };
          List<MethodInfo> li = _cache[instance];
          try {
            foreach (MethodInfo mi in li.Distinct()) {
              mi.Invoke(instance, param);
            }
          } finally {
            li.Clear();
            _cache.Remove(instance);
          }
        }
      }
    }

  }
}
