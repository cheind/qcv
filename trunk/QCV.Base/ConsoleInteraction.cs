using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.Base {

  public class ConsoleInteraction : IInteraction {
    private HashSet<string> _known_windows = new HashSet<string>();
    private Dictionary<Type, Action<string, object> > _show_lookup;
    Action<string, object> _show_else;
    private ThreadedWorker _w = new ThreadedWorker();

    public ConsoleInteraction(Runtime r) {
      r.RuntimeShutdownEvent += new EventHandler(RuntimeShutdownEvent);
      r.RuntimeStartingEvent += new EventHandler(RuntimeStartingEvent);
      _show_lookup = new Dictionary<Type,Action<string,object>>()
      {
        {typeof(Image<Bgr, byte>), (id, o) => {
          Image<Bgr, byte> image = o as Image<Bgr, byte>;
          _w.Invoke(() => {
            if (!_known_windows.Contains(id)) {
              _known_windows.Add(id);
              CvInvoke.cvNamedWindow(id);
            }

            CvInvoke.cvShowImage(id, image.Copy());
            CvInvoke.cvWaitKey(1);
            return null;
          });
        }}
      };
      _show_else = (id, o) => {
        Console.WriteLine(String.Format("{0} : {1}", id, o.ToString()));
      };

    }

    void RuntimeStartingEvent(object sender, EventArgs e) {
      _w.Start();
    }

    void RuntimeShutdownEvent(object sender, EventArgs e) {
      _w.Stop();
    }


    public void Show(string id, object o) {
      Type t = o.GetType();
      if (_show_lookup.ContainsKey(t)) {
        _show_lookup[t](id, o);
      } else {
        _show_else(id, o);
      }
    }

    public void Query(string text, out object o) {
      throw new NotImplementedException();
    }

  }
}
