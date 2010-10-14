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

  /// <summary>
  /// Find methods per reflection.
  /// </summary>
  public class MethodInfoScanner {

    /// <summary>
    /// Find all event methods exposed the filter
    /// </summary>
    /// <param name="instance">The filter to search for events</param>
    /// <returns>A collection of event methods</returns>
    public static MethodInfo[] FindEventMethods(IFilter instance) {
      if (instance == null) {
        throw new ArgumentException("Instance is null");
      }

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
