// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace QCV.Base {

  /// <summary>
  /// Provides file system globbing functionality
  /// </summary>
  public static class Globbing {

    /// <summary>
    /// Find all files matching the glob pattern.
    /// </summary>
    /// <remarks>The implementation is currently limited to the most simple globbing expression
    /// of the form 'x/y/*.cs'. An improved implementation can be provided once QCV is moved
    /// to .NET 4.0 using the DirectoryInfo.GetFileSystemInfos method and overloads.</remarks>
    /// <param name="glob">The globbing pattern</param>
    /// <returns>File paths matching the globbing expression</returns>
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
