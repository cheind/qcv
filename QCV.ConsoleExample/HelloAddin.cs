using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class DemoFilter : QCV.Base.IFilter {
    private string _message = "no message given";

    public DemoFilter() {}

    public DemoFilter(string message) {
      _message = message;
    }

    public void Execute(Dictionary<string, object> b, System.ComponentModel.CancelEventArgs e) {
      Console.WriteLine(String.Format("Executing {0} : {1}", this.GetType().FullName, _message));
    }
  }


  [Base.Addins.Addin]
  public class HelloAddin : IExample {

    public void Run(string[] args) {
      
      // Make sure addin framework scans current assembly
      QCV.Base.Addins.AddinHost h = new QCV.Base.Addins.AddinHost();
      h.DiscoverInAssembly(Assembly.GetExecutingAssembly());

      QCV.Base.IFilter filter =
        h.CreateInstance<QCV.Base.IFilter>("QCV.ConsoleExample.DemoFilter");

      if (filter != null) {
        filter.Execute(null, null);
      }

      filter =
        h.CreateInstance<QCV.Base.IFilter>(
          "QCV.ConsoleExample.DemoFilter", 
          new object[] { "hello world!" }
        );

      if (filter != null) {
        filter.Execute(null, null);
      }
    }
  }
}
