using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CLICommandsV2 : MonoBehaviour
{
	public List<string> PastCommands = new List<string>();
	public List<string> Functions = new List<string>();
	public List<string> CommandNames = new List<string>();
	public List<string> DirectoryHistory = new List<string>();
	public List<CLICMDS> SystemCommands = new List<CLICMDS>();
	public string CommandLine;
	public string[] inputArray;
	public string[] ParseArray;

	public bool AutoScroll;

	private InternetBrowser ib;
	//private Notepad note;
	private Tracer trace;
	//private MissionBrow mb;
	//private CurContracts cc;
	//private SiteList sl;
	//private AccLog al;
	//private Desktop dsk;
	//private Tourtial tut;
	//private DirSearch ds;
	//private TreeView tv;
	//private Clock clk;
	//private CD cd;
	private JailDew jd;
	private Unicom uc;
	private Test test;
	private Boot boot;
	private DesktopEnviroment os;
	private Defalt def;
	//private SoundControl sc;
	private ErrorProm ep;
	private Computer com;
	private SystemPanel sp;
	private CLIV2 cliv2;

	private MusicPlayer mp;

	private SoundControl sc;

	private Notepad note;

	private GameObject system;
	private GameObject hardware;
	private GameObject hacking;
	private GameObject prompt;
	private GameObject applications;
	private GameObject database;
	private GameObject camobj;
	private GameObject Missions;
	private GameObject Cam;
	private GameObject Crash;
	private GameObject Desktops;

	private SysCrashMan SCM;

	private ScreenShot screenshot;

	private MissionGen misgen;
	private CurContracts curcon;

	private NotfiPrompt noti;

	private RAM ram;
	private PSU psu;
	private HardDrives hdd;
	private Motherboard mb;
	private Networks net;

	private FileUtility fu;
	private AppMan appman;

	public int FileIndex;
	public float FileSize;
	public string FileName;
	public string FileLocation;
	public string FileType;
	public string FileTarget;
	public string FileContent;
	public float FileVersion;
	public ProgramSystemv2 ProgramFile;

	public string SpecialChar = "!@#$%^&*()_+-=<>?:{}[]';/.,";
	public string Uppercase = "ABCDEFGHIJKLMNOQRSTUVWXYZ";
	public string Lowercase = "abcdefghijklmnopqrstuvwxyz";
	public string AllCharacters = "!@#$%^&*()_+-=<>?:{}[]';/.,ABCDEFGHIJKLMNOQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
	public string Numbers = "1234567890";
	public string CustomCharacterCheck;

	public bool ColorErrorCheck;

	public bool UpdateCheck;

	public string FolderName;
	public string ComAddress;

	public string Username;
	public string Password;
	public string storedConnection;

	public string Parse;
	public string TypedFileName;
	public string LastItem;
	public int ParseArrayLength;
	public int CurrentParse;
	public int NameIndex;

	public bool UpdateCommand;
	public int CommandIndex;

	public string twd;
	public int localdirectoryid;

	public bool writeInfo;

	public ProgramSystem TempCopy;

	public int SelectedFolder;

	public bool SetScrollPos;

	private ShutdownProm ShutDownWindow;

	public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
	public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

	public ProgramSystem HeldFile;

	public ColorSystem OSColour;
	public OSFPCSystem OSFPC;

	public string StringCommand;
	public bool EnableStringCommandCheck;
	public bool StringCommandDone;
	public List<string> ListOfStringCommands = new List<string>();
	public string StringCommandTask;
	public string FileExplorerFileType;

	// Use this for initialization
	void Start ()
	{
		applications = GameObject.Find("Applications");
		prompt = GameObject.Find("Prompts");
		hacking = GameObject.Find("Hacking");
		system = GameObject.Find("System");
		hardware = GameObject.Find("Hardware");
		database = GameObject.Find("Database");
		camobj = GameObject.Find("Camera");
		Missions = GameObject.Find("Missions");
		Crash = GameObject.Find("Crash");
		Desktops = GameObject.Find("Desktops");

		ShutDownWindow = prompt.GetComponent<ShutdownProm>();


		sp = system.GetComponent<SystemPanel>();
		noti = prompt.GetComponent<NotfiPrompt>();
		fu = system.GetComponent<FileUtility> ();
		appman = system.GetComponent<AppMan> ();

		cliv2 = system.GetComponent<CLIV2> ();

		SCM = Crash.GetComponent<SysCrashMan> ();

		note = applications.GetComponent<Notepad> ();

		misgen = Missions.GetComponent<MissionGen> ();
		curcon = Missions.GetComponent<CurContracts>();

		AfterStart();

		if(twd == "")
		{
			twd = "Gateway";
		}

		if (Application.isEditor)
		{
			Customize.cust.TerminalFontSize = 14;
			Customize.cust.TerminalTextPosMod = 7.5f;
			Customize.cust.Mode = "Small";
			//Customize.cust.TerminalCommandCharacterSplit = " ";
			//Customize.cust.TerminalSpaceCharacterSplit = @"\";
		}

		StringCommandTask = "na";
	}

	void AfterStart()
	{
		screenshot = applications.GetComponent<ScreenShot>();

		mp = applications.GetComponent<MusicPlayer>();
		ib = applications.GetComponent<InternetBrowser>();
		jd = database.GetComponent<JailDew>();
		uc = GetComponent<Unicom>();
		test = GetComponent<Test>();
		//cd = GetComponent<CD>();
		//dsk = GetComponent<Desktop>();
		com = GetComponent<Computer>();
		//note = GetComponent<Notepad>();
		trace = GetComponent<Tracer>();
		//mb = GetComponent<MissionBrow>();
		//cc = GetComponent<CurContracts>();
		//sl = GetComponent<SiteList>();
		//al = GetComponent<AccLog>();
		//tut = GetComponent<Tourtial>();
		//ds = GetComponent<DirSearch>();
		//tv = GetComponent<TreeView>();
		//clk = GetComponent<Clock>();
		//ep = Prompt.GetComponent<ErrorProm>();
		boot = GetComponent<Boot>();
		os = GetComponent<DesktopEnviroment>();
		def = GetComponent<Defalt>();
		sc = system.GetComponent<SoundControl>();

		ram = hardware.GetComponent<RAM>();
		psu = hardware.GetComponent<PSU>();
		mb = hardware.GetComponent<Motherboard>();
		net = hardware.GetComponent<Networks>();

		ep = prompt.GetComponent<ErrorProm>();

		//sc = GetComponent<SoundControl>();

		if (GameControl.control.Commands.Count > 0)
		{
			for (int i = 0; i < SystemCommands.Count; i++) 
			{
				if (PastCommands [0] == "") 
				{
					if (SystemCommands [i].Func == "help") 
					{
						PastCommands.Add ("Type " + SystemCommands[i].Name + " for the list of system commands");
					}
				}
			}
		}
	}

	void Update()
	{
		if (GameControl.control.Commands.Count > 0)
		{
			for (int i = 0; i < SystemCommands.Count; i++) 
			{
				if (PastCommands.Count == 0) 
				{
					if (SystemCommands [i].Func == "-help") 
					{
						if(GameControl.control.GatewayStatus.Terminal == true)
						{
							PastCommands.Add("Command-Line Interface Version 3.0");
						}
						PastCommands.Add ("Type " + GameControl.control.Commands[i].Name + " for the list of system commands");
					}
				}
			}
		}

		if(EnableStringCommandCheck == true)
		{
			ProcessingString();
		}

		if(StringCommandTask != "na")
		{
			switch(StringCommandTask)
			{
				case "MakeFakeApp":
					if(StringCommandDone)
					{
						RunFakeApps(StringCommand);
					}
					break;
			}
		}
	}

	void StartProcessingString()
	{
		ResetStringCommand();
		EnableStringCommandCheck = true;
	}

	void ResetStringCommand()
	{
		StringCommand = "";
		StringCommandDone = false;
		ListOfStringCommands.RemoveRange(0, ListOfStringCommands.Count);
	}

	void ProcessingString()
	{
		for (int i = 0; i < ParseArray.Length; i++)
		{
			ListOfStringCommands.Add(ParseArray[i]);
		}
		ListOfStringCommands.RemoveAt(0);
		for (int i = 0; i < ListOfStringCommands.Count; i++)
		{
			if(i == 0)
			{
				StringCommand += ListOfStringCommands[i];
			}
			else
			{
				StringCommand += " " + ListOfStringCommands[i];
			}
		}
		StringCommandDone = true;
		EnableStringCommandCheck = false;
	}

	public void SetSystemCommands()
	{
		if(SystemCommands.Count == 0)
		{
			//MAIN
			SystemCommands.Add(new CLICMDS("-help","-help", "-help",""));
			SystemCommands.Add(new CLICMDS("help","help", "help", ""));
			SystemCommands.Add(new CLICMDS("listcommands","listcommands", "listcommands", ""));
			//DEV
			SystemCommands.Add(new CLICMDS("aiprorun", "aiprun", "aiprogramrun", ""));
			SystemCommands.Add(new CLICMDS("version","ver", "version", ""));
			SystemCommands.Add(new CLICMDS("errorlog", "elog", "errorlog", ""));
			SystemCommands.Add(new CLICMDS("viewrep", "viewrep", "viewrep", ""));
			SystemCommands.Add(new CLICMDS("accountinfo", "accinfo", "accountinfo", ""));
			SystemCommands.Add(new CLICMDS("doubleclickdelay", "dblclkdelay", "doubleclickdelay", ""));
			SystemCommands.Add(new CLICMDS("setbankbalance", "setbnkbal", "setbankbalance", ""));
			SystemCommands.Add(new CLICMDS("printinfo", "prntinfo", "writeinfo", ""));
			SystemCommands.Add(new CLICMDS("url", "url", "url", ""));
			SystemCommands.Add(new CLICMDS("error", "error", "error", ""));
			SystemCommands.Add(new CLICMDS("notification", "noti", "noti", ""));
			SystemCommands.Add(new CLICMDS("syscrash", "syscrash", "syscrash", ""));
			SystemCommands.Add(new CLICMDS("skipmission", "skpmis", "skipmission", ""));
			SystemCommands.Add(new CLICMDS("settimemod", "settimemod", "settimemod", ""));
			SystemCommands.Add(new CLICMDS("enablemusicplayer", "enablemusicply", "enablemusicplayer", ""));
			SystemCommands.Add(new CLICMDS("openchatroom", "opnchatroom", "openchatroom", ""));
			SystemCommands.Add(new CLICMDS("shutdown", "shutdwn", "shutdown", ""));
			SystemCommands.Add(new CLICMDS("logout", "logout", "logout", ""));
			SystemCommands.Add(new CLICMDS("restart", "restart", "restart", ""));
			SystemCommands.Add(new CLICMDS("safemode", "safemode", "safemode", ""));
			SystemCommands.Add(new CLICMDS("installdefaltos", "allos", "installdefaltos", ""));
			SystemCommands.Add(new CLICMDS("createrealexe", "mkreal", "createrealexe", ""));
			SystemCommands.Add(new CLICMDS("createnewbankaccount", "mkbnkacc", "createnewbankaccount", ""));
			SystemCommands.Add(new CLICMDS("mkfakeapp", "mkfakeapp", "mkfakeapp", ""));
			SystemCommands.Add(new CLICMDS("printstring","prtstr", "prtstr", ""));
			SystemCommands.Add(new CLICMDS("printscreen","prtscrn", "screenshot", ""));
			SystemCommands.Add(new CLICMDS("string","str", "str", ""));
			SystemCommands.Add(new CLICMDS("ofe", "ofe", "ofe", ""));
			SystemCommands.Add(new CLICMDS("devinstall", "devinstall", "devinstall", ""));
			SystemCommands.Add(new CLICMDS("resetdomaindb", "resetdomaindb", "resetdomaindb", ""));
			SystemCommands.Add(new CLICMDS("regedit", "regedit", "regedit", ""));
			//SYSTEM PREFRENCES
			SystemCommands.Add(new CLICMDS("volume","vol", "volume", ""));
			SystemCommands.Add(new CLICMDS("sound", "snd", "sound", ""));
			SystemCommands.Add(new CLICMDS("background", "bg", "background", ""));
			SystemCommands.Add(new CLICMDS("aql", "aql", "aql", ""));
			SystemCommands.Add(new CLICMDS("theme", "theme", "theme", ""));
			//SystemCommands.Add(new CLICMDS("..", "back"));

			//TERMINAL PREFRENCES
			SystemCommands.Add(new CLICMDS("mode","mode", "mode", ""));
			SystemCommands.Add(new CLICMDS("setautodelete", "setautodelete", "setautodelete", ""));
			SystemCommands.Add(new CLICMDS("setfontsize", "setfontsize", "clifontsize", ""));
			SystemCommands.Add(new CLICMDS("settextpos", "settextpos", "clitextpos", ""));
			SystemCommands.Add(new CLICMDS("setcommandchar", "setcommandchar", "setcommandchar", ""));
			SystemCommands.Add(new CLICMDS("setspacechar", "setspacechar", "setspacechar", ""));

			//COLOR COMMANDS
			SystemCommands.Add(new CLICMDS("color","color", "color", ""));
			SystemCommands.Add(new CLICMDS("font", "font", "font", ""));
			SystemCommands.Add(new CLICMDS("button", "button", "button", ""));
			SystemCommands.Add(new CLICMDS("window", "window", "window", ""));
			SystemCommands.Add(new CLICMDS("red", "red", "red", ""));
			SystemCommands.Add(new CLICMDS("green", "green", "green", ""));
			SystemCommands.Add(new CLICMDS("blue", "blue", "blue", ""));
			//NETWORK COMMANDS
			SystemCommands.Add(new CLICMDS("-r", "-r", "-r", ""));
			SystemCommands.Add(new CLICMDS("dl", "dl", "dl", ""));
			SystemCommands.Add(new CLICMDS("ul", "ul", "ul", ""));
			SystemCommands.Add(new CLICMDS("connect", "connect", "connect", ""));
			//LOCAL COMMANDS
			SystemCommands.Add(new CLICMDS("-l","-l", "-l", ""));

			//UTILITY COMMANDS
			SystemCommands.Add(new CLICMDS("clear","clear", "clear", ""));
			SystemCommands.Add(new CLICMDS("remove","rm", "rm", ""));
			SystemCommands.Add(new CLICMDS("Change Directory","cd", "cd", ""));
			SystemCommands.Add(new CLICMDS("run","run", "run", ""));
			SystemCommands.Add(new CLICMDS("open", "open", "open", ""));
			SystemCommands.Add(new CLICMDS("printworkingdirectory", "pwd", "pwd", ""));
			SystemCommands.Add(new CLICMDS("list","ls", "ls", ""));
			SystemCommands.Add(new CLICMDS("quit","q", "close", ""));
			SystemCommands.Add(new CLICMDS("copy","copy", "copy", ""));
			SystemCommands.Add(new CLICMDS("paste", "paste", "paste", ""));
			SystemCommands.Add(new CLICMDS("copy1", "copy1", "copy1", ""));
			SystemCommands.Add(new CLICMDS("paste1", "paste1", "paste1", ""));
			SystemCommands.Add(new CLICMDS("paste2", "paste2", "paste2", ""));
			SystemCommands.Add(new CLICMDS("makedirectory", "mkdir", "mkdir", ""));
			SystemCommands.Add(new CLICMDS("delete", "del", "del", ""));
			SystemCommands.Add(new CLICMDS("makedocument", "mkdoc", "mkdoc", ""));
			SystemCommands.Add(new CLICMDS("time", "time", "time", ""));
			SystemCommands.Add(new CLICMDS("whoami", "whoami", "whoami", ""));
			SystemCommands.Add(new CLICMDS("systeminfo", "systeminfo", "systeminfo", ""));
			SystemCommands.Add(new CLICMDS("listprograms", "listprograms", "listprograms", ""));
			SystemCommands.Add(new CLICMDS("selectbankaccount", "selectbankaccount", "selectbankaccount", ""));
			SystemCommands.Add(new CLICMDS("restart", "restart", "restart", ""));
			SystemCommands.Add(new CLICMDS("exit", "exit", "exit", ""));
			SystemCommands.Add(new CLICMDS("syspan", "syspan", "syspan", ""));
			SystemCommands.Add(new CLICMDS("time", "time", "time", ""));
			SystemCommands.Add(new CLICMDS("date", "date", "date", ""));
			SystemCommands.Add(new CLICMDS("datetime", "datetime", "datetime", ""));
			//PROGRAM COMMANDS
			SystemCommands.Add(new CLICMDS("listemails", "listemails", "printemails", ""));
			SystemCommands.Add(new CLICMDS("viewemails", "viewemails", "viewselectedemail", ""));
			SystemCommands.Add(new CLICMDS("selectnotisnd", "selectnotisnd", "selectnotisnd", ""));

		//	if (GameControl.control.ShortCommands == true)
  //          {
		//		//SystemCommands.Add(new CLICMDS("dlv2", "dlv2"));
		//		//SystemCommands.Add(new CLICMDS("del", "del"));
		//	}
  //          else
  //          {
  //              //PREFRENCES
  //              SystemCommands.Add(new CLICMDS("volume", "volume"));
  //              SystemCommands.Add(new CLICMDS("sound", "sound"));
  //              SystemCommands.Add(new CLICMDS("background", "background"));
  //              SystemCommands.Add(new CLICMDS("addtoquicklaunch", "aql"));
  //              SystemCommands.Add(new CLICMDS("theme", "theme"));
  //              //TERMINAL
  //              SystemCommands.Add(new CLICMDS("mode", "mode"));
  //              SystemCommands.Add(new CLICMDS("setautodelete", "setautodelete"));
  //              SystemCommands.Add(new CLICMDS("setfontsize", "clifontsize"));
  //              SystemCommands.Add(new CLICMDS("settextpos", "clitextpos"));
  //              SystemCommands.Add(new CLICMDS("setcommandchar", "setcommandchar"));
  //              SystemCommands.Add(new CLICMDS("setspacechar", "setspacechar"));
  //              //COLOR COMMANDS
  //              SystemCommands.Add(new CLICMDS("color", "color"));
  //              SystemCommands.Add(new CLICMDS("font", "font"));
  //              SystemCommands.Add(new CLICMDS("button", "button"));
  //              SystemCommands.Add(new CLICMDS("window", "window"));
  //              SystemCommands.Add(new CLICMDS("red", "red"));
  //              SystemCommands.Add(new CLICMDS("blue", "blue"));
  //              SystemCommands.Add(new CLICMDS("green", "green"));
  //              //NETWORK COMMANDS
  //              SystemCommands.Add(new CLICMDS("remote", "-r"));
  //              SystemCommands.Add(new CLICMDS("download", "dl"));
  //              SystemCommands.Add(new CLICMDS("upload", "ul"));
  //              SystemCommands.Add(new CLICMDS("connect", "connect"));
  //              //LOCAL COMMANDS
  //              SystemCommands.Add(new CLICMDS("local", "-l"));
  //              //UTILITY COMMANDS
  //              SystemCommands.Add(new CLICMDS("clear", "clear"));
  //              SystemCommands.Add(new CLICMDS("remove", "rm"));
  //              SystemCommands.Add(new CLICMDS("changedirectory", "cd"));
  //              SystemCommands.Add(new CLICMDS("run", "run"));
  //              SystemCommands.Add(new CLICMDS("open", "open"));
  //              SystemCommands.Add(new CLICMDS("printworkingdirectory", "pwd"));
  //              SystemCommands.Add(new CLICMDS("list", "ls"));
  //              SystemCommands.Add(new CLICMDS("close", "close"));
  //              SystemCommands.Add(new CLICMDS("copy", "copy"));
  //              SystemCommands.Add(new CLICMDS("paste", "paste"));
  //              SystemCommands.Add(new CLICMDS("copy1", "copy1"));
  //              SystemCommands.Add(new CLICMDS("paste1", "paste1"));
  //              SystemCommands.Add(new CLICMDS("paste2", "paste2"));
  //              SystemCommands.Add(new CLICMDS("makedirectory", "mkdir"));
  //              SystemCommands.Add(new CLICMDS("delete", "del"));
  //              SystemCommands.Add(new CLICMDS("makedocument", "mkdoc"));
  //              SystemCommands.Add(new CLICMDS("time", "time"));
  //              SystemCommands.Add(new CLICMDS("whoami", "whoami"));
  //              SystemCommands.Add(new CLICMDS("systeminfo", "systeminfo"));
  //              SystemCommands.Add(new CLICMDS("listprograms", "listprograms"));
  //              SystemCommands.Add(new CLICMDS("selectbankaccount", "selectbankaccount"));
  //              SystemCommands.Add(new CLICMDS("restart", "restart"));
  //              SystemCommands.Add(new CLICMDS("exit", "exit"));
  //              SystemCommands.Add(new CLICMDS("syspan", "syspan"));
		//		SystemCommands.Add(new CLICMDS("time", "time"));
		//		SystemCommands.Add(new CLICMDS("date", "date"));
		//		SystemCommands.Add(new CLICMDS("datetime", "datetime"));
  //              //PROGRAM COMMANDS
  //              SystemCommands.Add(new CLICMDS("listemails", "printemails"));
  //              SystemCommands.Add(new CLICMDS("viewemails", "viewselectedemail"));
  //              SystemCommands.Add(new CLICMDS("selectnotisnd", "selectnotisnd"));
  //          }
		}

		if (GameControl.control.Commands.Count == 0)
		{
			for (int i = 0; i < SystemCommands.Count; i++) 
			{
				GameControl.control.Commands.Add (SystemCommands[i]);
			}
		}

		if (GameControl.control.Commands.Count < SystemCommands.Count) 
		{
			for (int i = 0; i < SystemCommands.Count; i++) 
			{
				if (GameControl.control.Commands[i].Func != SystemCommands [i].Func)
				{
					GameControl.control.Commands.Insert(i,SystemCommands[i]);
				}
			}
		}
	}

	public void SetFontColor()
	{
  //      var Fontcolor = new Color32();
  //      Fontcolor.r = (byte)GameControl.control.SelectedOS.Colour.Font.Red;
		//Fontcolor.g = (byte)GameControl.control.SelectedOS.Colour.Font.Green;
		//Fontcolor.b = (byte)GameControl.control.SelectedOS.Colour.Font.Blue;
		//Fontcolor.a = (byte)GameControl.control.SelectedOS.Colour.Font.Alpha;
		//com.colors[1] = Fontcolor;
	}

	public void SetButtonColor()
	{
  //      var ButtonColor = new Color32();
  //      ButtonColor.r = (byte)GameControl.control.SelectedOS.Colour.Button.Red;
		//ButtonColor.g = (byte)GameControl.control.SelectedOS.Colour.Button.Green;
		//ButtonColor.b = (byte)GameControl.control.SelectedOS.Colour.Button.Blue;
		//ButtonColor.a = (byte)GameControl.control.SelectedOS.Colour.Button.Alpha;
		//com.colors[2] = ButtonColor;
	}

	public void SetWindowColor()
	{
  //      var WindowColor = new Color32();
  //      WindowColor.r = (byte)GameControl.control.SelectedOS.Colour.Window.Red;
		//WindowColor.g = (byte)GameControl.control.SelectedOS.Colour.Window.Green;
		//WindowColor.b = (byte)GameControl.control.SelectedOS.Colour.Window.Blue;
		//WindowColor.a = (byte)GameControl.control.SelectedOS.Colour.Window.Alpha;
		//com.colors[3] = WindowColor;
	}

	void ThemeChange()
	{
		if (inputArray [1] != "")
		{
			int theme = 0;
			int.TryParse (inputArray [1],out theme);
			GameControl.control.GUIID = theme;
		}
	}

	void RunProgram()
	{
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		{
            if (inputArray[1] == Index.ToString())
            {
                if (twd == GameControl.control.ProgramFiles[Index].Location)
                {
                    appman.ProgramName = GameControl.control.ProgramFiles[Index].Name;
                    appman.SelectedApp = GameControl.control.ProgramFiles[Index].Target;
                }
            }
            if (GameControl.control.ProgramFiles[Index].Name == inputArray[1]) 
			{
				if (twd == GameControl.control.ProgramFiles [Index].Location) 
				{
                    appman.ProgramName = GameControl.control.ProgramFiles[Index].Name;
                    appman.SelectedApp = GameControl.control.ProgramFiles[Index].Target;
				}
			}
			if(inputArray[1] == GameControl.control.ProgramFiles[Index].Location + "/" + GameControl.control.ProgramFiles[Index].Name)
			{
				appman.ProgramName = GameControl.control.ProgramFiles[Index].Name;
				appman.SelectedApp = GameControl.control.ProgramFiles[Index].Target;
			}
		}
	}

	//void OpenFile()
	//{
	//	for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
	//	{
	//		if (GameControl.control.ProgramFiles[i].Name == ParseArray[1]) 
	//		{
	//			if (twd == GameControl.control.ProgramFiles [i].Location) 
	//			{
	//				note.SelectedDocument = i;
	//				if (note.show == true && note.enabled == true) 
	//				{
	//					note.CurrentWorkingTitle = GameControl.control.ProgramFiles[i].Name;
	//					note.TypedTitle = GameControl.control.ProgramFiles[i].Name;
	//					note.TypedText = GameControl.control.ProgramFiles[i].Content;
	//					note.SaveLocation = GameControl.control.ProgramFiles [i].Location;
	//					note.ShowFileContent = true;
	//				}
	//				if (note.show == false || note.enabled == false) 
	//				{
	//					appman.SelectedApp = "Notepad";
	//					note.CurrentWorkingTitle = GameControl.control.ProgramFiles[i].Name;
	//					note.TypedTitle = GameControl.control.ProgramFiles[i].Name;
	//					note.TypedText = GameControl.control.ProgramFiles[i].Content;
	//					note.SaveLocation = GameControl.control.ProgramFiles [i].Location;
	//					note.ShowFileContent = true;
	//				}	
	//			}
	//		}
	//	}
	//}

	void MakeFile()
	{
		if (inputArray [1] != "") 
		{
			for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
			{
				if (GameControl.control.ProgramFiles [i].Name == FolderName && GameControl.control.ProgramFiles [i].Location == twd)
				{
					ep.enabled = true;
					ep.show = true;
					ep.playsound = true;
					ep.ErrorTitle = "File Utility Error - 85";
					ep.ErrorMsg = "A file with that name already exists in that directory.";
					FileIndex = -1;
				}
				else 
				{
					FileIndex = i;
				}
			}
			if (FileIndex != -1) 
			{
				if (twd.Length > 3)
				{
					FolderName = inputArray [1];
					GameControl.control.ProgramFiles.Add(new ProgramSystem(FolderName, "", "", "", "", "", twd, "" + twd + "/" + FolderName, "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				} 
				else 
				{
					FolderName = inputArray [1];
					GameControl.control.ProgramFiles.Add(new ProgramSystem(FolderName, "", "", "", "", "", twd, "" + twd + "" + FolderName, "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				}
				FolderName = "";
				FileIndex = -1;
			}	
		}
	}

	void CopyFile()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
		{
			if (GameControl.control.ProgramFiles[i].Name == ParseArray[1]) 
			{
				TempCopy = GameControl.control.ProgramFiles[i];
			}
		}
	}

	void CopyFile1()
	{
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		{
			if (GameControl.control.ProgramFiles[Index].Name == inputArray[1])
			{
				if (GameControl.control.ProgramFiles[Index].Location == twd)
				{
					FileIndex = Index;
					HeldFile = GameControl.control.ProgramFiles[FileIndex];
					//FileName = GameControl.control.ProgramFiles[Index].Name;
					//FileSize = GameControl.control.ProgramFiles[Index].Used;
					//FileContent = GameControl.control.ProgramFiles[Index].Content;
					//FileTarget = GameControl.control.ProgramFiles[Index].Target;
					//FileVersion = GameControl.control.ProgramFiles[Index].Version;
					//FileType = GameControl.control.ProgramFiles[Index].Type.ToString();
					PastCommands.Add (FileName + "Has been copied From : " + twd);
				}
			}
		}
	}

	void PasteFile()
	{
		TempCopy.Location = twd;
		TempCopy.Target = TempCopy.Location + "/" + TempCopy.Name;
		GameControl.control.ProgramFiles.Add(TempCopy);
	}

	void PasteFile1()
	{
		if (fu.ProgramHandle.Count <= 0)
		{
			if (FileIndex != -1)
			{
				if (inputArray.Length > 1)
				{
					if (inputArray[1] != "")
					{
						for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++)
						{
							if (GameControl.control.ProgramFiles[Index].Extension == ProgramSystem.FileExtension.Fdl || GameControl.control.ProgramFiles[Index].Extension == ProgramSystem.FileExtension.Dir)
							{
								if (GameControl.control.ProgramFiles[Index].Target == inputArray[1])
								{
									string Location = inputArray[1];
									//fu.ProgramHandle.Add(new FileUtilitySystem("Paste", FileName, Location,"", "", FileTarget, FileContent, FileType, false, true, true, false, FileVersion, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
									//fu.AddWindow();
									PastCommands.Add(FileName + " is being pasted to: " + Location);
								}
							}
						}
					}
					else
					{
						string FileLocation = twd;
						//fu.ProgramHandle.Add(new FileUtilitySystem("Paste", FileName, FileLocation,"", "", FileTarget, FileContent, FileType, false, true, true, false, FileVersion, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
						//fu.AddWindow();
						PastCommands.Add(FileName + " is being pasted to: " + FileLocation);
					}
				}
				else
				{
					string FileLocation = twd;
					//fu.ProgramHandle.Add(new FileUtilitySystem("Paste", FileName, FileLocation,"", "", FileTarget, FileContent, FileType, false, true, true, false, FileVersion, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
					//fu.AddWindow();
					PastCommands.Add(FileName + " is being pasted to: " + FileLocation);
				}
			}
		}
	}

	void ProgramQuickLaunch()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
		{
			if (GameControl.control.ProgramFiles[i].Name == ParseArray[1]) 
			{
				//GameControl.control.QuickProgramList.Add (GameControl.control.ProgramFiles[i]);
			}
		}
	}

    void CloseProgram()
    {
        for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++)
        {
            if (inputArray[1] == Index.ToString())
            {
                if (twd == GameControl.control.ProgramFiles[Index].Location)
                {
                    appman.ProgramName = GameControl.control.ProgramFiles[Index].Name;
                    appman.SelectedApp = GameControl.control.ProgramFiles[Index].Target;
                }
            }
            if (GameControl.control.ProgramFiles[Index].Name == inputArray[1])
            {
                if (twd == GameControl.control.ProgramFiles[Index].Location)
                {
                    appman.ProgramName = GameControl.control.ProgramFiles[Index].Name;
                    appman.SelectedApp = GameControl.control.ProgramFiles[Index].Target;
                }
            }
        }
    }

	void RemoteFileDelete()
	{
		string LocationName = "";
		if (ib.CurrentLocation != "")
		{

			for (int Index = 0; Index < GameControl.control.CompanyServerData.Count; Index++)
			{
				if (GameControl.control.CompanyServerData[Index].Name == ib.CurrentLocation)
				{
					LocationName = GameControl.control.CompanyServerData[Index].Name;
					if (GameControl.control.CompanyServerData[Index].Files.Count > 0)
					{
						for (int i = 0; i < GameControl.control.CompanyServerData[Index].Files.Count; i++)
						{
							if (GameControl.control.CompanyServerData[Index].Files[i].Name == inputArray[2])
							{
								if (GameControl.control.CompanyServerData[Index].Files[i].Location == ib.AddressBar)
								{
									FileIndex = i;
									FileName = GameControl.control.CompanyServerData[Index].Files[i].Name;
									FileSize = GameControl.control.CompanyServerData[Index].Files[i].Used;
									FileLocation = GameControl.control.CompanyServerData[Index].Files[i].Location;
								}
							}
						}
					}
				}
			}

			//for (int Index = 0; Index < GameControl.control.WebsiteFiles.Count; Index++)
			//{
			//	if (GameControl.control.WebsiteFiles[Index].Name == inputArray[2])
			//	{
			//		if (GameControl.control.WebsiteFiles[Index].Location == ib.CurrentLocation)
			//		{
			//			FileIndex = Index;
			//			FileName = GameControl.control.WebsiteFiles[FileIndex].Name;
			//			FileSize = GameControl.control.WebsiteFiles[FileIndex].Used;
			//		}
			//	}
			//}

			if (fu.ProgramHandle.Count <= 0)
			{
				if (FileIndex != -1)
				{
					//fu.ProgramHandle.Add(new FileUtilitySystem("Delete", FileName, FileLocation, LocationName, "", "", "", "", false, true, true, false, 0, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.RemoteDelete));
					//fu.AddWindow();
				}
			}
		}
	}

	void DownloadFile()
	{
		string LocationName = "";
		if (ib.CurrentLocation != "")
		{

			for (int Index = 0; Index < GameControl.control.CompanyServerData.Count; Index++)
			{
				if (GameControl.control.CompanyServerData[Index].Name == ib.CurrentLocation)
				{
					LocationName = GameControl.control.CompanyServerData[Index].Name;
					if (GameControl.control.CompanyServerData[Index].Files.Count > 0)
					{
						for (int i = 0; i < GameControl.control.CompanyServerData[Index].Files.Count; i++)
						{
							if (GameControl.control.CompanyServerData[Index].Files[i].Name == inputArray[1])
							{
								if (GameControl.control.CompanyServerData[Index].Files[i].Location == ib.AddressBar)
								{
									FileIndex = i;
									FileName = GameControl.control.CompanyServerData[Index].Files[i].Name;
									FileSize = GameControl.control.CompanyServerData[Index].Files[i].Used;
									FileLocation = GameControl.control.CompanyServerData[Index].Files[i].Location;
								}
							}
						}
					}
				}
			}

			if (fu.ProgramHandle.Count <= 0)
			{
				if (FileIndex != -1)
				{
					//fu.ProgramHandle.Add(new FileUtilitySystem("Download", FileName, Customize.cust.DownloadPath, "", FileLocation, "", "", "", false, true, true, false, 0, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Download));
					//fu.AddWindow();
				}
			}
		}
	}

	void CLIDeleteFile()
	{
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++)
		{
			if (GameControl.control.ProgramFiles[Index].Name == inputArray[1])
			{
				if (GameControl.control.ProgramFiles[Index].Location == twd)
				{
					FileIndex = Index;
					FileName = GameControl.control.ProgramFiles[FileIndex].Name;
					FileSize = GameControl.control.ProgramFiles[FileIndex].Used;
				}
			}
		}

		if (fu.ProgramHandle.Count <= 0)
		{
			if (FileIndex != -1)
			{
				//fu.ProgramHandle.Add(new FileUtilitySystem("Delete", FileName, twd,"", "", "", "", "", false, true, true, false, 0, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.LocalDelete));
				//fu.AddWindow();
			}
		}
	}

	void NewDirectory()
	{
		if (inputArray [1] != "") 
		{
			for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
			{
				if (GameControl.control.ProgramFiles [i].Name == FolderName && GameControl.control.ProgramFiles [i].Location == twd)
				{
					ep.enabled = true;
					ep.show = true;
					ep.playsound = true;
					ep.ErrorTitle = "File Utility Error - 85";
					ep.ErrorMsg = "A file with that name already exists in that directory.";
					FileIndex = -1;
				}
				else 
				{
					FileIndex = i;
				}
			}
			if (FileIndex != -1) 
			{
				if (twd.Length > 3)
				{
					FolderName = inputArray[1];
					GameControl.control.ProgramFiles.Add(new ProgramSystem(FolderName, "", "", "", "", "", twd, "" + twd + "/" + FolderName, "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				}
				else
				{
					FolderName = inputArray[1];
					GameControl.control.ProgramFiles.Add(new ProgramSystem(FolderName, "", "", "", "", "", twd, "" + twd + "" + FolderName, "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				}
				FolderName = "";
				FileIndex = -1;
			}	
		}
	}

	void LocalFileDelete()
	{
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		{
			if (GameControl.control.ProgramFiles[Index].Name == inputArray [2])
			{
				if (GameControl.control.ProgramFiles[Index].Location == inputArray[3])
				{
					FileIndex = Index;
					FileLocation = inputArray[3];
					FileName = GameControl.control.ProgramFiles[FileIndex].Name;
					FileSize = GameControl.control.ProgramFiles[FileIndex].Used;
					HeldFile = GameControl.control.ProgramFiles[FileIndex];
				}
			}
		}

		if (fu.ProgramHandle.Count <= 0) 
		{
			if (FileIndex != -1) 
			{
                if (GameControl.control.ProgramFiles[FileIndex].Extension == ProgramSystem.FileExtension.Fdl)
				{
					fu.ForceDone = true;
					fu.FileIndex = GameControl.control.ProgramFiles.Count-1;
					//fu.ProgramHandle.Add (new FileUtilitySystem ("Delete", FileName, FileLocation,"","", "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.LocalFolderDelete));
				}
				else
				{
					//fu.ProgramHandle.Add (new FileUtilitySystem ("Delete", FileName, FileLocation,"","", "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.LocalDelete));
				}
				//fu.AddWindow ();
			}
		}
	}

	void UploadFile()
	{
		if (ib.CurrentLocation != "")
		{
			for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++)
			{
				if (GameControl.control.ProgramFiles[Index].Name == inputArray[1])
				{
					FileIndex = Index;
					FileName = GameControl.control.ProgramFiles[FileIndex].Name;
					FileSize = GameControl.control.ProgramFiles[FileIndex].Used;
				}
			}
			if (fu.ProgramHandle.Count <= 0)
			{
				if (FileIndex != -1)
				{
					//fu.ProgramHandle.Add(new FileUtilitySystem("Upload", FileName, FileLocation,"", "", "", "", "", false, true, true, false, 0, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Upload));
					//fu.AddWindow();
				}
			}
		}
	}

	void PrintEmails()
	{
		for (int i = 0; i < GameControl.control.EmailData.Count; i++) 
		{
			PastCommands.Add ("#" + i + "\t" + GameControl.control.EmailData [i].Subject);
		}
	}

	void ViewEmailContent()
	{
		int selectedemail = 0;
		int.TryParse (inputArray [1],out selectedemail);
		PastCommands.Add (GameControl.control.EmailData [selectedemail].Subject);
		PastCommands.Add ("");
		PastCommands.Add (GameControl.control.EmailData [selectedemail].Sender);
		PastCommands.Add ("");
		PastCommands.Add (GameControl.control.EmailData [selectedemail].Content);
	}

	void PrintCommands()
	{
		for (int i = 0; i < GameControl.control.Commands.Count; i++) 
		{
			PastCommands.Add (GameControl.control.Commands[i].Name);
		}
		SetScrollPos = true;
	}

	void PrintPrograms()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
		{
			if (GameControl.control.ProgramFiles [i].Extension == ProgramSystem.FileExtension.Exe)
			{
				PastCommands.Add (GameControl.control.ProgramFiles [i].Name);
			}
		}
	}

	void ChangeLocalDirectory()
	{
		if (writeInfo == true)
		{
			PastCommands.Add("CLI Working Directory Changed From : " + DirectoryHistory[DirectoryHistory.Count - 1]);
			PastCommands.Add("CLI Working Directory Changed To : " + twd);
		}
		DirectoryHistory.Add(twd);
		twd = GameControl.control.ProgramFiles[localdirectoryid].Target;
		//string concat = String.Join("", DirectoryHistory.ToArray());
	}

	void ChangeLocalDirectoryUp()
	{
		if(DirectoryHistory.Count > 0)
		{
			int cdupid = DirectoryHistory.Count - 1;
			twd = DirectoryHistory[cdupid];
			DirectoryHistory.RemoveAt(cdupid);
		}
	}

	void CheckLocalDirectoryChange()
	{
		for (localdirectoryid = 0; localdirectoryid < GameControl.control.ProgramFiles.Count; localdirectoryid++)
		{
			if (inputArray[1] == GameControl.control.ProgramFiles[localdirectoryid].Name)
			{
				if (twd == GameControl.control.ProgramFiles[localdirectoryid].Location)
				{
					if (twd != GameControl.control.ProgramFiles[localdirectoryid].Target)
					{
						ChangeLocalDirectory();
					}
				}
			}
			else if (inputArray[1] == GameControl.control.ProgramFiles[localdirectoryid].Location)
			{
				if (twd != GameControl.control.ProgramFiles[localdirectoryid].Location)
				{
					ChangeLocalDirectory();
				}
			}
			else if (inputArray[1] == GameControl.control.ProgramFiles[localdirectoryid].Target)
			{
				if (twd != GameControl.control.ProgramFiles[localdirectoryid].Target)
				{
					ChangeLocalDirectory();
				}
			}
			else if (inputArray[1] == localdirectoryid.ToString())
			{
				if (twd == GameControl.control.ProgramFiles[localdirectoryid].Location)
				{
					if (twd != GameControl.control.ProgramFiles[localdirectoryid].Target)
					{
						ChangeLocalDirectory();
					}
				}
			}
		}
	}

	void CreateRealExe(string Name, string GameLocation, string RealLocation)
	{
		if (Name != "")
		{
			for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
			{
				if (GameControl.control.ProgramFiles[i].Name == Name && GameControl.control.ProgramFiles[i].Location == GameLocation)
				{
					ep.enabled = true;
					ep.show = true;
					ep.playsound = true;
					ep.ErrorTitle = "File Utility Error - 85";
					ep.ErrorMsg = "A file with that name already exists in that directory.";
					FileIndex = -1;
				}
				else
				{
					FileIndex = i;
				}
			}
			if (FileIndex != -1)
			{
				if (GameLocation.Length > 3)
				{
					GameControl.control.ProgramFiles.Add(new ProgramSystem(Name, "", "", "", "", "", GameLocation, RealLocation, "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				}
				FileIndex = -1;
			}
		}
	}

	void RegEdit()
    {
		switch (ParseArray[3])
		{
			case "string":
				Registry.SetStringData("Player", ParseArray[1], ParseArray[2], ParseArray[4]);
				break;
		}
	}

	void InstallOS()
	{

		//OSColour.Button.Red = 255;
		//OSColour.Button.Green = 255;
		//OSColour.Button.Blue = 255;
		//OSColour.Button.Alpha = 255;

		//OSColour.Window.Red = 255;
		//OSColour.Window.Green = 255;
		//OSColour.Window.Blue = 255;
		//OSColour.Window.Alpha = 255;

		//OSColour.Font.Alpha = 255;

		GameControl.control.ProgramFiles.Add(new ProgramSystem("" + "TreeOS", "", "", "", "", "", "C:/System", "", "", "", ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
		//GameControl.control.ProgramFiles.Add(new ProgramSystem("" + "FluidicIceOS", "", "", "", "", "", "C:/System", "", "", "", ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
		GameControl.control.ProgramFiles.Add(new ProgramSystem("" + "AppatureOS", "", "", "", "", "", "C:/System", "", "", "", ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
		GameControl.control.ProgramFiles.Add(new ProgramSystem("" + "EthelOS", "", "", "", "", "", "C:/System", "", "", "", ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
		GameControl.control.OSName.Add(new OperatingSystems("TreeOS", OperatingSystems.OSName.TreeOS));
		//GameControl.control.OSName.Add(new OperatingSystems("FluidicIceOS", OperatingSystems.OSName.FluidicIceOS));
		GameControl.control.OSName.Add(new OperatingSystems("AppatureOS", OperatingSystems.OSName.AppatureOS));
		GameControl.control.OSName.Add(new OperatingSystems("EthelOS", OperatingSystems.OSName.EthelOS));
	}

	void NameChange()
	{
		for (int i = 0; i < GameControl.control.Commands.Count; i++) 
		{
			if (!CommandNames.Contains (GameControl.control.Commands [i].Name))
			{
				CommandNames.Add (GameControl.control.Commands [i].Name);
			}
		}

		for (int WordIndex = 0; WordIndex < CommandNames.Count; WordIndex++) 
		{
			if (ParseArray [1] == CommandNames [WordIndex])
			{
				NameIndex = WordIndex;
			}
		}

		if (ParseArray [2] != "" || ParseArray [2] != " ") 
		{
			if (LastItem != GameControl.control.Commands[NameIndex].Func) 
			{
				GameControl.control.Commands[NameIndex].Name = ParseArray [2];
				PastCommands.Add (GameControl.control.Commands [NameIndex].Name + "Has been changed to" + ParseArray [2]);
			}
		}

		CommandNames.RemoveRange (0, CommandNames.Count);
	}

	void RunFakeApps(string ProgramName)
	{
		appman.ProgramName = ProgramName;
		appman.FakeApp = true;
		appman.SelectedApp = "fakeapp";
		StringCommandTask = "na";
	}

	public void OpenFileExplorer(string FileType)
	{
		//if (Application.isEditor == true)
		//{
		//	StringCommand = EditorUtility.OpenFilePanel("Select File Path", "", FileType);
		//	WWW www = new WWW("file:///" + StringCommand);
		//}
	}

	public void CommandCheck()
	{
		Functions.RemoveRange (0, Functions.Count);
		CommandLine = "";
		ParseArray = Regex.Split(Parse,Customize.cust.TerminalCommandCharacterSplit,RegexOptions.IgnoreCase);
		ParseArrayLength = ParseArray.Length;
		if (GameControl.control.Commands.Count > 0)
		{
			for (int i = 0; i < GameControl.control.Commands.Count; i++) 
			{
				//ParseArray = Regex.Split(Parse,GameControl.control.Commands[i].Name,RegexOptions.IgnoreCase);
				for (int j = 0; j < ParseArray.Length; j++) 
				{
					CurrentParse = j;
					if (GameControl.control.ShortCommands == true)
					{
						if (ParseArray[j] == GameControl.control.Commands[i].ShortHand)
						{
							if (!Functions.Contains(GameControl.control.Commands[i].Func))
							{
								Functions.Add(GameControl.control.Commands[i].Func);
							}

							LastItem = ParseArray[ParseArray.Length - 1];
						}
					}
					else
					{
						if (ParseArray[j] == GameControl.control.Commands[i].Name)
						{
							if (!Functions.Contains(GameControl.control.Commands[i].Func))
							{
								Functions.Add(GameControl.control.Commands[i].Func);
							}

							LastItem = ParseArray[ParseArray.Length - 1];
						}
					}
				}
			}
		}

		if (ParseArray.Length > Functions.Count)
		{
			if (!Functions.Contains (LastItem)) 
			{
				Functions.Add(LastItem);
			}
		}

		if (Functions.Count == ParseArray.Length) 
		{
			for (int k = 0; k < Functions.Count; k++)
			{
				if (k == 0) 
				{
					CommandLine += Functions [k];
				} 
				else 
				{
					CommandLine += "▓" + Functions [k];
				}
			}
		}
		
		if (CommandLine.Contains(Customize.cust.TerminalSpaceCharacterSplit))
		{
			CommandLine = CommandLine.Replace(Customize.cust.TerminalSpaceCharacterSplit, " ");
		}

		if (CommandLine != "")
		{
			CheckInput();
		}

		SpecialChecks();
	}

	public void SpecialChecks()
	{
		switch(ParseArray[0])
		{
			case "str":
			StartProcessingString();
				break;
		}
	}

	public void CheckInput()
	{
		inputArray = CommandLine.Split('▓');

		switch (inputArray[0])
		{
		case "createrealexe":
				CreateRealExe(ParseArray[1], ParseArray[2], ParseArray[3]);
				break;

			case "resetdomaindb":
				GameControl.control.CompanyServerData.RemoveRange(0, GameControl.control.CompanyServerData.Count);
				ShutDownWindow.Restart();
				break;

			case "installdefaltos":
				InstallOS();
				break;

		case "ofe":
				OpenFileExplorer(StringCommand);
				ResetStringCommand();
				break;

			case "aiprogramrun":
				appman.ProgramRequest("desktop", "Desktop", ParseArray[1]);
				break;

			case "regedit":
				RegEdit();
				PastCommands.Add(StringCommand);
				//ResetStringCommand();
				break;

			case "screenshot":
				PastCommands.Add("Screenshot Taken");
				screenshot.TakeShot();
			break;

			case "prtstr":
				PastCommands.Add(StringCommand);
				break;

			//case "devinstall":
			//	GameControl.control.ProgramFiles.Add(new ProgramSystem())
			//	break;

			case "aql":
            if (inputArray[1] == "")
            {
                ProgramQuickLaunch();
            }
			break;

            case "error":
                if (ParseArray[1] == "1")
                {
                    PastCommands.Add("Displayed an error");
                    ep.SoundSelect = 0;
                    ep.playsound = true;
                    ep.ErrorTitle = "Genric Error - 343";
                    ep.ErrorMsg = "This is a generic user error from Command Prompt";
                    appman.SelectedApp = "Error Prompt";
                }
                break;

		case"syscrash":
			noti.ForcedMusicSetting = true;
			noti.ForcedMusicOption = false;
			noti.NewNotification("Critical Error","OS Kernal Panic","This was done by the user through CLI.");
			SCM.StopCodeWord = "MANUAL_LAUNCH_CRASH";
			SCM.StopCodeNumber = "0xD34DD13D";
			SCM.CodeDetail = "K3RN31-94N1C-C11-U23R-D3F";
			SCM.ExtraDetail = "14M-7H3-D327R0Y3R-0F-7H12-02";
			SCM.enabled = true;
			Desktops.SetActive (false);
			break;

		case"mkfakeapp":
				RunFakeApps(StringCommand);
				break;

			case "url":
            if (inputArray[1] != "" && inputArray[1] != "help")
            {
                Application.OpenURL(inputArray[1]);
            }
            else
            {
                PastCommands.Add("error: address cannot be found.");
            }
            break;

			case "openchatroom":
				if (inputArray[1] == "Info")
				{
					PastCommands.Add("Available Commands:");
					PastCommands.Add("Help");
					PastCommands.Add("Refresh");
					PastCommands.Add("CMD");
					PastCommands.Add("Invite");
					PastCommands.Add("Define");
					PastCommands.Add("Disconnect");
				}
				if (inputArray[1] == "Refresh")
				{
					PastCommands.Add("Main Chat: Levi");
					PastCommands.Add("Lobby: ");
					PastCommands.Add("ERROR");
				}
				if (inputArray[1] == "Help")
				{
					PastCommands.Add("There is none to be found here.");
				}
				break;


		case "noti":
            if (inputArray[1] != "")
            {
				noti.NewNotification("CLI Noti","Test",inputArray[1]);
            }
            else
            {
				noti.NewNotification("CLI Noti","Test","Test");
            }
            break;

		case "skipmission":
			curcon.SkipMission();
			//for(int i = 0; i < GameControl.control.EmailData.Count;i++)
			//	{
			//		if(GameControl.control.EmailData[i].Type == EmailSystem.EmailType.Contract)
			//		{
			//			GameControl.control.EmailData.RemoveAt(i);
			//		}
			//	}
			//GameControl.control.Contracts.RemoveAt(0);
			PastCommands.Add("skipped mission");
			break;

			case "writeinfo":
			if (inputArray [1] == "true") 
			{
				writeInfo = true;
				PastCommands.Add ("Now writing extra info");
			} 
			else 
			{
				if (inputArray [1] != "false")
				{
					PastCommands.Add ("ERROR: Aurgument must be a true or false ");
				} 
				else 
				{
					writeInfo = false;
				}
			}
			break;

		case "cd":
			if (inputArray [1] != "") 
			{
				if (inputArray[1] == "back" || inputArray[1] == "..")
				{
					ChangeLocalDirectoryUp();
				}
				else
				{
					CheckLocalDirectoryChange();
				}
			}
			if (inputArray [1] == "")
			{
				PastCommands.Add ("Invalid Directory");
			}
			break;

		case "pwd":
				if (inputArray.Length > 1)
				{
					if (inputArray [1] == "help") 
					{
						PastCommands.Add("printworkingdirectory");
						PastCommands.Add("shows what file you are currently in");
					}
				}
			PastCommands.Add (twd);
			break;

		case "version":
			for (int i = 0; i < GameControl.control.GameVersion.Count; i++)
			{
				PastCommands.Add(GameControl.control.GameVersion[i]);
			}
			break;

		case "listcommands":
			PrintCommands();
			break;

		case "-help":
			PrintCommands();
			break;

			case "viewrep":
			for(int i = 0; i < GameControl.control.Rep.Count; i++)
			{
				PastCommands.Add ("Rep Count: " + GameControl.control.Rep.Count);
				PastCommands.Add ("Rep Name: " + GameControl.control.Rep[i].Name);
				PastCommands.Add ("Rep Level: " + GameControl.control.Rep[i].RepLevel);
			}
			break;

		case"syspan":
				appman.SelectedApp = "System Panel";
			break;

		case"del":
				CLIDeleteFile();
			break;

			//case "copy":
			//	CopyFile();
			//	break;

			//case "paste":
			//	PasteFile();
			//	break;

		case "copy":
			CopyFile1();
			break;

		case "paste":
			PasteFile1();
			break;

			//case "paste2":
			//	PasteFile2();
			//	break;
			//
			//		case "move":
			//			MoveFile();
			//			break;

		case "mkdir":
			NewDirectory();
			break;

		case"mkdoc":
			MakeFile();
			break;

		case"ls":
			for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
			{
				if (GameControl.control.ProgramFiles [i].Location == twd) 
				{
					PastCommands.Add (i + "# " + GameControl.control.ProgramFiles [i].Name);
				}
			}
			break;

		case "accountinfo":
			//PastCommands.Add ("Serial Key: " + GameControl.control.SerialKey);
			PastCommands.Add ("Current User Name: " + GameControl.control.ProfileName);
			break;

		case "listprograms":
			PrintPrograms();
			break;

		case "run":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("run");
				PastCommands.Add("lets you run a program");
				PastCommands.Add("in the directory your currently in");
				PastCommands.Add("example run Calculator");
			}
			else
			{
				RunProgram();
			}
		break;

		case "close":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("run");
				PastCommands.Add("lets you run a program");
				PastCommands.Add("in the directory your currently in");
				PastCommands.Add("example run Calculator");
			}
			else
			{
				RunProgram();
			}
			break;

			case "enablemusicplayer":
			mp.enabled = true;
			PastCommands.Add("Music Player has been enabled");
			PastCommands.Add("Warnning may cause longer loading times");
			PastCommands.Add("Or more stuttering while loading songs");
			PastCommands.Add("Only use .wav files");
			break;

			case "settimemod":
			if (inputArray[1] != "")
			{
				if (inputArray[1] == "current")
				{
					PastCommands.Add("Current Time Multi: " + GameControl.control.TimeMulti);
				}
				else if (inputArray[1] == "reset")
				{
					GameControl.control.TimeMulti = 1;
					PastCommands.Add("Time Multi Reset: " + GameControl.control.TimeMulti);
				}
				else
				{
					int select = 0;
					int.TryParse(inputArray[1], out select);
					GameControl.control.TimeMulti = select;
					PastCommands.Add("Time Multi: " + select);
				}
			}
			break;

            case "open":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("open");
				PastCommands.Add("lets you open a file");
				PastCommands.Add("in the directory your currently in");
				PastCommands.Add("example open Test");
			}
			else
			{
				//OpenFile();
			}
			break;

		case "printemails":
			PrintEmails();
			break;

		case "viewselectedemail":
			ViewEmailContent();
			break;



			//LOCAL COMMANDS
		case"-l":
			switch (inputArray [1])
			{
			case"ls":
				for (int i = 0; i < com.PageFile.Count; i++) 
				{
					PastCommands.Add (com.PageFile[i].Name);
				}
				break;

			case"cd":
				if (inputArray [2] != "") 
				{
					com.ComAddress = inputArray [2];
				}
				break;

			case"copy1":
				if (inputArray [2] != "") 
				{

				}
				break;

			case"paste1":
				if (inputArray [2] != "") 
				{
					if (fu.ProgramHandle.Count <= 0) 
					{
						if (FileIndex != -1) 
						{
							FileName = inputArray[2];
							string FileLocation = inputArray[3];
							//fu.ProgramHandle.Add (new FileUtilitySystem ("Paste", FileName, FileLocation,"","", "","","", false, true, true, false, 0, 0,0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
							//fu.AddWindow ();
						}
					} 
				}
				break;

			case"mkdir":
				if (inputArray [2] != "")
				{
					if (inputArray [2].Contains ("▓")) 
					{
						ep.enabled = true;
						ep.show = true;
						ep.playsound = true;
						ep.ErrorTitle = "File Utility Error - 98";
						ep.ErrorMsg = "Cannot make a new folder with a space to fix this replace all spaces with_underscores";
					} 
					else 
					{
						for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
						{
							if (GameControl.control.ProgramFiles [i].Name == inputArray [2]) 
							{
								if (GameControl.control.ProgramFiles [i].Location == inputArray [3]) 
								{
									ep.enabled = true;
									ep.show = true;
									ep.playsound = true;
									ep.ErrorTitle = "File Utility Error - 85";
									ep.ErrorMsg = "A file with that name already exists in that directory.";
									FileIndex = -1;
								} 
								else 
								{
									FileIndex = i;
								}
							}
						}
					}
					if (FileIndex != -1)
					{
						GameControl.control.ProgramFiles.Add(new ProgramSystem(inputArray[2], "", "", "", "", "", inputArray[3], "" + inputArray[3] + "/" + inputArray[2], "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
					}
				}
				break;

			case"pwd":
				PastCommands.Add (com.ComAddress);
				break;

			case"del":
                LocalFileDelete();
				break;

			case"rm":
				if (inputArray [2] != "") 
				{
					for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
					{
						if (GameControl.control.ProgramFiles[i].Name.Contains(inputArray[2])) 
						{
							FileName = GameControl.control.ProgramFiles[i].Name;
							FileSize = GameControl.control.ProgramFiles[i].Used;
							FileLocation = GameControl.control.ProgramFiles[i].Location;
							if (inputArray [3] != "") 
							{
								if (GameControl.control.ProgramFiles [i].Location.Contains (inputArray [3])) 
								{
									FileIndex = i;
								}
							} 
							else
							{
								PastCommands.Add ("Aurgument 3 is null");
								FileName = "";
								FileSize = 0;
								FileLocation = "";
								FileIndex = -1;
							}
						}
					}
					if (fu.ProgramHandle.Count <= 0) 
					{
						if (FileIndex != -1) 
						{
							//fu.ProgramHandle.Add (new FileUtilitySystem ("Delete", FileName, FileLocation,"","", "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.LocalDelete));
							//fu.AddWindow ();
						}
					} 
				}
				break;
			}
			break;

			//REMOTE COMMANDS
		case"-r":
			switch (inputArray [1])
			{
			case"ls":
				ib.Request = true;
				break;
			case"cd":
				if (inputArray [2] != "") 
				{
					if (storedConnection != "")
					{
						ib.AddressBar = storedConnection + "/" + inputArray [2];
					}
					if (inputArray [2].Contains (storedConnection + "/"))
					{
						ib.AddressBar = inputArray[2];
					}
					if (inputArray [2] == storedConnection)
					{
						ib.AddressBar = storedConnection;
					}
					if(storedConnection == "")
					{
						PastCommands.Add("Remote connection cannot be found");
					}
				}
				break;

			case"pwd":
				PastCommands.Add (ib.AddressBar);
				break;

//			case"login":
//				Username = inputArray [2];
//				Password = inputArray [3];
//				break;

			case"rm":
				RemoteFileDelete ();
				break;
			}
			break;

		case "dl":
			DownloadFile();
			break;

		case "ul":
			UploadFile();
			break;

		case "theme":
			ThemeChange();
			break;

		case "clear":
			PastCommands.RemoveRange(0,PastCommands.Count);
			break;

		case "selectbankaccount":
			if (inputArray [1] != "")
			{
				int select = 0;
				int.TryParse (inputArray [1],out select);
				//GameControl.control.SelectedBank = select;
				PastCommands.Add ("Selected account " + select);
			}
			break;

		case "setbankbalance":
			if (inputArray [1] != "")
			{
				float amount = 0;
				float.TryParse (inputArray [1],out amount);
					for (int i = 0; i < GameControl.control.BankData.Count; i++)
					{
						for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
						{
							if (GameControl.control.BankData[i].Accounts[j].Primary == true)
							{
								GameControl.control.BankData[i].Accounts[j].AccountBalance = amount;
								PastCommands.Add("Added  " + amount + " to " + GameControl.control.BankData[i].Accounts[j].AccountNumber);
							}
						}
					}
			}
			break;

		case "createnewbankaccount":
			string accountnumber = StringGenerator.RandomNumberChar(6, 6);
			string accountpass = StringGenerator.RandomMixedChar(15, 15);
			GameControl.control.StoredLogins.Add(new LoginSystem("LEC Bank", accountnumber, accountpass, 0));
			for (int i = 0; i < GameControl.control.BankData.Count; i++)
			{
				GameControl.control.BankData[i].Accounts.Add(new BankAccountsSystem("192.168.56.91", "LEC Bank", accountnumber, accountnumber, accountpass, 0, 1,0, 0, 0, 1, false,false,null));
			}
			break;

			case "time":
			PastCommands.Add ("" + GameControl.control.Time.CurrentTime);
			break;

		case "date":
			PastCommands.Add ("" + GameControl.control.Time.FullDate);
			PastCommands.Add ("" + GameControl.control.Time.DayName);
			//PastCommands.Add ("" + ProfileController.procon.DayName + " " + ProfileController.procon.Day + " " + ProfileController.procon.MonthName);
			break;

		case "datetime":
			PastCommands.Add ("" + GameControl.control.Time.CurrentTime);
			PastCommands.Add ("" + GameControl.control.Time.FullDate);
			PastCommands.Add ("" + GameControl.control.Time.DayName);
			//PastCommands.Add ("" + ProfileController.procon.DayName + " " + ProfileController.procon.Day + " " + ProfileController.procon.MonthName);
			break;

		case "doubleclickdelay":
			if (inputArray [1] != "")
			{
				if (inputArray [1].Contains (AllCharacters))
				{
					PastCommands.Add ("Invalid character! type a numerical value [0-9]");
				}
				else 
				{
					float delay = 0;
					float.TryParse (inputArray [1],out delay);
					Customize.cust.DoubleClickDelayMenu = delay/1000;
					PastCommands.Add ("double click delay successfully set to " + Customize.cust.DoubleClickDelayMenu*1000f + "ms");
				}
			}
			break;

        case "selectnotisnd":
            if (inputArray[1] != "")
            {
                int red = 0;
                int.TryParse(inputArray[1], out red);
                Customize.cust.SelectedNotiSound = red;
            }
            break;

            case "color":
			switch (inputArray [1]) 
			{
			case "help":
				PastCommands.Add ("There is 3 kinds of options");
				PastCommands.Add ("for the first input text/font buttons and windows");
				PastCommands.Add ("example color text ");
				PastCommands.Add ("Next argument takes the type of color");
				PastCommands.Add ("example red green or blue");
				PastCommands.Add("type like so. color text red");
				PastCommands.Add("next just type the number like so");
				PastCommands.Add("color text red 125");
				PastCommands.Add("or you can type like this");
				PastCommands.Add("color font g 172");
				break;

			case "font":
				switch (inputArray [2]) 
				{

				case "red":
					if (inputArray [3] != "")
					{
						float red = 0;
						float.TryParse (inputArray [3],out red);
						GameControl.control.SelectedOS.Colour.Font.Red = red;
						SetFontColor();
					}
					break;

				case "green":
					if (inputArray [3] != "")
					{
						float green = 0;
						float.TryParse (inputArray [3],out green);
						GameControl.control.SelectedOS.Colour.Font.Green = green;
						SetFontColor();
					}
					break;

				case "blue":
					if (inputArray [3] != "")
					{
						float blue = 0;
						float.TryParse (inputArray [3],out blue);
						GameControl.control.SelectedOS.Colour.Font.Blue = blue;
						SetFontColor();
					}
					break;

				case "alpha":
					if (inputArray [3] != "")
					{
						float alpha = 0;
						float.TryParse (inputArray [3],out alpha);
						GameControl.control.SelectedOS.Colour.Font.Alpha = alpha;
						SetFontColor();
					}
					break;
				}
				break;

			case "button":
				switch (inputArray [2]) 
				{

				case "red":
					if (inputArray [3] != "")
					{
						float red = 0;
						float.TryParse (inputArray [3],out red);
						GameControl.control.SelectedOS.Colour.Button.Red = red;
						SetButtonColor();
					}
					break;

				case "green":
					if (inputArray [3] != "")
					{
						float green = 0;
						float.TryParse (inputArray [3],out green);
						GameControl.control.SelectedOS.Colour.Button.Green = green;
						SetButtonColor();
					}
					break;

				case "blue":
					if (inputArray [3] != "")
					{
						float blue = 0;
						float.TryParse (inputArray [3],out blue);
						GameControl.control.SelectedOS.Colour.Button.Blue = blue;
						SetButtonColor();
					}
					break;

				case "alpha":
					if (inputArray [3] != "")
					{
						float alpha = 0;
						float.TryParse (inputArray [3],out alpha);
						GameControl.control.SelectedOS.Colour.Button.Alpha = alpha;
						SetButtonColor();
					}
					break;
				}
				break;

			case "window":
				switch (inputArray [2]) 
				{

				case "red":
					if (inputArray [3] != "")
					{
						float red = 0;
						float.TryParse (inputArray [3],out red);
						GameControl.control.SelectedOS.Colour.Window.Red = red;
						SetWindowColor();
					}
					break;

				case "green":
					if (inputArray [3] != "")
					{
						float green = 0;
						float.TryParse (inputArray [3],out green);
						GameControl.control.SelectedOS.Colour.Window.Green = green;
						SetWindowColor();
					}
					break;

				case "blue":
					if (inputArray [3] != "")
					{
						float blue = 0;
						float.TryParse (inputArray [3],out blue);
						GameControl.control.SelectedOS.Colour.Window.Blue = blue;
						SetWindowColor();
					}
					break;

				case "alpha":
					if (inputArray [3] != "")
					{
						float alpha = 0;
						float.TryParse (inputArray [3],out alpha);
						GameControl.control.SelectedOS.Colour.Window.Alpha = alpha;
						SetWindowColor();
					}
					break;
				}
				break;
			}
			break;

		case "connect":
			ib.AddressBar = inputArray[1];
			ib.Inputted = inputArray[1];
			ib.WebSiteInfo();
			storedConnection = inputArray[1];
			break;

		case "systeminfo":
			//for (int i = 0; i < cpu.CPUSpeed.Count; i++)
			//{
			//	PastCommands.Add ("Core" + i + " " + cpu.CPUSpeed[i]);
			//}
			PastCommands.Add ("Memory: " + ram.RemainingRAM);
			PastCommands.Add ("Power: " + psu.RemainingPower);
			//PastCommands.Add ("Hard Drives: " + HardwareController.hdcon.HDDFreeSpace);
			break;

		case "restart":
				ShutDownWindow.Restart();
				break;

		case "safemode":
			GameControl.control.SelectedOS.Name = OperatingSystems.OSName.SafeMode;
			GameControl.control.GatewayStatus.Terminal = true;
			GameControl.control.GatewayStatus.Booted = false;
			GameControl.control.GatewayStatus.SafeMode = true;
			SceneManager.LoadScene("Game");
			break;

		case "shutdown":
			ShutDownWindow.Shutdown();
			break;

		case "logout":
			if(GameControl.control.GatewayStatus.Terminal == true)
			{
				PastCommands.Add("Signing Out..");
				ShutDownWindow.SignOut();
			}
			else
			{
				ShutDownWindow.SignOutUI();
			}
			break;

		case "exit":
			PastCommands.RemoveRange(0, PastCommands.Count);
			appman.SelectedApp = "CLI";
			break;

		case "background":
			if (inputArray [1] == "help")
			{
				PastCommands.Add ("This command allows you to");
				PastCommands.Add ("change the background by");
				PastCommands.Add ("typing background C:/users/admin/pictures/test.jpg");
			}
			else
			{
				Customize.cust.CustomTexFileNames [4] = inputArray [1];
				sp.ApplyBackgrounds();
			}
			break;

		case "sound":
			sc.SoundSelect = 1;
			sc.PlaySound();
			break;

		case "mode":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("mode");
				PastCommands.Add("This allows you to change");
				PastCommands.Add("how big the terminal is");
				PastCommands.Add("it has 4 modes");
				PastCommands.Add("Small|Medium|Large|Terminal");
				PastCommands.Add("example use of this command");
				PastCommands.Add("mode Terminal");
			}
			else
			{
				Customize.cust.Mode = inputArray [1];
				cliv2.PosCheck();
				PastCommands.Add ("CLIv2 Window has been set to " + Customize.cust.Mode + " Mode.");
			}
			break;

		case "setautodelete":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("setautodelete");
				PastCommands.Add("This allows you to set");
				PastCommands.Add("after how many past lines");
				PastCommands.Add("will be showed before the first");
				PastCommands.Add("line gets deleted");
				PastCommands.Add("example use of this command");
				PastCommands.Add("setautodelete 50");
			}
			else
			{
				int amt = 0;
				int.TryParse (inputArray [1], out amt);
				Customize.cust.DeletionAmt = amt;
				PastCommands.Add ("Automatic Past Delete has been set to " + Customize.cust.DeletionAmt);
			}
			break;

		case "clifontsize":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("clifontsize");
				PastCommands.Add("This allows you to change");
				PastCommands.Add("The terminals font size");
				PastCommands.Add("example use of this command");
				PastCommands.Add("setfontsize 24");
			}
			else
			{
				int clifontsize = 0;
				int.TryParse (inputArray [1], out clifontsize);
				Customize.cust.TerminalFontSize = clifontsize;
				PastCommands.Add ("CLIv2 Font Size has been set to " + Customize.cust.TerminalFontSize);
			}
			break;

		case "clitextpos":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("clitextpos");
				PastCommands.Add("This allows you to offset");
				PastCommands.Add("The input text postion");
				PastCommands.Add("example use of this command");
				PastCommands.Add("settextpos 7.5");
			}
			else
			{
				float clitextpos = 0;
				float.TryParse (inputArray [1], out clitextpos);
				Customize.cust.TerminalTextPosMod = clitextpos;
				PastCommands.Add ("CLIv2 Text Position has been set to " + Customize.cust.TerminalTextPosMod);
			}
			break;

		case "setcommandchar":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("setcommandchar");
				PastCommands.Add("This allows you to change");
				PastCommands.Add("what is defined as a command");
				PastCommands.Add("an example would be run;Test or run Test");
				PastCommands.Add("to set a character for commands");
				PastCommands.Add("setcommandchar ;");
			}
			else
			{
				Customize.cust.TerminalCommandCharacterSplit = inputArray[1];
				PastCommands.Add ("Command Character has been set to " + Customize.cust.TerminalCommandCharacterSplit);
			}
			break;

		case "whoami":
			PastCommands.Add (GameControl.control.ProfileName);
			break;

		case "setspacechar":
			if (inputArray[1] == "help")
			{
				PastCommands.Add("setspacechar");
				PastCommands.Add("This allows you to change");
				PastCommands.Add("what is defined as a space for a file");
				PastCommands.Add("an example would be open Test_Doc");
				PastCommands.Add("to set a character for spaces");
				PastCommands.Add("setspacechar _");
			}
			else
			{
				Customize.cust.TerminalSpaceCharacterSplit = inputArray[1];
				PastCommands.Add ("Space Character has been set to " + Customize.cust.TerminalSpaceCharacterSplit);
			}
			break;

		case "volume":
			if (inputArray [1] == "") 
			{
				PastCommands.Add ("Volume Not set please type a value between 0 or 100");
			} 
			else 
			{
				for (int i = 0; i < AllCharacters.Length; i++)
				{
					if (inputArray [1].Contains (AllCharacters [i].ToString ())) 
					{
						PastCommands.Add ("Invalid character type please type a number character between 0 and 100");
					} 
					else
					{
						if (AllCharacters[i].ToString() == "z") 
						{
							float volume = 0;
							float.TryParse (inputArray [1], out volume);
							Customize.cust.Volume = volume / 100;
							sc.SetVolume ();
							sc.SoundSelect = 1;
							sc.PlaySound ();
						}
					}
				}
			}
			break;
		}
	}
}