using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VolumeController : MonoBehaviour
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

    public Rect VolumeName;
    public Rect VolumeBar;
    public Rect VolumeText;

    public Rect VolumeName1;
    public Rect VolumeBar1;
    public Rect VolumeText1;

    public Rect VolumeName2;
    public Rect VolumeBar2;
    public Rect VolumeText2;

    public Rect VolumeName3;
    public Rect VolumeBar3;
    public Rect VolumeText3;

    private Desktop desk;


    // Use this for initialization

    void Start()
    {
        Hardware = GameObject.Find("Hardware");
        Puter = GameObject.Find("System");

        com = Puter.GetComponent<Computer>();

        desk = Puter.GetComponent<Desktop>();

        if (Game == true)
        {
            native_height = Customize.cust.native_height;
            native_width = Customize.cust.native_width;
        }

        windowRect.width = 300;
        windowRect.height = 150;

        VolumeBar = new Rect(10, 48, 100, 22);
        VolumeName = new Rect(VolumeBar.x, VolumeBar.y-18, 100, 100);
        VolumeText = new Rect(VolumeBar.width + 12, VolumeBar.y-5, 100, 22);

        VolumeBar1 = new Rect(10, 75, 100, 22);
        VolumeName1 = new Rect(VolumeBar1.x, VolumeBar1.y-18, 100, 100);
        VolumeText1 = new Rect(VolumeBar1.width + 12, VolumeBar1.y - 5, 100, 22);

        VolumeBar2 = new Rect(10, 102, 100, 22);
        VolumeName2 = new Rect(VolumeBar2.x, VolumeBar2.y-18, 100, 100);
        VolumeText2 = new Rect(VolumeBar2.width + 12, VolumeBar2.y - 5, 100, 22);

        VolumeBar3 = new Rect(10, 129, 100, 22);
        VolumeName3 = new Rect(VolumeBar3.x, VolumeBar3.y - 18, 100, 100);
        VolumeText3 = new Rect(VolumeBar3.width + 12, VolumeBar3.y - 5, 100, 22);

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

        GUI.DragWindow(new Rect(2, 2, CloseButton.x - 3, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x - 3, 21), "Audio Settings");

        GUI.Label(new Rect(VolumeName), "Generic Sound");
        float TempVol = Customize.cust.Volume * 100;
        Customize.cust.Volume = GUI.HorizontalSlider(new Rect(VolumeBar), Customize.cust.Volume, 0, 1);
        GUI.Label(new Rect(VolumeText), "% " + TempVol.ToString("F0"));


        GUI.Label(new Rect(VolumeName1), "SoundTrack");
        float TempVol1 = Customize.cust.SoundtrackVolume * 100;
        Customize.cust.SoundtrackVolume = GUI.HorizontalSlider(new Rect(VolumeBar1), Customize.cust.SoundtrackVolume, 0, 1);
        GUI.Label(new Rect(VolumeText1),"% " + TempVol1.ToString("F0"));


        GUI.Label(new Rect(VolumeName2), "Notification");
        float TempVol2 = Customize.cust.NotiVolume * 100;
        Customize.cust.NotiVolume = GUI.HorizontalSlider(new Rect(VolumeBar2), Customize.cust.NotiVolume, 0, 1);
        GUI.Label(new Rect(VolumeText2), "% " + TempVol2.ToString("F0"));

        GUI.Label(new Rect(VolumeName3), "Trace Tracker");
        float TempVol3 = Customize.cust.TraceBeepsVolume * 100;
        Customize.cust.TraceBeepsVolume = GUI.HorizontalSlider(new Rect(VolumeBar3), Customize.cust.TraceBeepsVolume, 0, 1);
        GUI.Label(new Rect(VolumeText3), "% " + TempVol3.ToString("F0"));

        //if (Customize.cust.EnableSoundTrack == true)
        //{
        //    if (GUI.Button(new Rect(5, 100, 20, 20), desk.SpeakerIconArray[3]))
        //    {
        //        Customize.cust.EnableSoundTrack = false;
        //    }
        //}
        //else
        //{
        //    if (GUI.Button(new Rect(5, 100, 20, 20), desk.SpeakerIconArray[0]))
        //    {
        //        Customize.cust.EnableSoundTrack = true;
        //    }
        //}
    }
}
