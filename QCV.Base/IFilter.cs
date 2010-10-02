using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QCV.Base {
  
  public interface IFilter {
    void Execute(Dictionary<string, object> b, CancelEventArgs e);
  }
}
