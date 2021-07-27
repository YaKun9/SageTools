using System;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        /// <summary>
        /// 从字符串中截取指定长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">指定的长度</param>
        /// <param name="returnAllOrThrowWhenIndexOutOfRange">当指定的长度超出或小于0时,True返回整体字符串(默认),False抛出IndexOutOfRange异常</param>
        /// <returns></returns>
        public static string SubSpecifiedLengthStr(this string str, int length, bool returnAllOrThrowWhenIndexOutOfRange = true)
        {

            if (str.IsNullOrEmpty())
            {
                return str;
            }
            if (length == 0)
            {
                return string.Empty;
            }

            if (length < 0 || length > str.Length)
            {
                return returnAllOrThrowWhenIndexOutOfRange ? str : throw new IndexOutOfRangeException();
            }
            return str.Substring(length);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }

        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return !str.IsNullOrWhiteSpace();
        }
    }
}
