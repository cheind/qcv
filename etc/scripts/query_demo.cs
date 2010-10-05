using System;
using System.Collections.Generic;
using QCV.Base.Extensions;

namespace Scripts {

  [QCV.Base.Addins.Addin]
  public class QueryDemo : QCV.Base.IFilter, QCV.Base.IFilterListProvider {
    
    public QCV.Base.FilterList CreateFilterList(QCV.Base.Addins.AddinHost h) {
      return new QCV.Base.FilterList() {
        h.CreateInstance<QCV.Base.IFilter>("QCV.Toolbox.Camera", new object[]{0, 320, 200, "source"}),
        h.CreateInstance<QCV.Base.IFilter>("QCV.Toolbox.ShowImage", new object[]{"source"}),
        h.CreateInstance<QCV.Base.IFilter>("Scripts.QueryDemo")
      };
    }
    
    public void Execute(Dictionary<string, object> b) {
      bool result = b.FetchInteractor().Query("Continue?", null);
      if (!result) {
        b["cancel"] = true;
      }
    }
  }
}