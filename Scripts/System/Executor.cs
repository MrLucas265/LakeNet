using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Executor : MonoBehaviour
{
    private GameObject Hardware;
    private GameObject Puter;
    public AudioSource AS;
    public Rect windowRect;
    public float native_width = 1920;
    public float native_height = 1080;
    public int windowID;

    public bool show;

    public Rect CloseButton;

    private Computer com;
    private AppMan appman;

    public Rect VolumeName;
    public Rect VolumeBar;

    public string ProgramName;
    public string ProgramtoRunName;


    // Use this for initialization

    void Start()
    {
        Hardware = GameObject.Find("Hardware");
        Puter = GameObject.Find("System");

        com = Puter.GetComponent<Computer>();
        appman = Puter.GetComponent<AppMan>();

        //windowRect.width = 300;
        //windowRect.height = 71;

        //VolumeBar = new Rect(2, 24, windowRect.width-4, 22);
        //VolumeName = new Rect(2, 47, 60, 22);

        VolumeBar = new Rect(2, 24, 235, 22);
        VolumeName = new Rect(238, 24, 60, 22);

        windowRect.width = 300;
        windowRect.height = 48;


        CloseButton = new Rect(windowRect.width - 23, 2, 21, 21);



        AfterStart();
    }

    void AfterStart()
    {
        windowRect.x = Customize.cust.windowx[windowID];
        windowRect.y = Customize.cust.windowy[windowID];
    }

    void OnGUI()
    {
        Customize.cust.windowx[windowID] = windowRect.x;
        Customize.cust.windowy[windowID] = windowRect.y;

        GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

        if (show == true)
        {
            GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
        }
    }

    void DoMyWindow(int WindowID)
    {
        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
            {
                Close();
            }
        }
        else
        {
            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
            GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]);
        }

        Render();
    }

    public void ExecuteProgram()
    {
        for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
        {
            if (GameControl.control.ProgramFiles[i].Name == ProgramtoRunName)
            {
                appman.SelectedApp = GameControl.control.ProgramFiles[i].Target;
            }
        }
    }

    public void Close()
    {
        appman.SelectedApp = "Executor";
    }

    void Render()
    {
        GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
        GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

        GUI.DragWindow(new Rect(2, 2, CloseButton.x - 3, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x - 3, 21), "Executor");

        ProgramtoRunName = GUI.TextField(new Rect(VolumeBar), ProgramtoRunName);

        if (GUI.Button(new Rect(VolumeName), "Execute"))
        {
            ExecuteProgram();
            Close();
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            ExecuteProgram();
            Close();
        }
    }
}
