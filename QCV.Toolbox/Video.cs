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
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace QCV.Toolbox {
  
  /// <summary>
  /// Represents a camera source
  /// </summary>
  [Serializable]
  [Base.Addins.Addin]
  public class Video : Source {

    private string _path = null;
    private Emgu.CV.Capture _device = null;
    private bool _loop = false;
    
    public Video() {}

    public Video(string video_path) 
    {
      this.VideoPath = video_path;
    }

    public Video(string video_path, string name) {
      this.Name = name;
      this.VideoPath = video_path;
    }

    public Video(string video_path, string name, bool loop) {
      this.Name = name;
      this.VideoPath = video_path;
      this.Loop = loop;
    }

    public Video(SerializationInfo info, StreamingContext context) : base (info, context)
    {
      string path = (string)info.GetValue("path", typeof(string));
      this.Loop = info.GetBoolean("loop");
      this.VideoPath = path;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("path", _path);
      info.AddValue("loop", _loop);
    }

    public bool Loop {
      get { return _loop; }
      set { _loop = value; }
    }


    [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
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
        _device = null;
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

    public override void Execute(Dictionary<string, object> b) {
      Image<Bgr, byte> i = this.Frame();
      b[this.Name] = i;
      b["cancel"] = (i == null);
    }
  }
}
