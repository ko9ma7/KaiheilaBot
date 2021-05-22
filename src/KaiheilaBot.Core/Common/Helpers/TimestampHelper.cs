using System;

namespace KaiheilaBot.Core.Common.Helpers
{
    public static class TimestampHelper
    {
        public static long GetTimestamp(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, minute, second, millisecond, TimeSpan.Zero);
            return GetTimestamp(dateTimeOffset);
        }

        public static long GetTimestamp(DateTimeOffset dateTimeOffset)
        {
            var unixDateTime = dateTimeOffset.ToUnixTimeMilliseconds();
            return unixDateTime;
        }

        public static long GetTimestamp(DateTime dateTime)
        {
            var dateTimeOffset = new DateTimeOffset(dateTime);
            return GetTimestamp(dateTimeOffset);
        }

        public static DateTimeOffset GetDateTimeOffset(long timestamp)
        {
            var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            return dateTimeOffset;
        }

        public static DateTime GetDateTime(long timestamp)
        {
            var dateTime = GetDateTimeOffset(timestamp).LocalDateTime;
            return dateTime;
        }
    }
}
