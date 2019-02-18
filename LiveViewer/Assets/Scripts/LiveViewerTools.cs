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
using Formatting = Newtonsoft.Json.Formatting;

public class LiveViewerTools
{
    public const string PACKAGE_NAME = "com.pavostudio.live2dviewerex";
    public const string DATAPATH_PATH = "/data/data/" + PACKAGE_NAME + "/";
    public const string EXT_PRESISTDATA_PATH = "/SDcard/Android/data/" + PACKAGE_NAME + "/files";
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

    public bool SavePlayerPrefData(PrefData newData = null, string path = PLAYERPREFS_PATH)
    {
        try
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
        catch (Exception e)
        {
            return false;
        }

        return true;
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

