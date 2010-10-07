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

namespace QCV.Toolbox {

  [Serializable]
  public abstract class Source : QCV.Base.Resource, QCV.Base.IFilter, ISerializable {
    private string _name = "source";
    private bool _loop = false;

    public Source() 
    {}

    public Source(SerializationInfo info, StreamingContext context)
    {
      this.Name = (string)info.GetValue("name", typeof(string));
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("name", _name);
      info.AddValue("loop", _loop);
    }

    public string Name {
      get { return _name; }
      set { _name = value; }
    }


    public abstract void Execute(Dictionary<string, object> b);
  }
}
