using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hangman2.Converters
{
    abstract class Converter : IValueConverter
    {
        public abstract object Convert(object v, Type t, object p, CultureInfo c);

        public virtual object ConvertBack(object v, Type t, object p, CultureInfo c) =>
            throw new NotSupportedException("Зворотне перетворення не підтримується");
    }
}
