using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.Base {
  public interface IDataInteractor {
    void Show(string id, object o);
    void Query(string text, out object o);
  }
}
