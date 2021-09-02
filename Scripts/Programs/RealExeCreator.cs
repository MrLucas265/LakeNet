using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RealExeCreator : MonoBehaviour
{
    public bool show;
    private Computer com;
    public int windowID;
    public Rect windowRect;

    private AppMan appman;

    private Defalt defalt;

    public float native_width = 1920;
    public float native_height = 1080;

    private Rect CloseButton;


    public string Name;
    public string GameLocation;
    public string RealLocation;

    public bool CreateDesktopIcon;
    public bool AddQL;

    private List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
    private List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

    void Start()
    {
        com = GetComponent<Computer>();
        defalt = GetComponent<Defalt>();
        appman = GetComponent<AppMan>();

        PosCheck();

        native_height = Customize.cust.native_height;
        native_width = Customize.cust.native_width;

        windowRect.width = 300;
        windowRect.height = 243;

        CloseButton = new Rect(windowRect.width - 23, 2, 21, 21);
    }

    void PosCheck()
    {
        if (Customize.cust.windowx[windowID] == 0)
        {
            Customize.cust.windowx[windowID] = Screen.width / 2;
        }
        if (Customize.cust.windowy[windowID] <= 35)
        {
            Customize.cust.windowy[windowID] = 35;
        }

        windowRect.x = Customize.cust.windowx[windowID];
        windowRect.y = Customize.cust.windowy[windowID];
    }

    void OnGUI()
    {
        Customize.cust.windowx[windowID] = windowRect.x;
        Customize.cust.windowy[windowID] = windowRect.y;
        GUI.skin = com.Skin[GameControl.control.GUIID];

        if (show == true)
        {
            GUI.color = com.colors[Customize.cust.WindowColorInt];
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
        }
    }

    void DoMyWindow(int windowID)
    {
        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
            {
                appman.SelectedApp = "Real Exe Creator";
            }

            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
            GUI.contentColor = com.colors[Customize.cust.FontColorInt];
        }
        else
        {
            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
            GUI.contentColor = com.colors[Customize.cust.FontColorInt];

            GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
        }

        GUI.DragWindow(new Rect(2, 2, CloseButton.x-3, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x - 3, 21), "" + "Real Exe Creator");

        GUI.Label(new Rect(2, 30, 200, 21), "Game File Name: ");
        Name = GUI.TextField(new Rect(2, 50, 250, 21),Name);

        GUI.Label(new Rect(2, 70, 200, 21), "Game File Path: "); 
        GameLocation = GUI.TextField(new Rect(2, 90, 250, 21), GameLocation);

        GUI.Label(new Rect(2, 110, 300, 21), "Real File Path: ie. C:/Programs/test.mp4 "); 
        RealLocation = GUI.TextField(new Rect(2, 130, 250, 21), RealLocation);

        GUI.Toggle(new Rect(2, 160, 236, 21),CreateDesktopIcon, "Create Desktop Icon");

        GUI.Toggle(new Rect(2, 180, 236, 21), AddQL, "Add to quick launch");

        if (GUI.Button(new Rect(2, 220, 120, 21), "Create", com.Skin[GameControl.control.GUIID].customStyles[0]))
        {
            GameControl.control.DesktopIconList.Add(new ProgramSystem(Name, "", "", "", "", "", GameLocation, RealLocation, "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        }
    }
}
