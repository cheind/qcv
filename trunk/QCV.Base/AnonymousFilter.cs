// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;

namespace QCV.Base {

  /// <summary>
  /// A filter that takes an action to be executed.
  /// </summary>
  public class AnonymousFilter : IFilter {

    /// <summary>
    /// The action to carry out.
    /// </summary>
    private Action<Dictionary<string, object>> _action;

    /// <summary>
    /// Initializes a new instance of the AnonymousFilter class.
    /// </summary>
    /// <param name="action">Action to be carried out.</param>
    public AnonymousFilter(Action<Dictionary<string, object>> action) {
      _action = action;
    }

    /// <summary>
    /// Execute the filter.
    /// </summary>
    /// <param name="b">Bundle containing filter parameters.</param>
    public void Execute(Dictionary<string, object> b) {
      _action(b);
    }
  }
}
