// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using Microsoft.VisualC;

namespace QCV.Base {

  /// <summary>
  /// In memory compiler for .NET compliant languages.
  /// </summary>
  [Serializable]
  public class Compiler {
    /// <summary>
    /// Logger for logging purposes.
    /// </summary>
    private static readonly ILog _logger = LogManager.GetLogger(typeof(Compiler));

    /// <summary>
    /// List of compiled assemblies.
    /// </summary>
    private List<CompilerResults> _results = new List<CompilerResults>();

    /// <summary>
    /// Provides compilation for csharp input.
    /// </summary>
    private CSharpCodeProvider _csharp;

    /// <summary>
    /// Provides compilation for vb input.
    /// </summary>
    private VBCodeProvider _vb;

    /// <summary>
    /// Provides compilation for cpp input.
    /// </summary>
    private CppCodeProvider _cpp;

    /// <summary>
    /// Parameters passed to the compiler.
    /// </summary>
    private CompilerParameters _cp;

    /// <summary>
    /// Dictionary of global compiler settings.
    /// </summary>
    private Dictionary<string, string> _pp;

    /// <summary>
    /// Initializes a new instance of the Compiler class.
    /// </summary>
    public Compiler() 
      : this(new string[] {}, false) 
    {}

    /// <summary>
    /// Initializes a new instance of the Compiler class.
    /// </summary>
    /// <param name="debug">True if compilation should support debugging, false otherwise</param>
    public Compiler(bool debug)
      : this(new string[] {}, debug) 
    {}

    /// <summary>
    /// Initializes a new instance of the Compiler class.
    /// </summary>
    /// <param name="references">A list of named assembly references to be used in compilation</param>
    /// <param name="debug">True if compilation should support debugging, false otherwise</param>
    public Compiler(IEnumerable<string> references, bool debug) {
      _cp = new CompilerParameters(references.ToArray());
      _cp.GenerateExecutable = false;
      _cp.GenerateInMemory = !debug;
      _cp.IncludeDebugInformation = debug;
      _cp.TempFiles.KeepFiles = debug;

      _pp = new Dictionary<string, string>() {
        { "CompilerVersion", "v3.5" } 
      };

      _csharp = new CSharpCodeProvider(_pp);
      _vb = new VBCodeProvider(_pp);
      _cpp = new CppCodeProvider();
    }

    /// <summary>
    /// Gets the list of compiler results.
    /// </summary>
    public IList<CompilerResults> CompilerResults {
      get { return _results; }
    }

    /// <summary>
    /// Gets the compiled assemblies.
    /// </summary>
    public IEnumerable<Assembly> CompiledAssemblies {
      get { return _results.Select((cr) => { return cr.CompiledAssembly; }); }
    }

    /// <summary>
    /// Compile the given source file.
    /// </summary>
    /// <param name="source_path">Path to file</param>
    /// <returns>True if compilation succeeded, false otherwise</returns>
    public bool CompileFromFile(string source_path) {
      return CompileFromFile(new string[] { source_path });
    }

    /// <summary>
    /// Compile the given source files.
    /// </summary>
    /// <param name="source_paths">A list of source files</param>
    /// <returns>True if compilation succeeded, false otherwise</returns>
    public bool CompileFromFile(IEnumerable<string> source_paths) {

      // Split sources into various languages
      IEnumerable<string> csharp = source_paths.Where(
        (s) => { return s.EndsWith(_csharp.FileExtension, StringComparison.InvariantCultureIgnoreCase); }
      );

      IEnumerable<string> vb = source_paths.Where(
        (s) => { return s.EndsWith(_vb.FileExtension, StringComparison.InvariantCultureIgnoreCase); }
      );

      IEnumerable<string> cpp = source_paths.Where(
        (s) => { return s.EndsWith(_cpp.FileExtension, StringComparison.InvariantCultureIgnoreCase); }
      );

      try {
        _results = new List<CompilerResults>();

        if (csharp.Count() > 0) {
          _results.Add(_csharp.CompileAssemblyFromFile(_cp, csharp.ToArray()));
        }

        if (vb.Count() > 0) {
          _results.Add(_vb.CompileAssemblyFromFile(_cp, vb.ToArray()));
        }

        if (cpp.Count() > 0) {
          _results.Add(_cpp.CompileAssemblyFromFile(_cp, cpp.ToArray()));
        }

        bool success = _results.All((cr) => { return !cr.Errors.HasErrors; });

        if (success) {
          _logger.Info(FormatCompilerResults());
        } else {
          _logger.Error(FormatCompilerResults());
        }

        return success;

      } catch (IOException err) {
        _logger.Error(String.Format("Failed - Exception raised '{0}'", err.Message));
        return false;
      } catch (ArgumentException err) {
        _logger.Error(String.Format("Failed - Exception raised '{0}'", err.Message));
        return false;
      }
    }

    /// <summary>
    /// Format the results of compilation.
    /// </summary>
    /// <returns>Formatted results</returns>
    public string FormatCompilerResults() {
      StringBuilder sb = new StringBuilder();

      bool success = _results.All((cr) => { return !cr.Errors.HasErrors; });

      if (success) {
        sb.Append("Success");
      } else {
        sb.AppendLine("Failed");
        foreach (CompilerResults cr in _results) {
          sb.Append(FormatErrors(cr));
        }
      }

      return sb.ToString();
    }

    /// <summary>
    /// Format compilation errors.
    /// </summary>
    /// <param name="cr">Compiler results</param>
    /// <returns>Formated compilation errors</returns>
    public string FormatErrors(CompilerResults cr) {
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < cr.Errors.Count; i++) {
        sb.AppendLine(i.ToString() + ": " + cr.Errors[i].ToString());
      }

      string nl = Environment.NewLine;
      string final = sb.ToString();
      return final.Remove(final.Length - nl.Length, nl.Length);
    }
  }
}

