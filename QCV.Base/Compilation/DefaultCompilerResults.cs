// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QCV.Base.Compilation {

  /// <summary>
  /// Default compiler results for .NET code providers.
  /// </summary>
  public class DefaultCompilerResults : ICompilerResults {

    /// <summary>
    /// Results of codedom provider.
    /// </summary>
    private CompilerResults _results;

    /// <summary>
    /// Initializes a new instance of the DefaultCompilerResults class.
    /// </summary>
    /// <param name="results">The results of the compilation</param>
    public DefaultCompilerResults(CompilerResults results) {
      _results = results;
    }

    /// <summary>
    /// Gets a value indicating whether the compilation succeeded or not.
    /// </summary>
    public bool Success {
      get { return !_results.Errors.HasErrors; }
    }

    /// <summary>
    /// Get the errors formatted as human readable text.
    /// </summary>
    /// <returns>The formatted errors.</returns>
    public string GetFormattedErrors() {
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < _results.Errors.Count; i++) {
        sb.AppendLine(i.ToString() + ": " + _results.Errors[i].ToString());
      }

      string nl = Environment.NewLine;
      string final = sb.ToString();
      return final.Remove(final.Length - nl.Length, nl.Length);
    }

    /// <summary>
    /// Get the list of compiled assemblies.
    /// </summary>
    /// <returns>The compiled assemblies.</returns>
    public IEnumerable<Assembly> GetCompiledAssemblies() {
      if (this.Success) {
        return new Assembly[] { _results.CompiledAssembly };
      } else {
        return new Assembly[] {};
      }
      
    }
  }
}
