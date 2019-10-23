using System;
using System.Globalization;
using CodeCube.Mvc.Resources;

namespace CodeCube.Mvc.Extensions
{
    ///<summary>
    /// An extension method to convert a datetime value to a pretty, readable, date value
    ///</summary>
    public static class DatetimeExtensions
    {
        ///<summary>
        ///</summary>
        ///<param name="dateTime"></param>
        ///<param name="allowFutureDate"></param>
        ///<returns></returns>
        public static string ToPrettyDate(this DateTime dateTime, bool allowFutureDate = false)
        {
            //get the elapsed time since the date to convert
            //var timeStampFrom = new TimeSpan(dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond).TotalSeconds;
            //var timeStampTo = new TimeSpan(DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond).TotalSeconds;
            var timeSpanElapsed = DateTime.Now.Subtract(dateTime);
            var inFuture = false;

            //timedifference in seconds
            //var differenceInSeconds = timeStampTo - timeStampFrom;
            int differenceInSeconds = Convert.ToInt32(timeSpanElapsed.TotalSeconds);

            double differenceInHours = timeSpanElapsed.TotalHours;

            //get the difference in days
            double differenceInDays = Math.Round(timeSpanElapsed.TotalDays);

            if (differenceInSeconds < 0)
            {
                //differenceInSeconds = timeStampFrom - timeStampTo;
                inFuture = true;

                if (!allowFutureDate) return Resource.JustNow;
            }

            if (differenceInDays < 0) return String.Empty;

            //difference smaller then one day
            if (Convert.ToInt32(differenceInDays) == 0)
            {
                // smaller then one minute
                if (differenceInSeconds < 60) return Resource.JustNow;
                if (differenceInSeconds < 120)
                {
                    //is the value in the future?
                    return inFuture ? Resource.InAnMinute : Resource.AMinuteAgo;
                }
                if (differenceInSeconds < 3600)
                {
                    //if the value in the future?
                    return String.Format(inFuture ? Resource.InXMinutes : Resource.xMinutesAgo, Math.Floor((double)differenceInSeconds / 60));
                }
                if (differenceInSeconds < 7200)
                {
                    //is the value in the future
                    return inFuture ? Resource.InAnHour : Resource.OneHourAgo;
                }
                if (differenceInSeconds < 86400)
                {
                    //if the value in the future?
                    return String.Format(inFuture ? Resource.InXHours : Resource.xHoursAgo, Math.Floor((double)differenceInSeconds / 3600));
                }
            }
            else if (differenceInHours < 24)
            {
                return String.Format(inFuture ? Resource.InXHours : Resource.xHoursAgo, Math.Floor((double)differenceInSeconds / 3600));
            }
            else if (Convert.ToInt32(differenceInDays) == 1) //is the difference one day
            {
                //is the value in the future?
                return inFuture ? Resource.Tomorrow : Resource.Yesterday;
            }
            else if (differenceInDays < 7)// is the difference smaller then a week
            {
                //is the value in the future?
                return String.Format(inFuture ? Resource.InXDays : Resource.xDaysAgo, differenceInDays);
            }
            else if (Convert.ToInt32(differenceInDays) == 7) //is the difference a week?
            {
                //is the value in the future
                return inFuture ? Resource.InAWeek : Resource.OneWeekAgo;
            }
            else if (differenceInDays < (7 * 6)) //is the difference between a week and a month?
            {
                //is the value in the future?
                return String.Format(inFuture ? Resource.InXWeeks : Resource.xWeeksAgo, Math.Ceiling(differenceInDays / 7));
            }
            else if (differenceInDays < 365) //is the difference between a month an a year?
            {
                //is the value in the future?
                return String.Format(inFuture ? Resource.InXMonths : Resource.xMonthsAgo, Math.Ceiling(differenceInDays / (365 / 12)));
            }
            else // the difference is bigger then a year
            {
                return String.Format(Resource.xYearsAgo, Math.Round(differenceInDays / 365));
            }

            return dateTime.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns an string as readable (dutch) datetime format
        /// </summary>
        /// <example>22 december 2010</example>
        /// <param name="date">The date to convert</param>
        /// <returns>Readable datetime as string</returns>
        public static string AsReadableDate(this DateTime date)
        {
            var dt = $"{date:d MMMM yyyy}";
            return dt;
        }

        /// <summary>
        /// Returns an string as readable (dutch) datetime format
        /// </summary>
        /// <example>22 december 2010 15:49</example>
        /// <param name="date">The date to convert</param>
        /// <returns>Readable datetime as string</returns>
        public static string AsReadableDateTime(this DateTime date)
        {
            var dt = $"{date:d MMMM yyyy HH:mm}";
            return dt;
        }
    }
}
