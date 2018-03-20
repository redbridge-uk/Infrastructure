using System;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Redbridge.Forms
{
    public class DefaultIconMapper : IIconResourceMapper, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var stringValue = value.ToString();
                Icon enumResult;
                if (Enum.TryParse(stringValue, out enumResult))
                {
                    IconSize size = IconSize.Small;
                    if ( parameter != null )
                    {
                        var parameterString = parameter.ToString();
                        if ( Enum.TryParse(parameterString, out size) )
                        {
                            return MapResource(enumResult, Color.Black, size);
                        }
                    }

                    return MapResource(enumResult, Color.Black, IconSize.Medium);
                }
            }

            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string MapResource(Icon icon, Color color, IconSize iconSize = IconSize.Medium)
        {
            var builder = new StringBuilder();
            builder.Append("ic_");

            switch (icon)
            {
                case Icon.Add:
                    builder.Append("add");
                    break;
                case Icon.AddBox:
                    builder.Append("add_box");
                    break;
                case Icon.AddCircle:
                    builder.Append("add_circle");
                    break;
                case Icon.Alarm:
                    builder.Append("alarm_on");
                    break;
                case Icon.Account:
                    builder.Append("account_circle");
                    break;
                case Icon.Calendar:
                    builder.Append("perm_contact_calendar");
                    break;
                case Icon.Clock:
                    builder.Append("access_time");
                    break;
                case Icon.Email:
                    builder.Append("email");
                    break;
                case Icon.Refresh:
                    builder.Append("autorenew");
                    break;
                case Icon.International:
                    builder.Append("language");
                    break;
                case Icon.Flight:
                case Icon.Airport:
                    builder.Append("flight");
                    break;
                case Icon.Hospital:
                    builder.Append("local_hospital");
                    break;
                case Icon.Settings:
                    builder.Append("settings");
                    break;
                case Icon.School:
                    builder.Append("school");
                    break;
                case Icon.Restaurant:
                    builder.Append("local_dining");
                    break;
                case Icon.Hotel:
                    builder.Append("local_hotel");
                    break;
                case Icon.Phone:
                    builder.Append("phone");
                    break;
                case Icon.Train:
                    builder.Append("directions_subway");
                    break;
                case Icon.Bed:
                    builder.Append("local_hotel");
                    break;
                case Icon.Home:
                case Icon.House:
                    builder.Append("home");
                    break;
                case Icon.Broadband:
                case Icon.Wifi:
                    builder.Append("wifi");
                    break;

                case Icon.Taxi:
                case Icon.Police:
                    builder.Append("local_taxi");
                    break;
                case Icon.Money:
                case Icon.Currency:
                    builder.Append("attach_money");
                    break;

                default:
                    builder.Append("home");
                    break;
            }

            if (color == Color.White)
            {
                builder.Append("_white");
            }

            if (iconSize == IconSize.Medium)
                builder.Append("_2x");

            if (iconSize == IconSize.Large)
                builder.Append("_3x");

            builder.Append(".png");
            return builder.ToString();
        }
    }
}
