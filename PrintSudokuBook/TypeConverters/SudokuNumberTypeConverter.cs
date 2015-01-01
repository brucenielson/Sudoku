using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SudokuGame.TypeConverters
{
    public class SudokuNumberTypeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int v = (int)value;
            if (v == 0)
                return "";
            else
                return v.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string v = (string)value;
            if (v == "")
                return 0;
            else
            {
                int result = 0;
                try
                {
                    result = System.Convert.ToInt32(v);
                    if (result > 9 || result < 1)
                        result = 0;
                }
                catch
                {
                    result = 0;
                }
                return result;
            }
        }
    }
}
