using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif
using Formatting = Newtonsoft.Json.Formatting;

public class LiveViewerTools
{
    public const string PACKAGE_NAME = "com.pavostudio.live2dviewerex";
    public const string DATAPATH_PATH = "/data/data/" + PACKAGE_NAME + "/";
    public const string EXT_PRESISTDATA_PATH = "/sdcard/Android/data/" + PACKAGE_NAME + "/files";
    public const string PLAYERPREFS_PATH = DATAPATH_PATH + "shared_prefs/com.pavostudio.live2dviewerex.v2.playerprefs.xml";

    public PrefData prefData;
    public SaveData saveData;
    public PresetData presetData;

    private XmlNode dataNode;
    private XmlDocument playerPrefsDocument;
#if UNITY_ANDROID
    private AndroidJavaObject toolClass;

    protected internal AndroidJavaObject ToolClass
    {
        get
        {
            if (toolClass == null)
            {
                toolClass = new AndroidJavaObject("com.zy.tools.Tool", new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"));
            }
            return toolClass;
        }

        set
        {
            toolClass = value;
        }
    }
#endif
    public XmlDocument ReadXml(string path)
    {
        Console.WriteLine("Read xml " + path);
        XmlDocument xml = new XmlDocument();
        xml.Load(path);
        return xml;
    }

    public XmlElement FindElement(XmlNode root, string name)
    {
        foreach (XmlElement node in root.ChildNodes)
        {
            if (node.Name == "string" && node.GetAttribute("name") == name)
            {
                return node;
            }
        }

        return null;
    }
    public PrefData ReadPlayerPrefData(string path = PLAYERPREFS_PATH)
    {
        try
        {
            File.ReadAllText(path);
        }
        catch (Exception e)
        {
#if UNITY_ANDROID
            string newpath = Application.persistentDataPath + "/" + Path.GetFileName(path);


            string command = string.Format("cp -f {0} {1}", path, newpath);
            bool root = ToolClass.Call<bool>("RootCommand", command);
            if (root)
            {
                path = newpath;
                new Thread(() =>
                {
                    Thread.Sleep(1000);
                    if (File.Exists(newpath))
                    {

                        File.Delete(newpath);
                    }
                }).Start();
            }
            else
            {
                throw e;
            }
#else
            throw e;
#endif

        }
        playerPrefsDocument = ReadXml(path);
        string data = "";
        XmlNode map = playerPrefsDocument.SelectSingleNode("map");
        dataNode = FindElement(map, "data");
#if UNITY_5_3_OR_NEWER
        data = WWW.UnEscapeURL(FindElement(map, "data").InnerText);
#else
        data = HttpUtility.UrlDecode(FindElement(map, "data").InnerText);
#endif
        data = JJJLMMOLDJJ.IOAMJKJJOJO(data);
        Console.WriteLine(data);
        prefData = ConsoleApp1.COGFDJGBDDE.HAPNAIKFGNI<PrefData>(data, false, false);
        Console.WriteLine(prefData.userPoint);
        return prefData;
    }
    public ModelData.CharState LoadCaraState(string data)
    {
        ModelData.CharState state = ConsoleApp1.COGFDJGBDDE.HAPNAIKFGNI<ModelData.CharState>(data, false, true);
        return state;
    }

    public bool SavePlayerPrefData(PrefData newData = null, string path = PLAYERPREFS_PATH)
    {
        try
        {
            if (newData == null)
            {
                newData = prefData;
            }
            string data = JJJLMMOLDJJ.ICLIEPENIGG(ConsoleApp1.COGFDJGBDDE.JDNKNLNDGNB(newData, true));
#if UNITY_5_3_OR_NEWER
            data = WWW.EscapeURL(data);
#else
            data = HttpUtility.UrlEncode(data);
#endif

            Console.WriteLine(data);
            dataNode.InnerText = data;
#if UNITY_ANDROID
            string newpath = Application.persistentDataPath + "/" + Path.GetFileName(path);

            dataNode.OwnerDocument.Save(newpath);
            string command = string.Format("cp -f {0} {1}", newpath, path);
            bool root = ToolClass.Call<bool>("RootCommand", command);
            new Thread(() =>
            {
                Thread.Sleep(100);
                if (File.Exists(newpath))
                {

                    File.Delete(newpath);
                }
            }).Start();
            return root;
#else
            dataNode.OwnerDocument.Save(path);
            return true;
#endif
        }
        catch (Exception e)
        {
            return false;
        }

        return false;
    }

    public string ReadDatString(string file)
    {

        byte[] numArray = File.ReadAllBytes(file);
        EPGPCKFMMPF.CNEDIOODIHD(numArray);
        string data = Encoding.UTF8.GetString(numArray);
        data = ConvertJsonString(data);
        File.WriteAllText(file + ".json", data);
        return data;
    }

    public void SaveDat(object data, string file)
    {
        object[] customAttributes = data.GetType().GetCustomAttributes(false);
        if (customAttributes.Any(a => a is SerializableAttribute))
        {

            byte[] bytes = Encoding.UTF8.GetBytes(COGFDJGBDDE.JDNKNLNDGNB(data, false));
            EPGPCKFMMPF.CNEDIOODIHD(bytes);
            File.WriteAllBytes(file, bytes);
        }
        else
        {
            throw new CustomAttributeFormatException();
        }
    }
    public void SaveDatString(string data, string file)
    {

        byte[] bytes = Encoding.UTF8.GetBytes(data);
        EPGPCKFMMPF.CNEDIOODIHD(bytes);
        File.WriteAllBytes(file, bytes);

    }

    public SaveData LoadSave(string data)
    {
        saveData = ConsoleApp1.COGFDJGBDDE.HAPNAIKFGNI<SaveData>(data, false, true);
        return saveData;
    }

    public PresetData LoadAutoSave(string data)
    {
        presetData = ConsoleApp1.COGFDJGBDDE.HAPNAIKFGNI<PresetData>(data, false, true);
        return presetData;
    }

    public void OpenLpk()
    {
        //LFPNEHOMABO.PDJMKIOBAPD(presetData.charDatas[0].zipFilePath,new FGHKCBIHELO())
    }

    public static string ConvertJsonString(string str)
    {
        //格式化json字符串
        JsonSerializer serializer = new JsonSerializer();
        TextReader tr = new StringReader(str);
        JsonTextReader jtr = new JsonTextReader(tr);
        object obj = serializer.Deserialize(jtr);
        if (obj != null)
        {
            StringWriter textWriter = new StringWriter();
            JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
            {
                Formatting = Formatting.Indented,
                Indentation = 4,
                IndentChar = ' '
            };
            serializer.Serialize(jsonWriter, obj);
            return textWriter.ToString();
        }
        else
        {
            return str;
        }
    }
}

