using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Emgu.CV;
using Emgu.CV.Structure;
using System.ComponentModel;
using System.Threading;
using log4net;

namespace QCV.Base {

  public class Runtime : Resource {
    private static readonly ILog _logger = LogManager.GetLogger(typeof(Runtime));
    private static readonly ILog _filter_logger = LogManager.GetLogger(typeof(IFilter));
    private BackgroundWorker _bw = new BackgroundWorker();
    private FixedTimeStep _fts = new FixedTimeStep();
    private ManualResetEvent _stopped = new ManualResetEvent(false);
    private Exception _last_error;

    public event EventHandler RuntimeStartingEvent;
    public event EventHandler RuntimeStoppedEvent;
    public event EventHandler RuntimeShutdownEvent;

    public Runtime() {
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(DoWork);
      _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
    }

    void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      _last_error = e.Result as Exception;
      _stopped.Set();
      _logger.Info("Stopped");
      if (RuntimeStoppedEvent != null) {
        RuntimeStoppedEvent(this, new EventArgs());
      }
    }

    public double FPS {
      get {
        return _fts.FPS;
      }
      set {
        _fts.FPS = value;
      }
    }

    public bool Running {
      get { return _bw.IsBusy; }
    }

    public bool HasError {
      get { return _last_error != null; }
    }

    public Exception Error {
      get { return _last_error; }
    }

    /// <summary>
    /// Start frame grabbing asynchronously
    /// </summary>
    public void Run(FilterList fl, int wait) {
      Run(fl, new Dictionary<string, object>(), wait);
    }

    public void Run(FilterList fl, Dictionary<string, object> b, int wait)
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
        b["filter_logger"] = _filter_logger;
        
        _bw.RunWorkerAsync(b);
        if (wait == -1) {
          _stopped.WaitOne();
        } else if (wait > 0) {
          if (!_stopped.WaitOne(wait * 1000))
            this.Stop(true);
        }
      }
    }

    public void Stop(bool wait) {
      _bw.CancelAsync();
      if (wait)
        _stopped.WaitOne();
    }

    public void Shutdown() {
      this.Dispose();
    }

    protected override void DisposeManaged() {
      this.Stop(true);
      if (RuntimeShutdownEvent != null) {
        RuntimeShutdownEvent(this, new EventArgs());
      }
    }

    void DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      Dictionary<string, object> info = 
        e.Argument as Dictionary<string, object>;

      FilterList fl = info["filterlist"] as FilterList;

      bool stop = bw.CancellationPending;
      CancelEventArgs ev = new CancelEventArgs(false);
      try {
        while (!stop) {
          _fts.UpdateAndWait();

          foreach (IFilter f in fl) {
            f.Execute(info, ev);
            if (ev.Cancel) {
              _logger.Info(String.Format("Filter {0} requests stop", f.GetType().FullName));
              stop = true;
              break;
            }
          }
          stop |= bw.CancellationPending;
        }
      } catch (Exception ex) {
        _logger.Error(String.Format("Runtime catched error {0} {1}", ex.Message, ex.StackTrace));
        e.Result = ex;
      }
    }
  };
}

