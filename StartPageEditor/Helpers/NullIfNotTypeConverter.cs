using System;
using System.Globalization;
using System.Windows.Data;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// ProjectModelValueConverter
  /// </summary>
  /// <seealso cref="System.Windows.Data.IValueConverter" />
  public class NullIfNotTypeConverter : IValueConverter
  {
    /// <summary>
    /// TODO: redo this
    /// this is a hacky way to circumvent the binding mismatch of the treeview selectedItem
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value != null)
      {
        if (value.GetType() == targetType)
        {
          return value;
        }
      }
      return null;
    }

    /// <summary>
    /// ConvertBack
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
