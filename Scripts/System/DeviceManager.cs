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

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;

    public string Title;

    private GameObject Hardware;
    private CPU cpu;
    private RAM ram;
    private PSU psu;
    private HardDrives hdd;
    private Networks net;

    public int Index;

    private SoundControl sc;
    private AppMan appman;

    public int Selected;

    public int SelectedDevice;

    public Rect Test = new Rect(0, 0, 0, 0);
    public Rect TestL = new Rect(0, 0, 0, 0);

    public Menu SelectedMenu;


    public enum Menu
    {
        CPU,
        RAM,
        PSU,
        GPU,
        Disk,
        NET,
        Motherboard,
        Home
    }

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

        windowRect.width = 400;

        CloseButton = new Rect(windowRect.width - 22, 1, 21, 21);
        MiniButton = new Rect(windowRect.width - 43, 1, 21, 21);
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
        CloseAllMainMenuSubMenus();
    }

    void CloseAllMainMenuSubMenus()
    {
        SelectedMenu = Menu.Home;
        Selected = -1;
        Index = 0;
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
        GUI.DragWindow(new Rect(1, 1, windowRect.width - 44, 21));

        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
            {
                appman.SelectedApp = "Device Manager";
            }
        }
        else
        {
            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
            GUI.contentColor = com.colors[Customize.cust.FontColorInt];
            GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
        }

        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        if (MiniButton.Contains(Event.current.mousePosition))
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

        GUI.Box(new Rect(1, 1, windowRect.width - 44, 21), "Device Manager" + Title);

        GUI.contentColor = Color.white;

        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        MenuSystem();

        switch(SelectedMenu)
        {
            case Menu.CPU:
                RenderCPUTab();
                break;
            case Menu.GPU:
                RenderGPUTab();
                break;
            case Menu.RAM:
                RenderRAMTab();
                break;
            case Menu.PSU:
                RenderPSUTab();
                break;
            case Menu.Disk:
                RenderDisksTab();
                break;
            case Menu.Motherboard:
                RenderMotherboardTab();
                break;
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

    void RenderMotherboardTab()
    {

    }

    void PlayClickSound()
    {
        sc.SoundSelect = 3;
        sc.PlaySound();
    }

    void MenuSystem()
    {
        if (GUI.Button(new Rect(1, 23, 50, 20), "CPU"))
        {
            Title = " - CPU";
            CloseAllMainMenus();
            SelectedMenu = Menu.CPU;
        }

        if (GUI.Button(new Rect(52, 23, 50, 20), "GPU"))
        {
            Title = " - GPU";
            CloseAllMainMenus();
            SelectedMenu = Menu.GPU;
        }

        if (GUI.Button(new Rect(103, 23, 50, 20), "RAM"))
        {
            Title = " - RAM";
            CloseAllMainMenus();
            SelectedMenu = Menu.RAM;
        }

        if (GUI.Button(new Rect(154, 23, 50, 20), "PSU"))
        {
            Title = " - Power Supply";
            CloseAllMainMenus();
            SelectedMenu = Menu.PSU;
        }

        if (GUI.Button(new Rect(205, 23, 60, 20), "Disks"))
        {
            Title = " - Storage Devices";
            CloseAllMainMenus();
            SelectedMenu = Menu.Disk;
        }

        if (GUI.Button(new Rect(266, 23, 90, 20), "Motherboard"))
        {
            Title = " - Motherboard";
            CloseAllMainMenus();
            SelectedMenu = Menu.Motherboard;
        }
    }
}