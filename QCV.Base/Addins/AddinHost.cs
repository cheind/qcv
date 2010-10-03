/*
 * RDVision http://rdvision.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Reflection;
using log4net;

namespace QCV.Base.Addins {

  public class AddinHost : List<AddinInfo> {
    private static readonly ILog _logger = LogManager.GetLogger(typeof(AddinHost));

    /// <summary>
    /// Discovers add-ins from current set of loaded assemblies
    /// </summary>
    public void DiscoverInDomain() {
      foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()) {
        DiscoverInAssembly(a);
      }
    }

    public void DiscoverInAssembly(IEnumerable<Assembly> assemblies) {
      foreach (Assembly a in assemblies) {
        DiscoverInAssembly(a);
      }
    }

    public void Merge(AddinHost other) {
      
      foreach(AddinInfo ai in other) {
        int idx = this.FindIndex((a) => { return a.FullName == ai.FullName; });
        if (idx >= 0) {
          this[idx] = ai;
        }
      }
      
    }

    /// <summary>
    /// Discovers add-ins from current set of loaded assemblies
    /// </summary>
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
    /// Discover addins from the directory path
    /// </summary>
    /// <param name="directory_path">Directory path</param>
    /// <param name="recursive"></param>
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
        Assembly a = AppDomain.CurrentDomain.Load(assembly_path);
        DiscoverInAssembly(a);
      } catch (System.BadImageFormatException) {
        //_logger.Debug(String.Format("'{0}' is not a valid assembly.", assembly_path));
      } catch (System.IO.FileLoadException) {
        //_logger.Debug(String.Format("'{0}' already loaded.", assembly_path));
      } catch (System.TypeLoadException) {
        //_logger.Warn(String.Format("Type load exception during loading of '{0}' occurred.", assembly_path));
      }
    }

    /// <summary>
    /// Find addins that are assignable to the provided type.
    /// </summary>
    /// <param name="type_of">Type</param>
    /// <returns>Enumeration of addin infos</returns>
    public IEnumerable<AddinInfo> FindAddins(Type type_of) {
      return this.Select(ai => ai).Where(ai => ai.TypeOf(type_of));
    }

    /// <summary>
    /// Find addins that are assignable to the provided type.
    /// </summary>
    /// <param name="type_of">Type</param>
    /// <returns>Enumeration of addin infos</returns>
    public IEnumerable<AddinInfo> FindAddins(Type type_of, Func<AddinInfo, bool> predicate) {
      return this.Where(ai => ai.TypeOf(type_of) && predicate(ai));
    }

    /// <summary>
    /// Create default constructed instance of addin
    /// </summary>
    /// <param name="ai"></param>
    /// <returns></returns>
    public object CreateInstance(AddinInfo ai) {
      return CreateInstance(ai, null);
    }

    /// <summary>
    /// Create default constructed instance of addin
    /// </summary>
    /// <param name="ai"></param>
    /// <returns></returns>
    public object CreateInstance(AddinInfo ai, object[] args) {
      return Activator.CreateInstance(ai.Type, args);
    }

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
    /// <param name="t"></param>
    /// <returns></returns>
    private bool IsAddin(Type t) {
      return Attribute.IsDefined(t, typeof(AddinAttribute));
    }
  }
}
