using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OSFPCSystem
{
    public string BackgroundAddress;
    public string ScreenSaverBackgroundAddress;
    public string ScreenSaverPictureAddress;
    public string MouseCursorAddress;
    public bool ShowDesktopIcons;
    public bool ShowDesktopBackground;
    public int SelectedBackground;
    public string DownloadPath;
    public bool GridMode;
    public List<ProgramSystemv2> QuickList;
    public List<ProgramSystemv2> DesktopList;
    public List<ProgramSystemv2> BarList;
}