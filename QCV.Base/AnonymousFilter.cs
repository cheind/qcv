using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QCV.Base {

  public class AnonymousFilter : IFilter {
    Action<Bundle, CancelEventArgs> _action;

    public AnonymousFilter(Action<Bundle, CancelEventArgs> action) {
      _action = action;
    }

    public void Execute(Bundle b, CancelEventArgs e) {
      _action(b, e);
    }
  }
}
