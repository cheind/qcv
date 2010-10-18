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
using log4net;
using QCV.Base.Extensions;

namespace QCV.Toolbox {
  
  /// <summary>
  /// A filter that reads images from a camera device.
  /// </summary>
  [Serializable]
  [Base.Addin]
  public class Camera : Source {
    /// <summary>
    /// Logger used for logging purposes.
    /// </summary>
    private static readonly ILog _logger = LogManager.GetLogger(typeof(Camera));

    /// <summary>
    /// Hardware device index of camera.
    /// </summary>
    private int _device_index = -1;

    /// <summary>
    /// Capture device used.
    /// </summary>
    private Emgu.CV.Capture _device = null;
    
    /// <summary>
    /// Initializes a new instance of the Camera class.
    /// </summary>
    public Camera() {
      this.DeviceIndex = 0;
    }

    /// <summary>
    /// Initializes a new instance of the Camera class.
    /// </summary>
    /// <param name="device_index">Device index of camera</param>
    public Camera(int device_index) {
      this.DeviceIndex = device_index;
    }

    /// <summary>
    /// Initializes a new instance of the Camera class.
    /// </summary>
    /// <param name="device_index">Device index of camera</param>
    /// <param name="width">Target frame width</param>
    /// <param name="height">Target frame height</param>
    public Camera(int device_index, int width, int height) {
      this.DeviceIndex = device_index;
      this.FrameWidth = width;
      this.FrameHeight = height;
    }

    /// <summary>
    /// Initializes a new instance of the Camera class.
    /// </summary>
    /// <param name="device_index">Device index of camera</param>
    /// <param name="width">Target frame width</param>
    /// <param name="height">Target frame height</param>
    /// <param name="name">Keyname to use to store images in bundle</param>
    public Camera(int device_index, int width, int height, string name) {
      this.DeviceIndex = device_index;
      this.FrameWidth = width;
      this.FrameHeight = height;
      this.Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the Camera class.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    protected Camera(SerializationInfo info, StreamingContext context) : base(info, context)
    {
      _device_index = -1;
      int dev_id = (int)info.GetValue("device_index", typeof(int));
      this.DeviceIndex = dev_id;
      Size last_frame_size = (Size)info.GetValue("frame_size", typeof(Size));
      this.FrameWidth = last_frame_size.Width;
      this.FrameHeight = last_frame_size.Height;
    }

    /// <summary>
    /// Gets or sets the hardware device index.
    /// </summary>
    public int DeviceIndex {
      get { 
        lock (this) { 
          return _device_index; 
        } 
      }

      set {
        lock (this) {
          if (_device != null) {
            _device.Dispose();
            _device = null;
          }

          try {
            if (value >= 0) {
              _device = new Emgu.CV.Capture(value);
              _device_index = value;
            } else {
              _device_index = -1;
              _device = null;
            }
          } catch (NullReferenceException) {
            _logger.Warn(String.Format("Could not connect to camera at device index {0}", value));
            _device_index = -1;
            _device = null;
          }
        }
      }
    }

    /// <summary>
    /// Gets or sets the target frame width of device.
    /// </summary>
    public int FrameWidth {
      get { return (int)GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 0); }
      set { SetProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, value); }
    }

    /// <summary>
    /// Gets or sets the target frame height of device.
    /// </summary>
    public int FrameHeight {
      get { return (int)GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 0); }
      set { SetProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, value); }
    }

    /// <summary>
    /// Produce the next frame.
    /// </summary>
    /// <returns>The next image or null</returns>
    public Image<Bgr, byte> Frame() {
      if (_device != null) {
        return _device.QueryFrame();
      } else {
        return null;
      }
    }

    /// <summary>
    /// Execute the filter.
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
      info.AddValue("device_index", _device_index);
      info.AddValue("frame_size", new Size(this.FrameWidth, this.FrameHeight));
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

    /// <summary>
    /// Read hardware property.
    /// </summary>
    /// <param name="prop">Property to read</param>
    /// <param name="def">Default value</param>
    /// <returns>Property value or default</returns>
    private double GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP prop, double def) {
      double value = def;
      lock (this) {
        if (_device != null) {
          value = _device.GetCaptureProperty(prop);
        }
      }

      return value;
    }

    /// <summary>
    /// Write hardware property.
    /// </summary>
    /// <param name="prop">Property to write</param>
    /// <param name="v">Value to write</param>
    private void SetProperty(Emgu.CV.CvEnum.CAP_PROP prop, int v) {
      double value = v;
      lock (this) {
        if (_device != null) {
          _device.SetCaptureProperty(prop, value);
        }
      }
    }

  }
}
