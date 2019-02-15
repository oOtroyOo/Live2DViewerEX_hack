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
        private static string file = "com.pavostudio.live2dviewerex.v2.playerprefs.xml";
        private static string carafile = "save/model/0aebec1a5c751a10396c4790bedbb49e";

        private static XmlElement dataNode;
        static void Main(string[] args)
        {
            LiveViewerTools liveViewerTools = new LiveViewerTools();
            string path = new StackFrame(true).GetFileName();
            DirectoryInfo dir = new FileInfo(path).Directory.Parent;
            FileInfo fileInfo;

            fileInfo = new FileInfo(dir.FullName + "\\" + file);
            PrefData prefData = liveViewerTools.ReadPlayerPrefData(fileInfo.FullName);
            Console.WriteLine(prefData.userPoint);
            prefData.userPoint = int.MaxValue - 1;
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

            Console.ReadKey();
        }


 

    }
}
