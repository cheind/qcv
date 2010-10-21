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
using QCV.Base.Extensions;

namespace QCV.Base {

  /// <summary>
  /// A host for addins.
  /// </summary>
  /// <remarks>
  /// AddinHost provides methods to discover plugins in files and 
  /// assemblies, create instance of plugins and find addins.
  /// </remarks>
  public class AddinHost : List<Type> {
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
      List<Type> addins = new List<Type>();
      foreach (Type t in a.GetExportedTypes()) {
        if (t.IsAddin() && !this.Any(type => type == t)) {
          addins.Add(t);
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
      tmp.AddRange(other.Union(this, new TypeFullNameComparer()));
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
    public IEnumerable<Type> FindAddins(Type type_of) {
      return this.Select(ai => ai).Where(ai => ai.IsTypeOf(type_of));
    }

    /// <summary>
    /// Find addins that are assignable to the provided type.
    /// </summary>
    /// <param name="type_of">Addin type</param>
    /// <param name="predicate">Search predicate</param>
    /// <returns>Enumeration of addin types</returns>
    public IEnumerable<Type> FindAddins(Type type_of, Func<Type, bool> predicate) {
      return this.Where(t => t.IsTypeOf(type_of) && predicate(t));
    }

    /// <summary>
    /// Create default constructed instance of addin
    /// </summary>
    /// <param name="addin">Type of addin</param>
    /// <returns>Initialized addin instance or null</returns>
    public object CreateInstance(Type addin) {
      return CreateInstance(addin, null);
    }

    /// <summary>
    /// Create instance of addin.
    /// </summary>
    /// <param name="addin">Type of addin</param>
    /// <param name="args">Arguments passed to the addins constructor</param>
    /// <returns>Initialized addin instance or null</returns>
    public object CreateInstance(Type addin, object[] args) {
      return Activator.CreateInstance(addin, args);
    }

    /// <summary>
    /// Create instance of default constructible addin.
    /// </summary>
    /// <typeparam name="T">Type of addin</typeparam>
    /// <param name="full_name">Fullname of addin</param>
    /// <returns>Created instance on success, null otherwise</returns>
    public T CreateInstance<T>(string full_name) {
      Type addin = FindAddins(
        typeof(T), 
        (e) => { return e.IsDefaultConstructible() && e.FullName == full_name; }
      ).FirstOrDefault() as Type;
      if (addin != null) {
        return (T)CreateInstance(addin);
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
      Type addin = FindAddins(
        typeof(T),
        (e) => { return e.FullName == full_name; }
      ).FirstOrDefault() as Type;
      if (addin != null) {
        return (T)CreateInstance(addin, args);
      } else {
        return default(T);
      }
    }

  }
}
