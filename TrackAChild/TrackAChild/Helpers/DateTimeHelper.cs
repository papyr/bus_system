using System;
using System.Collections.Generic;

namespace TrackAChild.Helpers
{
    public static class DateTimeHelper
    {
        const int numberOfYears = 100;
        public static IEnumerable<DateTime> AllDatesInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new DateTime(year, month, day);
            }
        }

        public static IEnumerable<DateTime> GetEveryXFromYear(int year)
        {
            List<DateTime> rangeOfXDays = new List<DateTime>();
            for (int index = 0; index < numberOfYears; ++index)
            {
                for (int monthCount = 1; monthCount < 13; ++monthCount) // total months + 1
                {
                    rangeOfXDays.AddRange(AllDatesInMonth(year + index, monthCount));
                }
            }

            return rangeOfXDays;
        }
    }
}
