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
using System.ComponentModel;

using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Runtime.Serialization;

namespace QCV.Sources {
  
  /// <summary>
  /// Represents a camera source
  /// </summary>
  [Serializable]
  [Base.Addins.Addin]
  public class Video : Source, ISerializable {

    private string _path = null;
    private Emgu.CV.Capture _device = null;
    
    public Video() {}

    public Video(SerializationInfo info, StreamingContext context)
    {
      string path = (string)info.GetValue("path", typeof(string));
      this.VideoPath = path;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("path", _path);
    }


    public string VideoPath {
      get { lock (this) { return _path; } }
      set {
        lock(this) {
          if (_device != null) {
            _device.Dispose();
            _device = null;
          }
          try {
            if (File.Exists(value)) {
              _device = new Emgu.CV.Capture(value);
              _path = value;
            } else {
              _path = null;
              _device = null;
            }
          } catch (NullReferenceException) {
            _path = null;
            _device = null;
          }
        }
      }
    }

    protected override void DisposeManaged() {
      if (_device != null) {
        _device.Dispose();
      }
    }

    public Image<Bgr, byte> Frame() {
      if (_device != null) {
        Image<Bgr, byte> i = _device.QueryFrame();
        if (i == null && this.Loop) {
          this.VideoPath = _path;
          i = _device.QueryFrame();
        }
        return i;
      } else {
        return null;
      }
    }

    public override void Execute(QCV.Base.Bundle bundle, CancelEventArgs e) {
      Image<Bgr, byte> i = this.Frame();
      bundle.Store(this.Name, i);
      e.Cancel = (i == null);
    }
  }
}
