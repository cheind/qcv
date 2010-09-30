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

namespace QCV.Toolbox.Sources {

  [Serializable]
  public abstract class Source : QCV.Base.Resource, QCV.Base.IFilter {
    private IntrinsicCameraParameters _intrinsics = null;
    private string _name;
    private bool _loop;

    public Source() {
      this.Name = "source";
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

    public abstract void Execute(QCV.Base.Bundle b, System.ComponentModel.CancelEventArgs e);
  }
}
