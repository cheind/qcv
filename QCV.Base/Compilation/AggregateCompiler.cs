// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace QCV.Base.Compilation {

  /// <summary>
  /// Provides compilation for multiple languages.
  /// </summary>
  /// <remarks>The aggregate compiler splits multiple language input
  /// into groups of equal source language. For each such group the designated
  /// compiler is invoked.
  /// <para>You should not mix different inputs written in different languages 
  /// that depend on each other, because it is unknown which language compiler is
  /// invoked first.</para>
  /// </remarks>
  public class AggregateCompiler : CompilerBase {

    /// <summary>
    /// The list of registered compilers.
    /// </summary>
    private List<CompilerBase> _compilers = new List<CompilerBase>();

    /// <summary>
    /// Initializes a new instance of the AggregateCompiler class.
    /// </summary>
    /// <param name="settings">Settings to pass to each registered compiler.</param>
    public AggregateCompiler(CompilerSettings settings)
    : base(settings) 
    {}

    /// <summary>
    /// Registers a new compiler by its type.
    /// </summary>
    /// <param name="compiler_type">Type of compiler to register</param>
    public void AddCompiler(Type compiler_type) {
      if (!typeof(CompilerBase).IsAssignableFrom(compiler_type)) {
        throw new ArgumentException(String.Format("Type {0} is not a compiler", compiler_type));
      }

      CompilerBase c = Activator.CreateInstance(compiler_type, new object[] { this.Settings }) as CompilerBase;
      if (c == null) {
        throw new ArgumentException(String.Format("Could not create compiler of type {0}", compiler_type.FullName));
      }

      _compilers.Add(c);
    }

    /// <summary>
    /// Tests if any registered language can handle the given path.
    /// </summary>
    /// <param name="path">Path to file to test.</param>
    /// <returns>True if any registered compiler can handle the compilation.</returns>
    public override bool CanCompileFile(string path) {
      return _compilers.Any((c) => c.CanCompileFile(path));
    }

    /// <summary>
    /// Compile the given file paths
    /// </summary>
    /// <param name="paths">Paths to files to compile</param>
    /// <returns>The result of compilation</returns>
    /// <exception cref="ArgumentException">Path cannot be compiled.</exception>
    public override ICompilerResults CompileFiles(IEnumerable<string> paths) {
      // groups is IEnumerable<IGrouping<CompilerBase, string>>
      var groups = paths.GroupBy((p) => _compilers.FirstOrDefault((c) => c.CanCompileFile(p)));

      if (groups.FirstOrDefault((g) => g.Key == null) != null) {
        throw new ArgumentException("Not all file paths can be compiled.");
      }

      List<ICompilerResults> results = new List<ICompilerResults>();

      foreach (IGrouping<CompilerBase, string> group in groups) {
        CompilerBase c = group.Key;
        results.Add(c.CompileFiles(group));
      }

      return new AggregateCompilerResults(results);
    }
  }
}
