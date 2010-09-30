using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

namespace RDV.Base.Extensions {
  public static class BundleExtensions {
    public static Image<Bgr, byte> FetchImage(this Bundle b, string key) {
      return b.Fetch<Image<Bgr, byte>>(key);
    }

    public static IRuntime FetchRuntime(this Bundle b) {
      return b.Fetch<IRuntime>("runtime");
    }

    public static FilterList FetchPlaylist(this Bundle b) {
      return b.Fetch<FilterList>("playlist");
    }
  }

}
