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
  
  [Serializable]
  public class FilterList : List<IFilter> {

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

    public static void Save(string path, FilterList playlist) {
      using (Stream s = File.OpenWrite(path)) {
        if (s != null) {
          IFormatter formatter = new BinaryFormatter();
          formatter.Serialize(s, playlist);
        }
      }
    }


  }
}
