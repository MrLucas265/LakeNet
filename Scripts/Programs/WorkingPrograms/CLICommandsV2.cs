using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class CLICommandsV2 : MonoBehaviour
{
	public List<string> PastCommands = new List<string>();
	public List<string> Functions = new List<string>();
	public List<string> CommandNames = new List<string>();
	public List<CLICMDS> SystemCommands = new List<CLICMDS>();
	public string CommandLine;
	public string[] inputArray;
	public string[] ParseArray;

	public bool AutoScroll;

	private InternetBrowser ib;
	//private Notepad note;
	private Progtive pro;
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
	private OS os;
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

	private CPU cpu;
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

	public bool writeInfo;

	public ProgramSystem TempCopy;

	public int SelectedFolder;

	public bool SetScrollPos;

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
		pro = GetComponent<Progtive>();
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
		os = GetComponent<OS>();
		def = GetComponent<Defalt>();
		sc = system.GetComponent<SoundControl>();

		cpu = hardware.GetComponent<CPU>();
		ram = hardware.GetComponent<RAM>();
		psu = hardware.GetComponent<PSU>();
		hdd = hardware.GetComponent<HardDrives>();
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
						if(boot.Terminal == true)
						{
							PastCommands.Add("Command-Line Interface Version 3.0");
						}
						PastCommands.Add ("Type " + GameControl.control.Commands[i].Name + " for the list of system commands");
					}
				}
			}
		}
	}

	public void SetSystemCommands()
	{
		if(SystemCommands.Count == 0)
		{
			//MAIN
			SystemCommands.Add(new CLICMDS("-help", "-help"));
			SystemCommands.Add(new CLICMDS("help", "help"));
			SystemCommands.Add(new CLICMDS("listcommands", "listcommands"));
			//DEV
			SystemCommands.Add(new CLICMDS("version", "version"));
			SystemCommands.Add(new CLICMDS("errorlog", "errorlog"));
			SystemCommands.Add(new CLICMDS("viewrep", "viewrep"));
			SystemCommands.Add(new CLICMDS("accountinfo", "accountinfo"));
			SystemCommands.Add(new CLICMDS("doubleclickdelay", "doubleclickdelay"));
			SystemCommands.Add(new CLICMDS("setbankbalance", "setbankbalance"));
			SystemCommands.Add(new CLICMDS("printinfo", "writeinfo"));
			SystemCommands.Add(new CLICMDS("url", "url"));
			SystemCommands.Add(new CLICMDS("error", "error"));
			SystemCommands.Add(new CLICMDS("notification", "noti"));
			SystemCommands.Add(new CLICMDS("syscrash", "syscrash"));
			SystemCommands.Add(new CLICMDS("skipmission", "skipmission"));
			SystemCommands.Add(new CLICMDS("settimemod", "settimemod"));
			SystemCommands.Add(new CLICMDS("enablemusicplayer", "enablemusicplayer"));

			if (GameControl.control.ShortCommands == true)
            {
                //PREFRENCES
                SystemCommands.Add(new CLICMDS("vol", "volume"));
                SystemCommands.Add(new CLICMDS("snd", "sound"));
                SystemCommands.Add(new CLICMDS("bg", "background"));
                SystemCommands.Add(new CLICMDS("aql", "aql"));
                SystemCommands.Add(new CLICMDS("theme", "theme"));
                //TERMINAL
                SystemCommands.Add(new CLICMDS("mode", "mode"));
                SystemCommands.Add(new CLICMDS("setautodelete", "setautodelete"));
                SystemCommands.Add(new CLICMDS("setfontsize", "clifontsize"));
                SystemCommands.Add(new CLICMDS("settextpos", "clitextpos"));
                SystemCommands.Add(new CLICMDS("setcommandchar", "setcommandchar"));
                SystemCommands.Add(new CLICMDS("setspacechar", "setspacechar"));
                //COLOR COMMANDS
                SystemCommands.Add(new CLICMDS("color", "color"));
                SystemCommands.Add(new CLICMDS("font", "font"));
                SystemCommands.Add(new CLICMDS("button", "button"));
                SystemCommands.Add(new CLICMDS("window", "window"));
                SystemCommands.Add(new CLICMDS("red", "red"));
                SystemCommands.Add(new CLICMDS("blue", "blue"));
                SystemCommands.Add(new CLICMDS("green", "green"));
                //NETWORK COMMANDS
                SystemCommands.Add(new CLICMDS("-r", "-r"));
                SystemCommands.Add(new CLICMDS("dl", "dl"));
                SystemCommands.Add(new CLICMDS("ul", "ul"));
                SystemCommands.Add(new CLICMDS("connect", "connect"));
                //LOCAL COMMANDS
                SystemCommands.Add(new CLICMDS("-l", "-l"));
                //UTILITY COMMANDS
                SystemCommands.Add(new CLICMDS("clear", "clear"));
                SystemCommands.Add(new CLICMDS("rm", "rm"));
                SystemCommands.Add(new CLICMDS("cd", "cd"));
                SystemCommands.Add(new CLICMDS("run", "run"));
                SystemCommands.Add(new CLICMDS("open", "open"));
                SystemCommands.Add(new CLICMDS("pwd", "pwd"));
                SystemCommands.Add(new CLICMDS("ls", "ls"));
                SystemCommands.Add(new CLICMDS("q", "quit"));
                SystemCommands.Add(new CLICMDS("copy", "copy"));
                SystemCommands.Add(new CLICMDS("paste", "paste"));
                SystemCommands.Add(new CLICMDS("copy1", "copy1"));
                SystemCommands.Add(new CLICMDS("paste1", "paste1"));
                SystemCommands.Add(new CLICMDS("paste2", "paste2"));
                SystemCommands.Add(new CLICMDS("mkdir", "mkdir"));
                SystemCommands.Add(new CLICMDS("del", "del"));
                SystemCommands.Add(new CLICMDS("mkdoc", "mkdoc"));
                SystemCommands.Add(new CLICMDS("time", "time"));
                SystemCommands.Add(new CLICMDS("whoami", "whoami"));
                SystemCommands.Add(new CLICMDS("systeminfo", "systeminfo"));
                SystemCommands.Add(new CLICMDS("screenshot", "screenshot"));
                SystemCommands.Add(new CLICMDS("listprograms", "listprograms"));
                SystemCommands.Add(new CLICMDS("selectbankaccount", "selectbankaccount"));
                SystemCommands.Add(new CLICMDS("restart", "restart"));
                SystemCommands.Add(new CLICMDS("exit", "exit"));
                SystemCommands.Add(new CLICMDS("syspan", "syspan"));
				SystemCommands.Add(new CLICMDS("time", "time"));
				SystemCommands.Add(new CLICMDS("date", "date"));
				SystemCommands.Add(new CLICMDS("datetime", "datetime"));
                //PROGRAM COMMANDS
                SystemCommands.Add(new CLICMDS("listemails", "printemails"));
                SystemCommands.Add(new CLICMDS("viewemails", "viewselectedemail"));
                SystemCommands.Add(new CLICMDS("selectnotisnd", "selectnotisnd"));
            }
            else
            {
                //PREFRENCES
                SystemCommands.Add(new CLICMDS("volume", "volume"));
                SystemCommands.Add(new CLICMDS("sound", "sound"));
                SystemCommands.Add(new CLICMDS("background", "background"));
                SystemCommands.Add(new CLICMDS("addtoquicklaunch", "aql"));
                SystemCommands.Add(new CLICMDS("theme", "theme"));
                //TERMINAL
                SystemCommands.Add(new CLICMDS("mode", "mode"));
                SystemCommands.Add(new CLICMDS("setautodelete", "setautodelete"));
                SystemCommands.Add(new CLICMDS("setfontsize", "clifontsize"));
                SystemCommands.Add(new CLICMDS("settextpos", "clitextpos"));
                SystemCommands.Add(new CLICMDS("setcommandchar", "setcommandchar"));
                SystemCommands.Add(new CLICMDS("setspacechar", "setspacechar"));
                //COLOR COMMANDS
                SystemCommands.Add(new CLICMDS("color", "color"));
                SystemCommands.Add(new CLICMDS("font", "font"));
                SystemCommands.Add(new CLICMDS("button", "button"));
                SystemCommands.Add(new CLICMDS("window", "window"));
                SystemCommands.Add(new CLICMDS("red", "red"));
                SystemCommands.Add(new CLICMDS("blue", "blue"));
                SystemCommands.Add(new CLICMDS("green", "green"));
                //NETWORK COMMANDS
                SystemCommands.Add(new CLICMDS("remote", "-r"));
                SystemCommands.Add(new CLICMDS("download", "dl"));
                SystemCommands.Add(new CLICMDS("upload", "ul"));
                SystemCommands.Add(new CLICMDS("connect", "connect"));
                //LOCAL COMMANDS
                SystemCommands.Add(new CLICMDS("local", "-l"));
                //UTILITY COMMANDS
                SystemCommands.Add(new CLICMDS("clear", "clear"));
                SystemCommands.Add(new CLICMDS("remove", "rm"));
                SystemCommands.Add(new CLICMDS("changedirectory", "cd"));
                SystemCommands.Add(new CLICMDS("run", "run"));
                SystemCommands.Add(new CLICMDS("open", "open"));
                SystemCommands.Add(new CLICMDS("printworkingdirectory", "pwd"));
                SystemCommands.Add(new CLICMDS("list", "ls"));
                SystemCommands.Add(new CLICMDS("close", "q"));
                SystemCommands.Add(new CLICMDS("copy", "copy"));
                SystemCommands.Add(new CLICMDS("paste", "paste"));
                SystemCommands.Add(new CLICMDS("copy1", "copy1"));
                SystemCommands.Add(new CLICMDS("paste1", "paste1"));
                SystemCommands.Add(new CLICMDS("paste2", "paste2"));
                SystemCommands.Add(new CLICMDS("makedirectory", "mkdir"));
                SystemCommands.Add(new CLICMDS("delete", "del"));
                SystemCommands.Add(new CLICMDS("makedocument", "mkdoc"));
                SystemCommands.Add(new CLICMDS("time", "time"));
                SystemCommands.Add(new CLICMDS("whoami", "whoami"));
                SystemCommands.Add(new CLICMDS("systeminfo", "systeminfo"));
                SystemCommands.Add(new CLICMDS("screenshot", "screenshot"));
                SystemCommands.Add(new CLICMDS("listprograms", "listprograms"));
                SystemCommands.Add(new CLICMDS("selectbankaccount", "selectbankaccount"));
                SystemCommands.Add(new CLICMDS("restart", "restart"));
                SystemCommands.Add(new CLICMDS("exit", "exit"));
                SystemCommands.Add(new CLICMDS("syspan", "syspan"));
				SystemCommands.Add(new CLICMDS("time", "time"));
				SystemCommands.Add(new CLICMDS("date", "date"));
				SystemCommands.Add(new CLICMDS("datetime", "datetime"));
                //PROGRAM COMMANDS
                SystemCommands.Add(new CLICMDS("listemails", "printemails"));
                SystemCommands.Add(new CLICMDS("viewemails", "viewselectedemail"));
                SystemCommands.Add(new CLICMDS("selectnotisnd", "selectnotisnd"));
            }
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

	void SetFontColor()
	{
		Color32 Fontcolor;
		Fontcolor.r = (byte)Customize.cust.FontR;
		Fontcolor.g = (byte)Customize.cust.FontG;
		Fontcolor.b = (byte)Customize.cust.FontB;
		Fontcolor.a = (byte)Customize.cust.FontA;
		com.colors[1] = Fontcolor;
	}

	void SetButtonColor()
	{
		Color32 ButtonColor;
		ButtonColor.r = (byte)Customize.cust.ButtonR;
		ButtonColor.g = (byte)Customize.cust.ButtonG;
		ButtonColor.b = (byte)Customize.cust.ButtonB;
		ButtonColor.a = (byte)Customize.cust.ButtonA;
		com.colors[2] = ButtonColor;
	}

	void SetWindowColor()
	{
		Color32 WindowColor;
		WindowColor.r = (byte)Customize.cust.WindowR;
		WindowColor.g = (byte)Customize.cust.WindowG;
		WindowColor.b = (byte)Customize.cust.WindowB;
		WindowColor.a = (byte)Customize.cust.WindowA;
		com.colors[3] = WindowColor;
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
		//if (inputArray.Length > 2)
		//{
		//	if (inputArray[2] != "")
		//	{
		//		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		//		{
		//			if (GameControl.control.ProgramFiles [Index].Name == ParseArray[2]) 
		//			{
		//				if (GameControl.control.ProgramFiles[Index].Location == ParseArray[1]) 
		//				{
		//					appman.enabled = true;
		//					appman.SelectedApp = GameControl.control.ProgramFiles[Index].Target;
		//				}
		//			}
		//		}
		//	}
		//	else
		//	{
		//		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		//		{
		//			if (GameControl.control.ProgramFiles[Index].Name == inputArray[1]) 
		//			{
		//				if (twd == GameControl.control.ProgramFiles [Index].Location) 
		//				{
		//					appman.enabled = true;
		//					appman.SelectedApp = GameControl.control.ProgramFiles[Index].Target;
		//				}
		//			}
		//		}
		//	}
		//}
		//else
		//{
		//	for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		//	{
		//		if (GameControl.control.ProgramFiles[Index].Name == inputArray[1]) 
		//		{
		//			if (twd == GameControl.control.ProgramFiles [Index].Location) 
		//			{
		//				appman.enabled = true;
		//				appman.SelectedApp = GameControl.control.ProgramFiles[Index].Target;
		//			}
		//		}
		//	}
		//}
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
		}
	}

	void OpenFile()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
		{
			if (GameControl.control.ProgramFiles[i].Name == ParseArray[1]) 
			{
				if (twd == GameControl.control.ProgramFiles [i].Location) 
				{
					note.SelectedDocument = i;
					if (note.show == true && note.enabled == true) 
					{
						note.CurrentWorkingTitle = GameControl.control.ProgramFiles[i].Name;
						note.TypedTitle = GameControl.control.ProgramFiles[i].Name;
						note.TypedText = GameControl.control.ProgramFiles[i].Content;
						note.SaveLocation = GameControl.control.ProgramFiles [i].Location;
						note.ShowFileContent = true;
					}
					if (note.show == false || note.enabled == false) 
					{
						appman.SelectedApp = "Notepad";
						note.CurrentWorkingTitle = GameControl.control.ProgramFiles[i].Name;
						note.TypedTitle = GameControl.control.ProgramFiles[i].Name;
						note.TypedText = GameControl.control.ProgramFiles[i].Content;
						note.SaveLocation = GameControl.control.ProgramFiles [i].Location;
						note.ShowFileContent = true;
					}	
				}
			}
		}
	}

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
					GameControl.control.ProgramFiles.Add (new ProgramSystem (FolderName, "", "", "", twd, "" + twd +  "/" + FolderName, 0,0,0,0,0,0, false, ProgramSystem.ProgramType.Txt));
				} 
				else 
				{
					FolderName = inputArray [1];
					GameControl.control.ProgramFiles.Add (new ProgramSystem (FolderName, "", "", "", twd, "" + twd +  "" + FolderName, 0,0,0,0,0,0, false, ProgramSystem.ProgramType.Txt));
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
					FileName = GameControl.control.ProgramFiles[Index].Name;
					FileSize = GameControl.control.ProgramFiles[Index].Used;
					FileContent = GameControl.control.ProgramFiles[Index].Content;
					FileTarget = GameControl.control.ProgramFiles[Index].Target;
					FileVersion = GameControl.control.ProgramFiles[Index].Version;
					FileType = GameControl.control.ProgramFiles[Index].Type.ToString();
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
							if (GameControl.control.ProgramFiles[Index].Type == ProgramSystem.ProgramType.Fdl || GameControl.control.ProgramFiles[Index].Type == ProgramSystem.ProgramType.Dir)
							{
								if (GameControl.control.ProgramFiles[Index].Target == inputArray[1])
								{
									string Location = inputArray[1];
									fu.ProgramHandle.Add(new FileUtilitySystem("Paste", FileName, Location, "", FileTarget, FileContent, FileType, false, true, true, false, FileVersion, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
									fu.AddWindow();
									PastCommands.Add(FileName + " is being pasted to: " + Location);
								}
							}
						}
					}
					else
					{
						string FileLocation = twd;
						fu.ProgramHandle.Add (new FileUtilitySystem ("Paste", FileName, FileLocation,"", FileTarget,FileContent,FileType, false, true, true, false,FileVersion,0,0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
						fu.AddWindow ();
						PastCommands.Add (FileName + " is being pasted to: " + FileLocation);
					}
				}
				else
				{
					string FileLocation = twd;
					fu.ProgramHandle.Add (new FileUtilitySystem ("Paste", FileName, FileLocation,"", FileTarget,FileContent,FileType, false, true, true, false,FileVersion,0,0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
					fu.AddWindow ();
					PastCommands.Add (FileName + " is being pasted to: " + FileLocation);
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
				GameControl.control.QuickProgramList.Add (GameControl.control.ProgramFiles[i]);
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
		if (ib.CurrentLocation != "") 
		{
			for (int Index = 0; Index < GameControl.control.WebsiteFiles.Count; Index++) 
			{
				if (GameControl.control.WebsiteFiles[Index].Name == inputArray [2])
				{
					if (GameControl.control.WebsiteFiles [Index].Location == ib.CurrentLocation)
					{
						FileIndex = Index;
						FileName = GameControl.control.WebsiteFiles[FileIndex].Name;
						FileSize = GameControl.control.WebsiteFiles[FileIndex].Used;
					}
				}
			}

			if (fu.ProgramHandle.Count <= 0) 
			{
				if (FileIndex != -1) 
				{
					fu.ProgramHandle.Add (new FileUtilitySystem ("Delete", FileName, FileLocation, "","","","", false, true, true, false, 0, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.RemoteDelete));
					fu.AddWindow ();
				}
			}
		}
	}

	void CLIDeleteFile()
	{
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		{
			if (GameControl.control.ProgramFiles[Index].Name == inputArray [1])
			{
				if (GameControl.control.ProgramFiles [Index].Location == twd)
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
				fu.ProgramHandle.Add (new FileUtilitySystem ("Delete", FileName, twd, "","","","", false, true, true, false, 0, 0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.LocalDelete));
				fu.AddWindow ();
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
					FolderName = inputArray [1];
					GameControl.control.ProgramFiles.Add (new ProgramSystem (FolderName, "", "", "", twd, "" + twd +  "/" + FolderName, 0,0,0,0,0,0, false, ProgramSystem.ProgramType.Fdl));
				} 
				else 
				{
					FolderName = inputArray [1];
					GameControl.control.ProgramFiles.Add (new ProgramSystem (FolderName, "", "", "", twd, "" + twd +  "" + FolderName, 0,0,0,0,0,0, false, ProgramSystem.ProgramType.Fdl));
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
				}
			}
		}

		if (fu.ProgramHandle.Count <= 0) 
		{
			if (FileIndex != -1) 
			{
                if (GameControl.control.ProgramFiles[FileIndex].Type == ProgramSystem.ProgramType.Fdl)
				{
					fu.ForceDone = true;
					fu.FileIndex = GameControl.control.ProgramFiles.Count-1;
					fu.ProgramHandle.Add (new FileUtilitySystem ("Delete", FileName, FileLocation,"", "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.LocalFolderDelete));
				}
				else
				{
					fu.ProgramHandle.Add (new FileUtilitySystem ("Delete", FileName, FileLocation,"", "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.LocalDelete));
				}
				fu.AddWindow ();
			}
		}
	}

	void DownloadFile()
	{
		if (ib.CurrentLocation != "")
		{
			for (int Index = 0; Index < GameControl.control.WebsiteFiles.Count; Index++) 
			{
				if (GameControl.control.WebsiteFiles [Index].Name == inputArray [1])
				{
					FileIndex = Index;
					FileName = GameControl.control.WebsiteFiles [FileIndex].Name;
					FileSize = GameControl.control.WebsiteFiles [FileIndex].Used;
				}
			}
			if (fu.ProgramHandle.Count <= 0) 
			{
				if (FileIndex != -1)
				{
					fu.ProgramHandle.Add (new FileUtilitySystem ("Download", FileName, "", Customize.cust.DownloadPath, "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Download));
					fu.AddWindow ();
				}
			}
		}
	}

	void UploadFile()
	{
		if (ib.CurrentLocation != "")
		{
			for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
			{
				if (GameControl.control.ProgramFiles[Index].Name == inputArray [1])
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
					fu.ProgramHandle.Add (new FileUtilitySystem ("Upload", FileName, FileLocation,"", "","","", false, true, true, false, 0, 0,0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Upload));
					fu.AddWindow ();
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
			if (GameControl.control.ProgramFiles [i].Type == ProgramSystem.ProgramType.Exe)
			{
				PastCommands.Add (GameControl.control.ProgramFiles [i].Name);
			}
		}
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

	public void CommandCheck()
	{
		Functions.RemoveRange (0, Functions.Count);
		CommandLine = "";
		//ParseArray = Parse.Split(' ');
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
					if (ParseArray[j] == GameControl.control.Commands [i].Name) 
					{
						if (!Functions.Contains (GameControl.control.Commands [i].Func))
						{
							Functions.Add(GameControl.control.Commands [i].Func);
						}

						LastItem = ParseArray [ParseArray.Length - 1];
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
	}

	public void CheckInput()
	{
		inputArray = CommandLine.Split('▓');

		switch(inputArray[0])
		{

		case"aql":
            if (inputArray[1] == "")
            {
                ProgramQuickLaunch();
            }
			break;

            case "error":
                if (inputArray[1] == "1")
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
				for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
				{
					if (inputArray [1] == GameControl.control.ProgramFiles [i].Name)
					{
						if (twd == GameControl.control.ProgramFiles [i].Location) 
						{
							if (twd != GameControl.control.ProgramFiles [i].Target) 
							{
								if (writeInfo == true) 
								{
									PastCommands.Add ("CLI Working Directory Changed From : " + twd);
									twd = GameControl.control.ProgramFiles [i].Target;
									PastCommands.Add ("CLI Working Directory Changed To : " + twd);
								} 
								else 
								{
									twd = GameControl.control.ProgramFiles [i].Target;
								}
							}
						} 
					}
					else if (inputArray [1] == GameControl.control.ProgramFiles [i].Location) 
					{
						if (twd != GameControl.control.ProgramFiles [i].Location)
						{
							if (writeInfo == true) 
							{
								PastCommands.Add ("CLI Working Directory Changed From : " + twd);
								twd = GameControl.control.ProgramFiles [i].Location;
								PastCommands.Add ("CLI Working Directory Changed To : " + twd);
							} 
							else 
							{
								twd = GameControl.control.ProgramFiles [i].Location;
							}
						}
					}
					else if (inputArray [1] == GameControl.control.ProgramFiles [i].Target) 
					{
						if (twd != GameControl.control.ProgramFiles [i].Target) 
						{
							if (writeInfo == true) 
							{
								PastCommands.Add ("CLI Working Directory Changed From : " + twd);
								twd = GameControl.control.ProgramFiles [i].Target;
								PastCommands.Add ("CLI Working Directory Changed To : " + twd);
							} 
							else 
							{
								twd = GameControl.control.ProgramFiles [i].Target;
							}
						}
					}
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
			PastCommands.Add ("Serial Key: " + GameControl.control.SerialKey);
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

			case "q":
            if (inputArray[1] == "help")
            {
                PastCommands.Add("close");
                PastCommands.Add("lets you close a running program");
                PastCommands.Add("example close Calculator");
            }
            else
            {
                CloseProgram();
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
				OpenFile();
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
							fu.ProgramHandle.Add (new FileUtilitySystem ("Paste", FileName, FileLocation,"", "","","", false, true, true, false, 0, 0,0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
							fu.AddWindow ();
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
						GameControl.control.ProgramFiles.Add (new ProgramSystem (inputArray [2], "", "", "", inputArray [3], "" + inputArray [3] + "/" + inputArray [2], 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
						//GameControl.control.ProgramFiles.Add (new ProgramSystem (inputArray [2], "", "", "", inputArray [3], "" + inputArray [4], 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
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
							fu.ProgramHandle.Add (new FileUtilitySystem ("Delete", FileName, FileLocation,"", "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.LocalDelete));
							fu.AddWindow ();
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
				GameControl.control.SelectedBank = select;
				PastCommands.Add ("Selected account " + select);
			}
			break;

		case "setbankbalance":
			if (inputArray [1] != "")
			{
				float amount = 0;
				float.TryParse (inputArray [1],out amount);
				GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance = amount;
				PastCommands.Add ("Added  " + amount + " to " + GameControl.control.SelectedBank);
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
						Customize.cust.FontR = red;
						SetFontColor();
						//SetFontColor();
					}
					break;

				case "green":
					if (inputArray [3] != "")
					{
						float green = 0;
						float.TryParse (inputArray [3],out green);
						Customize.cust.FontG = green;
						SetFontColor();
					}
					break;

				case "blue":
					if (inputArray [3] != "")
					{
						float blue = 0;
						float.TryParse (inputArray [3],out blue);
						Customize.cust.FontB = blue;
						SetFontColor();
					}
					break;

				case "alpha":
					if (inputArray [3] != "")
					{
						float alpha = 0;
						float.TryParse (inputArray [3],out alpha);
						Customize.cust.FontB = alpha;
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
						Customize.cust.ButtonR = red;
						SetFontColor();
						//SetFontColor();
					}
					break;

				case "green":
					if (inputArray [3] != "")
					{
						float green = 0;
						float.TryParse (inputArray [3],out green);
						Customize.cust.ButtonG = green;
						SetFontColor();
					}
					break;

				case "blue":
					if (inputArray [3] != "")
					{
						float blue = 0;
						float.TryParse (inputArray [3],out blue);
						Customize.cust.ButtonB = blue;
						SetFontColor();
					}
					break;

				case "alpha":
					if (inputArray [3] != "")
					{
						float alpha = 0;
						float.TryParse (inputArray [3],out alpha);
						Customize.cust.ButtonB = alpha;
						SetFontColor();
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
						Customize.cust.WindowR = red;
						SetFontColor();
						//SetFontColor();
					}
					break;

				case "green":
					if (inputArray [3] != "")
					{
						float green = 0;
						float.TryParse (inputArray [3],out green);
						Customize.cust.WindowG = green;
						SetFontColor();
					}
					break;

				case "blue":
					if (inputArray [3] != "")
					{
						float blue = 0;
						float.TryParse (inputArray [3],out blue);
						Customize.cust.WindowB = blue;
						SetFontColor();
					}
					break;

				case "alpha":
					if (inputArray [3] != "")
					{
						float alpha = 0;
						float.TryParse (inputArray [3],out alpha);
						Customize.cust.WindowA = alpha;
						SetFontColor();
					}
					break;
				}
				break;
			}
			break;

		case "connect":
			ib.AddressBar = inputArray[1];
			ib.Inputted = inputArray[1];
			storedConnection = inputArray[1];
			break;

		case "systeminfo":
			for (int i = 0; i < cpu.CPUSpeed.Count; i++)
			{
				PastCommands.Add ("Core" + i + " " + cpu.CPUSpeed[i]);
			}
			PastCommands.Add ("Memory: " + ram.RemainingRAM);
			PastCommands.Add ("Power: " + psu.RemainingPower);
			PastCommands.Add ("Hard Drives: " + HardwareController.hdcon.HDDFreeSpace);
			break;

		case "restart":
				GameControl.control.Booted = false;
				Application.LoadLevel(1);
			break;

		case "exit":
				PastCommands.RemoveRange(0, PastCommands.Count);
				appman.SelectedApp = "Command Line V3";
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