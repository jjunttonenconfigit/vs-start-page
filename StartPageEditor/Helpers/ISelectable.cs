namespace StartPageEditor.Helpers
{
  /// <summary>
  /// ISelectable, to support multiple treeview item selection (not currently supported)
  /// </summary>
  public interface ISelectable
  {
    /// <summary>
    /// Gets or sets a value indicating whether this instance is selected.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
    /// </value>
    bool IsSelected {get;set;}
  }
}
