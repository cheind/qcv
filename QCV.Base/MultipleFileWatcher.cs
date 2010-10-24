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
  /// Watches multiple files for changes.
  /// </summary>
  /// <remarks> MultipleFileWatcher will deal with changed events only. Since a single 
  /// file save process may involve multiple file system events, MultipleFileWatcher 
  /// will try to filter subsequent events if they occur in a high frequency.
  /// </remarks>
  public class MultipleFileWatcher : Resource {
    /// <summary>
    /// Keeps track of last modification event
    /// </summary>
    private DateTime _last_update = DateTime.Now;

    /// <summary>
    /// List of file system watchers
    /// </summary>
    private List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

    /// <summary>
    /// List of files to watch for changes.
    /// </summary>
    private List<string> _files = new List<string>();

    /// <summary>
    /// Initializes a new instance of the MultipleFileWatcher class.
    /// </summary>
    public MultipleFileWatcher() {
    }

    /// <summary>
    /// Occurs when a file or multiple files changed.
    /// </summary>
    public event FileSystemEventHandler Changed;

    /// <summary>
    /// Watches the given file path.
    /// </summary>
    /// <param name="path">Path to file.</param>
    /// <exception cref="ArgumentException">File does not exist</exception>
    public void AddFilePath(string path) 
    {
      if (!File.Exists(path)) {
        throw new ArgumentException(String.Format("File {0} does not exist.", path));
      }

      string abs_path = Path.GetFullPath(path);

      // Do nothing if path is already known
      if (_files.Contains(abs_path)) {
        return;
      }

      FileSystemWatcher watch = new FileSystemWatcher();
      FileInfo fi = new FileInfo(abs_path);

      watch.Path = fi.Directory.FullName;
      watch.Filter = fi.Name;
      watch.NotifyFilter = System.IO.NotifyFilters.LastWrite;
      watch.IncludeSubdirectories = false;
      watch.Changed += new FileSystemEventHandler(FileChanged);

      _watchers.Add(watch);
      _files.Add(abs_path);
    }

    /// <summary>
    /// Watch a set of file paths.
    /// </summary>
    /// <param name="paths">File paths to watch.</param>
    public void AddFilePaths(IEnumerable<string> paths) {
      foreach (string path in paths) {
        this.AddFilePath(path);
      }
    }

    /// <summary>
    /// Free resources
    /// </summary>
    protected override void DisposeManaged() {
      _watchers.ForEach((w) => w.Dispose());
    }

    /// <summary>
    /// Occurs when a watched file has changed.
    /// </summary>
    /// <param name="sender">Sender of event</param>
    /// <param name="e">Arguments of change</param>
    private void FileChanged(object sender, FileSystemEventArgs e) {
      // Since file modifications can trigger multiple events,
      // we only deal with one such event per second.
      if ((DateTime.Now - _last_update).TotalSeconds > 1.0) {
        // Sleep a bit to let open file handles come to rest.
        // Todo: A better idea is to start a countdown timer once
        // a change event is detected and restart the countdown each time
        // another change event occurs within the tick frequency of the
        // timer.
        System.Threading.Thread.Sleep(50);

        FileSystemEventHandler h = Changed;
        if (h != null) {
          h(this, e);
        }

        _last_update = DateTime.Now;
      }
    }

  }
}
