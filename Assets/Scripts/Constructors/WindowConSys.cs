using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WindowConSys
{
    public string WindowName;
    public string ProgramName;
    public string ProcessName;
    public string Status;
    public string Type;
    public int WID;
    public int PID;
    public Rect windowRect;
    public List<Rect> windowButtons = new List<Rect>();
    public Rect titleBox;

    public WindowConSys(string windowName,string programName,string processName, string status, string type,int wid,int pid,Rect WindowRect, List<Rect> WindowButtons, Rect TitleBox)
    {
        WindowName = windowName;
        ProgramName = programName;
        ProcessName = processName;
        Status = status;
        Type = type;
        WID = wid;
        PID = pid;
        windowRect = WindowRect;
        windowButtons = WindowButtons;
        titleBox = TitleBox;
    }
}
