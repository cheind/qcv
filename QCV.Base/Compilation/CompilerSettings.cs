// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;

namespace QCV.Base.Compilation {

  /// <summary>
  /// Compiler settings.
  /// </summary>
  public class CompilerSettings {

    /// <summary>
    /// .NET framework version to compile for.
    /// </summary>
    private Version _framework_version = TargetFramework.Version;

    /// <summary>
    /// True when to compile with debug information, false otherwise.
    /// </summary>
    private bool _debug = false;

    /// <summary>
    /// Assemblies to reference in compilation
    /// </summary>
    private IEnumerable<string> _assembly_reference_paths = new string[] {};

    /// <summary>
    /// Gets or sets the target .NET framework version to compile for.
    /// </summary>
    public Version FrameworkVersion {
      get { return _framework_version; }
      set { _framework_version = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to compile with or without debug information.
    /// </summary>
    public bool DebugInformation {
      get { return _debug; }
      set { _debug = value; }
    }

    /// <summary>
    /// Gets or sets the list of assemblies to reference in compilation.
    /// </summary>
    public IEnumerable<string> AssemblyReferences {
      get { return _assembly_reference_paths; }
      set { _assembly_reference_paths = value; }
    }

  }
}
