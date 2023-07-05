namespace PulseSpamApi.Misc
{
    public class DateComparator
    {
        public static bool dateOnlyCompare(DateTime d1, DateTime d2)
        {
            bool res = false;

            if (d1.Year == d2.Year && d1.Month == d2.Month && d1.Day == d2.Day)
            {
                res = true;
            }

            return res;
        }

        public static bool dateToStringCompare(DateTime d1, string d2)
        {
            bool res = false;

            string d1Parsed = d1.Date.ToString();

            if (d1.Equals(d2))
            {
                res = true;
            } else
            {
                res = false;
            }

            return res;
        }
    }
}
