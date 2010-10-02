using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QCV.Base {

  public class AnonymousFilter : IFilter {

    Action<Dictionary<string, object>, CancelEventArgs> _action;

    public AnonymousFilter(Action<Dictionary<string, object>, CancelEventArgs> action) {
      _action = action;
    }

    public void Execute(Dictionary<string, object> b, CancelEventArgs e) {
      _action(b, e);
    }
  }
}
