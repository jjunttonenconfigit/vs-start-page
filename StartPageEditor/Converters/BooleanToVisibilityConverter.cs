﻿using System;
using System.Windows;
using System.Windows.Data;

namespace StartPageEditor.Converters
{
  [ValueConversion(typeof(bool), typeof(Visibility))]
  internal class BooleanToVisibilityConverter : IValueConverter
  {
    public const string Invert = "Invert";

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    /// <exception cref="InvalidOperationException">The target must be a Visibility.</exception>
    public object Convert(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
    {
      if (targetType != typeof(Visibility))
      {
        throw new InvalidOperationException("The target must be a Visibility.");
      }
      bool? bValue = (bool?)value;

      if (parameter != null && parameter as string == Invert)
      {
        bValue = !bValue;
      }

      return bValue.HasValue && bValue.Value ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    /// <exception cref="NotSupportedException"></exception>
    public object ConvertBack(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
    {
      throw new NotSupportedException();
    }
  }
}
