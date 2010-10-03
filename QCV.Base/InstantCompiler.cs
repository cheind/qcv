using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;

namespace QCV.Base {
  public class InstantCompiler : Resource {
    private Compiler _c;
    private DateTime _last_update = DateTime.Now;
    private List<FileSystemWatcher> _watchers;
    private IEnumerable<string> _files;

    public delegate void BuildEventHandler(object sender, Compiler compiler);
    public event BuildEventHandler BuildSucceededEvent;

    public InstantCompiler(IEnumerable<string> watch_files)
    {
      _c = new Compiler();
      _files = watch_files;
      InstallFileSystemWatch(watch_files);
    }

    public InstantCompiler(IEnumerable<string> watch_files,
                           IEnumerable<string> references) 
    {
      _c = new Compiler(references);
      _files = watch_files;
      InstallFileSystemWatch(watch_files);
    }

    private void InstallFileSystemWatch(IEnumerable<string> watch_files) {
      _watchers = new List<FileSystemWatcher>();
      foreach (string file in watch_files) {
        FileSystemWatcher watch = new FileSystemWatcher();
        FileInfo fi = new FileInfo(file);

        watch.Path = fi.Directory.FullName;
        watch.Filter = fi.Name;
        watch.NotifyFilter = System.IO.NotifyFilters.LastWrite;
        watch.IncludeSubdirectories = false;
        watch.Changed +=new FileSystemEventHandler(FileChanged);

        _watchers.Add(watch);
      }
      _watchers.ForEach((w) => w.EnableRaisingEvents = true);
    }

    void FileChanged(object sender, FileSystemEventArgs e) {
      if ((DateTime.Now - _last_update).TotalSeconds > 1.0) {
        // Open file handles
        System.Threading.Thread.Sleep(50); 
        this.Compile();
        _last_update = DateTime.Now;
      }
    }

    protected override void DisposeManaged() {
      _watchers.ForEach((w) => w.Dispose());
    }

    public void Compile() {
      lock (_c) {
        if (_c.CompileFromFile(_files)) {
          if (BuildSucceededEvent != null) {
            BuildSucceededEvent(this, _c);
          }
        }
      }
    }

  }
}
