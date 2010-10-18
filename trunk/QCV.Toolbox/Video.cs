// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms.Design;
using Emgu.CV;
using Emgu.CV.Structure;
using QCV.Base.Extensions;

namespace QCV.Toolbox {
  
  /// <summary>
  /// A filter that reads images from a video file.
  /// </summary>
  /// <remarks>The video filter produces one frame
  /// each time the filter is invoked. All encoded frames will be produced.
  /// Therefore, if the video was recorded with 30fps your target frame 
  /// rate should also equal 30fps.</remarks>
  [Serializable]
  [Base.Addin]
  public class Video : Source {

    /// <summary>
    /// Path to video file.
    /// </summary>
    private string _path = null;

    /// <summary>
    /// Device that will play the video
    /// </summary>
    private Emgu.CV.Capture _device = null;

    /// <summary>
    /// A boolean value indicating whether the video is looped or not.
    /// </summary>
    private bool _loop = false;
    
    /// <summary>
    /// Initializes a new instance of the Video class.
    /// </summary>
    public Video() 
    {}

    /// <summary>
    /// Initializes a new instance of the Video class.
    /// </summary>
    /// <param name="video_path">Path to video file</param>
    public Video(string video_path) 
    {
      this.VideoPath = video_path;
    }

    /// <summary>
    /// Initializes a new instance of the Video class.
    /// </summary>
    /// <param name="video_path">Path to video file</param>
    /// <param name="name">Key to store images in bundle</param>
    public Video(string video_path, string name) {
      this.Name = name;
      this.VideoPath = video_path;
    }

    /// <summary>
    /// Initializes a new instance of the Video class.
    /// </summary>
    /// <param name="video_path">Path to video file</param>
    /// <param name="name">Key to store images in bundle</param>
    /// <param name="loop">True if video should be looped, false otherwise</param>
    public Video(string video_path, string name, bool loop) {
      this.Name = name;
      this.VideoPath = video_path;
      this.Loop = loop;
    }

    /// <summary>
    /// Initializes a new instance of the Video class.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    protected Video(SerializationInfo info, StreamingContext context) : base (info, context)
    {
      string path = (string)info.GetValue("path", typeof(string));
      this.Loop = info.GetBoolean("loop");
      this.VideoPath = path;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the video should be looped or not.
    /// </summary>
    /// <remarks>If a video is not looped, the video filter requests a runtime stop
    /// once it has completed producing the video.</remarks>
    public bool Loop {
      get { return _loop; }
      set { _loop = value; }
    }

    /// <summary>
    /// Gets or sets the path to the video file.
    /// </summary>
    [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
    public string VideoPath {
      get { 
        lock (this) { 
          return _path; 
        } 
      }

      set {
        lock (this) {
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
              throw new ArgumentException("Video path does not exist.");
            }
          } catch (NullReferenceException) {
            _path = null;
            _device = null;
          }
        }
      }
    }

    /// <summary>
    /// Produce next frame.
    /// </summary>
    /// <returns>The next frame if available, null otherwise</returns>
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

    /// <summary>
    /// Execute the video filter.
    /// </summary>
    /// <remarks>If looping of the video is disabled, this filter will
    /// request a stop of the runtime once it has completed producing
    /// all frames of the video.</remarks>
    /// <param name="b">Bundle of information</param>
    public override void Execute(Dictionary<string, object> b) {
      Image<Bgr, byte> i = this.Frame();
      b[this.Name] = i;
      if (i == null) {
        b.GetRuntime().RequestStop();
      }
    }

    /// <summary>
    /// Get object data for serialization.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("path", _path);
      info.AddValue("loop", _loop);
    }

    /// <summary>
    /// Dispose managed resources.
    /// </summary>
    protected override void DisposeManaged() {
      if (_device != null) {
        _device.Dispose();
        _device = null;
      }
    }
  }
}
