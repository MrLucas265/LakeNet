using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ErrorSystem
{
    public string Title;
    public string Message;
    public bool Restart;
    public int WindowID;

    public ErrorSystem(string title, string message,bool restart, int windowid)
    {
        Title = title;
        Message = message;
        Restart = restart;
        WindowID = windowid;
    }
}
