// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace QCV.Base {
  
  /// <summary>
  /// A sequential list of filters.
  /// </summary>
  [Serializable]
  public class FilterList : List<IFilter> {

    /// <summary>
    /// Load a filter list from disk.
    /// </summary>
    /// <param name="path">Path to load from</param>
    /// <returns>The loaded filter list</returns>
    public static FilterList Load(string path) {
      FilterList p = null;
      using (Stream s = File.Open(path, FileMode.Open)) {
        if (s != null) {
          IFormatter formatter = new BinaryFormatter();
          p = formatter.Deserialize(s) as FilterList;
        }
      }

      return p;
    }

    /// <summary>
    /// Save a filter list to disk.
    /// </summary>
    /// <param name="path">Path to save to</param>
    /// <param name="fl">Filter list to save</param>
    public static void Save(string path, FilterList fl) {
      using (Stream s = File.OpenWrite(path)) {
        if (s != null) {
          IFormatter formatter = new BinaryFormatter();
          formatter.Serialize(s, fl);
        }
      }
    }
  }
}
