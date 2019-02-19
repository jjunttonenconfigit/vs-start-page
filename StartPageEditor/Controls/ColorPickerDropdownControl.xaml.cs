using System.Windows;
using System.Windows.Controls;

namespace StartPageEditor.Controls
{
  /// <summary>
  /// Control refactored from: https://www.codeproject.com/Articles/140521/Color-Picker-using-WPF-Combobox
  /// Original author: sudheer muhammed
  /// 
  /// Interaction logic for ColorPickerDropdownControl.xaml
  /// </summary>
  public partial class ColorPickerDropdownControl : UserControl
  {
    /// <summary>
    /// The selected color property
    /// </summary>
    public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register("SelectedColor", typeof(string), typeof(ColorPickerDropdownControl),
          new FrameworkPropertyMetadata(null));

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorPickerDropdownControl"/> class.
    /// </summary>
    public ColorPickerDropdownControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Gets or sets the color of the selected.
    /// </summary>
    /// <value>
    /// The color of the selected.
    /// </value>
    public string SelectedColor
    {
      get => (string)GetValue(SelectedColorProperty);
      set => SetValue(SelectedColorProperty, value);
    }
  }
}
