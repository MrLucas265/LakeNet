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
    public Rect TitleBoxRect;
    public List<Rect> windowButtons = new List<Rect>();
    public bool Resize;
    public Rect ResizeRect;
    public Rect WindowResizeRect;

    public WindowConSys(string windowName,string programName,string processName, string status, string type,int wid,int pid,Rect WindowRect, List<Rect> WindowButtons, Rect titleboxrect,bool resize, Rect resizerect, Rect windowresizerect)
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
        TitleBoxRect = titleboxrect;
        Resize = resize;
        ResizeRect = resizerect;
        WindowResizeRect = windowresizerect;
    }
}
