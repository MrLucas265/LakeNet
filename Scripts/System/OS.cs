using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OS : MonoBehaviour
{
	private GameObject SysSoftware;
	private GameObject Hardware;
	private GameObject Applications;

	private Computer com;
	private SystemPanel sp;

	private InternetBrowser ib;

	private CPU cpu;
	private RAM ram;

	public Texture2D[] pic;
	public Texture2D[] Icon;
	public int SelectedIcon;

	public bool SetOSUsage;
	public bool StartUpdateOS;


    public bool Show;
    public Rect windowRect = new Rect(100, 100, 200, 200);
	public int native_width = 1920;
	public int native_height = 1080;
	public int windowID;

	private Defalt def;
	private TaskViewer tv;
	private AppMan appman;
	private SoundControl sc;

    private Notepad note;

    public int Index;

	public float CPUUsage;
	public float MemoryUsage;
	public float GraphicsUsage;
	public float DiskUsage;

	public int IconX;
	public int IconY;

	public Texture2D ProgramIcon;
	public float IconSize;
	public int IconCount;

	public bool StartRefresh;

	public float LastClick;

	public string NotLegitCopy;
	public string OSName;
	public string BuildNumber;

	public int Count;
	public int yCount;
	public int MaxIconsPerRow;

	public float xmod;
	public float ymod;

	public float xmodpic;
	public float ymodpic;

	public float IconWidth;
	public float IconHeight;

	public GUISkin skin;
	public GUIStyle FTextSize;
	public GUIStyle BTextSize;

	public List<Color> BackgroundColor = new List<Color>();
	public Color BackgroundColorSet;

	public Texture2D Button;
	public Texture2D EditBox;

	public Rect ContextwindowRect = new Rect (100, 100, 300, 200);
	public bool ShowContext;

	public int ContextMenuID;
	public int SelectedProgram;

	public List<string> ContextMenuOptions = new List<string>();

	public string SelectedOption;

	bool edit;

	private Boot boot;

    public bool Held;



	// Use this for initialization
	void Start () 
	{
		SysSoftware = GameObject.Find("System");
		Hardware = GameObject.Find("Hardware");
		Applications = GameObject.Find("Applications");

		cpu = Hardware.GetComponent<CPU>();
		ram = Hardware.GetComponent<RAM>();
		com = SysSoftware.GetComponent<Computer>();
		def = SysSoftware.GetComponent<Defalt>();
		sp = SysSoftware.GetComponent<SystemPanel>();
		tv = SysSoftware.GetComponent<TaskViewer>();
		ib = Applications.GetComponent<InternetBrowser>();
		appman = SysSoftware.GetComponent<AppMan>();
		sc = GetComponent<SoundControl>();
		boot = GetComponent<Boot>();

        note = Applications.GetComponent<Notepad>();



        if (boot.Terminal == true)
		{
			CPUUsage += 2;
			MemoryUsage += 500;
			GraphicsUsage += 5;
			Index = 2;
			SetOSUsage = false;
			StartUpdateOS = false;
			SetUsage ();
			SelectedIcon = 1;
			GameControl.control.BootTime = 0.4f;
			appman.SelectedApp = "Command Line V2";
		}
		else
		{
			StartUpdateOS = true;
		}

//		if (Customize.cust.native_width != 0)
//		{
//			native_width = Customize.cust.native_width;
//			native_height = Customize.cust.native_height;
//		}

		if (Application.isEditor == true) 
		{
			windowRect = new Rect (0, 0, Screen.width, Screen.height);
		} 
		else 
		{
			windowRect = new Rect (0, 0, Customize.cust.RezX, Customize.cust.RezY);
		}

		IconWidth = 10 * 12;
		xmodpic = 4;
		ymodpic = 8;
		xmod = 13;

		MaxIconsPerRow = 10;

		ContextMenuID = 98;
		ContextwindowRect.width = 100;
		//ContextwindowRect.height = 21*6+7;
	}

	void Update()
	{
		if (StartUpdateOS == true)
		{
			UpdateOS ();
		}

		if (StartRefresh == true) 
		{
			Refresh();
		}
	}

	void SetUsage()
	{
		//hdw.CurCPUBandwidth += CPUUsage;
		//hdw.CurRAMBan += MemoryUsage;
		//hdw.CurGPUBandwidth += GraphicsUsage;

		float CPUUsagePercent = 0;
		CPUUsagePercent = CPUUsage / cpu.MaxCPUSpeed * 100;

		float RAMUsagePercent = 0;
		RAMUsagePercent = MemoryUsage / ram.MaxRAM * 100;

		//cpu.CPUSpeed[0] += CPUUsage;
		//tv.RunningApplications.Add("System OS");
		//tv.CPUList.Add(CPUUsagePercent);
		//tv.MemoryList.Add(RAMUsagePercent);
		////tv.GraphicsList.Add(GraphicsUsage);
		//tv.GraphicsList.Add(0);
		//tv.DiskList.Add(0);
		//tv.NetworkList.Add(0);
		//tv.RunningApplicationsWindowID.Add(windowID);
	}

	void UpdateOS () 
	{
		switch (GameControl.control.SelectedOS.Name) 
		{
		case OperatingSystems.OSName.AppatureOS:
			CPUUsage += 2;
			MemoryUsage += 500;
			GraphicsUsage += 5;
			Index = 2;
			SetOSUsage = false;
			StartUpdateOS = false;
			SetUsage ();
			GameControl.control.BootTime = 0.4f;
			NotLegitCopy = "This copy of Appature is not legit.";
			break;
		case OperatingSystems.OSName.CSOSV1:
			CPUUsage += 1;
			MemoryUsage += 256;
			GraphicsUsage += 2;
			Index = 2;
			SetOSUsage = false;
			StartUpdateOS = false;
			SetUsage();
			SelectedIcon = 1;
			GameControl.control.BootTime = 0.4f;
			break;
		case OperatingSystems.OSName.TreeOS:
			CPUUsage += 1;
			MemoryUsage += 256;
			GraphicsUsage += 2;
			Index = 2;
			SetOSUsage = false;
			StartUpdateOS = false;
			SetUsage();
			GameControl.control.BootTime = 0.4f;
			break;
		case OperatingSystems.OSName.FluidicIceOS:
			CPUUsage += 0.1f;
			MemoryUsage += 256;
			GraphicsUsage += 2;
			if (Customize.cust.UseCustomBG == true) 
			{
				Index = 2;
			} 
			else 
			{
				Index = 1;
			}
			SetOSUsage = false;
			StartUpdateOS = false;
			SetUsage();
			SelectedIcon = 1;
			GameControl.control.BootTime = 0.4f;
			OSName = "FluidicIceOS A1";
			BuildNumber = "IT:Build 3.5.1";
			NotLegitCopy = "This copy of FluidicIceOS is not legit.";
			break;
        case OperatingSystems.OSName.QuantinitumOS:
            CPUUsage += 0.1f;
            MemoryUsage += 256;
            GraphicsUsage += 2;
            Index = 2;
            SetOSUsage = false;
            StartUpdateOS = false;
            SetUsage();
            SelectedIcon = 1;
            GameControl.control.BootTime = 0.4f;
            OSName = "FluidicIceOS A1";
            BuildNumber = "IT:Build 3.5.1";
            NotLegitCopy = "This copy of FluidicIceOS is not legit.";
            break;
            case OperatingSystems.OSName.HackLink:
			CPUUsage += 1;
			MemoryUsage += 256;
			GraphicsUsage += 2;
			Index = 2;
			SetOSUsage = false;
			StartUpdateOS = false;
			SetUsage();
			SelectedIcon = 1;
			GameControl.control.BootTime = 0.4f;
			break;
		case OperatingSystems.OSName.NOS:
			CPUUsage += 1;
			MemoryUsage += 256;
			GraphicsUsage += 2;
			Index = 2;
			SetOSUsage = false;
			StartUpdateOS = false;
			SetUsage();
			SelectedIcon = 1;
			GameControl.control.BootTime = 0.4f;
			break;
		}

//		switch (GameControl.control.defaltOS) 
//		{
//
//		case "":
//			if (SetOSUsage == true) 
//			{
//				//hdw.CurCPUBandwidth = 0;
//				//hdw.CurRAMBan = 0;
//				//hdw.CurGPUBandwidth = 0;
//				Index = 0;
//				SetOSUsage = false;
//				StartUpdateOS = false;
//			}
//			break;
//
//		case "CSOSV1.0":
//			if (SetOSUsage == true) 
//			{
//				CPUUsage += 2;
//				MemoryUsage += 500;
//				GraphicsUsage += 5;
//				Index = 1;
//				SetOSUsage = false;
//				StartUpdateOS = false;
//				SetUsage();
//			}
//			break;
//
//		case "Custom":
//			if (SetOSUsage == true) 
//			{
//				CPUUsage += 2;
//				MemoryUsage += 500;
//				GraphicsUsage += 5;
//				Index = 2;
//				SetOSUsage = false;
//				StartUpdateOS = false;
//				SetUsage();
//			}
//			break;
//
//		case "TreeOS":
//			if (SetOSUsage == true) 
//			{
//				CPUUsage += 2;
//				MemoryUsage += 500;
//				GraphicsUsage += 5;
//				Index = 7;
//				SetOSUsage = false;
//				StartUpdateOS = false;
//				SetUsage();
//			}
//			break;
//
//		case "Ice":
//			if (SetOSUsage == true) 
//			{
//				CPUUsage += 2;
//				MemoryUsage += 500;
//				GraphicsUsage += 5;
//				Index = 7;
//				SetOSUsage = false;
//				StartUpdateOS = false;
//				SetUsage();
//				SelectedIcon = 1;
//				GameControl.control.BootTime += 30;
//			}
//			break;
//		}
	}

	void OpenFld()
	{
        PlayClickSound();
        if (com.show == true && com.enabled == true)
        {
            com.History.Add(com.ComAddress);
            com.ComAddress = GameControl.control.DesktopIconList[SelectedProgram].Target;
            com.PageFile.RemoveRange(0, com.PageFile.Count);
            SelectedProgram = -1;
            com.MenuSelected = 0;
        }
        if (com.show == false || com.enabled == false)
        {
            appman.ProgramName = GameControl.control.DesktopIconList[SelectedProgram].Name;
            appman.SelectedApp = "Computer";
            com.History.Add(com.ComAddress);
            com.ComAddress = GameControl.control.DesktopIconList[SelectedProgram].Target;
            com.PageFile.RemoveRange(0, com.PageFile.Count);
            SelectedProgram = -1;
            com.MenuSelected = 0;
        }
	}

    void OpenTxt()
    {
        PlayClickSound();
        if (note.show == true && note.enabled == true)
        {
            note.CurrentWorkingTitle = GameControl.control.DesktopIconList[SelectedProgram].Name;
            note.TypedTitle = GameControl.control.DesktopIconList[SelectedProgram].Name;
            note.TypedText = GameControl.control.DesktopIconList[SelectedProgram].Content;
            note.SaveLocation = GameControl.control.DesktopIconList[SelectedProgram].Location;
            note.ShowFileContent = true;
        }
        if (note.show == false || note.enabled == false)
        {
            appman.ProgramName = GameControl.control.DesktopIconList[SelectedProgram].Name;
            appman.SelectedApp = "Notepad";
            note.CurrentWorkingTitle = GameControl.control.DesktopIconList[SelectedProgram].Name;
            note.TypedTitle = GameControl.control.DesktopIconList[SelectedProgram].Name;
            note.TypedText = GameControl.control.DesktopIconList[SelectedProgram].Content;
            note.SaveLocation = GameControl.control.DesktopIconList[SelectedProgram].Location;
            note.ShowFileContent = true;
        }
        SelectedProgram = -1;
    }

    void OpenExe()
	{
		PlayClickSound ();
        appman.ProgramName = GameControl.control.DesktopIconList[SelectedProgram].Name;
        appman.SelectedApp = GameControl.control.DesktopIconList[SelectedProgram].Target;
		SelectedProgram = -1;
		com.MenuSelected = 0;
	}

	void OpenReal()
	{
		PlayClickSound ();
		System.Diagnostics.Process.Start(GameControl.control.DesktopIconList[SelectedProgram].Target);
		SelectedProgram = -1;
		com.MenuSelected = 0;
	}

	void OpenRealWeb()
	{
		PlayClickSound ();
		Application.OpenURL(GameControl.control.DesktopIconList[SelectedProgram].Target);
		SelectedProgram = -1;
		com.MenuSelected = 0;
	}

	void OpenWeb()
	{
		PlayClickSound ();
		ib.AddressBar = GameControl.control.DesktopIconList[SelectedProgram].Target;
		ib.Inputted = GameControl.control.DesktopIconList[SelectedProgram].Target;
		SelectedProgram = -1;
		com.MenuSelected = 0;
	}

	void OnGUI()
	{
		GUI.skin = skin;
		FTextSize = new GUIStyle();
		BTextSize = new GUIStyle();

        IconWidth = 80;

        FTextSize.fontSize = 14;
        BTextSize.fontSize = 14;

        IconSize = IconHeight / 2;

        GUI.depth = 100;
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if(Show == true)
		{
			//GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
			GUI.BringWindowToBack (windowID);
		}

		if (ShowContext == true) 
		{
			ContextwindowRect.height = 21*ContextMenuOptions.Count+2;
			GUI.skin = com.Skin[GameControl.control.GUIID];
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			ContextwindowRect = WindowClamp.ClampToScreen(GUI.Window(ContextMenuID,ContextwindowRect,DoMyContextWindow,""));
		}

		if (Input.GetMouseButtonUp (0) || Input.GetMouseButtonUp (1) || Input.GetMouseButtonUp (2))
		{
			if (!ContextwindowRect.Contains (Event.current.mousePosition)) 
			{
				ShowContext = false;
			} 
		}
	}

	void AddContextOptions()
	{
		if (GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Exe)
		{
			ContextMenuOptions.Add ("Open");
			//ContextMenuOptions.Add ("Copy");
			ContextMenuOptions.Add ("Rename");
			if (!GameControl.control.QuickLaunchNames.Contains (GameControl.control.DesktopIconList [SelectedProgram].Name)) 
			{
				ContextMenuOptions.Add ("Pin to QL");
			}
			else
			{
				ContextMenuOptions.Add ("Unpin from QL");
			}
			ContextMenuOptions.Add ("Delete");
			//ContextMenuOptions.Add ("Create Icon");
			ContextMenuOptions.Add ("Details");
		}
        if (GameControl.control.DesktopIconList[SelectedProgram].Type == ProgramSystem.ProgramType.Txt)
        {
            ContextMenuOptions.Add("Open");
            //ContextMenuOptions.Add ("Copy");
            ContextMenuOptions.Add("Rename");
            if (!GameControl.control.QuickLaunchNames.Contains(GameControl.control.DesktopIconList[SelectedProgram].Name))
            {
                ContextMenuOptions.Add("Pin to QL");
            }
            else
            {
                ContextMenuOptions.Add("Unpin from QL");
            }
            ContextMenuOptions.Add("Delete");
            //ContextMenuOptions.Add ("Create Icon");
            ContextMenuOptions.Add("Details");
        }
        if (GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Real)
		{
			ContextMenuOptions.Add ("Open");
			//ContextMenuOptions.Add ("Copy");
			ContextMenuOptions.Add ("Rename");
			if (!GameControl.control.QuickLaunchNames.Contains (GameControl.control.DesktopIconList [SelectedProgram].Name)) 
			{
				ContextMenuOptions.Add ("Pin to QL");
			}
			else
			{
				ContextMenuOptions.Add ("Unpin from QL");
			}
			ContextMenuOptions.Add ("Delete");
			//ContextMenuOptions.Add ("Create Icon");
			ContextMenuOptions.Add ("Details");
		}
		if (GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Dir || GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Fdl)
		{
			ContextMenuOptions.Add ("Open");
			//ContextMenuOptions.Add ("Create Icon");
			ContextMenuOptions.Add ("Delete");
			ContextMenuOptions.Add ("Details");
		}
		//else 
		//{
		//	ContextMenuOptions.Add ("Open");
		//	//ContextMenuOptions.Add ("Copy");
		//	ContextMenuOptions.Add ("Rename");
		//	ContextMenuOptions.Add ("Delete");
		//	//ContextMenuOptions.Add ("Create Icon");
		//	ContextMenuOptions.Add ("Details");
		//}
	}

	void DoMyContextWindow(int WindowID)
	{
		//GUI.Box (new Rect (Input.mousePosition.x, Input.mousePosition.y, 100, 200), "");
		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		if (ContextMenuOptions.Count <= 0) 
		{
			AddContextOptions();
		}

		if (ContextMenuOptions.Count > 0) 
		{
			for (int i = 0; i < ContextMenuOptions.Count; i++) 
			{
				if (GUI.Button (new Rect (1, 1+21*i, ContextwindowRect.width - 2, 21), ContextMenuOptions[i])) 
				{
					SelectedOption = ContextMenuOptions [i];
				}
			}
		}

		switch (SelectedOption) 
		{
		case "Open":
			switch (GameControl.control.DesktopIconList [SelectedProgram].Type) 
			{
			case ProgramSystem.ProgramType.Txt:
				OpenTxt();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Fdl:
				OpenFld();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Ins:
				//OpenIns();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Exe:
				OpenExe();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Real:
				OpenReal();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Web:
				OpenWeb();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.RealWeb:
				OpenRealWeb();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Dir:
				OpenFld();
				CloseContextMenu();
				break;
			}
			break;
		case "Copy":
			//CopySystem ();
			PlayClickSound ();
			CloseContextMenu();
			break;
		case "Rename":
			//RenameContextOption();
			break;
		case "Delete":
			DeleteSystem();
			PlayClickSound();
			CloseContextMenu();
			break;
		case "Create Icon":
			//CreateIcon();
			PlayClickSound();
			CloseContextMenu();
			break;
		case "Pin to QL":
			CreateQLI();
			PlayClickSound();
			CloseContextMenu();
			break;
		case "Unpin from QL":
			CreateQLI();
			PlayClickSound();
			CloseContextMenu();
			break;
		case "Details":
            Details();
			PlayClickSound();
			CloseContextMenu();
			break;
		}
	}

	void CreateQLI()
	{
		if (GameControl.control.QuickProgramList.Count > 0)
		{
			if (!GameControl.control.QuickLaunchNames.Contains (GameControl.control.DesktopIconList[SelectedProgram].Name))
			{
				GameControl.control.QuickProgramList.Add (GameControl.control.DesktopIconList[SelectedProgram]);
				GameControl.control.QuickLaunchNames.Add(GameControl.control.DesktopIconList[SelectedProgram].Name);
			} 
			else 
			{
				GameControl.control.QuickProgramList.Remove (GameControl.control.DesktopIconList[SelectedProgram]);
				GameControl.control.QuickLaunchNames.Remove(GameControl.control.DesktopIconList[SelectedProgram].Name);
			}
		} 
		else 
		{
			GameControl.control.QuickProgramList.Add (GameControl.control.DesktopIconList[SelectedProgram]);
			GameControl.control.QuickLaunchNames.Add(GameControl.control.DesktopIconList[SelectedProgram].Name);
		}
	}

	void DeleteSystem()
	{
		GameControl.control.DesktopIconList.RemoveAt(SelectedProgram);
	}

    void Details()
    {
        PlayClickSound();
        appman.SelectedApp = "DetailsMenu";
        SelectedProgram = -1;
        com.MenuSelected = 0;
    }

    void DoMyWindow(int WindowID)
	{
        //GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.C)
        {
            Held = true;
        }

        if (Event.current.type == EventType.keyUp && Event.current.keyCode == KeyCode.C)
        {
            Held = false;
        }

        if (Held == true)
        {
            if (Event.current.type == EventType.ScrollWheel)
            {
                if (Event.current.delta.y < 0)
                {
                    IconHeight++;
                }

                if (Event.current.delta.y > 0)
                {
                    IconHeight--;
                }
            }
        }

        if (Customize.cust.CustomTexFileNames [4] == "") 
		{
			GUI.DrawTexture(new Rect (0, 0, windowRect.width, windowRect.height), pic[Index]);
		} 
		else 
		{
			GUI.DrawTexture(new Rect (0, 0, windowRect.width, windowRect.height), pic[Index]);
		}

		if (GameControl.control.SerialKey == "")
		{
			float OSNameWidth = OSName.Length+60;
			float BuildNumberWidth = BuildNumber.Length+60;
			//GUI.Label(new Rect (2, windowRect.height-78, 300, 22),"" +  GameControl.control.WebsiteFiles.Count);
			GUI.Label(new Rect (2, windowRect.height-54, 300, 22), OSName);
			GUI.Label(new Rect (2, windowRect.height-38, 300, 22), BuildNumber);
			GUI.Label(new Rect (2, windowRect.height-22, 300, 22), NotLegitCopy);
		}
		Icons();

        //		if (SelectedProgram == -1)
        //		{
        //			if (Input.GetMouseButtonUp (1)) 
        //			{
        //				if (new Rect (windowRect.x, windowRect.y + 30, windowRect.width, windowRect.height).Contains (Event.current.mousePosition))
        //				{
        //					ContextwindowRect.x = Input.mousePosition.x;
        //					ContextwindowRect.y = Screen.height - Input.mousePosition.y;
        //					ShowContext = true;
        //					GUI.BringWindowToFront(98);
        //				}
        //			}
        //		}
    }

	void CloseContextMenu()
	{
		ContextMenuOptions.RemoveRange (0, ContextMenuOptions.Count);
		SelectedOption = "";
		ShowContext = false;
	}

	void Refresh()
	{
		
	}

	void PlayClickSound()
	{
		sc.SoundSelect = 3;
		sc.PlaySound();
	}

	private int PerceivedBrightness(Color c)
	{
		return (int)Mathf.Sqrt(
			c.r * c.r * .299f +
			c.g * c.g * .587f +
			c.b * c.b * .114f);
	}

    public void RunTerminal()
    {
        appman.SelectedApp = "Command Line V3";
    }

    public void RunGateway()
    {
        appman.SelectedApp = "Computer";
    }

    public void RunDeviceManager()
    {
        appman.SelectedApp = "Device Manager";
    }

    public void RunProgramManager()
    {
        appman.SelectedApp = "Task Viewer";
    }

	public void RunProgramExecutor()
	{
		appman.SelectedApp = "Executor";
	}

	public void RunStartMenu()
	{
		appman.SelectedApp = "Start Menu";
	}

	public void RunQABugReport()
	{
		appman.SelectedApp = "Bug Report";
	}


	void Icons()
	{
		int rows = 0;
		float x = 0;
		float y = 0;

		if (GameControl.control.DesktopIconList.Count > 0)
		{
			for (int i = 0; i < GameControl.control.DesktopIconList.Count; i++) 
			{
				switch (GameControl.control.DesktopIconList [i].Type) 
				{
				case ProgramSystem.ProgramType.Dir:
					GUI.Box (new Rect (x + IconWidth/xmodpic, y + 30 + IconSize/ymodpic, IconSize, IconSize), com.Icon [0],FTextSize);
					if (GUI.Button (new Rect (x, y + 30, IconWidth, IconHeight),"")) 
					{
						if (Input.GetMouseButtonUp (0)) 
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
							{
								PlayClickSound ();
								SelectedProgram = i;
								OpenFld();
							} 
							else 
							{
								PlayClickSound();
								SelectedProgram = i;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp (1)) 
						{
							ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
							SelectedProgram = i;
							if (new Rect (x, y + 30, IconWidth, IconHeight).Contains (Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(98);
							}
						}
					}
					break;
				case ProgramSystem.ProgramType.Fdl:
					GUI.Box (new Rect (x + IconWidth/xmodpic, y + 30 + IconSize/ymodpic, IconSize, IconSize), com.Icon [1],FTextSize);
					//GUI.TextArea (new Rect (x, y + 30, IconSize, IconSize), GameControl.control.DesktopIconList [i].Name);
					if (GUI.Button (new Rect (x, y + 30, IconWidth, IconHeight),"")) 
					{
						if (Input.GetMouseButtonUp (0)) 
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
							{
								PlayClickSound ();
								SelectedProgram = i;
								OpenFld();
							} 
							else 
							{
								PlayClickSound();
								SelectedProgram = i;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp (1)) 
						{
							ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
							SelectedProgram = i;
							if (new Rect (x, y + 30, IconWidth, IconHeight).Contains (Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(98);
							}
						}
					}
					break;
				case ProgramSystem.ProgramType.Exe:
					GUI.Box (new Rect (x + IconWidth/xmodpic, y + 30 + IconSize/ymodpic, IconSize, IconSize), com.Icon [2],FTextSize);
					if (GUI.Button (new Rect (x, y + 30, IconWidth, IconHeight),"")) 
					{
						if (Input.GetMouseButtonUp (0)) 
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
							{
								PlayClickSound ();
								SelectedProgram = i;
								OpenExe();
							} 
							else 
							{
								PlayClickSound();
								SelectedProgram = i;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp (1)) 
						{
							ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
							SelectedProgram = i;
							if (new Rect (x, y + 30, IconWidth, IconHeight).Contains (Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(98);
							}
						}
					}
					break;
				case ProgramSystem.ProgramType.Real:
					GUI.Box (new Rect (x + IconWidth/xmodpic, y + 30 + IconSize/ymodpic, IconSize, IconSize), com.Icon [2],FTextSize);
					if (GUI.Button (new Rect (x, y + 30, IconWidth, IconHeight),"")) 
					{
						if (Input.GetMouseButtonUp (0)) 
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
							{
								PlayClickSound ();
								SelectedProgram = i;
								OpenReal();
							} 
							else 
							{
								PlayClickSound();
								SelectedProgram = i;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp (1)) 
						{
							ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
							SelectedProgram = i;
							if (new Rect (x, y + 30, IconWidth, IconHeight).Contains (Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(98);
							}
						}
					}
					break;
				case ProgramSystem.ProgramType.Web:
					GUI.Box (new Rect (x + IconWidth/xmodpic, y + 30 + IconSize/ymodpic, IconSize, IconSize), com.Icon [2],FTextSize);
					if (GUI.Button (new Rect (x, y + 30, IconWidth, IconHeight),"")) 
					{
						if (Input.GetMouseButtonUp (0)) 
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
							{
								PlayClickSound ();
								SelectedProgram = i;
								OpenWeb();
							} 
							else 
							{
								PlayClickSound();
								SelectedProgram = i;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp (1)) 
						{
							ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
							SelectedProgram = i;
							if (new Rect (x, y + 30, IconWidth, IconHeight).Contains (Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(98);
							}
						}
					}
					break;
				case ProgramSystem.ProgramType.RealWeb:
					GUI.Box (new Rect (x + IconWidth/xmodpic, y + 30 + IconSize/ymodpic, IconSize, IconSize), com.Icon [2],FTextSize);
					if (GUI.Button (new Rect (x, y + 30, IconWidth, IconHeight),"")) 
					{
						if (Input.GetMouseButtonUp (0)) 
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
							{
								PlayClickSound ();
								SelectedProgram = i;
								OpenRealWeb();
							} 
							else 
							{
								PlayClickSound();
								SelectedProgram = i;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp (1)) 
						{
							ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
							SelectedProgram = i;
							if (new Rect (x, y + 30, IconWidth, IconHeight).Contains (Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(98);
							}
						}
					}
					break;
				case ProgramSystem.ProgramType.Ins:
					GUI.Box (new Rect (x + IconWidth/xmodpic, y + 30 + IconSize/ymodpic, IconSize, IconSize), com.Icon [3],FTextSize);
					if (GUI.Button (new Rect (x, y + 30, IconWidth, IconHeight),"")) 
					{
						if (Input.GetMouseButtonUp (0)) 
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
							{
								PlayClickSound ();
								SelectedProgram = i;
								OpenExe();
							} 
							else 
							{
								PlayClickSound();
								SelectedProgram = i;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp (1)) 
						{
							ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
							SelectedProgram = i;
							if (new Rect (x, y + 30, IconWidth, IconHeight).Contains (Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(98);
							}
						}
					}
					break;
				case ProgramSystem.ProgramType.File:
					GUI.Box (new Rect (x + IconWidth/xmodpic, y + 30 + IconSize/ymodpic, IconSize, IconSize), com.Icon [4],FTextSize);
					if (GUI.Button (new Rect (x, y + 30, IconWidth, IconHeight),"")) 
					{
						if (Input.GetMouseButtonUp (0)) 
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
							{
								PlayClickSound ();
								SelectedProgram = i;
							} 
							else 
							{
								PlayClickSound();
								SelectedProgram = i;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp (1)) 
						{
							ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
							SelectedProgram = i;
							if (new Rect (x, y + 30, IconWidth, IconHeight).Contains (Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(98);
							}
						}
					}
					break;
				case ProgramSystem.ProgramType.Txt:
                    GUI.Box(new Rect(x + IconWidth / xmodpic, y + 30 + IconSize / ymodpic, IconSize, IconSize), com.Icon[4], FTextSize);
                    if (GUI.Button(new Rect(x, y + 30, IconWidth, IconHeight), ""))
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu)
                            {
                                PlayClickSound();
                                SelectedProgram = i;
                            }
                            else
                            {
                                PlayClickSound();
                                SelectedProgram = i;
                            }
                            LastClick = Time.time;
                        }
                        if (Input.GetMouseButtonUp(1))
                        {
                            ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
                            SelectedProgram = i;
                            if (new Rect(x, y + 30, IconWidth, IconHeight).Contains(Event.current.mousePosition))
                            {
                                ContextwindowRect.x = Input.mousePosition.x;
                                ContextwindowRect.y = Screen.height - Input.mousePosition.y;
                                ShowContext = true;
                                GUI.BringWindowToFront(98);
                            }
                        }
                    }
                    break;
				}
//				int TempX = (int)x;
//				int TempY = (int)y;
//				BackgroundColorSet.r = pic [2].GetPixel(TempX, TempY).r;
//				BackgroundColorSet.g = pic [2].GetPixel(TempX, TempY).g;
//				BackgroundColorSet.b = pic [2].GetPixel(TempX, TempY).b;
//				if (BackgroundColor.Count <= GameControl.control.DesktopIconList.Count) 
//				{
//					BackgroundColor.Add(pic [2].GetPixel(TempX, TempY));
//				}
				//TextSize.normal.textColor = (PerceivedBrightness(BackgroundColor[i]) > 130 ? Color.black : Color.white);
				//TextSize.font = (Font)Resources.Load ("Fonts/RedVelvetica-ShadowsBold");
				//TextSize.normal.textColor = com.colors[Customize.cust.FontColorInt];

				FTextSize.normal.background = Button;
				BTextSize.normal.background = Button;
				BTextSize.alignment = TextAnchor.MiddleCenter;
				BTextSize.wordWrap = true;
				FTextSize.alignment = TextAnchor.MiddleCenter;
				FTextSize.wordWrap = true;
				FTextSize.normal.textColor = Color.black;

				GUI.Label (new Rect (x, y +0 + IconHeight + 5 + 1, IconWidth, 23), GameControl.control.DesktopIconList [i].Name,FTextSize);
				GUI.Label(new Rect (x, y +0 + IconHeight + 5 - 1, IconWidth, 23), GameControl.control.DesktopIconList [i].Name,FTextSize);
				GUI.Label(new Rect (x, y + 0  + IconHeight + 5, IconWidth, 23), GameControl.control.DesktopIconList [i].Name,FTextSize);
				GUI.Label(new Rect (x, y + 0 + IconHeight + 5, IconWidth, 23), GameControl.control.DesktopIconList [i].Name,FTextSize);
				BTextSize.normal.textColor = Color.white;
				GUI.Label(new Rect (x, y+IconHeight+5, IconWidth, 23), GameControl.control.DesktopIconList [i].Name,BTextSize);
				//GUI.Label (new Rect (x+IconWidth/xmod, y+IconHeight+15, 200, 23), GameControl.control.DesktopIconList [i].Name,BTextSize);

				rows++;
				x += IconWidth + 30;
				if (rows == MaxIconsPerRow)
				{
					rows = 0;
					x = 0;
					y += IconHeight + 30;
				}
			}
		}
	}
}
