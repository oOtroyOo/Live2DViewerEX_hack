using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        private static string file = "com.pavostudio.live2dviewerex.v2.playerprefs.xml";
        private static FileInfo fileInfo;

        private static XmlElement dataNode;
        static void Main(string[] args)
        {
            string path = new StackFrame(true).GetFileName();
            DirectoryInfo dir = new FileInfo(path).Directory.Parent;
            fileInfo = new FileInfo(dir.FullName + "\\" + file);
            string data = ReadFile();
            PrefData prefData = Read(data);
            prefData.userPoint = int.MaxValue - 1;
            Console.WriteLine(prefData.userPoint);
            Save(prefData);
            Console.Read();
        }

        static string ReadFile()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(fileInfo.FullName);
            XmlNode map = xml.SelectSingleNode("map");
            foreach (XmlElement node in map.ChildNodes)
            {
                if (node.Name == "string" && node.GetAttribute("name") == "data")
                {
                    dataNode = node;
                    return HttpUtility.UrlDecode(node.InnerText);
                }
            }
            return "";
        }
        static PrefData Read(string DMFJIAEPOCK)
        {
            string data = JJJLMMOLDJJ.IOAMJKJJOJO(DMFJIAEPOCK);
            Console.WriteLine(data);
            PrefData prefData = ConsoleApp1.COGFDJGBDDE.HAPNAIKFGNI<PrefData>(data, false, false);
            Console.WriteLine(prefData.userPoint);
            return prefData;
        }
        static void Save(PrefData prefData)
        {
            string data = JJJLMMOLDJJ.ICLIEPENIGG(ConsoleApp1.COGFDJGBDDE.JDNKNLNDGNB(prefData, true));
            data = HttpUtility.UrlEncode(data);
            Console.WriteLine(data);
            dataNode.InnerText = data;
            dataNode.OwnerDocument.Save(file);
        }
    }
}
