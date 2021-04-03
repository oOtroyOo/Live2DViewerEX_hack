using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        //private static string file = "com.pavostudio.live2dviewerex.v2.playerprefs.xml";
        private static string carafile = "save/model/0aebec1a5c751a10396c4790bedbb49e";

        private static XmlElement dataNode;
        static void Main(string[] args)
        {
            LiveViewerTools liveViewerTools = new LiveViewerTools();
            string data = liveViewerTools.ReadDatString(@"G:\Works\Troy\Live2DViewerEX\EXT_PATH\files\save\pref-0.dat");
            liveViewerTools.SaveDatString(data,@"G:\Works\Troy\Live2DViewerEX\EXT_PATH\files\save\pref.dat");
            //data = data.Replace("Assembly-CSharp", "Assembly-CShack");

            //SaveData save = liveViewerTools.LoadSave(data);

            //var pref = ConsoleApp1.COGFDJGBDDE.HAPNAIKFGNI<PrefData>(data, false, false);
            return;
            if (args.Length == 0)
            {
                Console.WriteLine("请带一个文件参数");
                Console.ReadKey();
                return;
            }
            /*

               FileInfo fileInfo;

               fileInfo = new FileInfo(args[0]);
               PrefData prefData = liveViewerTools.ReadPlayerPrefDataFromXml(fileInfo.FullName);
               Console.WriteLine(prefData.userPoint);
               prefData.userPoint = short.MaxValue - 1;
               liveViewerTools.SavePlayerPrefData(prefData, fileInfo.FullName);

               fileInfo = new FileInfo(dir.FullName + "\\" + "save/model/0aebec1a5c751a10396c4790bedbb49e");
               string data = liveViewerTools.ReadDatString(fileInfo.FullName);
               ModelData.CharState state = liveViewerTools.LoadCaraState(data);
               state.intimacy = 100;
               Console.WriteLine(new DateTime(state.lastTicks));
               state.lastTicks = new DateTime(2019, 2, 16).Ticks;// DateTime.Now.Ticks;
               state.maxIntimacy = 1;
               //liveViewerTools.SaveDat(state, fileInfo.FullName);


               fileInfo = new FileInfo(dir.FullName + "\\" + "save/save.dat");
               data = liveViewerTools.ReadDatString(fileInfo.FullName);
               SaveData save = liveViewerTools.LoadSave(data);
               //liveViewerTools.SaveDat(save, fileInfo.FullName);

               fileInfo = new FileInfo(dir.FullName + "\\" + "save/autosave.dat");
               data = liveViewerTools.ReadDatString(fileInfo.FullName);
               PresetData autosave = liveViewerTools.LoadAutoSave(data);
               //liveViewerTools.SaveDat(autosave, fileInfo.FullName);
               */
            Console.ReadKey();
        }




    }
}
