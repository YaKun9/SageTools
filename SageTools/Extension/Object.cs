using System;
using Newtonsoft.Json;

namespace SageTools.Extension
{
    public static partial class Extension
    {
        /// <summary>
        ///     ==null 拓展
        /// </summary>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        ///     !=null 拓展
        /// </summary>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        ///     True/False时分别执行不同的操作
        /// </summary>
        public static void IfFalse(this bool @this, Action trueAction, Action falseAction = null)
        {
            if (!@this)
                trueAction();
            else
                falseAction?.Invoke();
        }

        /// <summary>
        ///     True/False时分别执行不同的操作
        /// </summary>
        public static void IfTrue(this bool @this, Action trueAction, Action falseAction = null)
        {
            if (@this)
                trueAction();
            else
                falseAction?.Invoke();
        }

        /// <summary>
        ///     使用 Newtonsoft.Json 库实现对象的深克隆。
        /// </summary>
        /// <typeparam name="T">要克隆的对象类型。</typeparam>
        /// <param name="source">要克隆的对象。</param>
        /// <returns>克隆后的新对象。</returns>
        public static T DeepClone<T>(this T source)
        {
            // 如果对象为 null，则返回 null
            if (source == null)
                return default;
            // 使用 Newtonsoft.Json 序列化和反序列化实现深克隆
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }

}