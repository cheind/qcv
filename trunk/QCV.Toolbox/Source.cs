// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;

using System.Runtime.Serialization;

namespace QCV.Toolbox {

  /// <summary>
  /// Base class for filters that produce from a hardware or software
  /// device.
  /// </summary>
  [Serializable]
  public abstract class Source : QCV.Base.Resource, QCV.Base.IFilter, ISerializable {
    /// <summary>
    /// Key name of the source to store images in bundle.
    /// </summary>
    private string _name = "source";

    /// <summary>
    /// Initializes a new instance of the Source class.
    /// </summary>
    public Source() 
    {}

    /// <summary>
    /// Initializes a new instance of the Source class.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    public Source(SerializationInfo info, StreamingContext context)
    {
      this.Name = (string)info.GetValue("name", typeof(string));
    }

    /// <summary>
    /// Gets or sets the name to use to store information in bundle.
    /// </summary>
    public string Name {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// Get object data for serialization.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("name", _name);
    }

    /// <summary>
    /// Execute the filter.
    /// </summary>
    /// <param name="b">Bundle of information</param>
    public abstract void Execute(Dictionary<string, object> b);
  }
}
