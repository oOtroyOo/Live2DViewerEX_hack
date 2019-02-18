using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public EditorUI EditorUi;
    public Button[] buttonList = new Button[1];
    void Start()
    {
        Program.Main(null);
        buttonList[0].onClick.AddListener(OnOpenPlayer);
        buttonList[1].onClick.AddListener(OnOpenModel);
    }

    public void OnOpenPlayer()
    {
        if (Program.UserCheck())
        {
            PrefData player = Program.UserLoad();
            string data = JsonConvert.SerializeObject(player);
            data = LiveViewerTools.ConvertJsonString(data);
            EditorUi.SetText(data);
            EditorUi.OnSave = OnUserSave;
        }
        else
        {
            PopUI.ShowMessage(LogType.Error, "请开启Root权限");
        }
    }

    private bool OnUserSave(string arg)
    {
        PrefData player;
        try
        {
            player = JsonConvert.DeserializeObject<PrefData>(arg);

        }
        catch (Exception e)
        {
            PopUI.ShowMessage(LogType.Exception, e.Message);
            return false;
        }

        if (player != null)
        {

            return Program.UserSave(player);
        }

        return false;
    }

    public void OnOpenModel()
    {

        if (FileBrowser.CheckPermission() == FileBrowser.Permission.Granted)
        {
#if UNITY_EDITOR
            Directory.CreateDirectory(LiveViewerTools.EXT_PRESISTDATA_PATH);
#endif
            FileBrowser.ShowLoadDialog(OnFileOpenSuccess, () => { }, false, LiveViewerTools.EXT_PRESISTDATA_PATH);
        }
        else
        {
            PopUI.ShowMessage(LogType.Error, "请开启文件权限");
        }
    }

    private void OnFileOpenSuccess(string path)
    {
        string data = Program.liveViewerTools.ReadDatString(path);
        EditorUi.SetText(data);
        EditorUi.OnSave = (text) => CheckSaveModel(text, path);
    }

    private bool CheckSaveModel(string text, string path)
    {
        try
        {
            var obj = JsonConvert.DeserializeObject(text);
            Program.liveViewerTools.SaveDatString(text, path);
            return true;
        }
        catch (Exception e)
        {
            PopUI.ShowMessage(LogType.Exception, e.Message);
        }
        return false;
    }
}
