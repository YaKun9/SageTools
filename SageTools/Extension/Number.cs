using System;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        /// <summary>
        /// 阿拉伯数字转中文
        /// </summary>
        /// <param name="arabNum">阿拉伯数字 0~9 </param>
        /// <returns>对应的中文 例如：零</returns>
        public static string ArabToChinese(this int arabNum)
        {
            if (arabNum < 0 || arabNum > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(arabNum), $"取值应在0~9，当前为{arabNum}");
            }
            var chineseChars = new[] {"零", "一", "二", "三", "四", "五", "六", "七", "八", "九"};
            return chineseChars[arabNum];
        }

        /// <summary>
        /// 转为Int16(short)，失败时返回指定的值，默认0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">失败时返回的默认值</param>
        /// <returns>转换后的值，失败时返回指定的默认值</returns>
        public static short ToInt16(this object obj, short defaultValue = 0)
        {
            if (obj.IsNull()) return defaultValue;
            short.TryParse(obj.ToString(), out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转为Int32(int)，失败时返回指定的值，默认0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">失败时返回的默认值</param>
        /// <returns>转换后的值，失败时返回指定的默认值</returns>
        public static int ToInt32(this object obj, int defaultValue = 0)
        {
            if (obj.IsNull()) return defaultValue;
            int.TryParse(obj.ToString(), out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转为Int64(long)，失败时返回指定的值，默认0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">失败时返回的默认值</param>
        /// <returns>转换后的值，失败时返回指定的默认值</returns>
        public static long ToInt64(this object obj, long defaultValue = 0)
        {
            if (obj.IsNull()) return defaultValue;
            long.TryParse(obj.ToString(), out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转为Single(float)，失败时返回指定的值，默认0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">失败时返回的默认值</param>
        /// <returns>转换后的值，失败时返回指定的默认值</returns>
        public static float ToSingle(this object obj, float defaultValue = 0)
        {
            if (obj.IsNull()) return defaultValue;
            float.TryParse(obj.ToString(), out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转为Double(double)，失败时返回指定的值，默认0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">失败时返回的默认值</param>
        /// <returns>转换后的值，失败时返回指定的默认值</returns>
        public static double ToDouble(this object obj, double defaultValue = 0)
        {
            if (obj.IsNull()) return defaultValue;
            double.TryParse(obj.ToString(), out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转为Decimal(decimal)，失败时返回指定的值，默认0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">失败时返回的默认值</param>
        /// <returns>转换后的值，失败时返回指定的默认值</returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue = 0)
        {
            if (obj.IsNull()) return defaultValue;
            decimal.TryParse(obj.ToString(), out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// int转布尔，只有1=true，其他均为false
        /// </summary>
        public static bool ToBool(this int num)
        {
            return num == 1;
        }

        /// <summary>
        /// 计算分页页数
        /// </summary>
        /// <param name="totalCount">总条数</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static int ToPageCount(this int totalCount,int pageSize)
        {
            if(totalCount==0 || pageSize==0)return 0;
            if (pageSize == 1) return totalCount;
            return (int)Math.Ceiling(1D * totalCount / pageSize);
        }

        public static DateTime ToDateTime(this long timeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timeStamp);
        }
    }
}