using System;
using UnityEngine;



public class AndroidRunable : AndroidJavaProxy
{
    private readonly Action action;
    public AndroidRunable(Action action) : base("java.lang.Runnable")
    {
        this.action = action;
    }
    public void run()
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}

