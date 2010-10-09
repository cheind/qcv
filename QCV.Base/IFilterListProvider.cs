// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

namespace QCV.Base {
  public interface IFilterListProvider {
    FilterList CreateFilterList(Addins.AddinHost h);
  }
}
