using System;
using System.Globalization;

namespace SageTools.Extension
{
    public partial class Extension
    {
        /// <summary>
        /// 判断时间是否在指定范围内，不包含起止时间
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue">开始时间</param>
        /// <param name="maxValue">结束时间</param>
        /// <returns></returns>
        public static bool Between(this DateTime @this, DateTime minValue, DateTime maxValue) => minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;

        /// <summary>
        /// 获取对于指定时间节点已经过去了多久
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static TimeSpan Elapsed(this DateTime datetime) => DateTime.Now - datetime;

        /// <summary>
        /// 获取时间过去了多久 startTime-endTime
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static TimeSpan Elapsed(this DateTime startTime, DateTime endTime) => startTime - endTime;

        /// <summary>
        /// 获取指定天的结束时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime @this)
        {
            DateTime dateTime = new DateTime(@this.Year, @this.Month, @this.Day);
            dateTime = dateTime.AddDays(1.0);
            return dateTime.Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }

        /// <summary>
        /// 获取时间是否在指定范围内,包含起始
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue) => @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;

        /// <summary>
        /// 是否是下午
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsAfternoon(this DateTime @this) => @this.TimeOfDay >= new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;

        /// <summary>
        /// 是否是上午
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsMorning(this DateTime @this) => @this.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;

        /// <summary>
        /// 是否是当前时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNow(this DateTime @this) => @this == DateTime.Now;

        /// <summary>
        /// 获取一天的开始时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime @this) => new DateTime(@this.Year, @this.Month, @this.Day);

        public static string ToLongDateString(this DateTime @this) => @this.ToString("D", (IFormatProvider)DateTimeFormatInfo.CurrentInfo);

        public static string ToLongDateString(this DateTime @this, string culture) => @this.ToString("D", (IFormatProvider)new CultureInfo(culture));

        public static string ToLongDateString(this DateTime @this, CultureInfo culture) => @this.ToString("D", (IFormatProvider)culture);

        public static string ToLongDateTimeString(this DateTime @this) => @this.ToString("F", (IFormatProvider)DateTimeFormatInfo.CurrentInfo);

        public static string ToLongDateTimeString(this DateTime @this, string culture) => @this.ToString("F", (IFormatProvider)new CultureInfo(culture));

        public static string ToLongDateTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("F", (IFormatProvider)culture);

        /// <summary>
        /// 获取相对时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="includeTime"></param>
        /// <param name="asPlusMinus"></param>
        /// <param name="compareTo"></param>
        /// <param name="includeSign"></param>
        /// <returns></returns>
        public static string ToRelativeTime(this DateTime dt, bool includeTime = true, bool asPlusMinus = false, DateTime? compareTo = null, bool includeSign = true)
        {
            DateTime utcNow = compareTo ?? DateTime.Now;
            return asPlusMinus
                ? (dt <= utcNow ? ToRelativeTimeSimple(utcNow - dt, includeSign ? "-" : "") : ToRelativeTimeSimple(dt - utcNow, includeSign ? "+" : ""))
                : (dt <= utcNow ? ToRelativeTimePast(dt, utcNow, includeTime) : ToRelativeTimeFuture(dt, utcNow, includeTime));
        }

        private static string ToRelativeTimePast(DateTime dt, DateTime utcNow, bool includeTime = true)
        {
            var timeSpan = utcNow - dt;
            var totalSeconds = timeSpan.TotalSeconds;
            if (totalSeconds < 1.0)
                return "刚刚";
            if (totalSeconds < 60.0)
                return timeSpan.Seconds == 1 ? "1秒前" : timeSpan.Seconds.ToString() + "秒前";
            if (totalSeconds < 3600.0)
                return timeSpan.Minutes == 1 ? "1分钟前" : timeSpan.Minutes.ToString() + "分钟前";
            if (totalSeconds < 86400.0)
                return timeSpan.Hours == 1 ? "1小时前" : timeSpan.Hours.ToString() + "小时前";
            int days = timeSpan.Days;
            if (days == 1)
                return "昨天";
            if (days <= 2)
                return days + "天前";
            return utcNow.Year == dt.Year ? dt.ToString(includeTime ? "MM-dd HH:mm" : "MM-dd") : dt.ToString(includeTime ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd");
        }

        private static string ToRelativeTimeFuture(DateTime dt, DateTime utcNow, bool includeTime = true)
        {
            TimeSpan timeSpan = dt - utcNow;
            double totalSeconds = timeSpan.TotalSeconds;
            if (totalSeconds < 1.0)
                return "刚刚";
            if (totalSeconds < 60.0)
                return timeSpan.Seconds == 1 ? "1秒后" : timeSpan.Seconds.ToString() + "秒后";
            if (totalSeconds < 3600.0)
                return timeSpan.Minutes == 1 ? "1分钟后" : timeSpan.Minutes.ToString() + "分钟后";
            if (totalSeconds < 86400.0)
                return timeSpan.Hours == 1 ? "1小时后" : timeSpan.Hours.ToString() + "小时后";
            int num = (int)Math.Round(timeSpan.TotalDays, 0);
            if (num == 1)
                return "明天";
            if (num <= 10)
                return num.ToString() + "天后" + (num > 1 ? "s" : "");
            return utcNow.Year == dt.Year ? dt.ToString(includeTime ? "MM-dd HH:mm" : "MM-dd") : dt.ToString(includeTime ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd");
        }

        private static string ToRelativeTimeSimple(TimeSpan ts, string sign)
        {
            double totalSeconds = ts.TotalSeconds;
            if (totalSeconds < 1.0)
                return "1秒前";
            if (totalSeconds < 60.0)
                return sign + ts.Seconds.ToString() + "秒";
            if (totalSeconds < 3600.0)
                return sign + ts.Minutes.ToString() + "分钟";
            return totalSeconds < 86400.0 ? sign + ts.Hours.ToString() + "小时" : ts.Days.ToString() + "天" + (sign == "-" ? "前" : "后");
        }

        public static string ToRFC1123String(this DateTime @this) => @this.ToString("r", (IFormatProvider)DateTimeFormatInfo.CurrentInfo);

        public static string ToRFC1123String(this DateTime @this, string culture) => @this.ToString("r", (IFormatProvider)new CultureInfo(culture));

        public static string ToRFC1123String(this DateTime @this, CultureInfo culture) => @this.ToString("r", (IFormatProvider)culture);

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime @this)
        {
            return (long)(@this - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

    }
}