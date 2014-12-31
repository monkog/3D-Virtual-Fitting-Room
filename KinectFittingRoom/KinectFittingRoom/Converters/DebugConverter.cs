using System.Diagnostics;
using System.Windows.Data;

namespace KinectFittingRoom.Converters
{
    /// <summary>
    /// Debug converter
    /// </summary>
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }
}
