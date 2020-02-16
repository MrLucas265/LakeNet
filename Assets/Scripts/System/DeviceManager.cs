using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    public Rect windowRect = new Rect(100, 100, 200, 200);
    public float native_width = 1920;
    public float native_height = 1080;
    public int windowID;

    public Rect CloseButton;
    public Rect MiniButton;
    public Rect SettingsButton;
    public Rect DefaltSetting;
    public Rect DefaltBoxSetting;

    public bool minimize;

    private Computer com;

    public bool show;

    public List<string> RunningApplications = new List<string>();
    public List<int> RunningApplicationsWindowID = new List<int>();
    public List<float> CPUList = new List<float>();
    public List<float> MemoryList = new List<float>();
    public List<float> GraphicsList = new List<float>();
    public List<float> DiskList = new List<float>();
    public List<float> NetworkList = new List<float>();

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;

    public bool ShowApplications;
    public bool showStartUp;
    public bool showDetails;
    public bool showServices;
    public bool showPerformance;
    public bool showUsers;
    public bool showHistory;
    public bool showSettings;
    public bool showSpecs;

    public bool showCPU;
    public bool showRAM;
    public bool showPSU;
    public bool showDisk;
    public bool showNET;
    public bool showGPU;
    public bool AnyShowen;

    public Texture2D[] Pics;

    public string Title;

    public string hint;

    public bool CompactMode;
    public bool ChangeLayout;
    public string CompactModeName;

    private GameObject Hardware;
    private CPU cpu;
    private RAM ram;
    private PSU psu;
    private HardDrives hdd;
    private Networks net;

    public int Index;

    private SoundControl sc;
    private AppMan appman;

    //CONTEXT MENU
    public Rect ContextwindowRect = new Rect(100, 100, 100, 200);
    public bool ShowContext;
    public int ContextMenuID;
    public int SelectedProgram;
    public List<string> ContextMenuOptions = new List<string>();
    public string SelectedOption;
    public float LastClick;
    public ProgramSystem SelectedTask;

    public int Selected;

    public int SelectedDevice;

    public Rect Test = new Rect(0, 0, 0, 0);
    public Rect TestL = new Rect(0, 0, 0, 0);

    // Use this for initialization
    void Start()
    {
        Hardware = GameObject.Find("Hardware");

        cpu = Hardware.GetComponent<CPU>();
        ram = Hardware.GetComponent<RAM>();
        psu = Hardware.GetComponent<PSU>();
        com = GetComponent<Computer>();
        sc = GetComponent<SoundControl>();
        appman = GetComponent<AppMan>();

        PosCheck();

        ContextwindowRect.width = 100;

        windowRect.width = 400;

        CloseButton = new Rect(windowRect.width - 22, 1, 21, 21);
        MiniButton = new Rect(windowRect.width - 43, 1, 21, 21);

        if (CompactMode == true)
        {
            minimize = true;
            Minimize();
            showSpecs = true;
        }
    }

    void PosCheck()
    {
        if (Customize.cust.windowx[windowID] == 0)
        {
            Customize.cust.windowx[windowID] = Screen.width / 2;
        }
        if (Customize.cust.windowy[windowID] == 0)
        {
            Customize.cust.windowy[windowID] = Screen.height / 2;
        }

        windowRect.x = Customize.cust.windowx[windowID];
        windowRect.y = Customize.cust.windowy[windowID];
    }

    void Minimize()
    {
        if(minimize == true)
        {
            windowRect.height = 23;
        }
        else
        {
            windowRect.height = 200;
        }
    }

    void CloseAllMainMenus()
    {
        ShowApplications = false;
        showStartUp = false;
        showDetails = false;
        showServices = false;
        showPerformance = false;
        showUsers = false;
        showHistory = false;
        showSettings = false;
        showSpecs = false;
        CloseAllMainMenuSubMenus();
    }

    void CloseAllMainMenuSubMenus()
    {
        showCPU = false;
        showRAM = false;
        showPSU = false;
        showGPU = false;
        showDisk = false;
        showNET = false;
        AnyShowen = false;
        Selected = -1;
        Index = 0;
    }

    void OnGUI()
    {
        Customize.cust.windowx[windowID] = windowRect.x;
        Customize.cust.windowy[windowID] = windowRect.y;

        GUI.skin = com.Skin[GameControl.control.GUIID];

        //set up scaling
        //float rx = Screen.width / native_width;
        //float ry = Screen.height / native_height;

        //GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

        if (show == true)
        {
            GUI.color = com.colors[Customize.cust.WindowColorInt];
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
        }

        if (ShowContext == true)
        {
            ContextwindowRect.height = 21 * ContextMenuOptions.Count + 2;
            GUI.skin = com.Skin[GameControl.control.GUIID];
            GUI.color = com.colors[Customize.cust.WindowColorInt];
            ContextwindowRect = WindowClamp.ClampToScreen(GUI.Window(ContextMenuID, ContextwindowRect, DoMyContextWindow, ""));
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            if (!ContextwindowRect.Contains(Event.current.mousePosition))
            {
                ShowContext = false;
            }
        }
    }

    void DoMyWindow(int WindowID)
    {
        GUI.DragWindow(new Rect(1, 1, windowRect.width - 44, 21));

        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
            {
                this.enabled = false;
            }
        }
        else
        {
            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
            GUI.contentColor = com.colors[Customize.cust.FontColorInt];
            if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]))
            {
                this.enabled = false;
            }
        }

        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        if (MiniButton.Contains(Event.current.mousePosition))
        {
            if (CompactMode == true)
            {
                if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
                {
                    minimize = !minimize;
                    Minimize();
                }
            }
            else
            {
                if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
                {
                    minimize = !minimize;
                    Minimize();
                }
            }
        }
        else
        {
            if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
            {
                minimize = !minimize;
                Minimize();
            }
        }

        GUI.Box(new Rect(1, 1, windowRect.width - 44, 21), "Device Manager" + Title);

        GUI.contentColor = Color.white;

        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        MenuSystem();

        if (showCPU == true)
        {
            RenderCPUTab();
        }

        if (showGPU == true)
        {
            RenderGPUTab();
        }

        if (showRAM == true)
        {
            RenderRAMTab();
        }

        if (showPSU == true)
        {
            RenderPSUTab();
        }

        if (showDisk == true)
        {
            RenderDisksTab();
        }

        if (showPerformance == true)
        {
            GUI.Button(new Rect(2, 50, 40, 40), Pics[1]);
            GUI.Button(new Rect(2, 100, 40, 40), Pics[2]);
            //GUI.Button (new Rect (2, 50, 60, 20),"Memory");
            //GUI.Button (new Rect (2, 50, 60, 20),"GPU");
            //GUI.Button (new Rect (2, 50, 60, 20),"Disk");
            //GUI.Button (new Rect (2, 50, 60, 20),"Network");

            //			scrollpos = GUI.BeginScrollView(new Rect(2, 71, 500, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
            //			for (scrollsize = 0; scrollsize < RunningApplications.Count; scrollsize++)
            //			{
            //				//GUI.Button (new Rect (2, scrollsize * 20, 20, 20),"" + scrollsize);
            //				GUI.Button (new Rect (0, scrollsize * 20, 150, 20), RunningApplications [scrollsize]);
            //				GUI.Button (new Rect (151, scrollsize * 20, 60, 20),"" + CPU [scrollsize]);
            //				GUI.Button (new Rect (212, scrollsize * 20, 60, 20),"" + Memory [scrollsize]);
            //				GUI.Button (new Rect (273, scrollsize * 20, 60, 20),"" + Graphics [scrollsize]);
            //				GUI.Button (new Rect (334, scrollsize * 20, 60, 20),"" + Disk [scrollsize]);
            //				GUI.Button (new Rect (395, scrollsize * 20, 60, 20),"" + Network [scrollsize]);
            //			}
            //			GUI.EndScrollView();
        }

        if (showSettings == true)
        {
            GUI.Label(new Rect(1, 170, 200, 21), hint);

            if (CompactMode)
            {
                if (GUI.Button(new Rect(1, 44 + 2, 150, 20), "Normal Mode"))
                {
                    CompactMode = false;
                    showSettings = false;
                    minimize = false;
                    Minimize();
                }
            }
            else
            {
                if (GUI.Button(new Rect(1, 44 + 2, 150, 20), "Compact Mode"))
                {
                    CompactMode = true;
                    showSpecs = true;
                    showSettings = false;
                    minimize = true;
                    Minimize();
                }
            }
        }

        if (ChangeLayout == true)
        {
            if (AnyShowen == false)
            {
                if (GUI.Button(new Rect(2, 24, 75, 21), "Specs"))
                {
                    showSettings = false;
                    showSpecs = true;
                }
            }
            if (AnyShowen == true)
            {
                if (GUI.Button(new Rect(2, 24, 74, 21), "Back"))
                {
                    CloseAllMainMenuSubMenus();
                }
            }
        }
    }

    void RenderCPUTab()
    {
        GUI.Box(new Rect(1, 44, 63, 22), "Name");

        GUI.Box(new Rect(1, 67, 63, 22), "Socket");

        GUI.Box(new Rect(149, 67, 65, 22), "Voltage");

        GUI.Box(new Rect(65, 44, 200, 22), GameControl.control.Gateway.InstalledCPU[SelectedDevice].Name);

        GUI.Box(new Rect(65, 67, 50, 22), GameControl.control.Gateway.InstalledCPU[SelectedDevice].Socket);

        GUI.Box(new Rect(215, 67, 50, 22), "" + GameControl.control.Gateway.InstalledCPU[SelectedDevice].Voltage.ToString("F2"));


        GUI.Box(new Rect(1,100,60,21), "Cores");

        GUI.Box(new Rect(62,100,50,21),"" + GameControl.control.Gateway.InstalledCPU[SelectedDevice].Cores);

        GUI.Box(new Rect(113, 122, 100, 21), "Speed");

        GUI.Box(new Rect(214, 122, 50, 21), "" + GameControl.control.Gateway.InstalledCPU[SelectedDevice].CurrentSpeed.ToString("F3") + GameControl.control.SpaceName + "s");

        GUI.Box(new Rect(1, 122, 60, 21), "Usage");

        GUI.Box(new Rect(62,122,50,21), "" + GameControl.control.Gateway.InstalledCPU[SelectedDevice].UsagePercent.ToString("F2"));

        GUI.Box(new Rect(113,144,100,21), "Power Draw");

        GUI.Box(new Rect(214, 144, 50, 21), "" + GameControl.control.Gateway.InstalledCPU[SelectedDevice].PowerDraw.ToString("F2"));

        GUI.Box(new Rect(1, 166, 60, 21), "Status");

        if (GameControl.control.Gateway.InstalledCPU[SelectedDevice].HealthPercentage < 25)
        {
            GUI.backgroundColor = Color.red;
            GUI.contentColor = Color.white;
        }
        if (GameControl.control.Gateway.InstalledCPU[SelectedDevice].HealthPercentage > 25)
        {
            GUI.backgroundColor = Color.yellow;
            GUI.contentColor = Color.grey;
        }
        if (GameControl.control.Gateway.InstalledCPU[SelectedDevice].HealthPercentage > 50)
        {
            GUI.backgroundColor = Color.green;
            GUI.contentColor = Color.grey;
        }

        GUI.Box(new Rect(62, 166, 100, 21), GameControl.control.Gateway.InstalledCPU[SelectedDevice].Status);

        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
        GUI.contentColor = com.colors[Customize.cust.FontColorInt];



        //GUI.Box(new Rect(203, 44, 50, 22), "P-Usage");

        //GUI.Box(new Rect(254, 44, 50, 22), "Health");

        //GUI.Box(new Rect(203, 67 + 22, 50, 22), "" + GameControl.control.Gateway.InstalledCPU[SelectedDevice].PowerDraw);

        //GUI.Box(new Rect(254, 67 + 22, 50, 22), "%" + GameControl.control.Gateway.InstalledCPU[SelectedDevice].HealthPercentage.ToString("F0"));
    }

    void RenderGPUTab()
    {

    }

    void RenderRAMTab()
    {

    }

    void RenderDisksTab()
    {

    }

    void RenderPSUTab()
    {

    }

    void AddContextOptions()
    {
        //if (GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Exe)
        //{
        //	ContextMenuOptions.Add ("Open");
        //	//ContextMenuOptions.Add ("Copy");
        //	ContextMenuOptions.Add ("Rename");
        //	if (!GameControl.control.QuickLaunchNames.Contains (GameControl.control.DesktopIconList [SelectedProgram].Name)) 
        //	{
        //		ContextMenuOptions.Add ("Pin to QL");
        //	}
        //	else
        //	{
        //		ContextMenuOptions.Add ("Unpin from QL");
        //	}
        //	ContextMenuOptions.Add ("Delete");
        //	//ContextMenuOptions.Add ("Create Icon");
        //	ContextMenuOptions.Add ("Details");
        //}
        //else if (GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Dir || GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Fdl)
        //{
        //	ContextMenuOptions.Add ("Open");
        //	//ContextMenuOptions.Add ("Create Icon");
        //	ContextMenuOptions.Add ("Delete");
        //	ContextMenuOptions.Add ("Details");
        //}
        //else 
        //{
        //	ContextMenuOptions.Add ("Open");
        //	//ContextMenuOptions.Add ("Copy");
        //	ContextMenuOptions.Add ("Rename");
        //	ContextMenuOptions.Add ("Delete");
        //	//ContextMenuOptions.Add ("Create Icon");
        //	ContextMenuOptions.Add ("Details");
        //}
        ContextMenuOptions.Add("Kill");
        //ContextMenuOptions.Add ("Copy");
        ContextMenuOptions.Add("Location");
        //ContextMenuOptions.Add ("Create Icon");
        ContextMenuOptions.Add("Details");
    }

    void PlayClickSound()
    {
        sc.SoundSelect = 3;
        sc.PlaySound();
    }

    void DoMyContextWindow(int WindowID)
    {
        //GUI.Box (new Rect (Input.mousePosition.x, Input.mousePosition.y, 100, 200), "");
        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        if (ContextMenuOptions.Count <= 0)
        {
            AddContextOptions();
        }

        if (ContextMenuOptions.Count > 0)
        {
            for (int i = 0; i < ContextMenuOptions.Count; i++)
            {
                if (GUI.Button(new Rect(1, 1 + 21 * i, ContextwindowRect.width - 2, 21), ContextMenuOptions[i]))
                {
                    SelectedOption = ContextMenuOptions[i];
                }
            }
        }

        switch (SelectedOption)
        {
            case "Kill":
                switch (SelectedTask.Type)
                {
                    case ProgramSystem.ProgramType.Txt:
                        //EndProgram();
                        CloseContextMenu();
                        break;
                    case ProgramSystem.ProgramType.Fdl:
                        //OpenFld();
                        CloseContextMenu();
                        break;
                    case ProgramSystem.ProgramType.Ins:
                        //OpenIns();
                        CloseContextMenu();
                        break;
                    case ProgramSystem.ProgramType.Exe:
                        EndProgram();
                        CloseContextMenu();
                        break;
                    case ProgramSystem.ProgramType.Dir:
                        //OpenFld();
                        CloseContextMenu();
                        break;
                }
                break;
            case "Location":
                ProgramLocation();
                PlayClickSound();
                CloseContextMenu();
                break;
            case "Details":
                //DeleteSystem();
                PlayClickSound();
                CloseContextMenu();
                break;
        }
    }

    void ProgramLocation()
    {
        PlayClickSound();
        if (com.show == true)
        {
            com.ComAddress = SelectedTask.Location;
        }
        else
        {
            appman.SelectedApp = "Computer";
            com.ComAddress = SelectedTask.Location;
        }
        //		SelectedProgram = -1;
        //		com.MenuSelected = 0;
    }

    void EndProgram()
    {
        PlayClickSound();
        appman.SelectedApp = SelectedTask.Target;
        SelectedProgram = -1;
        com.MenuSelected = 0;
    }


    void CloseContextMenu()
    {
        ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
        SelectedOption = "";
        ShowContext = false;
    }

    void MenuSystem()
    {
        if (ChangeLayout == false)
        {
            if (GUI.Button(new Rect(1, 23, 50, 20), "CPU"))
            {
                Title = " - CPU";
                CloseAllMainMenus();
                showCPU = true;
            }

            if (GUI.Button(new Rect(52, 23, 50, 20), "GPU"))
            {
                Title = " - GPU";
                CloseAllMainMenus();
                showGPU = true;
            }

            if (GUI.Button(new Rect(103, 23, 50, 20), "RAM"))
            {
                Title = " - RAM";
                CloseAllMainMenus();
                showRAM = true;
            }

            if (GUI.Button(new Rect(154, 23, 60, 20), "Disks"))
            {
                Title = " - Storage Devices";
                CloseAllMainMenus();
                showDisk = true;
            }

            if (GUI.Button(new Rect(215, 23, 50, 20), "PSU"))
            {
                Title = " - Power Supply";
                CloseAllMainMenus();
                showPSU = true;
            }
        }
    }
}