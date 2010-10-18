// qcv.exe -s stay_responsive.cs Example.Basic.StayResponsive --run

using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Extensions;

namespace Example.Basic {

  [Addin]
  public class StayResponsive : IFilter, IFilterListProvider {

    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        this
      };
    }

    public void Execute(Dictionary<string, object> bundle) {

      // If you have a long running operation, make sure 
      // to stay responsive cancellation events.

      while (true) {
        // Test if a stop request is pending
        if (bundle.GetRuntime().StopRequested) {
          return;
        }

        System.Threading.Thread.Sleep(50);
      }
    }

  }
}