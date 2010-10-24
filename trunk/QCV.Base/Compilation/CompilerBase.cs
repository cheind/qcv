// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.Base.Compilation {

  /// <summary>
  /// Provides compiler functionality.
  /// </summary>
  public abstract class CompilerBase {

    /// <summary>
    /// The provided compiler settings.
    /// </summary>
    private CompilerSettings _settings;

    /// <summary>
    /// Initializes a new instance of the CompilerBase class.
    /// </summary>
    /// <param name="settings">The settings for compilation</param>
    public CompilerBase(CompilerSettings settings) {
      _settings = settings;
    }

    /// <summary>
    /// Gets the provided compiler settings.
    /// </summary>
    public CompilerSettings Settings {
      get { return _settings; }
    }

    /// <summary>
    /// Test if compiler can handle the given file path.
    /// </summary>
    /// <param name="path">Path to file to compile</param>
    /// <returns>True if compiler can handle path, false otherwise</returns>
    public abstract bool CanCompileFile(string path);

    /// <summary>
    /// Compile the set of files.
    /// </summary>
    /// <param name="paths">Paths to files to compile.</param>
    /// <returns>The result of compilation</returns>
    public abstract ICompilerResults CompileFiles(IEnumerable<string> paths);
  }
}
