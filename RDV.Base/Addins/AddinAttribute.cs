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

namespace RDV.Base.Addins {

  /// <summary>
  /// Flags addins
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class AddinAttribute : System.Attribute {
    public AddinAttribute() {}
  }
}
