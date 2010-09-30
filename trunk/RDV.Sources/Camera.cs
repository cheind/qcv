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
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace RDV.Sources {
  
  /// <summary>
  /// Represents a camera source
  /// </summary>
  [Serializable]
  public class Camera : Source {

    private int _device_index = -1;
    private Emgu.CV.Capture _device = null;
    
    public Camera() {}

    public Camera(SerializationInfo info, StreamingContext context)
    {
      _device_index = -1;
      int dev_id = (int)info.GetValue("device_index", typeof(int));
      this.DeviceIndex = dev_id;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("device_index", _device_index);
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
            _device_index = -1;
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
        return _device.QueryFrame();
      } else {
        return null;
      }
    }

    public bool Frame(RDV.Base.Bundle bundle) {
      Image<Bgr, byte> i = this.Frame();
      bundle.Store(this.Name, i);
      return i != null;
    }
  }
}
