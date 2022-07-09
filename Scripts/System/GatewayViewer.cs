using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GatewayViewer : MonoBehaviour
{
    private GameObject Hardware;
    private GameObject Puter;
    public AudioSource AS;
    public Rect windowRect;
    public float native_width = 1920;
    public float native_height = 1080;
    public int windowID;

    public Texture2D sdIcon;
    public Texture2D restartIcon;
    public Texture2D logIcon;
    public bool show;

    public bool Game;

    public bool shutdown;
    public bool restart;

    public GUISkin Skin;

    public Rect CloseButton;

    private Clock clk;
    private CD cd;
    private Boot boot;
    private DesktopEnviroment os;
    private Defalt def;
    private Computer com;
    private SoundControl sc;
    private HardwareCFile hcf;
    int DesktopStyle = 3;

    private AppMan appman;

    public Rect Gateway;
    public Texture2D GatewayPic;
    public List<Rect> CPU = new List<Rect>();
    public List<Texture2D> CPUPic = new List<Texture2D>();

 //   public List<StorageSlotSystem> StorageSockets = new List<StorageSlotSystem>();
    public List<Rect> Storage = new List<Rect>();
    public List<Texture2D> StoragePic = new List<Texture2D>();

    public List<SocketSystem> MemorySockets = new List<SocketSystem>();
    public List<Rect> Memory = new List<Rect>();
    public List<Texture2D> MemoryPic = new List<Texture2D>();


    // Use this for initialization

    void Start()
    {
        Hardware = GameObject.Find("Hardware");
        Puter = GameObject.Find("System");
        cd = GetComponent<CD>();
        com = Puter.GetComponent<Computer>();
        clk = Puter.GetComponent<Clock>();
        boot = Puter.GetComponent<Boot>();
        os = Puter.GetComponent<DesktopEnviroment>();
        sc = Puter.GetComponent<SoundControl>();
        def = Puter.GetComponent<Defalt>();
        hcf = Hardware.GetComponent<HardwareCFile>();
        appman = Puter.GetComponent<AppMan>();

        if (Game == true)
        {
            native_height = Customize.cust.native_height;
            native_width = Customize.cust.native_width;
        }

        AfterStart();

        //MotherBoard2 CPU Socket = 107Y
    }

    void AfterStart()
    {
        windowRect.x = Customize.cust.windowx[windowID];
        windowRect.y = Customize.cust.windowy[windowID];
    }

    public void UpdateDevicePOS()
    {
        var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");
        for (int i = 0; i < person.Gateway.CPU.Count;i++)
        {
            person.Gateway.CPU[i].UIPosX = person.Gateway.Motherboard.CPUSockets[i].POSX;
            person.Gateway.CPU[i].UIPosY = person.Gateway.Motherboard.CPUSockets[i].POSY;
        }
        for (int i = 0; i < person.Gateway.StorageDevices.Count; i++)
        {
            person.Gateway.StorageDevices[i].POSX = person.Gateway.Motherboard.StorageSlots[i].POSX;
            person.Gateway.StorageDevices[i].POSY = person.Gateway.Motherboard.StorageSlots[i].POSY;
        }
    }

    public void UpdatePos()
    {
        UpdateDevicePOS();

        MotherboardCheck();

        CPUCheck();

        StorageCheck();

        MemoryCheck();

        if (GatewayPic != null)
        {
            Gateway.x = 2;
            Gateway.y = 25;
            Gateway.width = GatewayPic.width;
            Gateway.height = GatewayPic.height;

            windowRect.width = Gateway.width * 1.07f;
            windowRect.height = Gateway.height * 1.15f;

            CloseButton = new Rect(windowRect.width - 22, 2, 21, 21);
        }
    }

    void MotherboardCheck()
    {
        for (int j = 0; j < hcf.ListOfMotherboardImages.Count; j++)
        {
            if (GameControl.control.Gateway.SelectedMotherboardImage == j)
            {
                GatewayPic = hcf.ListOfMotherboardImages[j];
            }
        }
    }

    void CPUCheck()
    {
        var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

        var PlayerCPU = person.Gateway.CPU;

        CPU.RemoveRange(0, CPU.Count);
        CPUPic.RemoveRange(0, CPUPic.Count);

        for (int i = 0; i < PlayerCPU.Count; i++)
        {
            for (int j = 0; j < hcf.ListOfCPUImages.Count; j++)
            {
                if (PlayerCPU[i].SelectedCPUImage == j)
                {
                    CPUPic.Add(hcf.ListOfCPUImages[j]);
                    CPU.Add(new Rect(PlayerCPU[i].UIPosX, PlayerCPU[i].UIPosY, CPUPic[i].width, CPUPic[i].height));
                }
            }
        }
    }

    void StorageCheck()
    {
        var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

        var PlayerStorageDevice = person.Gateway.StorageDevices;

//        StorageSockets.RemoveRange(0, StorageSockets.Count);
        Storage.RemoveRange(0, Storage.Count);
        StoragePic.RemoveRange(0, StoragePic.Count);

        for (int i = 0; i < PlayerStorageDevice.Count; i++)
        {
 //           StorageSockets.Add(person.Gateway.Motherboard.StorageSlots[i]);

            for (int j = 0; j < hcf.ListOfStorageImages.Count; j++)
            {
                if (PlayerStorageDevice[i].SelectedImageNumber == j)
                {
                    StoragePic.Add(hcf.ListOfStorageImages[j]);
                    Storage.Add(new Rect(PlayerStorageDevice[i].POSX, PlayerStorageDevice[i].POSY, StoragePic[i].width, StoragePic[i].height));
                }
            }
        }
    }

    void MemoryCheck()
    {
        var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

        var PlayerRAM = person.Gateway.RAM;

        Memory.RemoveRange(0, Memory.Count);
        MemoryPic.RemoveRange(0, MemoryPic.Count);

        for (int i = 0; i < PlayerRAM.Count; i++)
        {

            for (int j = 0; j < hcf.ListOfMemoryImages.Count; j++)
            {
                if (PlayerRAM[i].SelectedImage == j)
                {
                    MemoryPic.Add(hcf.ListOfMemoryImages[j]);
                    Memory.Add(new Rect(PlayerRAM[i].PosX, PlayerRAM[i].PosY, MemoryPic[i].width, MemoryPic[i].height));
                }
            }
        }
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
        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
            {
                appman.SelectedApp = "Gateway Viewer";
            }
        }
        else
        {
            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
            GUI.contentColor = com.colors[Customize.cust.FontColorInt];
            GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
        }

        Render();
    }

    void Render()
    {
        var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

        var PlayerStorageDevice = person.Gateway.StorageDevices;
        var PlayerCPU = person.Gateway.CPU;

        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        UpdatePos();

        GUI.DragWindow(new Rect(2, 2, CloseButton.x - 3, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x - 3, 21), "Gateway");

        GUI.DrawTexture(new Rect(Gateway), GatewayPic);

        if(PlayerCPU.Count > 0)
        {
            for (int i = 0; i < PlayerCPU.Count; i++)
            {
                GUI.DrawTexture(new Rect(CPU[i]), CPUPic[i]);
            }
        }

        if (PlayerStorageDevice.Count > 0)
        {
            for (int i = 0; i < PlayerStorageDevice.Count; i++)
            {
                GUI.DrawTexture(new Rect(Storage[i]), StoragePic[i]);
            }
        }

        if (MemorySockets.Count > 0)
        {
            for (int i = 0; i < MemorySockets.Count; i++)
            {
                GUI.DrawTexture(new Rect(Memory[i]), MemoryPic[i]);
            }
        }
    }
}
