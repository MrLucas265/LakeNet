﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeOSDesktop : MonoBehaviour 
{
	private GameObject HackingSoftware;
	private GameObject AppSoftware;
	private GameObject SysSoftware;
	//private GameObject Computer;

	public bool show;

	public bool updateUI;
	float Scale;
	public bool ShowAllApps;

	public string ProgramName;


	public bool showApplications;
	public bool showBrowsers;

	// public bool showHacks;
	// public bool showTools;
	//public bool showBypass;
	// public bool showSec;
	//public bool showLanTools;
	//public bool showOther;
	//public bool showHardwareDrivers;
	//public bool showHUD;

	public bool showCatWeb;
	public bool showCatCon;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;
	public int Select;

	public bool MilitaryTime;
	public bool BootTime;

	public float MTF;
	public string MTS;

	private Computer com;
	private InternetBrowser ib;
	private Notepad note;
	private MissionBrow mb;
	private CurContracts cc;
	private SiteList sl;
	private AccLog al;
	private Tracer trace;
	private Descy cy;
	private DirSearch ds;
	private Favs fav;
	private TreeView tv;
	private SystemMap sm;
	private MonitorBypass mPass;
	private WebSecViewer wsv;
	private ShutdownProm sdp;
	private Desktop1 dsk1;
	private Clock clk;
	private Defalt defalt;
	private SoundControl sc;
	private AppMenu am;
	private AudioSource Audio;
	private AppMan appman;
	private Boot boot;
	private POST post;
	private DesktopEnviroment os;
	private Mouse mouse;
	private Desktop desk;

	public bool ShowShutdown;

	public int windowID;
	private Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;

	private Rect DesktopEnvElement;

	private Rect Group;

	private Rect AppButton;
	private Rect GatewayButton;
	private Rect NetButton;
	private Rect EmailButton;
	private Rect InfoButton;
	private Rect PlayerButton;

	private Rect Notepad;
	private Rect Map;
	private Rect SysInfo;
	private Rect Console;
	private Rect LogoutButton;
	private Rect SettingsButton;
	private  Rect ShowAllButton;

	private Rect SearchButton;
	private Rect SearchBar;

	private Rect QuickList;


	private  Rect AppMenuButtons;

	public float SystemButtonsY;

	public Rect Clock;
	public Rect Hint;

	public int boostAmt;

	private Rect AppMenuSelectArea;
	private Rect AppMenuBgPos = new Rect(5,184,235,300);

	private Rect SearchList;


	public List<string> ListOfSites = new List<string>();
	public string SearchSites;
	public string Searched;
	public string Inputted;
	public bool SearchDone;
	public int SearchCount;
	public bool UpdateSearchUI;

	public float ButtonHeight;

	public float ClockX;

	public int FontSize;

	int DesktopStyle = 3;

	public float SpeakerX;
	public Rect SpeakerButton;
	public Rect VolumeBar;
	public Rect VolumeText;
	public bool ShowVolume;

	public float DesktopY;
	public float Timer;
	public float StartTime;
	public bool Snap;

	// Use this for initialization
	void Start () 
	{
		AppSoftware = GameObject.Find("Applications");
		HackingSoftware = GameObject.Find("Hacking");
		SysSoftware = GameObject.Find("System");
		//Computer = GameObject.Find("Computer");

		//System
		clk = SysSoftware.GetComponent<Clock>();
		defalt = SysSoftware.GetComponent<Defalt>();
		am = SysSoftware.GetComponent<AppMenu>();
		com = SysSoftware.GetComponent<Computer>();
		sc = SysSoftware.GetComponent<SoundControl>();
		appman = SysSoftware.GetComponent<AppMan>();
		boot = SysSoftware.GetComponent<Boot>();
		post = SysSoftware.GetComponent<POST>();
		os = SysSoftware.GetComponent<DesktopEnviroment>();
		mouse = SysSoftware.GetComponent<Mouse>();
		desk = SysSoftware.GetComponent<Desktop>();

		//Applications
		al = AppSoftware.GetComponent<AccLog>();
		sm = AppSoftware.GetComponent<SystemMap>();
		ib = AppSoftware.GetComponent<InternetBrowser>();

		//Hacking
		trace = HackingSoftware.GetComponent<Tracer>();
		cy = HackingSoftware.GetComponent<Descy>();
		ds = HackingSoftware.GetComponent<DirSearch>();

		mb = GetComponent<MissionBrow>();
		cc = GetComponent<CurContracts>();
		sl = GetComponent<SiteList>();
		note = GetComponent<Notepad>();
		fav = GetComponent<Favs>();
		tv = GetComponent<TreeView>();
		mPass = GetComponent<MonitorBypass>();
		wsv = GetComponent<WebSecViewer>();
		sdp = GetComponent<ShutdownProm>();
		Audio = GetComponent<AudioSource>();

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		Audio.volume = Customize.cust.Volume;
		DesktopY = 75;
		StartTime = 0.030f;
		Snap = true;
	}

	public void UpdateUI()
	{
		DesktopEnvElement.width = 4000;
		DesktopEnvElement.height = 1000;
		DesktopEnvElement.y = native_height = Screen.height - 64 * Customize.cust.UIScale;
		AppMenuBgPos.y = DesktopEnvElement.y - 300;

		Group.width = Group.height = 35 * Customize.cust.UIScale;
		AppMenuBgPos.width = 235 * Customize.cust.UIScale;

		AppMenuSelectArea = new Rect(0,0,AppMenuBgPos.width,AppMenuBgPos.height + 50);

		Scale = Customize.cust.UIScale;

		AppButton = new Rect(1 * Scale,DesktopY,Group.width,Group.height);
		GatewayButton = new Rect(AppButton.x + Group.width * Scale,DesktopY,Group.width,Group.height);
		NetButton = new Rect(GatewayButton.x + Group.width * Scale,DesktopY,Group.width,Group.height);
		InfoButton = new Rect(NetButton.x + Group.width * Scale,DesktopY,Group.width,Group.height);
		EmailButton = new Rect(InfoButton.x + Group.width * Scale,DesktopY,Group.width,Group.height);
		PlayerButton = new Rect(EmailButton.x + Group.width * Scale,DesktopY,Group.width,Group.height);

		AppMenuButtons = new Rect(5 * Scale,scrollsize * 20 * Scale,100*Scale,20*Scale);

		Notepad = new Rect(5* Scale, 5 * Scale, 100* Scale, 20* Scale);
		Console = new Rect(5* Scale, 35 * Scale, 100* Scale, 20* Scale);
		SysInfo = new Rect(5* Scale, 65 * Scale, 100* Scale, 20* Scale);
		Map = new Rect(5* Scale, 95 * Scale,100* Scale, 20* Scale);

		SystemButtonsY = Group.y + 265;

		LogoutButton = new Rect(210 * Scale, SystemButtonsY,20 * Scale,20 * Scale);
		SettingsButton = new Rect (210 * Scale, SystemButtonsY - 32, 20 * Scale, 20 * Scale);

		SearchButton = new Rect(85 * Scale,SystemButtonsY,20 * Scale,20 * Scale);
		SearchBar = new Rect(5 * Scale,SystemButtonsY,75 * Scale,20 * Scale);

		ShowAllButton = new Rect(5 * Scale,SystemButtonsY - 32,100 * Scale,20 * Scale);

		//QuickList = new Rect (105 * Scale + 23f,0 * Scale,125 * Scale - 22.5f,220 * Scale);
		QuickList = new Rect (105 * Scale + 23f, 0 * Scale, 125 * Scale - 22.5f, SystemButtonsY - 45);
		//10, 30, 230, 300
		SearchList = new Rect (5,5,175,250);

		ButtonHeight = Notepad.height;

		ClockX = Screen.width - 75;
		Clock = new Rect(ClockX,DesktopY-2,70,37);
		//Hint = new Rect(ClockX-350,DesktopY+12,300,22);

		SpeakerX = Screen.width - 113;
		SpeakerButton = new Rect (SpeakerX,DesktopY,35 * Scale,35 * Scale);
		VolumeBar = new Rect (SpeakerX+5, 6, 100, 10);
		VolumeText = new Rect (SpeakerX-35, 2, 100, 100);

		//5, 250, 100, 20
		updateUI = false;
	}

	void PlayClickSound()
	{
		sc.SoundSelect = 3;
		sc.PlaySound();
	}

	void PlayHoverSound()
	{
		sc.SoundSelect = 4;
		sc.PlaySound();
	}

	void FontUpdate()
	{
		FontSize = Customize.cust.FontSize;
		GUI.skin.label.fontSize = FontSize;
		GUI.skin.button.fontSize = FontSize;
		GUI.skin.textArea.fontSize = FontSize;
		GUI.skin.textField.fontSize = FontSize;
		GUI.skin.box.fontSize = FontSize;
	}

	void OnGUI()
	{
		GUI.depth = -30;
		GUI.skin = com.Skin[GameControl.control.GUIID];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//float rx = native_width;
		//float ry = native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1));

		if (DesktopY <= 29)
		{
			mouse.ShowMouse = true;
		}

		if (boot.enabled == false && post.enabled == false)
		{
			GUI.BeginGroup(new Rect(DesktopEnvElement));
			DesktopEnv();
			GUI.EndGroup();
			if (DesktopY != 29 && Snap == false)
			{
				Timer -= 1 * Time.deltaTime;

				if (Timer <= 0)
				{
					DesktopY--;
					UpdateUI();
					Timer = StartTime;
				}
			}

			if (Snap == true)
			{
				DesktopY = 29;
				UpdateUI();
			}
		}

		FontUpdate();

		if (LogoutButton.Contains (Event.current.mousePosition)) 
		{
			com.Hint = "Put your gateway on standby";
		}

		//		if(GUI.Button(new Rect(CMDButton),CMDIcon))
		//		{
		//			defalt.showcmd = !defalt.showcmd;
		//		}

		if (Console.Contains (Event.current.mousePosition)) 
		{
			com.Hint = "Launch the system command interface";
		}

		if (PlayerButton.Contains (Event.current.mousePosition)) 
		{
			com.Hint = "Launch rep and balance tracker";
		}

		//		if(GUI.Button(new Rect(HardwareButton),HardIcon))
		//		{
		//			hd.show = !hd.show;
		//		}
		if (SysInfo.Contains (Event.current.mousePosition)) 
		{
			com.Hint = "Launch system info";
		}

		//		if(GUI.Button(new Rect(MapButton),MapIcon))
		//		{
		//			sm.show =! sm.show;
		//		}

		if (Map.Contains (Event.current.mousePosition)) 
		{
			com.Hint = "Launch communications map";
		}
	}

	void Update()
	{

	}

	void ClockUI()
	{
		//if (ProfileController.procon.Hour >= 12) 
		//{
		//	MTS = " PM";
		//} 
		//else 
		//{
		//	MTS = " AM";
		//}

		//if (ProfileController.procon.Hour < 13 && MilitaryTime == false)
		//{
		//	GUI.TextArea (new Rect (Clock), "" + ProfileController.procon.Hour.ToString ("00") + ":" + ProfileController.procon.Min.ToString ("00") + MTS + "\n" + ProfileController.procon.Day.ToString ("F0") + "" + "/" + ProfileController.procon.Month.ToString("00") + "/" + ProfileController.procon.CurYear);
		//} 
		//if (ProfileController.procon.Hour >= 13 && MilitaryTime == false)
		//{
		//	MTF = ProfileController.procon.Hour;
		//	MTF -= 12;
		//	GUI.TextArea (new Rect (Clock), "" + MTF.ToString("00") + ":" + ProfileController.procon.Min.ToString("00") + MTS + "\n" + ProfileController.procon.Day.ToString("F0") + "" + "/" + ProfileController.procon.Month.ToString("00") +  "/" + ProfileController.procon.CurYear);
		//}
		//if (MilitaryTime == true || BootTime == true)
		//{
		//	GUI.TextArea (new Rect (Clock), "" + ProfileController.procon.Hour.ToString("00") + ":" + ProfileController.procon.Min.ToString("00") + "\n" + ProfileController.procon.Day.ToString("F0") + "" + "/" + ProfileController.procon.Month.ToString("00") +  "/" + ProfileController.procon.CurYear);
		//}
	}

	void SpeakerUI()
	{
		if (ShowVolume == true)
		{
			float TempVol = Customize.cust.Volume * 100;
			Customize.cust.Volume = GUI.HorizontalSlider (new Rect (VolumeBar), Customize.cust.Volume, 0, 1);
			GUI.Label (new Rect(VolumeText),"%" + TempVol.ToString("F0"));
		}

		if (GUI.Button (new Rect (SpeakerButton),desk.SpeakerIcon,com.Skin[GameControl.control.GUIID].customStyles[DesktopStyle])) 
		{
			ShowVolume = !ShowVolume;
		}

		if (Input.GetMouseButtonDown(0) && !SpeakerButton.Contains(Event.current.mousePosition) && !VolumeBar.Contains(Event.current.mousePosition))
		{
			ShowVolume = false;
		}

		sc.SetVolume();
	}

	public void DesktopEnv()
	{
		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		//GUI.contentColor = com.colors[Customize.cust.FontColorInt];
		//GUI.color = Color.blue;

		ClockUI();
		SpeakerUI();

		//GUI.TextArea(new Rect (Hint), com.Hint);

		if (GUI.Button (new Rect (AppButton), desk.ApplicationsIcon,com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
		{
			sc.SoundSelect = 3;
			sc.PlaySound();
			appman.SelectedApp = "Start Menu";
		}

		if(new Rect(AppButton).Contains (Event.current.mousePosition)) 
		{
			com.Hint = "Application Launcher";
		}

		if(GUI.Button(new Rect(GatewayButton),desk.GatewayIcon,com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
		{
			PlayClickSound();
            for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
            {
                if (GameControl.control.ProgramFiles[i].Target == "Computer")
                {
                    appman.ProgramName = GameControl.control.ProgramFiles[i].Name;
                    appman.SelectedApp = GameControl.control.ProgramFiles[i].Target;
                }
            }
            show = false;
		}
		if (new Rect(GatewayButton).Contains (Event.current.mousePosition)) 
		{
			com.Hint = "this is your gateway";
		}

		if(GUI.Button(new Rect(NetButton),desk.InternetIcon,com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
		{
			PlayClickSound();
            for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
            {
                if (GameControl.control.ProgramFiles[i].Target == "Net Viewer")
                {
                    appman.ProgramName = GameControl.control.ProgramFiles[i].Name;
                    appman.SelectedApp = GameControl.control.ProgramFiles[i].Target;
                }
            }
            show = false;
		}

		if(GUI.Button(new Rect(EmailButton),desk.EmailIcon,com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
		{
			PlayClickSound();
            for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
            {
                if (GameControl.control.ProgramFiles[i].Target == "Email")
                {
                    appman.ProgramName = GameControl.control.ProgramFiles[i].Name;
                    appman.SelectedApp = GameControl.control.ProgramFiles[i].Target;
                }
            }
            show = false;
		}

		if(GUI.Button(new Rect(InfoButton),desk.InfoIcon,com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
		{
			PlayClickSound();
			show = false;
		}

		if(GUI.Button(new Rect(PlayerButton),GameControl.control.UserPic[GameControl.control.ProfilePicID],com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
		{
			PlayClickSound();
			show = false;
		}

		if (new Rect(NetButton).Contains (Event.current.mousePosition)) 
		{
			com.Hint = "Internet Browser";
		}

		if (new Rect(EmailButton).Contains (Event.current.mousePosition)) 
		{
			com.Hint = "This is your email";
		}
		if (new Rect(InfoButton).Contains (Event.current.mousePosition)) 
		{
			com.Hint = "information about software and hardware";
		}

		if (!GatewayButton.Contains (Event.current.mousePosition) 
			&& !AppButton.Contains (Event.current.mousePosition) 
			&& !NetButton.Contains (Event.current.mousePosition) 
			&& !EmailButton.Contains (Event.current.mousePosition)
			&& !InfoButton.Contains (Event.current.mousePosition) 
			&& !LogoutButton.Contains (Event.current.mousePosition) 
			&& !SysInfo.Contains(Event.current.mousePosition) 
			&& !Console.Contains(Event.current.mousePosition)
			&& !PlayerButton.Contains(Event.current.mousePosition)
			&& !Map.Contains(Event.current.mousePosition))
		{
			com.Hint = "";
		}	
	}
}