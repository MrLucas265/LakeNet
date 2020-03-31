using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private OS os;
    private Defalt def;
    private Computer com;
    private SoundControl sc;
    private HardwareCFile hcf;
    int DesktopStyle = 3;

    public Rect Gateway;
    public Texture2D GatewayPic;
    public List<SocketSystem> CPUSockets = new List<SocketSystem>();
    public List<Rect> CPU = new List<Rect>();
    public List<Texture2D> CPUPic = new List<Texture2D>();

    public List<SocketSystem> StorageSockets = new List<SocketSystem>();
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
        os = Puter.GetComponent<OS>();
        sc = Puter.GetComponent<SoundControl>();
        def = Puter.GetComponent<Defalt>();
        hcf = Hardware.GetComponent<HardwareCFile>();
        if (Game == true)
        {
            native_height = Customize.cust.native_height;
            native_width = Customize.cust.native_width;
        }

        AfterStart();
    }

    void AfterStart()
    {
        windowRect.x = Customize.cust.windowx[windowID];
        windowRect.y = Customize.cust.windowy[windowID];
    }

    public void UpdatePos()
    {
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
        CPUSockets.RemoveRange(0, CPUSockets.Count);
        CPU.RemoveRange(0, CPU.Count);
        CPUPic.RemoveRange(0, CPUPic.Count);

        for (int i = 0; i < GameControl.control.Gateway.CPUSockets.Count; i++)
        {
            CPUSockets.Add(GameControl.control.Gateway.CPUSockets[i]);

            for (int j = 0; j < hcf.ListOfCPUImages.Count; j++)
            {
                if (GameControl.control.Gateway.CPUSockets[i].SelectedImageNumber == j)
                {
                    CPUPic.Add(hcf.ListOfCPUImages[j]);
                    CPU.Add(new Rect(CPUSockets[i].POSX, CPUSockets[i].POSY, CPUPic[i].width, CPUPic[i].height));
                }
            }
        }
    }

    void StorageCheck()
    {
        StorageSockets.RemoveRange(0, StorageSockets.Count);
        Storage.RemoveRange(0, Storage.Count);
        StoragePic.RemoveRange(0, StoragePic.Count);

        for (int i = 0; i < GameControl.control.Gateway.StorageSlots.Count; i++)
        {
            StorageSockets.Add(GameControl.control.Gateway.StorageSlots[i]);

            for (int j = 0; j < hcf.ListOfStorageImages.Count; j++)
            {
                if (GameControl.control.Gateway.StorageSlots[i].SelectedImageNumber == j)
                {
                    StoragePic.Add(hcf.ListOfStorageImages[j]);
                    Storage.Add(new Rect(StorageSockets[i].POSX, StorageSockets[i].POSY, StoragePic[i].width, StoragePic[i].height));
                }
            }
        }
    }

    void MemoryCheck()
    {
        MemorySockets.RemoveRange(0, MemorySockets.Count);
        Memory.RemoveRange(0, Memory.Count);
        MemoryPic.RemoveRange(0, MemoryPic.Count);

        for (int i = 0; i < GameControl.control.Gateway.MemorySlots.Count; i++)
        {
            MemorySockets.Add(GameControl.control.Gateway.MemorySlots[i]);

            for (int j = 0; j < hcf.ListOfMemoryImages.Count; j++)
            {
                if (GameControl.control.Gateway.MemorySlots[i].SelectedImageNumber == j)
                {
                    MemoryPic.Add(hcf.ListOfMemoryImages[j]);
                    Memory.Add(new Rect(MemorySockets[i].POSX, MemorySockets[i].POSY, MemoryPic[i].width, MemoryPic[i].height));
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

        Render();
    }

    void Render()
    {
        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        UpdatePos();

        GUI.DragWindow(new Rect(2, 2, CloseButton.x - 3, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x - 3, 21), "Gateway");

        GUI.DrawTexture(new Rect(Gateway), GatewayPic);

        if(CPUSockets.Count > 0)
        {
            for (int i = 0; i < CPUSockets.Count; i++)
            {
                GUI.DrawTexture(new Rect(CPU[i]), CPUPic[i]);
            }
        }

        if (StorageSockets.Count > 0)
        {
            for (int i = 0; i < StorageSockets.Count; i++)
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
