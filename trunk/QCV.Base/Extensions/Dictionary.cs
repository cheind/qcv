// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;
using log4net;

namespace QCV.Base.Extensions {

  /// <summary>
  /// Extensions for the generic Dictionary class.
  /// </summary>
  public static class DictionaryExtensions {

    /// <summary>
    /// Retrieve a typed element by name.
    /// </summary>
    /// <typeparam name="T">Type to cast element to</typeparam>
    /// <param name="b">Dictionary to query</param>
    /// <param name="key">The name of the element</param>
    /// <returns>The requested element</returns>
    public static T Get<T>(this Dictionary<string, object> b, string key) {
      return (T)b[key];
    }

    /// <summary>
    /// Retrieve an image by name.
    /// </summary>
    /// <param name="b">Dictionary to query</param>
    /// <param name="key">The name of the element</param>
    /// <returns>The requested image</returns>
    public static Image<Bgr, byte> GetImage(this Dictionary<string, object> b, string key) {
      return b.Get<Image<Bgr, byte>>(key);
    }

    /// <summary>
    /// Retrieve the runtime.
    /// </summary>
    /// <param name="b">Dictionary to query</param>
    /// <returns>The runtime</returns>
    public static IRuntime GetRuntime(this Dictionary<string, object> b) {
      return b.Get<IRuntime>("runtime");
    }

    /// <summary>
    /// Retrieve the filter list.
    /// </summary>
    /// <param name="b">Dictionary to query</param>
    /// <returns>The filter list</returns>
    public static FilterList GetFilterList(this Dictionary<string, object> b) {
      return b.Get<FilterList>("filterlist");
    }

    /// <summary>
    /// Retrieve the data interactor.
    /// </summary>
    /// <param name="b">Dictionary to query</param>
    /// <returns>The data interactor</returns>
    public static IDataInteractor GetInteractor(this Dictionary<string, object> b) {
      return b.Get<IDataInteractor>("interactor");
    }
  }

}
