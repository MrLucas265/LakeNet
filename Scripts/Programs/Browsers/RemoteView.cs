using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteView : MonoBehaviour
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
	private WebSec ws;
	private SystemMap sm;
	private InternetBrowser ib;
	//private CabbageCorp cc;

	private Rect CloseButton;
	private Rect MiniButton;
	private Rect DefaltSetting;
	private Rect DefaltBoxSetting;
	private Rect URLLocation;
	private Rect URLSearchLocation;

	public string SiteName;

	public string Inputted;

	public bool minimize;

	public bool Focused;

	public bool Connected;

	public string TypedIP;

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
		//SetPos();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
	}

	void SetPos()
	{
		//DefaltSetting = new Rect (0,2,500,300);
	}

	void WebSearch()
	{
		ws = AppSoftware.GetComponent<WebSec>();
		sm = AppSoftware.GetComponent<SystemMap>();

		//cc = Database.GetComponent<CabbageCorp>();

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
		if (Input.GetMouseButtonDown(0))
		{
			def.SelectedWindowID = this.windowID;
		}

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [0])) 
			{
				this.enabled = false;
				this.show = false;
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

		if (Connected == false)
		{
			windowRect.width = 300;
			windowRect.height = 100;
			CloseButton = new Rect (278,2,21,21);
			MiniButton = new Rect (256,2,21,21);
			DefaltBoxSetting = new Rect (2,2,253,21);
			URLLocation = new Rect (58,50,240,21);
			URLSearchLocation = new Rect (2,50,54,21);
			GUI.Box (new Rect (DefaltBoxSetting), "Remote Viewer");

			if (GUI.Button (new Rect (URLSearchLocation), "Connect", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
			{
				//ib.AddressBar = TypedIP;
				Inputted = TypedIP;
				//GameControl.control.Sites.Add (ib.Inputted);
			}

			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
			{
				//ib.AddressBar = TypedIP;
				Inputted = TypedIP;
				//GameControl.control.Sites.Add (ib.Inputted);
			}

			TypedIP = GUI.TextField (new Rect (URLLocation), TypedIP);
		} 
		else
		{
			windowRect.width = 500;
			windowRect.height = 300;
			CloseButton = new Rect (478,2,21,21);
			MiniButton = new Rect (456,2,21,21);
			DefaltBoxSetting = new Rect (2,2,380,21);
			GUI.Box(new Rect(DefaltBoxSetting),"Remote Viewer: " + SiteName);

			if (GUI.Button (new Rect (new Rect (382,2,74,21)), "Disconnect", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
			{
				Connected = false;
				Inputted = "";
				SiteName = "";
				ws.UpdateSecCheck = false;
//				trace.stopping = true;
//				ib.showAddressBar = true;
//				PasswordSetup();
//				sm.Disconnect();
//				trace.UpdateTimer = false;
//				logged = false;
//				UsrName = "";
//				password = "";
//				sm.BounceIPs.Remove(sm.CabbageIP);
//				sm.BouncedConnections.Remove(sm.CabbagePos);
//				ib.AddressBar = "";
//				ib.connected = false;

			}
		}

		DesktopViewInfo();
	}

	void DesktopViewInfo()
	{
		switch(Inputted)
		{
		case "1":
			SiteName = "856.837.679.437";
			Connected = true;
			ws.UpdateSecCheck = true;
			//cc.RenderSite ();
			//ib.SiteAdminPass = cc.SiteAdminPass;
			sm.Connect ();
			break;
		}
	}

}
