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

    public void GenerateUI(object instance) {
      _pg.InvokeIfRequired(() => {
        _pg.SelectedObject = instance;
      });
    }

  }
}
