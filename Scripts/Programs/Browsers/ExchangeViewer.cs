using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeViewer : MonoBehaviour
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
	private ShareTrades st;
	private WebSec ws;
	private SystemMap sm;
	private GStocks gstocks;
	private StockExchangeBrowser seb;
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

	public Rect TabMenuRect;

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

		if (seb.AddressBar == "") 
		{
			seb.AddressBar = "www.stockexchange.com";
			seb.Inputted = "www.stockexchange.com";
		}
	}

	void PosCheck()
	{
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		if (Customize.cust.windowx[windowID] == 0) 
		{
			if (Customize.cust.windowy[windowID] == 0) 
			{
				Customize.cust.windowx [windowID] = Screen.width / 2;
				Customize.cust.windowy [windowID] = Screen.height / 2;
			}
		}
	}


	void SetPos()
	{
		CloseButton = new Rect (477,2,21,21);
		MiniButton = new Rect (455,2,21,21);
		DefaltSetting = new Rect (2,2,500,300);
		windowRect.width = DefaltSetting.width;
		windowRect.height = DefaltSetting.height;
	}

	void WebSearch()
	{
		ws = AppSoftware.GetComponent<WebSec>();
		sm = AppSoftware.GetComponent<SystemMap>();
		st = Database.GetComponent<ShareTrades>();
		gstocks = Database.GetComponent<GStocks>();
		//cc = Database.GetComponent<CabbageCorp>();

		dr = Minigames.GetComponent<DragRacer>();

		seb = AppSoftware.GetComponent<StockExchangeBrowser>();
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

	void Close()
	{
		appman.SelectedApp = "Exchange Viewer";
		seb.TempHistory.RemoveRange(0, seb.TempHistory.Count);
		seb.AddressBar = "www.stockexchange.com";
		seb.Inputted = "www.stockexchange.com";
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

		if (seb.MainPage == false)
		{
			DefaltBoxSetting = new Rect (2 + 24, 2, MiniButton.x - 24 - 3, 21);
			if (GUI.Button (new Rect (2, 2, DefaltBoxSetting.x - 3, 21), "<")) 
			{
				seb.Inputted = "www.stockexchange.com";
			}
		} 
		else 
		{
			DefaltBoxSetting = new Rect (2,2,MiniButton.x-3,21);
		}

		GUI.DragWindow (new Rect (DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting),"Exchange Viewer");

		seb.WebSiteInfo();

		if (seb.connected == false)
		{
			GUI.TextArea(new Rect(10, 50, 300, 200), "" + seb.ErrorCode + "\n" + "\n" + seb.ErrorDesc + "\n" + "\n" + seb.ErrorSoloution);
		}
	}

}
