using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace RDV.Base {
  
  [Serializable]
  public class FilterList : List<Func<Bundle, bool>> {

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
