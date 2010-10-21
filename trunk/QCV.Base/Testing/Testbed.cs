// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System.Collections.Generic;

namespace QCV.Base.Testing {

  /// <summary>
  /// Provides a runtime environment for testing purposes
  /// </summary>
  public class Testbed {

    /// <summary>
    /// Runtime to be used
    /// </summary>
    private TestRuntime _runtime = new TestRuntime();

    /// <summary>
    /// Interactor to be used
    /// </summary>
    private TestInteractor _interactor = new TestInteractor();

    /// <summary>
    /// Instance of a bundle.
    /// </summary>
    private Dictionary<string, object> _bundle = new Dictionary<string, object>();

    /// <summary>
    /// Initializes a new instance of the Testbed class.
    /// </summary>
    public Testbed() {
      _bundle["interactor"] = _interactor;
    }

    /// <summary>
    /// Gets the runtime used to drive the test.
    /// </summary>
    public TestRuntime Runtime {
      get { return _runtime; }
    }

    /// <summary>
    /// Gets the test interactor.
    /// </summary>
    public TestInteractor Interactor {
      get { return _interactor; }
    }
    
    /// <summary>
    /// Gets the bundle.
    /// </summary>
    public Dictionary<string, object> Bundle {
      get { return _bundle; }
    }

    /// <summary>
    /// Reset the bundle to empty state.
    /// </summary>
    public void ResetBundle() {
      _bundle.Clear();
      _bundle.Add("interactor", _interactor);
    }
  }
}
