using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PacChat.Resources.CustomControls.ColourPicker
{
    public class BrushToRGBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var brush = (SolidColorBrush)value;
            return brush.Color.R;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
