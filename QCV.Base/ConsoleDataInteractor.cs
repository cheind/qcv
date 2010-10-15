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

  public class ConsoleDataInteractor : IDataInteractor {

    private OpenCVImageHandler _ih;
    private Dictionary<Type, Action<string, object>> _show_lookup;
    private Action<string, object> _show_else;
    private EventInvocationCache _eic;

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

    public void Show(string id, object o) {
      Type t = o.GetType();
      if (_show_lookup.ContainsKey(t)) {
        _show_lookup[t](id, o);
      } else {
        _show_else(id, o);
      }
    }

    public bool Query(string text, object o) {
      throw new NotImplementedException();
    }

    public void ExecutePendingEvents(IFilter instance, Dictionary<string, object> bundle) {
      _eic.InvokeEvents(instance, bundle);
    }

    public void CacheEvent(IFilter instance, MethodInfo mi) {
      _eic.Add(instance, mi);
    }

    private class OpenCVImageHandler {
      private HashSet<string> _known_windows = new HashSet<string>();
      private ThreadedWorker _w = new ThreadedWorker();

      public OpenCVImageHandler(Runtime r) {
        r.RuntimeShutdownEvent += new EventHandler(RuntimeShutdownEvent);
        r.RuntimeStartingEvent += new EventHandler(RuntimeStartingEvent);
      }

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

      private void RuntimeStartingEvent(object sender, EventArgs e) {
        _w.Start();
      }

      private void RuntimeShutdownEvent(object sender, EventArgs e) {
        _w.Stop();
      }
    }

  }
}
