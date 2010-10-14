// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

namespace QCV.Base {

  /// <summary>
  /// Defines the interface for creators of filter lists.
  /// </summary>
  public interface IFilterListProvider {

    /// <summary>
    /// Create the filter list.
    /// </summary>
    /// <param name="h">The collection of known addins</param>
    /// <returns>A new filter list</returns>
    FilterList CreateFilterList(Addins.AddinHost h);
  }
}
