// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace QCV.Base.Compilation {

  /// <summary>
  /// The result of a compilation.
  /// </summary>
  public interface ICompilerResults {

    /// <summary>
    /// Gets a value indicating whether the compilation succeeded or not.
    /// </summary>
    bool Success { get; }

    /// <summary>
    /// Get the errors formatted as human readable text.
    /// </summary>
    /// <returns>The formatted errors.</returns>
    string GetFormattedErrors();

    /// <summary>
    /// Get the list of compiled assemblies.
    /// </summary>
    /// <returns>The compiled assemblies.</returns>
    IEnumerable<Assembly> GetCompiledAssemblies();
  }
}
