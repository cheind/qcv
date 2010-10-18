// qcv.exe -s stop_runtime.cs Example.Basic.StopRuntime --run

using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Extensions;

namespace Example.Basic {

  [Addin]
  public class StopRuntime : IFilter, IFilterListProvider {

    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        this
      };
    }

    public void Execute(Dictionary<string, object> bundle) {
      // Request a stop of the runtime.
      // The runtime will process the request as soon as possible,
      // but not before the filter has completed its work.
      bool request_ok = bundle.GetRuntime().RequestStop();
      
      if (!request_ok) {
        // In case the request wasn't posted with success and stopping
        // is a must one can force the runtime to stop by triggering
        // an exception.
        throw new ApplicationException("Failed to request a stop of runtime.");
      }
    }

  }
}