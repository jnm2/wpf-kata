using System.Windows;
using System.Windows.Controls;

namespace WpfKata
{
    public static class AttachedProperties
    {
        public static readonly DependencyProperty SelectAllOnFocusProperty = DependencyProperty.RegisterAttached(
            "SelectAllOnFocus",
            typeof(bool),
            typeof(AttachedProperties),
            new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits, OnSelectAllOnFocusChanged));

        private static void OnSelectAllOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBox textBox) return;

            if (e.NewValue is true)
            {
                textBox.GotFocus += TextBox_GotKeyboardFocus;
                textBox.PreviewMouseDown += TextBox_PreviewMouseDown;
            }
            else
            {
                textBox.GotFocus -= TextBox_GotKeyboardFocus;
                textBox.PreviewMouseDown -= TextBox_PreviewMouseDown;
            }
        }

        private static void TextBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((TextBox)sender is { IsFocused: false } textBox)
            {
                textBox.Focus();
                e.Handled = true;
            }
        }

        private static void TextBox_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        /// <summary>Helper for getting <see cref="SelectAllOnFocusProperty"/> from <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="DependencyObject"/> to read <see cref="SelectAllOnFocusProperty"/> from.</param>
        /// <returns><c>SelectAllOnFocus</c> property value.</returns>
        [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
        public static bool GetSelectAllOnFocus(DependencyObject element)
        {
            return (bool)element.GetValue(SelectAllOnFocusProperty);
        }

        /// <summary>Helper for setting <see cref="SelectAllOnFocusProperty"/> on <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="DependencyObject"/> to set <see cref="SelectAllOnFocusProperty"/> on.</param>
        /// <param name="value"><c>SelectAllOnFocus</c> property value.</param>
        public static void SetSelectAllOnFocus(DependencyObject element, bool value)
        {
            element.SetValue(SelectAllOnFocusProperty, value);
        }
    }
}
