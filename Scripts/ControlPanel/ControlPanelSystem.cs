using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlPanelSystem
{
    public string Name;
    public string Location;
    public string Target;

    public enum Menu
    {
        Home,
        Display,
        Notification,
        Account,
        Mouse,
        WebBrowser,
        QuickLaunch,
        Commands,
        Soundtrack,
        Download,
        DefaultPrograms,
        Autosave,
        DevSettings,
        Backgrounds,
        DisplaySettings,
        Scaling,
        ScreenSaver,
        Font,
        Color,
        Desktop,
        FontColor,
        ButtonColor,
        WindowColor,
        ProfilePic,
        Password,
        Hint,
        DefaultDocuments,
        Themes,
    }

    public Menu TargetMenu;

    public ControlPanelSystem()
    {

    }

    public ControlPanelSystem(string name,string location,string target,Menu targetmenu)
    {
        Name = name;
        Location = location;
        Target = target;
        TargetMenu = targetmenu;
    }
}