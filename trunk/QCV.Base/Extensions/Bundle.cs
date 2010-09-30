using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

namespace QCV.Base.Extensions {
  public static class BundleExtensions {
    public static Image<Bgr, byte> FetchImage(this Bundle b, string key) {
      return b.Fetch<Image<Bgr, byte>>(key);
    }

    public static Runtime FetchRuntime(this Bundle b) {
      return b.Fetch<Runtime>("runtime");
    }

    public static FilterList FetchPlaylist(this Bundle b) {
      return b.Fetch<FilterList>("playlist");
    }

    public static IInteraction FetchInteraction(this Bundle b) {
      return b.Fetch<IInteraction>("interaction");
    }
  }

}
