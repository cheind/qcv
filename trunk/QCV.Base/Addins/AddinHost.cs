// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;

namespace QCV.Base.Addins {

  /// <summary>
  /// Hosts AddinInfo instances.
  /// </summary>
  /// <remarks>
  /// AddinHost provides methods to discover plugins in files and 
  /// assemblies, create instance of plugins and find addins.
  /// </remarks>
  public class AddinHost : List<AddinInfo> {
    /// <summary>
    /// Logger object used to log messages.
    /// </summary>
    private static readonly ILog _logger = LogManager.GetLogger(typeof(AddinHost));

    /// <summary>
    /// Discover addins in the current set of loaded assemblies
    /// </summary>
    public void DiscoverInDomain() {
      foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()) {
        DiscoverInAssembly(a);
      }
    }

    /// <summary>
    /// Discover addins in the specified assemblies.
    /// </summary>
    /// <param name="assemblies">The collection of assemblies.</param>
    public void DiscoverInAssembly(IEnumerable<Assembly> assemblies) {
      foreach (Assembly a in assemblies) {
        DiscoverInAssembly(a);
      }
    }

    /// <summary>
    /// Discover addins in the specified assembly
    /// </summary>
    /// <param name="a">The assembly to search in.</param>
    public void DiscoverInAssembly(Assembly a) {
      List<AddinInfo> addins = new List<AddinInfo>();
      foreach (Type t in a.GetExportedTypes()) {
        if (IsAddin(t) && !this.Any(ai => ai.Type == t)) {
          addins.Add(new AddinInfo(t));
        }
      }

      this.AddRange(addins);
    }

    /// <summary>
    /// Merge the content of other AddinHost with this addin host.
    /// </summary>
    /// <remarks>Uses the addins full name for equality testing.
    /// In case of duplicates, the values in other take precedence 
    /// over values in this.</remarks>
    /// <param name="other">AddinHost to merge with</param>
    public void MergeByFullName(AddinHost other) {
      AddinHost tmp = new AddinHost();
      tmp.AddRange(other.Union(this, new AddinInfoFullNameComparer()));
      this.Clear();
      this.AddRange(tmp);      
    }

    /// <summary>
    /// Discover addins from the directory path
    /// </summary>
    /// <param name="directory_path">Directory path</param>
    public void DiscoverInDirectory(string directory_path) {
      if (Directory.Exists(directory_path))
      {
        foreach (string file in Directory.GetFiles(directory_path, "*.dll"))
        {
          DiscoverInFile(file);
        }
      }
    }

    /// <summary>
    /// Discover exported types in assembly
    /// </summary>
    /// <param name="assembly_path">Path to assembly</param>
    public void DiscoverInFile(string assembly_path) {
      try {
        Assembly a = Assembly.LoadFrom(assembly_path);
        DiscoverInAssembly(a);
      } catch (System.BadImageFormatException) {
        // _logger.Debug(String.Format("'{0}' is not a valid assembly.", assembly_path));
      } catch (System.IO.FileLoadException) {

        // _logger.Debug(String.Format("'{0}' already loaded.", assembly_path));
      } catch (System.TypeLoadException) {

        // _logger.Warn(String.Format("Type load exception during loading of '{0}' occurred.", assembly_path));
      }
    }

    /// <summary>
    /// Find addins that are assignable to the provided type.
    /// </summary>
    /// <param name="type_of">Addin type</param>
    /// <returns>Enumeration of addin infos</returns>
    public IEnumerable<AddinInfo> FindAddins(Type type_of) {
      return this.Select(ai => ai).Where(ai => ai.TypeOf(type_of));
    }

    /// <summary>
    /// Find addins that are assignable to the provided type.
    /// </summary>
    /// <param name="type_of">Addin type</param>
    /// <param name="predicate">Search predicate</param>
    /// <returns>Enumeration of addin infos</returns>
    public IEnumerable<AddinInfo> FindAddins(Type type_of, Func<AddinInfo, bool> predicate) {
      return this.Where(ai => ai.TypeOf(type_of) && predicate(ai));
    }

    /// <summary>
    /// Create default constructed instance of addin
    /// </summary>
    /// <param name="ai">Info of addin</param>
    /// <returns>Initialized addin instance or null</returns>
    public object CreateInstance(AddinInfo ai) {
      return CreateInstance(ai, null);
    }

    /// <summary>
    /// Create instance of addin.
    /// </summary>
    /// <param name="ai">Info of addin</param>
    /// <param name="args">Arguments passed to the addins constructor</param>
    /// <returns>Initialized addin instance or null</returns>
    public object CreateInstance(AddinInfo ai, object[] args) {
      return Activator.CreateInstance(ai.Type, args);
    }

    /// <summary>
    /// Create instance of default constructible addin.
    /// </summary>
    /// <typeparam name="T">Type of addin</typeparam>
    /// <param name="full_name">Fullname of addin</param>
    /// <returns>Created instance on success, null otherwise</returns>
    public T CreateInstance<T>(string full_name) {
      AddinInfo ai = FindAddins(
        typeof(T), 
        (e) => { return e.DefaultConstructible && e.FullName == full_name; }
      ).FirstOrDefault() as AddinInfo;
      if (ai != null) {
        return (T)CreateInstance(ai);
      } else {
        return default(T);
      }
    }

    /// <summary>
    /// Create instance of addin.
    /// </summary>
    /// <typeparam name="T">Type of addin</typeparam>
    /// <param name="full_name">Fullname of addin</param>
    /// <param name="args">Arguments to be passed to the addins constructor</param>
    /// <returns>Created instance on success, null otherwise</returns>
    public T CreateInstance<T>(string full_name, object[] args) {
      AddinInfo ai = FindAddins(
        typeof(T),
        (e) => { return e.FullName == full_name; }
      ).FirstOrDefault() as AddinInfo;
      if (ai != null) {
        return (T)CreateInstance(ai, args);
      } else {
        return default(T);
      }
    }

    /// <summary>
    /// Test if type is flagged as addin
    /// </summary>
    /// <param name="t">Type to test</param>
    /// <returns>True if type is an addin, false otherwise.</returns>
    private bool IsAddin(Type t) {
      return Attribute.IsDefined(t, typeof(AddinAttribute));
    }
  }
}
