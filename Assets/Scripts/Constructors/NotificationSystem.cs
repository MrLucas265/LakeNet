using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotificationSystem
{
    public string Title;
    public string Subtitle;
    public string Message;
    public string Time;
    public string Date;
    public NotificationType Type;

    public enum NotificationType
    {
        System,
        Mail,
        Message,
        Reminder
    }


    public NotificationSystem(string title, string subtitle, string message,string time,string date, NotificationType type)
    {
        Title = title;
        Subtitle = subtitle;
        Message = message;
        Time = time;
        Date = date;
        Type = type;
    }
}