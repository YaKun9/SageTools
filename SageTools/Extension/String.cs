using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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
                    throw new Exception(string.Format("Unsupported Type: {0}", (object)@this));
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
        /// 满足条件则添加
        /// </summary>
        public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var obj in values)
            {
                if (predicate(obj))
                    @this.Append((object)obj);
            }

            return @this;
        }

        /// <summary>
        /// 满足条件则添加
        /// </summary>
        public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var obj in values)
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
            @this.AppendLine(string.Format(format, (object)args));
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

        /// <summary>
        ///  转换为日期格式
        /// </summary>
        public static DateTime ToDateTime(this string @this) => Convert.ToDateTime(@this);

        /// <summary>
        /// 转为字节数组
        /// </summary>
        /// <param name="base64Str">base64字符串</param>
        /// <returns></returns>
        public static byte[] ToBytes_FromBase64Str(this string base64Str)
        {
            return Convert.FromBase64String(base64Str);
        }

        /// <summary>
        /// 转换为MD5加密后的字符串（默认加密为32位）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5String(this string str)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("x2"));
            }

            md5.Dispose();

            return sb.ToString();
        }

        /// <summary>
        /// 转换为MD5加密后的字符串（16位）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5String16(this string str)
        {
            return str.ToMD5String().Substring(8, 16);
        }

        /// <summary>
        /// Base64加密
        /// 注:默认采用UTF8编码
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(this string source)
        {
            return Base64Encode(source, Encoding.UTF8);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <param name="encoding">加密采用的编码方式</param>
        /// <returns></returns>
        public static string Base64Encode(this string source, Encoding encoding)
        {
            string encode;
            var bytes = encoding.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }

            return encode;
        }

        /// <summary>
        /// Base64解密
        /// 注:默认使用UTF8编码
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(this string result)
        {
            return Base64Decode(result, Encoding.UTF8);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <param name="encoding">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(this string result, Encoding encoding)
        {
            string decode;
            var bytes = Convert.FromBase64String(result);
            try
            {
                decode = encoding.GetString(bytes);
            }
            catch
            {
                decode = result;
            }

            return decode;
        }

        /// <summary>
        /// Base64Url编码
        /// </summary>
        /// <param name="text">待编码的文本字符串</param>
        /// <returns>编码的文本字符串</returns>
        public static string Base64UrlEncode(this string text)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(text);
            var base64 = Convert.ToBase64String(plainTextBytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');

            return base64;
        }

        /// <summary>
        /// Base64Url解码
        /// </summary>
        /// <param name="base64UrlStr">使用Base64Url编码后的字符串</param>
        /// <returns>解码后的内容</returns>
        public static string Base64UrlDecode(this string base64UrlStr)
        {
            base64UrlStr = base64UrlStr.Replace('-', '+').Replace('_', '/');
            switch (base64UrlStr.Length % 4)
            {
                case 2:
                    base64UrlStr += "==";
                    break;

                case 3:
                    base64UrlStr += "=";
                    break;
            }

            var bytes = Convert.FromBase64String(base64UrlStr);

            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 计算SHA1摘要
        /// 注：默认使用UTF8编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] ToSHA1Bytes(this string str)
        {
            return str.ToSHA1Bytes(Encoding.UTF8);
        }

        /// <summary>
        /// 计算SHA1摘要
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static byte[] ToSHA1Bytes(this string str, Encoding encoding)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            var inputBytes = encoding.GetBytes(str);
            var outputBytes = sha1.ComputeHash(inputBytes);

            return outputBytes;
        }

        /// <summary>
        /// 转为SHA1哈希加密字符串
        /// 注：默认使用UTF8编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToSHA1String(this string str)
        {
            return str.ToSHA1String(Encoding.UTF8);
        }

        /// <summary>
        /// 转为SHA1哈希
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ToSHA1String(this string str, Encoding encoding)
        {
            var sha1Bytes = str.ToSHA1Bytes(encoding);
            var resStr = BitConverter.ToString(sha1Bytes);
            return resStr.Replace("-", "").ToLower();
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToSHA256String(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var hash = SHA256.Create().ComputeHash(bytes);

            var builder = new StringBuilder();
            foreach (var t in hash)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// HMACSHA256算法
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="secret">密钥</param>
        /// <returns></returns>
        public static string ToHMACSHA256String(this string text, string secret)
        {
            secret ??= "";
            var keyByte = Encoding.UTF8.GetBytes(secret);
            var messageBytes = Encoding.UTF8.GetBytes(text);
            using var hmacsha256 = new HMACSHA256(keyByte);
            var hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        /// <summary>
        /// string转int
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            str = str.Replace("\0", "");
            if (string.IsNullOrEmpty(str))
                return 0;
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// string转long
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static long ToLong(this string str)
        {
            str = str.Replace("\0", "");
            if (string.IsNullOrEmpty(str))
                return 0;

            return Convert.ToInt64(str);
        }

        /// <summary>
        /// 二进制字符串转为Int
        /// </summary>
        /// <param name="str">二进制字符串</param>
        /// <returns></returns>
        public static int ToInt_FromBinString(this string str)
        {
            return Convert.ToInt32(str, 2);
        }

        /// <summary>
        /// 将16进制字符串转为Int
        /// </summary>
        /// <param name="str">数值</param>
        /// <returns></returns>
        public static int ToInt0X(this string str)
        {
            int num = Int32.Parse(str, NumberStyles.HexNumber);
            return num;
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static double ToDouble(this string str)
        {
            return Convert.ToDouble(str);
        }

        /// <summary>
        /// string转byte[]
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        /// <summary>
        /// string转byte[]
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="theEncoding">需要的编码</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str, Encoding theEncoding)
        {
            return theEncoding.GetBytes(str);
        }

        /// <summary>
        /// 将16进制字符串转为Byte数组
        /// </summary>
        /// <param name="str">16进制字符串(2个16进制字符表示一个Byte)</param>
        /// <returns></returns>
        public static byte[] To0XBytes(this string str)
        {
            List<byte> resBytes = new List<byte>();
            for (int i = 0; i < str.Length; i = i + 2)
            {
                string numStr = $@"{str[i]}{str[i + 1]}";
                resBytes.Add((byte)numStr.ToInt0X());
            }

            return resBytes.ToArray();
        }

        /// <summary>
        /// 将ASCII码形式的字符串转为对应字节数组
        /// 注：一个字节一个ASCII码字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] ToASCIIBytes(this string str)
        {
            return str.ToList().Select(x => (byte)x).ToArray();
        }

        /// <summary>
        /// 删除Json字符串中键中的@符号
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns></returns>
        public static string RemoveAt(this string jsonStr)
        {
            Regex reg = new Regex("\"@([^ \"]*)\"\\s*:\\s*\"(([^ \"]+\\s*)*)\"");
            string strPatten = "\"$1\":\"$2\"";
            return reg.Replace(jsonStr, strPatten);
        }

        /// <summary>
        /// json数据转实体类,仅仅应用于单个实体类，速度非常快
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static T ToEntity<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return default(T);

            var type = typeof(T);
            var obj = Activator.CreateInstance(type, null);

            foreach (var item in type.GetProperties())
            {
                var info = obj.GetType().GetProperty(item.Name);
                var pattern = "\"" + item.Name + "\":\"(.*?)\"";
                foreach (Match match in Regex.Matches(json, pattern))
                {
                    switch (item.PropertyType.ToString())
                    {
                        case "System.String":
                            info?.SetValue(obj, match.Groups[1].ToString(), null);
                            break;

                        case "System.Int32":
                            info?.SetValue(obj, match.Groups[1].ToString().ToInt(), null);
                            break;

                        case "System.Int64":
                            info?.SetValue(obj, Convert.ToInt64(match.Groups[1].ToString()), null);
                            break;

                        case "System.DateTime":
                            info?.SetValue(obj, Convert.ToDateTime(match.Groups[1].ToString()), null);
                            break;
                    }
                }
            }

            return (T)obj;
        }

        /// <summary>
        /// 转为首字母大写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToFirstUpperStr(this string str)
        {
            return str[..1].ToUpper() + str[1..];
        }

        /// <summary>
        /// 转为首字母小写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToFirstLowerStr(this string str)
        {
            return str[..1].ToLower() + str[1..];
        }

        /// <summary>
        /// 转为网络终结点IPEndPoint
        /// </summary>=
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static IPEndPoint ToIPEndPoint(this string str)
        {
            IPEndPoint iPEndPoint = null;
            try
            {
                var strArray = str.Split(':').ToArray();
                var addr = strArray[0];
                var port = Convert.ToInt32(strArray[1]);
                iPEndPoint = new IPEndPoint(IPAddress.Parse(addr), port);
            }
            catch
            {
                iPEndPoint = null;
            }

            return iPEndPoint;
        }

        /// <summary>
        /// 将枚举类型的文本转为枚举类型
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="enumText">枚举文本</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string enumText) where TEnum : struct
        {
            System.Enum.TryParse(enumText, out TEnum value);

            return value;
        }

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