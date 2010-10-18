﻿/*
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
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Drawing;
using log4net;

using QCV.Base.Extensions;

namespace QCV.Toolbox {
  
  /// <summary>
  /// Represents a camera source
  /// </summary>
  [Serializable]
  [Base.Addin]
  public class Camera : Source {
    private static readonly ILog _logger = LogManager.GetLogger(typeof(Camera));
    private int _device_index = -1;
    private Emgu.CV.Capture _device = null;
    
    public Camera() {
      this.DeviceIndex = 0;
    }

    public Camera(int device_index) {
      this.DeviceIndex = device_index;
    }

    public Camera(int device_index, int width, int height) {
      this.DeviceIndex = device_index;
      this.FrameWidth = width;
      this.FrameHeight = height;
    }

    public Camera(int device_index, int width, int height, string name) {
      this.DeviceIndex = device_index;
      this.FrameWidth = width;
      this.FrameHeight = height;
      this.Name = name;
    }

    public Camera(SerializationInfo info, StreamingContext context) : base(info, context)
    {
      _device_index = -1;
      int dev_id = (int)info.GetValue("device_index", typeof(int));
      this.DeviceIndex = dev_id;
      Size last_frame_size = (Size)info.GetValue("frame_size", typeof(Size));
      this.FrameWidth = last_frame_size.Width;
      this.FrameHeight = last_frame_size.Height;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("device_index", _device_index);
      info.AddValue("frame_size", new Size(this.FrameWidth, this.FrameHeight));
    }

    public int DeviceIndex {
      get { lock (this) { return _device_index; } }
      set {
        lock(this) {
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
    /// Frame width of device
    /// </summary>
    public int FrameWidth {
      get { return (int)GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 0); }
      set { SetProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, value); }
    }

    /// <summary>
    /// Frame height of device
    /// </summary>
    public int FrameHeight {
      get { return (int)GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 0); }
      set { SetProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, value); }
    }

    public Image<Bgr, byte> Frame() {
      if (_device != null) {
        return _device.QueryFrame();
      } else {
        return null;
      }
    }

    public override void Execute(Dictionary<string, object> b) {
      Image<Bgr, byte> i = this.Frame();
      b[this.Name] = i;
      if (i == null) {
        b.GetRuntime().RequestStop();
      }
    }

    protected override void DisposeManaged() {
      if (_device != null) {
        _device.Dispose();
        _device = null;
      }
    }

    private double GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP prop, double def) {
      double value = def;
      lock (this) {
        if (_device != null) {
          value = _device.GetCaptureProperty(prop);
        }
      }
      return value;
    }

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
