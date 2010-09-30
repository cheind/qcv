/*
 * RDVision http://rdvision.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using System.Runtime.Serialization;

namespace RDV.Sources {

  [Serializable]
  public abstract class Source : RDV.Base.Resource, ISerializable {
    private IntrinsicCameraParameters _intrinsics = null;
    private string _name;
    private bool _loop;

    public Source() {
      this.Name = "source";
    }

    public Source(SerializationInfo info, StreamingContext context) {
      _intrinsics = (IntrinsicCameraParameters)info.GetValue("intrinsics", typeof(IntrinsicCameraParameters));
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("intrinsics", _intrinsics);
    }

    public string Name {
      get { return _name; }
      set { _name = value; }
    }

    public bool Loop {
      get { return _loop; }
      set { _loop = value; }
    }

    /// <summary>
    /// Access intrinsics of source
    /// </summary>
    public IntrinsicCameraParameters Intrinsics {
      get { return _intrinsics; }
      set { _intrinsics = value; }
    }


  }
}
