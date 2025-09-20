using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
	private DesktopEnviroment os;
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

	public List<ResolutionSystem> Rez = new List<ResolutionSystem>();

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

	public List<ProgramSystem> DefaultLaunchOptions = new List<ProgramSystem>();

	public string AutoSaveTimeString;

	public string SelectedFilePath;

    public float Red = 0;
    public float Green = 0;
    public float Blue = 0;
    public float Alpha = 0;

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
		DesktopColor,
		Background,
		BackgroundSettings,
		Dev,
		DefaultProgram,
		Autosave,
		Documents,
		Desktop,
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
		os = System.GetComponent<DesktopEnviroment>();
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

		AutoSaveTimeString = "" + Customize.cust.AutoSaveTime;
	}

	void SetPos()
	{
		CloseButton = new Rect (277,2,21,20);
		MiniButton = new Rect (255,2,21,20);
		DefaltSetting = new Rect (0,2,300,205);
	}

	void UpdateRezList()
	{
		Rez.Add (new ResolutionSystem("640x480","",640,480,false));
		Rez.Add(new ResolutionSystem("751x785", "", 751, 785, false));
		Rez.Add(new ResolutionSystem("800x600", "", 800, 600, false));
		Rez.Add(new ResolutionSystem("1024x768", "", 1024, 768, false));
		Rez.Add(new ResolutionSystem("1152x864", "", 1152, 864, false));
		Rez.Add(new ResolutionSystem("1280x720", "", 1280, 720, false));
		Rez.Add(new ResolutionSystem("1280x768", "", 1280, 768, false));
		Rez.Add(new ResolutionSystem("1366x768", "", 1366, 768, false));
		Rez.Add(new ResolutionSystem("1600x900", "", 1600, 900, false));
		Rez.Add(new ResolutionSystem("1680x1050", "", 1680, 1050, true));
		Rez.Add(new ResolutionSystem("1920x1080", "", 1920, 1080, true));
		Rez.Add(new ResolutionSystem("2560x1440", "", 2560, 1440, true));
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
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if(show == true)
		{
            GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");

            //GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	public void OpenFileExplorerBackground()
	{
		//appman.SelectedApp = "FileBrow"; // old
		appman.LaunchRequest = Program.Run("File Browser", ProgramSystemv2.ProgramTypes.FileBrow, "Player"); // new
	}

	public void OpenFileExplorerScreensaverBackground()
	{
		//if (Application.isEditor == true)
		//{
		//	appman.SelectedApp = "FileBrow";
		//	GameControl.control.SelectedOS.FPC.ScreenSaverBackgroundAddress = EditorUtility.OpenFilePanel("Select Screensaver Background", "", "png");
		//	WWW www = new WWW("file:///" + GameControl.control.SelectedOS.FPC.ScreenSaverBackgroundAddress);
		//}
	}

	public void OpenFileExplorerScreensaverPic()
	{
		//if (Application.isEditor == true)
		//{
		//	GameControl.control.SelectedOS.FPC.ScreenSaverPictureAddress = EditorUtility.OpenFilePanel("Select Screensaver Picture", "", "png");
		//	WWW www = new WWW("file:///" + GameControl.control.SelectedOS.FPC.ScreenSaverPictureAddress);
		//}
	}

	public void OpenFileExplorerMouseCursor()
	{
		//if (Application.isEditor == true)
		//{
		//	GameControl.control.SelectedOS.FPC.MouseCursorAddress = EditorUtility.OpenFilePanel("Select Mouse Cursor Image", "", "png");
		//	WWW www = new WWW("file:///" + GameControl.control.SelectedOS.FPC.MouseCursorAddress);
		//}
	}

	public void ApplyBackgrounds()
	{
		for(int i = 0; i < os.ListOfTextures.Count;i++)
        {
            if (os.ListOfTextures[i].Name == "CustomBackground")
            {
				os.ListOfTextures[i].Texture = null;

			}
        }
		Registry.SetStringData("Player", "ControlPanel", "BackgroundAddress", Registry.GetStringData("Player", "ControlPanel", "BackgroundField"));
	}

	public void ApplyMouseImage()
	{
		Customize.cust.Save();
		if (Customize.cust.CustomTexFileNames [3] != "")
		{
			UpdateCustomThemes();
			mouse.cursorImage = ct.tex1 [3];
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [0])) 
			{
                appman.SelectedApp = "System Panel";
			}
		} 
		else
		{
            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
			GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [1]);
		}

		if (MiniButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (MiniButton), "-",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		} 
		else
		{
            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
            if (GUI.Button (new Rect (MiniButton), "-",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		}

		RenderUI();
	}

	void RenderUI()
	{
        GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
        GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
        GUI.DragWindow(new Rect(DefaltBoxSetting));

		if(CatName != "")
		{
			GUI.Box(new Rect(DefaltBoxSetting), "System Panel - " + CatName);
		}
		else
		{
			GUI.Box(new Rect(DefaltBoxSetting), "System Panel" + CatName);
		}

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
		case Menu.DesktopColor:
			DesktopColorUI();
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
		case Menu.DefaultProgram:
			DefaultProgram();
			break;
		case Menu.Autosave:
			AutoSave();
			break;
		case Menu.Documents:
			Documents();
			break;
		case Menu.Desktop:
			Desktop();
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
			CatName = "Themes";
		}
	}

	void Home()
	{
		if(GUI.Button(new Rect(3,30,110,20),"Display"))
		{
			SelectedMenu = Menu.Display;
			CatName = "Display";
		}

		if(GUI.Button(new Rect(123,30, 110, 20),"Commands"))
		{
			SelectedMenu = Menu.Commands;
			CatName = "Commands";
		}

		if(GUI.Button(new Rect(123,60, 110, 20),"Soundtrack"))
		{
			SelectedMenu = Menu.Soundtrack;
			CatName = "Soundtrack";
		}

        if (GUI.Button(new Rect(123, 90, 110, 20), "Download"))
        {
            SelectedMenu = Menu.Download;
            CatName = "Download";
        }

		if (GUI.Button(new Rect(123, 120, 110, 20), "Default Program"))
		{
			SelectedMenu = Menu.DefaultProgram;
			CatName = "Default Program";
		}

		if (GUI.Button(new Rect(123, 150, 110, 20), "Autosave"))
		{
			SelectedMenu = Menu.Autosave;
			CatName = "Autosave";
		}

		if(GameControl.control.SelectedOS.Options.DisableColourOption == false)
		{
			if (GUI.Button(new Rect(123, 180, 110, 20), "Dev Settings"))
			{
				SelectedMenu = Menu.Dev;
				CatName = "Dev Settings";
			}
		}

        //		if(GUI.Button(new Rect(123,60,100,20),"Web Browser"))
        //		{
        //			SelectedMenu = Menu.WebBrowser;
        //			CatName = "Web Browser";
        //		}

        if (GUI.Button(new Rect(3,60, 110, 20),"Notfication"))
		{
			SelectedMenu = Menu.Notification;
			CatName = "Notfications";
		}

		if(GUI.Button(new Rect(3,90, 110, 20),"Account"))
		{
			SelectedMenu = Menu.Account;
			CatName = "Account";
		}

		if(GUI.Button(new Rect(3,120, 110, 20),"Mouse"))
		{
			SelectedMenu = Menu.Mouse;
			CatName = "Mouse Settings";
		}

		if(GUI.Button(new Rect(3,150, 110, 20),"Web Browser"))
		{
			SelectedMenu = Menu.WebBrowser;
			CatName = "Web Browser";
		}

//		if(GUI.Button(new Rect(3,150,100,20),"Clock"))
//		{
//			SelectedMenu = Menu.Clock;
//			CatName = "Clock";
//		}

		if(GUI.Button(new Rect(3,180, 110, 20),"Quick Launch"))
		{
			SelectedMenu = Menu.QuickLaunch;
			CatName = "Quick Launch";
			QuickLaunchScan();
		}
	}

	void DefaultProgram()
	{
		if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = "Home";
		}

		if (GUI.Button(new Rect(3, 30, 100, 20), "Documents"))
		{
			SelectedMenu = Menu.Documents;
			CatName = "Documents";
		}

	}

	void AutoSave()
	{
		if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = "Home";
		}

		if(Customize.cust.EnableAutoSave == true)
		{
			if (GUI.Button(new Rect(3, 30, 150, 20), "Autosave Enabled"))
			{
				Customize.cust.EnableAutoSave = false;
			}
		}
		else
		{
			if (GUI.Button(new Rect(3, 30, 150, 20), "Autosave Disabled"))
			{
				Customize.cust.EnableAutoSave = true;
			}
		}

		if (AutoSaveTimeString == "")
		{
			AutoSaveTimeString = "1";
		}
		else
		{
			Customize.cust.AutoSaveTime = float.Parse(AutoSaveTimeString);
		}

		AutoSaveTimeString = GUI.TextField(new Rect(3, 60, 50, 21), "" + AutoSaveTimeString);
		AutoSaveTimeString = Regex.Replace(AutoSaveTimeString, @"[^1-9]", "");
	}

	void Documents()
	{
		if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
		{
			SelectedMenu = Menu.DefaultProgram;
			CatName = "Default Programs";
		}

		DefaultLaunchOptions.RemoveRange(0, DefaultLaunchOptions.Count);

		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			for (int j = 0; j < GameControl.control.ProgramFiles[i].Type.Count; j++)
			{
				if (GameControl.control.ProgramFiles[i].Type[j] == ProgramSystem.FileType.DocumentReaders)
				{
					if(!DefaultLaunchOptions.Contains(GameControl.control.ProgramFiles[i]))
					{
						DefaultLaunchOptions.Add(GameControl.control.ProgramFiles[i]);
					}
				}
			}
		}

		scrollpos = GUI.BeginScrollView(new Rect(5, 50, 150, 144), scrollpos, new Rect(0, 0, 0, scrollsize * 24));
		for (scrollsize = 0; scrollsize < DefaultLaunchOptions.Count; scrollsize++)
		{
			if (GUI.Button(new Rect(0, scrollsize * 24, 100, 21), DefaultLaunchOptions[scrollsize].Name))
			{
				for (int i = 0; i < GameControl.control.DefaultLaunchedPrograms.Count; i++)
				{
					for (int j = 0; j < GameControl.control.DefaultLaunchedPrograms[i].Type.Count; j++)
					{
						if (GameControl.control.DefaultLaunchedPrograms[i].Type[j] == ProgramSystem.FileType.DocumentReaders)
						{
							GameControl.control.DefaultLaunchedPrograms.RemoveAt(i);
							GameControl.control.DefaultLaunchedPrograms.Add(DefaultLaunchOptions[scrollsize]);
						}
					}
				}
			}
		}
		GUI.EndScrollView();

		for (int i = 0; i < GameControl.control.DefaultLaunchedPrograms.Count; i++)
		{
			for (int j = 0; j < GameControl.control.DefaultLaunchedPrograms[i].Type.Count; j++)
			{
				if (GameControl.control.DefaultLaunchedPrograms[i].Type[j] == ProgramSystem.FileType.DocumentReaders)
				{
					GUI.Box(new Rect(175, 100, 100, 21), GameControl.control.DefaultLaunchedPrograms[i].Name);
				}
			}
		}

	}

	void WebBrowser()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = "Home";
		}

		GUI.Label (new Rect (5, 40, 200, 22),"Web Browsers Home Page");
		Customize.cust.WebBrowserHomepage = GUI.TextField (new Rect (5, 60, 200, 22),"" + Customize.cust.WebBrowserHomepage);
	}

    void DownloadLocation()
    {
        if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
        {
            SelectedMenu = Menu.Home;
            CatName = "Home";
        }

        GUI.Label(new Rect(5, 40, 200, 22), "Download Location");
        Customize.cust.DownloadPath = GUI.TextField(new Rect(5, 60, 200, 22), "" + Customize.cust.DownloadPath);
    }

	void Soundtrack()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = "Home";
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
		if (GameControl.control.ShortCommands == true)
		{
			if (CommandNames.Count < GameControl.control.Commands.Count)
			{
				for (int i = 0; i < GameControl.control.Commands.Count; i++)
				{
					CommandNames.Add(GameControl.control.Commands[i].ShortHand);
				}
			}
		}
		else
		{
			if (CommandNames.Count < GameControl.control.Commands.Count)
			{
				for (int i = 0; i < GameControl.control.Commands.Count; i++)
				{
					CommandNames.Add(GameControl.control.Commands[i].Name);
				}
			}
		}

		if (GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = "Home";
		}

		GUI.Box(new Rect(2,23,100,21),"Name");
		GUI.Box(new Rect(103,23,100,21),"Function");

		scrollpos = GUI.BeginScrollView(new Rect(2, 45, 225, 154), scrollpos, new Rect(0, 0, 0, scrollsize*22));
		for (scrollsize = 0; scrollsize < GameControl.control.Commands.Count; scrollsize++)
		{
			CommandNames[scrollsize] = GUI.TextField (new Rect (0, scrollsize * 22, 100, 21), CommandNames[scrollsize]);
			GUI.TextField (new Rect (101, scrollsize * 22, 100, 21),  GameControl.control.Commands[scrollsize].Func);
		}
		GUI.EndScrollView();

		//if (GUI.Button(new Rect(215, 25, 50, 21), "Search"))
		//{

		//}

		if (GameControl.control.ShortCommands == true)
		{
			if (GUI.Button(new Rect(235, 100, 40, 21), "Apply"))
			{
				for (int i = 0; i < GameControl.control.Commands.Count; i++)
				{
					GameControl.control.Commands[i].ShortHand = CommandNames[i];
				}
			}

			if (GUI.Button(new Rect(204, 23, 94, 21), "Cur:ShortCMD"))
			{
				GameControl.control.ShortCommands = false;
				CommandNames.RemoveRange(0, CommandNames.Count);
			}
		}
		else
		{
			if (GUI.Button(new Rect(235, 100, 40, 21), "Apply"))
			{
				for (int i = 0; i < GameControl.control.Commands.Count; i++)
				{
					GameControl.control.Commands[i].Name = CommandNames[i];
				}
			}

			if (GUI.Button(new Rect(204, 23, 94, 21), "Cur:LongCMD"))
			{
				GameControl.control.ShortCommands = true;
				CommandNames.RemoveRange(0, CommandNames.Count);
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
			CatName = "Home";
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
			CatName = "Home";
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
		IconDelay = Registry.GetFloatData("Player", "System", "DoubleClickSpeed") * 1000;
		Registry.SetIntData("Player", "System", "DoubleClickSpeed", (int)IconDelay);
		Registry.SetStringData("Player", "System", "DoubleClickSpeed", "Double Click Delay " + IconDelay.ToString("F0") + "ms");
		GUI.Label (new Rect (5, 120, 200, 20), Registry.GetStringData("Player", "System", "DoubleClickSpeed"));
		Registry.SetFloatData("Player", "System", "DoubleClickSpeed",GUI.HorizontalSlider (new Rect (5, 140, 290, 20), Registry.GetFloatData("Player", "System", "DoubleClickSpeed"), 0.05f, 2));

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
			if(GUI.Button(new Rect(5,30,150,20),"Change Profile Pic"))
			{
				showPics = true;
				CatName = "Profile Picture";
			}

			if(GUI.Button(new Rect(5,60, 150, 20),"Change Password"))
			{
				showPass = true;
				CatName = "User Password";
			}

			if (GUI.Button(new Rect(5, 90, 150, 20), "Change Hint"))
			{
				showName = true;
				CatName = "User Hint";
			}

			if (GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				SelectedMenu = Menu.Home;
				CatName = "Home";
			}
		}

		if (showPics == true) 
		{
			GUI.contentColor = Color.white;

			scrollpos = GUI.BeginScrollView(new Rect(5, 60, 50, 130), scrollpos, new Rect(0, 0, 0, scrollsize*32));
			for (scrollsize = 0; scrollsize < GameControl.control.UserPic.Count; scrollsize++)
			{
				if(GUI.Button(new Rect(0, scrollsize * 32, 32, 32),GameControl.control.UserPic[scrollsize]))
				{
					Select = scrollsize;
					//ProfileController.procon.ProfileID[GameControl.control.ProfileID] = Select;
					GameControl.control.ProfilePicID = ProfileController.procon.ProfilePic[GameControl.control.ProfileID];
					ProfileController.procon.ProfilePic[GameControl.control.ProfileID] = Select;
					ProfileController.procon.Save();
					showPics = false;
				}
			}
			GUI.EndScrollView();

			if(GUI.Button(new Rect(2,2,20,20),"<-"))
			{
				showPics = false;
				CatName = "Account";
			}
		}

		if (showName == true)
		{
			if (GUI.Button(new Rect(5, 100, 60, 20), "Set Hint"))
			{
				ProfileController.procon.PasswordHint[GameControl.control.ProfileID] = TempName;
				ProfileController.procon.Save();
			}

			if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
			{
				showName = false;
				CatName = "Account";
			}

			TempName = GUI.TextField(new Rect(5, 70, 150, 20), TempName);

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
				CatName = "Account";
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
				CatName = "Home";
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
			CatName = "Home";
		}

		if (Customize.cust.CustomThemeSelectorEnabled == true)
		{
			if(GUI.Button(new Rect(113,30,100,20),"Theme"))
			{
				SelectedMenu = Menu.Theme;
				CatName = "Themes";
			}
		}

		if (GameControl.control.SelectedOS.Options.DisableColourOption == false)
		{
			if (GUI.Button (new Rect (3, 180, 100, 20), "Color"))
			{
				SelectedMenu = Menu.Color;
				CatName = "Color";
			}
		}

		if (GUI.Button(new Rect(3, 30, 100, 20), "Desktop"))
		{
			SelectedMenu = Menu.Desktop;
			CatName = "Desktop";
		}

		if (GUI.Button(new Rect(3,120,100,20),"Screen Saver"))
		{
			SelectedMenu = Menu.ScreenSaver;
			LocalScreenSaverMenu = "Main";
			CatName = "Screen Saver";
		}

		if(GUI.Button(new Rect(3,60,100,20),"Settings"))
		{
			SelectedMenu = Menu.Settings;
			CatName = "Settings";
		}

		if (GUI.Button (new Rect (3, 150, 100, 20), "Font"))
		{
			SelectedMenu = Menu.Font;
			CatName = "Font Settings";
		}

		if (GUI.Button (new Rect (3, 90, 100, 20), "Scaling"))
		{
			SelectedMenu = Menu.Scaling;
			CatName = "Desktop Scaling";
		}
	}

	void Desktop()
    {
		if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
		{
			SelectedMenu = Menu.Display;
			CatName = "Display";
		}

		if (Registry.GetBoolData("Player", "System", "ShowDesktopIcons") == true)
        {
			if (GUI.Button(new Rect(3, 30, 130, 20), "Hide Icons"))
			{
				Registry.SetBoolData("Player", "System", "ShowDesktopIcons", false);
			}
		}
		else
        {
			if (GUI.Button(new Rect(3, 30, 130, 20), "Show Icons"))
			{
				Registry.SetBoolData("Player", "System", "ShowDesktopIcons", true);
			}
		}

		if (Registry.GetBoolData("Player", "System", "ShowDesktopBackground") == true)
		{
			if (GUI.Button(new Rect(3, 60, 130, 20), "Hide Background"))
			{
				Registry.SetBoolData("Player", "System", "ShowDesktopBackground",false);
			}
		}
		else
		{
			if (GUI.Button(new Rect(3, 60, 130, 20), "Show Background"))
			{
				Registry.SetBoolData("Player", "System", "ShowDesktopBackground", true);
			}
		}

		if (GUI.Button(new Rect(3, 90, 130, 20), "Set Background"))
		{
			SelectedMenu = Menu.Background;
			CatName = "Backgrounds";
		}

		if (GUI.Button(new Rect(3, 120, 130, 20), "Background Color"))
		{
			SelectedMenu = Menu.DesktopColor;
			CatName = "Desktop Color";
		}
	}

	void Scaling()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Display;
			CatName = "Display";
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
			ep.ErrorTitle = "AttentionUpdate Scaling";
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
			CatName = "Display";
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
				CatName = "Display";
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

					SaveScreenSaverData();
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


			if(GUI.Button(new Rect(5, 30, 60, 20),"Types"))
			{
				LocalScreenSaverMenu = "Types";
			}

			if(GUI.Button(new Rect(5, 60, 150, 20),"Picture Settings"))
			{
				LocalScreenSaverMenu = "ScreenSaverPic";
			}

			if(GUI.Button(new Rect(5, 90, 150, 20),"Background Settings"))
			{
				LocalScreenSaverMenu = "ScreenSaverBG";
			}

			break;

			case "ScreenSaverPic":

				if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
				{
					LocalScreenSaverMenu = "Custom";
				}

				if (GUI.Button(new Rect(5, 30, 150, 20), "Open FE SSPIC"))
				{
					OpenFileExplorerScreensaverPic();
				}

				GUI.Label(new Rect(5, 80, 300, 200), "Custom ScreenSaver Picture");
				GameControl.control.SelectedOS.FPC.ScreenSaverPictureAddress = GUI.TextField(new Rect(5, 100, 250, 20), GameControl.control.SelectedOS.FPC.ScreenSaverPictureAddress); //6

				if (GUI.Button(new Rect(5, 180, 50, 20), "Apply"))
				{
					ApplyScreenSaverPIC();
				}

				break;


			case "ScreenSaverBG":

				if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
				{
					LocalScreenSaverMenu = "Custom";
				}

				if (GUI.Button(new Rect(5, 30, 150, 20), "Open FE SSBG"))
				{
					OpenFileExplorerScreensaverBackground();
				}

				GUI.Label(new Rect(5, 80, 300, 200), "Custom ScreenSaver Background");
				GameControl.control.SelectedOS.FPC.ScreenSaverBackgroundAddress = GUI.TextField(new Rect(5, 100, 250, 20), GameControl.control.SelectedOS.FPC.ScreenSaverBackgroundAddress); //6

				if (GUI.Button(new Rect(5, 180, 50, 20), "Apply"))
				{
					ApplyScreenSaverBG();
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

	public void UpdateCustomThemes()
	{
		ct.enabled = true;
		ct.Once = false;
		ct.UpdatePics();
	}

	public void ApplyScreenSaverBG()
	{
		if (FilePathData.FileSelected == true)
		{
			GameControl.control.SelectedOS.FPC.ScreenSaverBackgroundAddress = FilePathData.Path;
			FilePathData.GetSelectedFile();
		}
		for (int i = 0; i < GameControl.control.OSName.Count; i++)
		{
			if (GameControl.control.OSName[i].Title == GameControl.control.SelectedOS.Title && GameControl.control.OSName[i].Name == GameControl.control.SelectedOS.Name)
			{
				GameControl.control.OSName[i].FPC.ScreenSaverBackgroundAddress = GameControl.control.SelectedOS.FPC.ScreenSaverBackgroundAddress;
				Customize.cust.CustomTexFileNames[5] = GameControl.control.SelectedOS.FPC.ScreenSaverBackgroundAddress;
				Customize.cust.Save();

				if (Customize.cust.CustomTexFileNames[5] != "")
				{
					UpdateCustomThemes();
					ss.ScreensaverBackGround = ct.tex1[5];
				}
			}
		}
	}

	public void ApplyScreenSaverPIC()
	{
		if (FilePathData.FileSelected == true)
		{
			GameControl.control.SelectedOS.FPC.ScreenSaverPictureAddress = FilePathData.Path;
			FilePathData.GetSelectedFile();
		}
		for (int i = 0; i < GameControl.control.OSName.Count; i++)
		{
			if (GameControl.control.OSName[i].Title == GameControl.control.SelectedOS.Title && GameControl.control.OSName[i].Name == GameControl.control.SelectedOS.Name)
			{
				GameControl.control.OSName[i].FPC.ScreenSaverPictureAddress = GameControl.control.SelectedOS.FPC.ScreenSaverPictureAddress;
				Customize.cust.CustomTexFileNames[6] = GameControl.control.SelectedOS.FPC.ScreenSaverPictureAddress;
				Customize.cust.Save();
				if (Customize.cust.CustomTexFileNames[6] != "")
				{
					UpdateCustomThemes();
					ss.ScreensaverPicture = ct.tex1[6];
				}
			}
		}
	}

	public void SaveScreenSaverData()
	{
		for (int i = 0; i < GameControl.control.OSName.Count; i++)
		{
			if (GameControl.control.OSName[i].Title == GameControl.control.SelectedOS.Title && GameControl.control.OSName[i].Name == GameControl.control.SelectedOS.Name)
			{
				Customize.cust.Save();
			}
		}
	}

	void Theme()
	{
		Customize.cust.GUIID = Registry.GetIntData("Player", "System", "Skin");
		GUI.Label(new Rect(10, 75, 200, 200), "" + Registry.GetIntData("Player", "System", "Skin"));

		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Dev;
		}

        Registry.SetStringData("Player", "System", "Skin",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].ToString());

		GUI.Label(new Rect(100, 100, 100, 22), Registry.GetStringData("Player", "System", "Skin"));

        if (GUI.Button(new Rect(100,75,20,20),">"))
		{
			if(Registry.GetIntData("Player","System","Skin")<GameControl.control.Skins.Length-1)
			{
                Registry.SetIntData("Player", "System", "Skin", Registry.GetIntData("Player", "System", "Skin") + 1);
			}
		}

		if(GUI.Button(new Rect(75,75,20,20),"<"))
		{
			if(Registry.GetIntData("Player", "System", "Skin") > 0)
			{
                Registry.SetIntData("Player", "System", "Skin", Registry.GetIntData("Player", "System", "Skin") - 1);
            }
		}

        if (GUI.Button(new Rect(50, 150, 100, 20), "White Font"))
        {
            TestCode.KeywordCheck("Player", "Font Color: " + 255 + ":" + 255 + ":" + 255 + ":" + 255 + ";");
        }

        if (GUI.Button(new Rect(50, 125, 100, 20), "Black Font"))
        {
            TestCode.KeywordCheck("Player", "Font Color: " + 0 + ":" + 0 + ":" + 0 + ":" + 255 + ";");
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
			CatName = "Display";
		}

		if(GUI.Button(new Rect(10,60,100,20),"Font"))
		{
			SelectedMenu = Menu.FontColor;
			CatName = "Font Color";
		}

		if(GUI.Button(new Rect(10,90,100,20),"Buttons"))
		{
			SelectedMenu = Menu.ButtonColor;
			CatName = "Button Color";
		}

		if (GUI.Button (new Rect (10, 120, 100, 20), "Windows")) 
		{
			SelectedMenu = Menu.WindowColor;
			CatName = "Window Color";
		}
	}

	//Simple FontColorUI Change Function using normal Public Vars
    void FontColorUI()
	{
		if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
		{
			SelectedMenu = Menu.Color;
			CatName = "Color";
		}

		GUI.Label(new Rect(5, 60, 300, 300), "Red");
		GUI.Label(new Rect(5, 80, 300, 300), "Green");
		GUI.Label(new Rect(5, 100, 300, 300), "Blue");
		GUI.Label(new Rect(5, 120, 300, 300), "Alpha");

        Red = GUI.HorizontalSlider(new Rect(60, 65, 200, 20), Red, 0, 255);
        Green = GUI.HorizontalSlider(new Rect(60, 85, 200, 20), Green, 0, 255);
        Blue = GUI.HorizontalSlider(new Rect(60, 105, 200, 20), Blue, 0, 255);
        Alpha = GUI.HorizontalSlider(new Rect(60, 125, 200, 20), Alpha, 0, 255);

        GUI.Label(new Rect(260, 60, 300, 300), "" + Red.ToString("F0"));
		GUI.Label(new Rect(260, 80, 300, 300), "" + Green.ToString("F0"));
		GUI.Label(new Rect(260, 100, 300, 300), "" + Blue.ToString("F0"));
		GUI.Label(new Rect(260, 120, 300, 300), "" + Alpha.ToString("F0"));

		if (GUI.Button(new Rect(200, 180, 60, 22), "Apply"))
		{
			TestCode.KeywordCheck("Player", "Font Color: " + Red + ":" + Green + ":" + Blue + ":" + Alpha + ";");
		}

	}

    //void FontColorUI()
    //{
    //	if(GUI.Button(new Rect(2,2,20,20),"<-"))
    //	{
    //		SelectedMenu = Menu.Color;
    //		CatName = "Color";
    //	}

    //	GUI.Label(new Rect(5, 60, 300, 300), "Red");
    //	GUI.Label(new Rect(5, 80, 300, 300), "Green");
    //	GUI.Label(new Rect(5, 100, 300, 300), "Blue");
    //	GUI.Label(new Rect(5, 120, 300, 300), "Alpha");

    //	//Setting Float to Color Data
    //       byte red = DataConverter.FloatToByte(Registry.GetFloatColorData("Player", "System", "FontColor").Red);
    //       byte green = DataConverter.FloatToByte(Registry.GetFloatColorData("Player", "System", "FontColor").Green);
    //       byte blue = DataConverter.FloatToByte(Registry.GetFloatColorData("Player", "System", "FontColor").Blue);
    //       byte alpha = DataConverter.FloatToByte(Registry.GetFloatColorData("Player", "System", "FontColor").Alpha);

    //	//Actual Color Data
    //       float red1 = DataConverter.ByteToFloat(Registry.GetColorData("Player", "System", "FontColor").r);
    //       float green1 = DataConverter.ByteToFloat(Registry.GetColorData("Player", "System", "FontColor").g);
    //       float blue1 = DataConverter.ByteToFloat(Registry.GetColorData("Player", "System", "FontColor").b);
    //       float alpha1 = DataConverter.ByteToFloat(Registry.GetColorData("Player", "System", "FontColor").a);

    //       //GUI.Label(new Rect(260, 60, 300, 300), "" + red1.ToString("F0"));
    //       //GUI.Label(new Rect(260, 80, 300, 300), "" + green1.ToString("F0"));
    //       //GUI.Label(new Rect(260, 100, 300, 300), "" + blue1.ToString("F0"));
    //       //GUI.Label(new Rect(260, 120, 300, 300), "" + alpha1.ToString("F0"));

    //       GUI.Label(new Rect(260, 60, 300, 300), "" + red.ToString("F0"));
    //	GUI.Label(new Rect(260, 80, 300, 300), "" + green.ToString("F0"));
    //	GUI.Label(new Rect(260, 100, 300, 300), "" + blue.ToString("F0"));
    //	GUI.Label(new Rect(260, 120, 300, 300), "" + alpha.ToString("F0"));

    //	Registry.SetRedFloatColorData("Player", "System", "FontColor", GUI.HorizontalSlider(new Rect(60, 65, 200, 20), Registry.GetRedFloatColorData("Player", "System", "FontColor"), 0, 255));
    //	Registry.SetGreenFloatColorData("Player", "System", "FontColor", GUI.HorizontalSlider(new Rect(60, 85, 200, 20), Registry.GetGreenFloatColorData("Player", "System", "FontColor"), 0, 255));
    //	Registry.SetBlueFloatColorData("Player", "System", "FontColor", GUI.HorizontalSlider(new Rect(60, 105, 200, 20), Registry.GetBlueFloatColorData("Player", "System", "FontColor"), 0, 255));
    //	Registry.SetAlphaFloatColorData("Player", "System", "FontColor", GUI.HorizontalSlider(new Rect(60, 125, 200, 20), Registry.GetAlphaFloatColorData("Player", "System", "FontColor"), 0, 255));

    //	//Registry.SetRedFloatColorData("Player", "System", "FontColor", GUI.HorizontalSlider(new Rect(60, 65, 200, 20), red1, 0, 255));
    //	//Registry.SetGreenFloatColorData("Player", "System", "FontColor", GUI.HorizontalSlider(new Rect(60, 85, 200, 20), green1, 0, 255));
    //	//Registry.SetBlueFloatColorData("Player", "System", "FontColor", GUI.HorizontalSlider(new Rect(60, 105, 200, 20), blue1, 0, 255));
    //	//Registry.SetAlphaFloatColorData("Player", "System", "FontColor", GUI.HorizontalSlider(new Rect(60, 125, 200, 20), alpha1, 0, 255));

    //	if(GUI.Button(new Rect(220,150,60,22),"set"))
    //	{
    //           TestCode.KeywordCheck("Player", "Font Color: " + red + ":" + blue + ":" + green + ":" + alpha + ";");
    //       }

    //	Customize.cust.FontColorInt = 1;
    //}

    void DesktopColorUI()
	{
		if (GUI.Button(new Rect(2, 2, 20, 20), "<-"))
		{
			SelectedMenu = Menu.Desktop;
			CatName = "Desktop";
		}

		GUI.Label(new Rect(5, 60, 300, 300), "Red");
		GUI.Label(new Rect(5, 80, 300, 300), "Green");
		GUI.Label(new Rect(5, 100, 300, 300), "Blue");
		GUI.Label(new Rect(5, 120, 300, 300), "Alpha");

		GUI.Label(new Rect(260, 60, 300, 300), "" + Registry.GetRedFloatColorData("Player", "System", "DesktopBackgroundColor").ToString("F0"));
		GUI.Label(new Rect(260, 80, 300, 300), "" + Registry.GetGreenFloatColorData("Player", "System", "DesktopBackgroundColor").ToString("F0"));
		GUI.Label(new Rect(260, 100, 300, 300), "" + Registry.GetBlueFloatColorData("Player", "System", "DesktopBackgroundColor").ToString("F0"));
		GUI.Label(new Rect(260, 120, 300, 300), "" + Registry.GetAlphaFloatColorData("Player", "System", "DesktopBackgroundColor").ToString("F0"));

		//Registry.SetRedColorData("Player","System","WindowColor",GUI.HorizontalSlider (new Rect (60, 65, 200, 20), Registry.GetRedColorData("Player", "System", "WindowColor"), 1, 255));
		Registry.SetRedFloatColorData("Player", "System", "DesktopBackgroundColor", GUI.HorizontalSlider(new Rect(60, 65, 200, 20), Registry.GetRedFloatColorData("Player", "System", "DesktopBackgroundColor"), 0, 255));
		Registry.SetGreenFloatColorData("Player", "System", "DesktopBackgroundColor", GUI.HorizontalSlider(new Rect(60, 85, 200, 20), Registry.GetGreenFloatColorData("Player", "System", "DesktopBackgroundColor"), 0, 255));
		Registry.SetBlueFloatColorData("Player", "System", "DesktopBackgroundColor", GUI.HorizontalSlider(new Rect(60, 105, 200, 20), Registry.GetBlueFloatColorData("Player", "System", "DesktopBackgroundColor"), 0, 255));
		Registry.SetAlphaFloatColorData("Player", "System", "DesktopBackgroundColor", GUI.HorizontalSlider(new Rect(60, 125, 200, 20), Registry.GetAlphaFloatColorData("Player", "System", "DesktopBackgroundColor"), 0, 255));


		byte red = DataConverter.FloatToByte(Registry.GetRedFloatColorData("Player", "System", "DesktopBackgroundColor"));
		byte green = DataConverter.FloatToByte(Registry.GetBlueFloatColorData("Player", "System", "DesktopBackgroundColor"));
		byte blue = DataConverter.FloatToByte(Registry.GetGreenFloatColorData("Player", "System", "DesktopBackgroundColor"));
		byte alpha = DataConverter.FloatToByte(Registry.GetAlphaFloatColorData("Player", "System", "DesktopBackgroundColor"));

		TestCode.KeywordCheck("Player", "Desktop Background Color:" + red + ":" + blue + ":" + green + ":" + alpha + ";");
	}

	void WindowColorUI()
	{
		if (GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Color;
			CatName = "Color";
		}

		GUI.Label(new Rect(5,60,300,300),"Red");
		GUI.Label(new Rect(5,80,300,300),"Green");
		GUI.Label(new Rect(5,100,300,300),"Blue");
		GUI.Label(new Rect(5,120,300,300),"Alpha");

		GUI.Label(new Rect(260,60,300,300),"" + Registry.GetRedFloatColorData("Player", "System", "WindowColor").ToString("F0"));
		GUI.Label(new Rect(260,80,300,300),"" + Registry.GetGreenFloatColorData("Player", "System", "WindowColor").ToString("F0"));
		GUI.Label(new Rect(260,100,300,300),"" + Registry.GetBlueFloatColorData("Player", "System", "WindowColor").ToString("F0"));
		GUI.Label(new Rect(260,120,300,300),"" + Registry.GetAlphaFloatColorData("Player", "System", "WindowColor").ToString("F0"));

		//Registry.SetRedColorData("Player","System","WindowColor",GUI.HorizontalSlider (new Rect (60, 65, 200, 20), Registry.GetRedColorData("Player", "System", "WindowColor"), 1, 255));
		Registry.SetRedFloatColorData("Player","System","WindowColor",GUI.HorizontalSlider(new Rect(60, 65, 200, 20), Registry.GetRedFloatColorData("Player", "System", "WindowColor"), 0, 255));
		Registry.SetGreenFloatColorData("Player", "System", "WindowColor", GUI.HorizontalSlider(new Rect(60, 85, 200, 20), Registry.GetGreenFloatColorData("Player", "System", "WindowColor"), 0, 255));
		Registry.SetBlueFloatColorData("Player", "System", "WindowColor", GUI.HorizontalSlider(new Rect(60, 105, 200, 20), Registry.GetBlueFloatColorData("Player", "System", "WindowColor"), 0, 255));
		Registry.SetAlphaFloatColorData("Player", "System", "WindowColor", GUI.HorizontalSlider(new Rect(60, 125, 200, 20), Registry.GetAlphaFloatColorData("Player", "System", "WindowColor"), 0, 255));


		byte red = DataConverter.FloatToByte(Registry.GetRedFloatColorData("Player", "System", "WindowColor"));
		byte green = DataConverter.FloatToByte(Registry.GetBlueFloatColorData("Player", "System", "WindowColor"));
		byte blue = DataConverter.FloatToByte(Registry.GetGreenFloatColorData("Player", "System", "WindowColor"));
		byte alpha = DataConverter.FloatToByte(Registry.GetAlphaFloatColorData("Player", "System", "WindowColor"));

		TestCode.KeywordCheck("Player", "Window Color: " + red + ":" + blue + ":" +  green + ":" + alpha + ";");

		Customize.cust.WindowColorInt = 3;
	}

	void ButtonColorUI()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Color;
			CatName = "Color";
		}

		GUI.Label(new Rect(5,60,300,300),"Red");
		GUI.Label(new Rect(5,80,300,300),"Green");
		GUI.Label(new Rect(5,100,300,300),"Blue");
		GUI.Label(new Rect(5,120,300,300),"Alpha");

		GUI.Label(new Rect(260, 60, 300, 300), "" + Registry.GetRedFloatColorData("Player", "System", "ButtonColor").ToString("F0"));
		GUI.Label(new Rect(260, 80, 300, 300), "" + Registry.GetGreenFloatColorData("Player", "System", "ButtonColor").ToString("F0"));
		GUI.Label(new Rect(260, 100, 300, 300), "" + Registry.GetBlueFloatColorData("Player", "System", "ButtonColor").ToString("F0"));
		GUI.Label(new Rect(260, 120, 300, 300), "" + Registry.GetAlphaFloatColorData("Player", "System", "ButtonColor").ToString("F0"));

		//Registry.SetRedColorData("Player","System","WindowColor",GUI.HorizontalSlider (new Rect (60, 65, 200, 20), Registry.GetRedColorData("Player", "System", "WindowColor"), 1, 255));
		Registry.SetRedFloatColorData("Player", "System", "ButtonColor", GUI.HorizontalSlider(new Rect(60, 65, 200, 20), Registry.GetRedFloatColorData("Player", "System", "ButtonColor"), 0, 255));
		Registry.SetGreenFloatColorData("Player", "System", "ButtonColor", GUI.HorizontalSlider(new Rect(60, 85, 200, 20), Registry.GetGreenFloatColorData("Player", "System", "ButtonColor"), 0, 255));
		Registry.SetBlueFloatColorData("Player", "System", "ButtonColor", GUI.HorizontalSlider(new Rect(60, 105, 200, 20), Registry.GetBlueFloatColorData("Player", "System", "ButtonColor"), 0, 255));
		Registry.SetAlphaFloatColorData("Player", "System", "ButtonColor", GUI.HorizontalSlider(new Rect(60, 125, 200, 20), Registry.GetAlphaFloatColorData("Player", "System", "ButtonColor"), 0, 255));


        byte red = DataConverter.FloatToByte(Registry.GetRedFloatColorData("Player", "System", "ButtonColor"));
		byte green = DataConverter.FloatToByte(Registry.GetBlueFloatColorData("Player", "System", "ButtonColor"));
		byte blue = DataConverter.FloatToByte(Registry.GetGreenFloatColorData("Player", "System", "ButtonColor"));
		byte alpha = DataConverter.FloatToByte(Registry.GetAlphaFloatColorData("Player", "System", "ButtonColor"));

		TestCode.KeywordCheck("Player", "Button Color: " + red + ":" + blue + ":" + green + ":" + alpha + ";");

		Customize.cust.ButtonColorInt = 2;
	}

	void ScreenUI()
	{
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Display;
			CatName = "Display";
		}

		scrollpos = GUI.BeginScrollView(new Rect(5, 60, 100, 100), scrollpos, new Rect(0, 0, 0, Res*20));
		for (Res = 0; Res < Rez.Count; Res++) 
		{
			if(GUI.Button(new Rect(2, Res * 20, 80, 20), "" + Rez[Res].RezName))
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
                Customize.cust.RezX = Rez[Res].RezX;
				Customize.cust.RezY = Rez[Res].RezY;
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
		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Desktop;
			CatName = "Desktop";
		}

		if(GUI.Button(new Rect(60,30,100,21),"Settings"))
		{
			SelectedMenu = Menu.BackgroundSettings;
		}

		GUI.contentColor = Color.white;

		scrollpos = GUI.BeginScrollView(new Rect(5, 60, 275, 130), scrollpos, new Rect(0, 0, 0, scrollsize*64));
		for (scrollsize = 0; scrollsize < os.ListOfBackgroundImages.Count; scrollsize++)
		{
			if(GUI.Button(new Rect(0, scrollsize * 64, 275, 64), os.ListOfBackgroundImages[scrollsize], GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [DesktopStyle]))
			{
				//Registry.SetTextrue2DData("Player", "ControlPanel", "BackgroundAddress", os.ListOfBackgroundImages[selec]);
				Registry.SetIntData("Player","System", "SelectedBackground",scrollsize);
			}
		}
		GUI.EndScrollView();
	}

	void BackgroundSettings()
	{
		if(Registry.GetStringData("Player", "ControlPanel", "BackgroundField") == null)
        {
			Registry.SetStringData("Player", "ControlPanel", "BackgroundField","");
		}

		if (FilePathData.FileSelected == true)
		{
			Registry.SetStringData("Player", "ControlPanel", "BackgroundField",FilePathData.Path);
			FilePathData.GetSelectedFile();
		}

		if (GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Background;
		}

		if(GUI.Button(new Rect(65,30,70,21),"Apply"))
		{
			ApplyBackgrounds();
		}

		if (GUI.Button(new Rect(65, 60, 70, 21), "Open FE"))
		{
			OpenFileExplorerBackground();
		}

		if(Registry.GetStringData("Player", "ControlPanel", "BackgroundField") == "" || Registry.GetStringData("Player", "ControlPanel", "BackgroundField") == null)
        {
			Registry.SetStringData("Player", "ControlPanel", "BackgroundField", "");
		}

		GUI.Label(new Rect(100, 120, 150, 21),"Current Aspect: " + Registry.GetStringData("Player", "System", "Aspect"));

        if (Registry.GetStringData("Player", "System", "Aspect") == "Fit")
        {
            if (GUI.Button(new Rect(2, 120, 70, 21), "Scale"))
            {
                Registry.SetStringData("Player", "System", "Aspect", "Scale");
            }
        }
        if (Registry.GetStringData("Player", "System", "Aspect") == "Scale")
        {
            if (GUI.Button(new Rect(2, 120, 70, 21), "Default"))
            {
                Registry.SetStringData("Player", "System", "Aspect", "Default");
            }
        }
        if (Registry.GetStringData("Player", "System", "Aspect") == "Default" || Registry.GetStringData("Player", "System", "Aspect") == "")
        {
            if (GUI.Button(new Rect(2, 120, 70, 21), "Fit"))
            {
                Registry.SetStringData("Player", "System", "Aspect", "Fit");
            }
        }

        if (GUI.Button(new Rect(2, 150, 70, 21), "Toggle"))
		{
			if (Registry.GetBoolData("Player", "System", "SelectedBackground") == false)
				Registry.SetBoolData("Player", "System", "SelectedBackground", true);
			else
				Registry.SetBoolData("Player", "System", "SelectedBackground", false);
		}

		Registry.SetStringData("Player", "ControlPanel", "BackgroundField", GUI.TextField(new Rect(2, 182, 296, 21), Registry.GetStringData("Player", "ControlPanel", "BackgroundField")));
	}

	void QuickLaunch()
	{
		GUI.Label(new Rect(10,180,300,100), Status);

		if(GUI.Button(new Rect(2,2,20,20),"<-"))
		{
			SelectedMenu = Menu.Home;
			CatName = "Home";
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
			if (GameControl.control.ProgramFiles[ScanCount].Extension == ProgramSystem.FileExtension.Exe)
			{
				SelectablePrograms.Add(GameControl.control.ProgramFiles[ScanCount]);
			}
		}
	}

	void QuickLaunchCheck()
	{
		//if (GameControl.control.QuickProgramList.Count > 0)
		//{
		//	if (!GameControl.control.QuickLaunchNames.Contains (SelectablePrograms[Selecting].Name))
		//	{
		//		GameControl.control.QuickProgramList.Add (SelectablePrograms [Selecting]);
		//		GameControl.control.QuickLaunchNames.Add(SelectablePrograms [Selecting].Name);
		//		Status = "Added " + SelectablePrograms [Selecting].Name + " to quick launch list";
		//	} 
		//	else 
		//	{
		//		GameControl.control.QuickProgramList.Remove (SelectablePrograms [Selecting]);
		//		GameControl.control.QuickLaunchNames.Remove(SelectablePrograms [Selecting].Name);
		//		Status = "Removed " + SelectablePrograms [Selecting].Name + " from quick launch list";
		//	}
		//} 
		//else 
		//{
		//	GameControl.control.QuickProgramList.Add (SelectablePrograms [Selecting]);
		//	GameControl.control.QuickLaunchNames.Add(SelectablePrograms [Selecting].Name);
		//	Status = "Added " + SelectablePrograms [Selecting].Name + " to quick launch list";
		//}
	}
}
