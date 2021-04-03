using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif
using Formatting = Newtonsoft.Json.Formatting;

public class LiveViewerTools
{
    /* 已弃用
    public const string DATAPATH_PATH = "/data/data/" + PACKAGE_NAME + "/";
    public const string PLAYERPREFS_FILE = "com.pavostudio.live2dviewerex.v2.playerprefs.xml";
    public const string PLAYERPREFS_PATH = DATAPATH_PATH + "shared_prefs/" + PLAYERPREFS_FILE;
     
     */
    public const string PACKAGE_NAME = "com.pavostudio.live2dviewerex";
    public const string PrefDataPath = "save/pref.dat";
    public const string CharaPath = "save/cha/";

    public PrefData prefData;
    public SaveData saveData;
    public PresetData presetData;

    public static string WorkingDir =>
#if !UNITY_EDITOR&&UNITY_ANDROID
        Application.persistentDataPath;
#else
        Environment.CurrentDirectory;
#endif
    private XmlNode dataNode;
    private XmlDocument playerPrefsDocument;
#if UNITY_ANDROID
    public static string EXT_PRESISTDATA_PATH { get; } = Application.persistentDataPath.Replace(Application.identifier, PACKAGE_NAME);

    private AndroidJavaObject toolClass;

    private bool requested = false;

    protected internal AndroidJavaObject Tool
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
    public bool RootCommand(string command)
    {
        return Tool.Call<bool>("RootCommand", command);
    }

    public void CopyFiles()
    {
        if (Application.isMobilePlatform)
        {
            try
            {
                CopyEntireDir(EXT_PRESISTDATA_PATH, WorkingDir);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            if (!requested)
            {

                ManualResetEvent manualResetEvent = new ManualResetEvent(false);
                Tool.Set("pkg", PACKAGE_NAME);
                Tool.Call("RequestDataPermission", new AndroidRunable(() =>
                {
                    Debug.Log("RequestDataPermission back");
                    requested = true;
                    Tool.Call("CopyAllFiles");
                    manualResetEvent.Set();
                }));
                manualResetEvent.WaitOne();
            }
            else
            {
                Tool.Call("CopyAllFiles");
            }
        }
        //try
        //{
        //    CopyEntireDir(EXT_PRESISTDATA_PATH, Application.persistentDataPath);
        //}
        //catch (Exception e)
        //{
        //    Debug.LogException(e);
        //}

    }

    public bool CopyBackFile(string file)
    {
        try
        {
            File.Copy(file, file.Replace(Application.identifier, PACKAGE_NAME));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return Tool.Call<bool>("CopyBack", file);
        }

        //File.Copy(Application.persistentDataPath + "/" + file, EXT_PRESISTDATA_PATH);
    }
#endif

    public static void CopyEntireDir(string sourcePath, string destPath)
    {
        //Now Create all of the directories
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
            SearchOption.AllDirectories))
            Directory.CreateDirectory(dirPath.Replace(sourcePath, destPath));

        //Copy all the files & Replaces any files with the same name
        foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
            SearchOption.AllDirectories))
            File.Copy(newPath, newPath.Replace(sourcePath, destPath), true);
    }
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

    /* 已弃用
    [Obsolete]
    public PrefData ReadPlayerPrefDataFromXml(string path = PLAYERPREFS_PATH)
    {
        playerPrefsDocument = ReadXml(path);
        string data = "";
        XmlNode map = playerPrefsDocument.SelectSingleNode("map");
        dataNode = FindElement(map, "data");
#if UNITY_5_3_OR_NEWER
        data = WWW.UnEscapeURL(FindElement(map, "data").InnerText);
#else
        data = WebUtility.UrlDecode(FindElement(map, "data").InnerText);
#endif
        data = JJJLMMOLDJJ.IOAMJKJJOJO(data);
        Console.WriteLine(data);
        prefData = LoadPrefData(data);
        return prefData;
    }
    */
    public PrefData LoadPrefData(string json)
    {
        prefData = Serializer.Deserialize<PrefData>(json, false, false);
        return prefData;
    }

    public ModelData.CharState LoadCaraState(string data)
    {
        ModelData.CharState state = Serializer.Deserialize<ModelData.CharState>(data, false, true);
        return state;
    }

    /*
    //已弃用
    [Obsolete]
    public bool SavePlayerPrefData(PrefData newData = null)
    {
        try
        {
            if (newData == null)
            {
                newData = prefData;
            }
            string data = JJJLMMOLDJJ.ICLIEPENIGG(Serializer.Serialize(newData, true));
#if UNITY_5_3_OR_NEWER
            data = WWW.EscapeURL(data);
#else
            data = WebUtility.UrlEncode(data);
#endif

            Console.WriteLine(data);
            dataNode.InnerText = data;
#if UNITY_ANDROID
            string newpath = Application.persistentDataPath + "/" + Path.GetFileName(path);

            dataNode.OwnerDocument.Save(newpath);
            string command = string.Format("cp -rf {0} {1}", newpath, path);
            bool root = ToolClass.Call<bool>("RootCommand", command);
            //new Thread(() =>
            //{
            //    Thread.Sleep(100);
            //    if (File.Exists(newpath))
            //    {

            //        File.Delete(newpath);
            //    }
            //}).Start();
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
    */
    public string ReadDatString(string file)
    {
        if (!File.Exists(file))
        {
            file = WorkingDir + "/" + file;
        }
        byte[] numArray = File.ReadAllBytes(file);
        TransCodeByteArray(numArray);
        string data = Encoding.UTF8.GetString(numArray);
        string prittyJsonString = PrittyJsonString(data);
        //File.WriteAllText(WorkingDir + "/" + file + ".json", prittyJsonString);
        return prittyJsonString;
    }

    //EPGPCKFMMPF.CNEDIOODIHD
    public void TransCodeByteArray(byte[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = (byte)((data.Length & 255) ^ (int)data[i]);
        }

    }

    public void SaveDat(object obj, string file)
    {
        string json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        SaveDatString(json, file);
    }
    public bool SaveDatString(string data, string file)
    {
        if (!file.Contains(WorkingDir))
        {
            file = WorkingDir + "/" + file;
        }
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        TransCodeByteArray(bytes);
        File.WriteAllBytes(file, bytes);
#if UNITY_ANDROID
        return CopyBackFile(file);
#else
        return true;
#endif
    }

    public SaveData LoadSave(string data)
    {
        saveData = Serializer.Deserialize<SaveData>(data, false, true);
        return saveData;
    }

    public PresetData LoadAutoSave(string data)
    {
        presetData = Serializer.Deserialize<PresetData>(data, false, true);
        return presetData;
    }

    public void OpenLpk()
    {
        //LFPNEHOMABO.PDJMKIOBAPD(presetData.charDatas[0].zipFilePath,new FGHKCBIHELO())
    }

    public static string PrittyJsonString(string str)
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
public class Serializer
{
    public static string ErrorMessage;


    public static T Deserialize<T>(string jsonStr, bool ignoreSetting = false, bool useTypeBinding = false)
    {
        Serializer.ErrorMessage = (string)null;
        if (ignoreSetting)
            return JsonConvert.DeserializeObject<T>(jsonStr);
        if (useTypeBinding)
            return JsonConvert.DeserializeObject<T>(jsonStr, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                SerializationBinder = new SerializationBinder(),
                Error = ((target, args) =>
                 {
                     ErrorMessage = args.ErrorContext.Error.Message;
                     args.ErrorContext.Handled = true;
                 })
            });
        return JsonConvert.DeserializeObject<T>(jsonStr, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Error = ((target, args) =>
            {
                ErrorMessage = args.ErrorContext.Error.Message;
                args.ErrorContext.Handled = true;
            })
        });
    }

    public static string Serialize(object obj, bool ignoreSetting = false)
    {
        if (ignoreSetting)
            return JsonConvert.SerializeObject(obj);
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        });
    }


}
public class SerializationBinder : DefaultSerializationBinder
{
    public virtual System.Type OPJGBCHGHGD(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "模型 {0} 设定")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Интервал")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type JLHCBAILINH(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == ".model.json")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Visualization")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type EKJMPDKLDBH(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "Full Size")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Webコントロール")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type OOKDLGNAGKC(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "陰影")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Load data failed")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type BMMHEEIAIFI(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "Flexible")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Live2D")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type PLKBPGKMMIA(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "ParamEyeROpen")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Manga 4")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type KIGIGMEMOEF(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "30FPS")
                return typeof(ModelData.ModelSetting);
            if (typeName == "キーバインディング - 表情")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type MKFJOPKPANJ(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "グループ")
                return typeof(ModelData.ModelSetting);
            if (typeName == "雪")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type INKFPCIACJH(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "上傳物品")
                return typeof(ModelData.ModelSetting);
            if (typeName == "８ビット")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type FMFMBOJOHDM(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "+")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Нет настраиваемого контента")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type CEFDGCLLKAJ(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "按键绑定")
                return typeof(ModelData.ModelSetting);
            if (typeName == "全屏")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type MEKBKLHIJKJ(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "模型")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Compat")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type HAMDPHBMBLN(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "Отменить")
                return typeof(ModelData.ModelSetting);
            if (typeName == "[AVProVideo]HLSParser cannot parse stream ")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type OPLNOMNNGNK(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "壞掉的玻璃2")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Recent Events: ")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type EAMBCNACMBD(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "SetFocusRotation")
                return typeof(ModelData.ModelSetting);
            if (typeName == "текст")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type JPJGBLLDMOJ(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "costume")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Save")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type MFDOFNLPAIL(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "[")
                return typeof(ModelData.ModelSetting);
            if (typeName == "水平縮放")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public override System.Type BindToType(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "SettingData+ModelSetting")
                return typeof(ModelData.ModelSetting);
            if (typeName == "SettingData+ModelSetting[]")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type EOLKNADKFBF(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "/")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Array is empty, or not valid.")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type LNHGMDPJHGH(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "New Cell Shading")
                return typeof(ModelData.ModelSetting);
            if (typeName == "_UseYpCbCr")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type GNOLIFGCAFJ(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "小部件透明度")
                return typeof(ModelData.ModelSetting);
            if (typeName == "DefaultWallpaperOffsetEmulator")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type OJDOGKLAMMJ(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "Deinitialise")
                return typeof(ModelData.ModelSetting);
            if (typeName == "ユーザー")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type FNNMBLDEMED(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "/")
                return typeof(ModelData.ModelSetting);
            if (typeName == "/")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type KEEGCONLBDB(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "Глобальные настройки")
                return typeof(ModelData.ModelSetting);
            if (typeName == "init_param")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type POHEHFNPLLL(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "weight")
                return typeof(ModelData.ModelSetting);
            if (typeName == "连接出错，程序即将退出")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type MFOKLJMLLLN(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "Array is empty, or not valid.")
                return typeof(ModelData.ModelSetting);
            if (typeName == "リズムに合わせてスケール")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type CCGONACEOLD(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "Bubble Auto Close")
                return typeof(ModelData.ModelSetting);
            if (typeName == "preview")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type JGEHFNFHLJI(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "秒の表示")
                return typeof(ModelData.ModelSetting);
            if (typeName == "Список изменений")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type EHJGONMDCOF(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "發射數量")
                return typeof(ModelData.ModelSetting);
            if (typeName == "オフセット")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }

    public virtual System.Type LJHLDPEFDDC(string assemblyName, string typeName)
    {
        if (typeName != null)
        {
            if (typeName == "setup")
                return typeof(ModelData.ModelSetting);
            if (typeName == "ビデオ")
                return typeof(ModelData.ModelSetting[]);
        }
        return base.BindToType(assemblyName, typeName);
    }
}