using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisplayNotificationSystem
{
    public string Title;
    public string Subtitle;
    public string Message;
    public bool PlaySound;
    public float DisplayTime;
    public bool AutoDismiss;


    public DisplayNotificationSystem(string title, string subtitle, string message, bool playsound, float displaytime,bool autodismiss)
    {
        Title = title;
        Subtitle = subtitle;
        Message = message;
        PlaySound = playsound;
        DisplayTime = displaytime;
        AutoDismiss = autodismiss;
    }
}