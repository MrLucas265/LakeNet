using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucOSAppMenu : MonoBehaviour
{
	private GameObject HackingSoftware;
	private GameObject AppSoftware;
	private GameObject SysSoftware;
	private GameObject Computer;

	private Computer com;
	private InternetBrowser ib;
	private Progtive prog;
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
	private Rect windowRect = new Rect(100, 100, 200, 200);
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
	private Rect ShowPinnedButton;
	private Rect SpeakerButton;

	private Rect SearchButton;
	private Rect SearchBar;

	private Rect QuickList;

	public float SystemButtonsY;

	private Rect AppMenuSelectArea;
	private Rect AppMenuBgPos = new Rect(5,184,235,300);

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
	public Texture2D[] SpeakerIcon;
	public bool ShowShutdown;

	void Start () 
	{
		SysSoftware = GameObject.Find("System");
		AppSoftware = GameObject.Find("Applications");
		HackingSoftware = GameObject.Find("Hacking");

		com = SysSoftware.GetComponent<Computer>();
		ib = AppSoftware.GetComponent<InternetBrowser>();
		note = AppSoftware.GetComponent<Notepad>();
		prog = HackingSoftware.GetComponent<Progtive>();
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

		windowID = appmenu.windowID;
	}

	void UpdateUI()
	{
		DesktopEnvElement.width = 4000;
		DesktopEnvElement.height = 1000;
		DesktopEnvElement.y = native_height = Screen.height - 64 * Customize.cust.UIScale;
		AppMenuBgPos.y = DesktopEnvElement.y - 256;

		Group.width = Group.height = 35 * Customize.cust.UIScale;
		AppMenuBgPos.x = 0;
		AppMenuBgPos.width = 200 * Customize.cust.UIScale;

		AppMenuSelectArea = new Rect(0,0,AppMenuBgPos.width,AppMenuBgPos.height + 50);

		Scale = Customize.cust.UIScale;

		Notepad = new Rect(5* Scale, 5 * Scale, 100* Scale, 20* Scale);
		Console = new Rect(5* Scale, 35 * Scale, 100* Scale, 20* Scale);
		SysInfo = new Rect(5* Scale, 65 * Scale, 100* Scale, 20* Scale);
		Map = new Rect(5* Scale, 95 * Scale,100* Scale, 20* Scale);

		SystemButtonsY = Group.y + 265;

		SpeakerButton = new Rect(110 * Scale, SystemButtonsY - 24, 20 * Scale, 20 * Scale);

		LogoutButton = new Rect(5 * Scale, SystemButtonsY - -0,20 * Scale,20 * Scale);
		SettingsButton = new Rect (5 * Scale, SystemButtonsY - 24, 20 * Scale, 20 * Scale);

		//SearchButton = new Rect(5 * Scale,SystemButtonsY,20 * Scale,20 * Scale);
		SearchBar = new Rect(5 * Scale,SystemButtonsY,75 * Scale,20 * Scale);

		ShowAllButton = new Rect(5 * Scale,SystemButtonsY - 72,20 * Scale,20 * Scale);


		ShowPinnedButton = new Rect(5 * Scale, SystemButtonsY - 96, 20 * Scale, 20 * Scale);

		GatewayButton = new Rect(5 * Scale, SystemButtonsY - 48, 20 * Scale, 20 * Scale);

		QuickList = new Rect (40 * Scale, 3 * Scale, 150 * Scale, AppMenuBgPos.height-15);

		SearchList = new Rect (5,5,175,250);

		ButtonHeight = Notepad.height;

		updateUI = false;
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
		GUI.skin = com.Skin[GameControl.control.GUIID];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			AppMenuBgPos = GUI.Window(windowID,AppMenuBgPos,ShowAppMenu,""); 
		}
	}

	void Close()
	{
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

	void SpeakerUI()
	{
		if (Customize.cust.Volume <= 0)
		{
			if (GUI.Button(new Rect(SpeakerButton), SpeakerIcon[0], com.Skin[GameControl.control.GUIID].customStyles[DesktopStyle]))
			{
				PlayClickSound();
				appman.SelectedApp = "Volume Controller";
				Close();
			}
		}
		else if (Customize.cust.Volume > 0 && Customize.cust.Volume < 0.32f)
		{
			if (GUI.Button(new Rect(SpeakerButton), SpeakerIcon[1], com.Skin[GameControl.control.GUIID].customStyles[DesktopStyle]))
			{
				PlayClickSound();
				appman.SelectedApp = "Volume Controller";
				Close();
			}
		}
		else if (Customize.cust.Volume < 0.64f)
		{
			if (GUI.Button(new Rect(SpeakerButton), SpeakerIcon[2], com.Skin[GameControl.control.GUIID].customStyles[DesktopStyle]))
			{
				PlayClickSound();
				appman.SelectedApp = "Volume Controller";
				Close();
			}
		}
		else if (Customize.cust.Volume > 0.64f)
		{
			if (GUI.Button(new Rect(SpeakerButton), SpeakerIcon[3], com.Skin[GameControl.control.GUIID].customStyles[DesktopStyle]))
			{
				PlayClickSound();
				appman.SelectedApp = "Volume Controller";
				Close();
			}
		}
	}

	void ShowAppMenu(int WindowID)
	{
		if (show == true) 
		{
			if(!AppMenuSelectArea.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0))
			{
				Close();
			}
			if(!AppMenuSelectArea.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(1))
			{
				Close();
			}
			if(!AppMenuSelectArea.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(2))
			{
				Close();
			}

			GUI.backgroundColor = Color.white;

			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];

			GUI.contentColor = com.colors[Customize.cust.FontColorInt];

			//SpeakerUI();

			if (GUI.Button(new Rect(SettingsButton),SettingsIcon,com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
			{
				PlayClickSound();
                appman.ProgramName = "System Panel";
                appman.SelectedApp = "System Panel";
                Close();
			}

			if(GUI.Button(new Rect(LogoutButton),LogoutIcon,com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
			{
				PlayClickSound();
                appman.ProgramName = "Shutdown";
                appman.SelectedApp = "Shutdown";
                Close();
			}

			if (GUI.Button(new Rect(GatewayButton), "G", com.Skin[GameControl.control.GUIID].customStyles[DesktopStyle]))
			{
				PlayClickSound();
				appman.ProgramName = "Computer";
				appman.SelectedApp = "Computer";
				Close();
			}

			if (GUI.Button(new Rect(ShowAllButton), "A",com.Skin[GameControl.control.GUIID].customStyles[DesktopStyle]))
			{
				PlayClickSound();
				UpdateProgramList();
				ShowAllApps = true;
			}

			if (GUI.Button(new Rect(ShowPinnedButton), "P", com.Skin[GameControl.control.GUIID].customStyles[DesktopStyle]))
			{
				PlayClickSound();
				ShowAllApps = false;
			}

			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return) 
			{
				ActivateSearch();
			}

			//SearchSites = GUI.TextField(new Rect(SearchBar),SearchSites);

			//Quick Launch
			if (SearchSites == "")
			{
				if (ShowAllApps == false) 
				{
					scrollpos = GUI.BeginScrollView(new Rect(QuickList), scrollpos, new Rect(0, 0, 0,scrollsize * 21));
					for (scrollsize = 0; scrollsize < GameControl.control.QuickProgramList.Count; scrollsize++)
					{
						if(GUI.Button(new Rect(0 * Scale,scrollsize * 21,130 * Scale,ButtonHeight),GameControl.control.QuickProgramList[scrollsize].Name))
						{
							PlayClickSound();
                            appman.ProgramName = GameControl.control.QuickProgramList[scrollsize].Name;
                            appman.SelectedApp = GameControl.control.QuickProgramList[scrollsize].Target;
                        }
					}
					GUI.EndScrollView();
					//End of Quick Launch
				}

				if (ShowAllApps == true) 
				{
					scrollpos = GUI.BeginScrollView(new Rect(QuickList), scrollpos, new Rect(0, 0, 0,scrollsize * 21));
					for (scrollsize = 0; scrollsize < ListOfPrograms.Count; scrollsize++)
					{
						if(GUI.Button(new Rect(0 * Scale,scrollsize * 21,130 * Scale,20),ListOfPrograms[scrollsize]))
						{
							PlayClickSound();
                            appman.ProgramName = ListOfPrograms[scrollsize];
                            appman.SelectedApp = ListOfProgramTargets[scrollsize];
                        }
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
                                appman.ProgramName = ListOfSites[scrollsize];
                                appman.SelectedApp = ListOfTargets[scrollsize];
                            }
						}
					}
					GUI.EndScrollView();
				}
			}
		}
	}
}
