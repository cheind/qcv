using System;
using System.Collections.Generic;

using QCV.Base;
using QCV.Base.Addins;

namespace Tutorial {

  [Addin]
  public class HelloWorld : IFilter, IFilterListProvider {
    
    // Create a new FilterList containing a single HelloWorld filter.
    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        this
      };
    }
    
    // Execute filter
    public void Execute(Dictionary<string, object> bundle) {
      System.Console.WriteLine("Hello World!");
      bundle["cancel"] = true;
    }
    
  }
}