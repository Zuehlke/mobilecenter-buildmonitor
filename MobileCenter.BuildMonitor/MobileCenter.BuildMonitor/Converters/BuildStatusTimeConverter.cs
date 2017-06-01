using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenter.BuildMonitor.Converters
{
    public class BuildStatusTimeConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime finishedDate)
            {
                if (finishedDate>DateTime.MinValue)
                {
                    return $"Finished: {finishedDate}";
                }
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
