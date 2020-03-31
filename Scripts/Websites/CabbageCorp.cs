//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class CabbageCorp : MonoBehaviour 
//{
//	public int StartCount;
//	public List<string> EmailSubject = new List<string>();
//	public List<string> NoteTitle = new List<string>();

//	public List<string> Logs = new List<string>();

//	const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//	const string AccNo = "1234567890";

//	public bool logged;
//	public bool showMenu;

//	public int Select;

//	public string UsrName;
//	public string password;
//	public string SiteAdminPass;

//	private GameObject Computer;
//	private GameObject Prompts;
//	private GameObject Applications;
//	private GameObject Hacking;
//	private GameObject System;

//	private InternetBrowser ib;
//	private Computer com;
//	private ErrorProm ep;
//	private Tracer trace;
//	private SystemMap sm;
//	private TextReader tr;
//	private Progtive prog;
//	private Defalt def;
//	private WebSec ws;
//	private PasswordList pl;
//	private CLICommandsV2 clic;

//	public Vector2 scrollpos = Vector2.zero;
//	public int scrollsize;

//	public List<Color> Colors = new List<Color>();
//	public Color32 rgb1 = new Color32(0,0,0,0);
//	public Color32 buttonColor = new Color32(0,0,0,0);
//	public Color32 fontColor = new Color32(0,0,0,0);
//	public Color32 ClosefontColor = new Color32(0,0,0,0);
//	public Color32 ClosebuttonColor = new Color32(0,0,0,0);
//	public Color32 WindowColor = new Color32(0,0,0,0);
//	public Color32 TitlebarColor = new Color32(0,0,0,0);

//	//DesktopEnvStuff
//	public Texture2D AppMenuIcon;
//	public Texture2D ExplorerIcon;
//	public Texture2D UserIcon;

//	private Rect CloseButton;
//	private Rect MiniButton;

//	public Rect AppMenuPos;
//	public Rect AppMenuButtonPos;
//	public Rect ExplorerPos;
//	public Rect ExplorerTitlePos;

//	public bool showExplorer;
//	public bool MinimizeExplorer;
//	public bool showAppMenu;

//	void Start()
//	{
//		Computer = GameObject.Find("Computer");
//		Prompts = GameObject.Find("Prompts");
//		Applications = GameObject.Find("Applications");
//		Hacking = GameObject.Find("Hacking");
//		System = GameObject.Find("System");
//		StartCount = 100;
//		fileGenPrivate ();
//		Documents();
//		WebSearch();
//		LoadPresetColors();

//		ExplorerPos = new Rect(100,100,200,150);
//		AppMenuPos = new Rect(1,135,125,125);
//		AppMenuButtonPos = new Rect(1, 264, 35, 35);
//	}

//	void LoadPresetColors()
//	{
//		//rgb1.r = 255;
//		//rgb1.g = 255;
//		//rgb1.b = 255;
//		//rgb1.a = 255;

//		buttonColor.r = 0;
//		buttonColor.g = 255;
//		buttonColor.b = 0;
//		buttonColor.a = 255;

//		fontColor.r = 0;
//		fontColor.g = 255;
//		fontColor.b = 0;
//		fontColor.a = 255;

//		ClosebuttonColor.r = 255;
//		ClosebuttonColor.g = 0;
//		ClosebuttonColor.b = 0;
//		ClosebuttonColor.a = 255;

//		ClosefontColor.r = 255;
//		ClosefontColor.g = 255;
//		ClosefontColor.b = 255;
//		ClosefontColor.a = 255;

//		WindowColor.r = 0;
//		WindowColor.g = 255;
//		WindowColor.b = 0;
//		WindowColor.a = 255;

//		TitlebarColor.r = 0;
//		TitlebarColor.g = 150;
//		TitlebarColor.b = 0;
//		TitlebarColor.a = 255;
//	}


//	void WebSearch()
//	{
//		// APPLICATIONS
//		sm = Applications.GetComponent<SystemMap>();
//		tr = Applications.GetComponent<TextReader>();
//		ib = Applications.GetComponent<InternetBrowser>();
//		ws = Applications.GetComponent<WebSec>();
//		// PROMPTS
//		ep = Prompts.GetComponent<ErrorProm>();
//		// HACKING
//		trace = Hacking.GetComponent<Tracer>();
//		prog = Hacking.GetComponent<Progtive>();
//		// SYSTEM
//		com = System.GetComponent<Computer>();
//		def = System.GetComponent<Defalt>();
//		pl = System.GetComponent<PasswordList>();
//		clic = System.GetComponent<CLICommandsV2>();
//	}

//	public string GetRandomString(int min, int max)
//	{
//		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
//		string retMe = "";
//		for(int i=0; i<charAmount; i++)
//		{
//			retMe += glyphs[Random.Range(0, glyphs.Length)];
//		}
//		return retMe;
//	}

//	public string GetRandomNumber(int min, int max)
//	{
//		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
//		string retMe = "";
//		for(int i=0; i<charAmount; i++)
//		{
//			retMe += AccNo[Random.Range(0, AccNo.Length)];
//		}
//		return retMe;
//	}

//	void PasswordSetup()
//	{
//		//SiteAdminPass = GetRandomString (8, 8);
//	}

//	void Update()
//	{
//		if (SiteAdminPass == "") 
//		{
//			PasswordSetup();
//		}

//		if (GameControl.control.CabbageFileName.Count < StartCount || GameControl.control.CabbageFileSize.Count < StartCount) 
//		{
//			fileGenPrivate ();
//		}
//	}

//	public void fileGenPrivate()
//	{
//		for(int i=0; i<1; i++)
//		{
//			GameControl.control.CabbageFileName.Add(GetRandomString(4,4));
//			GameControl.control.CabbageFileName.Sort();
//			GameControl.control.CabbageFileSize.Add (Random.Range (1, 50));
//		}
//	}

//	public void Documents()
//	{
//		EmailSubject.Add("Secuirty Loophole");
//		EmailSubject.Add("Test 1");
//		EmailSubject.Add("Test 2");
//		NoteTitle.Add("Important Note");
//	}


//	void AppMenu()
//	{
//		GUI.backgroundColor = WindowColor;
//		GUI.contentColor = WindowColor;
//		GUI.Box(new Rect(AppMenuPos),"");

//		GUI.backgroundColor = TitlebarColor;
//		GUI.contentColor = ClosefontColor;

//		GUI.Button(new Rect(AppMenuPos.x,AppMenuPos.y,AppMenuPos.width,24),UserIcon);
//		GUI.Label(new Rect(AppMenuPos.x + 26,AppMenuPos.y,AppMenuPos.width,24),"" + ib.Username);

//		if (!AppMenuPos.Contains (Event.current.mousePosition))
//		{
//			if (!AppMenuButtonPos.Contains (Event.current.mousePosition))
//			{
//				if (Input.GetMouseButtonDown (0))
//				{
//					showAppMenu = false;
//				}
//			}
//		}
//	}

//	void Explorer()
//	{
//		ExplorerTitlePos = new Rect (ExplorerPos.x, ExplorerPos.y, ExplorerPos.width - 43, 21);
//		CloseButton = new Rect (ExplorerPos.x + 200 - 22,ExplorerPos.y,21,21);
//		MiniButton = new Rect (ExplorerPos.x + 200 - 43,ExplorerPos.y,21,21);

//		GUI.Box(new Rect(ExplorerPos),"");
//		GUI.backgroundColor = TitlebarColor;
//		GUI.contentColor = fontColor;
//		GUI.Box(new Rect(ExplorerTitlePos),"Explorer");

//		if (!CloseButton.Contains (Event.current.mousePosition)) 
//		{
//			GUI.backgroundColor = TitlebarColor;
//			GUI.contentColor = fontColor;
//			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
//			{
//				showExplorer = false;
//			}
//		}
//		else 
//		{
//			GUI.backgroundColor = ClosebuttonColor;
//			GUI.contentColor = ClosefontColor;
//			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [2])) 
//			{
//				showExplorer = false;
//			}
//		}

//		if (ExplorerTitlePos.Contains (Event.current.mousePosition)) 
//		{
//			if (Input.GetMouseButton (0))
//			{
//				ExplorerPos.x = Event.current.mousePosition.x-75;
//				ExplorerPos.y = Event.current.mousePosition.y-10;
//			}
//		} 

//		if (MiniButton.Contains (Event.current.mousePosition)) 
//		{
//			GUI.backgroundColor = TitlebarColor;
//			GUI.contentColor = fontColor;
//			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
//			{
//				MinimizeExplorer = !MinimizeExplorer;
//			}
//		} 
//		else
//		{
//			GUI.backgroundColor = TitlebarColor;
//			GUI.contentColor = fontColor;
//			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
//			{
//				MinimizeExplorer = !MinimizeExplorer;
//			}
//		}

//		if (MinimizeExplorer == true) 
//		{
//			ExplorerPos.height = 21;
//		} 
//		else
//		{
//			ExplorerPos.height = 150;
//		}
//	}

//	void DesktopEnv()
//	{
//		if (GUI.Button (new Rect (AppMenuButtonPos), AppMenuIcon)) 
//		{
//			showAppMenu = !showAppMenu;
//		}

//		if (GUI.Button (new Rect (45, 264, 35, 35), ExplorerIcon)) 
//		{
//			showExplorer = !showExplorer;
//		}
//	}

//	public void RenderSite()
//	{
//		GUI.backgroundColor = buttonColor;
//		GUI.contentColor = fontColor;

//		DesktopEnv();
//		if (showExplorer == true)
//		{
//			Explorer();
//		}
//		if (showAppMenu == true)
//		{
//			AppMenu();
//		}
//	}
//}