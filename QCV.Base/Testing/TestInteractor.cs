// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QCV.Base.Testing {

  /// <summary>
  /// Provides a data interactor especially designed for testing purposes.
  /// </summary>
  public class TestInteractor : IDataInteractor {
    /// <summary>
    /// Event cache to hold filter events.
    /// </summary> 
    private EventInvocationCache _cache = new EventInvocationCache();

    /// <summary>
    /// Action to be carried out on show request.
    /// </summary>
    private Action<string, object> _show_handler = (s, o) => { };

    /// <summary>
    /// Function to be invoked on query request.
    /// </summary>
    private Func<string, object, bool> _query_handler = (s, o) => { return false; };

    /// <summary>
    /// Show values.
    /// </summary>
    /// <remarks>Forwarded to <see cref="ShowHandler"/>.</remarks>
    /// <param name="id">Show identifier</param>
    /// <param name="o">Value to show</param>
    public void Show(string id, object o) {
      _show_handler(id, o);
    }

    /// <summary>
    /// Post a query and wait for the answer.
    /// </summary>
    /// <remarks>Forwarded to <see cref="QueryHandler"/>.</remarks>
    /// <param name="text">Query caption</param>
    /// <param name="o">Optional query object the user should complete.</param>
    /// <returns>False if the query was cancelled, true otherwise.</returns>
    public bool Query(string text, object o) {
      return _query_handler(text, o);
    }

    /// <summary>
    /// Execute pending filter events.
    /// </summary>
    /// <param name="instance">The filter requesting to execute its pending events</param>
    /// <param name="bundle">The bundle parameter to pass to the event methods to be invoked.</param>
    public void ExecutePendingEvents(IFilter instance, Dictionary<string, object> bundle) {
      _cache.InvokeEvents(instance, bundle);
    }

    /// <summary>
    /// Trigger a filter event.
    /// </summary>
    /// <param name="instance">The target filter</param>
    /// <param name="event_name">The event method name</param>
    public void TriggerEvent(IFilter instance, string event_name) {
      MethodInfo m = MethodInfoScanner.FindEventMethods(instance).First((mi) => mi.Name == event_name);
      if (m == null) {
        throw new ArgumentException(String.Format("No such event {0}", event_name));
      }

      _cache.Add(instance, m);
    }
  }
}
