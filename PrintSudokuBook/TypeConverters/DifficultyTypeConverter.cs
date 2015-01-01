using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SudokuGame.TypeConverters
{
    public class DifficultyTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double v = (double)value;

            if (v > 0 && v <= 0.33)
                return "Trivial";
            else if (v > 0.33 && v <= 0.66)
                return "Easy";
            else // if (v > 0.66)
                return "Medium";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
