using System.Windows;

namespace StartPageControl.Helpers
{
  /// <summary>
  /// class taken from https://gist.github.com/jrgcubano/54ecbc61cbfaaa83b4dd14de03adf296 
  /// cheers to Jorge Rodríguez Galán
  /// </summary>
  public static class SizeObserver
  {
    /// <summary>
    /// The observe property
    /// </summary>
    public static readonly DependencyProperty ObserveProperty = DependencyProperty.RegisterAttached(
        "Observe",
        typeof(bool),
        typeof(SizeObserver),
        new FrameworkPropertyMetadata(OnObserveChanged));

    /// <summary>
    /// The observed width property
    /// </summary>
    public static readonly DependencyProperty ObservedWidthProperty = DependencyProperty.RegisterAttached(
        "ObservedWidth",
        typeof(double),
        typeof(SizeObserver));

    /// <summary>
    /// The observed height property
    /// </summary>
    public static readonly DependencyProperty ObservedHeightProperty = DependencyProperty.RegisterAttached(
        "ObservedHeight",
        typeof(double),
        typeof(SizeObserver));

    /// <summary>
    /// Gets the observe.
    /// </summary>
    /// <param name="frameworkElement">The framework element.</param>
    /// <returns></returns>
    public static bool GetObserve(FrameworkElement frameworkElement)
    {
      return (bool)frameworkElement.GetValue(ObserveProperty);
    }

    /// <summary>
    /// Sets the observe.
    /// </summary>
    /// <param name="frameworkElement">The framework element.</param>
    /// <param name="observe">if set to <c>true</c> [observe].</param>
    public static void SetObserve(FrameworkElement frameworkElement, bool observe)
    {
      frameworkElement.SetValue(ObserveProperty, observe);
    }

    /// <summary>
    /// Gets the width of the observed.
    /// </summary>
    /// <param name="frameworkElement">The framework element.</param>
    /// <returns></returns>
    public static double GetObservedWidth(FrameworkElement frameworkElement)
    {
      return (double)frameworkElement.GetValue(ObservedWidthProperty);
    }

    /// <summary>
    /// Sets the width of the observed.
    /// </summary>
    /// <param name="frameworkElement">The framework element.</param>
    /// <param name="observedWidth">Width of the observed.</param>
    public static void SetObservedWidth(FrameworkElement frameworkElement, double observedWidth)
    {
      frameworkElement.SetValue(ObservedWidthProperty, observedWidth);
    }

    /// <summary>
    /// Gets the height of the observed.
    /// </summary>
    /// <param name="frameworkElement">The framework element.</param>
    /// <returns></returns>
    public static double GetObservedHeight(FrameworkElement frameworkElement)
    {
      return (double)frameworkElement.GetValue(ObservedHeightProperty);
    }

    /// <summary>
    /// Sets the height of the observed.
    /// </summary>
    /// <param name="frameworkElement">The framework element.</param>
    /// <param name="observedHeight">Height of the observed.</param>
    public static void SetObservedHeight(FrameworkElement frameworkElement, double observedHeight)
    {
      frameworkElement.SetValue(ObservedHeightProperty, observedHeight);
    }

    /// <summary>
    /// Called when [observe changed].
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
    private static void OnObserveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
      var frameworkElement = (FrameworkElement)dependencyObject;

      if ((bool)e.NewValue)
      {
        frameworkElement.SizeChanged += OnFrameworkElementSizeChanged;
        UpdateObservedSizesForFrameworkElement(frameworkElement);
      }
      else
      {
        frameworkElement.SizeChanged -= OnFrameworkElementSizeChanged;
      }
    }

    /// <summary>
    /// Called when [framework element size changed].
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="SizeChangedEventArgs"/> instance containing the event data.</param>
    private static void OnFrameworkElementSizeChanged(object sender, SizeChangedEventArgs e)
    {
      UpdateObservedSizesForFrameworkElement((FrameworkElement)sender);
    }

    private static void UpdateObservedSizesForFrameworkElement(FrameworkElement frameworkElement)
    {
      // WPF 4.0 onwards
      frameworkElement.SetCurrentValue(ObservedWidthProperty, frameworkElement.ActualWidth);
      frameworkElement.SetCurrentValue(ObservedHeightProperty, frameworkElement.ActualHeight);

      // WPF 3.5 and prior
      ////SetObservedWidth(frameworkElement, frameworkElement.ActualWidth);
      ////SetObservedHeight(frameworkElement, frameworkElement.ActualHeight);
    }
  }
}
