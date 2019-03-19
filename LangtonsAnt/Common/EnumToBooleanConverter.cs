using System;
using System.Globalization;
using System.Windows.Data;

namespace LangtonsAnt.Common
{
    /// <summary>
    /// The EnumToBooleanConverter class represents an IValueConverter, which is used to convert between an enum and a bool.
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// The Convert method is called to convert an enum to a bool.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }

            var currentState = value.ToString();
            var stateStrings = parameter.ToString();
            var found = false;

            foreach (var state in stateStrings.Split(','))
            {
                found = (currentState == state.Trim());

                if (found)
                {
                    break;
                }
            }

            return found;
        }

        /// <summary>
        /// The ConvertBack method is called to convert a bool to an enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return null;
            }

            bool useValue = (bool)value;
            string targetValue = parameter.ToString();

            if (useValue)
            {
                return Enum.Parse(targetType, targetValue);
            }

            return null;
        }
    }
}
