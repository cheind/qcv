// qcv.exe -s displaying_values.cs Example.Basic.DisplayingValues --run

using System;
using System.Collections.Generic;
using System.Diagnostics;

using QCV.Base;
using QCV.Base.Extensions;

namespace Example.Basic {

  [Addin]
  public class DisplayingValues : IFilter, IFilterListProvider {
    // Stopwatch to measure elapsed time between calls
    private Stopwatch _sw = new Stopwatch();
    
    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        // Display FPS
        this
      };
    }
    
    public void Execute(Dictionary<string, object> bundle) {
      if (_sw.IsRunning) {
        _sw.Stop();
        IDataInteractor idi = bundle.GetInteractor();
        idi.Show("DisplayingValues.FPS", 1.0 / _sw.Elapsed.TotalSeconds);
        _sw.Reset();
      }
      _sw.Start();      
    }
    
  }
}