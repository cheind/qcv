using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.ConsoleExample {

  class MyFilterList : Base.IFilterListProvider {
    
    public QCV.Base.FilterList CreateFilterList(QCV.Base.AddinHost h) {
      return new QCV.Base.FilterList() {
        h.CreateInstance<Base.IFilter>("QCV.Toolbox.Camera", new object[]{0, 320, 200, "camera1"}),
        h.CreateInstance<Base.IFilter>("QCV.Toolbox.ShowImage", new object[]{"camera1"})
      };
    }
  }

  [Base.Addin]
  public class HelloFilterListProvider : IExample {
    public void Run(string[] args) {

      QCV.Base.AddinHost h = new QCV.Base.AddinHost();
      h.DiscoverInDomain();
      h.DiscoverInDirectory(Environment.CurrentDirectory);

      Base.IFilterListProvider p = new MyFilterList();

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();
      Dictionary<string, object> env = new Dictionary<string, object>() {
        {"interactor", new QCV.Base.ConsoleDataInteractor(runtime)}
      };

      runtime.CycleTime.FPS = 30.0;
      runtime.Start(p.CreateFilterList(h), env, 10);
      runtime.Shutdown();
    }
  }
}

