// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using Emgu.CV;
using Emgu.CV.Structure;
using QCV.Base.Extensions;

namespace QCV.Toolbox {

  /// <summary>
  /// A filter that records a video.
  /// </summary>
  [Base.Addin]
  [Serializable]
  public class RecordVideo : Base.Resource, Base.IFilter, ISerializable {

    /// <summary>
    /// Videowriter backend
    /// </summary>
    private VideoWriter _vw = null;

    /// <summary>
    /// Path to record to
    /// </summary>
    private string _path = "video.avi";
      
    /// <summary>
    /// The key name to fetch the image from the bundle.
    /// </summary>
    private string _bag_name = "source";

    /// <summary>
    /// Target frames per second
    /// </summary>
    private int _fps = 30;

    /// <summary>
    /// Frame width
    /// </summary>
    private int _frame_width = 320;

    /// <summary>
    /// Frame height
    /// </summary>
    private int _frame_height = 200;
    
    /// <summary>
    /// Initializes a new instance of the RecordVideo class.
    /// </summary>
    public RecordVideo() 
    {}

    /// <summary>
    /// Initializes a new instance of the RecordVideo class.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    protected RecordVideo(SerializationInfo info, StreamingContext context)
    {
      _path = (string)info.GetValue("path", typeof(string));
      _bag_name = (string)info.GetValue("bag_name", typeof(string));
      _fps = (int)info.GetValue("fps", typeof(int));
      _frame_width = (int)info.GetValue("frame_width", typeof(int));
      _frame_height = (int)info.GetValue("frame_height", typeof(int));
    }

    /// <summary>
    /// Gets or sets the target frame height
    /// </summary>
    public int FrameHeight {
      get { return _frame_height; }
      set { _frame_height = value; }
    }

    /// <summary>
    /// Gets or sets the target frame width
    /// </summary>
    public int FrameWidth {
      get { return _frame_width; }
      set { _frame_width = value; }
    }

    /// <summary>
    /// Gets or sets the target frames per second.
    /// </summary>
    public int FPS {
      get { return _fps; }
      set { _fps = value; }
    }
    
    /// <summary>
    /// Gets or sets the path of the output video file.
    /// </summary>
    public string VideoPath
    {
      get { return _path; }
      set { _path = value; }
    }

    /// <summary>
    /// Gets or sets the key of the image to use as video input in the bundle.
    /// </summary>
    public string BagName {
      get { return _bag_name; }
      set { _bag_name = value; }
    }

    /// <summary>
    /// Execute the filter.
    /// </summary>
    /// <param name="b">Bundle of information</param>
    public void Execute(Dictionary<string, object> b) {
      if (_vw == null) {
        _vw = new VideoWriter(_path, _fps, _frame_width, _frame_height, true);
      }

      Image<Bgr, byte> i = b.GetImage(_bag_name);
      Size s = i.Size;
      if (s.Width != _frame_width || s.Height != _frame_height) {
        i = i.Resize(_frame_width, _frame_height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
      }

      _vw.WriteFrame(i);
    }

    /// <summary>
    /// Get object data for serialization.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("path", _path);
      info.AddValue("bag_name", _bag_name);
      info.AddValue("fps", _fps);
      info.AddValue("frame_width", _frame_width);
      info.AddValue("frame_height", _frame_height);
    }

    /// <summary>
    /// Dispose managed resources.
    /// </summary>
    protected override void DisposeManaged() {
      _vw.Dispose();
    }
  }
}
