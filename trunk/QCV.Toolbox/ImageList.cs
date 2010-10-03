using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Emgu.CV;
using Emgu.CV.Structure;
using System.ComponentModel;


namespace QCV.Toolbox {

  [Serializable]
  [Base.Addins.Addin]
  public class ImageList : Source {
    private string _directory_path = null;
    private string _pattern = null;
    private int _id = 0;
    private string[] _files = new string[0];

    public ImageList() {
    }

    public ImageList(SerializationInfo info, StreamingContext context) : base(info, context)
    {
      _id = 0;
      string path = (string)info.GetValue("directory_path", typeof(string));
      string pattern = (string)info.GetValue("pattern", typeof(string));
      this.DirectoryPath = path;
      this.FilePattern = pattern;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("directory_path", _directory_path);
      info.AddValue("pattern", _pattern);
    }


    public string DirectoryPath {
      get { return _directory_path; }
      set {
        _directory_path = value;
        UpdateFiles();
      }
    }

    public string FilePattern {
      get { return _pattern; }
      set {
        _pattern = value;
        UpdateFiles();
      }
    }

    public Image<Bgr, byte> Frame() {
      lock (this) {
        if (_id < _files.Length) {
          Image<Bgr, byte> i = new Image<Bgr, byte>(_files[_id]);
          _id += 1;
          if (this.Loop && _id == _files.Length) {
            _id = 0;
          }
          return i;
        } else {
          return null;
        }
      }
    }

    public override void Execute(Dictionary<string, object> b, CancelEventArgs e) {
      Image<Bgr, byte> i = this.Frame();
      b[this.Name] = i;
      e.Cancel = (i == null);
    }

    void UpdateFiles() {
      lock (this) {
        if (_directory_path != null && _pattern != null) {
          _files = Directory.GetFiles(_directory_path, _pattern);
          _id = 0;
        }
      }
    }
  }
}
