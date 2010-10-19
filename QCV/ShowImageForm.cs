// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV {
  public partial class ShowImageForm : Form {
    public ShowImageForm() {
      InitializeComponent();
    }

    public IImage Image {
      get {
        return _imgbox.Image;
      }
      set {
        if (value != null) {
          _imgbox.Image = value;
          this.ClientSize = value.Size;
        }
      }
    }
  }
}
