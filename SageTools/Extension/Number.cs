using System;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        #region Int

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
            var chineseChars = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            return chineseChars[arabNum];
        }

        public static int ToInt(this object obj)
        {
            if (obj.IsNull()) throw new ArgumentNullException(nameof(obj),"null不可以转int，如需返回默认值，请使用重载方法");
            if (int.TryParse(obj.ToString(), out int value))
            {
                return value;
            }
            throw new ArgumentException("值无法转为Int", nameof(obj));
        }

        /// <summary>
        /// 转为Int，失败时返回指定的值，默认0
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">失败时返回的默认值</param>
        /// <returns>转换后的值，失败时返回指定的默认值</returns>
        public static int ToInt(this object obj,int defaultValue)
        {
            if (obj.IsNull()) return defaultValue;
            int.TryParse(obj.ToString(), out defaultValue);
            return defaultValue;
        }
        #endregion
    }
}
