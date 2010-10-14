// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;

namespace QCV.Base {
  
  /// <summary>
  /// Defines the interface for a filter.
  /// </summary>
  public interface IFilter {

    /// <summary>
    /// Execute the filter.
    /// </summary>
    /// <param name="b">The bundle containing the filter information.</param>
    void Execute(Dictionary<string, object> b);
  }
}
