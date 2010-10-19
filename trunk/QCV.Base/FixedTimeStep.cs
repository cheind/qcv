// ----------------------------------------------------------
// <project>QCV</project>
// <author>Christoph Heindl</author>
// <copyright>Copyright (c) Christoph Heindl 2010</copyright>
// <license>New BSD</license>
// ----------------------------------------------------------

using System;
using System.Diagnostics;

namespace QCV.Base {

  /// <summary>
  /// Provides methods to control the cylce time.
  /// </summary>
  /// <remarks>If the cycle-time is not eaten up by code, FixedTimeStep will block the calling thread to
  /// stick with the cylce time.</remarks>
  [Serializable]
  public class FixedTimeStep {

    /// <summary>
    /// The stopwatch measuring the timespan between two cycles.
    /// </summary>
    private Stopwatch _sw;

    /// <summary>
    /// Number of nanosecs per machine tick.
    /// </summary>
    private double _ns_per_tick;

    /// <summary>
    /// Target cycle time to achieve (frames-per-second).
    /// </summary>
    private double _fps;

    /// <summary>
    /// The cycle time measured in ticks.
    /// </summary>
    private long _cycle_time_ticks;

    /// <summary>
    /// The sleep mode to use.
    /// </summary>
    private EPauseMode _pause_mode;

    /// <summary>
    /// A boolean value indicating whether the cylce control is active or not.
    /// </summary>
    private bool _enabled;

    /// <summary>
    /// Initializes a new instance of the FixedTimeStep class.
    /// </summary>
    /// <param name="fps">The target cycle time in cycles per second</param>
    public FixedTimeStep(double fps) {
      _sw = new Stopwatch();
      _ns_per_tick = 1000000000.0 / Stopwatch.Frequency;
      this.FPS = fps;
      this.PauseMode = EPauseMode.Adaptive;
      _enabled = true;
    }

    /// <summary>
    /// Initializes a new instance of the FixedTimeStep class.
    /// </summary>
    public FixedTimeStep() : this(Double.MaxValue) {
    }

    /// <summary>
    /// The various sleep modes to block the calling
    /// thread in order to enforce the cycle time.
    /// </summary>
    public enum EPauseMode {

      /// <summary>
      /// Put the thread asleep.
      /// </summary>
      Sleep,

      /// <summary>
      /// Employ busy waiting.
      /// </summary>
      SpinWait,

      /// <summary>
      /// Choose between Sleep and SpinWait depending on the time interval to wait.
      /// </summary>
      Adaptive
    }

    /// <summary>
    /// Gets or sets the desired cycle time in (frames-per-second)
    /// </summary>
    public double FPS {
      get { 
        return _fps; 
      }

      set {
        if (value <= 0.0) {
          throw new ArgumentException("FPS must be greater than zero");
        }

        _fps = value;
        _cycle_time_ticks = (long)(((1.0 / _fps) * 1000000000) / _ns_per_tick);
      }
    }

    /// <summary>
    /// Gets or sets the sleep mode.
    /// </summary>
    public EPauseMode PauseMode {
      get { return _pause_mode; }
      set { _pause_mode = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is enabled or disabled.
    /// </summary>
    public bool Enabled {
      get { return _enabled; }
      set { _enabled = value; }
    }

    /// <summary>
    /// Fetch latest amount of time elapsed and wait to satisfy cycle time
    /// </summary>
    public void UpdateAndWait() {
      if (this.Enabled) {
        DoWait();
      }
    }

    /// <summary>
    /// Wait to enforce cylce time.
    /// </summary>
    private void DoWait() {
      if (_sw.IsRunning) {
        _sw.Stop();
        long elapsed_ticks = _sw.ElapsedTicks;

        long wait_time_ticks = _cycle_time_ticks - elapsed_ticks;
        long wait_time_ms = (long)((wait_time_ticks * _ns_per_tick) / 1000000);

        if (wait_time_ticks > 0) {
          switch (_pause_mode) {
            case EPauseMode.Sleep:
              SleepWait(wait_time_ms);
              break;
            case EPauseMode.SpinWait:
              SpinWait(wait_time_ticks);
              break;
            case EPauseMode.Adaptive:
              if (wait_time_ms > 50) {
                SleepWait(wait_time_ms);
              } else {
                SpinWait(wait_time_ticks);
              }

              break;
          }
        }

        _sw.Reset();
      }

      _sw.Start();
    }

    /// <summary>
    /// Put the thread a sleep in a resource friendly way.
    /// </summary>
    /// <param name="wait_time_ms">Number of minimum milliseconds to sleep</param>
    private void SleepWait(long wait_time_ms) {
      System.Threading.Thread.Sleep((int)wait_time_ms);
    }

    /// <summary>
    /// Put the thread a sleep by spinning
    /// </summary>
    /// <param name="wait_time_ticks">Number of iterations to spin</param>
    private void SpinWait(long wait_time_ticks) {
      _sw.Reset();
      _sw.Start();
      while (_sw.ElapsedTicks < wait_time_ticks) {
        System.Threading.Thread.SpinWait(1000);
      }

      _sw.Stop();
    }
  }
}
