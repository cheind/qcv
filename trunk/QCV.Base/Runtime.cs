// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using log4net;

namespace QCV.Base {

  /// <summary>
  /// Processes a list of filters.
  /// </summary>
  /// <remarks>
  /// <para> The runtime is responsible for processing of the filter list.
  /// As such, it offers <see cref="Run"/>, <see cref="Stop"/> and <see cref="Shutdown"/>
  /// methods. The processing of the filter list is done in an background task. The runtime 
  /// supports putting the calling thread into a resource-efficient wait mode until the above 
  /// mentioned operations have completed.</para>
  /// <para> The runtime stops when any of the stopping criteria are met. See <see cref="Run"/>
  /// for a detailed explanation. In case an error occurs during processing it is catched and safed
  /// for further processing in the <see cref="Error"/> property.
  /// </para>
  /// </remarks>
  public class Runtime : Resource, IRuntime {

    /// <summary>
    /// The logger associated with the <see cref="Runtime"/> class.
    /// </summary>
    private static readonly ILog _logger = LogManager.GetLogger(typeof(Runtime));

    /// <summary>
    /// Processing of the filter list is done in the backgrounds worker task.
    /// </summary>
    private BackgroundWorker _bw = new BackgroundWorker();

    /// <summary>
    /// Controls the cycle time of the runtime
    /// </summary>
    private FixedTimeStep _fts = new FixedTimeStep();

    /// <summary>
    /// Synchronization event used to block/unblock calling thread.
    /// </summary>
    private ManualResetEvent _stopped = new ManualResetEvent(false);

    /// <summary>
    /// Last error catched by runtime.
    /// </summary>
    private Exception _last_error;

    /// <summary>
    /// Initializes a new instance of the Runtime class.
    /// </summary>
    public Runtime() {
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(DoWork);
      _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
    }

    /// <summary>
    /// Occurs when the runtime is starting.
    /// </summary>
    public event EventHandler RuntimeStartingEvent;

    /// <summary>
    /// Occurs when the runtime has stopped.
    /// </summary>
    public event EventHandler RuntimeStoppedEvent;

    /// <summary>
    /// Occurs when the runtime completed the shutdown.
    /// </summary>
    /// <seealso cref="Shutdown"/>
    public event EventHandler RuntimeShutdownEvent;

    /// <summary>
    /// Gets the instance that controls the cylce time of the runtime.
    /// </summary>
    /// <remarks>The cycle time refers to the time it takes to complete
    /// one loop in the filter list.</remarks>
    public FixedTimeStep CycleTime {
      get { return _fts; }
    }

    /// <summary>
    /// Gets a value indicating whether the runtime is currently running or not.
    /// </summary>
    public bool Started {
      get { return _bw.IsBusy; }
    }

    /// <summary>
    /// Gets a value indicating whether the runtime has catched an error during processing or not.
    /// </summary>
    public bool HasError {
      get { return _last_error != null; }
    }

    /// <summary>
    /// Gets a value indicating whether a cancellation request is currently pending or not.
    /// </summary>
    public bool StopRequested {
      get { return _bw.CancellationPending; }
    }

    /// <summary>
    /// Gets the stored exception
    /// </summary>
    /// <remarks>The runtime stores possible exceptions that occur during the processing
    /// of the filter list. Use this property to retrieve a possible catched exception.</remarks>
    public Exception Error {
      get { return _last_error; }
    }

    /// <summary>
    /// Start the runtime.
    /// </summary>
    /// <param name="fl">Filter list to process in runtime</param>
    /// <param name="bundle">Bundle to pass to filter list</param>
    /// <returns>True if request is accepted, false otherwise.</returns>
    public bool RequestStart(FilterList fl, Dictionary<string, object> bundle) {
      return Start(fl, bundle, 0);
    }

    /// <summary>
    /// Start processing the filter list.
    /// </summary>
    /// <remarks>For details on parameters see <see cref="Run"/> overloads.</remarks>
    /// <param name="fl">The filter list to run</param>
    /// <param name="wait">The waiting mode for the calling thread</param>
    /// <returns>True if start command was processed successfully, false otherwise</returns>
    public bool Start(FilterList fl, int wait) {
      return Start(fl, new Dictionary<string, object>(), wait);
    }

    /// <summary>
    /// Start processing the filter list.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling Runtime.Run will start processing of the filter list given, if
    /// the runtime is not busy at the time the call is made. The runtime stops
    /// processing of the filter list if any of the following criteria are met
    /// <list type="bullet">
    /// <item><description>Runtime.Stop or Runtime.Shutdown is called.</description></item>
    /// <item><description>A filter cancels execution.</description></item>
    /// <item><description>An exception is raised during processing of a filter.</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// The wait parameter has the following meanings.
    /// <list type="table">
    /// <listheader><term>Value</term><description>Meaning</description></listheader>  
    /// <item><term>-1</term><description>Block the calling thread until Runtime.Run stops.</description></item>
    /// <item><term>0</term><description>Request a start of the runtime and return immediately.</description></item>
    /// <item><term> >0 </term><description>Start the runtime and block the calling thread fo
    /// the specified number of seconds.</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <param name="fl">The filter list to process.</param>
    /// <param name="b">The set of parameters to pass to the filter list.</param>
    /// <param name="wait">The waiting mode for the calling thread.</param>
    /// <returns>True if start command was processing successfully, false otherwise</returns>
    public bool Start(FilterList fl, Dictionary<string, object> b, int wait)
    {
      if (!_bw.IsBusy) {
        _stopped.Reset();
        _last_error = null;
        _logger.Info("Starting");
        if (RuntimeStartingEvent != null) {
          RuntimeStartingEvent(this, new EventArgs());
        }

        b["filterlist"] = fl;
        b["runtime"] = this;

        _bw.RunWorkerAsync(b);
        if (wait == -1) {
          _stopped.WaitOne();
        } else if (wait > 0) {
          if (!_stopped.WaitOne(wait * 1000)) {
            this.Stop(true);
          }
        }

        return true;
      } else {
        return false;
      }
    }

    /// <summary>
    /// Request a runtime stop.
    /// </summary>
    /// <remarks>RequestStop posts a request to the runtime to stop.
    /// This request, if accepted, should be carried out as soon as possible.
    /// RequestStop generally does not block until the runtime has stopped.
    /// Use the <see cref="RuntimeStoppedEvent"/> to get notified.</remarks>
    /// <returns>True if request is accepted, false otherwise.</returns>
    public bool RequestStop() {
      return Stop(false);
    }

    /// <summary>
    /// Stop the runtime.
    /// </summary>
    /// <remarks>The calling thread can be blocked resource-efficient until
    /// the operation has completed. If the calling thread does not wait,
    /// this operations posts a stop request to the async processing of the 
    /// runtime. Use the Runtime.Running property or the RuntimeStoppedEvent
    /// to test the state of the runtime.</remarks>
    /// <param name="wait">When true blocks the calling thread until the 
    /// operation has completed, otherwise returns immediately.</param>
    /// <returns>True if stop command was processed successfully, false otherwise.</returns>
    public bool Stop(bool wait) {
      if (_bw.IsBusy) {
        _bw.CancelAsync();
        if (wait) {
          _stopped.WaitOne();
        }

        return true;
      } else {
        return false;
      }
    }

    /// <summary>
    /// Shutdown the runtime.
    /// </summary>
    /// <remarks>Stops the runtime in blocking mode and afterwards destroys the runtime
    /// triggering a RuntimeShutdownEvent. After Shutdown the runtime instance cannot
    /// be used for further processing.
    /// Make sure you shutdown the runtime once you don't require it anymore.</remarks>
    public void Shutdown() {
      this.Dispose();
    }

    /// <summary>
    /// Dispose all managed resourced.
    /// </summary>
    protected override void DisposeManaged() {
      this.Stop(true);
      if (RuntimeShutdownEvent != null) {
        RuntimeShutdownEvent(this, new EventArgs());
      }
    }

    /// <summary>
    /// Called by the background worker on completion.
    /// </summary>
    /// <param name="sender">The background worker instance.</param>
    /// <param name="e">The event arguments.</param>
    private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      _last_error = e.Result as Exception;
      _stopped.Set();
      _logger.Info("Stopped");
      if (RuntimeStoppedEvent != null) {
        RuntimeStoppedEvent(this, new EventArgs());
      }
    }

    /// <summary>
    /// The backround workers main loop.
    /// </summary>
    /// <param name="sender">The background worker instance.</param>
    /// <param name="e">The event arguments.</param>
    private void DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      Dictionary<string, object> info = 
        e.Argument as Dictionary<string, object>;

      FilterList fl = info["filterlist"] as FilterList;
      if (fl == null || fl.Count == 0) {
        _logger.Warn("No filters to work on");
        return;
      }

      try {
        while (!bw.CancellationPending) {
          _fts.UpdateAndWait();

          foreach (IFilter f in fl) {
            f.Execute(info);
            if (bw.CancellationPending) {
              break;
            }
          }
        }
      } catch (TargetInvocationException ex) {
        _logger.Error(String.Format("Runtime catched error {0} {1}", ex.InnerException.Message, ex.InnerException.StackTrace));
        e.Result = ex.InnerException;
      } catch (Exception ex) {
        _logger.Error(String.Format("Runtime catched error {0} {1}", ex.Message, ex.StackTrace));
        e.Result = ex;
      }
    }
  }
}

