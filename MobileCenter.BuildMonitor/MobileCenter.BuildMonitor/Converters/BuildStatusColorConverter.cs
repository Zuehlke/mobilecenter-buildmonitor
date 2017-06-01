using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenter.BuildMonitor.Converters
{
    public class BuildStatusColorConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = value as string;

            if(!string.IsNullOrEmpty(status))
            {
                switch (status)
                {
                    case "succeeded": return Xamarin.Forms.Color.FromHex("6fa22e");
                    case "failed": return Xamarin.Forms.Color.FromHex("e2553d");
                    default: return Xamarin.Forms.Color.FromHex("3192b3");
                }
            }

            return Xamarin.Forms.Color.DeepSkyBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
