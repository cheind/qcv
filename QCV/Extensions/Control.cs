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
  }
}
