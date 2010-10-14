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
  /// Watches source file for modifications. and builds them when a change occurs.
  /// </summary>
  public class InstantCompiler : Resource {

    /// <summary>
    /// Compiler that performs building of source files.
    /// </summary>
    private Compiler _c;

    /// <summary>
    /// Keeps track of last modification event
    /// </summary>
    private DateTime _last_update = DateTime.Now;

    /// <summary>
    /// List of watchers
    /// </summary>
    private List<FileSystemWatcher> _watchers;

    /// <summary>
    /// List of files to watch for changes.
    /// </summary>
    private IEnumerable<string> _files;

    /// <summary>
    /// Initializes a new instance of the InstantCompiler class.
    /// </summary>
    /// <param name="watch_files">Source files to watch for changes</param>
    /// <param name="debug">True if compilation should generate debug information, false otherwise</param>
    public InstantCompiler(IEnumerable<string> watch_files, bool debug)
    {
      _c = new Compiler(debug);
      _files = watch_files;
      InstallFileSystemWatch(watch_files);
    }

    /// <summary>
    /// Initializes a new instance of the InstantCompiler class.
    /// </summary>
    /// <param name="watch_files">Source files to watch for changes</param>
    /// <param name="references">Additional assembly references passed to compiler</param>
    /// <param name="debug">True if compilation should generate debug information, false otherwise</param>
    public InstantCompiler(IEnumerable<string> watch_files,
                           IEnumerable<string> references,
                           bool debug) 
    {
      _c = new Compiler(references, debug);
      _files = watch_files;
      InstallFileSystemWatch(watch_files);
    }

    /// <summary>
    /// Defines the delegate that corresponds to a build event.
    /// </summary>
    /// <param name="sender">The InstantCompiler that triggered the event</param>
    /// <param name="compiler">The Compiler that performed the compilation</param>
    public delegate void BuildEventHandler(object sender, Compiler compiler);

    /// <summary>
    /// Occurs when a build succeeded.
    /// </summary>
    public event BuildEventHandler BuildSucceededEvent;

    /// <summary>
    /// Manual invoke a compilation
    /// </summary>
    public void Compile() {
      lock (_c) {
        if (_c.CompileFromFile(_files)) {
          if (BuildSucceededEvent != null) {
            BuildSucceededEvent(this, _c);
          }
        }
      }
    }

    /// <summary>
    /// Free resources
    /// </summary>
    protected override void DisposeManaged() {
      _watchers.ForEach((w) => w.Dispose());
    }

    /// <summary>
    /// Create watches on files.
    /// </summary>
    /// <param name="watch_files">Files to create watchers for</param>
    private void InstallFileSystemWatch(IEnumerable<string> watch_files) {
      _watchers = new List<FileSystemWatcher>();
      foreach (string file in watch_files) {
        FileSystemWatcher watch = new FileSystemWatcher();
        FileInfo fi = new FileInfo(file);

        watch.Path = fi.Directory.FullName;
        watch.Filter = fi.Name;
        watch.NotifyFilter = System.IO.NotifyFilters.LastWrite;
        watch.IncludeSubdirectories = false;
        watch.Changed += new FileSystemEventHandler(FileChanged);

        _watchers.Add(watch);
      }

      _watchers.ForEach((w) => w.EnableRaisingEvents = true);
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
        // Open file handles
        System.Threading.Thread.Sleep(50); 
        this.Compile();
        _last_update = DateTime.Now;
      }
    }
  }
}
