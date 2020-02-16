using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefox : MonoBehaviour
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
	private TestSite testsite;
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

		def = SysSoftware.GetComponent<Defalt>();
		com = SysSoftware.GetComponent<Computer>();
		Database = GameObject.Find("Database");
		Minigames = GameObject.Find("MiniGames");
		WebSearch();
		SetPos();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
	}

	void SetPos()
	{
		AddbookmarkButton = new Rect (456,25,21,21);
		ExtraButton = new Rect (478,25,21,21);
		CloseButton = new Rect (478,2,21,21);
		MiniButton = new Rect (456,2,21,21);
		DefaltSetting = new Rect (0,2,500,300);
		//DefaltBoxSetting = new Rect (47,2,475,21);
		URLLocation = new Rect (48,25,250,21);
		//URLSearchLocation = new Rect (231,2,21,21);
		ForwardButtonLocation  = new Rect (25,25,21,21);
		BackButtonLocation  = new Rect (2,25,21,21);
		MenuBarBoxLocation = new Rect (2,2,500,47);
	}

	void WebSearch()
	{
		ws = AppSoftware.GetComponent<WebSec>();
		sm = AppSoftware.GetComponent<SystemMap>();

		testsite = Database.GetComponent<TestSite>();
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

				if(GUI.Button(new Rect(0,HistScrollsize*22,21,21),RemoveBookmarkIcon))
				{
					GameControl.control.Sites.RemoveAt(HistScrollsize);
					break;
				}
			}
			GUI.EndScrollView();
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = com.Skin[GameControl.control.GUIID];

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}


	}

	void DoMyWindow(int WindowID)
	{
		GUI.Box (new Rect (MenuBarBoxLocation),"");

		if (Input.GetMouseButtonDown(0))
		{
			def.SelectedWindowID = this.windowID;
		}

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
			{
			}
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

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow (new Rect (DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting),"Firefox");

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
			DefaltBoxSetting = new Rect (2,2,454,21);

			if (GUI.Button (new Rect (ForwardButtonLocation), ForwardIcon, com.Skin [GameControl.control.GUIID].customStyles [2])) {
				if (SelectedPage >= GameControl.control.Sites.Count - 1)
				{
					SelectedPage = GameControl.control.Sites.Count - 1;
				} 
				else
				{
					SelectedPage++;
				}
				ib.Inputted = GameControl.control.Sites [SelectedPage];
				ib.AddressBar = GameControl.control.Sites [SelectedPage];
			}

			if (GUI.Button (new Rect (BackButtonLocation), BackIcon, com.Skin [GameControl.control.GUIID].customStyles [2])) {
				if (SelectedPage <= 0)
				{
					SelectedPage = 0;
				} 
				else 
				{
					SelectedPage--;
				}
				ib.Inputted = GameControl.control.Sites [SelectedPage];
				ib.AddressBar = GameControl.control.Sites [SelectedPage];
			}

			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return)
			{
				ib.Inputted = ib.AddressBar;
				GameControl.control.Sites.Add (ib.Inputted);
			}

			if (GUI.Button (new Rect (URLSearchLocation), SearchIcon, com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				ib.Inputted = ib.AddressBar;
				GameControl.control.Sites.Add (ib.Inputted);
			}

			if (AddbookmarkButton.Contains (Event.current.mousePosition)) 
			{
				if (GUI.Button (new Rect (AddbookmarkButton), AddBookmarkIcon,com.Skin [GameControl.control.GUIID].customStyles [2])) 
				{
					if (!GameControl.control.FavSites.Contains (ib.Inputted)) 
					{
						GameControl.control.FavSites.Add (ib.Inputted);
					}
				}
			} 
			else
			{
				GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
				GUI.contentColor = com.colors[Customize.cust.FontColorInt];
				if (GUI.Button (new Rect (AddbookmarkButton), AddBookmarkIcon,com.Skin [GameControl.control.GUIID].customStyles [2])) 
				{
					if (!GameControl.control.FavSites.Contains (ib.Inputted)) 
					{
						GameControl.control.FavSites.Add (ib.Inputted);
					}
				}
			}

			if (ExtraButton.Contains (Event.current.mousePosition)) 
			{
				if (GUI.Button (new Rect (ExtraButton), "|||",com.Skin [GameControl.control.GUIID].customStyles [2])) 
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
				GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
				GUI.contentColor = com.colors[Customize.cust.FontColorInt];
				if (GUI.Button (new Rect (ExtraButton), "|||",com.Skin [GameControl.control.GUIID].customStyles [2])) 
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

		switch(ib.Inputted)
		{
		case "test":
			testsite.RenderSite();
			sm.Connect();
			break;
		case "www.becassystems.com":
			SiteName = "Becas";
			ws.UpdateSecCheck = true;
			becas.RenderSite ();
			ib.SiteAdminPass = becas.SiteAdminPass;
			sm.Connect();
			break;
		case "www.revatest.com":
			SiteName = "Reva Test";
			ws.UpdateSecCheck = true;
			//RevaTest.RenderSite ();
			//ib.SiteAdminPass = RevaTest.SiteAdminPass;
			sm.Connect();
			break;
		case "www.ping.com":
			ping.RenderSite();
			sm.Connect();
			break;
		case "www.unicom.com":
			SiteName = "Unicom";
			ws.UpdateSecCheck = true;
			uc.RenderSite();
			ib.SiteAdminPass = uc.SiteAdminPass;
			sm.Connect();
			break;
		case "www.jaildew.com":
			SiteName = "Jaildew";
			ws.UpdateSecCheck = true;
			jd.RenderSite ();
			ib.SiteAdminPass = jd.SiteAdminPass;
			sm.Connect ();
			break;
		case "www.reva.com":
			SiteName = "Reva";
			ws.UpdateSecCheck = true;
			reva.RenderSite();
			sm.Connect();
			break;
		case "www.lecbank.com":
			SiteName = "LEC";
			ws.UpdateSecCheck = true;
			LEC.RenderSite();
			sm.Connect();
			break;
		case "game":
			mgw.RenderSite();
			mgw.showMenu = true;
			sm.Connect();
			break;
		case "shares":
			st.RenderSite();
			sm.Connect();
			break;
		case "servers":
			sh.RenderSite();
			sm.Connect();
			break;
		case "tugs":
			tug.RenderSite();
			sm.Connect();
			break;
		case "drag":
			dr.GameRender();
			sm.Connect();
			break;
		case "stock":
			st.RenderSite();
			sm.Connect();
			break;
		case "test1":
			test1.RenderSite();
			sm.Connect();
			break;
		case "test2":
			hs.RenderSite();
			sm.Connect();
			break;
		case "gstocks":
			gstocks.RenderSite();
			sm.Connect();
			break;
		}
	}

}
