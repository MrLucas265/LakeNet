using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetViewer : MonoBehaviour
{
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;
	public bool show;

	private GameObject SysSoftware;
	private GameObject AppSoftware;

	private Computer com;
	private Defalt def;

	private GameObject Database;
	private GameObject Minigames;
	private Component script;

	public bool Search;

	// collection of scripts
	private AppMan appman;
	private Becas becas;
	private Test RevaTest;
	private Test1 test1;
	private Ping ping;
	private Unicom uc;
	private JailDew jd;
	private LECBank LEC;
	private Reva reva;
	private MiniGameWeb mgw;
	private ShareTrades st;
	private TUG tug;
	private WebSec ws;
	private SystemMap sm;
	private ServerHost sh;
	private GStocks gstocks;
	private HardwareSite hs;
	private InternetBrowser ib;
	//private CabbageCorp cc;

	private DragRacer dr;

	private Rect AddbookmarkButton;
	private Rect ExtraButton;
	private Rect CloseButton;
	private Rect MiniButton;
	private Rect DefaltSetting;
	private Rect DefaltBoxSetting;
	private Rect URLLocation;
	private Rect URLSearchLocation;
	private Rect ForwardButtonLocation;
	private Rect BackButtonLocation;
	private Rect MenuBarBoxLocation;

	public Texture2D SearchIcon;
	public Texture2D ForwardIcon;
	public Texture2D BackIcon;
	public int SelectedPage;

	public string SiteName;

	public bool minimize;

	public bool Focused;

	public int Page;
	public Rect TabMenuRect;

	public Vector2 FavsScrollpos = Vector2.zero;
	public int FavsScrollsize;

	public Vector2 HistScrollpos = Vector2.zero;
	public int HistScrollsize;

	public Texture2D AddBookmarkIcon;
	public Texture2D RemoveBookmarkIcon;

	// Use this for initialization
	void Start() 
	{
		SysSoftware = GameObject.Find("System");
		AppSoftware = GameObject.Find("Applications");
		appman = SysSoftware.GetComponent<AppMan>();
		def = SysSoftware.GetComponent<Defalt>();
		com = SysSoftware.GetComponent<Computer>();
		Database = GameObject.Find("Database");
		Minigames = GameObject.Find("MiniGames");
		WebSearch();
		PosCheck();
		SetPos();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		if (ib.TempHistory.Count == 0) 
		{
			ib.Home();
		}

		if (ib.AddressBar == "")
		{
			ib.AddressBar = "www.ping.com";
		}
	}

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0) 
		{
			if (Customize.cust.windowy[windowID] == 0) 
			{
				Customize.cust.windowx [windowID] = Screen.width / 2;
				Customize.cust.windowy [windowID] = Screen.height / 2;
			}
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}


	void SetPos()
	{
		AddbookmarkButton = new Rect (411,2,21,21);
		ExtraButton = new Rect (433,2,21,21);
		CloseButton = new Rect (477,2,21,21);
		MiniButton = new Rect (455,2,21,21);
		DefaltSetting = new Rect (0,2,500,300);
		DefaltBoxSetting = new Rect (43,2,181,21);
		URLLocation = new Rect (169,2,241,21);
		URLSearchLocation = new Rect (147,2,21,21);
		ForwardButtonLocation  = new Rect (24,2,21,21);
		BackButtonLocation  = new Rect (2,2,21,21);
		MenuBarBoxLocation = new Rect (2,2,500,23);
	}

	void WebSearch()
	{
		ws = AppSoftware.GetComponent<WebSec>();
		sm = AppSoftware.GetComponent<SystemMap>();
		becas = Database.GetComponent<Becas>();
		RevaTest = Database.GetComponent<Test>();
		ping = Database.GetComponent<Ping>();
		uc = Database.GetComponent<Unicom>();
		jd = Database.GetComponent<JailDew>();
		LEC = Database.GetComponent<LECBank>();
		reva = Database.GetComponent<Reva>();
		st = Database.GetComponent<ShareTrades>();
		mgw = Minigames.GetComponent<MiniGameWeb>();
		tug = Database.GetComponent<TUG>();
		sh = Database.GetComponent<ServerHost>();
		test1 = Database.GetComponent<Test1>();
		gstocks = Database.GetComponent<GStocks>();
		hs = Database.GetComponent<HardwareSite>();
		//cc = Database.GetComponent<CabbageCorp>();

		dr = Minigames.GetComponent<DragRacer>();

		ib = AppSoftware.GetComponent<InternetBrowser>();
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


	void ExtraMenu()
	{
		if(GUI.Button(new Rect(501,1,100,20),"Bookmarks"))
		{
			Page = 2;
		}

		if(GUI.Button(new Rect(501,21,100,20),"History"))
		{
			Page = 3;
		}
	}

	void Close()
	{
		appman.SelectedApp = "Net Viewer";
		ib.TempHistory.RemoveRange(0, ib.TempHistory.Count);
		//ib.AddressBar = Customize.cust.WebBrowserHomepage;
		//ib.Inputted = Customize.cust.WebBrowserHomepage;
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		if(show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}


	}

	void DoMyWindow(int WindowID)
	{
		//GUI.Box (new Rect (MenuBarBoxLocation),"");

		if (Input.GetMouseButtonDown(0))
		{
			def.SelectedWindowID = this.windowID;
		}

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [0])) 
			{
				Close();
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

		GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

		GUI.DragWindow (new Rect (DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting),"Net Viewer");

		if (Page >= 1) 
		{
			windowRect.width = 650;
			ExtraMenu();
			//GUI.Box (new Rect (TabMenu), "");
		}
		else
		{
			windowRect.width = 500;
		}

		switch (Page)
		{
		case 2:
			BookmarkedMenu();
			break;
		case 3:
			HistoryMenu();
			break;
		case 4:
			HistoryMenu();
			break;
		}

		if (ib.showAddressBar == true) 
		{
			DefaltBoxSetting = new Rect (46,2,100,21);

			GUI.contentColor = Color.white;

			if (GUI.Button (new Rect (ForwardButtonLocation), ForwardIcon, GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
			{
				ib.Foward();
			}

			if (GUI.Button (new Rect (BackButtonLocation), BackIcon, GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2]))
			{
				ib.Back();
			}

			//if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return)
			//{
			//	ib.connected = false;
			//	ib.InitalTime = 1;
			//	ib.SiteConnection();
			//}

			if (GUI.Button(new Rect(URLSearchLocation), SearchIcon, GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[2]))
			{
				ib.SiteConnectingStuff();
				ib.SiteConnectingStuff();
				ib.ClearCurrentConnectionStuff();
			}

			if (ib.ErrorDesc == "")
			{
				ib.SiteConnectingStuff();
				ib.ClearCurrentConnectionStuff();
			}

			if (AddbookmarkButton.Contains (Event.current.mousePosition)) 
			{
				if (GUI.Button (new Rect (AddbookmarkButton), AddBookmarkIcon,GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
				{
					if (!GameControl.control.FavSites.Contains (ib.Inputted)) 
					{
						GameControl.control.FavSites.Add (ib.Inputted);
					}
				}
			} 
			else
			{
				GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
				GUI.contentColor = Color.white;
				if (GUI.Button (new Rect (AddbookmarkButton), AddBookmarkIcon,GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
				{
					if (!GameControl.control.FavSites.Contains (ib.Inputted)) 
					{
						GameControl.control.FavSites.Add (ib.Inputted);
					}
				}
			}

			if (ExtraButton.Contains (Event.current.mousePosition)) 
			{
				GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
				GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
				if (GUI.Button (new Rect (ExtraButton), "|||",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
				{
					//showTabMenu = !showTabMenu;
					if (Page == 0)
					{
						Page = 1;
					} 
					else 
					{
						Page = 0;
					}
				}
			} 
			else
			{
				GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
				GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
				if (GUI.Button (new Rect (ExtraButton), "|||",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
				{
					//showTabMenu = !showTabMenu;
					if (Page == 0)
					{
						Page = 1;
					} 
					else 
					{
						Page = 0;
					}
				}
			}

			//GUI.backgroundColor = Color.white;
			ib.AddressBar = GUI.TextField (new Rect (URLLocation), ib.AddressBar);
		} 
		else
		{
			DefaltBoxSetting = new Rect (2,2,453,21);
		}

        ib.WebSiteInfo();

        if (ib.connected == false)
        {
            GUI.TextArea(new Rect(10, 50, 300, 200), "" + ib.ErrorCode + "\n" + "\n" + ib.ErrorDesc + "\n" + "\n" + ib.ErrorSoloution);
        }
	}


	void BookmarkedMenu()
	{
		if (GameControl.control.FavSites.Count >= 1)
		{
			FavsScrollpos = GUI.BeginScrollView(new Rect(501, 45, 150, 200), FavsScrollpos, new Rect(0, 0, 0, FavsScrollsize * 20));
			for (FavsScrollsize = 0; FavsScrollsize < GameControl.control.FavSites.Count; FavsScrollsize++)
			{
				if(GUI.Button(new Rect(22,FavsScrollsize*22,140,21),GameControl.control.FavSites[FavsScrollsize]))
				{
					ib.Inputted = GameControl.control.FavSites [FavsScrollsize];
					ib.AddressBar = GameControl.control.FavSites [FavsScrollsize];
				}
				GUI.contentColor = Color.white;
				if(GUI.Button(new Rect(0,FavsScrollsize*22,21,21),RemoveBookmarkIcon))
				{
					GameControl.control.FavSites.RemoveAt(FavsScrollsize);
					break;
				}
			}
			GUI.EndScrollView();
		}
	}

	void HistoryMenu()
	{
		if (GameControl.control.Sites.Count >= 1)
		{
			HistScrollpos = GUI.BeginScrollView(new Rect(501, 45, 150, 200), HistScrollpos, new Rect(0, 0, 0, HistScrollsize * 20));
			for (HistScrollsize = 0; HistScrollsize < GameControl.control.Sites.Count; HistScrollsize++)
			{
				if(GUI.Button(new Rect(22,HistScrollsize*22,140,21),GameControl.control.Sites[HistScrollsize]))
				{
					ib.Inputted = GameControl.control.Sites [HistScrollsize];
					ib.AddressBar = GameControl.control.Sites [HistScrollsize];
				}
				GUI.contentColor = Color.white;
				if(GUI.Button(new Rect(0,HistScrollsize*22,21,21),RemoveBookmarkIcon))
				{
					GameControl.control.Sites.RemoveAt(HistScrollsize);
					break;
				}
			}
			GUI.EndScrollView();
		}
	}

}
