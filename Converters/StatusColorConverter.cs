using System;
using System.Globalization;

namespace MyToDoMauiApp.Converters
{
	public class StatusColorConverter : IValueConverter
	{
		public StatusColorConverter()
		{
		}

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (Color)Application.Current.Resources[(bool)value ? "CompletedColor" : "ActiveColor"];
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

