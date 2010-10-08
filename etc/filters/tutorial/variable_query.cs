// qcv.exe -s variable_query.cs Tutorial.VariableQuery --run

using System;
using System.Collections.Generic;
using System.Diagnostics;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions;

namespace Tutorial {

  [Addin]
  public class VariableQuery : IFilter, IFilterListProvider {
    
    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        this
      };
    }
    
    public void OnPrintGreeting(Dictionary<string, object> bundle) {
      IDataInteractor idi = bundle.FetchInteractor();
      string name = "John Doe";
      
      if (idi.Query("What's your user name?", name)) {
        // User responded to query
        Console.WriteLine(String.Format("Hello {0}", name));
      } else {
        // User cancelled query, don't greet anyone
      }
      
    }
    
    public void Execute(Dictionary<string, object> bundle) {
      IDataInteractor idi = bundle.FetchInteractor();
      // Process all pending events, supplying them with the current bundle information
      idi.ExecutePendingEvents(this, bundle);
    }
    
  }
}