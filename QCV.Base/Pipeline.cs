/*
 * RDVision http://rdvision.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace QCV.Base {

  public class Pipeline : Resource {
    private BackgroundWorker _bw = new BackgroundWorker();
    private FixedTimeStep _fts = new FixedTimeStep(30);
    private object _lock_event = new  object();
    private ManualResetEvent _stopped = new ManualResetEvent(false);

    public Pipeline() {
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(DoWork);
    }

    /// <summary>
    /// Get and set desired frames per second
    /// </summary>
    public double FrameRate {
      get { return _fts.FPS; }
      set { _fts.FPS = value; }
    }

    /// <summary>
    /// Start frame grabbing asynchronously
    /// </summary>
    public void Run(FilterList s, IRuntime r, int wait) {
      if (!_bw.IsBusy) {
        _stopped.Reset();
        _bw.RunWorkerAsync(new object[]{s,r});
        if (wait == 0) {
          _stopped.WaitOne();
        } else if (wait > 0) {
          if (!_stopped.WaitOne(wait * 1000))
            this.Stop();
        }
      }
    }

    public void Stop() {
      _bw.CancelAsync();
      _stopped.WaitOne();
    }

    protected override void DisposeManaged() {
      this.Stop();
    }

    void DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      object[] args = e.Argument as object[];
      FilterList playlist = args[0] as FilterList;
      IRuntime runtime = args[1] as IRuntime;

      bool stop = bw.CancellationPending;
      while (!stop) {
        _fts.UpdateAndWait();
        Bundle b = new Bundle();
        b.Store("playlist", playlist);
        b.Store("runtime", runtime);

        foreach (Func<Bundle, bool> f in playlist) {
          if (!f(b)) {
            stop = true;
            break;
          }
        }
        stop |= bw.CancellationPending;
      }
      
      e.Cancel = true;
      _stopped.Set();
    }
  }
}
