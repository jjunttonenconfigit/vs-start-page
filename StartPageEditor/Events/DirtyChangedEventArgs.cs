namespace StartPageEditor.Events
{
  /// <summary>
  /// DirtyChangedEventArgs
  /// </summary>
  public class DirtyChangedEventArgs
  {
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="DirtyChangedEventArgs"/> is dirty.
    /// </summary>
    /// <value>
    ///   <c>true</c> if dirty; otherwise, <c>false</c>.
    /// </value>
    public bool Dirty { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="DirtyChangedEventArgs"/> class.
    /// </summary>
    /// <param name="dirty">if set to <c>true</c> [dirty].</param>
    public DirtyChangedEventArgs(bool dirty)
    {
      this.Dirty = dirty;
    }
  }
}
