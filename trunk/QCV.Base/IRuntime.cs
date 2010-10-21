// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.Base {

  /// <summary>
  /// Defines minimal set of methods and events a runtime has to support.
  /// </summary>
  /// <remarks>A runtime is responsible for processing filter lists. As such, it controls
  /// the starting and stopping of the execution of filter lists. </remarks>
  public interface IRuntime {

    /// <summary>
    /// Occurs when the runtime is about to start.
    /// </summary>
    event EventHandler RuntimeStartingEvent;

    /// <summary>
    /// Occurs when the runtime stopped.
    /// </summary>
    event EventHandler RuntimeStoppedEvent;

    /// <summary>
    /// Gets a value indicating whether the runtime has started or not.
    /// </summary>
    bool Running { get; }

    /// <summary>
    /// Gets a value indicating whether a stop request is pending or not.
    /// </summary>
    bool StopRequested { get; }

    /// <summary>
    /// Start the runtime.
    /// </summary>
    /// <remarks>RequestStart posts a request to the runtime to start.
    /// This request, if accepted, should be carried out as soon as possible.
    /// RequestStart generally does not block until the runtime has started.
    /// Use the <see cref="RuntimeStartingEvent"/> to get notified.</remarks>
    /// <param name="fl">Filter list to process in runtime</param>
    /// <param name="bundle">Bundle to pass to filter list</param>
    /// <returns>True if request is accepted, false otherwise.</returns>
    bool RequestStart(FilterList fl, Dictionary<string, object> bundle);

    /// <summary>
    /// Request a runtime stop.
    /// </summary>
    /// <remarks>RequestStop posts a request to the runtime to stop.
    /// This request, if accepted, should be carried out as soon as possible.
    /// RequestStop generally does not block until the runtime has stopped.
    /// Use the <see cref="RuntimeStoppedEvent"/> to get notified.</remarks>
    /// <returns>True if request is accepted, false otherwise.</returns>
    bool RequestStop();
    
  }
}
