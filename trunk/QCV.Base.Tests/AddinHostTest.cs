using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace QCV.Base.Tests {

  [TestFixture]
  public class AddinHostTest {

    [Base.Addin]
    public class CarriesAddin {
    };

    [Test]
    public void DiscoverInAssembly() {
      Base.AddinHost h = new AddinHost();
      h.DiscoverInAssembly(Assembly.GetExecutingAssembly());

      Assert.NotNull(h.FirstOrDefault((t) => t == typeof(CarriesAddin)));
      Assert.Null(h.FirstOrDefault((t) => t == typeof(string)));

      Type type = h.FirstOrDefault((t) => t == typeof(CarriesAddin));
      Assert.NotNull(h.CreateInstance(type) as CarriesAddin);

      Assert.IsInstanceOf<CarriesAddin>(h.CreateInstance<CarriesAddin>("QCV.Base.Tests.AddinHostTest+CarriesAddin"));
    }

    [Test]
    public void DiscoverInFile() {
      Base.AddinHost h = new AddinHost();
      h.DiscoverInFile(Assembly.GetExecutingAssembly().CodeBase);
      Assert.NotNull(h.FirstOrDefault((t) => t == typeof(CarriesAddin)));
      Assert.Null(h.FirstOrDefault((t) => t == typeof(string)));
    }

    [Test]
    public void DiscoverInDirectory() {
      Base.AddinHost h = new AddinHost();
      string file_path = new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
      h.DiscoverInDirectory(Path.GetDirectoryName(file_path));
      Assert.NotNull(h.FirstOrDefault((t) => t == typeof(CarriesAddin)));
      Assert.Null(h.FirstOrDefault((t) => t == typeof(string)));
    }

    [Test]
    public void Create() {
      Base.AddinHost h = new AddinHost();
      h.DiscoverInAssembly(Assembly.GetExecutingAssembly());
      Assert.IsInstanceOf<CarriesAddin>(h.CreateInstance<CarriesAddin>("QCV.Base.Tests.AddinHostTest+CarriesAddin"));
    }



  }
}
