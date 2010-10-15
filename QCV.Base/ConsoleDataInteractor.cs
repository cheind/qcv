// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Reflection;

namespace QCV.Base {

  /// <summary>
  /// Data interactor implementation for console applications.
  /// </summary>
  public class ConsoleDataInteractor : IDataInteractor {

    /// <summary>
    /// Single threaded OpenCV handler.
    /// </summary>
    private OpenCVImageHandler _ih;

    /// <summary>
    /// Lookup table for show requests.
    /// </summary>
    private Dictionary<Type, Action<string, object>> _show_lookup;

    /// <summary>
    /// The action to be carried out when no other show action matches.
    /// </summary>
    private Action<string, object> _show_else;

    /// <summary>
    /// Cache of filter event notifications
    /// </summary>
    private EventInvocationCache _eic;

    /// <summary>
    /// Initializes a new instance of the ConsoleDataInteractor class.
    /// </summary>
    /// <param name="r">The runtime used</param>
    public ConsoleDataInteractor(Runtime r) {
      _ih = new OpenCVImageHandler(r);
      _eic = new EventInvocationCache();

      _show_lookup = new Dictionary<Type, Action<string, object>>()
      {
        {typeof(Image<Bgr, byte>), (id, o) => {_ih.Show(id, o as Image<Bgr, byte>); } }
      };
      _show_else = (id, o) => {
        Console.WriteLine(String.Format("{0} : {1}", id, o.ToString()));
      };
    }

    /// <summary>
    /// Show values and images
    /// </summary>
    /// <param name="id">Show identifier</param>
    /// <param name="o">Object to show</param>
    public void Show(string id, object o) {
      Type t = o.GetType();
      if (_show_lookup.ContainsKey(t)) {
        _show_lookup[t](id, o);
      } else {
        _show_else(id, o);
      }
    }

    /// <summary>
    /// Post a query and wait for the answer.
    /// </summary>
    /// <param name="text">Caption of query.</param>
    /// <param name="o">Optional query object the user should complete.</param>
    /// <returns>False if the query was cancelled, true otherwise.</returns>
    public bool Query(string text, object o) {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Execute pending filter events
    /// </summary>
    /// <param name="instance">The filter requesting to execute its pending events</param>
    /// <param name="bundle">The bundle parameter to pass to the event methods to be invoked</param>
    public void ExecutePendingEvents(IFilter instance, Dictionary<string, object> bundle) {
      _eic.InvokeEvents(instance, bundle);
    }

    /// <summary>
    /// Cache an event notification
    /// </summary>
    /// <param name="instance">Target instance</param>
    /// <param name="mi">Reflected method information</param>
    public void CacheEvent(IFilter instance, MethodInfo mi) {
      _eic.Add(instance, mi);
    }

    /// <summary>
    /// Provides a single threaded environment for handling
    /// OpenCV stuff.
    /// </summary>
    /// <remarks>OpenCV requires window handles and co to be
    /// served by a single thread. Since the runtime may run on different
    /// background threads, we need to make sure that only one thread communicates
    /// with OpenCV.</remarks>
    private class OpenCVImageHandler {
      /// <summary>
      /// A set of known window ideas already passed to OpenCV.
      /// </summary>
      private HashSet<string> _known_windows = new HashSet<string>();

      /// <summary>
      /// The thread that communicates with OpenCV.
      /// </summary>
      private ThreadedWorker _w = new ThreadedWorker();

      /// <summary>
      /// Initializes a new instance of the OpenCVImageHandler class.
      /// </summary>
      /// <param name="r">The runtime used</param>
      public OpenCVImageHandler(Runtime r) {
        r.RuntimeShutdownEvent += new EventHandler(RuntimeShutdownEvent);
        r.RuntimeStartingEvent += new EventHandler(RuntimeStartingEvent);
      }

      /// <summary>
      /// Show image visualization.
      /// </summary>
      /// <param name="id">Caption text</param>
      /// <param name="image">The image to show</param>
      public void Show(string id, Image<Bgr, byte> image) {
        _w.Invoke(() => {
          if (!_known_windows.Contains(id)) {
            _known_windows.Add(id);
            CvInvoke.cvNamedWindow(id);
          }

          CvInvoke.cvShowImage(id, image.Copy());
          CvInvoke.cvWaitKey(1);
          return null;
        });
      }

      /// <summary>
      /// Occurs when the runtime starts.
      /// </summary>
      /// <param name="sender">The runtime starting</param>
      /// <param name="e">Additional event parameters</param>
      private void RuntimeStartingEvent(object sender, EventArgs e) {
        _w.Start();
      }

      /// <summary>
      /// Occurs when the runtime shutsdown.
      /// </summary>
      /// <param name="sender">The runtime shut down.</param>
      /// <param name="e">Additional event parameters</param>
      private void RuntimeShutdownEvent(object sender, EventArgs e) {
        _w.Stop();
      }
    }

  }
}
