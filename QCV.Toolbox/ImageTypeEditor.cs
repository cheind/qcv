// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace QCV.Toolbox {

  [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
  public class ImageTypeEditor : UITypeEditor {

    public ImageTypeEditor() {
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
      return UITypeEditorEditStyle.Modal;
    }

    public override bool GetPaintValueSupported(ITypeDescriptorContext context) {
      return true;
    }

    public override void PaintValue(PaintValueEventArgs e) {
      if (e.Value != null) {
        Image<Bgr, byte> i = e.Value as Image<Bgr, byte>;
        e.Graphics.DrawImage(i.ToBitmap(), e.Bounds);
      }
    }

    public override object EditValue(
        ITypeDescriptorContext context,
        IServiceProvider provider,
        object value) {

      if (context == null) {
        return null;
      }
      using (OpenFileDialog ofd = new OpenFileDialog()) {
        ofd.Filter = "Image Files|*.bmp;*.png;*.jpg";
        if (ofd.ShowDialog() == DialogResult.OK) {
          Image<Bgr, byte> image = new Image<Bgr, byte>(ofd.FileName);
          return image;
        } else {
          return value;
        }
      }
    }
  }
}
