using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeView : MonoBehaviour 
{
	public float native_width = 1920;
	public float native_height = 1080;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public int windowID;
	public bool Drag;
	public bool show;

	private Computer com;
	private Defalt def;

	private GameObject SysSoftware;

	public List<string> History = new List<string>();
	public string CurrentLocation;
	public string ItemText;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public List<string> InfoNames = new List<string>();
	public List<string> ProgramTarget = new List<string>();
	public List<string> ProgramNames = new List<string>();

	public List<string> CMDNames = new List<string>();
	public List<string> CMDFunc = new List<string>();

	public List<CHMSystem> CurrentPage = new List<CHMSystem>();

	private GameObject Prompts;
	private NotfiPrompt noti;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;
	public bool minimize;

	private AppMan appman;

	// Use this for initialization
	void Start ()
	{
		SysSoftware = GameObject.Find("System");
		Prompts = GameObject.Find("Prompts");
		com = SysSoftware.GetComponent<Computer>();
		def = SysSoftware.GetComponent<Defalt>();
		noti = Prompts.GetComponent<NotfiPrompt>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		appman = SysSoftware.GetComponent<AppMan>();

		CloseButton = new Rect(178,1,21,21);
		MiniButton = new Rect(157,1,21,21);

		DefaltSetting.width = 200;
		DefaltSetting.height = 200;

		PosCheck();

		InstalledProgramCheck();
		CommandCheck();
	}

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0) 
		{
			Customize.cust.windowx [windowID] = Screen.width / 2;
		}
		if (Customize.cust.windowy[windowID] == 0) 
		{
			Customize.cust.windowy [windowID] = Screen.height / 2;
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}

	// Update is called once per frame
	void Update ()
	{
		
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
		//set up scaling
		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;
		//  GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 1, 0)), new Vector3(FloatXSize, FloatYSize, 1))

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		//now create your GUI normally, as if you were in your native resolution
		//The GUI.matrix will scale everything automatically.
		//example
		if(show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}
	}

	void InstalledProgramCheck()
	{
		for (int i = 0; i < GameControl.control.ProgramInfo.Count; i++) 
		{
			InfoNames.Add (GameControl.control.ProgramInfo [i].Name);
		}
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
		{
			if (GameControl.control.ProgramFiles [i].Extension == ProgramSystem.FileExtension.Exe)
			{
				ProgramTarget.Add (GameControl.control.ProgramFiles [i].Target);
				ProgramNames.Add (GameControl.control.ProgramFiles [i].Name);
			}
		}

		AddMissingProgramData();
	}

	void CommandCheck()
	{
		for (int i = 0; i < GameControl.control.ProgramInfo.Count; i++) 
		{
			InfoNames.Add (GameControl.control.ProgramInfo [i].Name);
		}
		for (int i = 0; i < GameControl.control.Commands.Count; i++) 
		{
			CMDFunc.Add (GameControl.control.Commands [i].Func);
			CMDNames.Add (GameControl.control.Commands [i].Name);
		}

		AddMissingCommandsData();
	}

	void AddMissingProgramData()
	{
		if (!InfoNames.Contains("Programs")) 
		{
			GameControl.control.ProgramInfo.Add(new CHMSystem ("Programs", "","Main Menu","Programs",CHMSystem.FileType.Directory));
		}
		if (!InfoNames.Contains("Notepad"))
		{
			if (ProgramTarget.Contains("Notepad")) 
			{
				int ID = 0;
				ID = ProgramTarget.IndexOf ("Notepad");

				string Name = "";
				Name = ProgramNames[ID];

				string Desc = "";
				Desc = "Notepad is a simple minimalistic text editor and a basic text-editing program at that which enables computer users to create files and documents quick and easily.";

				string Save = "";
				Save = "To save a file in notepad you can click save as or if a file as already been saved and open you can click save";

				string Open = "";
				Open = "To open a file you can double click where the file is located in gateway or just click open file in notepad";

				GameControl.control.ProgramInfo.Add(new CHMSystem (Name, "","Programs",Name, CHMSystem.FileType.Folder));
				GameControl.control.ProgramInfo.Add(new CHMSystem ("Description", Desc,Name,"", CHMSystem.FileType.Folder));
				GameControl.control.ProgramInfo.Add(new CHMSystem ("How to save", Save,Name,"", CHMSystem.FileType.Folder));
				GameControl.control.ProgramInfo.Add(new CHMSystem ("How to open", Open,Name,"", CHMSystem.FileType.Folder));

				DisplayNotification();
			}
		}
	}

	void AddMissingCommandsData()
	{
		if (!InfoNames.Contains("Commands")) 
		{
			GameControl.control.ProgramInfo.Add(new CHMSystem ("Commands", "","Main Menu","Commands", CHMSystem.FileType.Folder));
		}
		if (!InfoNames.Contains("Network")) 
		{
			GameControl.control.ProgramInfo.Add(new CHMSystem ("Network", "","Commands","Network", CHMSystem.FileType.Folder));
		}

		if (!InfoNames.Contains("connect"))
		{
			if (CMDFunc.Contains("connect")) 
			{
				int ID = 0;
				ID = CMDFunc.IndexOf ("connect");

				string Name = "";
				Name = CMDNames[ID];

				string Desc = "";
				Desc = Name + " is used to establish a connection to a remote gateway or website by typing an address or ip";

				GameControl.control.ProgramInfo.Add(new CHMSystem (Name, "","Network",Name, CHMSystem.FileType.Folder));
				GameControl.control.ProgramInfo.Add(new CHMSystem ("Description", Desc,Name,"", CHMSystem.FileType.Folder));

				DisplayNotification();
			}
		}

		if (!InfoNames.Contains("dl"))
		{
			if (CMDFunc.Contains("dl")) 
			{
				int ID = 0;
				ID = CMDFunc.IndexOf ("dl");

				string Name = "";
				Name = CMDNames[ID];

				string Desc = "";
				Desc = "To use " + Name + " you have to type in a file name that is found on a remote server like this " + Name + "^testfile";

				GameControl.control.ProgramInfo.Add(new CHMSystem (Name, "","Network",Name, CHMSystem.FileType.Folder));
				GameControl.control.ProgramInfo.Add(new CHMSystem ("Description", Desc,Name,"", CHMSystem.FileType.Folder));

				DisplayNotification();
			}
		}

		if (!InfoNames.Contains("ul"))
		{
			if (CMDFunc.Contains("ul")) 
			{
				int ID = 0;
				ID = CMDFunc.IndexOf ("ul");

				string Name = "";
				Name = CMDNames[ID];

				string Desc = "";
				Desc = "To use " + Name + " you have to type in a file name that is found on your local system like this " + Name + "^testfile";

				GameControl.control.ProgramInfo.Add(new CHMSystem (Name, "","Network",Name, CHMSystem.FileType.Folder));
				GameControl.control.ProgramInfo.Add(new CHMSystem ("Description", Desc,Name,"", CHMSystem.FileType.Folder));

				DisplayNotification();
			}
		}

		if (!InfoNames.Contains("ls"))
		{
			if (CMDFunc.Contains("ls")) 
			{
				int ID = 0;
				ID = CMDFunc.IndexOf ("ls");

				string Name = "";
				Name = CMDNames[ID];

				string Desc = "";
				Desc = Name + " or list can be used as a local or remote command by -l^" + Name + " or -r^"+ Name;

				GameControl.control.ProgramInfo.Add(new CHMSystem (Name, "","Commands",Name, CHMSystem.FileType.Folder));
				GameControl.control.ProgramInfo.Add(new CHMSystem ("Description", Desc,Name,"", CHMSystem.FileType.File));

				DisplayNotification();
			}
		}
	}

	void DisplayNotification()
	{
		noti.ShowNoti = true;
		noti.Notification = "New info has been added";
		if (Customize.cust.PlayNotiSound == false)
		{
			Customize.cust.PlayNotiSound = true;
		}
		noti.playsound = true;
		noti.DisplayTime = 5;
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains (Event.current.mousePosition))
		{
			if (GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [0]))
			{
				appman.SelectedApp = "CHM";
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

		GUI.DragWindow (new Rect (1, 1, 156, 21));
		GUI.Box (new Rect (1, 1, 156, 21), "CHM");

		RenderText();
	}

	void ReloadPage()
	{
		CurrentPage.RemoveRange (0, CurrentPage.Count);

		for (int i = 0; i < GameControl.control.ProgramInfo.Count; i++)
		{
			if (GameControl.control.ProgramInfo [i].Location == CurrentLocation)
			{
				CurrentPage.Add (GameControl.control.ProgramInfo [i]);
			}
		}
	}

	void RenderText()
	{
		if (History.Count > 0) 
		{
			if (GUI.Button (new Rect (0, 25, 40, 21), "Back")) 
			{
				if (ItemText == "") 
				{
					History.RemoveAt (History.Count-1);
					CurrentLocation = History[History.Count-1];
					ReloadPage();
				} 
				else 
				{
					ItemText = "";
				}
			}
		}

		if (History.Count == 0) 
		{
			CurrentLocation = "Main Menu";
			ReloadPage();
		}

		if (CurrentPage.Count == 0) 
		{
			ReloadPage();
		}

		if (ItemText == "") 
		{
			scrollpos = GUI.BeginScrollView(new Rect(1, 60, 180, 180), scrollpos, new Rect(0, 0, 0, scrollsize * 22));
			for (scrollsize = 0; scrollsize < CurrentPage.Count; scrollsize++)
			{
				if (CurrentPage[scrollsize].TargetLocation == "") 
				{
					if (GUI.Button (new Rect (0, scrollsize * 22, 100, 21), CurrentPage[scrollsize].Name))
					{
						ItemText = CurrentPage[scrollsize].Content;
					}
				}
				else
				{
					if (GUI.Button (new Rect (0, scrollsize * 22, 100, 21), CurrentPage[scrollsize].Name)) 
					{
						History.Add (CurrentPage[scrollsize].TargetLocation);
						CurrentLocation = CurrentPage[scrollsize].TargetLocation;
						ReloadPage();
					}
				}
			}
			GUI.EndScrollView ();
		} 
		else 
		{
			GUI.TextArea (new Rect (5, 50, 190, 140),ItemText);
		}
	}
}