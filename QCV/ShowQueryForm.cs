using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using QCV.Extensions;
using System.Threading;

namespace QCV {
  public partial class ShowQueryForm : Form {
    
    ManualResetEvent _r = new ManualResetEvent(false);
    Form _owner = null;
    bool _result;

    public delegate void OnQueryBeginEventHandler(object sender, string text, object query);
    public delegate void OnQueryEndEventHandler(object sender, bool results);

    public event OnQueryBeginEventHandler OnQueryBeginEvent;
    public event OnQueryEndEventHandler OnQueryEndEvent;

    public ShowQueryForm(Form owner) {
      InitializeComponent();
      _owner = owner;
    }

    private ShowQueryForm() {
      InitializeComponent();
    }

    public bool Query(string text, object query) {
      this.InvokeIfRequired(() => {
        if (OnQueryBeginEvent != null) {
          OnQueryBeginEvent(this, text, query);
        }
        _lb_query_text.Text = text;
        _pg.SelectedObject = query;        
      });

      _owner.BeginInvoke(new MethodInvoker(() => this.Show()));

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
      _result = true;
      _r.Set();
    }

    public void Cancel() {
      _result = false;
      _r.Set();
    }

    private void _btn_ok_Click(object sender, EventArgs e) {
      Commit();
      this.Hide();
    }

    private void _btn_cancel_Click(object sender, EventArgs e) {
      Cancel();
      this.Hide();
    }

    private void ShowQueryForm_FormClosing(object sender, FormClosingEventArgs e) {
      this.Cancel();
    }

    private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) {

    }
  }
}
