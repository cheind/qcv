using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QCV {
  public partial class PropertyForm : Form {

    public PropertyForm() {
      InitializeComponent();
    }

    public IEnumerable<Base.IFilter> Filters {
      set {
        PopulateComboBox(value);
        ResetPropertyGrid();
      }
    }

    private void ResetPropertyGrid() {
      _pg.SelectedObject = null;
    }

    private void PopulateComboBox(IEnumerable<QCV.Base.IFilter> filters) {
      _cmb_filters.Items.Clear();
      foreach (QCV.Base.IFilter f in filters) {
        _cmb_filters.Items.Add(f);
      }
    }

    private void _cmb_filters_SelectedIndexChanged(object sender, EventArgs e) {
      if (_cmb_filters.SelectedIndex >= 0) {
        _pg.SelectedObject = _cmb_filters.SelectedItem;
      }
    }


  }
}
