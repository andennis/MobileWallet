using System;

namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime TruncateMiliseconds(this DateTime dt)
        {
            return dt.AddMilliseconds(-dt.Millisecond);
        }
    }
}
