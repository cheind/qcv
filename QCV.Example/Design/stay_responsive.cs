// qcv.exe -s stay_responsive.cs Tutorial.StayResponsive --run

using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions;

namespace Tutorial {

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
        // Test if a cancellation request is pending
        if (bundle.GetRuntime().CancellationPending) {
          return;
        }

        System.Threading.Thread.Sleep(50);
      }
    }

  }
}