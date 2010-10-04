using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using log4net;

namespace QCV.Base.Extensions {
  public static class DictionaryExtensions {

    public static T Fetch<T>(this Dictionary<string, object> b, string key) {
      return (T)b[key];
    }

    public static Image<Bgr, byte> FetchImage(this Dictionary<string, object> b, string key) {
      return b.Fetch< Image<Bgr, byte> >(key);
    }

    public static Runtime FetchRuntime(this Dictionary<string, object> b) {
      return b.Fetch<Runtime>("runtime");
    }

    public static FilterList FetchFilterList(this Dictionary<string, object> b) {
      return b.Fetch<FilterList>("filterlist");
    }

    public static IDataInteractor FetchInteractor(this Dictionary<string, object> b) {
      return b.Fetch<IDataInteractor>("interactor");
    }

    public static ILog FetchDefaultLogger(this Dictionary<string, object> b) {
      return b.Fetch<ILog>("filter_logger");
    }
  }

}
