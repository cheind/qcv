// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QCV.Extensions {
  public static class ControlExtensions {

    public static void InvokeIfRequired(this Control control, MethodInvoker action) {
      if (control.InvokeRequired) {
        control.Invoke(action);
      } else {
        action();
      }
    }

    public static void BeginInvokeIfRequired(this Control control, MethodInvoker action) {
      if (control.InvokeRequired) {
        control.BeginInvoke(action);
      } else {
        action();
      }
    }
  }
}
