using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class DemoFilter : QCV.Base.IFilter {

    public void Execute(QCV.Base.Bundle b, System.ComponentModel.CancelEventArgs e) {
      Console.WriteLine(String.Format("Executing {0}", this.GetType().FullName));
    }

  }


  [Base.Addins.Addin]
  public class HelloAddin : IExample {

    public void Run(string[] args) {
      
      // Make sure addin framework scans current assembly
      QCV.Base.Addins.AddinHost.DiscoverInAssembly(Assembly.GetExecutingAssembly());

      QCV.Base.IFilter filter =
        QCV.Base.Addins.AddinHost.FindAndCreateInstance(
        typeof(QCV.Base.IFilter),
        "QCV.ConsoleExample.DemoFilter") as QCV.Base.IFilter;

      if (filter != null) {
        filter.Execute(null, null);
      }
    }
  }
}
