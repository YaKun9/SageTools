using System;
using System.Linq;
using System.Text;

namespace SageTools.Utils
{
    /// <summary>
    /// 短 ID 生成类
    /// <para>代码参考自：https://github.com/MonkSoul/Furion </para>
    /// </summary>
    public static class ShortIdGenUtil
    {
        /// <summary>
        /// 短 ID 生成器期初数据
        /// </summary>
        private static Random _random = new Random();

        private const string Big = "ABCDEFGHIJKLMNPQRSTUVWXY";
        private const string Small = "abcdefghjklmnopqrstuvwxyz";
        private const string Number = "0123456789";
        private const string Special = "_-";
        private static string _pool = $"{Small}{Big}";

        /// <summary>
        /// 线程安全锁
        /// </summary>
        private static readonly object ThreadLock = new object();

        /// <summary>
        /// 生成目前比较主流的短 ID
        /// <para>包含字母、数字，不包含特殊字符</para>
        /// <para>默认生成 8 位</para>
        /// </summary>
        /// <returns></returns>
        public static string NextId()
        {
            return NextId(new GenerationOptions
            {
                UseNumbers = true,
                UseSpecialCharacters = false,
                Length = 8
            });
        }

        /// <summary>
        /// 生成短 ID
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string NextId(GenerationOptions options)
        {
            // 配置必填
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            // 判断生成的长度是否小于规定的长度，规定为 8
            if (options.Length < Constants.MINIMUM_AUTO_LENGTH)
            {
                throw new ArgumentException(
                    $"The specified length of {options.Length} is less than the lower limit of {Constants.MINIMUM_AUTO_LENGTH} to avoid conflicts.");
            }
            var characterPool = ShortIdGenUtil._pool;
            var poolBuilder = new StringBuilder(characterPool);

            // 是否包含数字
            if (options.UseNumbers)
            {
                poolBuilder.Append(Number);
            }

            // 是否包含特殊字符
            if (options.UseSpecialCharacters)
            {
                poolBuilder.Append(Special);
            }
            var pool = poolBuilder.ToString();

            // 生成拼接
            var output = new char[options.Length];
            for (var i = 0; i < options.Length; i++)
            {
                lock (ThreadLock)
                {
                    var charIndex = _random.Next(0, pool.Length);
                    output[i] = pool[charIndex];
                }
            }
            return new string(output);
        }

        /// <summary>
        /// 设置参与运算的字符，最少 50 位
        /// </summary>
        /// <param name="characters"></param>
        public static void SetCharacters(string characters)
        {
            if (string.IsNullOrWhiteSpace(characters))
            {
                throw new ArgumentException("The replacement characters must not be null or empty.");
            }
            var charSet = characters
                .ToCharArray()
                .Where(x => !char.IsWhiteSpace(x))
                .Distinct()
                .ToArray();
            if (charSet.Length < Constants.MINIMUM_CHARACTER_SET_LENGTH)
            {
                throw new InvalidOperationException(
                    $"The replacement characters must be at least {Constants.MINIMUM_CHARACTER_SET_LENGTH} letters in length and without whitespace.");
            }
            lock (ThreadLock)
            {
                _pool = new string(charSet);
            }
        }

        /// <summary>
        /// 设置种子步长
        /// </summary>
        /// <param name="seed"></param>
        public static void SetSeed(int seed)
        {
            lock (ThreadLock)
            {
                _random = new Random(seed);
            }
        }

        /// <summary>
        /// 重置所有配置
        /// </summary>
        public static void Reset()
        {
            lock (ThreadLock)
            {
                _random = new Random();
                _pool = $"{Small}{Big}";
            }
        }

        /// <summary>
        /// 短 ID 约束
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// 最小长度
            /// </summary>
            public const int MINIMUM_AUTO_LENGTH = 8;

            /// <summary>
            /// 最大长度
            /// </summary>
            public const int MAXIMUM_AUTO_LENGTH = 14;

            /// <summary>
            /// 最小可选字符长度
            /// </summary>
            public const int MINIMUM_CHARACTER_SET_LENGTH = 50;
        }

        /// <summary>
        /// 生成配置选项
        /// </summary>
        public class GenerationOptions
        {
            /// <summary>
            /// 是否使用数字
            /// </summary>
            public bool UseNumbers { get; set; }

            /// <summary>
            /// 是否使用特殊字符
            /// </summary>
            public bool UseSpecialCharacters { get; set; }

            /// <summary>
            /// 生成长度
            /// </summary>
            public int Length { get; set; }
        }
    }
}