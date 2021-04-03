using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public EditorUI EditorUi;
    public Button[] buttonList = new Button[1];


    private static XmlElement dataNode;
    public static LiveViewerTools liveViewerTools = new LiveViewerTools();
#if UNITY_ANDROID
    private static AndroidJavaObject toolClass;

    protected static AndroidJavaObject ToolClass
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
    IEnumerator Start()
    {
        buttonList[0].onClick.AddListener(OnOpenPlayer);
        buttonList[1].onClick.AddListener(OnOpenModel);

        PopUI.ShowMessage(LogType.Log, "读取文件中");
        yield return new WaitForSeconds(0.1f);
        liveViewerTools.CopyFiles();

    }

    public static void UserSave(string data)
    {
        if (liveViewerTools.SaveDatString(data, LiveViewerTools.PrefDataPath))
        {

        }
        else
        {
            PopUI.ShowMessage(LogType.Error, "保存失败，可能是权限不足");
        }
    }

    public static string UserLoad()
    {
        try
        {
            return liveViewerTools.ReadDatString(LiveViewerTools.PrefDataPath);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            PopUI.ShowMessage(LogType.Error, "读取失败，可能是权限不足");
            return null;
        }
    }

    public static bool UserCheck()
    {
        return RequestRoot();
    }


    public static bool RequestRoot()
    {
        try
        {
            return ToolClass.Call<bool>("upgradeRootPermission");
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
    public void OnOpenPlayer()
    {
        //liveViewerTools.CopyFiles();
        //if (UserCheck())
        //{
        string data = UserLoad();
        if (data == null)
        {
            PopUI.ShowMessage(LogType.Error, "读取失败");
            return;
        }
        //data = LiveViewerTools.PrittyJsonString(data);
        EditorUi.SetText(data);
        EditorUi.OnSave = UserSave;
        //}
        //else
        //{
        //    PopUI.ShowMessage(LogType.Error, "没有Root权限或文件不存在");
        //}
    }

    public void OnOpenModel()
    {
        //liveViewerTools.CopyFiles();
        if (FileBrowser.CheckPermission() == FileBrowser.Permission.Granted)
        {
#if UNITY_EDITOR
            //Directory.CreateDirectory(LiveViewerTools.EXT_PRESISTDATA_PATH);
#endif
            FileBrowser.ShowLoadDialog(OnFileOpenSuccess, () => { }, false, LiveViewerTools.WorkingDir + "/" + LiveViewerTools.CharaPath);
        }
        else
        {
            PopUI.ShowMessage(LogType.Error, "请开启文件权限");
        }
    }

    private void OnFileOpenSuccess(string path)
    {
        string data = liveViewerTools.ReadDatString(path);
        EditorUi.SetText(data);
        EditorUi.OnSave = (text) => CheckSaveModel(text, path);
    }

    private bool CheckSaveModel(string text, string path)
    {
        try
        {
            var obj = JsonConvert.DeserializeObject(text);
            liveViewerTools.SaveDatString(text, path);
            return true;
        }
        catch (Exception e)
        {
            PopUI.ShowMessage(LogType.Exception, e.Message);
        }
        return false;
    }
}
