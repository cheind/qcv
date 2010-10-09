// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;

namespace QCV.Base {
  
  public interface IFilter {
    void Execute(Dictionary<string, object> b);
  }
}
