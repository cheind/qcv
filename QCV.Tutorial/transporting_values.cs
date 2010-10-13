// qcv.exe -s transporting_values.cs Tutorial.TransportingValues --run

using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions;

namespace Tutorial {

  class AFilter : IFilter {
    
    public void Execute(Dictionary<string, object> bundle) {
      bundle["A"] = DateTime.Now;
    }
  }

  class BFilter : IFilter {

    public void Execute(Dictionary<string, object> bundle) {
      System.Console.WriteLine(String.Format("A:{0}", bundle.Get<DateTime>("A")));
    }
  }

  [Addin]
  public class TransportingValues : IFilterListProvider {

    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        new AFilter(),
        new BFilter()
      };
    }

  }
}