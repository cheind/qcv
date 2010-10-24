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
using Microsoft.CSharp;

namespace QCV.Base.Compilation {

  /// <summary>
  /// Provides compilation for the c-sharp language.
  /// </summary>
  public class CSharpCompiler : CompilerBase {

    /// <summary>
    /// Parameters passed to the compiler.
    /// </summary>
    private CompilerParameters _cp;

    /// <summary>
    /// Provides compilation for csharp input.
    /// </summary>
    private CSharpCodeProvider _csharp;

    /// <summary>
    /// Initializes a new instance of the CSharpCompiler class.
    /// </summary>
    /// <param name="settings">The compiler settings</param>
    public CSharpCompiler(CompilerSettings settings)
      : base(settings) 
    {
      _cp = new CompilerParameters(settings.AssemblyReferences.ToArray());
      _cp.GenerateExecutable = false;
      _cp.GenerateInMemory = !settings.DebugInformation;
      _cp.IncludeDebugInformation = settings.DebugInformation;
      _cp.TempFiles.KeepFiles = settings.DebugInformation;

      string framework_version = String.Format("v{0}.{1}", settings.FrameworkVersion.Major, settings.FrameworkVersion.Minor);

      Dictionary<string, string> csettings = new Dictionary<string, string>() {
        { "CompilerVersion", framework_version} 
      };

      _csharp = new CSharpCodeProvider(csettings);
    }

    /// <summary>
    /// Test if compiler can handle the given file path.
    /// </summary>
    /// <param name="path">Path to file to compile</param>
    /// <returns>True if compiler can handle path, false otherwise</returns>
    public override bool CanCompileFile(string path) {
      return path.EndsWith(_csharp.FileExtension, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Compile the set of files.
    /// </summary>
    /// <param name="paths">Paths to files to compile.</param>
    /// <returns>The result of compilation</returns>
    public override ICompilerResults CompileFiles(IEnumerable<string> paths) {
      CompilerResults cr = _csharp.CompileAssemblyFromFile(_cp, paths.ToArray());
      return new DefaultCompilerResults(cr);
    }
  }
}
