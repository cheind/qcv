using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using QCV.Extensions;

namespace QCV {
  public partial class FilterSettings : UserControl {
    public FilterSettings() {
      InitializeComponent();
    }

    public QCV.Base.EventInvocationCache EventCache {
      set {
        _fl_events.EventCache = value;
      }
    }

    public void GenerateUI(IEnumerable<QCV.Base.IFilter> filters) {
      _cmb_filters.InvokeIfRequired(() => {
        object last_selected = _cmb_filters.SelectedItem;
        _cmb_filters.Items.Clear();
        foreach (QCV.Base.IFilter f in filters) {
          _cmb_filters.Items.Add(f);
        }
        if (last_selected != null) {
          Type t = last_selected.GetType();
          foreach (object o in _cmb_filters.Items) {
            if (o.GetType().FullName == t.FullName) {
              _cmb_filters.SelectedItem = o;
              GenerateUI(o);
              break;
            }
          }
        } else {
          if (_cmb_filters.Items.Count > 0) {
            _cmb_filters.SelectedItem = _cmb_filters.Items[0];
            GenerateUI(_cmb_filters.SelectedItem);
          }
        }
      });
    }

    private void _cmb_filters_SelectedIndexChanged(object sender, EventArgs e) {
      if (_cmb_filters.SelectedIndex >= 0) {
        GenerateUI(_cmb_filters.SelectedItem);
      }
    }

    private void GenerateUI(object target) {
      _fl_events.InvokeIfRequired(() => _fl_events.GenerateUI(target));
      _fl_properties.InvokeIfRequired(() => _fl_properties.GenerateUI(target));
    }

  }
}
