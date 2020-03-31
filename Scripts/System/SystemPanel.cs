using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

public class SystemPanel : MonoBehaviour 
{
	private GameObject Prompt;
	private GameObject Computer;
	private GameObject System;

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;
	private Computer com;
	private Defalt def;
	public bool show;

	public string ComAddress;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;
	public int scrollsize1;
	public int Res;
	public int Select;
	public bool FullScreen;
	public string Hint;
	public bool showPics;
	public bool showName;
	public bool showPass;
	public string TempName;
	public string TempPass;

	private ErrorProm ep;
	private CustomTheme ct;
	private OS os;
	private Desktop desk;
	private Mouse mouse;
    private AppMan appman;
	private ScreenSaver ss;
    private SoundControl sc;

	public string SkinName;
	public string SSTimeString;
    public string NotiSoundString;

	public float w;
	public float h;

	public string CatName;

	public List<string> Rez = new List<string>();

	public List<string> ShortDate = new List<string>();
	public List<string> LongDate = new List<string>();
	public List<string> Time = new List<string>();

	public List<Texture2D> Textures = new List<Texture2D>();

	public bool showShortDate;
	public bool showLongDate;
	public bool showTime;

	public string Status;

	public List<Texture2D> BackgroundPics = new List<Texture2D>();
	public int BgSelect;

	public int QuickLaunchSize;

	public string red;
	public string green;
	public string blue;
	public string alpha;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	public bool minimize;

	int DesktopStyle = 3;
	public float Scale;

	public List<ProgramSystem> SelectablePrograms = new List<ProgramSystem>();
	public List<string> ProgramName = new List<string>();
	public bool SelectableProgramsScan;
	public int ScanCount;
	public int Selecting;

	public bool UIV2;

	public List<string> CommandNames = new List<string>();

	public List<AudioClip> clips = new List<AudioClip>();
	public string path = "";

	public string LocalScreenSaverMenu;


	enum Menu
	{
		Home,
		WebBrowser,
		Commands,
		Soundtrack,
        Download,
        Account,
		Mouse,
		Clock,
		QuickLaunch,
		Scaling,
		Font,
		Theme,
		ScreenSaver,
		Color,
		Settings,
		Notification,
		Display,
		FontColor,
		WindowColor,
		ButtonColor,
		Background,
		BackgroundSettings,
		Dev,
	}

	Menu SelectedMenu;

	// Use this for initialization
	void Start () 
	{
		Prompt = GameObject.Find("Prompts");
		System = GameObject.Find("System");

		ComAddress = "S:/Settings";
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		ep = Prompt.GetComponent<ErrorProm>();
		def = System.GetComponent<Defalt>();
		com = System.GetComponent<Computer>();
		os = System.GetComponent<OS>();
		desk = System.GetComponent<Desktop>();
		ct = System.GetComponent<CustomTheme>();
		mouse = System.GetComponent<Mouse>();
        appman = System.GetComponent<AppMan>();
		ss = System.GetComponent<ScreenSaver>();
        sc = System.GetComponent<SoundControl>();

        TempName = "";
		TempPass = "";
		UpdateRezList ();
		AddTime ();
		SetPos();

		Scale = Customize.cust.UIScale;

		windowRect.x = Screen.width-windowRect.width-1;
		windowRect.y = 50;
	}

	void SetPos()
	{
		CloseButton = new Rect (277,2,21,20);
		MiniButton = new Rect (255,2,21,20);
		DefaltSetting = new Rect (0,2,300,205);
	}

	void UpdateRezList()
	{
		Rez.Add ("640x480");
		Rez.Add ("800x600");
		Rez.Add ("1024x768");
		Rez.Add ("1152x864");
		Rez.Add ("1280x720");
		Rez.Add ("1280x768");
		Rez.Add ("1366x768");
		Rez.Add ("1600x900");
		Rez.Add ("1680x1050");
		Rez.Add ("1920x1080");
		Rez.Add ("2560x1440");
	}

	void AddTime()
	{
		ShortDate.Add("d/m/yy");
		ShortDate.Add("d/mm/yy");
		ShortDate.Add("dd/m/yy");
		ShortDate.Add("dd/mm/yy");
		ShortDate.Add("d/m/yyyy");
		ShortDate.Add("dd/m/yyyy");
		ShortDate.Add("dd/mm/yyyy");

		LongDate.Add ("nd/dd/nm/yyyy");
		LongDate.Add ("dd/nm/yyyy");

		Time.Add("h/m");
		Time.Add("h/m/apm");
		Time.Add("hh/m");
		Time.Add("hh/m/apm");
		Time.Add("h/mm");
		Time.Add("h/mm/apm");
		Time.Add("hh/mm");
		Time.Add("hh/mm/apm");
	}

	public void SetFontColor()
	{
		Color32 Fontcolor;
		Fontcolor.r = (byte)Customize.cust.FontR;
		Fontcolor.g = (byte)Customize.cust.FontG;
		Fontcolor.b = (byte)Customize.cust.FontB;
		Fontcolor.a = (byte)Customize.cust.FontA;
		com.colors[1] = Fontcolor;
	}

	public void SetButtonColor()
	{
		Color32 ButtonColor;
		ButtonColor.r = (byte)Customize.cust.ButtonR;
		ButtonColor.g = (byte)Customize.cust.ButtonG;
		ButtonColor.b = (byte)Customize.cust.ButtonB;
		ButtonColor.a = (byte)Customize.cust.ButtonA;
		com.colors[2] = ButtonColor;
	}

	public void SetWindowColor()
	{
		Color32 WindowColor;
		WindowColor.r = (byte)Customize.cust.WindowR;
		WindowColor.g = (byte)Customize.cust.WindowG;
		WindowColor.b = (byte)Customize.cust.WindowB;
		WindowColor.a = (byte)Customize.cust.WindowA;
		com.colors[3] = WindowColor;
	}
	
	void Update()
	{
		w = GameControl.control.wh;
		h = GameControl.control.wh;

		if (GameControl.control.wh <= 0)
		{
			GameControl.control.wh = 1;
		}

		if (Customize.cust.AA > 8) 
		{
			Customize.cust.AA = 8;
		}
	}

	public void RezSelect()
	{

		switch(Customize.cust.RezSelect) 
		{
		case 0:
			Customize.cust.RezX = 640;
			Customize.cust.RezY = 480;
			break;

		case 1:
			Customize.cust.RezX = 800;
			Customize.cust.RezY = 600;
			break;

		case 2:
			Customize.cust.RezX = 1024;
			Customize.cust.RezY = 768;
			break;

		case 3:
			Customize.cust.RezX = 1152;
			Customize.cust.RezY = 864;
			break;

		case 4:
			Customize.cust.RezX = 1280;
			Customize.cust.RezY = 720;
			break;

		case 5:
			Customize.cust.RezX = 1280;
			Customize.cust.RezY = 768;
			break;

		case 6:
			Customize.cust.RezX = 1366;
			Customize.cust.RezY = 768;
			break;

		case 7:
			Customize.cust.RezX = 1600;
			Customize.cust.RezY = 900;
			break;

		case 8:
			Customize.cust.RezX = 1680;
			Customize.cust.RezY = 1050;
			break;

		case 9:
			Customize.cust.RezX = 1920;
			Customize.cust.RezY = 1080;
			break;
		case 10:
			Customize.cust.RezX = 2560;
			Customize.cust.RezY = 1440;
			break;
		case 11:
			Screen.SetResolution(640, 480, Customize.cust.FullScreen);
			Customize.cust.RezX = 640;
			Customize.cust.RezY = 480;
			break;
		}
	}

	void Minimize()
	{
		if (minimize == true) 
		{
			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,25));
		}
		else
		{
			//private Rect DefaltSetting = new Rect (windowRect.x,windowRect.y,300,205);
			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,DefaltSetting.height));
		}
	}

	void OnGUI()
	{
		GUI.skin = com.Skin[GameControl.control.GUIID];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	public void ApplyBackgrounds()
	{
		Customize.cust.native_width = os.native_width;
		Customize.cust.native_height = os.native_height;
		Customize.cust.Save();
		if (Customize.cust.CustomTexFileNames [4] != "") {
			ct.enabled = true;
			ct.Once = false;
			ct.UpdatePics ();
			os.pic [2] = ct.tex1 [4];
			Customize.cust.UseCustomBG = true;
			os.Index = 2;
		} 
		else
		{
			os.pic[2] = BackgroundPics[Customize.cust.SelectedBackground];
			Customize.cust.UseCustomBG = true;
			os.Index = 2;
		}
	}

	public void ApplyMouseImage()
	{
		Customize.cust.Save();
		if (Customize.cust.CustomTexFileNames [3] != "")
		{
			ct.enabled = true;
			ct.Once = false;
			ct.UpdatePics ();
			mouse.cursorImage = ct.tex1 [3];
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
                appman.SelectedApp = "System Panel";
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1]);
		}

		if (MiniButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		}

		RenderUI();
	}

	void RenderUI()
	{
		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];
		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting), "System Panel" + CatName);

		SystemPanelUI();

		if (SelectedMenu == Menu.Home)
		{
			DefaltBoxSetting = new Rect (2,2,252,20);
		} 
		else
		{
			DefaltBoxSetting = new Rect (23, 2, 231, 20);
		}

//		if (ComAddress == "S:/Settings/Themes/Custom")
//		{
//			if(GUI.Button(new Rect(2,2,20,20),"<-"))
//			{
//				ComAddress = "S:/Settings/Themes";
//			}
//
//			scrollpos = GUI.BeginScrollView(new Rect(1, 60, 275, 60), scrollpos, new Rect(0, 0, 0, scrollsize*20));
//			for (scrollsize = 0; scrollsize < 4; scrollsize++) 
//			{
//				Customize.cust.CustomTexFileNames[scrollsize] = GUI.TextField(new Rect (5, scrollsize*20, 250, 20), Customize.cust.CustomTexFileNames[scrollsize]);
//			}
//
//			GUI.EndScrollView();
//
//			if(GUI.Button(new Rect(5, 175, 60, 20),"Apply"))
//			{
//				ct.enabled = true;
//				ct.Once = false;
//				ct.UpdatePics();
//			}
//		}
	}

	void SystemPanelUI()
	{
		switch (SelectedMenu) 
		{
		case Menu.Home:
			Home();
			break;
		case Menu.Commands:
			Commands();
			break;
		case Menu.Soundtrack:
			Soundtrack();
			break;
        case Menu.Download:
            DownloadLocation();
            break;
            case Menu.WebBrowser:
			WebBrowser();
			break;
		case Menu.Account:
			Account();
			break;
		case Menu.Mouse:
			MouseSettings();
			break;
		case Menu.Clock:
			Clock();
			break;
		case Menu.QuickLaunch:
			QuickLaunch();
			break;
		case Menu.Scaling:
			Scaling();
			break;
		case Menu.Font:
			Font();
			break;
		case Menu.Display:
			Display();
			break;
		case Menu.ScreenSaver:
			ScreenSaver();
			break;
		case Menu.Theme:
			Theme();
			break;
		case Menu.Dev:
			DevSettings();
			break;
		case Menu.Color:
			Colors();
			break;
		case Menu.Settings:
			ScreenUI();
			break;
		case Menu.WindowColor:
			WindowColorUI();
			break;
		case Menu.FontColor:
			FontColorUI();
			break;
		case Menu.ButtonColor:
			ButtonColorUI();
			break;
		case Menu.Background:
			Background();
			break;
		case Menu.BackgroundSettings:
			BackgroundSettings();
			break;
		case Menu.Notification:
			Notifications();
			break;

		}
	}

	void DevSettings()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
		}

		if(GUI.Button(new Rect(113,30,100,20),"Theme"))
		{
			SelectedMenu = Menu.Theme;
			CatName = " - Themes";
		}
	}

	void Home()
	{
		if(GUI.Button(new Rect(3,30,100,20),"Display"))
		{
			SelectedMenu = Menu.Display;
			CatName = " - Display";
		}

		if(GUI.Button(new Rect(123,30,100,20),"Commands"))
		{
			SelectedMenu = Menu.Commands;
			CatName = " - Commands";
		}

		if(GUI.Button(new Rect(123,60,100,20),"Soundtrack"))
		{
			SelectedMenu = Menu.Soundtrack;
			CatName = " - Soundtrack";
		}

        if (GUI.Button(new Rect(123, 90, 100, 20), "Download"))
        {
            SelectedMenu = Menu.Download;
            CatName = " - Download";
        }

		if (GUI.Button(new Rect(123, 180, 100, 20), "Dev Settings"))
		{
			SelectedMenu = Menu.Dev;
			CatName = " - Dev Settings";
		}

        //		if(GUI.Button(new Rect(123,60,100,20),"Web Browser"))
        //		{
        //			SelectedMenu = Menu.WebBrowser;
        //			CatName = " - Web Browser";
        //		}

        if (GUI.Button(new Rect(3,60,100,20),"Notfication"))
		{
			SelectedMenu = Menu.Notification;
			CatName = " - Notfications";
		}

		if(GUI.Button(new Rect(3,90,100,20),"Account"))
		{
			SelectedMenu = Menu.Account;
			CatName = " - Account";
		}

		if(GUI.Button(new Rect(3,120,100,20),"Mouse"))
		{
			SelectedMenu = Menu.Mouse;
			CatName = " - Mouse Settings";
		}

		if(GUI.Button(new Rect(3,150,100,20),"Web Browser"))
		{
			SelectedMenu = Menu.WebBrowser;
			CatName = " - Web Browser";
		}

//		if(GUI.Button(new Rect(3,150,100,20),"Clock"))
//		{
//			SelectedMenu = Menu.Clock;
//			CatName = " - Clock";
//		}

		if(GUI.Button(new Rect(3,180,100,20),"Quick Launch"))
		{
			SelectedMenu = Menu.QuickLaunch;
			CatName = " - Quick Launch";
			QuickLaunchScan();
		}
	}

	void WebBrowser()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = " - Home";
		}

		GUI.Label (new Rect (5, 40, 200, 22),"Web Browsers Home Page");
		Customize.cust.WebBrowserHomepage = GUI.TextField (new Rect (5, 60, 200, 22),"" + Customize.cust.WebBrowserHomepage);
	}

    void DownloadLocation()
    {
        if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
        {
            SelectedMenu = Menu.Home;
            CatName = " - Home";
        }

        GUI.Label(new Rect(5, 40, 200, 22), "Download Location");
        Customize.cust.DownloadPath = GUI.TextField(new Rect(5, 60, 200, 22), "" + Customize.cust.DownloadPath);
    }

	void Soundtrack()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = " - Home";
		}
		if (Customize.cust.EnableSoundTrack == true)
		{
			if (GUI.Button(new Rect(5, 100, 150, 20), "Soundtrack Mute"))
			{
				Customize.cust.EnableSoundTrack = false;
			}
		}
		else
		{
			if (GUI.Button(new Rect(5, 100, 150, 20), "Soundtrack Unmute"))
			{
				Customize.cust.EnableSoundTrack = true;
			}
		}

			float TempVol = Customize.cust.SoundtrackVolume * 100;
			GUI.Label (new Rect(210, 135, 200, 22),"% " + TempVol.ToString("F0"));

		Customize.cust.SoundtrackVolume = GUI.HorizontalSlider (new Rect (5, 140, 200, 22), Customize.cust.SoundtrackVolume, 0f, 1);
	}

	void Commands()
	{
		if (CommandNames.Count <= GameControl.control.Commands.Count)
		{
			for (int i = 0; i < GameControl.control.Commands.Count; i++)
			{
				CommandNames.Add(GameControl.control.Commands[i].Name);
			}
		}

		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = " - Home";
		}

		GUI.Box(new Rect(5,25,100,21),"Name");
		GUI.Box(new Rect(110,25,100,21),"Function");

		if (GUI.Button (new Rect (215, 25, 50, 21), "Search"))
		{

		}

		scrollpos = GUI.BeginScrollView(new Rect(5, 50, 225, 144), scrollpos, new Rect(0, 0, 0, scrollsize*24));
		for (scrollsize = 0; scrollsize < GameControl.control.Commands.Count; scrollsize++)
		{
			CommandNames[scrollsize] = GUI.TextField (new Rect (0, scrollsize * 24, 100, 21), CommandNames[scrollsize]);
			GUI.TextField (new Rect (105, scrollsize * 24, 100, 21),  GameControl.control.Commands[scrollsize].Func);
		}
		GUI.EndScrollView();

		if(GUI.Button(new Rect(235, 100, 40, 21),"Apply"))
		{
			for (int i = 0; i < GameControl.control.Commands.Count; i++)
			{
				GameControl.control.Commands[i].Name = CommandNames[i];
			}
		}
	}

    public static int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }


    void Notifications()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = " - Home";
		}

		//GUI.Toggle (new Rect (3, 30, 100, 20), Customize.cust.SideNoti, "Side Notification");

		Customize.cust.SideNoti = GUI.Toggle (new Rect (3, 30, 150, 20), Customize.cust.SideNoti, "Sidebar Alert");

		Customize.cust.PlayNotiSound = GUI.Toggle (new Rect (3, 60, 150, 20), Customize.cust.PlayNotiSound, "Play Alert Sound");

        NotiSoundString = GUI.TextField(new Rect(3, 90, 50, 20),"" + NotiSoundString);
        NotiSoundString = Regex.Replace(NotiSoundString, @"[^0-9]", "");

        GUI.TextField(new Rect(3, 120, 150, 20), "Selected Sound: " + Customize.cust.SelectedNotiSound);

        if (GUI.Button(new Rect(60, 90, 50, 20),"Apply"))
        {
            int SelectedNotiSound = IntParseFast(NotiSoundString);

            if (SelectedNotiSound > 2)
            {
                SelectedNotiSound = 2;
            }
            if (SelectedNotiSound <= 0)
            {
                SelectedNotiSound = 0;
            }

            if(NotiSoundString == "")
            {
                SelectedNotiSound = 0;
            }

            Customize.cust.SelectedNotiSound = SelectedNotiSound;

            sc.SoundSelect = Customize.cust.SelectedNotiSound;
            sc.PlaySound();
        }
    }

	void MouseSettings()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = " - Home";
		}
		//Customize.cust.DoubleClickEnable = GUI.Toggle (new Rect (3, 30, 150, 20), Customize.cust.DoubleClickEnable, "Icon Double Click");
		//Customize.cust.MouseSpeed = GUI.HorizontalSlider (new Rect (50, 65, 200, 20), Customize.cust.MouseSpeed, 0.05f, 2);
		if (Customize.cust.DoubleClickEnable == true)
		{
//			float IconDelay;
//			IconDelay = Customize.cust.DoubleClickDelayIcon * 1000;
//			GUI.Label (new Rect (250, 80, 200, 20), "" + IconDelay.ToString("F0") + "ms");
//			GUI.Label (new Rect (5, 60, 200, 20), "Icon Double Click Delay");
//			Customize.cust.DoubleClickDelayIcon = GUI.HorizontalSlider (new Rect (30, 75, 250, 20), Customize.cust.DoubleClickDelayIcon, 0.05f, 2);
		}

		GUI.Label(new Rect(2,20,100,22),"Cursor Size: " + Customize.cust.CursorSize);

		if(GUI.Button(new Rect(2,40,21,21),"<"))
		{
			Customize.cust.CursorSize--;
			Customize.cust.CustomCursorSize = true;
		}

		if(GUI.Button(new Rect(25,40,21,21),">"))
		{
			Customize.cust.CursorSize++;
			Customize.cust.CustomCursorSize = true;
		}

		if(GUI.Button(new Rect(50,40,42,21),"Reset"))
		{
			Customize.cust.CursorSize = 16;
			Customize.cust.CustomCursorSize = false;
		}

		if (Customize.cust.UsingCustomCursor == false)
		{
			if(GUI.Button(new Rect(2,70,200,21),"Use Custom Mouse Image"))
			{
				Customize.cust.UsingCustomCursor = true;
			}
		} 
		else 
		{
			if(GUI.Button(new Rect(2,70,200,21),"Use System Mouse Image"))
			{
				Customize.cust.UsingCustomCursor = false;
			}
		}
		Customize.cust.CustomTexFileNames[3] = GUI.TextField(new Rect (2, 100, 296, 21), Customize.cust.CustomTexFileNames[3]);

		float IconDelay;
		IconDelay = Customize.cust.DoubleClickDelayMenu * 1000;
		GUI.Label (new Rect (5, 120, 200, 20), "Double Click Delay "+ IconDelay.ToString("F0") + "ms");
		Customize.cust.DoubleClickDelayMenu = GUI.HorizontalSlider (new Rect (5, 140, 290, 20), Customize.cust.DoubleClickDelayMenu, 0.05f, 2);

		if(GUI.Button(new Rect(258, 70, 40, 21),"Apply"))
		{
			if (Customize.cust.CustomTexFileNames [3] != "")
			{
				ApplyMouseImage();
			}
		}
	}

	void Account()
	{
		if (showPics == false && showName == false && showPass == false) 
		{
			if(GUI.Button(new Rect(5,60,150,20),"Change Profile Pic"))
			{
				showPics = true;
			}

			if(GUI.Button(new Rect(5,120,150,20),"Change Account Password"))
			{
				showPass = true;
			}

			if(GUI.Button(new Rect(5,90,150,20),"Change Account Name"))
			{
				ep.show = true;
				ep.ErrorMsg = "This will create a new profile. You can manually change the profile file name to the name you wish. This only updates the Login List";
				ep.ErrorTitle = "Account Name Warnning";
				ep.playsound = true;
                appman.SelectedApp = "Error Prompt";
                showName = true;
			}

			if(GUI.Button(new Rect(5,150,150,20),"Delete Profile Data"))
			{
				ep.show = true;
				ep.ErrorMsg = "This will delete your saved profile. Are you sure you want to do this?";
				ep.ErrorTitle = "Account Name Warnning";
				ep.playsound = true;
                appman.SelectedApp = "Error Prompt";
            }

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				SelectedMenu = Menu.Home;
				CatName = " - Home";
			}
		}

		if (showPics == true) 
		{
			scrollpos = GUI.BeginScrollView(new Rect(5, 60, 50, 130), scrollpos, new Rect(0, 0, 0, scrollsize*32));
			for (scrollsize = 0; scrollsize < GameControl.control.UserPic.Count; scrollsize++)
			{
				if(GUI.Button(new Rect(0, scrollsize * 32, 32, 32),GameControl.control.UserPic[scrollsize]))
				{
					Select = scrollsize;
					ProfileController.procon.ProfileID[GameControl.control.ProfileID] = Select;
					GameControl.control.ProfilePicID = ProfileController.procon.ProfileID[GameControl.control.ProfileID];
					ProfileController.procon.Save();
					showPics = false;
				}
			}
			GUI.EndScrollView();

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				showPics = false;
			}
		}

		if (showName == true) 
		{
			if(GUI.Button(new Rect(5,100,60,20),"Set Name"))
			{
				ProfileController.procon.Profiles[GameControl.control.ProfileID] = TempName;
				ProfileController.procon.Save();
			}

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				showName = false;
			}

			TempName = GUI.TextField(new Rect (5, 70, 150, 20), TempName);

		}

		if (showPass == true) 
		{
			if(GUI.Button(new Rect(5,100,60,20),"Set Pass"))
			{
				ProfileController.procon.ProfilePassWord[GameControl.control.ProfileID] = TempPass;
				ProfileController.procon.Save();
			}

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				showPass = false;
			}

			TempPass = GUI.TextField(new Rect (5, 70, 150, 20), TempPass);

		}
	}

	void Clock()
	{
		if (showShortDate == false && showLongDate == false && showTime == false) 
		{
			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				SelectedMenu = Menu.Home;
				CatName = " - Home";
			}

			if(GUI.Button(new Rect(5,90,70,20),"Short Date"))
			{
				showShortDate = true;
			}

			if(GUI.Button(new Rect(5,120,70,20),"Long Date"))
			{
				showLongDate = true;
			}

			if(GUI.Button(new Rect(5,150,70,20),"Time"))
			{
				showTime = true;
			}
		}

		if (showShortDate == true) 
		{
			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				showShortDate = false;
			}

			int i = 0;
			scrollpos = GUI.BeginScrollView(new Rect(5, 60, 100, 100), scrollpos, new Rect(0, 0, 0, i*20));
			for (i = 0; i < ShortDate.Count; i++) 
			{
				if(GUI.Button(new Rect(2, i * 20, 90, 20), "" + ShortDate[i]))
				{
					Customize.cust.DateFormat = ShortDate[i];
				}
			}
			GUI.EndScrollView();
		}

		if (showLongDate == true) 
		{
			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				showLongDate = false;
			}

			int i = 0;
			scrollpos = GUI.BeginScrollView(new Rect(5, 60, 100, 100), scrollpos, new Rect(0, 0, 0, i*20));
			for (i = 0; i < LongDate.Count; i++) 
			{
				if(GUI.Button(new Rect(2, i * 20, 90, 20), "" + LongDate[i]))
				{
					Customize.cust.DateFormat = LongDate[i];
				}
			}
			GUI.EndScrollView();
		}

		if (showTime == true) 
		{
			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				showTime = false;
			}

			int i = 0;
			scrollpos = GUI.BeginScrollView(new Rect(5, 60, 100, 100), scrollpos, new Rect(0, 0, 0, i*20));
			for (i = 0; i < Time.Count; i++) 
			{
				if(GUI.Button(new Rect(2, i * 20, 90, 20), "" + Time[i]))
				{
					Customize.cust.DateFormat = Time[i];
				}
			}
			GUI.EndScrollView();
		}
	}

	void Display()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = " - Home";
		}

		if (Customize.cust.CustomThemeSelectorEnabled == true)
		{
			if(GUI.Button(new Rect(113,30,100,20),"Theme"))
			{
				SelectedMenu = Menu.Theme;
				CatName = " - Themes";
			}
		}

		if (Customize.cust.CustomThemeColorEnabled == true)
		{
			if (GUI.Button (new Rect (3, 180, 100, 20), "Color"))
			{
				SelectedMenu = Menu.Color;
				CatName = " - Color";
			}
		}

		if (GUI.Button (new Rect (3, 30, 100, 20), "Backgrounds"))
		{
			SelectedMenu = Menu.Background;
			CatName = " - Backgrounds";
		}

		if(GUI.Button(new Rect(3,120,100,20),"Screen Saver"))
		{
			SelectedMenu = Menu.ScreenSaver;
			LocalScreenSaverMenu = "Main";
			CatName = " - Screen Saver";
		}

		if(GUI.Button(new Rect(3,60,100,20),"Settings"))
		{
			SelectedMenu = Menu.Settings;
			CatName = " - Settings";
		}

		if (GUI.Button (new Rect (3, 150, 100, 20), "Font"))
		{
			SelectedMenu = Menu.Font;
			CatName = " - Font Settings";
		}

		if (GUI.Button (new Rect (3, 90, 100, 20), "Scaling"))
		{
			SelectedMenu = Menu.Scaling;
			CatName = " - Desktop Scaling";
		}
	}

	void Scaling()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Display;
		}

		if(GUI.Button(new Rect(5,50,55,20),"- Scale"))
		{
			Scale-=0.05f;
		}

		if(GUI.Button(new Rect(75,50,55,20),"Scale +"))
		{
			Scale+=0.05f;
		}

		if(GUI.Button(new Rect(5,72,45,20),"Apply"))
		{
			ep.Restart = true;
			ep.ErrorTitle = "Attention - Update Scaling";
			ep.ErrorMsg = "To update scaling you must restart the system";
			ep.SoundSelect = 0;
			ep.playsound = true;
            appman.SelectedApp = "Error Prompt";
            Customize.cust.UIScale = Scale;
		}

		GUI.Label(new Rect(5,90,300,20),"Current Scale Mode " + Scale.ToString("F2"));
	}

	void Font()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Display;
		}

		if(GUI.Button(new Rect(5,50,55,20),"- Font"))
		{
			Customize.cust.FontSize-=1;
		}

		if(GUI.Button(new Rect(75,50,55,20),"Font +"))
		{
			Customize.cust.FontSize+=1;
		}

		GUI.Label(new Rect(5,70,300,20),"Font Size " + Customize.cust.FontSize.ToString("F0"));
	}

	void ScreenSaver()
	{
		switch (LocalScreenSaverMenu)
		{
		case "Main":

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				SelectedMenu = Menu.Display;
			}

			Customize.cust.ScreenSaverEnabled = GUI.Toggle (new Rect (5, 30, 200, 20), Customize.cust.ScreenSaverEnabled, "Enable/Disable Screen Saver");

			if(GUI.Button(new Rect(5, 80, 100, 20),"Active Time"))
			{
				LocalScreenSaverMenu = "Active";
			}

			if(GUI.Button(new Rect(5, 101, 100, 20),"Customize"))
			{
				LocalScreenSaverMenu = "Custom";
			}

			if(GUI.Button(new Rect(5, 180, 50, 20),"Apply"))
			{
				if (SSTimeString != "") 
				{
					Customize.cust.SSActiveTime = float.Parse(SSTimeString);	
				}

				ApplyScreenSaver ();
			}

			break;
		case "Active":

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				LocalScreenSaverMenu = "Main";
			}

			GUI.Label (new Rect(5,60,300,300),"Time before Screen Saver is active");
			SSTimeString = GUI.TextField(new Rect (5, 80, 50, 20),"" + SSTimeString);
			GUI.TextField(new Rect (5, 107, 175, 20),"Current Active Time: " + Customize.cust.SSActiveTime);
			SSTimeString = Regex.Replace(SSTimeString, @"[^0-9]", "");

			if(GUI.Button(new Rect(5, 180, 50, 20),"Apply"))
			{
				if (SSTimeString != "") 
				{
					Customize.cust.SSActiveTime = float.Parse(SSTimeString);	
				}
			}

			break;
		case "Custom":

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				LocalScreenSaverMenu = "Main";
			}


			if(GUI.Button(new Rect(5, 30, 100, 20),"Types"))
			{
				LocalScreenSaverMenu = "Types";
			}


			GUI.Label (new Rect(5,80,300,200),"Custom ScreenSaver Picture");
			Customize.cust.CustomTexFileNames[6] = GUI.TextField(new Rect (5, 100, 250, 20), Customize.cust.CustomTexFileNames[6]);

			GUI.Label (new Rect(5,130,300,200),"Custom ScreenSaver Background");
			Customize.cust.CustomTexFileNames[5] = GUI.TextField(new Rect (5, 150, 250, 20), Customize.cust.CustomTexFileNames[5]);

			if(GUI.Button(new Rect(5, 180, 50, 20),"Apply"))
			{
				ApplyScreenSaver ();
			}

			break;

		case "Types":

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				LocalScreenSaverMenu = "Custom";
			}
				
			GUI.Label (new Rect(5,130,200,200),"Current Type: " + Customize.cust.ScreenSaverType);

			scrollpos = GUI.BeginScrollView(new Rect(2, 30, 100, 130), scrollpos, new Rect(0, 0, 0, scrollsize*20));
			for (scrollsize = 0; scrollsize < ss.ScreenSaverTypes.Count; scrollsize++)
			{
				if(GUI.Button(new Rect(0, scrollsize * 20, 80, 20),ss.ScreenSaverTypes[scrollsize]))
				{
					Customize.cust.ScreenSaverType = ss.ScreenSaverTypes[scrollsize];
				}
			}
			GUI.EndScrollView();

			break;
		}
	}

	public void ApplyScreenSaver()
	{
		Customize.cust.Save();

		ct.enabled = true;
		ct.Once = false;
		ct.UpdatePics();

		if (Customize.cust.CustomTexFileNames [6] != "")
		{
			ss.ScreensaverPicture = ct.tex1 [6];
		}

		if (Customize.cust.CustomTexFileNames [5] != "")
		{
			ss.ScreensaverBackGround = ct.tex1 [5];
		}
	}

	void Theme()
	{
		Customize.cust.GUIID = GameControl.control.GUIID;
		GUI.Label(new Rect(10, 75, 200, 200), "" + GameControl.control.GUIID);

		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Dev;
		}

		if(GUI.Button(new Rect(100,75,20,20),">"))
		{
			if(GameControl.control.GUIID<com.Skin.Length-1)
			{
				GameControl.control.GUIID++;
			}
		}

		if(GUI.Button(new Rect(75,75,20,20),"<"))
		{
			if(GameControl.control.GUIID>0)
			{
				GameControl.control.GUIID--;
			}
		}

		if (GameControl.control.GUIID == 10) 
		{
			if(GUI.Button(new Rect(10,100,150,20),"Custom Theme Settings"))
			{
				ComAddress = "S:/Settings/Themes/Custom";
			}
		}
	}

	IEnumerator LoadFile(string path)
	{
		WWW www = new WWW("file://" + path);

		AudioClip clip = www.GetAudioClip(false);
		while(!clip.isReadyToPlay)
			yield return www;

		clip.name = Path.GetFileName(path);
		clips.Add(clip);
	}

	void Colors()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Display;
		}

		if(GUI.Button(new Rect(10,60,100,20),"Font"))
		{
			SelectedMenu = Menu.FontColor;
		}

		if(GUI.Button(new Rect(10,90,100,20),"Buttons"))
		{
			SelectedMenu = Menu.ButtonColor;
		}

//		path = GUI.TextField (new Rect (10, 150, 100, 20), path);
//
//		if(GUI.Button(new Rect(10,180,100,20),"Load File"))
//		{
//			LoadFile(path);
//		}


		if (GUI.Button (new Rect (10, 120, 100, 20), "Windows")) 
		{
			SelectedMenu = Menu.WindowColor;
		}
	}

	void FontColorUI()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Color;
		}

		SetFontColor();

		GUI.Label(new Rect(5,60,300,300),"Red");
		GUI.Label(new Rect(5,80,300,300),"Green");
		GUI.Label(new Rect(5,100,300,300),"Blue");
		GUI.Label(new Rect(5,120,300,300),"Alpha");

		GUI.Label(new Rect(260,60,300,300),"" + Customize.cust.FontR.ToString("F0"));
		GUI.Label(new Rect(260,80,300,300),"" + Customize.cust.FontG.ToString("F0"));
		GUI.Label(new Rect(260,100,300,300),"" + Customize.cust.FontB.ToString("F0"));
		GUI.Label(new Rect(260,120,300,300),"" + Customize.cust.FontA.ToString("F0"));

		GUI.contentColor = Color.white;

		Customize.cust.FontR = GUI.HorizontalSlider (new Rect (50, 65, 200, 20), Customize.cust.FontR, 1, 255);
		Customize.cust.FontG = GUI.HorizontalSlider (new Rect (50, 85, 200, 20), Customize.cust.FontG, 1, 255);
		Customize.cust.FontB = GUI.HorizontalSlider (new Rect (50, 105, 200, 20), Customize.cust.FontB, 1, 255);
		Customize.cust.FontA = GUI.HorizontalSlider (new Rect (50, 125, 200, 20), Customize.cust.FontA, 1, 255);

		Customize.cust.FontColorInt = 1;
	}

	void WindowColorUI()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Color;
		}

		SetWindowColor();

		GUI.Label(new Rect(5,60,300,300),"Red");
		GUI.Label(new Rect(5,80,300,300),"Green");
		GUI.Label(new Rect(5,100,300,300),"Blue");
		GUI.Label(new Rect(5,120,300,300),"Alpha");

		GUI.Label(new Rect(260,60,300,300),"" + Customize.cust.WindowR.ToString("F0"));
		GUI.Label(new Rect(260,80,300,300),"" + Customize.cust.WindowG.ToString("F0"));
		GUI.Label(new Rect(260,100,300,300),"" + Customize.cust.WindowB.ToString("F0"));
		GUI.Label(new Rect(260,120,300,300),"" + Customize.cust.WindowA.ToString("F0"));

		GUI.backgroundColor = Color.white;

		Customize.cust.WindowR = GUI.HorizontalSlider (new Rect (60, 65, 200, 20), Customize.cust.WindowR, 1, 255);
		Customize.cust.WindowG = GUI.HorizontalSlider (new Rect (60, 85, 200, 20), Customize.cust.WindowG, 1, 255);
		Customize.cust.WindowB = GUI.HorizontalSlider (new Rect (60, 105, 200, 20), Customize.cust.WindowB, 1, 255);
		Customize.cust.WindowA = GUI.HorizontalSlider (new Rect (60, 125, 200, 20), Customize.cust.WindowA, 1, 255);

		Customize.cust.WindowColorInt = 3;
	}

	void ButtonColorUI()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Color;
		}

		SetButtonColor();

		GUI.Label(new Rect(5,60,300,300),"Red");
		GUI.Label(new Rect(5,80,300,300),"Green");
		GUI.Label(new Rect(5,100,300,300),"Blue");
		GUI.Label(new Rect(5,120,300,300),"Alpha");

		GUI.Label(new Rect(260,60,300,300),"" + Customize.cust.ButtonR.ToString("F0"));
		GUI.Label(new Rect(260,80,300,300),"" + Customize.cust.ButtonG.ToString("F0"));
		GUI.Label(new Rect(260,100,300,300),"" + Customize.cust.ButtonB.ToString("F0"));
		GUI.Label(new Rect(260,120,300,300),"" + Customize.cust.ButtonA.ToString("F0"));

		GUI.backgroundColor = Color.white;

		Customize.cust.ButtonR = GUI.HorizontalSlider (new Rect (60, 65, 200, 20), Customize.cust.ButtonR, 1, 255);
		Customize.cust.ButtonG = GUI.HorizontalSlider (new Rect (60, 85, 200, 20), Customize.cust.ButtonG, 1, 255);
		Customize.cust.ButtonB = GUI.HorizontalSlider (new Rect (60, 105, 200, 20), Customize.cust.ButtonB, 1, 255);
		Customize.cust.ButtonA = GUI.HorizontalSlider (new Rect (60, 125, 200, 20), Customize.cust.ButtonA, 1, 255);

		Customize.cust.ButtonColorInt = 2;
	}

	void ScreenUI()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Display;
		}

		scrollpos = GUI.BeginScrollView(new Rect(5, 60, 100, 100), scrollpos, new Rect(0, 0, 0, Res*20));
		for (Res = 0; Res < Rez.Count; Res++) 
		{
			if(GUI.Button(new Rect(2, Res * 20, 80, 20), "" + Rez[Res]))
			{
				ep.enabled = true;
				ep.show = true;
				ep.ErrorTitle = "Rez Select Warning";
				ep.ErrorMsg = "The gateway must restart for changes to take effect. Are you sure?";
				ep.Restart = true;
				ep.playsound = true;
                appman.SelectedApp = "Error Prompt";
                //Screen.SetResolution (Customize.cust.RezX, Customize.cust.RezY, Customize.cust.FullScreen);
                //desk.UpdateUI();
                Customize.cust.RezSelect = Res;
				RezSelect();
			}
		}
		GUI.EndScrollView();

		GUI.Label (new Rect (5, 160, 200, 21),"Current Rez " + Customize.cust.RezX + "x" + Customize.cust.RezY);
		GUI.Label (new Rect (5, 180, 200, 21),"Current SW " + Screen.width + "x" + Screen.height);

		Customize.cust.FullScreen = GUI.Toggle(new Rect (170*w, 60*h, 120*w, 20*h), Customize.cust.FullScreen, "Fullscreen ");

		GUI.Label (new Rect (200*w, 80*h, 120*w, 120*h),"V-Sync " + Customize.cust.VSync);

		if(GUI.Button(new Rect(270*w,80*h,20*w,20*h),">"))
		{
			if (Customize.cust.VSync < 2) 
			{
				Customize.cust.VSync ++;
			} 
		}

		if(GUI.Button(new Rect(170*w,80*h,20*w,20*h),"<"))
		{
			if (Customize.cust.VSync > 0) 
			{
				Customize.cust.VSync--;
			} 
		}

		GUI.Label (new Rect (200*w, 105*h, 120*w, 120*h), "AA " + Customize.cust.AA);

		if(GUI.Button(new Rect(270*w,105*h,20*w,20*h),">") && Customize.cust.AA < 8)
		{
			if (Customize.cust.AA == 0) 
			{
				Customize.cust.AA +=2;
			} 
			else 
			{
				Customize.cust.AA*=2;
			}
		}

		if(GUI.Button(new Rect(170*w,105*h,20*w,20*h),"<") && Customize.cust.AA >= 0)
		{
			if (Customize.cust.AA == 2) 
			{
				Customize.cust.AA-=2;
			} 
			else 
			{
				Customize.cust.AA/=2;
			}
		}
		//int whi = (int)wh;
		//GUI.skin.font.fontSize = whi;
		//GameControl.control.wh = GUI.HorizontalSlider(new Rect (170, 160, 100, 20), GameControl.control.wh, 1, 3);
	}

	void Background()
	{
		GUI.contentColor = Color.white;
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Display;
		}

		if(GUI.Button(new Rect(60,30,100,21),"Settings"))
		{
			SelectedMenu = Menu.BackgroundSettings;
		}

		scrollpos = GUI.BeginScrollView(new Rect(5, 60, 275, 130), scrollpos, new Rect(0, 0, 0, scrollsize*64));
		for (scrollsize = 0; scrollsize < BackgroundPics.Count; scrollsize++)
		{
			if(GUI.Button(new Rect(0, scrollsize * 64, 275, 64),BackgroundPics[scrollsize],com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
			{
				Customize.cust.SelectedBackground = scrollsize;
				os.pic [2] = BackgroundPics[Customize.cust.SelectedBackground];
			}
		}
		GUI.EndScrollView();
	}

	void BackgroundSettings()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Background;
		}

		if(GUI.Button(new Rect(65,30,70,21),"Apply"))
		{
			ApplyBackgrounds();
		}

		Customize.cust.CustomTexFileNames[4] = GUI.TextField(new Rect (2, 182, 296, 21), Customize.cust.CustomTexFileNames[4]);
	}

	void QuickLaunch()
	{
		GUI.Label(new Rect(10,180,300,100), Status);

		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = " - Home";
		}

		if (GUI.Button (new Rect (200, 180, 80, 20), "Refresh"))
		{
			QuickLaunchScan();
		}

		scrollpos = GUI.BeginScrollView(new Rect(5, 70, 290, 100), scrollpos, new Rect(0, 0, 0, QuickLaunchSize*20));
		for (QuickLaunchSize = 0; QuickLaunchSize < SelectablePrograms.Count; QuickLaunchSize++) 
		{
			if(GUI.Button(new Rect(3, QuickLaunchSize * 20, 150, 20), "" + SelectablePrograms[QuickLaunchSize].Name))
			{
				Selecting = QuickLaunchSize;
				QuickLaunchCheck();
			}
		}
		GUI.EndScrollView();
	}

	void QuickLaunchScan()
	{
		SelectablePrograms.RemoveRange (0, SelectablePrograms.Count);

		for (ScanCount = 0; ScanCount < GameControl.control.ProgramFiles.Count; ScanCount++) 
		{
			if (GameControl.control.ProgramFiles[ScanCount].Type == ProgramSystem.ProgramType.Exe)
			{
				SelectablePrograms.Add(GameControl.control.ProgramFiles[ScanCount]);
			}
		}
	}

	void QuickLaunchCheck()
	{
		if (GameControl.control.QuickProgramList.Count > 0)
		{
			if (!GameControl.control.QuickLaunchNames.Contains (SelectablePrograms[Selecting].Name))
			{
				GameControl.control.QuickProgramList.Add (SelectablePrograms [Selecting]);
				GameControl.control.QuickLaunchNames.Add(SelectablePrograms [Selecting].Name);
				Status = "Added " + SelectablePrograms [Selecting].Name + " to quick launch list";
			} 
			else 
			{
				GameControl.control.QuickProgramList.Remove (SelectablePrograms [Selecting]);
				GameControl.control.QuickLaunchNames.Remove(SelectablePrograms [Selecting].Name);
				Status = "Removed " + SelectablePrograms [Selecting].Name + " from quick launch list";
			}
		} 
		else 
		{
			GameControl.control.QuickProgramList.Add (SelectablePrograms [Selecting]);
			GameControl.control.QuickLaunchNames.Add(SelectablePrograms [Selecting].Name);
			Status = "Added " + SelectablePrograms [Selecting].Name + " to quick launch list";
		}
	}
}
