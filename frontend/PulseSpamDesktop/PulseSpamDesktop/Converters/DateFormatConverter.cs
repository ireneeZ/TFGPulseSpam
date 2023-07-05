using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseSpamDesktop.Converters
{
    public class DateFormatConverter
    {
        public static string dateFormatConverter(DateTime fecha)
        {
            string fechaConverted;
            int month = int.Parse(fecha.Month.ToString());
            int day = int.Parse(fecha.Day.ToString());
            string monthConverted = month.ToString();
            string dayConverted = day.ToString();
            if (month < 10)
            {
                monthConverted = "0" + month;
            }
            if (day < 10)
            {
                dayConverted = "0" + day;
            }
            fechaConverted = fecha.Year + "-" + monthConverted + "-" + dayConverted;
            return fechaConverted;
        }
    }
}
