using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.Base {
  public interface IFilterListProvider {
    FilterList CreateFilterList(Addins.AddinHost h);
  }
}
