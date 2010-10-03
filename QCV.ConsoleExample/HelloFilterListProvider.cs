using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {

  class MyFilterList : Base.IFilterListProvider {
    
    public QCV.Base.FilterList CreateFilterList(QCV.Base.Addins.AddinHost h) {
      return new QCV.Base.FilterList() {
        h.CreateInstance<Base.IFilter>("QCV.Toolbox.Camera", new object[]{0, 320, 200, "camera1"}),
        h.CreateInstance<Base.IFilter>("QCV.Toolbox.ShowImage", new object[]{"camera1"})
      };
    }
  }

  [Base.Addins.Addin]
  public class HelloFilterListProvider : IExample {
    public void Run(string[] args) {

      QCV.Base.Addins.AddinHost h = new QCV.Base.Addins.AddinHost();
      h.DiscoverInDomain();
      h.DiscoverInDirectory(Environment.CurrentDirectory);

      Base.IFilterListProvider p = new MyFilterList();

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      Dictionary<string, object> env = new Dictionary<string, object>() {
        {"interaction", new QCV.Base.ConsoleInteraction(runtime)}
      };

      runtime.FPS = 30.0;
      runtime.Run(p.CreateFilterList(h), env, 10);
      runtime.Shutdown();
    }
  }
}

