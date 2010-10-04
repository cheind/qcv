using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.Base {

  public class ConsoleDataInteractor : IDataInteractor {

    class OpenCVImageHandler {
      private HashSet<string> _known_windows = new HashSet<string>();
      private ThreadedWorker _w = new ThreadedWorker();

      public OpenCVImageHandler(Runtime r) {
        r.RuntimeShutdownEvent += new EventHandler(RuntimeShutdownEvent);
        r.RuntimeStartingEvent += new EventHandler(RuntimeStartingEvent);
      }

      void RuntimeStartingEvent(object sender, EventArgs e) {
        _w.Start();
      }

      void RuntimeShutdownEvent(object sender, EventArgs e) {
        _w.Stop();
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
    };


    private OpenCVImageHandler _ih;
    private Dictionary<Type, Action<string, object> > _show_lookup;
    private Action<string, object> _show_else;

    public ConsoleDataInteractor(Runtime r) {
      _ih = new OpenCVImageHandler(r);

      _show_lookup = new Dictionary<Type,Action<string,object>>()
      {
        {typeof(Image<Bgr, byte>), (id, o) => {_ih.Show(id, o as Image<Bgr, byte>);}}
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

  }
}
