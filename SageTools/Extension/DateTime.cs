using System;
using System.Globalization;

namespace SageTools.Extension
{
    public partial class Extension
    {
        /// <summary>
        ///     判断时间是否在指定范围内，不包含起止时间
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue">开始时间</param>
        /// <param name="maxValue">结束时间</param>
        /// <returns></returns>
        public static bool Between(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        /// <summary>
        ///     获取对于指定时间节点已经过去了多久
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }

        /// <summary>
        ///     获取时间过去了多久 startTime-endTime
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static TimeSpan Elapsed(this DateTime startTime, DateTime endTime)
        {
            return startTime - endTime;
        }

        /// <summary>
        ///     获取指定天的结束时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime @this)
        {
            var dateTime = new DateTime(@this.Year, @this.Month, @this.Day);
            dateTime = dateTime.AddDays(1.0);
            return dateTime.Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }

        /// <summary>
        ///     获取时间是否在指定范围内,包含起始
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        /// <summary>
        ///     是否是下午
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsAfternoon(this DateTime @this)
        {
            return @this.TimeOfDay >= new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
        }

        /// <summary>
        ///     是否是上午
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsMorning(this DateTime @this)
        {
            return @this.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
        }

        /// <summary>
        ///     是否是当前时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNow(this DateTime @this)
        {
            return @this == DateTime.Now;
        }

        /// <summary>
        ///     获取一天的开始时间
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day);
        }

        /// <summary>
        ///     将 DateTime 转换为长日期字符串，使用当前区域信息格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <returns>长日期字符串。</returns>
        public static string ToLongDateString(this DateTime @this)
        {
            return @this.ToString("D", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     将 DateTime 转换为长日期字符串，使用指定文化名称的区域信息格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <param name="culture">指定的文化名称。</param>
        /// <returns>长日期字符串。</returns>
        public static string ToLongDateString(this DateTime @this, string culture)
        {
            return @this.ToString("D", new CultureInfo(culture));
        }

        /// <summary>
        ///     将 DateTime 转换为长日期字符串，使用指定的 CultureInfo 格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <param name="culture">指定的 CultureInfo 对象。</param>
        /// <returns>长日期字符串。</returns>
        public static string ToLongDateString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("D", culture);
        }

        /// <summary>
        ///     将 DateTime 转换为长日期时间字符串，使用当前区域信息格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <returns>长日期时间字符串。</returns>
        public static string ToLongDateTimeString(this DateTime @this)
        {
            return @this.ToString("F", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     将 DateTime 转换为长日期时间字符串，使用指定文化名称的区域信息格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <param name="culture">指定的文化名称。</param>
        /// <returns>长日期时间字符串。</returns>
        public static string ToLongDateTimeString(this DateTime @this, string culture)
        {
            return @this.ToString("F", new CultureInfo(culture));
        }

        /// <summary>
        ///     将 DateTime 转换为长日期时间字符串，使用指定的 CultureInfo 格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <param name="culture">指定的 CultureInfo 对象。</param>
        /// <returns>长日期时间字符串。</returns>
        public static string ToLongDateTimeString(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("F", culture);
        }
        
        /// <summary>
        ///     获取相对时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="includeTime"></param>
        /// <param name="asPlusMinus"></param>
        /// <param name="compareTo"></param>
        /// <param name="includeSign"></param>
        /// <returns></returns>
        public static string ToRelativeTime(this DateTime dt, bool includeTime = true, bool asPlusMinus = false, DateTime? compareTo = null, bool includeSign = true)
        {
            var utcNow = compareTo ?? DateTime.Now;
            return asPlusMinus
                ? dt <= utcNow ? ToRelativeTimeSimple(utcNow - dt, includeSign ? "-" : "") : ToRelativeTimeSimple(dt - utcNow, includeSign ? "+" : "")
                : dt <= utcNow
                    ? ToRelativeTimePast(dt, utcNow, includeTime)
                    : ToRelativeTimeFuture(dt, utcNow, includeTime);
        }

        /// <summary>
        ///     将过去的时间转换为相对时间字符串。
        /// </summary>
        /// <param name="dt">要转换的 DateTime 对象。</param>
        /// <param name="utcNow">当前的 UTC 时间。</param>
        /// <param name="includeTime">是否包含时间信息。</param>
        /// <returns>相对时间字符串。</returns>
        private static string ToRelativeTimePast(DateTime dt, DateTime utcNow, bool includeTime = true)
        {
            var timeSpan = utcNow - dt;
            var totalSeconds = timeSpan.TotalSeconds;
            if (totalSeconds < 1.0)
                return "刚刚";
            if (totalSeconds < 60.0)
                return timeSpan.Seconds == 1 ? "1秒前" : timeSpan.Seconds + "秒前";
            if (totalSeconds < 3600.0)
                return timeSpan.Minutes == 1 ? "1分钟前" : timeSpan.Minutes + "分钟前";
            if (totalSeconds < 86400.0)
                return timeSpan.Hours == 1 ? "1小时前" : timeSpan.Hours + "小时前";
            var days = timeSpan.Days;
            if (days == 1)
                return "昨天";
            if (days <= 2)
                return days + "天前";
            return utcNow.Year == dt.Year ? dt.ToString(includeTime ? "MM-dd HH:mm" : "MM-dd") : dt.ToString(includeTime ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd");
        }

        /// <summary>
        ///     将未来的时间转换为相对时间字符串。
        /// </summary>
        /// <param name="dt">要转换的 DateTime 对象。</param>
        /// <param name="utcNow">当前的 UTC 时间。</param>
        /// <param name="includeTime">是否包含时间信息。</param>
        /// <returns>相对时间字符串。</returns>
        private static string ToRelativeTimeFuture(DateTime dt, DateTime utcNow, bool includeTime = true)
        {
            var timeSpan = dt - utcNow;
            var totalSeconds = timeSpan.TotalSeconds;
            if (totalSeconds < 1.0)
                return "刚刚";
            if (totalSeconds < 60.0)
                return timeSpan.Seconds == 1 ? "1秒后" : timeSpan.Seconds + "秒后";
            if (totalSeconds < 3600.0)
                return timeSpan.Minutes == 1 ? "1分钟后" : timeSpan.Minutes + "分钟后";
            if (totalSeconds < 86400.0)
                return timeSpan.Hours == 1 ? "1小时后" : timeSpan.Hours + "小时后";
            var num = (int)Math.Round(timeSpan.TotalDays, 0);
            if (num == 1)
                return "明天";
            if (num <= 10)
                return num + "天后" + (num > 1 ? "s" : "");
            return utcNow.Year == dt.Year ? dt.ToString(includeTime ? "MM-dd HH:mm" : "MM-dd") : dt.ToString(includeTime ? "yyyy-MM-dd HH:mm" : "yyyy-MM-dd");
        }

        /// <summary>
        ///     将时间跨度转换为简单的相对时间字符串。
        /// </summary>
        /// <param name="ts">要转换的 TimeSpan 对象。</param>
        /// <param name="sign">表示时间方向的符号，"-" 表示过去，"+" 表示未来。</param>
        /// <returns>相对时间字符串。</returns>
        private static string ToRelativeTimeSimple(TimeSpan ts, string sign)
        {
            var totalSeconds = ts.TotalSeconds;
            if (totalSeconds < 1.0)
                return "1秒前";
            if (totalSeconds < 60.0)
                return sign + ts.Seconds + "秒";
            if (totalSeconds < 3600.0)
                return sign + ts.Minutes + "分钟";
            return totalSeconds < 86400.0 ? sign + ts.Hours + "小时" : ts.Days + "天" + (sign == "-" ? "前" : "后");
        }

        /// <summary>
        ///     将 DateTime 转换为 RFC1123 格式的字符串，使用当前区域信息格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <returns>RFC1123 格式的字符串。</returns>
        public static string ToRFC1123String(this DateTime @this)
        {
            return @this.ToString("r", DateTimeFormatInfo.CurrentInfo);
        }

        /// <summary>
        ///     将 DateTime 转换为 RFC1123 格式的字符串，使用指定文化名称的区域信息格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <param name="culture">指定的文化名称。</param>
        /// <returns>RFC1123 格式的字符串。</returns>
        public static string ToRFC1123String(this DateTime @this, string culture)
        {
            return @this.ToString("r", new CultureInfo(culture));
        }

        /// <summary>
        ///     将 DateTime 转换为 RFC1123 格式的字符串，使用指定的 CultureInfo 格式化。
        /// </summary>
        /// <param name="this">要转换的 DateTime 对象。</param>
        /// <param name="culture">指定的 CultureInfo 对象。</param>
        /// <returns>RFC1123 格式的字符串。</returns>
        public static string ToRFC1123String(this DateTime @this, CultureInfo culture)
        {
            return @this.ToString("r", culture);
        }
        
        /// <summary>
        ///     获取时间戳(秒)
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime @this)
        {
            return (long)(@this - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        /// <summary>
        ///     时间戳转时间(秒)
        /// </summary>
        /// <param name="timeStamp">时间戳(秒)</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timeStamp);
        }

        /// <summary>
        ///     转换为日期格式
        /// </summary>
        public static DateTime ToDateTime(this string @this)
        {
            return Convert.ToDateTime(@this);
        }
    }
}