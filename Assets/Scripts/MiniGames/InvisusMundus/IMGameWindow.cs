using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IMGameWindow : MonoBehaviour
{
    private GameObject Puter;
    private GameObject Game;
    public AudioSource AS;
    public Rect windowRect;
    public float native_width = 1920;
    public float native_height = 1080;
    public int windowID;

    public bool show;

    public Rect CloseButton;

    private Computer com;
    private AppMan appman;
    private IMController control;


    // Use this for initialization

    void Start()
    {
        Puter = GameObject.Find("System");
        Game = GameObject.Find("Invisus Mundus");

        com = Puter.GetComponent<Computer>();
        appman = Puter.GetComponent<AppMan>();
        control = Game.GetComponent<IMController>();

        windowRect.width = 600;
        windowRect.height = 300;


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

        GUI.skin = com.Skin[GameControl.control.GUIID];

        if (show == true)
        {
            GUI.color = com.colors[Customize.cust.WindowColorInt];
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
        }
    }

    void DoMyWindow(int WindowID)
    {
        WindowCloseButton();
        WindowTitleBar();
        RenderGame();
    }

    void RenderGame()
    {
        if (control.Quit == true)
        {
            show = false;
        }

        control.enabled = true;
        control.RenderGame();
    }

    void WindowCloseButton()
    {
        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
            {
                show = false;
                this.enabled = false;
            }
        }
        else
        {
            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
            GUI.contentColor = com.colors[Customize.cust.FontColorInt];
            GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
        }
    }

    void WindowTitleBar()
    {
        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        GUI.DragWindow(new Rect(2, 2, CloseButton.x - 3, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x - 3, 21), "Invisus Mundus");
    }
}
