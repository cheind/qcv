// qcv.exe -s using_addins.cs Example.Basic.UsingAddins --run

using System;
using System.Collections.Generic;
using log4net;

using QCV.Base;
using QCV.Base.Addins;
using QCV.Base.Extensions;

namespace Example.Basic {

  /// <summary>
  /// An dummy filter
  /// </summary>
  /// <remarks>Make sure to flag your addins with the 
  /// <see cref="QCV.Base.Addins.AddinAttribute"/> and provide a 
  /// public class modifier.</remarks>
  [Addin]
  public class AddinFilter : IFilter {
    public void Execute(Dictionary<string, object> bundle) {
      Console.WriteLine("Hello World!");
    }
  }
  
  [Addin]
  public class UsingAddins : IFilterListProvider {
    
    public FilterList CreateFilterList(AddinHost host) {
      return new FilterList() {
        // Create an instance of QCV.Toolbox.Camera
        host.CreateInstance<IFilter>("QCV.Toolbox.Camera", new object[] {0,320,200,"camera"} ),
        // Create an instance of "Example.Basic.AddinFilter"
        host.CreateInstance<IFilter>("Example.Basic.AddinFilter")
      };
    }
  }
}