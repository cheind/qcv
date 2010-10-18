// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

using Emgu.CV;
using Emgu.CV.Structure;

using QCV.Base.Extensions;

namespace QCV.Toolbox {

  /// <summary>
  /// A filter that reads images from a directory. 
  /// </summary>
  [Serializable]
  [Base.Addin]
  public class ImageList : Source {
    /// <summary>
    /// Path to directory to read images from.
    /// </summary>
    private string _directory_path = null;

    /// <summary>
    /// The wildcard pattern to select images.
    /// </summary>
    private string _pattern = null;

    /// <summary>
    /// Current id of image.
    /// </summary>
    private int _id = 0;

    /// <summary>
    /// Array of files found in directory.
    /// </summary>
    private string[] _files = new string[0];

    /// <summary>
    /// True if filter should loop, false otherwise.
    /// </summary>
    private bool _loop = false;

    /// <summary>
    /// Initializes a new instance of the ImageList class.
    /// </summary>
    public ImageList() 
    {}

    /// <summary>
    /// Initializes a new instance of the ImageList class.
    /// </summary>
    /// <param name="directory_path">Path to directory containing the images</param>
    /// <param name="file_pattern">Pattern to use for selecting images</param>
    public ImageList(string directory_path, string file_pattern) {
      this.DirectoryPath = directory_path;
      this.FilePattern = file_pattern;
    }

    /// <summary>
    /// Initializes a new instance of the ImageList class.
    /// </summary>
    /// <param name="directory_path">Path to directory containing the images</param>
    /// <param name="file_pattern">Pattern to use for selecting images</param>
    /// <param name="name">Keyname to store images in bundle</param>
    public ImageList(string directory_path, string file_pattern, string name) {
      this.DirectoryPath = directory_path;
      this.FilePattern = file_pattern;
      this.Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the ImageList class.
    /// </summary>
    /// <param name="directory_path">Path to directory containing the images</param>
    /// <param name="file_pattern">Pattern to use for selecting images</param>
    /// <param name="name">Keyname to store images in bundle</param>
    /// <param name="loop">True if filter should loop, false otherwise</param>
    public ImageList(string directory_path, string file_pattern, string name, bool loop) {
      this.DirectoryPath = directory_path;
      this.FilePattern = file_pattern;
      this.Name = name;
      this.Loop = loop;
    }

    /// <summary>
    /// Initializes a new instance of the ImageList class.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    protected ImageList(SerializationInfo info, StreamingContext context) : base(info, context)
    {
      _id = 0;
      string path = (string)info.GetValue("directory_path", typeof(string));
      string pattern = (string)info.GetValue("pattern", typeof(string));
      this.Loop = info.GetBoolean("loop");
      this.DirectoryPath = path;
      this.FilePattern = pattern;
    }

    /// <summary>
    /// Gets or sets the directory containing the images.
    /// </summary>
    public string DirectoryPath {
      get { 
        return _directory_path; 
      }

      set {
        _directory_path = value;
        UpdateFiles();
      }
    }

    /// <summary>
    /// Gets or sets the pattern to select the images.
    /// </summary>
    public string FilePattern {
      get { 
        return _pattern; 
      }

      set {
        _pattern = value;
        UpdateFiles();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the filter should loop images or not.
    /// </summary>
    public bool Loop {
      get { return _loop; }
      set { _loop = value; }
    }

    /// <summary>
    /// Produce the next frame.
    /// </summary>
    /// <returns>The next image in the image list or null</returns>
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

    /// <summary>
    /// Execute the filter
    /// </summary>
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
      info.AddValue("directory_path", _directory_path);
      info.AddValue("pattern", _pattern);
      info.AddValue("loop", _loop);
    }

    /// <summary>
    /// Update the list of images.
    /// </summary>
    private void UpdateFiles() {
      lock (this) {
        if (_directory_path != null && _pattern != null) {
          if (Directory.Exists(_directory_path)) {
            _files = Directory.GetFiles(_directory_path, _pattern);
            _id = 0;
          } else {
            throw new ArgumentException("Directory does not exist");
          }
        }
      }
    }

  }
}
