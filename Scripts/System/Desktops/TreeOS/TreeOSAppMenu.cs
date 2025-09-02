using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOSAppMenu : MonoBehaviour
{
	private GameObject HackingSoftware;
	private GameObject AppSoftware;
	private GameObject SysSoftware;
	private GameObject Computer;

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
	private AppMenu appmenu;

	private AppMan appman;

	public int windowID;
	public  Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;

	public bool show;

	public bool updateUI;
	float Scale;
	public bool ShowAllApps;

	public string ProgramName;

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
	private Rect ShowAllButton;

	private Rect SearchButton;
	private Rect SearchBar;

	private Rect QuickList;



	private  Rect AppMenuButtons;

	public float SystemButtonsY;

	private Rect AppMenuSelectArea;
	public Rect AppMenuBgPos;

	private Rect SearchList;


	public List<string> ListOfSites = new List<string>();
	public List<string> ListOfTargets = new List<string>();

	public List<string> ListOfPrograms = new List<string>();
	public List<string> ListOfProgramTargets = new List<string>();

	public string SearchSites;
	public string Searched;
	public string Inputted;
	public bool SearchDone;
	public int SearchCount;
	public bool UpdateSearchUI;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;
	public int Select;

	public float ButtonHeight;

	int DesktopStyle = 3;

	public Texture2D GatewayIcon;
	public Texture2D ApplicationsIcon;
	public Texture2D InternetIcon;
	public Texture2D EmailIcon;
	public Texture2D InfoIcon;
	public Texture2D LogoutIcon;
	public Texture2D CMDIcon;
	public Texture2D HardIcon;
	public Texture2D MapIcon;
	public Texture2D HUDAnaIcon;
	public Texture2D SearchIcon;
	public Texture2D SettingsIcon;
	public bool ShowShutdown;

	public float X;
	public int AppMenuState;

	public float Speed;

	public float Timer;
	public float Cooldown;

	void Start () 
	{
		SysSoftware = GameObject.Find("System");
		AppSoftware = GameObject.Find("Applications");
		HackingSoftware = GameObject.Find("Hacking");

		com = SysSoftware.GetComponent<Computer>();
		ib = AppSoftware.GetComponent<InternetBrowser>();
		note = AppSoftware.GetComponent<Notepad>();
		al = SysSoftware.GetComponent<AccLog>();

		trace = HackingSoftware.GetComponent<Tracer>();

		cy = SysSoftware.GetComponent<Descy>();
		ds = SysSoftware.GetComponent<DirSearch>();
		tv = SysSoftware.GetComponent<TreeView>();
		al = AppSoftware.GetComponent<AccLog>();
		sm = AppSoftware.GetComponent<SystemMap>();
		clk = SysSoftware.GetComponent<Clock>();
		defalt = SysSoftware.GetComponent<Defalt>();
		sc = SysSoftware.GetComponent<SoundControl>();
		appmenu = SysSoftware.GetComponent<AppMenu>();
		appman = SysSoftware.GetComponent<AppMan>();

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		UpdateUI();

		X = Screen.height+AppMenuBgPos.height-233;
		Speed = 3;
		Cooldown = 0.001f;

		windowID = appmenu.windowID;
	}

	void UpdateUI()
	{
		AppMenuBgPos = new Rect(1,X,210,300);

		DesktopEnvElement.width = 4000;
		DesktopEnvElement.height = 1000;
		DesktopEnvElement.y = native_height = Screen.height - 64 * Customize.cust.UIScale;
		AppMenuBgPos.y = DesktopEnvElement.y - 270;

		Group.width = Group.height = 35 * Customize.cust.UIScale;
		AppMenuBgPos.x = 0;
		AppMenuBgPos.width = 160 * Customize.cust.UIScale;

		AppMenuSelectArea = new Rect(0,0,AppMenuBgPos.width + 50,AppMenuBgPos.height + 0);

		Scale = Customize.cust.UIScale;

		AppMenuButtons = new Rect(5 * Scale,scrollsize * 20 * Scale,100*Scale,20*Scale);

		Notepad = new Rect(5* Scale, 5 * Scale, 100* Scale, 20* Scale);
		Console = new Rect(5* Scale, 35 * Scale, 100* Scale, 20* Scale);
		SysInfo = new Rect(5* Scale, 65 * Scale, 100* Scale, 20* Scale);
		Map = new Rect(5* Scale, 95 * Scale,100* Scale, 20* Scale);

		SystemButtonsY = Group.y + 276;

		LogoutButton = new Rect(188 * Scale, SystemButtonsY - -0,20 * Scale,20 * Scale);
		SettingsButton = new Rect (188 * Scale, SystemButtonsY - 24, 20 * Scale, 20 * Scale);

		SearchButton = new Rect(85 * Scale,SystemButtonsY,20 * Scale,20 * Scale);
		SearchBar = new Rect(2 * Scale,SystemButtonsY,79 * Scale,20 * Scale);

		ShowAllButton = new Rect(2 * Scale,SystemButtonsY - 24,104 * Scale,20 * Scale);

		QuickList = new Rect (3 * Scale, 3 * Scale, 205 * Scale, 231);

		SearchList = new Rect (3,3,205,231);

		ButtonHeight = Notepad.height;

		updateUI = false;
	}

	void UpdateAppMenuPos()
	{
		switch (AppMenuState) 
		{
		case 1:
			if (X > Screen.height-AppMenuBgPos.height-33)
			{
				if (Timer >= 0) 
				{
					Timer -= Time.deltaTime;
				}
				UpdateUI();
				if (Timer <= 0) 
				{
					X-=Speed;
					Timer = Cooldown;
				}
			}
			if (X >= Screen.height+AppMenuBgPos.height-33)
			{
				AppMenuState = 0;
			}
			break;
		case 2:
			if (X < Screen.height+AppMenuBgPos.height-233) 
			{
				if (Timer >= 0) 
				{
					Timer -= Time.deltaTime;
				}
				UpdateUI();
				if (Timer <= 0) 
				{
					X+=Speed;
					Timer = Cooldown;
				}
			}
			if (X >= Screen.height+AppMenuBgPos.height-233) 
			{
				Close();
			}
			break;
		}
	}

	void UpdateProgramList()
	{
		ListOfPrograms.Clear();
		ListOfProgramTargets.Clear();

		for (int b = 0; b < GameControl.control.ProgramFiles.Count; b++) 
		{
			if (GameControl.control.ProgramFiles[b].Extension == ProgramSystem.FileExtension.Exe) 
			{
				ListOfPrograms.Add(GameControl.control.ProgramFiles[b].Name);
				ListOfProgramTargets.Add(GameControl.control.ProgramFiles[b].Target);
			}
		}
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
			if (!ListOfSites[SearchCount].ToLower().Contains (Inputted.ToLower())) 
			{
				ListOfSites.RemoveAt (SearchCount);
				ListOfTargets.RemoveAt (SearchCount);
			}
		}
	}

	void PlayClickSound()
	{
		sc.SoundSelect = 3;
		sc.PlaySound();
	}

	void OnGUI()
	{
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if(show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			AppMenuBgPos = GUI.Window(windowID,AppMenuBgPos,ShowAppMenu,"");
			if (AppMenuState == 0)
			{
				AppMenuState = 1;
			}
		}

		if (AppMenuState > 0)
		{
			UpdateAppMenuPos();
		}
	}

	void Close()
	{
		AppMenuState = 0;
		show = false;
		appmenu.show = false;
		SearchSites = "";
		Inputted = "";
		this.enabled = false;
	}

	void ActivateSearch()
	{
		if(SearchSites != "")
		{
			UpdateSiteList();
			SearchDone = false;
			Inputted = SearchSites;
			UpdateSearchUI = true;
		}
	}

	void ShowAppMenu(int WindowID)
	{
		if (show == true) 
		{
			AppMenuBgPos = new Rect(1,X,210,300);

			if(!AppMenuSelectArea.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0))
			{
				AppMenuState = 2;
			}
			if(!AppMenuSelectArea.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(1))
			{
				AppMenuState = 2;
			}
			if(!AppMenuSelectArea.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(2))
			{
				AppMenuState = 2;
			}

			GUI.backgroundColor = Color.white;

			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");

			if(GUI.Button(new Rect(SettingsButton),SettingsIcon,GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [DesktopStyle]))
			{
				PlayClickSound();
                appman.ProgramName = "System Panel";
                appman.SelectedApp = "System Panel";
                AppMenuState = 2;
			}

			if(GUI.Button(new Rect(LogoutButton),LogoutIcon,GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [DesktopStyle]))
			{
				PlayClickSound();
                appman.ProgramName = "Shutdown";
                appman.SelectedApp = "Shutdown";
                AppMenuState = 2;
			}

			if(GUI.Button(new Rect(SearchButton), SearchIcon,GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [DesktopStyle]))
			{
				ActivateSearch();
			}

			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) 
			{
				ActivateSearch();
			}

			SearchSites = GUI.TextField(new Rect(SearchBar),SearchSites);

			//Quick Launch
			if (SearchSites == "")
			{
				if (!ShowAllApps) 
				{
					scrollpos = GUI.BeginScrollView(new Rect(QuickList), scrollpos, new Rect(0, 0, 0,scrollsize * 21));
					//for (scrollsize = 0; scrollsize < GameControl.control.QuickProgramList.Count; scrollsize++)
					//{
					//	if(GUI.Button(new Rect(0 * Scale,scrollsize * 21,189 * Scale,ButtonHeight),GameControl.control.QuickProgramList[scrollsize].Name))
					//	{
					//		PlayClickSound();
     //                       appman.ProgramName = GameControl.control.QuickProgramList[scrollsize].Name;
     //                       appman.SelectedApp = GameControl.control.QuickProgramList[scrollsize].Target;
     //                   }
					//}
					GUI.EndScrollView();
					//End of Quick Launch

					if (GUI.Button (new Rect (ShowAllButton), "Show All"))
					{
						PlayClickSound();
						UpdateProgramList();
						ShowAllApps = true;
					}
				}
                else
				{
					if (GUI.Button (new Rect (ShowAllButton), "< Back"))
					{
						PlayClickSound();
						ShowAllApps = false;
					}

					scrollpos = GUI.BeginScrollView(new Rect(QuickList), scrollpos, new Rect(0, 0, 0,scrollsize * 21));
					for (scrollsize = 0; scrollsize < ListOfPrograms.Count; scrollsize++)
					{
						if(GUI.Button(new Rect(0 * Scale,scrollsize * 21,189 * Scale,20),ListOfPrograms[scrollsize]))
						{
							PlayClickSound();
                            appman.ProgramName = ListOfPrograms[scrollsize];
                            appman.SelectedApp = ListOfProgramTargets[scrollsize];
                        }
						//						if(GUI.Button(new Rect(0 * Scale,scrollsize * 21,120 * Scale,20),GameControl.control.ProgramFiles[scrollsize].Name))
						//						{
						//							PlayClickSound();
						//							if (GameControl.control.ProgramFiles [scrollsize].Type == ProgramSystem.ProgramType.Fdl) 
						//							{
						//								com.ComAddress = GameControl.control.ProgramFiles[scrollsize].Target;
						//							}
						//							if (GameControl.control.ProgramFiles [scrollsize].Type == ProgramSystem.ProgramType.Dir) 
						//							{
						//								com.ComAddress = GameControl.control.ProgramFiles[scrollsize].Name;
						//							}
						//							if (GameControl.control.ProgramFiles [scrollsize].Type == ProgramSystem.ProgramType.Exe) 
						//							{
						//								appman.SelectedApp = GameControl.control.ProgramFiles[scrollsize].Target;
						//							}
						//						}
					}
					GUI.EndScrollView();
				}
			} 
			else 
			{
				if(SearchSites == "")
				{
					UpdateSearchUI = false;
				}

				if(UpdateSearchUI == true)
				{
                    if (Inputted != "")
                    {
                        for (scrollsize = 0; scrollsize < ListOfSites.Count; scrollsize++)
                        {
                            SearchCheck();
                        }
                    }
                    scrollpos = GUI.BeginScrollView(new Rect(SearchList), scrollpos, new Rect(0, 0, 0, scrollsize * ButtonHeight));
					for (scrollsize = 0; scrollsize < ListOfSites.Count; scrollsize++)
					{
						if(Inputted != "" && ListOfSites.Count >= 1)
						{
							if(GUI.Button(new Rect(0 * Scale,scrollsize * ButtonHeight,100 * Scale,ButtonHeight - 1),ListOfSites[scrollsize]))
							{
                                appman.ProgramName = ListOfPrograms[scrollsize];
                                appman.SelectedApp = ListOfProgramTargets[scrollsize];
                            }
						}
					}
					GUI.EndScrollView();
				}
			}
		}
	}
}
