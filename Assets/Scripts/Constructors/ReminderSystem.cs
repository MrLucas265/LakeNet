using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReminderSystem
{
    public string Title;
    public string Subtitle;
    public string Message;
    public DateSystem CreatedTime;
    public DateSystem EventStart;
    public DateSystem EventEnd;
    public string ReminderType;
    public string Repeat;
    public int RepeatTimes;

    public ReminderSystem(string title, string subtitle, string message,DateSystem created,DateSystem start,DateSystem end,string remindertype,string repeat,int repeattimes)
    {
        Title = title;
        Subtitle = subtitle;
        Message = message;
        CreatedTime = created;
        EventStart = start;
        EventEnd = end;
        ReminderType = remindertype;
        Repeat = repeat;
        RepeatTimes = repeattimes;
    }
}