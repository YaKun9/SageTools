using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SageTools.Extension
{
    /// <summary>
    /// Json拓展类
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 将对象转换为 JSON 字符串。
        /// </summary>
        /// <typeparam name="T">对象的类型。</typeparam>
        /// <param name="obj">要转换的对象。</param>
        /// <param name="indented">是否格式化输出。</param>
        /// <param name="camelCase">是否使用小驼峰命名。</param>
        /// <returns>对象的 JSON 字符串表示。</returns>
        public static string ToJson<T>(this T obj, bool indented = false, bool camelCase = false)
        {
            var settings = new JsonSerializerSettings();

            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }

            if (camelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 将 JSON 字符串转换为对象。
        /// </summary>
        /// <typeparam name="T">目标对象的类型。</typeparam>
        /// <param name="json">要转换的 JSON 字符串。</param>
        /// <returns>从 JSON 字符串转换的对象。</returns>
        public static T FromJson<T>(this string json)
        {
            return json.IsNullOrWhiteSpace() ? default : JsonConvert.DeserializeObject<T>(json);
        }
    }
}