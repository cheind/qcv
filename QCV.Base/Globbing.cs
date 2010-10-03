using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QCV.Base {

  public static class Globbing {

    public static IEnumerable<string> Glob(string glob) {
      string new_glob = glob.Replace('/', '\\');
      int last_dir_pos = new_glob.LastIndexOf(Path.DirectorySeparatorChar);
      if (last_dir_pos >= 0) {
        string head = new_glob.Substring(0, last_dir_pos);
        string tail = new_glob.Substring(last_dir_pos + 1, new_glob.Length - last_dir_pos - 1);
        string full_head = Path.GetFullPath(head);
        return Directory.GetFiles(full_head, tail);
      } else {
        return Directory.GetFiles(Environment.CurrentDirectory, new_glob);
      }
    }
  }
}
