using StartPageEditor.Events;
using System;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// DirtyTracker
  /// TODO: poor man's Dirty tracker, no support for undo 
  /// </summary>
  public static class DirtyTracker
  {
    /// <summary>
    /// Occurs when [dirty changed].
    /// </summary>
    public static event EventHandler<DirtyChangedEventArgs> DirtyChanged;
    /// <summary>
    /// The is dirty
    /// </summary>
    private static bool _isDirty;
    /// <summary>
    /// The locker
    /// </summary>
    private static readonly object Locker = new object();
    /// <summary>
    /// Gets or sets a value indicating whether this instance is dirty.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is dirty; otherwise, <c>false</c>.
    /// </value>
    public static bool IsDirty
    {
      get => _isDirty;
      set
      {
        lock (Locker)
        {
          _isDirty = value;
          DirtyChanged?.Invoke(null, new DirtyChangedEventArgs(value));
        }
      }
    }
  }
}
