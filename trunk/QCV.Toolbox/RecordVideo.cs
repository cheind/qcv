using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Emgu.CV;

using QCV.Base.Extensions;
using Emgu.CV.Structure;
using System.Drawing;

namespace QCV.Toolbox {

  [Base.Addins.Addin]
  [Serializable]
  public class RecordVideo : Base.Resource, Base.IFilter, ISerializable {
    private VideoWriter _vw = null;
    private string _path = "video.avi", _bag_name = "source";
    private int _fps = 30, _frame_width = 320, _frame_height = 200;
    
    public RecordVideo() {}


    public RecordVideo(SerializationInfo info, StreamingContext context)
    {
      _path = (string)info.GetValue("path", typeof(string));
      _bag_name = (string)info.GetValue("bag_name", typeof(string));
      _fps = (int)info.GetValue("fps", typeof(int));
      _frame_width = (int)info.GetValue("frame_width", typeof(int));
      _frame_height = (int)info.GetValue("frame_height", typeof(int));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("path", _path);
      info.AddValue("bag_name", _bag_name);
      info.AddValue("fps", _fps);
      info.AddValue("frame_width", _frame_width);
      info.AddValue("frame_height", _frame_height);
    }

    public int FrameHeight {
      get { return _frame_height; }
      set { _frame_height = value; }
    }

    public int FrameWidth {
      get { return _frame_width; }
      set { _frame_width = value; }
    }

    public int FPS {
      get { return _fps; }
      set { _fps = value; }
    }
    
    public string VideoPath
    {
      get { return _path; }
      set { _path = value; }
    }

    public string BagName {
      get { return _bag_name; }
      set { _bag_name = value; }
    }

    public void Execute(Dictionary<string, object> b) {
      if (_vw == null) {
        _vw = new VideoWriter(_path, _fps, _frame_width, _frame_height, true);
      }
      Image<Bgr, byte> i = b.GetImage(_bag_name);
      Size s = i.Size;
      if (s != new Size(_frame_width, _frame_height)) {
        i = i.Resize(_frame_width, _frame_height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
      }
      _vw.WriteFrame(i);
    }

    protected override void DisposeManaged() {
      _vw.Dispose();
    }
  }
}
