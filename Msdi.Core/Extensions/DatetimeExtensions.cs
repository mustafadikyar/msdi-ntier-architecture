using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msdi.Core.Extensions
{
    /// <summary>
    /// Extension methods for Datetime
    /// </summary>
    public static class DatetimeExtensions
    {

        /// <summary>
        /// Returns the instance of Datetime representing the start of minute
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime StartOfMinute(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        /// <summary>
        /// Returns the instance of Datetime representing the end of minute
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime EndOfMinute(this DateTime dateTime)
        {
            return dateTime.StartOfMinute().AddMinutes(1).AddSeconds(-1);
        }

        /// <summary>
        /// Returns the instance of Datetime representing the start of hour
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime StartOfHour(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0);
        }

        /// <summary>
        /// Returns the instance of Datetime representing the end of hour
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime EndOfHour(this DateTime dateTime)
        {
            return dateTime.StartOfHour().AddHours(1).AddSeconds(-1);
        }

        /// <summary>
        /// Returns the instance of Datetime representing the start of day
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        /// <summary>
        /// Returns the instance of Datetime representing the end of day
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return StartOfDay(dateTime).AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// Start of week
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <param name="startingDayOfWeek">Starting day of week. Default is Monday</param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek? startingDayOfWeek = DayOfWeek.Monday)
        {
            var startDay = startingDayOfWeek ?? CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            var offset = dateTime.DayOfWeek - startDay;

            return dateTime.AddDays(-1 * offset).StartOfDay();
        }

        /// <summary>
        /// End of week
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <param name="startingDayOfWeek">Starting day of week. Default Monday</param>
        /// <returns></returns>
        public static DateTime EndOfWeek(this DateTime dateTime, DayOfWeek? startingDayOfWeek = DayOfWeek.Monday)
        {
            return dateTime.StartOfWeek(startingDayOfWeek).AddDays(7).AddSeconds(-1);
        }


        /// <summary>
        /// Returns the instance of Datetime representing the start of month
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
        }

        /// <summary>
        /// Returns the instance of Datetime representing the end of day
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime dateTime)
        {
            return StartOfMonth(dateTime).AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// Returns the instance of Datetime representing the start of year
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime StartOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Returns the instance of Datetime representing the end of year
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime dateTime)
        {
            return dateTime.StartOfYear().AddYears(1).AddSeconds(-1);
        }

        /// <summary>
        /// Returns True if the provided DateTime is in the future, otherwise False
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static bool IsInFuture(this DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }

        /// <summary>
        /// Returns True if the provided DateTime is Saturday or Sunday, otherwise False
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Returns true of the provided DateTime is between startTime and endTime, 
        /// inclusive specifies if the startTime and the endTime needs to be taken into consideration or not
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="inclusive">Specifies if the Start time and End time needs to be taken into consideration or not</param>
        /// <returns></returns>
        public static bool InBetween(this DateTime dateTime, DateTime startTime, DateTime endTime, bool inclusive = false)
        {
            return inclusive
                   ? dateTime >= startTime && dateTime <= endTime
                   : dateTime > startTime && dateTime > endTime;
        }

        /// <summary>
        /// Returns true if the provided instance of Datetime is within today, otherwise False
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static bool IsToday(this DateTime dateTime)
        {
            var now = DateTime.Now;

            return InBetween(dateTime, now.StartOfDay(), now.EndOfDay(), true);
        }

        /// <summary>
        /// Returns true if the provided instance of Datetime is within the current month, otherwise false
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static bool IsCurrentMonth(this DateTime dateTime)
        {
            var now = DateTime.Now;

            return InBetween(dateTime, now.StartOfMonth(), now.EndOfMonth(), true);
        }

        /// <summary>
        /// Returns true if the provided instance of Datetime is within the current year, otherwise false
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <returns></returns>
        public static bool IsCurrentYear(this DateTime dateTime)
        {
            var now = DateTime.Now;

            return InBetween(dateTime, now.StartOfYear(), now.EndOfYear(), true);
        }

        /// <summary>
        /// Returns the Calender Week of the provided instance of datetime
        /// </summary>
        /// <param name="dateTime">Instance of Datetime</param>
        /// <param name="startingDay">Starting day of week (default is Monday, German standard)</param>
        /// <returns></returns>
        public static int GetCalenderWeek(this DateTime dateTime, DayOfWeek startingDay = DayOfWeek.Monday)
        {
            int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, startingDay);

            return weekNumber;
        }

    }
}
