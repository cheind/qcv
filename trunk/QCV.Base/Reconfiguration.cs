// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using QCV.Base.Extensions;

namespace QCV.Base {

  /// <summary>
  /// Reconfigures a filter list. 
  /// </summary>
  public class Reconfiguration {

    /// <summary>
    /// Creates a new filter list from an existing one.
    /// </summary>
    /// <remarks>If the filter is not found in the addin host or
    /// the type hasn't changed, the old filter is copied by reference to the new
    /// filter list. Otherwise, a new instance of the new type is initialized and
    /// added to the new filter list</remarks>
    /// <param name="fl">Current filter list</param>
    /// <param name="h">List of available addins</param>
    /// <returns>The new filter list</returns>
    public FilterList Update(FilterList fl, AddinHost h) {

      FilterList fl_new = new FilterList();
      foreach (IFilter f in fl) {
        Type t = f.GetType();
        Type addin = h.FindAddins(
          typeof(IFilter),
          (ai) => { return ai.IsDefaultConstructible() && ai.FullName == t.FullName; }).FirstOrDefault();

        if (addin == null || t == addin) {
          // Just move filter over
          fl_new.Add(f);
        } else {
          // Create a new instance
          fl_new.Add(h.CreateInstance(addin) as IFilter);
        }
      }

      return fl_new;
    }

    /// <summary>
    /// Copy property values between two filter lists
    /// </summary>
    /// <param name="from">The filter list to read properties from</param>
    /// <param name="to">The filter list to copy properties to</param>
    public void CopyPropertyValues(FilterList from, FilterList to) {

      for (int i = 0; i < from.Count; ++i) {
        if (!Object.ReferenceEquals(from[i], to[i])) {
          CopyPropertyValues(from[i], to[i]);
        }
      }

    }

    /// <summary>
    /// Copy property values between to filters.
    /// </summary>
    /// <remarks>A property value will only be copied if
    /// the target property is writable and has the same type.</remarks>
    /// <param name="source">The filter to copy from</param>
    /// <param name="dest">The filter to copy to</param>
    private void CopyPropertyValues(IFilter source, IFilter dest) {
      PropertyInfo[] f = source.GetType().GetProperties();
      PropertyInfo[] t = dest.GetType().GetProperties();

      foreach (PropertyInfo fpi in f) {
        if (fpi.CanRead) {
          PropertyInfo tpi = t.FirstOrDefault(
            (pi) => {
              return pi.CanWrite &&
                     pi.PropertyType == fpi.PropertyType &&
                     pi.Name == fpi.Name;
            });
          if (tpi != null) {
            tpi.SetValue(dest, fpi.GetValue(source, null), null);
          }
        }
      }
    }
  }
}
