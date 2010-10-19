// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Forms;
using System.Drawing;

namespace QCV {
  public partial class Main : QCV.Base.IDataInteractor {
    private Dictionary<string, ShowImageForm> _show_forms = new Dictionary<string, ShowImageForm>();
    private QCV.Base.EventInvocationCache _ev_cache = new QCV.Base.EventInvocationCache();

    public void Show(string id, object o) {
      if (o == null) {
        return;
      }

      if (o is Image<Bgr, byte>) {
        Image<Bgr, byte> img = o as Image<Bgr, byte>;
        this.Invoke(new MethodInvoker(delegate {
          ShowImageForm f = null;
          if (!_show_forms.ContainsKey(id)) {
            f = new ShowImageForm();
            f.Text = id;
            this.AddOwnedForm(f);
            f.FormClosing += new FormClosingEventHandler(AnyFormClosing);
            f.Show();
            _show_forms.Add(id, f);
          } else {
            f = _show_forms[id];
          }
          Rectangle r = f.ClientRectangle;
          f.Image = img.Resize(r.Width, r.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, true);
        }));
      } else {
        // Show stringified version in datagrid
        this.Invoke(new MethodInvoker(delegate {
          ValuesDataSet.KeyValuesRow r = valuesDataSet.KeyValues.FindById(id);
          if (r != null) {
            // Update
            r.Value = o.ToString();
          } else {
            r = valuesDataSet.KeyValues.NewKeyValuesRow();
            r.Id = id;
            r.Value = o.ToString();
            valuesDataSet.KeyValues.Rows.Add(r);
          }
        }));
      }
    }

    public bool Query(string text, object o) {
      return _query_form.Query(text, o);
    }


    public void CacheEvent(QCV.Base.IFilter instance, System.Reflection.MethodInfo mi) {
      _ev_cache.Add(instance, mi);
    }

    public void ExecutePendingEvents(QCV.Base.IFilter instance, Dictionary<string, object> bundle) {
      _ev_cache.InvokeEvents(instance, bundle);
    }

  }
}
