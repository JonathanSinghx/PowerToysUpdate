﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Microsoft.PowerToys.Run.Plugin.TimeDate.UnitTests")]

namespace Microsoft.PowerToys.Run.Plugin.TimeDate.Components
{
    internal static class TimeAndDateHelper
    {
        /// <summary>
        /// Get the format for the time string
        /// </summary>
        /// <param name="targetFormat">Type of format</param>
        /// <param name="timeLong">Show date with weekday and name of month (long format)</param>
        /// <param name="dateLong">Show time with seconds (long format)</param>
        /// <returns>String that identifies the time/date format (<see href="https://learn.microsoft.com/dotnet/api/system.datetime.tostring"/>)</returns>
        internal static string GetStringFormat(FormatStringType targetFormat, bool timeLong, bool dateLong)
        {
            switch (targetFormat)
            {
                case FormatStringType.Time:
                    return timeLong ? "T" : "t";
                case FormatStringType.Date:
                    return dateLong ? "D" : "d";
                case FormatStringType.DateTime:
                    if (timeLong & dateLong)
                    {
                        return "F"; // Friday, October 31, 2008 5:04:32 PM
                    }
                    else if (timeLong & !dateLong)
                    {
                        return "G"; // 10/31/2008 5:04:32 PM
                    }
                    else if (!timeLong & dateLong)
                    {
                        return "f"; // Friday, October 31, 2008 5:04 PM
                    }
                    else
                    {
                        // (!timeLong & !dateLong)
                        return "g"; // 10/31/2008 5:04 PM
                    }

                default:
                    return string.Empty; // Windows default based on current culture settings
            }
        }

        /// <summary>
        /// Returns the number week in the month (Used code from 'David Morton' from <see href="https://social.msdn.microsoft.com/Forums/vstudio/bf504bba-85cb-492d-a8f7-4ccabdf882cb/get-week-number-for-month"/>)
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>Number of week in the month</returns>
        internal static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            {
                date = date.AddDays(1);
            }

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

        /// <summary>
        /// Returns the number of the day in the week
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Number of the day in the week</returns>
        internal static int GetNumberOfDayInWeek(DateTime date)
        {
            int daysInWeek = 7;
            int adjustment = 1; // We count from 1 to 7 and not from 0 to 6
            int formatSettingFirstDayOfWeek = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            return ((int)(date.DayOfWeek + daysInWeek - formatSettingFirstDayOfWeek) % daysInWeek) + adjustment;
        }

        /// <summary>
        /// Convert input string to a <see cref="DateTime"/> object in local time
        /// </summary>
        /// <param name="input">String with date/time</param>
        /// <param name="timestamp">The new <see cref="DateTime"/> object</param>
        /// <returns>True on success, otherwise false</returns>
        internal static bool ParseStringAsDateTime(in string input, out DateTime timestamp)
        {
            if (DateTime.TryParse(input, out timestamp))
            {
                // Known date/time format
                return true;
            }
            else if (Regex.IsMatch(input, @"^u\d+") && input.Length <= 12 && long.TryParse(input.TrimStart('u'), out long secondsInt))
            {
                // unix time stamp
                // we use long instead of int because int ist to small after 03:14:07 UTC 2038-01-19
                timestamp = new DateTime(1970, 1, 1).AddSeconds(secondsInt).ToLocalTime();
                return true;
            }
            else if (Regex.IsMatch(input, @"^ft\d+") && long.TryParse(input.TrimStart("ft".ToCharArray()), out long secondsLong))
            {
                // windows file time
                timestamp = new DateTime(secondsLong);
                return true;
            }
            else
            {
                timestamp = new DateTime(1, 1, 1, 1, 1, 1);
                return false;
            }
        }
    }

    /// <summary>
    /// Type of time/date format
    /// </summary>
    internal enum FormatStringType
    {
        Time,
        Date,
        DateTime,
    }
}
