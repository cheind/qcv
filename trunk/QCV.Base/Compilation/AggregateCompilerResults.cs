// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.Base.Compilation {

  /// <summary>
  /// Results from a multiple language compilation.
  /// </summary>
  public class AggregateCompilerResults : ICompilerResults {

    /// <summary>
    /// The list of compiler results.
    /// </summary>
    private IEnumerable<ICompilerResults> _results;

    /// <summary>
    /// Initializes a new instance of the AggregateCompilerResults class.
    /// </summary>
    /// <param name="results">The results of individual compilers.</param>
    public AggregateCompilerResults(IEnumerable<ICompilerResults> results) {
      _results = results;
    }

    /// <summary>
    /// Gets a value indicating whether the compilation succeeded or not.
    /// </summary>
    public bool Success {
      get {
        return _results.All((r) => r.Success);
      }
    }

    /// <summary>
    /// Get the errors formatted as human readable text.
    /// </summary>
    /// <returns>The formatted errors.</returns>
    public string GetFormattedErrors() {
      StringBuilder sb = new StringBuilder();

      bool empty = !_results.Any();
      if (empty) {
        sb.Append("Nothing to compile");
      } else {

        if (this.Success) {
          sb.Append("Success");
        } else {
          sb.AppendLine("Failed");
          foreach (ICompilerResults cr in _results) {
            sb.Append(cr.GetFormattedErrors());
          }
        }
      }

      return sb.ToString();
    }

    /// <summary>
    /// Get the compiled assemblies.
    /// </summary>
    /// <returns>The compiled assemblies</returns>
    public IEnumerable<System.Reflection.Assembly> GetCompiledAssemblies() {
      return _results.SelectMany((r) => r.GetCompiledAssemblies());
    }
  }
}
