using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLICommands : MonoBehaviour
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

	private VersionList vl;

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

		SCM = Crash.GetComponent<SysCrashMan> ();

		note = applications.GetComponent<Notepad> ();

		vl = system.GetComponent<VersionList>();

		misgen = Missions.GetComponent<MissionGen> ();

		AfterStart();

		if(twd == "")
		{
			twd = "Gateway";
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
					if (SystemCommands [i].Func == "listcommands") 
					{
						PastCommands.Add ("Type " + SystemCommands[i].Name + " for the list of system commands");
					}
				}
			}
		}
	}

	public void SetSystemCommands()
	{
		if(SystemCommands.Count == 0)
		{
			//PREFRENCES
			SystemCommands.Add(new CLICMDS("volume","volume"));
			SystemCommands.Add(new CLICMDS("sound","sound"));
			SystemCommands.Add(new CLICMDS("background","background"));
			SystemCommands.Add(new CLICMDS("aql","aql"));
			SystemCommands.Add(new CLICMDS("theme","theme"));
			//TERMINAL
			SystemCommands.Add(new CLICMDS("mode","mode"));
			SystemCommands.Add(new CLICMDS("setautodelete","setautodelete"));
			SystemCommands.Add(new CLICMDS("setfontsize","clifontsize"));
			SystemCommands.Add(new CLICMDS("settextpos","clitextpos"));
			SystemCommands.Add(new CLICMDS("setcommandchar","setcommandchar"));
			SystemCommands.Add(new CLICMDS("setspacechar","setspacechar"));
			//COLOR COMMANDS
			SystemCommands.Add(new CLICMDS("color","color"));
			SystemCommands.Add(new CLICMDS("font","font"));
			SystemCommands.Add(new CLICMDS("button","button"));
			SystemCommands.Add(new CLICMDS("window","window"));
			SystemCommands.Add(new CLICMDS("red","red"));
			SystemCommands.Add(new CLICMDS("blue","blue"));
			SystemCommands.Add(new CLICMDS("green","green"));
			//NETWORK COMMANDS
			SystemCommands.Add(new CLICMDS("-r","-r"));
			SystemCommands.Add(new CLICMDS("dl","dl"));
			SystemCommands.Add(new CLICMDS("ul","ul"));
			SystemCommands.Add(new CLICMDS("connect","connect"));
			//LOCAL COMMANDS
			SystemCommands.Add(new CLICMDS("-l","-l"));
			//UTILITY COMMANDS
			SystemCommands.Add(new CLICMDS("h","help"));
			SystemCommands.Add(new CLICMDS("cls","clear"));
			SystemCommands.Add(new CLICMDS("rm","rm"));
			SystemCommands.Add(new CLICMDS("cd","cd"));
			SystemCommands.Add(new CLICMDS("run","run"));
			SystemCommands.Add(new CLICMDS("open","open"));
			SystemCommands.Add(new CLICMDS("pwd","pwd"));
			SystemCommands.Add(new CLICMDS("ls","ls"));
			SystemCommands.Add(new CLICMDS("copy","copy"));
			SystemCommands.Add(new CLICMDS("paste","paste"));
			SystemCommands.Add(new CLICMDS("mkdir","mkdir"));
			SystemCommands.Add(new CLICMDS("del","del"));
			SystemCommands.Add(new CLICMDS("mkdoc","mkdoc"));
			SystemCommands.Add(new CLICMDS("time","time"));
			SystemCommands.Add(new CLICMDS("alias","alias"));
			SystemCommands.Add(new CLICMDS("whoami","whoami"));
			SystemCommands.Add(new CLICMDS("systeminfo","systeminfo"));
			SystemCommands.Add(new CLICMDS("screenshot","screenshot"));
			SystemCommands.Add(new CLICMDS("lscmds","listcommands"));
			SystemCommands.Add(new CLICMDS("lsapps","listprograms"));
			SystemCommands.Add(new CLICMDS("restart","restart"));
			SystemCommands.Add(new CLICMDS("exit","exit"));
			//PROGRAM COMMANDS
			SystemCommands.Add(new CLICMDS("lsemails","printemails"));
			SystemCommands.Add(new CLICMDS("viewemail","viewselectedemail"));
			//DEV
			SystemCommands.Add(new CLICMDS("godmode","godmode"));
			SystemCommands.Add(new CLICMDS("noti1","noti1"));
			SystemCommands.Add(new CLICMDS("noti2","noti2"));
			SystemCommands.Add(new CLICMDS("error1","error1"));
			SystemCommands.Add(new CLICMDS("error2","error2"));
			SystemCommands.Add(new CLICMDS("error3","error3"));
			SystemCommands.Add(new CLICMDS("enablemusicplayer","enablemusicplayer"));
			SystemCommands.Add(new CLICMDS("setreplevel","setreplevel"));
			SystemCommands.Add(new CLICMDS("setbalance","setbalance"));
			SystemCommands.Add(new CLICMDS("motherload","motherload"));
			SystemCommands.Add(new CLICMDS("makebankaccount","makebankaccount"));
			SystemCommands.Add(new CLICMDS("selectbankaccount","selectbankaccount"));
			SystemCommands.Add(new CLICMDS("websitefilecount","websitefilecount"));
			SystemCommands.Add(new CLICMDS("missionlistcount","missionlistcount"));
			SystemCommands.Add(new CLICMDS("missioncount","missioncount"));
			SystemCommands.Add(new CLICMDS("missioncompliercheck","missioncompliercheck"));
			SystemCommands.Add(new CLICMDS("missiongencheck","missiongencheck"));
			SystemCommands.Add(new CLICMDS("setmissiongencheck","setmissiongencheck"));
			SystemCommands.Add(new CLICMDS("version","version"));
			SystemCommands.Add(new CLICMDS("errorlog","errorlog"));
			SystemCommands.Add(new CLICMDS("viewrep","viewrep"));
			SystemCommands.Add(new CLICMDS("accountinfo","accountinfo"));
			SystemCommands.Add(new CLICMDS("doubleclickdelay","doubleclickdelay"));
			SystemCommands.Add(new CLICMDS("printinfo","writeinfo"));
			SystemCommands.Add(new CLICMDS("syscrash","syscrash"));
            SystemCommands.Add(new CLICMDS("url", "url"));

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

	void ProgramExecute()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
		{
			if (GameControl.control.ProgramFiles[i].Name == ParseArray[1]) 
			{
				if (twd == GameControl.control.ProgramFiles [i].Location) 
				{
					appman.enabled = true;
					appman.SelectedApp = GameControl.control.ProgramFiles[i].Target;
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

	void PasteFile()
	{
		TempCopy.Location = twd;
		TempCopy.Target = TempCopy.Location + "/" + TempCopy.Name;
		GameControl.control.ProgramFiles.Add(TempCopy);
	}

	void MoveFile()
	{
		
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
					fu.ProgramHandle.Add (new FileUtilitySystem ("Download", FileName, FileLocation,"", "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Download));
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
			PastCommands.Add ("#" + i + "\t" + GameControl.control.Commands [i].Name + "\t" + GameControl.control.Commands [i].Func);
		}
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
		ParseArray = Parse.Split('^');
		ParseArrayLength = ParseArray.Length;

		if (GameControl.control.Commands.Count > 0)
		{
			for (int i = 0; i < GameControl.control.Commands.Count; i++) 
			{
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
		case"file1":
			GameControl.control.ProgramFiles.Add(new ProgramSystem ("Exe1", "", "", "", "D:/", "", 0,0, 25,0, 100, 1, false, ProgramSystem.ProgramType.Exe));
			break;

//		case"alias":
//			NameChange();
//			break;

		case"aql":
			ProgramQuickLaunch();
			break;

		case"syscrash":
			SCM.StopCodeWord = "MANUAL_LAUNCH_CRASH";
			SCM.StopCodeNumber = "0xD34DD13D";
			SCM.CodeDetail = "K3RN31-94N1C-C11-U23R-D3F";
			SCM.ExtraDetail = "14M-7H3-D327R0Y3R-0F-7H12-02";
			SCM.enabled = true;
			Desktops.SetActive (false);
			break;

        case "url":
            if (inputArray[1] != "")
            {
                Application.OpenURL(inputArray[1]);
            }
            else
            {
                PastCommands.Add("error: address cannot be found.");
            }
            break;

		case"screenshot":
			//StartCoroutine(screenshot.ScreenshotEncode());
			break;

		case"writeinfo":
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
			if (inputArray [1] != "" && inputArray [1] != "-help") 
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
			if (inputArray [1] == "-help") 
			{
				PastCommands.Add ("[type -r or -l][cmd][directory]");
				PastCommands.Add ("example");
				PastCommands.Add ("-l cd F:/Programs");
			}
			break;

		case "pwd":
			if (inputArray.Length >=2) 
			{
				if (inputArray [1] == "-help")
				{
					PastCommands.Add ("[type -r or -l][cmd]");
					PastCommands.Add ("example");
					PastCommands.Add ("-l pwd [out]");
				} 
			}
			PastCommands.Add ("CLI Working Directory: " + twd);
			break;

		case "enablemusicplayer":
			mp.enabled = true;
			PastCommands.Add ("Music Player has been enabled");
			PastCommands.Add ("Warnning may cause longer loading times");
			PastCommands.Add ("Or more stuttering while loading songs");
			PastCommands.Add ("Only use .wav files");
			break;

		case "errorlog":
			mp.enabled = true;
			PastCommands.Add ("All commands with -l are broken");
			PastCommands.Add ("Screenshot command wont work");
			break;

		case "version":
			if (vl.VersionLines.Count == 0)
			{
				vl.AddPasswordsList ();
			}

			for(int i = 0; i < vl.VersionLines.Count; i++)
			{
				PastCommands.Add(vl.VersionLines [i]);
			}
			break;

		case "listcommands":
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

		case"del":
			CLIDeleteFile();
			break;

//		case "copy":
//			CopyFile();
//			break;
//
//		case "paste":
//			PasteFile();
//			break;
//
//		case "move":
//			MoveFile();
//			break;

		case"mkdir":
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
					PastCommands.Add (GameControl.control.ProgramFiles [i].Name);
				}
			}
			break;

		case "accountinfo":
			PastCommands.Add ("Serial Key: " + GameControl.control.SerialKey);
			PastCommands.Add ("Account Name: " + GameControl.control.ProfileName);
			break;

		case "listprograms":
			PrintPrograms();
			break;

		case "missioncompliercheck":
			PastCommands.Add ("Complier status: " + misgen.ComplierDone);
			break;

		case "setmissiongencheck":
			misgen.startGen = true;
			PastCommands.Add ("Mission Generation Check: " + misgen.startGen);
			break;

		case "missiongencheck":
			PastCommands.Add ("Mission Generation Check: " + misgen.startGen);
			break;

		case "missioncount":
			PastCommands.Add ("Total Mission count: " + misgen.Count);
			break;

		case "missionlistcount":
			PastCommands.Add ("Total Missions List: " + misgen.MissionList.Count);
			PastCommands.Add ("Total Missions Names: " + misgen.MissionList.Count);
			break;

		case "websitefilecount":
			PastCommands.Add ("Website Total Files: " + GameControl.control.WebsiteFiles.Count);
			break;

		case "run":
			ProgramExecute();
		break;

		case"open":
			OpenFile();
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
					for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
					{
						if (GameControl.control.ProgramFiles[Index].Name == inputArray[2])
						{
							FileIndex = Index;
							FileName = GameControl.control.ProgramFiles[Index].Name;
							FileSize = GameControl.control.ProgramFiles[Index].Used;
						}
					}
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
		
		case "godmode":
			GameControl.control.QuickProgramList.Add(new ProgramSystem("Password Breaker","","","","C:/Programs","Password Cracker",0,0,2,0,100,1,false,ProgramSystem.ProgramType.Exe));
			PastCommands.Add ("Added Progitive to system files");
			PastCommands.Add ("Added Progitive to quick list");

			GameControl.control.QuickProgramList.Add(new ProgramSystem("Dictionary Cracker","","","","C:/Programs","Dictionary Cracker",0,0,2,0,100,1,false,ProgramSystem.ProgramType.Exe));
			PastCommands.Add ("Added Dictionary to system files");
			PastCommands.Add ("Added Dictionary to quick list");

			GameControl.control.QuickProgramList.Add(new ProgramSystem("Trace Tracker","","","","C:/Programs","Trace Tracker",0,0,2,0,100,1,false,ProgramSystem.ProgramType.Exe));
			PastCommands.Add ("Added Tracer to system files");
			PastCommands.Add ("Added Tracer to quick list");

			GameControl.control.QuickProgramList.Add(new ProgramSystem("System Map","","","","C:/Programs","System Map",0,0,2,0,100,1,false,ProgramSystem.ProgramType.Exe));
			PastCommands.Add ("Added Map to system files");
			PastCommands.Add ("Added Map to quick list");

			sc.SoundSelect = 6;
			sc.PlaySound();
			break;

//		case "autoscroll":
//			switch (inputArray [1]) 
//			{
//			case "0":
//				AutoScroll = false;
//				break;
//
//			case "1":
//				AutoScroll = true;
//				break;
//			}
//			break;

		case "clear":
			PastCommands.RemoveRange(0,PastCommands.Count);
//			if (GameControl.control.Commands.Count > 0)
//			{
//				for (int i = 0; i < SystemCommands.Count; i++) 
//				{
//					if (PastCommands.Count == 0) 
//					{
//						if (SystemCommands [i].Func == "help") 
//						{
//							PastCommands.Add ("Type " + SystemCommands[i].Name + " for the list of system commands");
//						}
//					}
//				}
//			}
			break;

		case "makebankaccount":
			GameControl.control.MyBankDetails.Add (new BankSystem("192.168.56.91","LEC Bank","Dev","1337","a",0,1,0,0,1));
			PastCommands.Add ("Bank account created");
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


		case "setbalance":
			if (inputArray [1] != "")
			{
				int money = 0;
				int.TryParse (inputArray [1],out money);
				if (GameControl.control.MyBankDetails.Count > 0)
				{
					if (GameControl.control.SelectedBank > GameControl.control.MyBankDetails.Count) 
					{
						PastCommands.Add ("ERROR: Selected Account does not exist");
					} 
					else
					{
						GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance = money;
						PastCommands.Add ("Bank Balance is now " + money);
					}
				} 
				else 
				{
					PastCommands.Add ("ERROR: No bank accounts exist");
				}
				//SetFontColor();
			}
			break;

		case "motherload":
			if (GameControl.control.MyBankDetails.Count > 0)
			{
				if (GameControl.control.SelectedBank > GameControl.control.MyBankDetails.Count) 
				{
					PastCommands.Add ("ERROR: Selected Account does not exist");
				} 
				else
				{
					GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance += 50000;
					PastCommands.Add ("Added $50,000 to your selected bank account");
				}
			} 
			else 
			{
				PastCommands.Add ("ERROR: No bank accounts exist");
			}
			break;

		case "setreplevel":
			if (inputArray [1] != "")
			{
				int replevel = 0;
				int.TryParse (inputArray [1],out replevel);
				GameControl.control.Rep[0].RepLevel = replevel;
				PastCommands.Add ("Reputation Level now " + replevel);
				//SetFontColor();
			}
			break;

		case "help":
			PastCommands.Add (" ");
			PastCommands.Add ("Working Commands");
			PastCommands.Add ("clear");
			PastCommands.Add ("auto_scroll [0-1]");
			PastCommands.Add ("run [name]");
			PastCommands.Add ("systeminfo");
			PastCommands.Add ("reboot");
			PastCommands.Add ("time");
			PastCommands.Add ("volume [0-100]");
			PastCommands.Add (" ");

			PastCommands.Add ("NEW COMMANDS");
			PastCommands.Add ("-r = Parse remote command");
			PastCommands.Add ("-l = Parse local command");
			PastCommands.Add ("ls = List current page contents");
			PastCommands.Add ("cd = Change working directory");
			PastCommands.Add ("pwd = Print working directory");
			PastCommands.Add ("rm = Remove/Delete files");
			PastCommands.Add ("ul = Upload file from local");
			PastCommands.Add ("dl = Download file from remote");
			PastCommands.Add ("[cmd] -help = To get more info on that command");
			PastCommands.Add ("mkdir = Make a new local directory");
			PastCommands.Add ("copy = Duplicates a local file");
			PastCommands.Add ("paste = pastes a copied file to a local directory");
			PastCommands.Add (" ");

			PastCommands.Add ("THEME");
			PastCommands.Add ("color [text|buttons|windows] [red|green|blue] [0-255]");
			PastCommands.Add ("background [address]");
			PastCommands.Add (" ");

			PastCommands.Add ("Dev Commands");
			PastCommands.Add ("error[1,2,3]");
			PastCommands.Add ("sound");
			PastCommands.Add ("noti[1,2][message]");
			PastCommands.Add (" ");

			PastCommands.Add ("Planned Commands");
			PastCommands.Add ("cd.open - redesigning");
			PastCommands.Add ("cd.close - redesigning");
			PastCommands.Add ("connect - redesigning");
			PastCommands.Add ("ui.skin [color] - redesigning");
			PastCommands.Add ("username - redesigning");
			PastCommands.Add ("password - redesigning");
			PastCommands.Add ("login - redesigning");
			PastCommands.Add ("dc - redesigning");
			PastCommands.Add (" ");

			PastCommands.Add ("Local Commands");
			PastCommands.Add ("[] = input arrays [1] [2]");
			PastCommands.Add ("list = [-l][ls]");
			PastCommands.Add ("pwd = [-l][pwd]");
			PastCommands.Add ("cd = [-l][cd][new directory]");
			PastCommands.Add ("copy = [-l][copy][file Name]");
			PastCommands.Add ("paste = [-l][paste][file Name][file Location]");
			PastCommands.Add ("delete file = [-l][rm][file Name][file Location]");
			PastCommands.Add ("new folder = [-l][mkdir][folder Name][folder Location]");
			PastCommands.Add (" ");

			PastCommands.Add ("Remote Commands");
			PastCommands.Add ("[] = input arrays [1] [2]");
			PastCommands.Add ("list = [-r][ls]");
			PastCommands.Add ("pwd = [-r][pwd]");
			PastCommands.Add ("cd = [-r][cd][new url]");
			PastCommands.Add ("upload = [ul][filename]");
			PastCommands.Add ("download = [dl][filename]");
			PastCommands.Add ("delete file = [-r][rm][file Name][file Location]");
			PastCommands.Add (" ");

			PastCommands.Add ("NOTES");
			PastCommands.Add ("ls command needs rework");
			PastCommands.Add ("pwd display Current working directory");
			PastCommands.Add ("cd display past to new directory");
			PastCommands.Add (" ");
			break;

		case "time":
			//PastCommands.Add ("" + ProfileController.procon.Hour + ":" + ProfileController.procon.Min.ToString("F0"));
			//PastCommands.Add ("" + ProfileController.procon.Day + "/" + ProfileController.procon.Month + "/" + ProfileController.procon.Year);
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

		case "color":
			switch (inputArray [1]) 
			{
			case "-help":
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

		case "reboot":
			PastCommands.Add ("Rebooting is disabled");
			break;

		case "error1":
			PastCommands.Add ("Displayed an error");
			ep.SoundSelect = 0;
			ep.playsound = true;
			ep.ErrorTitle = "Genric Error - 343";
			ep.ErrorMsg = "This is a generic user error from Command Prompt";
            appman.SelectedApp = "Error Prompt";
			break;

		case "error2":
			PastCommands.Add ("Displayed an error");
			ep.enabled = true;
			ep.show = true;
			ep.SoundSelect = 9;
			ep.playsound = true;
			ep.ErrorTitle = "Genric Error - 343";
			ep.ErrorMsg = "This is a generic user error from Command Prompt";
			break;

		case "error3":
			PastCommands.Add ("Displayed an error");
			ep.enabled = true;
			ep.show = true;
			ep.SoundSelect = 10;
			ep.playsound = true;
			ep.ErrorTitle = "Genric Error - 343";
			ep.ErrorMsg = "This is a generic user error from Command Prompt";
			break;

		case "noti1":
			noti.ShowNoti = true;
			noti.Notification = CommandLine;
			if (Customize.cust.PlayNotiSound == false)
			{
				Customize.cust.PlayNotiSound = true;
			}
			noti.playsound = true;
			noti.DisplayTime = 5;
			break;

		case "noti2":
			noti.ShowNoti = true;
			noti.Notification = CommandLine;
			if (Customize.cust.PlayNotiSound == false)
			{
				Customize.cust.PlayNotiSound = true;
			}
			noti.playsound = true;
			noti.DisplayTime = 5;
			break;

		case "background":
			if (inputArray [1] == "-help")
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