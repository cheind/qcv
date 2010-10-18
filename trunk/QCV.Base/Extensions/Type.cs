// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QCV.Base.Extensions {

  /// <summary>
  /// Extensions for the Type class.
  /// </summary>
  public static class TypeExtensions {

    /// <summary>
    /// Gets a value indicating whether the addin is default constructible or not.
    /// </summary>
    /// <param name="type">Target of extension</param>
    /// <returns>True if type is default constructible, false otherwise</returns>
    public static bool IsDefaultConstructible(this Type type) {
      return
          type.IsAbstract == false
          && type.IsGenericTypeDefinition == false
          && type.IsInterface == false
          && type.GetConstructor(Type.EmptyTypes) != null;
    }

    /// <summary>
    /// Test if type is flagged as addin.
    /// </summary>
    /// <param name="type">Target of extension</param>
    /// <returns>True if type is an addin, false otherwise.</returns>
    public static bool IsAddin(this Type type) {
      return Attribute.IsDefined(type, typeof(QCV.Base.AddinAttribute));
    }

    /// <summary>
    /// Test if type is assignable from given type.
    /// </summary>
    /// <param name="type">Target of extension</param>
    /// <param name="type_of">Type must be assignable from this type.</param>
    /// <returns>True if stored type implements or inherits given type, false otherwise</returns>
    public static bool IsTypeOf(this Type type, Type type_of) {
      return type_of.IsAssignableFrom(type);
    }

  }
}
