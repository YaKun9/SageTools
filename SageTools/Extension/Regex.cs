using System;
using System.Text.RegularExpressions;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        /// <summary>
        /// Regex.IsMatch()拓展
        /// </summary>
        public static bool IsMatch(this string input, string pattern) => Regex.IsMatch(input, pattern);

        /// <summary>
        /// Regex.IsMatch()拓展
        /// </summary>
        public static bool IsMatch(this string input, string pattern, RegexOptions options) => Regex.IsMatch(input, pattern, options);

        /// <summary>
        /// 正则判断是否为数字
        /// </summary>
        public static bool IsNumeric(this string @this) => !Regex.IsMatch(@this, "[^0-9]");

        /// <summary>
        /// 是否是正确的base64字符串
        /// </summary>
        public static bool IsValidBase64String(this string str) => Regex.IsMatch(str, "[A-Za-z0-9\\+\\/\\=]");

        /// <summary>
        /// 是否是正确的email地址
        /// </summary>
        public static bool IsValidEmail(this string obj) =>
            Regex.IsMatch(obj, "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");

        /// <summary>
        /// 是否是正确的ip地址
        /// </summary>
        public static bool IsValidIP(this string obj) => Regex.IsMatch(obj,
            "^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");

        /// <summary>
        /// 是否是正确的手机号
        /// </summary>
        public static bool IsValidMobile(string mobile)
        {
            if (mobile.IsNullOrEmpty())
                return false;
            mobile = mobile.Trim();
            return Regex.IsMatch(mobile, "^(1[3|4|5|6|7|8|9])\\d{9}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否是安全的SQL，防注入
        /// </summary>
        public static bool IsValidSafeSqlString(this string str) => !Regex.IsMatch(str, "[-|;|,|\\/|\\(|\\)|\\[|\\]|\\}|\\{|%|@|\\*|!|\\']");

        /// <summary>
        /// 是否是URL
        /// </summary>
        public static bool IsUrl(this string strUrl) => Regex.IsMatch(strUrl,
            "^(http|https)\\://([a-zA-Z0-9\\.\\-]+(\\:[a-zA-Z0-9\\.&%\\$\\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\\-]+\\.)*[a-zA-Z0-9\\-]+\\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\\:[0-9]+)*(/($|[a-zA-Z0-9\\.\\,\\?\\'\\\\\\+&%\\$#\\=~_\\-]+))*$");

        /// <summary>
        /// 是否为弱密码
        /// 注:密码必须包含数字、小写字母、大写字母和其他符号中的两种并且长度大于8
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static bool IsWeakPwd(this string pwd)
        {
            if (pwd.IsNullOrEmpty())
                throw new Exception("pwd不能为空");

            const string pattern = "(^[0-9]+$)|(^[a-z]+$)|(^[A-Z]+$)|(^.{0,8}$)";
            return Regex.IsMatch(pwd, pattern);
        }
    }
}