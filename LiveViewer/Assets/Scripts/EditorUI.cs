using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour
{
    public Action<string> OnSave;

    public InputField input;

    public Button SaveButton;
    public Button BackButton;
    // Use this for initialization
    void Awake()
    {
        SaveButton.onClick.AddListener(OnSaveButton);
        BackButton.onClick.AddListener(Hide);
    }

    private void OnSaveButton()
    {
        try
        {
            OnSave(input.text);
            Hide();
            
        }
        catch (Exception e)
        {
            PopUI.ShowMessage(LogType.Exception, e.Message);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string data)
    {
        input.text = data;
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
