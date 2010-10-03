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
  public partial class FilterProperties : UserControl {
    public FilterProperties() {
      InitializeComponent();
    }

    public IEnumerable<Base.IFilter> Filters {
      set {
        PopulateComboBox(value);
      }
    }

    private void PopulateComboBox(IEnumerable<QCV.Base.IFilter> filters) {
      _cmb_filters.InvokeIfRequired(() => {
        _cmb_filters.Items.Clear();
        foreach (QCV.Base.IFilter f in filters) {
          _cmb_filters.Items.Add(f);
        }
        _pg.SelectedObject = null;
      });
    }

    private void _cmb_filters_SelectedIndexChanged_1(object sender, EventArgs e) {
      _pg.InvokeIfRequired(() => {
        if (_cmb_filters.SelectedIndex >= 0) {
          _pg.SelectedObject = _cmb_filters.SelectedItem;
        }
      });
    }
  }
}
