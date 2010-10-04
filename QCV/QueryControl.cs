using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using QCV.Extensions;

namespace QCV {
  public partial class QueryControl : UserControl {
    ManualResetEvent _r = new ManualResetEvent(false);
    bool _result;

    public delegate void OnQueryBeginEventHandler(object sender, string text, object query);
    public delegate void OnQueryEndEventHandler(object sender, bool results);

    public event OnQueryBeginEventHandler OnQueryBeginEvent;
    public event OnQueryEndEventHandler OnQueryEndEvent;

    public QueryControl() {
      InitializeComponent();
      this.Enabled = false;
    }

    public bool Query(string text, object query) {
      this.InvokeIfRequired(() => {
        if (OnQueryBeginEvent != null) {
          OnQueryBeginEvent(this, text, query);
        }
        _lb_query_text.Text = text;
        if (query == null) {
          _pg.Visible = false;
        } else {
          _pg.Visible = true;
          _pg.SelectedObject = query;
        }
        this.Enabled = true;
      });

      _r.Reset();
      _r.WaitOne();

      this.InvokeIfRequired(() => {
        if (OnQueryEndEvent != null) {
          OnQueryEndEvent(this, _result);
        }
        _lb_query_text.Text = "No query present";
      });

      return _result;
    }

    public void Commit() {
      this.Enabled = false;
      _result = true;
      _r.Set();
    }

    public void Cancel() {
      this.Enabled = false;
      _result = false;
      _r.Set();
    }

    private void _btn_ok_Click(object sender, EventArgs e) {
      Commit();
    }



    private void _btn_cancel_Click(object sender, EventArgs e) {
      Cancel();
    }

  }
}
