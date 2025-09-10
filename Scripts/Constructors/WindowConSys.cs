using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WindowConSys
{
    public string ProgramName;
    public string WindowName;
    public string ProcessName;
    public string Status;
    public string Type;
    public int WID;
    public int PID;
    public int WPN;
    public string CurrentTask;
    public SRect windowRect;
    public SRect TitleBoxRect;
    public List<SRect> windowButtons = new List<SRect>();
    public bool Resize;
    public bool Dragging;
    public SRect ResizeRect;
    public SRect WindowResizeRect;
    public List<RegistrySystem> LocalRegister = new List<RegistrySystem>();
    public ResourceManagerSystem CurrentRMS;
    public ResourceManagerSystem InitalRMS;
    public ResourceManagerSystem TotalRMS;
    public bool show;
    public ProgramSystemv2.ProgramTypes ProgramType;

    public WindowConSys(string windowName,string programName,string processName, string status,
        string type,int wid,int pid,int wpn, SRect WindowRect, List<SRect> WindowButtons, SRect titleboxrect,
        bool resize, SRect resizerect, SRect windowresizerect,ResourceManagerSystem initalrms, ProgramSystemv2.ProgramTypes programtype)
    {
        WindowName = windowName;
        ProgramName = programName;
        ProcessName = processName;
        Status = status;
        Type = type;
        WID = wid;
        PID = pid;
        WPN = wpn;
        windowRect = WindowRect;
        windowButtons = WindowButtons;
        TitleBoxRect = titleboxrect;
        Resize = resize;
        ResizeRect = resizerect;
        WindowResizeRect = windowresizerect;
        InitalRMS = initalrms;
        ProgramType = programtype;
    }
}
