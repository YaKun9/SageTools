using System.Linq;
using SageTools.Extension;

namespace SageTools.Console;

public static class Extension
{
    public static string GetArgValue(this string[] args, string argKey)
    {
        if (args.IsNullOrEmpty() || argKey.IsNullOrWhiteSpace()) return default;
        argKey = '-' + argKey.TrimStart('-').TrimEnd('=') + '=';
        return args.Where(x => x.Contains(argKey)).Select(x => x.Replace(argKey, "")).FirstOrDefault();
    }
}