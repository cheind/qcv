// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace QCV.Base {

  /// <summary>
  /// Imitates the behaviour of BackgroundWorker using a custom thread.
  /// </summary>
  /// <remarks>
  /// <para>This class resembles BackgroundWorker in that arbitrary functions
  /// can be processed on a worker thread. In contrast to BackgroundWorker, which
  /// uses a thread from the applications thread pool, the thread associated with
  /// ThreadedWorker is a fixed one.</para>
  /// <para>This behaviour is desired with OpenCVs HighGUI functionality which
  /// requires a single thread to interact with.</para>
  /// <para>ThreadedWorker is currently only used in conjunction with <see cref="ConsoleDataInteractor"/>
  /// which requires a fixed thread for displaying images. Early tests show that upgrading to OpenCV 2.1 
  /// will require a single thread to fetch images from a camera source. So it might be necessary to 
  /// extend the application of ThreadedWorker.</para>
  /// </remarks>
  public class ThreadedWorker : Resource {

    /// <summary>
    /// Synchronization primitive to make queue invocations synchronized.
    /// </summary>
    private readonly object _queue_lock = new object();

    /// <summary>
    /// The worker thread.
    /// </summary>
    private Thread _thread;

    /// <summary>
    /// Boolean flag indicating whether the thread should stop processing or not.
    /// </summary>
    private bool _stop;

    /// <summary>
    /// The queue used for threaded communication
    /// </summary>
    private Queue<QueueEntry> _queue;

    /// <summary>
    /// Initializes a new instance of the ThreadedWorker class.
    /// </summary>
    public ThreadedWorker() {
      _queue = new Queue<QueueEntry>();
    }

    /// <summary>
    /// Invoke the given function on the thread and wait for completion.
    /// </summary>
    /// <param name="f">Function to execute on the worker thread.</param>
    /// <returns>The result of the computation</returns>
    public object Invoke(Func<object> f) {
      QueueEntry e = new QueueEntry(f);
      if (Enqueue(e)) {
        e.Wait.WaitOne();
      }

      return e.Result;
    }

    /// <summary>
    /// Start processing functions on the worker thread.
    /// </summary>
    public void Start() {
      if (_thread == null || !_thread.IsAlive) {
        _thread = new Thread(delegate() {

          _stop = false;
          while (!_stop) {
            QueueEntry e = null;
            e = Dequeue();
            try {
              e.Result = e.Fnc();
            } catch (Exception err) {
              e.Error = err;
            } finally {
              e.Wait.Set();
            }
          }

          // Empty queue without processing, but signal waits
          lock (_queue_lock) {
            while (_queue.Count > 0) {
              QueueEntry e = _queue.Dequeue();
              e.Wait.Set();
            }
          }

        });
        _thread.Start();
      }
    }

    /// <summary>
    /// Stops processing of pending queue entries.
    /// </summary>
    /// <remarks>Unprocessed queue entries will be pinged.</remarks>
    public void Stop() {
      if (_thread.IsAlive) {
        this.Invoke(() => { _stop = true; return null; });
        _thread.Join();
      }
    }

    /// <summary>
    /// Stops further processing.
    /// </summary>
    /// <seealso cref="Stop"/>
    protected override void DisposeManaged() {
      this.Stop();
    }

    /// <summary>
    /// Dequeue the top entry.
    /// </summary>
    /// <returns>The entry.</returns>
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

    /// <summary>
    /// Enqueue a new entry.
    /// </summary>
    /// <param name="e">The entry to enqueue</param>
    /// <returns>A value indicating whether the entry was enqueued successfully or not.</returns>
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

    /// <summary>
    /// Represents the communication entity being passed through
    /// the synchronized queue.
    /// </summary>
    internal class QueueEntry {

      /// <summary>
      /// The function to be carried out on the worker thread.
      /// </summary>
      public Func<object> Fnc;

      /// <summary>
      /// The synchronizaton primitive to realize a wait.
      /// </summary>
      public ManualResetEvent Wait;

      /// <summary>
      /// The result of invoking the function.
      /// </summary>
      public object Result;

      /// <summary>
      /// Any error that occurred during the processing of the function.
      /// </summary>
      public Exception Error;

      /// <summary>
      /// Initializes a new instance of the QueueEntry class.
      /// </summary>
      /// <param name="func">Function to be carried out on the thread.</param>
      public QueueEntry(Func<object> func) {
        Fnc = func;
        Wait = new ManualResetEvent(false);
        Result = null;
        Error = null;
      }
    }
  }
}
