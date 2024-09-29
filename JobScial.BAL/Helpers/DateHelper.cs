using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMOS.BAL.Helpers
{
    public static class DateHelper
    {
        public static bool IsValidBirthday(DateTime date)
        {
            DateTime currentDate = DateTime.Now.Date;
            if (currentDate.Year - date.Year < 13)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidBorder(DateTime date)
        {
            DateTime currentDate = DateTime.Today;
            DateTime minDate = new DateTime(1920, 1, 1);
            return date >= minDate && date <= currentDate;
        }

        #region Valid birthday for staff
        public static string ValidationBirthDay(DateTime birthDay)
        {
            DateTime now = DateTime.Now;
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan age = (TimeSpan)(now - birthDay);

            if (age.TotalDays < 18 * 365.25)
            {
                return "Age must be from 18 to older.";

            }
            else if (birthDay < start)
            {
                return "Birthday must be from 1970/01/01.";
            }

            return "";
        }
        #endregion

        #region Convert Unix Time ToDateTime
        public static DateTime ConvertUnixTimeToDateTime(long utcExpiredDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval = dateTimeInterval.AddSeconds(utcExpiredDate).ToUniversalTime();
            return dateTimeInterval;
        }
        #endregion
    }
}
