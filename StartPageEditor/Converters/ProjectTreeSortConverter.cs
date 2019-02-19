using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace StartPageEditor.Converters
{
  /// <summary>
  /// ProjectTreeSortConverter -
  /// BUG CHECK: very slow when the amount of items in a node get large and you move an item up or down
  /// </summary>
  /// <seealso cref="System.Windows.Data.IValueConverter" />
  public class ProjectTreeSortConverter : IValueConverter
  {
    /// <summary>
    /// Sort the treeview items
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
      System.Collections.IList collection = value as System.Collections.IList;
      ListCollectionView view = new ListCollectionView(collection);
      SortDescription sort = new SortDescription(parameter.ToString(), ListSortDirection.Ascending);
      view.SortDescriptions.Add(sort);

      return view;
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
