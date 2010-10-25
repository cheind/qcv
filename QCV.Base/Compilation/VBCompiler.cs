// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.VisualBasic;

namespace QCV.Base.Compilation {

  /// <summary>
  /// Provides compilation for the Visual Basic language.
  /// </summary>
  public class VBCompiler : CompilerBase {

    /// <summary>
    /// Logger object used to log messages.
    /// </summary>
    private static readonly ILog _logger = LogManager.GetLogger(typeof(VBCompiler));

    /// <summary>
    /// Parameters passed to the compiler.
    /// </summary>
    private CompilerParameters _cp;

    /// <summary>
    /// Provides compilation for vb input.
    /// </summary>
    private VBCodeProvider _vb;

    /// <summary>
    /// Initializes a new instance of the VBCompiler class.
    /// </summary>
    /// <param name="settings">The compiler settings</param>
    public VBCompiler(CompilerSettings settings)
      : base(settings) {
      _cp = new CompilerParameters(settings.AssemblyReferences.ToArray());
      _cp.GenerateExecutable = false;
      _cp.GenerateInMemory = !settings.DebugInformation;
      _cp.IncludeDebugInformation = settings.DebugInformation;
      _cp.TempFiles.KeepFiles = settings.DebugInformation;

      string framework_version = String.Format("v{0}.{1}", settings.FrameworkVersion.Major, settings.FrameworkVersion.Minor);

      Dictionary<string, string> csettings = new Dictionary<string, string>() {
        { "CompilerVersion", framework_version} 
      };

      _vb = new VBCodeProvider(csettings);
    }

    /// <summary>
    /// Test if compiler can handle the given file path.
    /// </summary>
    /// <param name="path">Path to file to compile</param>
    /// <returns>True if compiler can handle path, false otherwise</returns>
    public override bool CanCompileFile(string path) {
      return path.EndsWith(_vb.FileExtension, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Compile the set of files.
    /// </summary>
    /// <param name="paths">Paths to files to compile.</param>
    /// <returns>The result of compilation</returns>
    public override ICompilerResults CompileFiles(IEnumerable<string> paths) {
      CompilerResults cr = _vb.CompileAssemblyFromFile(_cp, paths.ToArray());
      DefaultCompilerResults dcr = new DefaultCompilerResults(cr);

      if (dcr.Success) {
        _logger.Info("Success");
      } else {
        _logger.Error(dcr.GetFormattedErrors());
      }

      return dcr;
    }
  }
}
