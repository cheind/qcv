// qcv.exe -s logging.cs Example.Basic.Logging --run

using System;
using System.Collections.Generic;
using log4net;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions;

namespace Example.Basic {

  [Addin]
  public class Logging : IFilter, IFilterListProvider {
    /// <summary>
    /// Logger for logging purposes.
    /// <summary/>
    private static readonly ILog _logger = LogManager.GetLogger(typeof(Logging));

    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        this
      };
    }

    public void Execute(Dictionary<string, object> bundle) {
      _logger.Info("Hello World!");

      bundle.GetRuntime().RequestStop();
    }

  }
}