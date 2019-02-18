using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using SimpleFileBrowser;
using UnityEngine;
using Debug = System.Diagnostics.Debug;


public class Program
{
    private static string file = "com.pavostudio.live2dviewerex.v2.playerprefs.xml";
    private static string carafile = "save/model/0aebec1a5c751a10396c4790bedbb49e";

    private static XmlElement dataNode;
    public static LiveViewerTools liveViewerTools;
    public static void Main(string[] args)
    {
        liveViewerTools = new LiveViewerTools();

        return;
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

    public static bool UserSave(PrefData prefData, string path = LiveViewerTools.PLAYERPREFS_PATH)
    {
        return liveViewerTools.SavePlayerPrefData(prefData, path);
    }

    public static PrefData UserLoad(string path = LiveViewerTools.PLAYERPREFS_PATH)
    {
#if UNITY_EDITOR
        Directory.CreateDirectory(Path.GetDirectoryName(path));
#endif
        return liveViewerTools.ReadPlayerPrefData(path);
    }

    public static bool UserCheck()
    {
        return RequestRoot();
    }


    public static bool RequestRoot()
    {
        ProcessStartInfo processInfo = new ProcessStartInfo("su");
#if UNITY_EDITOR
        processInfo.FileName = "cmd";
        processInfo.Arguments = "java";
#endif
        //processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;
        //processInfo.RedirectStandardError = true;
        //processInfo.RedirectStandardInput = true;
        processInfo.RedirectStandardOutput = true;

        try
        {
            PopUI.ShowMessage(LogType.Log, "等待Root");
            Process p = Process.Start(processInfo);
            while (!p.HasExited)
            {
                string s = p.StandardOutput.ReadToEnd();
                UnityEngine.Debug.Log(s);
                if (s.IndexOf("Denied", StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    return false;
                }
                Thread.Sleep(1);
            }


            return true;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
#if UNITY_EDITOR
        return true;
#endif
        return false;
    }
}

