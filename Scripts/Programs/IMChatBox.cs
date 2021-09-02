//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class IMChatBox : MonoBehaviour
//{
//    private GameObject Hardware;
//    private GameObject Puter;
//    public AudioSource AS;
//    public Rect windowRect;
//    public float native_width = 1920;
//    public float native_height = 1080;
//    public int windowID;

//    public Texture2D sdIcon;
//    public Texture2D restartIcon;
//    public Texture2D logIcon;
//    public bool show;

//    public bool Game;

//    public bool shutdown;
//    public bool restart;

//    public GUISkin Skin;

//    public Rect CloseButton;

//    private Clock clk;
//    private CD cd;
//    private Boot boot;
//    private OS os;
//    private Defalt def;
//    private Computer com;
//    private SoundControl sc;
//    private HardwareCFile hcf;
//    int DesktopStyle = 3;

//    private AppMan appman;

//    public List<ContactsSystem> Contacts = new List<ContactsSystem>();
//    // Use this for initialization

//    void Start()
//    {
//        Hardware = GameObject.Find("Hardware");
//        Puter = GameObject.Find("System");
//        cd = GetComponent<CD>();
//        com = Puter.GetComponent<Computer>();
//        clk = Puter.GetComponent<Clock>();
//        boot = Puter.GetComponent<Boot>();
//        os = Puter.GetComponent<OS>();
//        sc = Puter.GetComponent<SoundControl>();
//        def = Puter.GetComponent<Defalt>();
//        hcf = Hardware.GetComponent<HardwareCFile>();
//        appman = Puter.GetComponent<AppMan>();

//        if (Game == true)
//        {
//            native_height = Customize.cust.native_height;
//            native_width = Customize.cust.native_width;
//        }

//        AfterStart();
//        SetPos();
//    }

//    void AfterStart()
//    {
//        windowRect.x = Customize.cust.windowx[windowID];
//        windowRect.y = Customize.cust.windowy[windowID];
//    }

//    void SetPos()
//    {
//        windowRect.width = 300;
//        windowRect.height = 300;
//        CloseButton = new Rect(windowRect.width - 23, 2, 21, 21);

//    }

//    void OnGUI()
//    {
//        Customize.cust.windowx[windowID] = windowRect.x;
//        Customize.cust.windowy[windowID] = windowRect.y;

//        GUI.skin = com.Skin[GameControl.control.GUIID];

//        if (show == true)
//        {
//            GUI.color = com.colors[Customize.cust.WindowColorInt];
//            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
//        }
//    }

//    void DoMyWindow(int WindowID)
//    {
//        if (CloseButton.Contains(Event.current.mousePosition))
//        {
//            if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
//            {
//                //appman.SelectedApp = "Gateway Viewer";
//            }
//        }
//        else
//        {
//            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//            GUI.contentColor = com.colors[Customize.cust.FontColorInt];
//            GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
//        }

//        Render();
//    }

//    void Render()
//    {
//        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

//        GUI.DragWindow(new Rect(2, 2, CloseButton.x - 3, 21));
//        GUI.Box(new Rect(2, 2, CloseButton.x - 3, 21), "IMChatBox");

//        scrollpos = GUI.BeginScrollView(new Rect(SearchList), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
//        for (scrollsize = 0; scrollsize < ListOfSites.Count; scrollsize++)
//        {
//            if (Inputted != "" && ListOfSites.Count >= 1)
//            {
//                if (GUI.Button(new Rect(1 * Scale, 1 + scrollsize * 21, 150 * Scale, 20), ListOfSites[scrollsize]))
//                {
//                    appman.ProgramName = ListOfSites[scrollsize];
//                    appman.SelectedApp = ListOfTargets[scrollsize];
//                }
//            }
//        }
//        GUI.EndScrollView();
//    }
//}
