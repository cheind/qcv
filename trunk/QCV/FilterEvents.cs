﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace QCV {
  public partial class FilterEvents : UserControl {
    private QCV.Base.IDataInteractor _interactor = null;

    public FilterEvents() {
      InitializeComponent();
    }

    public QCV.Base.IDataInteractor DataInteractor {
      set {
        _interactor = value;
      }
    }


    public void GenerateUI(object instance) {
      _layouter.Controls.Clear();
      MethodInfo[] event_methods = QCV.Base.MethodInfoScanner.FindEventMethods(instance);
      foreach (MethodInfo mi in event_methods) {
        Button b = new Button();
        b.AutoSize = true;
        b.Text = mi.Name.Substring(2); // Remove On
        var lambda_mi = mi;
        var lambda_instance = instance;
        b.Click += new EventHandler((sender, ev) => {
          _interactor.CacheEvent(lambda_instance, lambda_mi);
        });
        _layouter.Controls.Add(b);
      }
    }

  }
}
