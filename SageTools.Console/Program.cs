using SageTools.Extension;
using System;
using System.Linq;
using System.Xml;

namespace SageTools.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Any(x => x.Contains("-h")))
            {
                ShowHelp();
                Environment.Exit(0);
            }

            System.Console.WriteLine("**************开始处理**************");

            var source = args.GetArgValue("s");
            var version = args.GetArgValue("v");
            var description = args.GetArgValue("d");
            var copyright = args.GetArgValue("r");

            UpdateXmlValues(source, description, copyright, version);

            System.Console.WriteLine("**************处理完毕**************");
        }

        private static void ShowHelp()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("使用帮助:");
            System.Console.WriteLine("-h 查看帮助");
            System.Console.WriteLine("-s 项目.csproj文件路径");
            System.Console.WriteLine("-v 指定的版本");
            System.Console.WriteLine("-d 描述");
            System.Console.WriteLine("-r 版权描述");
            System.Console.WriteLine();
        }

        private static void UpdateXmlValues(string xmlFilePath, string newDescription, string newCopyright, string newVersion)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            #region description

            var descriptionNode = xmlDoc.SelectSingleNode("//Description");
            if (descriptionNode != null && newDescription.IsNotNullOrWhiteSpace())
            {
                descriptionNode.InnerText = newDescription;
            }

            #endregion description

            #region copyright

            var copyrightNode = xmlDoc.SelectSingleNode("//Copyright");
            if (copyrightNode != null && newCopyright.IsNotNullOrWhiteSpace())
            {
                copyrightNode.InnerText = newCopyright;
            }

            #endregion copyright

            #region version

            var versionNode = xmlDoc.SelectSingleNode("//Version");
            if (versionNode != null && newVersion.IsNotNullOrWhiteSpace())
            {
                versionNode.InnerText = newVersion;
            }

            #endregion version

            xmlDoc.Save(xmlFilePath);
            System.Console.WriteLine("**************处理结果**************");
            System.Console.WriteLine($"文件路径：{xmlFilePath}");
            System.Console.WriteLine($"版本：{versionNode?.InnerText}");
            System.Console.WriteLine($"描述：{descriptionNode?.InnerText}");
            System.Console.WriteLine($"版权：{copyrightNode?.InnerText}");
        }
    }
}