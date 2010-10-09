﻿// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.ComponentModel;

namespace QCV.Base {

  /// <summary>
  /// A resource is a disposable object. 
  /// </summary>
  [Serializable]
  public class Resource : IDisposable {
    [NonSerialized]
    private bool _disposed;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Resource() {
      _disposed = false;
    }
    
    /// <summary>
    /// C# finalization code.
    /// Do not override in child classes.
    /// </summary>
    ~Resource() {
      // This destructor will run only if the Dispose method
      // does not get called.
      this.Dispose(false);
    }


    /// <summary>
    /// Dispose resource explicitly.
    /// </summary>
    public void Dispose() {
      // Release managed and unmanaged resources
      Dispose(true);
      // Take object off the finalization queue
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposing called from Dispose or the garbage collector.
    /// </summary>
    /// <remarks>
    /// Dispose is either called by the user invoking Dispose, or by
    /// the garbage collector. If called by the user, disposing will be true.
    /// If called by the garbage collector, disposing will be set to false.
    /// </remarks>
    protected virtual void Dispose(bool disposing) {
      // If not yet disposed
      if (!_disposed) {
        // If disposing equals true, dispose all managed
        // and unmanaged resources.
        if (disposing) {
          DisposeManaged();
        }
        // Call the appropriate methods to clean up
        // unmanaged resources here.
        DisposeUnmanaged();

        _disposed = true;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the resource is already disposed or not.
    /// </summary>
    [Browsable(false)]
    public virtual bool Disposed {
      get { return _disposed; }
    }

    /// <summary>
    /// Release, dispose, other managed resources acquired by this object
    /// </summary>
    protected virtual void DisposeManaged() {
    }

    /// <summary>
    /// Release, dispose, unmanaged resources acquired by this object
    /// </summary>
    protected virtual void DisposeUnmanaged() {
    }
  }
}
