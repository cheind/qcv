// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;

namespace QCV.Base.Testing {
  
  /// <summary>
  /// A runtime especially designed for testing purposes
  /// </summary>
  /// <remarks>This runtime is designed for testing. It provides therefore sequential
  /// foreground processing. That is, the filters are executed in the calling threads 
  /// context.</remarks>
  public class TestRuntime : IRuntime {
    /// <summary>
    /// The filter list to execute.
    /// </summary>
    private FilterList _fl = null;

    /// <summary>
    /// The bundle to pass to filters.
    /// </summary>
    private Dictionary<string, object> _bundle = null;

    /// <summary>
    /// A boolean value indicating the state of the runtime.
    /// </summary>
    private bool _running = false;

    /// <summary>
    /// A boolean value indicating whether a stop is requested or not.
    /// </summary>
    private bool _stop_request = false;

    /// <summary>
    /// Occurs when the runtime starts.
    /// </summary>
    public event EventHandler RuntimeStartingEvent;

    /// <summary>
    /// Occurs when the runtime stops.
    /// </summary>
    public event EventHandler RuntimeStoppedEvent;

    /// <summary>
    /// Gets a value indicating whether the runtime is currently running or not.
    /// </summary>
    public bool Running {
      get { return _running; }
    }

    /// <summary>
    /// Gets a value indicating whether a stop is currently pending or not.
    /// </summary>
    public bool StopRequested {
      get { return _stop_request; }
    }

    /// <summary>
    /// Start the runtime.
    /// </summary>
    /// <remarks>This runtime is designed for testing and does no
    /// background processing. To execute filters use the <see cref="SingleStep"/> or
    /// <see cref="Run"/> methods.</remarks>
    /// <param name="fl">The filter list to execute</param>
    /// <param name="bundle">The bundle to pass to filters.</param>
    /// <returns>True if the request is accepted, false otherwise</returns>
    public bool RequestStart(FilterList fl, Dictionary<string, object> bundle) {
      if (!_running) {
        if (fl == null) {
          throw new ArgumentException("Filerlist is null");
        }

        if (bundle == null) {
          throw new ArgumentException("Bundle is null");
        }

        _fl = fl;
        _bundle = bundle;
        _bundle.Add("runtime", this);
        _bundle.Add("filterlist", fl);

        _stop_request = false;
        _running = true;

        if (RuntimeStartingEvent != null) {
          RuntimeStartingEvent(this, new EventArgs());
        }

        return true;
      } else {
        return false;
      }
    }

    /// <summary>
    /// Execute all filters once.
    /// </summary>
    /// <remarks>
    /// <para>Runs in the same thread as the calling thread.</para>
    /// <para>Exits if a stop request is currently pending, or any executing filter requests a stop.</para>
    /// </remarks>
    public void SingleStep() {
      if (_running) {
        if (!_stop_request) {
          Step();
        }
      } else {
        throw new InvalidOperationException("Runtime not started");
      }
    }

    /// <summary>
    /// Execute all filters until a stop request occurs
    /// </summary>
    /// <remarks>
    /// <para>Runs in the same thread as the calling thread.</para>
    /// </remarks>
    public void Run() {
      if (_running) {
        while (!_stop_request) {
          Step();
        }
      }
    }

    /// <summary>
    /// Request a runtime stop.
    /// </summary>
    /// <returns>True if the request is accepted, false otherwise.</returns>
    public bool RequestStop() {
      if (_running) {
        _stop_request = true;
        _running = false;
        return true;
      } else {
        return false;
      }
    }

    /// <summary>
    /// Carry out a single processing step.
    /// </summary>
    private void Step() {
      foreach (IFilter f in _fl) {
        f.Execute(_bundle);
        if (_stop_request) {
          break;
        }
      }

      if (_stop_request) {
        _running = false;
        if (RuntimeStoppedEvent != null) {
          RuntimeStoppedEvent(this, new EventArgs());
        }
      }
    }
  }
}
