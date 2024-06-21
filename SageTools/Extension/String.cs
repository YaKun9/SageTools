using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        /// <summary>
        ///     判断字符串是否为 null 或空字符串。
        /// </summary>
        /// <param name="str">要检查的字符串。</param>
        /// <returns>如果字符串为 null 或空字符串，则返回 true；否则返回 false。</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        ///     判断字符串是否为 null、空字符串或仅包含空白字符。
        /// </summary>
        /// <param name="str">要检查的字符串。</param>
        /// <returns>如果字符串为 null、空字符串或仅包含空白字符，则返回 true；否则返回 false。</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        ///     判断字符串是否不为 null 且不为空字符串。
        /// </summary>
        /// <param name="str">要检查的字符串。</param>
        /// <returns>如果字符串不为 null 且不为空字符串，则返回 true；否则返回 false。</returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }

        /// <summary>
        ///     判断字符串是否不为 null、空字符串或仅包含空白字符。
        /// </summary>
        /// <param name="str">要检查的字符串。</param>
        /// <returns>如果字符串不为 null、空字符串或仅包含空白字符，则返回 true；否则返回 false。</returns>
        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return !str.IsNullOrWhiteSpace();
        }
        
        /// <summary>
        ///     字符串转布尔
        /// </summary>
        /// <param name="str">True=是/true/True/1 其他值均为false</param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            if (str.IsNullOrEmpty()) return false;
            var trueStrList = new List<string>
            {
                "是", "true", "True", "1"
            };
            return trueStrList.Contains(str);
        }

        /// <summary>
        ///     string.Format()拓展
        /// </summary>
        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        ///     string.Join()拓展
        /// </summary>
        public static string JoinWith(this string separator, string[] value)
        {
            return string.Join(separator, value);
        }

        /// <summary>
        ///     string.Join()拓展
        /// </summary>
        public static string JoinWith(this string separator, object[] values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        ///     string.Join()拓展
        /// </summary>
        public static string JoinWith<T>(this string separator, IEnumerable<T> values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        ///     string.Join()拓展
        /// </summary>
        public static string JoinWith(this string separator, IEnumerable<string> values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        ///     string.Join()拓展
        /// </summary>
        public static string JoinWith(this string separator, string[] value, int startIndex, int count)
        {
            return string.Join(separator, value, startIndex, count);
        }

        /// <summary>
        ///     string.Replace("oldValue","")
        /// </summary>
        public static string ReplaceByEmpty(this string str, params string[] values)
        {
            return values.Aggregate(str, (current, oldValue) => current.Replace(oldValue, ""));
        }

        /// <summary>
        ///     保存为文件
        /// </summary>
        public static void SaveAsFile(this string @this, string fileName, bool append = false)
        {
            using TextWriter textWriter = new StreamWriter(fileName, append);
            textWriter.Write(@this);
        }

        /// <summary>
        ///     保存为文件
        /// </summary>
        public static void SaveAsFile(this string @this, FileInfo file, bool append = false)
        {
            using TextWriter textWriter = new StreamWriter(file.FullName, append);
            textWriter.Write(@this);
        }

        /// <summary>
        ///     将数据库类型转换为C#类型
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
                    throw new Exception(string.Format("Unsupported Type: {0}", @this));
            }
        }

        /// <summary>
        ///     拼接URL
        /// </summary>
        /// <param name="host">host地址</param>
        /// <param name="url">链接</param>
        /// <param name="isSsl">是否使用https</param>
        /// <returns></returns>
        public static string CombineApiUrl(this string host, string url, bool isSsl = false)
        {
            var returnUrl = "";
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

        /// <summary>
        ///     解码 HTML 编码的字符串。
        /// </summary>
        /// <param name="s">要解码的字符串。</param>
        /// <returns>解码后的字符串。</returns>
        public static string HtmlDecode(this string s)
        {
            return HttpUtility.HtmlDecode(s);
        }

        /// <summary>
        ///     解码 HTML 编码的字符串并将结果写入 TextWriter。
        /// </summary>
        /// <param name="s">要解码的字符串。</param>
        /// <param name="output">用于接收解码后结果的 TextWriter。</param>
        public static void HtmlDecode(this string s, TextWriter output)
        {
            HttpUtility.HtmlDecode(s, output);
        }

        /// <summary>
        ///     对字符串进行 HTML 编码。
        /// </summary>
        /// <param name="s">要编码的字符串。</param>
        /// <returns>编码后的字符串。</returns>
        public static string HtmlEncode(this string s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        /// <summary>
        ///     对字符串进行 HTML 编码并将结果写入 TextWriter。
        /// </summary>
        /// <param name="s">要编码的字符串。</param>
        /// <param name="output">用于接收编码后结果的 TextWriter。</param>
        public static void HtmlEncode(this string s, TextWriter output)
        {
            HttpUtility.HtmlEncode(s, output);
        }

        /// <summary>
        ///     解码 URL 编码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串。</param>
        /// <returns>解码后的字符串。</returns>
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        ///     使用指定的编码解码 URL 编码的字符串。
        /// </summary>
        /// <param name="str">要解码的字符串。</param>
        /// <param name="e">指定的编码。</param>
        /// <returns>解码后的字符串。</returns>
        public static string UrlDecode(this string str, Encoding e)
        {
            return HttpUtility.UrlDecode(str, e);
        }

        /// <summary>
        ///     对字符串进行 URL 编码。
        /// </summary>
        /// <param name="str">要编码的字符串。</param>
        /// <returns>编码后的字符串。</returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        ///     使用指定的编码对字符串进行 URL 编码。
        /// </summary>
        /// <param name="str">要编码的字符串。</param>
        /// <param name="e">指定的编码。</param>
        /// <returns>编码后的字符串。</returns>
        public static string UrlEncode(this string str, Encoding e)
        {
            return HttpUtility.UrlEncode(str, e);
        }


        /// <summary>
        ///     截取字符串，多余部分以...结尾
        /// </summary>
        public static string Cut(this string @this, int maxLength)
        {
            if (@this == null || @this.Length <= maxLength)
                return @this;
            var length = maxLength - "...".Length;
            return @this.Substring(0, length) + "...";
        }

        /// <summary>
        ///     截取字符串，多余部分以指定内容结尾
        /// </summary>
        public static string Cut(this string @this, int maxLength, string suffix)
        {
            if (@this == null || @this.Length <= maxLength)
                return @this;
            var length = maxLength - suffix.Length;
            return @this.Substring(0, length) + suffix;
        }

        /// <summary>
        ///     满足条件则添加
        /// </summary>
        public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var obj in values)
                if (predicate(obj))
                    @this.Append(obj);

            return @this;
        }

        /// <summary>
        ///     满足条件则添加
        /// </summary>
        public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var obj in values)
                if (predicate(obj))
                    @this.AppendLine(obj.ToString());

            return @this;
        }

        /// <summary>
        ///     追加一行,并string.Format()
        /// </summary>
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, params object[] args)
        {
            @this.AppendLine(string.Format(format, args));
            return @this;
        }

        /// <summary>
        ///     追加一行,并string.Format()
        /// </summary>
        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
        {
            @this.AppendLine(string.Format(format, args));
            return @this;
        }

        /// <summary>
        ///     从左截取指定字符，超出长度则截取到末尾
        /// </summary>
        public static string LeftSafe(this string @this, int length)
        {
            return @this.Substring(0, Math.Min(length, @this.Length));
        }

        /// <summary>
        ///     从左截取指定字符，超出长度则截取到末尾
        /// </summary>
        public static string Left(this string @this, int length)
        {
            return @this.LeftSafe(length);
        }

        /// <summary>
        ///     从右截取指定字符，超出长度则截取到开头
        /// </summary>
        public static string RightSafe(this string @this, int length)
        {
            return @this.Substring(Math.Max(0, @this.Length - length));
        }

        /// <summary>
        ///     从右截取指定字符，超出长度则截取到开头
        /// </summary>
        public static string Right(this string @this, int length)
        {
            return @this.RightSafe(length);
        }

        /// <summary>
        ///     转为字节数组
        /// </summary>
        /// <param name="base64Str">base64字符串</param>
        /// <returns></returns>
        public static byte[] ToBytes_FromBase64Str(this string base64Str)
        {
            return Convert.FromBase64String(base64Str);
        }

        /// <summary>
        ///     转换为MD5加密后的字符串（默认加密为32位）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMd5String(this string str)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hashBytes) sb.Append(t.ToString("x2"));

            md5.Dispose();

            return sb.ToString();
        }

        /// <summary>
        ///     转换为MD5加密后的字符串（16位）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMd5String16(this string str)
        {
            return str.ToMd5String().Substring(8, 16);
        }

        /// <summary>
        ///     Base64加密
        ///     注:默认采用UTF8编码
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(this string source)
        {
            return Base64Encode(source, Encoding.UTF8);
        }

        /// <summary>
        ///     Base64加密
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
        ///     Base64解密
        ///     注:默认使用UTF8编码
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(this string result)
        {
            return Base64Decode(result, Encoding.UTF8);
        }

        /// <summary>
        ///     Base64解密
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
        ///     Base64Url编码
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
        ///     Base64Url解码
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
        ///     计算SHA1摘要
        ///     注：默认使用UTF8编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] ToSHA1Bytes(this string str)
        {
            return str.ToSha1Bytes(Encoding.UTF8);
        }

        /// <summary>
        ///     计算SHA1摘要
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static byte[] ToSha1Bytes(this string str, Encoding encoding)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            var inputBytes = encoding.GetBytes(str);
            var outputBytes = sha1.ComputeHash(inputBytes);

            return outputBytes;
        }

        /// <summary>
        ///     转为SHA1哈希加密字符串
        ///     注：默认使用UTF8编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToSha1String(this string str)
        {
            return str.ToSHA1String(Encoding.UTF8);
        }

        /// <summary>
        ///     转为SHA1哈希
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ToSHA1String(this string str, Encoding encoding)
        {
            var sha1Bytes = str.ToSha1Bytes(encoding);
            var resStr = BitConverter.ToString(sha1Bytes);
            return resStr.Replace("-", "").ToLower();
        }

        /// <summary>
        ///     SHA256加密
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToSHA256String(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var hash = SHA256.Create().ComputeHash(bytes);

            var builder = new StringBuilder();
            foreach (var t in hash) builder.Append(t.ToString("x2"));

            return builder.ToString();
        }

        /// <summary>
        ///     HMACSHA256算法
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
        ///     二进制字符串转为Int
        /// </summary>
        /// <param name="str">二进制字符串</param>
        /// <returns></returns>
        public static int ToInt_FromBinString(this string str)
        {
            return Convert.ToInt32(str, 2);
        }

        /// <summary>
        ///     将16进制字符串转为Int
        /// </summary>
        /// <param name="str">数值</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        /// <summary>
        ///     string转byte[]
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="theEncoding">需要的编码</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str, Encoding theEncoding)
        {
            return theEncoding.GetBytes(str);
        }

        /// <summary>
        ///     json数据转实体类,仅仅应用于单个实体类，速度非常快
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static T ToEntity<T>(this string json)
        {
            if (string.IsNullOrEmpty(json))
                return default;

            var type = typeof(T);
            var obj = Activator.CreateInstance(type, null);

            foreach (var item in type.GetProperties())
            {
                var info = obj.GetType().GetProperty(item.Name);
                var pattern = "\"" + item.Name + "\":\"(.*?)\"";
                foreach (Match match in Regex.Matches(json, pattern))
                    switch (item.PropertyType.ToString())
                    {
                        case "System.String":
                            info?.SetValue(obj, match.Groups[1].ToString(), null);
                            break;

                        case "System.Int32":
                            info?.SetValue(obj, match.Groups[1].ToString().ToInt32(), null);
                            break;

                        case "System.Int64":
                            info?.SetValue(obj, Convert.ToInt64(match.Groups[1].ToString()), null);
                            break;

                        case "System.DateTime":
                            info?.SetValue(obj, Convert.ToDateTime(match.Groups[1].ToString()), null);
                            break;
                    }
            }

            return (T)obj;
        }

        /// <summary>
        ///     转为首字母大写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToFirstUpperStr(this string str)
        {
            return str[..1].ToUpper() + str[1..];
        }

        /// <summary>
        ///     转为首字母小写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ToFirstLowerStr(this string str)
        {
            return str[..1].ToLower() + str[1..];
        }


        /// <summary>
        ///     将枚举类型的文本转为枚举类型
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="enumText">枚举文本</param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string enumText) where TEnum : struct
        {
            Enum.TryParse(enumText, out TEnum value);

            return value;
        }
    }
}