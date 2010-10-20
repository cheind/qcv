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

namespace QCV.Base {

  /// <summary>
  /// Information on the target framework version of .NET
  /// </summary>
  /// <remarks>Since the target framework version is not programmatically accessible
  /// from code, we circumvent this limitation by defining conditional preprocessor
  /// definitions in the project file that has access to the target framework version.
  /// For each target framework version a conditional constant should be defined
  /// <code>
  /// <DefineConstants Condition=" '$(TargetFrameworkVersion)' == 'v3.5' ">NETTARGETFRAMEWORK_35</DefineConstants>
  /// </code>
  /// Finally the <see cref="TargetFramework.Version"/> property has to be extended by a conditional compilation section.
  /// </remarks>
  public static class TargetFramework {

    /// <summary>
    /// Gets a value representing the target framework version of .NET
    /// </summary>
    public static Version Version {
      get {  
#if NETTARGETFRAMEWORK_40
        return new Version(4, 0);
#elif NETTARGETFRAMEWORK_35
        return new Version(3, 5);
#else
        return new Version(3, 5);
#endif
      }
    }
  }
}
