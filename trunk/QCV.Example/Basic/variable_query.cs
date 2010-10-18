// qcv.exe -s variable_query.cs Tutorial.VariableQuery --run

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions;

namespace Tutorial {

  [Addin]
  public class VariableQuery : IFilter, IFilterListProvider {
    
    // User will be queried to complete the values in this struct
    class Name {
      string _first_name = "John";
      string _last_name = "Doe";
      
      [Description("Your first name")]
      public String FirstName {
        get { return _first_name; }
        set { _first_name = value; }
      }
      
      [Description("Your last name")]
      public String LastName {
        get { return _last_name; }
        set { _last_name = value; }
      }
    };
    
    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        this
      };
    }
    
    public void OnPrintGreeting(Dictionary<string, object> bundle) {
      IDataInteractor idi = bundle.GetInteractor();
      Name n = new Name();
      
      if (idi.Query("What's your name?", n)) {
        // User positively responded to our query
        Console.WriteLine(String.Format("Hello {0} {1}", n.FirstName, n.LastName));
      } 
      
    }
    
    public void Execute(Dictionary<string, object> bundle) {
      IDataInteractor idi = bundle.GetInteractor();
      // Process all pending events, supplying them with the current bundle information
      idi.ExecutePendingEvents(this, bundle);
    }
    
  }
}