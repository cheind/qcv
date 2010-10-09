// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

namespace QCV.Base.Addins {

  /// <summary>
  /// Holds information about a particular addin.
  /// </summary>
  public class AddinInfo {

    /// <summary>
    /// Reflection type of addin
    /// </summary>
    private Type _type;

    /// <summary>
    /// Initializes a new instance of the AddinInfo class.
    /// </summary>
    /// <param name="t">Type this info refers to.</param>
    public AddinInfo(Type t) {
      _type = t;
    }

    /// <summary>
    /// Gets the assembly the addin is defined in.
    /// </summary>
    public Assembly Assembly {
      get { return _type.Assembly; }
    }

    /// <summary>
    /// Gets the reflection type of the addin.
    /// </summary>
    public Type Type {
      get { return _type; }
    }

    /// <summary>
    /// Gets the full name of the addin type.
    /// </summary>
    public string FullName {
      get { return _type.FullName; }
    }

    /// <summary>
    /// Gets the addins inner namespace name.
    /// </summary>
    public string Name {
      get { return _type.Name; }
    }

    /// <summary>
    /// Gets a value indicating whether the addin is default constructible or not.
    /// </summary>
    public bool DefaultConstructible {
      get {
        return
            _type.IsAbstract == false
            && _type.IsGenericTypeDefinition == false
            && _type.IsInterface == false
            && _type.GetConstructor(Type.EmptyTypes) != null;
      }
    }

    /// <summary>
    /// Report name of addin
    /// </summary>
    /// <returns>Addin name</returns>
    public override string ToString() {
      return this.FullName;
    }

    /// <summary>
    /// Fetch the first attribute of the given type
    /// </summary>
    public T Attribute<T>() where T : System.Attribute {
      System.Attribute a = System.Attribute.GetCustomAttribute(this.Type, typeof(T));
      if (a != null) {
        return a as T;
      } else {
        return null;
      }
    }

    /// <summary>
    /// Test if addin is of given type
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool TypeOf(Type t) {
      return t.IsAssignableFrom(_type);
    }
  }

  /// <summary>
  /// Compare two instances of AddinInfo by their types full name.
  /// </summary>
  class AddinInfoFullNameComparer : EqualityComparer<AddinInfo> {

    public override bool Equals(AddinInfo a, AddinInfo b) {
      return a.FullName == b.FullName;
    }

    public override int GetHashCode(AddinInfo a) {
      return a.FullName.GetHashCode();
    }
  }
}
