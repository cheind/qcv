// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

namespace QCV.Base {
  public class Reconfiguration {

    public void Update(FilterList fl, Addins.AddinHost h, out FilterList fl_new) {

      fl_new = new FilterList();
      foreach (IFilter f in fl) {
        Type t = f.GetType();
        Addins.AddinInfo fai = h.FindAddins(
          typeof(IFilter),
          (ai) => { return ai.DefaultConstructible && ai.FullName == t.FullName; }).FirstOrDefault();

        if (fai == null || t == fai.Type) {
          // Just move filter over
          fl_new.Add(f);
        } else {
          // Create a new instance
          fl_new.Add(h.CreateInstance(fai) as IFilter);
        }
      }
    }

    public void CopyPropertyValues(FilterList from, FilterList to) {

      for (int i = 0; i < from.Count; ++i) {
        if (!Object.ReferenceEquals(from[i], to[i])) {
          ProcessProperties(from[i], to[i]);
        }
      }

    }

    private void ProcessProperties(IFilter source, IFilter dest) {
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
