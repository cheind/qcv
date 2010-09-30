﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV {
  public partial class Main : Form {
    private Dictionary<string, ShowImageForm> _show_forms = new Dictionary<string, ShowImageForm>();
    private Base.FilterList _filters = new QCV.Base.FilterList();
    private Base.Runtime _runtime = new QCV.Base.Runtime();
    private PropertyForm _props = new PropertyForm();

    public Main() {
      InitializeComponent();
      this.AddOwnedForm(_props);

      Base.Addins.AddinStore.Discover();
      Base.Addins.AddinStore.Discover(Environment.CurrentDirectory);
      Base.Addins.AddinStore.Discover(Path.Combine(Environment.CurrentDirectory, "plugins"));

      _props.FormClosing += new FormClosingEventHandler(AnyFormClosing);
      _runtime.RuntimeFinishedEvent += new QCV.Base.Runtime.RuntimeFinishedEventHandler(RuntimeFinishedEvent);
      _runtime.ShowImageRequestEvent += new QCV.Base.Runtime.ShowImageRequestEventHandler(ShowImageRequestEvent);
      _filters = CreateFilterList(Environment.GetCommandLineArgs());
      PreprocessFilter(_filters);
      _props.Filters = _filters;
    }

    void ShowImageRequestEvent(object sender, string id, Image<Bgr, byte> image) {
      Image<Bgr, byte> copy = image.Copy();
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
        f.Image = copy.Resize(r.Width, r.Height, Emgu.CV.CvEnum.INTER.CV_INTER_NN, true);
      }));
    }

    private void PreprocessFilter(QCV.Base.FilterList filters) {
      ShowFPS fps = filters.FirstOrDefault(
        (f) => { return f is ShowFPS; }
      ) as ShowFPS;

      if (fps != null) {
        fps.FPSUpdateEvent += new ShowFPS.FPSUpdateEventHandler(FPSUpdateEvent);
      }
    }

    void FPSUpdateEvent(object sender, double fps) {
      if (_lb_status.InvokeRequired) {
        _btn_run.Invoke(new MethodInvoker(delegate {
          _lb_status.Text = String.Format("FPS: {0}", (int)fps);
        }));
      } else {
        _lb_status.Text = String.Format("FPS: {0}", (int)fps);
      }
    }

    void AnyFormClosing(object sender, FormClosingEventArgs e) {
      if (e.CloseReason != CloseReason.FormOwnerClosing) {
        e.Cancel = true;
        _props.Hide();
      }
    }

    void RuntimeFinishedEvent(object sender, EventArgs e) {
      if (_btn_run.InvokeRequired) {
        _btn_run.Invoke(new MethodInvoker(delegate { 
          _btn_run.Text = "Run"; 
          _lb_status.BackColor = Control.DefaultBackColor; 
        }));
      } else {
        _btn_run.Text = "Run";
        _lb_status.BackColor = Control.DefaultBackColor; 
      }
    }

    Base.FilterList CreateFilterList(IEnumerable<string> filter_names) {
      Base.FilterList fl = new QCV.Base.FilterList();
      foreach (string filter_name in filter_names) {
        IEnumerable<Base.Addins.AddinInfo> e = Base.Addins.AddinStore.FindAddins(
          typeof(Base.IFilter),
          (ai) => { return ai.FullName == filter_name; }
        );
        if (e.Count() > 0) {
          Base.IFilter f = Base.Addins.AddinStore.CreateInstance(e.First()) as Base.IFilter;
          fl.Add(f);
        }
      }
      return fl;
    }

    private void _btn_props_Click(object sender, EventArgs e) {
      _props.Show();
    }

    private void _btn_play_Click(object sender, EventArgs e) {
      if (_runtime.Running) {
        _runtime.Stop(false);
      } else {
        _btn_run.Text = "Stop";
        _lb_status.BackColor = Color.LightGreen;
        
        _runtime.Run(_filters, 0);
      }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      if (_runtime.Running) {
        _runtime.Stop(false);
        e.Cancel = true;
      }
    }
  }
}