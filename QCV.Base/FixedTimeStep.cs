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

    private Stopwatch _sw;
    private long _ns_per_tick;
    private double _fps;
    private long _cycle_time_ticks;
    private EPauseMode _pause_mode;
    private bool _enabled;

    /// <summary>
    /// Construct new fixed time-step helper
    /// </summary>
    public FixedTimeStep(double fps) {
      _sw = new Stopwatch();
      _ns_per_tick = 1000000000 / Stopwatch.Frequency;
      this.FPS = fps;
      this.PauseMode = EPauseMode.Adaptive;
      _enabled = true;
    }

    /// <summary>
    /// Construct new fixed time-step helper
    /// </summary>
    public FixedTimeStep() : this(Double.MaxValue) {
    }

    public enum EPauseMode {
      Sleep,
      SpinWait,
      Adaptive
    }

    /// <summary>
    /// Control the desired frame-rate (frames per second)
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

    public EPauseMode PauseMode {
      get { return _pause_mode; }
      set { _pause_mode = value; }
    }

    public bool Enabled {
      get { return _enabled; }
      set { _enabled = value; }
    }

    /// <summary>
    /// Fetch latest amount of time elapsed and wait
    /// to satisfy cycle time
    /// </summary>
    public void UpdateAndWait() {
      if (this.Enabled) {
        PerformWait();
      }
    }

    private void PerformWait() {
      if (_sw.IsRunning) {
        _sw.Stop();
        long elapsed_ticks = _sw.ElapsedTicks;

        long wait_time_ticks = _cycle_time_ticks - elapsed_ticks;
        long wait_time_ms = (wait_time_ticks * _ns_per_tick) / 1000000;
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

    private void SleepWait(long wait_time_ms) {
      System.Threading.Thread.Sleep((int)wait_time_ms);
    }

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
