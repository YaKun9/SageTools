﻿using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        /// <summary>
        /// 将流读为字符串
        /// 注：默认使用UTF-8编码
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">指定编码</param>
        /// <returns></returns>
        public static async Task<string> ReadToStringAsync(this Stream stream, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;

            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            var resStr = await new StreamReader(stream, encoding).ReadToEndAsync();

            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            return resStr;
        }
    }
}