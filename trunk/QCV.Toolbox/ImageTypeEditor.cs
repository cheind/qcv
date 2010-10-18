// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.Toolbox {

  /// <summary>
  /// A type editor that allows the user to select an image from the filesystem.
  /// </summary>
  [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
  public class ImageTypeEditor : UITypeEditor {

    /// <summary>
    /// Initializes a new instance of the ImageTypeEditor class.
    /// </summary>
    public ImageTypeEditor() 
    {}

    /// <summary>
    /// Get the edit style.
    /// </summary>
    /// <param name="context">The type descriptor context</param>
    /// <returns>The edit style</returns>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
      return UITypeEditorEditStyle.Modal;
    }

    /// <summary>
    /// Gets a boolean value indicating whether this type editor supports painting.
    /// </summary>
    /// <param name="context">The type descriptor context</param>
    /// <returns>True if painting is supported, false otherwise</returns>
    public override bool GetPaintValueSupported(ITypeDescriptorContext context) {
      return true;
    }

    /// <summary>
    /// Paint the type descriptor.
    /// </summary>
    /// <remarks>This will render a thumbnail of the selected image.</remarks>
    /// <param name="e">Paint arguments</param>
    public override void PaintValue(PaintValueEventArgs e) {
      if (e.Value != null) {
        Image<Bgr, byte> i = e.Value as Image<Bgr, byte>;
        e.Graphics.DrawImage(i.ToBitmap(), e.Bounds);
      }
    }

    /// <summary>
    /// Edit the value.
    /// </summary>
    /// <param name="context">The type descriptor context</param>
    /// <param name="provider">The service provider</param>
    /// <param name="value">The current value</param>
    /// <returns>The loaded image</returns>
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
