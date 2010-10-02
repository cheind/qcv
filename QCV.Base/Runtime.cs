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
    private Exception _last_error;
    private BackgroundWorker _bw = new BackgroundWorker();
    private FixedTimeStep _fts = new FixedTimeStep();
    private ManualResetEvent _stopped = new ManualResetEvent(false);
    private IInteraction _ii;

    public Runtime(IInteraction ii) {
      _ii = ii;
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(DoWork);
      _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
    }

    void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      _last_error = e.Result as Exception;
      _ii.RuntimeStopped();
      _stopped.Set();
      if (RuntimeFinishedEvent != null) {
        RuntimeFinishedEvent(this, new EventArgs());
      }
    }

    public delegate void RuntimeFinishedEventHandler(object sender, EventArgs e);
    public event RuntimeFinishedEventHandler RuntimeFinishedEvent;

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
    public void Run(FilterList s, int wait) {
      if (!_bw.IsBusy) {
        _stopped.Reset();
        _last_error = null;
        _ii.RuntimeStarted();
        _bw.RunWorkerAsync(new object[]{s,_ii});
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
      this.Stop(true);
      _ii.RuntimeShutdown();
    }

    protected override void DisposeManaged() {
      this.Stop(true);
    }

    void DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      object[] args = e.Argument as object[];
      FilterList filterlist = args[0] as FilterList;
      IInteraction ii = args[1] as IInteraction;

      bool stop = bw.CancellationPending;
      CancelEventArgs ev = new CancelEventArgs(false);
      try {
        while (!stop) {
          _fts.UpdateAndWait();
          Bundle b = new Bundle();
          b.Store("filterlist", filterlist);
          b.Store("runtime", this);
          b.Store("interaction", ii);
          b.Store("filter_logger", _filter_logger);

          foreach (IFilter f in filterlist) {
            f.Execute(b, ev);
            if (ev.Cancel) {
              stop = true;
              break;
            }
          }
          stop |= bw.CancellationPending;
        }
      } catch (Exception ex) {
        _logger.Error(String.Format("Filter raised error: {0}", ex.Message));
        e.Result = ex;
      }
    }
  };
}
