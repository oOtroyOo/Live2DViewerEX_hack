using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class PopUI : MonoBehaviour
{
    public Text text;

    private static PopUI instance;
    // Use this for initialization
    void Start()
    {
        instance = this;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Hide()
    {
        gameObject.SetActive(false);
        text.text = "";
    }
    public static void ShowMessage(LogType logType, string message)
    {
        if (instance != null)
        {
            instance.gameObject.SetActive(true);
            switch (logType)
            {
                case LogType.Warning:
                    message = "<color=yellow>" + message + "</color>";
                    break;
                case LogType.Error:
                case LogType.Exception:
                    message = "<color=red>" + message + "</color>";
                    break;
            }

            instance.text.text = message;
            instance.Invoke("Hide", 3);
        }
    }
}
