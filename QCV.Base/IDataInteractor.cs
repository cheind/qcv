// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace QCV.Base {

  /// <summary>
  /// Defines the interaction interface between filters and their environment.
  /// </summary>
  public interface IDataInteractor {

    /// <summary>
    /// Show values and images
    /// </summary>
    /// <remarks>
    /// <para>Implementations should associate equal ids. For example, when
    /// the user requests to show an image with the identifier of 'camera', subsequent
    /// calls to show using the same identifier should not result in distinct image windows.
    /// showing the value.</para>
    /// <para>Implementations should provide a reasonable amount of overloads for different
    /// value types. At least they should support displaying of images and displaying of
    /// values in stringized form.</para>
    /// </remarks>
    /// <param name="id">Show identifier</param>
    /// <param name="o">Value to show</param>
    void Show(string id, object o);

    /// <summary>
    /// Post a query and wait for the answer.
    /// </summary>
    /// <remarks>
    /// Ideally, implementations should provide the user a generic UI
    /// that allows him to complete any provided query object. If no query object
    /// is passed, the query should be treated as a yes/no query.
    /// </remarks>
    /// <param name="text">Caption of query.</param>
    /// <param name="o">Optional query object the user should complete.</param>
    /// <returns>False if the query was cancelled, true otherwise.</returns>
    bool Query(string text, object o);

    /// <summary>
    /// Execute pending filter events
    /// </summary>
    /// <remarks>
    /// Filters can expose event methods. These event methods are identified by
    /// by their name, they start with 'On', and accept a bundle as input parameter.
    /// Since filter processing is performed asynchronously, these events need to 
    /// be cached until the filter requests them through <see cref="ExecutePendingEvents"/>.
    /// </remarks>
    /// <seealso cref="EventInvocationCache"/>
    /// <param name="instance">The filter requesting to execute its pending events</param>
    /// <param name="bundle">The bundle parameter to pass to the event methods to be invoked.</param>
    void ExecutePendingEvents(object instance, Dictionary<string, object> bundle);
  }
}
