using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QCV.Base {

  public class ThreadedWorker : Resource {

    class QueueEntry {
      public Func<object> fnc;
      public ManualResetEvent wait;
      public object result;
      public Exception error;

      public QueueEntry(Func<object> func) {
        fnc = func;
        wait = new ManualResetEvent(false);
        result = null;
        error = null;
      }
    }

    private Thread _thread;
    private bool _stop;
    private Queue<QueueEntry> _queue;
    private readonly object _queue_lock = new object();

    public ThreadedWorker() {
      _queue = new Queue<QueueEntry>();
    }

    public object Invoke(Func<object> f) {
      QueueEntry e = new QueueEntry(f);
      if (Enqueue(e))
        e.wait.WaitOne();
      return e.result;
    }

    private bool Enqueue(QueueEntry e) {
      lock (_queue_lock) {
        if (_stop) {
          return false;
        }
        _queue.Enqueue(e);
        if (_queue.Count == 1) {
          Monitor.Pulse(_queue_lock);
        }
        return true;
      }
    }

    public void Start() {
      if (_thread == null || !_thread.IsAlive) {
        _thread = new Thread(delegate() {

          _stop = false;
          while (!_stop) {
            QueueEntry e = null;
            e = Dequeue();
            try {
              e.result = e.fnc();
            } catch (Exception err) {
              e.error = err;
            } finally {
              e.wait.Set();
            }
          }

          // Empty queue without processing, but signal waits
          lock (_queue_lock) {
            while (_queue.Count > 0) {
              QueueEntry e = _queue.Dequeue();
              e.wait.Set();
            }
          }

        });
        _thread.Start();
      }
    }

    private QueueEntry Dequeue() {
      QueueEntry e = null;
      lock (_queue_lock) {
        while (_queue.Count == 0) {
          Monitor.Wait(_queue_lock);
        }
        e = _queue.Dequeue();
      }
      return e;
    }

    public void Stop() {
      if (_thread.IsAlive) {
        this.Invoke(() => { _stop = true; return null; });
        _thread.Join();
      }
    }

    protected override void DisposeManaged() {
      this.Stop();
    }

  }
}
