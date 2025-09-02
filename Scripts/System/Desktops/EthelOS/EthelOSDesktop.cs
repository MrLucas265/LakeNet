using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EthelOSDesktop : MonoBehaviour 
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
	private Clock clock;
	private TaskViewer TaskView;

	public bool ShowShutdown;

	public int windowID;
	private Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;

	private Rect DesktopEnvElement;

	private Rect Group;

	private Rect AppButton;
	public Rect SearchBox;
	public Rect SearchBoxBG;
	private Rect GatewayButton;
	private Rect NetButton;
	private Rect EmailButton;
	private Rect InfoButton;
	private Rect PlayerButton;

	public Rect TaskBarHozScroll;
	private Rect TaskBarAppButton;
	public float Math;

	private Rect Notepad;
	private Rect Map;
	private Rect SysInfo;
	private Rect Console;
	private Rect LogoutButton;
	private Rect SettingsButton;
	private Rect ShowAllButton;

	private Rect SearchButton;
	public Rect SearchBar;

	private Rect QuickList;


	private  Rect AppMenuButtons;

	public float SystemButtonsY;

	public Rect ClockDisplay;
	public Rect Hint;

	public int boostAmt;

	private Rect AppMenuSelectArea;
	private Rect AppMenuBgPos = new Rect(5,184,235,300);

	public Rect SearchList;


	private Rect TaskBarBG;

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

	private AppMenu appmenu;

	public List<string> ListOfTargets = new List<string>();

	public int SelectedWindow;

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
		clock = SysSoftware.GetComponent<Clock>();
		appmenu = SysSoftware.GetComponent<AppMenu>();
		TaskView = SysSoftware.GetComponent<TaskViewer>();

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
	}

	public void UpdateUI()
	{
		DesktopEnvElement.width = 4000;
		DesktopEnvElement.height = 1000;
		DesktopEnvElement.y = native_height = Screen.height - 64 * Customize.cust.UIScale;
		AppMenuBgPos.y = DesktopEnvElement.y - 300;

		Group.height = 21 * Customize.cust.UIScale;
		Group.width = 100 * Customize.cust.UIScale;
		AppMenuBgPos.width = 235 * Customize.cust.UIScale;

		AppMenuSelectArea = new Rect(0,0,AppMenuBgPos.width,AppMenuBgPos.height + 50);

		Scale = Customize.cust.UIScale;

		AppButton = new Rect(1 * Scale,DesktopY + 4,Group.width,Group.height+8);
		SearchBox = new Rect(AppButton.x + Group.width * Scale, DesktopY + 4, Group.width, Group.height + 8);
		SearchBoxBG = new Rect(AppButton.x + Group.width * Scale, Screen.height-200, 200, 200);
		TaskBarBG = new Rect(0 * Scale, DesktopY + 3, Screen.width, Group.height+11);
		ClockDisplay = new Rect(Screen.width-Group.width-3, DesktopY + 4, Group.width, Group.height+8);
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
		SearchList = new Rect (2 + AppButton.x + Group.width * Scale, Screen.height - 198, 195, 165);

		ButtonHeight = Notepad.height;
		//Hint = new Rect(ClockX-350,DesktopY+12,300,22);

		SpeakerX = Screen.width - 113;
		SpeakerButton = new Rect (SpeakerX,DesktopY,35 * Scale,35 * Scale);
		VolumeBar = new Rect (SpeakerX+5, 6, 100, 10);
		VolumeText = new Rect (SpeakerX-35, 2, 100, 100);

		Math = ClockDisplay.x - (SearchBox.x + SearchBox.width) + 1;
		TaskBarHozScroll = new Rect(SearchBox.x + Group.width * Scale, SearchBox.y, Math, Group.height + 8);

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

	void UpdateSiteList()
	{
		ListOfSites.Clear();
		ListOfTargets.Clear();

		for (int b = 0; b < GameControl.control.ProgramFiles.Count; b++)
		{
			if (GameControl.control.ProgramFiles[b].Extension == ProgramSystem.FileExtension.Exe)
			{
				ListOfSites.Add(GameControl.control.ProgramFiles[b].Name);
				ListOfTargets.Add(GameControl.control.ProgramFiles[b].Target);
			}
		}
	}

	void SearchCheck()
	{
		for (SearchCount = 0; SearchCount < ListOfSites.Count; SearchCount++)
		{
			if (!ListOfSites[SearchCount].ToLower().Contains(Inputted.ToLower()))
			{
				ListOfSites.RemoveAt(SearchCount);
				ListOfTargets.RemoveAt(SearchCount);
			}
		}
	}

	void ActivateSearch()
	{
		if (SearchSites != "")
		{
			UpdateSiteList();
			SearchDone = false;
			Inputted = SearchSites;
			UpdateSearchUI = true;
		}
	}

	void SearchUI()
	{

		if (SearchSites == "")
		{
			UpdateSearchUI = false;
		}

		if (UpdateSearchUI == true)
		{
			if (Inputted != "")
			{
				for (scrollsize = 0; scrollsize < ListOfSites.Count; scrollsize++)
				{
					SearchCheck();
				}
			}
			scrollpos = GUI.BeginScrollView(new Rect(SearchList), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < ListOfSites.Count; scrollsize++)
			{
				if (Inputted != "" && ListOfSites.Count >= 1)
				{
					if (GUI.Button(new Rect(1 * Scale, 1 + scrollsize * 21, 150 * Scale, 20), ListOfSites[scrollsize]))
					{
						appman.ProgramName = ListOfSites[scrollsize];
						appman.SelectedApp = ListOfTargets[scrollsize];
					}
				}
			}
			GUI.EndScrollView();
		}
	}

	void OnGUI()
	{
		GUI.depth = -30;
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");

		GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

		if (!SearchBoxBG.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0))
		{
			SearchSites = "";
		}
		if (!SearchBoxBG.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(1))
		{
			SearchSites = "";
		}
		if (!SearchBoxBG.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(2))
		{
			SearchSites = "";
		}

		if (SearchSites != "")
		{
			GUI.Box(new Rect(SearchBoxBG), "");

			ActivateSearch();

			SearchUI();
			if(appmenu.show == true)
			{
				appman.SelectedApp = "Start Menu";
			}

		}

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

		//FontUpdate();

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

	void TaskBarUI()
	{
		for (int i = 0; i < TaskView.RunningTasks.Count; i++)
		{
			if(TaskBarHozScroll.x + TaskBarHozScroll.x / 2 * i + 1 <= Math)
			{
				if(i == 0)
				{
					if (GUI.Button(new Rect(TaskBarHozScroll.x + TaskBarHozScroll.x * i + 1, TaskBarHozScroll.y, 100 * Scale, TaskBarHozScroll.height), TaskView.RunningTasks[i].ProgramName))
					{
						SelectedWindow = TaskView.RunningTasks[i].RunningApplicationsWindowID;
						GUI.FocusWindow(SelectedWindow);
						GUI.BringWindowToFront(SelectedWindow);
					}
				}
				else
				{
					if (GUI.Button(new Rect(TaskBarHozScroll.x + TaskBarHozScroll.x / 2 * i + 1, TaskBarHozScroll.y, 100 * Scale, TaskBarHozScroll.height), TaskView.RunningTasks[i].ProgramName))
					{
						SelectedWindow = TaskView.RunningTasks[i].RunningApplicationsWindowID;
						GUI.FocusWindow(SelectedWindow);
						GUI.BringWindowToFront(SelectedWindow);
					}
				}
			}
		}
	}

	void ClockUI()
	{
		if (GUI.Button(new Rect(ClockDisplay), GameControl.control.Time.TodaysDate + "\n" + GameControl.control.Time.CurrentTime, GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[4]))
		{
			sc.SoundSelect = 3;
			sc.PlaySound();
			appman.SelectedApp = "Start Menu";
		}
	}

	void SpeakerUI()
	{
		if (ShowVolume == true)
		{
			float TempVol = Customize.cust.Volume * 100;
			Customize.cust.Volume = GUI.HorizontalSlider (new Rect (VolumeBar), Customize.cust.Volume, 0, 1);
			GUI.Label (new Rect(VolumeText),"%" + TempVol.ToString("F0"));
		}

		if (GUI.Button (new Rect (SpeakerButton),desk.SpeakerIcon,GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[DesktopStyle])) 
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
		//GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		//GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
		//GUI.color = Color.blue;
		//SpeakerUI();

		//GUI.TextArea(new Rect (Hint), com.Hint);

		GUI.Box(new Rect(TaskBarBG), "");

		SearchSites = GUI.TextField(new Rect(SearchBox), SearchSites);

		ClockUI();

		if (appmenu.show == true)
		{
			if (GUI.Button(new Rect(AppButton), "Appatures", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[4]))
			{
				sc.SoundSelect = 3;
				sc.PlaySound();
				appman.SelectedApp = "Start Menu";
			}
		}
		else
		{
			if (GUI.Button(new Rect(AppButton), "Appatures", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[DesktopStyle]))
			{
				sc.SoundSelect = 3;
				sc.PlaySound();
				appman.SelectedApp = "Start Menu";
			}
		}

		TaskBarUI();

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