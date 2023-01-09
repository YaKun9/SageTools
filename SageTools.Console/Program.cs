using System;
using System.IO;
using System.Linq;
using System.Xml;
using SageTools.Extension;

namespace SageTools.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var help = args.Any(x => x.Contains("-h"));
            if (help)
            {
                System.Console.WriteLine($"-source= 文件相对路径 {Environment.NewLine}-step= 版本号步长值（整数）{Environment.NewLine}-version= 直接指定版本,忽略-step");
                Environment.Exit(0);
            }
            System.Console.WriteLine("处理xml文件");
            var source = args.GetArgValue("source");
            var step = args.GetArgValue("step");
            var version = args.GetArgValue("version");

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, source);
            if (!File.Exists(path))
            {
                System.Console.WriteLine($"执行错误：【{path}】文件不存在");
                Environment.Exit(-1);
            }
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            var versionNode = xmlDoc.DocumentElement?.SelectSingleNode("PropertyGroup")?.SelectSingleNode("Version");
            if (versionNode.IsNull())
            {
                System.Console.WriteLine("执行错误：项目没有配置版本号");
                Environment.Exit(-1);
            }
            if (version.IsNullOrEmpty())
            {
                version = versionNode!.InnerText;
                var lastNum = version[(version.LastIndexOf(".", StringComparison.Ordinal) + 1)..];
                var newNum = lastNum.ToInt32() + step;
                var newVersion = version[..(version.LastIndexOf(".", StringComparison.Ordinal) + 1)] + newNum;
                versionNode.InnerText = newVersion;
                System.Console.WriteLine($"当前版本{version}，末尾版本号{lastNum}，新版本号{newVersion}");
            }
            else
            {
                System.Console.WriteLine($"直接指定新版本号为 {version}");
                versionNode!.InnerText = version;
            }

            var copyright = xmlDoc.DocumentElement?.SelectSingleNode("PropertyGroup")?.SelectSingleNode("Copyright");
            if (copyright.IsNotNull())
            {
                copyright!.InnerText = $"Copyright © LiuDanK 2021-{DateTime.Now.Year}";
            }

            xmlDoc.Save(path);
            System.Console.WriteLine("处理完毕!");
        }
    }
}