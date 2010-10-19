// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

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
