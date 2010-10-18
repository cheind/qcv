// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using QCV.Base.Extensions;

namespace QCV.Toolbox {

  /// <summary>
  /// A filter that displays the achieved frames per second.
  /// </summary>
  /// <remarks>This filter provides a resource efficient calculation of cycles per 
  /// second achieved. The result is shown using the <see cref="QCV.Base.IDataInteractor.Show"/> method.
  /// To lower the stress of displaying values and to reduce the flickering, this
  /// method will update its calcuated value at a user specifed interval <see cref="UpdateInterval"/>.
  /// </remarks>
  [Base.Addin]
  [Serializable]
  public class ShowFPS : Base.IFilter, ISerializable {

    /// <summary>
    /// Number of cycles completed.
    /// </summary>
    private int _iterations = 0;

    /// <summary>
    /// Time of last update.
    /// </summary>
    private DateTime _last_update = DateTime.Now;

    /// <summary>
    /// Interval of update.
    /// </summary>
    private double _update_interval = 1.0;

    /// <summary>
    /// Initializes a new instance of the ShowFPS class.
    /// </summary>
    public ShowFPS() 
    {}

    /// <summary>
    /// Initializes a new instance of the ShowFPS class.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    protected ShowFPS(SerializationInfo info, StreamingContext context)
    {
      _iterations = 0;
      _last_update = DateTime.Now;
      this.UpdateInterval = info.GetDouble("update_frequency");
    }

    /// <summary>
    /// Gets or sets the update interval of the filter
    /// </summary>
    public double UpdateInterval {
      get { return _update_interval; }
      set { _update_interval = value; }
    }

    /// <summary>
    /// Get object data for serialization.
    /// </summary>
    /// <param name="info">Serialization info</param>
    /// <param name="context">Streaming context</param>
    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("update_frequency", _update_interval);
    }

    /// <summary>
    /// Execute the filter.
    /// </summary>
    /// <param name="b">Bundle of information</param>
    public void Execute(Dictionary<string, object> b) {
      _iterations += 1;
      DateTime now = DateTime.Now;
      double elapsed = (now - _last_update).TotalSeconds;
      if (elapsed > _update_interval) {
        b.GetInteractor().Show("FPS", _iterations / elapsed );
        _last_update = now;
        _iterations = 0;
      }
    }

  }
}
