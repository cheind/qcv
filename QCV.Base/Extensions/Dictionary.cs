using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using log4net;

namespace QCV.Base.Extensions {
  public static class DictionaryExtensions {

    public static T Get<T>(this Dictionary<string, object> b, string key) {
      return (T)b[key];
    }

    public static Image<Bgr, byte> GetImage(this Dictionary<string, object> b, string key) {
      return b.Get< Image<Bgr, byte> >(key);
    }

    public static Runtime GetRuntime(this Dictionary<string, object> b) {
      return b.Get<Runtime>("runtime");
    }

    public static FilterList GetFilterList(this Dictionary<string, object> b) {
      return b.Get<FilterList>("filterlist");
    }

    public static IDataInteractor GetInteractor(this Dictionary<string, object> b) {
      return b.Get<IDataInteractor>("interactor");
    }

    public static ILog GetDefaultLogger(this Dictionary<string, object> b) {
      return b.Get<ILog>("filter_logger");
    }
  }

}
