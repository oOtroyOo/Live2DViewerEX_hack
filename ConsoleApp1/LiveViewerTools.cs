using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;
public class LiveViewerTools
{
    public const string PACKAGE_NAME = "com.pavostudio.live2dviewerex";
    public const string DATAPATH_PATH = "/data/data/" + PACKAGE_NAME + "/";
    public const string PLAYERPREFS_PATH = DATAPATH_PATH + "shared_prefs/com.pavostudio.live2dviewerex.v2.playerprefs.xml";

    public PrefData prefData;
    public SaveData saveData;
    public PresetData presetData;

    private XmlNode dataNode;
    private XmlDocument playerPrefsDocument;
    public XmlDocument ReadXml(string path)
    {
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
        playerPrefsDocument = ReadXml(path);
        string data = "";
        XmlNode map = playerPrefsDocument.SelectSingleNode("map");
        dataNode = FindElement(map, "data");
        data = HttpUtility.UrlDecode(FindElement(map, "data").InnerText);
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

    public void SavePlayerPrefData(PrefData newData = null, string path = PLAYERPREFS_PATH)
    {
        if (newData == null)
        {
            newData = prefData;
        }
        string data = JJJLMMOLDJJ.ICLIEPENIGG(ConsoleApp1.COGFDJGBDDE.JDNKNLNDGNB(newData, true));
        data = HttpUtility.UrlEncode(data);
        Console.WriteLine(data);
        dataNode.InnerText = data;
        dataNode.OwnerDocument.Save(path);
    }

    public string ReadDatString(string file)
    {

        byte[] numArray = File.ReadAllBytes(file);
        EPGPCKFMMPF.CNEDIOODIHD(numArray);
        string data = Encoding.UTF8.GetString(numArray);
        File.WriteAllText(file + ".json", data);
        return data;
    }

    public void SaveDat<T>(T data, string file)
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
        LFPNEHOMABO.PDJMKIOBAPD(presetData.charDatas[0].zipFilePath,new FGHKCBIHELO())
    }
}

