using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class FileExplorer : MonoBehaviour 
{
	GameObject Prompts;

    public float native_width = 1920;
    public float native_height = 1080;
	public Rect windowRect = new Rect (100, 100, 300, 200);
	public Rect ContextwindowRect = new Rect (100, 100, 300, 200);
    public int windowID;
    public bool Drag;
    public bool show;
    public int GUIID;
    public string ComAddress;
    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;
	public int Res;
	public List<Texture2D> Icon = new List<Texture2D>();
	public List<Texture2D> IconHighlight = new List<Texture2D>();
    public bool Delete;
    public bool Copy;
    public bool Install;
    public bool Uninstall;
	public int Select;
	public bool FullScreen;
	public string Hint;
	public bool showPics;
	public bool showName;
	public bool showPass;
	public string TempName;
	public string TempPass;

	private GameObject apps;

	private ErrorProm ep;
	private CustomTheme ct;
	private Defalt def;
	private InstallPrompt ip;
	private AppMan appman;
	private ConfirmPrompt cp;
	private CLICommandsV2 clic;
	private Notepadv2 note;
	private Computer com;

	private SoundControl sc;
	private FileUtility fu;

	public float w;
	public float h;

	public Color32 rgb;

	public float RemainingHDD;

	public string SkinName;
	public string SSTimeString;

	public List<string> Rez = new List<string>();

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	public bool minimize;

	public string TitleName;

    public string Program;

	public string SpaceName;
	public float MaxSpace;
	public float FreeSpace;

	public int SelectedProgram;

	public List<ProgramSystem> PageFile = new List<ProgramSystem>();
	public List<string> History = new List<string>();

	public int PosX;
	public int slotsX, slotsY;
	public string[] SplitDirectory;
	public int Count;
	public bool OneClick = false;
	public bool DoubleClick = false;
	public float Delay;
	public float LastClick;

	public float Cooldown;
	public float cd;

	public float cd1;

	public int MenuSelected;
	public string FolderName;
	public bool ShowNewFolder;

	public string OldName;
	public string NewName;

	public string OldFileLocation;
	public string NewFileLocation;

	public int FileIndex;
	public string FileName;
	public float FileSize;

	public bool Focused;

	public float ScrollposY;

	public float DisplayedItems;

	public bool Open;
	public int OpenFile;

	public bool ShowContext;

	public int ContextMenuID;

	public string QuickListName;

	public int contextMenuCount;

	public float Timer;
	public float Cooldown2;
	public bool StartTime;

	public List<string> ContextMenuOptions = new List<string>();

	public string SelectedOption;

	public bool ChangeFocus;

	public string SelectedFileType;
	public string SelectedFolderLocation;

	public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
	public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

	void Start()
    {
        SetPos();

        Prompts = GameObject.Find("Prompts");
		apps = GameObject.Find("Applications");

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		ct = GetComponent<CustomTheme>();
		def = GetComponent<Defalt>();
		sc = GetComponent<SoundControl>();
		clic = GetComponent<CLICommandsV2>();
		appman = GetComponent<AppMan>();
		fu = GetComponent<FileUtility>();
		note = apps.GetComponent<Notepadv2>();

		com = GetComponent<Computer>();

		cp = Prompts.GetComponent<ConfirmPrompt>();
		ip = Prompts.GetComponent<InstallPrompt>();
		ep = Prompts.GetComponent<ErrorProm>();

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		ComAddress = "Gateway";

		SelectedProgram = -1;

		windowRect.y = 30;

		ContextwindowRect.width = 100;
    }

	void SetPos()
	{
		CloseButton = new Rect (277,1,22,20);
		MiniButton = new Rect (256,1,21,20);
		DefaltSetting = new Rect (0,1,300,250);
		DefaltBoxSetting = new Rect (1,1,255,20);
        windowRect.height = DefaltSetting.height;

    }

    void Update()
    {
		w = GameControl.control.wh;
		h = GameControl.control.wh;

		if (GameControl.control.wh <= 0)
		{
			GameControl.control.wh = 1;
		}
    }

	void PlayClickSound()
	{
		sc.SoundSelect = 3;
		sc.PlaySound();
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

		if (ChangeFocus == true) 
		{
			GUI.BringWindowToFront(windowID);
			GUI.FocusWindow (windowID);
			ChangeFocus = false;
		}

		GUI.skin = com.Skin[GameControl.control.GUIID];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1));
		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
			//windowRect.width = 300 * w;
			//windowRect.height = 200 * h;
		}

		if (ShowContext == true) 
		{
			ContextwindowRect.height = 21*ContextMenuOptions.Count+2;
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			ContextwindowRect = WindowClamp.ClampToScreen(GUI.Window(ContextMenuID,ContextwindowRect,DoMyContextWindow,""));
		}

		if (Input.GetMouseButtonUp (0) || Input.GetMouseButtonUp (1) || Input.GetMouseButtonUp (2))
		{
			if (!ContextwindowRect.Contains (Event.current.mousePosition)) 
			{
				CloseContextMenu();
			} 
		}
	}

	public void DeleteFile()
	{
		clic.CommandLine = "-l▓rm▓" + PageFile[SelectedProgram].Name + "▓" + PageFile[SelectedProgram].Location;
		clic.CheckInput();
		clic.CommandLine = "";
	}

	public void AddNewFolder()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (GameControl.control.ProgramFiles [i].Name == FolderName)
			{
				if (GameControl.control.ProgramFiles [i].Location == ComAddress)
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
		if (FileIndex != -1) 
		{
			if (ComAddress.Length > 3)
			{
				GameControl.control.ProgramFiles.Add(new ProgramSystem(FolderName, "", "", "", "", "", ComAddress, "" + ComAddress + "/" + FolderName, "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 0, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
			} 
			else 
			{
				GameControl.control.ProgramFiles.Add(new ProgramSystem(FolderName, "", "", "", "", "", ComAddress, "" + ComAddress + "" + FolderName, "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 0, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
			}
			FolderName = "";
			MenuSelected = 0;
			SelectedProgram = -1;
		}
	}

	void FileRename()
	{
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		{
			if (GameControl.control.ProgramFiles [Index].Name == OldName) 
			{
				if (GameControl.control.ProgramFiles [Index].Extension == ProgramSystem.FileExtension.Fdl) 
				{
					OldFileLocation = GameControl.control.ProgramFiles[Index].Target;

					GameControl.control.ProgramFiles [Index].Name = NewName;

					if (ComAddress.Length > 3)
					{
						GameControl.control.ProgramFiles [Index].Target = ComAddress + "/" + NewName + "";
						NewFileLocation = GameControl.control.ProgramFiles [Index].Target;
					} 
					else
					{
						GameControl.control.ProgramFiles [Index].Target = "" + ComAddress + NewName + "";
						NewFileLocation = GameControl.control.ProgramFiles [Index].Target;
					}
					UpdateFileLocations();
				}
				else 
				{
					GameControl.control.ProgramFiles [Index].Name = NewName;
					NewName = "";
					OldName = "";
					MenuSelected = 0;
					SelectedProgram = -1;
				}
			}
		}
	}

	void UpdateFileLocations()
	{
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++)
		{
			if (GameControl.control.ProgramFiles [Index].Location == OldFileLocation) 
			{
				GameControl.control.ProgramFiles [Index].Location = NewFileLocation;
			}
		}
		NewName = "";
		OldName = "";
		OldFileLocation = "";
		NewFileLocation = "";
		MenuSelected = 0;
		SelectedProgram = -1;
	}

	void DeleteSystem()
	{
		cp.enabled = true;
		cp.show = true;
		cp.ErrorTitle = "Delete File";
		cp.ErrorMsg = "Are you sure you want to delete " + PageFile[SelectedProgram].Name;
		cp.InitalWindowID = windowID;
	}

	void CopySystem()
	{
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		{
			if (GameControl.control.ProgramFiles[Index].Name == PageFile[SelectedProgram].Name)
			{
				FileIndex = Index;
				FileName = GameControl.control.ProgramFiles[Index].Name;
				FileSize = GameControl.control.ProgramFiles[Index].Used;
			}
		}
	}

	//void PasteSystem()
	//{
	//	if (fu.ProgramHandle.Count <= 0) 
	//	{
	//		if (FileIndex != -1) 
	//		{
	//			string FileLocation = ComAddress;
	//			fu.ProgramHandle.Add (new FileUtilitySystem ("Paste", FileName, FileLocation,"", "","","", false, true, true, false, 0,0, 0, 0, 0, 0, 0, 0, FileSize, 0, 0, 0, FileUtilitySystem.ProgramType.Paste));
	//			fu.AddWindow ();
	//		}
	//	} 
	//}

	void Refresh()
	{
		PageFile.RemoveRange (0, PageFile.Count);
		for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++) 
		{
			if (GameControl.control.ProgramFiles [Index].Location == ComAddress) 
			{
				if (!PageFile.Contains (GameControl.control.ProgramFiles[Index])) 
				{
					PageFile.Add (GameControl.control.ProgramFiles [Index]);
				}
			}
		}
	}

	void OpenExe()
	{
		PlayClickSound ();
		appman.SelectedApp = PageFile [SelectedProgram].Target;
		SelectedProgram = -1;
	}

	//void OpenTxt()
	//{
	//	PlayClickSound ();
	//	for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
	//	{
	//		if(PageFile [SelectedProgram].Name == GameControl.control.ProgramFiles[i].Name && PageFile [SelectedProgram].Location == GameControl.control.ProgramFiles[i].Location)
	//		{
	//			note.SelectedDocument = i;
	//		}
	//	}
	//	if (note.show == true && note.enabled == true)
	//	{
	//		note.CurrentWorkingTitle = PageFile [SelectedProgram].Name;
	//		note.TypedTitle = PageFile [SelectedProgram].Name;
	//		note.TypedText = PageFile [SelectedProgram].Content;
	//		note.SaveLocation = PageFile [SelectedProgram].Location;
	//		note.ShowFileContent = true;
	//	}
	//	if (note.show == false || note.enabled == false) 
	//	{
	//		appman.SelectedApp = "Notepad";
	//		note.CurrentWorkingTitle = PageFile [SelectedProgram].Name;
	//		note.TypedTitle = PageFile [SelectedProgram].Name;
	//		note.TypedText = PageFile [SelectedProgram].Content;
	//		note.SaveLocation = PageFile [SelectedProgram].Location;
	//		note.ShowFileContent = true;
	//	}
	//	SelectedProgram = -1;
	//	appman.SelectedApp = "File Explorer";
	//	SelectedFileType = "";
	//}

	void OpenTxt()
	{
		PlayClickSound();
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (PageFile[SelectedProgram].Name == GameControl.control.ProgramFiles[i].Name && PageFile[SelectedProgram].Location == GameControl.control.ProgramFiles[i].Location)
			{
				appman.selecteddocument = i;
			}
		}
		if (appman.isDocumentReadingRunning == true)
		{
			ReadText(PageFile[SelectedProgram].Name, PageFile[SelectedProgram].Content, PageFile[SelectedProgram].Location);
		}
		if (appman.isDocumentReadingRunning == false)
		{
			for (int i = 0; i < GameControl.control.DefaultLaunchedPrograms.Count; i++)
			{
				for (int j = 0; j < GameControl.control.DefaultLaunchedPrograms[i].Type.Count; j++)
				{
					if (GameControl.control.DefaultLaunchedPrograms[i].Type[j] == ProgramSystem.FileType.DocumentReaders)
					{
						appman.SelectedApp = GameControl.control.DefaultLaunchedPrograms[i].Target;
					}
				}
			}
			ReadText(PageFile[SelectedProgram].Name, PageFile[SelectedProgram].Content, PageFile[SelectedProgram].Location);
		}
		SelectedProgram = -1;
		appman.SelectedApp = "File Explorer";
		SelectedFileType = "";
	}

	void ReadText(string filename, string content, string location)
	{
		appman.filename = filename;
		appman.content = content;
		appman.location = location;
		appman.showfilecontent = true;
	}

	void OpenIns()
	{
		//PlayClickSound ();
		//ip.enabled = true;
		//ip.show = true;
		//ip.Install = true;
		//ip.ProgramName = PageFile[SelectedProgram].Name;
		//ip.Size = PageFile[SelectedProgram].Used;
		//ip.ProgramVersion = PageFile[SelectedProgram].Version;
		//ip.ProgramTarget = PageFile[SelectedProgram].Target;
		//ip.ErrorTitle = ip.ProgramName + " Setup";
		//ip.ErrorMsg = "Do you wish to execute this file?" + "\n" +
		//	"Name: " + ip.ProgramName + "_setup.inspkg";
		//ip.ConfirmationMsg = "Install";
	}

	void OpenFld()
	{
		PlayClickSound ();
		History.Add (ComAddress);
		SelectedFolderLocation = PageFile[SelectedProgram].Target;
		ComAddress = PageFile [SelectedProgram].Target;
		PageFile.RemoveRange (0, PageFile.Count);
		SelectedProgram = -1;
		MenuSelected = 0;
	}

	void CreateIcon()
	{
		GameControl.control.DesktopIconList.Add (PageFile [SelectedProgram]);
	}

	void CreateQLI()
	{
		if (GameControl.control.QuickProgramList.Count > 0)
		{
			if (!GameControl.control.QuickLaunchNames.Contains (PageFile[SelectedProgram].Name))
			{
				GameControl.control.QuickProgramList.Add (PageFile[SelectedProgram]);
				GameControl.control.QuickLaunchNames.Add(PageFile[SelectedProgram].Name);
			} 
			else 
			{
				GameControl.control.QuickProgramList.Remove (PageFile[SelectedProgram]);
				GameControl.control.QuickLaunchNames.Remove(PageFile[SelectedProgram].Name);
			}
		} 
		else 
		{
			GameControl.control.QuickProgramList.Add (PageFile[SelectedProgram]);
			GameControl.control.QuickLaunchNames.Add(PageFile[SelectedProgram].Name);
		}
	}

	void AddContextOptions()
	{
		if (PageFile [SelectedProgram].Extension == ProgramSystem.FileExtension.Exe)
		{
			ContextMenuOptions.Add ("Open");
			ContextMenuOptions.Add ("Copy");
			ContextMenuOptions.Add ("Rename");
			if (!GameControl.control.QuickLaunchNames.Contains (PageFile [SelectedProgram].Name)) 
			{
				ContextMenuOptions.Add ("Pin to QL");
			}
			else
			{
				ContextMenuOptions.Add ("Unpin from QL");
			}
			ContextMenuOptions.Add ("Delete");
			ContextMenuOptions.Add ("Create Icon");
			ContextMenuOptions.Add ("Details");
		}
		else if (PageFile [SelectedProgram].Extension == ProgramSystem.FileExtension.Dir)
		{
			ContextMenuOptions.Add ("Open");
			ContextMenuOptions.Add ("Create Icon");
			ContextMenuOptions.Add ("Details");
		}
		else 
		{
			ContextMenuOptions.Add ("Open");
			ContextMenuOptions.Add ("Copy");
			ContextMenuOptions.Add ("Rename");
			ContextMenuOptions.Add ("Delete");
			ContextMenuOptions.Add ("Create Icon");
			ContextMenuOptions.Add ("Details");
		}
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
			switch (PageFile [SelectedProgram].Extension) 
			{
			case ProgramSystem.FileExtension.Txt:
				OpenTxt ();
				CloseContextMenu();
				break;
			case ProgramSystem.FileExtension.Fdl:
				OpenFld();
				CloseContextMenu();
				break;
			case ProgramSystem.FileExtension.Ins:
				OpenIns();
				CloseContextMenu();
				break;
			case ProgramSystem.FileExtension.Exe:
				OpenExe();
				CloseContextMenu();
				break;
			case ProgramSystem.FileExtension.Dir:
				OpenFld();
				CloseContextMenu();
				break;
			}
		break;
		case "Copy":
			CopySystem ();
			PlayClickSound ();
			CloseContextMenu();
			break;
		case "Rename":
			RenameContextOption();
			break;
		case "Delete":
			DeleteSystem();
			PlayClickSound();
			CloseContextMenu();
			break;
		case "Create Icon":
			CreateIcon();
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
			//DeleteSystem();
			PlayClickSound();
			CloseContextMenu();
			break;
		}
	}

	void ProgramExecution()
	{
		ScrollposY = scrollpos.y;
		scrollpos = GUI.BeginScrollView(new Rect(3, 85, 290, 100), scrollpos, new Rect(0, 0, 0, scrollsize*21));
		for (scrollsize = 0; scrollsize < PageFile.Count; scrollsize++) 
		{
			if (PageFile[scrollsize].Location == ComAddress)
			{
				switch (PageFile[scrollsize].Extension)
				{
				case ProgramSystem.FileExtension.Dir:
					if (SelectedProgram == scrollsize)
					{
						GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), IconHighlight[0]);
					} 
					else
					{
						GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), Icon[0]);
					}
					if (GUI.Button(new Rect(21, 21 * scrollsize, 250, 20), "" + PageFile[scrollsize].Name + " " + "(" + PageFile[scrollsize].Sender + ")" + " " + PageFile[scrollsize].Free.ToString("F2") + " free of " + PageFile[scrollsize].Capacity) || Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return)
					{
						if (Input.GetMouseButtonUp(0))
						{
							if (Time.time - LastClick < 0.5)
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
								OpenFld();
							}
							else
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp(1))
						{
							SelectedProgram = scrollsize;
							if (new Rect(21, 21 * SelectedProgram, 200, 20).Contains(Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(ContextMenuID);
							}
						}
					}
				break;

				case ProgramSystem.FileExtension.Fdl:

					if (GUI.Button(new Rect(21, 21 * scrollsize, 100, 20), "" + PageFile[scrollsize].Name))
					{
						if (Input.GetMouseButtonUp(0))
						{
							if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu)
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
								OpenFld();
							}
							else
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp(1))
						{
							SelectedProgram = scrollsize;
							if (new Rect(21, 21 * SelectedProgram, 100, 20).Contains(Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(ContextMenuID);
							}
						}
					}

					if (SelectedProgram == scrollsize)
					{
						GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), IconHighlight[1]);
					}
					else
					{
						GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), Icon[1]);
					}

				break;

				case ProgramSystem.FileExtension.Txt:
					if (SelectedFileType == "Text")
					{
						if (SelectedProgram == scrollsize)
						{
							GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), IconHighlight[4]);
						}
						else
						{
							GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), Icon[4]);
						}

						if (GUI.Button(new Rect(21, 21 * scrollsize, 100, 20), "" + PageFile[scrollsize].Name))
						{
							if (Input.GetMouseButtonUp(0))
							{
								if (Time.time - LastClick < 0.5)
								{
									PlayClickSound();
									SelectedProgram = scrollsize;
									OpenTxt();
								}
								else
								{
									PlayClickSound();
									SelectedProgram = scrollsize;
								}
								LastClick = Time.time;
							}

							if (Input.GetMouseButtonUp(1))
							{
								SelectedProgram = scrollsize;
								if (new Rect(21, 21 * SelectedProgram, 100, 20).Contains(Event.current.mousePosition))
								{
									ContextwindowRect.x = Input.mousePosition.x;
									ContextwindowRect.y = Screen.height - Input.mousePosition.y;
									ShowContext = true;
									GUI.BringWindowToFront(ContextMenuID);
								}
							}
						}
					}
				break;

				case ProgramSystem.FileExtension.File:
					if (SelectedFileType == "File")
					{
						if (SelectedProgram == scrollsize)
						{
							GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), IconHighlight[4]);
						}
						else
						{
							GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), Icon[4]);
						}
						if (GUI.Button(new Rect(21, 21 * scrollsize, 100, 20), "" + PageFile[scrollsize].Name))
						{
							if (Input.GetMouseButtonUp(0))
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
							}
							if (Input.GetMouseButtonUp(1))
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
								if (new Rect(21, 21 * SelectedProgram, 100, 20).Contains(Event.current.mousePosition))
								{
									ContextwindowRect.x = Input.mousePosition.x;
									ContextwindowRect.y = Screen.height - Input.mousePosition.y;
									ShowContext = true;
									GUI.BringWindowToFront(ContextMenuID);
								}
							}
						}
						if (GUI.Button(new Rect(122, 21 * scrollsize, 50, 20), "" + PageFile[scrollsize].Used))
						{
							PlayClickSound();
							SelectedProgram = scrollsize;
						}
						if (GUI.Button(new Rect(173, 21 * scrollsize, 50, 20), "" + PageFile[scrollsize].Type))
						{
							PlayClickSound();
							SelectedProgram = scrollsize;
						}
					}
					break;

				case ProgramSystem.FileExtension.Exe:
				if (SelectedFileType == "Exe")
				{
					if (SelectedProgram == scrollsize)
					{
						GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), IconHighlight[2]);
					}
					else
					{
						GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), Icon[2]);
					}
					if (GUI.Button(new Rect(21, 21 * scrollsize, 100, 20), "" + PageFile[scrollsize].Name))
					{
						if (Input.GetMouseButtonUp(0))
						{
							if (Time.time - LastClick < 0.5)
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
								OpenExe();
							}
							else
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
							}
							LastClick = Time.time;
						}
						if (Input.GetMouseButtonUp(1))
						{
							SelectedProgram = scrollsize;
							if (new Rect(21, 21 * SelectedProgram, 100, 20).Contains(Event.current.mousePosition))
							{
								ContextwindowRect.x = Input.mousePosition.x;
								ContextwindowRect.y = Screen.height - Input.mousePosition.y;
								ShowContext = true;
								GUI.BringWindowToFront(ContextMenuID);
							}
						}
					}
					if (GUI.Button(new Rect(122, 21 * scrollsize, 50, 20), "" + PageFile[scrollsize].Used))
					{
						if (Time.time - LastClick < 0.5)
						{
							PlayClickSound();
							SelectedProgram = scrollsize;
							appman.SelectedApp = PageFile[SelectedProgram].Target;
							SelectedProgram = -1;
						}
						else
						{
							PlayClickSound();
							SelectedProgram = scrollsize;
						}
						LastClick = Time.time;
					}
					if (GUI.Button(new Rect(173, 21 * scrollsize, 50, 20), "" + PageFile[scrollsize].Version))
					{
						if (Time.time - LastClick < 0.5)
						{
							PlayClickSound();
							SelectedProgram = scrollsize;
							appman.SelectedApp = PageFile[SelectedProgram].Target;
							SelectedProgram = -1;
						}
						else
						{
							PlayClickSound();
							SelectedProgram = scrollsize;
						}
						LastClick = Time.time;
					}
				}
				break;

				case ProgramSystem.FileExtension.OS:
					if (SelectedFileType == "OS")
					{
						GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), Icon[3]);
						if (GUI.Button(new Rect(21, 21 * scrollsize, 100, 20), "" + PageFile[scrollsize].Name))
						{

						}
						if (GUI.Button(new Rect(122, 21 * scrollsize, 50, 20), "" + PageFile[scrollsize].Used))
						{

						}
						if (GUI.Button(new Rect(173, 21 * scrollsize, 50, 20), "" + PageFile[scrollsize].Version))
						{

						}
					}
				break;

				case ProgramSystem.FileExtension.Ins:
					if (SelectedFileType == "Ins")
					{
						GUI.DrawTexture(new Rect(0, 21 * scrollsize, 20, 20), Icon[3]);
						if (GUI.Button(new Rect(21, 21 * scrollsize, 100, 20), "" + PageFile[scrollsize].Name))
						{
							if (Input.GetMouseButtonUp(0))
							{
								if (new Rect(21, 21 * SelectedProgram, 100, 20).Contains(Event.current.mousePosition))
								{
									if (Time.time - LastClick < 0.5)
									{
										PlayClickSound();
										SelectedProgram = scrollsize;
										OpenIns();

									}
									else
									{
										PlayClickSound();
										SelectedProgram = scrollsize;
									}
									LastClick = Time.time;
								}
							}
							if (Input.GetMouseButtonUp(1))
							{
								SelectedProgram = scrollsize;
								if (new Rect(21, 21 * SelectedProgram, 100, 20).Contains(Event.current.mousePosition))
								{
									ContextwindowRect.x = Input.mousePosition.x;
									ContextwindowRect.y = Screen.height - Input.mousePosition.y;
									ShowContext = true;
									GUI.BringWindowToFront(ContextMenuID);
								}
							}
						}
						if (GUI.Button(new Rect(122, 21 * scrollsize, 50, 20), "" + PageFile[scrollsize].Used))
						{
							if (Time.time - LastClick < 0.5)
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
								appman.SelectedApp = PageFile[SelectedProgram].Target;
								SelectedProgram = -1;
							}
							else
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
							}
							LastClick = Time.time;
						}
						if (GUI.Button(new Rect(173, 21 * scrollsize, 50, 20), "" + PageFile[scrollsize].Version))
						{
							if (Time.time - LastClick < 0.5)
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
								appman.SelectedApp = PageFile[SelectedProgram].Target;
								SelectedProgram = -1;
							}
							else
							{
								PlayClickSound();
								SelectedProgram = scrollsize;
							}
							LastClick = Time.time;
						}
					}
				break;
				}
			}	
		}
		GUI.EndScrollView();
	}

	public void SetFileExplorerData(string selectedFileType, string titleName, string program)
	{
		SelectedFileType = selectedFileType;
		TitleName = titleName;
		Program = program;
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
			{
				appman.SelectedApp = "File Explorer";
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]))
			{
				show = false;
			}
		}

		if (MiniButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				minimize = !minimize;
				Minimize();
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				minimize = !minimize;
				Minimize();
			}
		}

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting), TitleName);

		if (GUI.GetNameOfFocusedControl().Equals("Address"))
		{
			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.DownArrow)
			{
				//DisplayedItems = PageFile.Count / 21;
			}

			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.UpArrow)
			{

			}
		}
		else
		{
			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.DownArrow)
			{
				if (PageFile.Count > 0)
				{
					if (SelectedProgram < PageFile.Count - 1)
					{
						if (SelectedProgram >= 0)
						{
							scrollpos.y += 21;
							SelectedProgram++;
						}
					}
					if (SelectedProgram < 0)
					{
						SelectedProgram++;
					}
				}
				//DisplayedItems = PageFile.Count / 21;
			}

			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.UpArrow)
			{
				if (PageFile.Count > 0)
				{
					if (SelectedProgram < 0)
					{
						SelectedProgram = PageFile.Count;
						scrollpos.y = 21 * SelectedProgram;
					}
					if (SelectedProgram >= 1)
					{
						scrollpos.y -= 21;
						SelectedProgram--;
					}
				}
			}

			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Tab)
			{
				if (MenuSelected <= 1)
				{
					GUI.FocusControl("Address");
				}
				if (MenuSelected == 2)
				{
					GUI.FocusControl("FolderName");
				}
				if (MenuSelected == 3)
				{
					GUI.FocusControl("RenameFile");
				}
			}

			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Delete)
			{
				if (SelectedProgram >= 0)
				{
					DeleteSystem();
				}
			}
		}

		//		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.N)
		//		{
		//			MenuSelected = 2;
		//			GUI.FocusControl("FolderName");
		//		}

		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return)
		{
			if (GUI.GetNameOfFocusedControl().Equals("Address"))
			{
				SelectedProgram = -1;
				GUI.FocusControl(null);
				GUI.FocusWindow(windowID);
				//GUI.GetNameOfFocusedControl ().Equals ("");
			}
			if (GUI.GetNameOfFocusedControl().Equals("FolderName"))
			{
				GUI.FocusControl(null);
				GUI.FocusWindow(windowID);
				AddNewFolder();
				SelectedProgram = -1;
				//GUI.GetNameOfFocusedControl ().Equals ("");
			}
			else if (GUI.GetNameOfFocusedControl().Equals("RenameFile"))
			{
				GUI.FocusControl(null);
				GUI.FocusWindow(windowID);
				FileRename();
				SelectedProgram = -1;
				//GUI.GetNameOfFocusedControl ().Equals ("");
			}
			else
			{
				if (SelectedProgram >= 0)
				{
					if (PageFile[SelectedProgram].Extension == ProgramSystem.FileExtension.Exe)
					{
						PlayClickSound();
						appman.SelectedApp = PageFile[SelectedProgram].Target;
						SelectedProgram = -1;
					}
					if (PageFile[SelectedProgram].Extension == ProgramSystem.FileExtension.Fdl)
					{
						PlayClickSound();
						History.Add(ComAddress);
						ComAddress = PageFile[SelectedProgram].Target;
						PageFile.RemoveRange(0, PageFile.Count);
						SelectedProgram = -1;
						MenuSelected = 0;
					}
					if (PageFile[SelectedProgram].Extension == ProgramSystem.FileExtension.Dir)
					{
						PlayClickSound();
						History.Add(ComAddress);
						ComAddress = PageFile[SelectedProgram].Target;
						PageFile.RemoveRange(0, PageFile.Count);
						SelectedProgram = -1;
						MenuSelected = 0;
					}
					if (PageFile[SelectedProgram].Extension == ProgramSystem.FileExtension.Txt)
					{
						PlayClickSound();
						OpenTxt();
						SelectedProgram = -1;
					}
				}
			}
		}

        if (TitleName == "Save As")
        {
            note.SaveLocation = ComAddress;
            note.CurrentWorkingTitle = note.TypedTitle;
            GUI.Label(new Rect(2, windowRect.height - 25, 80, 22), "File Name: ");
            note.TypedTitle = GUI.TextField(new Rect(75, windowRect.height - 25, 150, 22), note.TypedTitle);
            if (GUI.Button(new Rect(230, windowRect.height - 25, 50, 22), "Save") || Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return)
            {
                if (note.TypedTitle != "")
                {
                    if (note.CurrentWorkingTitle != "")
                    {
                        if (note.SaveLocation != "")
                        {
                            note.Save();
                            appman.SelectedApp = "File Explorer";
                        }
                    }
                }
            }
        }


        ToolBar();

		if (MenuSelected < 2)
		{
			if (ComAddress != "Gateway")
			{
				if (SelectedProgram >= 0)
				{
					GUI.SetNextControlName("Address");
					ComAddress = GUI.TextField(new Rect(23, 44, 200, 20), ComAddress, 100);
					if (GUI.Button(new Rect(2, 44, 20, 20), "<-") || Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Backspace)
					{
						if (History.Count == 0)
						{
							ComAddress = "Gateway";
						}
						Count = History.Count - 1;
						if (Count == 0)
						{
							ComAddress = "Gateway";
							History.RemoveAt(Count);
							SelectedProgram = -1;
							MenuSelected = 0;
						}
						else
						{
							ComAddress = History[Count];
							History.RemoveAt(Count);
							SelectedProgram = -1;
							MenuSelected = 0;
						}
						PlayClickSound();
						PageFile.RemoveRange(0, PageFile.Count);
					}
				}
				else
				{
					GUI.SetNextControlName("Address");
					ComAddress = GUI.TextField(new Rect(23, 44, 200, 20), ComAddress, 100);
					if (GUI.Button(new Rect(2, 44, 20, 20), "<-") || Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Backspace)
					{
						if (History.Count == 0)
						{
							ComAddress = "Gateway";
						}
						Count = History.Count - 1;
						if (Count == 0)
						{
							ComAddress = "Gateway";
							History.RemoveAt(Count);
							SelectedProgram = -1;
							MenuSelected = 0;
						}
						else
						{
							ComAddress = History[Count];
							History.RemoveAt(Count);
							SelectedProgram = -1;
							MenuSelected = 0;
						}
						PlayClickSound();
						PageFile.RemoveRange(0, PageFile.Count);
					}
				}
			}
			else
			{
				if (SelectedProgram >= 0)
				{
					GUI.SetNextControlName("Address");
					ComAddress = GUI.TextField(new Rect(2, 44, 200, 20), ComAddress, 100);
				}
				else
				{
					GUI.SetNextControlName("Address");
					ComAddress = GUI.TextField(new Rect(2, 23, 200, 20), ComAddress, 100);
				}
			}
		}


		if (PageFile.Count > 0)
		{
			ProgramExecution();
		}

		Refresh();

		if (ComAddress == "C:/Downloads")
		{
			GUI.Label(new Rect(25, 63, 500, 500), "Name");
			GUI.Label(new Rect(133, 63, 500, 500), "Size");
			GUI.Label(new Rect(175, 63, 500, 500), "Version");
		}

		if (ComAddress == "C:/Documents")
		{
			GUI.Label(new Rect(25, 63, 500, 500), "Name");
			GUI.Label(new Rect(133, 63, 500, 500), "Size");
			GUI.Label(new Rect(175, 63, 500, 500), "Version");
		}

		if (ComAddress == "C:/Programs")
		{
			GUI.Label(new Rect(25, 63, 500, 500), "Name");
			GUI.Label(new Rect(133, 63, 500, 500), "Size");
			GUI.Label(new Rect(175, 63, 500, 500), "Version");
		}

		if(ComAddress == "C:/System")
		{
			GUI.Label(new Rect(25,63,500,500),"Name");
			GUI.Label(new Rect(133,63,500,500),"Size");
			GUI.Label(new Rect(175,63,500,500),"Version");
        }
	}


	void ToolBar()
	{
		switch(MenuSelected)
		{
		case 0:
			if (ComAddress != "Gateway") 
			{
				if (GUI.Button (new Rect (2, 23, 80, 20), "New Folder")) 
				{
					MenuSelected = 2;
					PlayClickSound ();
				}

				if (SelectedProgram >= 0) 
				{
					if (GUI.Button (new Rect (83, 23, 35, 20), "Edit")) 
					{
						PlayClickSound ();
						MenuSelected = 1;
					}

					if (GUI.Button (new Rect (119, 23, 70, 20), "Properties")) 
					{
						PlayClickSound ();
					}
				}
			} 
			else 
			{
				if (SelectedProgram >= 0) 
				{
					if (GUI.Button (new Rect (2, 23, 65, 20), "Rename"))
					{
						PlayClickSound();
					}
					if (GUI.Button (new Rect (68, 23, 70, 20), "Properties")) 
					{
						PlayClickSound();
					}
				}
			}
			break;
		case 1:
			if (GUI.Button (new Rect (2, 23, 40, 20), "Back")) 
			{
				PlayClickSound();
				MenuSelected = 0;
			}
			if (GUI.Button (new Rect (43, 23, 60, 20), "Rename")) 
			{
				PlayClickSound();
				OldName = PageFile[SelectedProgram].Name;
				NewName = PageFile[SelectedProgram].Name;
				MenuSelected = 3;
			}
			if (GUI.Button (new Rect (104, 23, 47, 20), "Delete")) 
			{
				DeleteSystem();
				PlayClickSound();
			}
			if (GUI.Button (new Rect (152, 23, 40, 20), "Copy")) 
			{
				CopySystem();
				PlayClickSound();
			}
			if (GUI.Button (new Rect (193, 23, 40, 20), "Paste")) 
			{
				//PasteSystem();
				PlayClickSound();
			}
			break;

		case 2:
			GUI.SetNextControlName ("FolderName");
			FolderName = GUI.TextField(new Rect(36, 23, 200, 20), FolderName, 100);
			if (GUI.Button (new Rect (2,23,33,20), "Add"))
			{
				if (FolderName != "") 
				{
					AddNewFolder();
				}
			}
			if (GUI.Button (new Rect (237,23,21,20), "X",com.Skin [GameControl.control.GUIID].customStyles [2]))
			{
				MenuSelected = 0;
			}
			break;

		case 3:
			GUI.SetNextControlName ("RenameFile");
			NewName = GUI.TextField(new Rect(66, 23, 200, 20), NewName, 100);
			if (GUI.Button (new Rect (2,23,63,20), "Rename"))
			{
				if (NewName != "") 
				{
					FileRename();
				}
			}
			if (GUI.Button (new Rect (267,23,21,20), "X",com.Skin [GameControl.control.GUIID].customStyles [2]))
			{
				MenuSelected = 0;
			}
			break;
		}
	}


	void CloseContextMenu()
	{
		ContextMenuOptions.RemoveRange (0, ContextMenuOptions.Count);
		SelectedOption = "";
		ShowContext = false;
	}

	void RenameContextOption()
	{
		GUI.UnfocusWindow();
		CloseContextMenu();
		PlayClickSound();
		OldName = PageFile[SelectedProgram].Name;
		NewName = PageFile[SelectedProgram].Name;
		MenuSelected = 3;
		ChangeFocus = true;
	}
}