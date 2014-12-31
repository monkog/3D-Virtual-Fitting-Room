using System;
using System.Windows.Data;

namespace KinectFittingRoom.Converters
{
    /// <summary>
    /// Converts the value adding the value of the parameter
    /// </summary>
    class IncreasedValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double number;
            double.TryParse((string)parameter, out number);

            return (double.Parse(value.ToString()) + number);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
