using StartPageControl.Models;
using System.Threading.Tasks;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// IStartPageGenerator
  /// </summary>
  public interface IStartPageGenerator
  {
    /// <summary>
    /// Cancels this instance.
    /// </summary>
    void Cancel();
    /// <summary>
    /// Gets a value indicating whether this <see cref="IStartPageGenerator"/> is cancelled.
    /// </summary>
    /// <value>
    ///   <c>true</c> if cancelled; otherwise, <c>false</c>.
    /// </value>
    bool Cancelled { get; }
    /// <summary>
    /// Generates the asynchronous.
    /// </summary>
    /// <param name="folders">The folders.</param>
    /// <returns></returns>
    Task<StartPageModel> GenerateAsync(string[] folders);

  }
}
