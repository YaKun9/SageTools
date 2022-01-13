using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

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

        /// <summary>
        /// 字符串转布尔
        /// </summary>
        /// <param name="str">True=是/true/True/1 其他值均为false</param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            if (str.IsNullOrEmpty()) return false;
            var trueStrList = new List<string>()
            {
                "是", "true", "True", "1"
            };
            return trueStrList.Contains(str);
        }

        /// <summary>
        /// string.Format()拓展
        /// </summary>
        public static string Format(this string str, params object[] args) => string.Format(str, args);

        /// <summary>
        /// string.Join()拓展
        /// </summary>
        public static string Join(this string separator, string[] value) => string.Join(separator, value);

        /// <summary>
        /// string.Join()拓展
        /// </summary>
        public static string Join(this string separator, object[] values) => string.Join(separator, values);

        /// <summary>
        /// string.Join()拓展
        /// </summary>
        public static string Join<T>(this string separator, IEnumerable<T> values) => string.Join<T>(separator, values);

        /// <summary>
        /// string.Join()拓展
        /// </summary>
        public static string Join(this string separator, IEnumerable<string> values) => string.Join(separator, values);

        /// <summary>
        /// string.Join()拓展
        /// </summary>
        public static string Join(this string separator, string[] value, int startIndex, int count) => string.Join(separator, value, startIndex, count);

        /// <summary>
        /// string.Replace("oldValue","")
        /// </summary>
        public static string ReplaceByEmpty(this string str, params string[] values)
        {
            foreach (var oldValue in values)
            {
                str = str.Replace(oldValue, "");
            }
            return str;
        }

        /// <summary>
        /// 保存为文件
        /// </summary>
        public static void SaveAsFile(this string @this, string fileName, bool append = false)
        {
            using TextWriter textWriter = new StreamWriter(fileName, append);
            textWriter.Write(@this);
        }

        /// <summary>
        /// 保存为文件
        /// </summary>
        public static void SaveAsFile(this string @this, FileInfo file, bool append = false)
        {
            using TextWriter textWriter = new StreamWriter(file.FullName, append);
            textWriter.Write(@this);
        }

        /// <summary>
        /// 将数据库类型转换为C#类型
        /// </summary>
        public static SqlDbType SqlTypeNameToSqlDbType(this string @this)
        {
            switch (@this.ToLower())
            {
                case "bigint":
                    return SqlDbType.BigInt;
                case "binary":
                    return SqlDbType.Binary;
                case "bit":
                    return SqlDbType.Bit;
                case "char":
                    return SqlDbType.Char;
                case "date":
                    return SqlDbType.Date;
                case "datetime":
                    return SqlDbType.DateTime;
                case "datetime2":
                    return SqlDbType.DateTime2;
                case "datetimeoffset":
                    return SqlDbType.DateTimeOffset;
                case "decimal":
                    return SqlDbType.Decimal;
                case "float":
                    return SqlDbType.Float;
                case "geography":
                    return SqlDbType.Udt;
                case "geometry":
                    return SqlDbType.Udt;
                case "hierarchyid":
                    return SqlDbType.Udt;
                case "image":
                    return SqlDbType.Image;
                case "int":
                    return SqlDbType.Int;
                case "money":
                    return SqlDbType.Money;
                case "nchar":
                    return SqlDbType.NChar;
                case "ntext":
                    return SqlDbType.NText;
                case "numeric":
                    return SqlDbType.Decimal;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "real":
                    return SqlDbType.Real;
                case "smalldatetime":
                    return SqlDbType.SmallDateTime;
                case "smallint":
                    return SqlDbType.SmallInt;
                case "smallmoney":
                    return SqlDbType.SmallMoney;
                case "sql_variant":
                    return SqlDbType.Variant;
                case "sysname":
                    return SqlDbType.NVarChar;
                case "text":
                    return SqlDbType.Text;
                case "time":
                    return SqlDbType.Time;
                case "timestamp":
                    return SqlDbType.Timestamp;
                case "tinyint":
                    return SqlDbType.TinyInt;
                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;
                case "varbinary":
                    return SqlDbType.VarBinary;
                case "varchar":
                    return SqlDbType.VarChar;
                case "xml":
                    return SqlDbType.Xml;
                default:
                    throw new Exception(string.Format("Unsupported Type: {0}", (object) @this));
            }
        }

        /// <summary>
        /// 拼接URL
        /// </summary>
        /// <param name="host">host地址</param>
        /// <param name="url">链接</param>
        /// <param name="isSsl">是否使用https</param>
        /// <returns></returns>
        public static string CombineApiUrl(this string host, string url, bool isSsl = false)
        {
            string returnUrl = "";
            returnUrl = !host.EndsWith("/") ? host + "/" + url.TrimStart('/') : host + url.TrimStart('/');
            isSsl.IfTrue(() =>
            {
                if (returnUrl.Contains("https://"))
                    return;
                returnUrl = returnUrl + "https://" + returnUrl;
            }, () =>
            {
                if (returnUrl.Contains("http://"))
                    return;
                returnUrl = returnUrl + "http://" + returnUrl;
            });
            return returnUrl;
        }

        public static string HtmlDecode(this string s) => HttpUtility.HtmlDecode(s);

        public static void HtmlDecode(this string s, TextWriter output) => HttpUtility.HtmlDecode(s, output);

        public static string HtmlEncode(this string s) => HttpUtility.HtmlEncode(s);

        public static void HtmlEncode(this string s, TextWriter output) => HttpUtility.HtmlEncode(s, output);

        public static string UrlDecode(this string str) => HttpUtility.UrlDecode(str);

        public static string UrlDecode(this string str, Encoding e) => HttpUtility.UrlDecode(str, e);

        public static string UrlEncode(this string str) => HttpUtility.UrlEncode(str);

        public static string UrlEncode(this string str, Encoding e) => HttpUtility.UrlEncode(str, e);

        /// <summary>
        /// 截取字符串，多余部分以...结尾
        /// </summary>
        public static string Cut(this string @this, int maxLength)
        {
            if (@this == null || @this.Length <= maxLength)
                return @this;
            int length = maxLength - "...".Length;
            return @this.Substring(0, length) + "...";
        }

        /// <summary>
        /// 截取字符串，多余部分以指定内容结尾
        /// </summary>
        public static string Cut(this string @this, int maxLength, string suffix)
        {
            if (@this == null || @this.Length <= maxLength)
                return @this;
            int length = maxLength - suffix.Length;
            return @this.Substring(0, length) + suffix;
        }

        /// <summary>
        /// 进行MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5(this string str)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str));
            string str1 = "";
            for (int index = 0; index < hash.Length; ++index)
                str1 += hash[index].ToString("x").PadLeft(2, '0');
            return str1;
        }

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
        public static bool IsValidEmail(this string obj) => Regex.IsMatch(obj, "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");

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
        /// 满足条件则添加
        /// </summary>
        
        public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (T obj in values)
            {
                if (predicate(obj))
                    @this.Append((object) obj);
            }
            return @this;
        }

        /// <summary>
        /// 满足条件则添加
        /// </summary>
        public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (T obj in values)
            {
                if (predicate(obj))
                    @this.AppendLine(obj.ToString());
            }
            return @this;
        }

        /// <summary>
        /// 追加一行,并string.Format()
        /// </summary>
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, params object[] args)
        {
            @this.AppendLine(string.Format(format, args));
            return @this;
        }

        /// <summary>
        /// 追加一行,并string.Format()
        /// </summary>
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
        {
            @this.AppendLine(string.Format(format, (object) args));
            return @this;
        }

        /// <summary>
        /// 从左截取指定字符，超出长度则截取到末尾
        /// </summary>
        public static string LeftSafe(this string @this, int length) => @this.Substring(0, Math.Min(length, @this.Length));

        /// <summary>
        /// 从左截取指定字符，超出长度则截取到末尾
        /// </summary>
        public static string Left(this string @this, int length) => @this.LeftSafe(length);

        /// <summary>
        /// 从右截取指定字符，超出长度则截取到开头
        /// </summary>
        public static string RightSafe(this string @this, int length) => @this.Substring(Math.Max(0, @this.Length - length));

        /// <summary>
        /// 从右截取指定字符，超出长度则截取到开头
        /// </summary>
        public static string Right(this string @this, int length) => @this.RightSafe(length);
    }
}