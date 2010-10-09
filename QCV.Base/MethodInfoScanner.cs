// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace QCV.Base {
  public class MethodInfoScanner {

    public static MethodInfo[] FindEventMethods(object instance) {
      if (instance == null) throw new ArgumentException("Instance is null");
      Regex r = new Regex("^On.+");

      return instance.GetType().GetMethods().Where(
        (mi) => {
          ParameterInfo[] parms = mi.GetParameters();
          return r.IsMatch(mi.Name) &&
                 mi.ReturnType == typeof(void) &&
                 parms.Length == 1 &&
                 parms[0].ParameterType == typeof(Dictionary<string, object>);
        }
      ).ToArray();
    }
  }
}
