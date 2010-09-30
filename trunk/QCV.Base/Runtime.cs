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

namespace QCV.Base {

  public class Runtime : Resource {
    private HashSet<string> _known_windows = new HashSet<string>();
    private BackgroundWorker _bw = new BackgroundWorker();
    private FixedTimeStep _fts = new FixedTimeStep();
    private ManualResetEvent _stopped = new ManualResetEvent(false);

    public Runtime() {
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(DoWork);
    }

    public double FPS {
      get {
        return _fts.FPS;
      }
      set {
        _fts.FPS = value;
      }
    }

    /// <summary>
    /// Start frame grabbing asynchronously
    /// </summary>
    public void Run(FilterList s, int wait) {
      if (!_bw.IsBusy) {
        _stopped.Reset();
        _bw.RunWorkerAsync(s);
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
      FilterList filterlist = e.Argument as FilterList;

      bool stop = bw.CancellationPending;
      CancelEventArgs ev = new CancelEventArgs(false);
      while (!stop) {
        _fts.UpdateAndWait();
        Bundle b = new Bundle();
        b.Store("filterlist", filterlist);
        b.Store("runtime", this);

        foreach (Action<Bundle, CancelEventArgs> f in filterlist) {
          f(b, ev);
          if (ev.Cancel) {
            stop = true;
            break;
          }
        }
        stop |= bw.CancellationPending;
      }

      e.Cancel = true;
      _stopped.Set();
    }

    public void Show(string id, Image<Bgr, byte> image, bool copy_image) {
      if (!_known_windows.Contains(id)) {
        _known_windows.Add(id);
        CvInvoke.cvNamedWindow(id);
      }

      CvInvoke.cvShowImage(id, copy_image ? image.Copy() : image);
      CvInvoke.cvWaitKey(1);
    }
  };
}
