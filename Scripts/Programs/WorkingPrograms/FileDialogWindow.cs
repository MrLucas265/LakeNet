using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileDialogWindow : MonoBehaviour
{
	public bool quit;

	private GameObject Puter;

	private GameObject WindowHandel;
	private WindowManager winman;

	private Computer com;
	private SoundControl sc;
//	private FileExplorer fp;
	private AppMan appman;

	public float native_width = 1920;
	public float native_height = 1080;

	public string ProgramNameForWinMan;

	public int SelectedWindowID;
	public int SelectedProgram;

	private Rect CloseButton;
	public Rect CurrentTimeRect;
	public Rect CurrentDateRect;

	public bool ShowSettings;

	// Vars for context menu
	public List<string> ContextMenuOptions = new List<string>();
	public string SelectedOption;
	public string ContextMenuName;

	public FileInfo outputFile; //the selected output file

	public string CurrentPath;

	public List<string> DirectoryFiles = new List<string>();
	public List<string> DirectoryNames = new List<string>();

	public DirectoryInfo currentDirectory;
	public FileInformation[] files;
	public DirectoryInformation[] directories, drives;
	public DirectoryInformation parentDir;

	public string searchPattern = "*";

	public bool TurnOn;

	public bool FileOn;

	public bool DriveSelected;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public string DisplayName;
	public string[] SplitPath;

	public string SelectedFile;

	public bool SortByName;
	public string FolderIcon;

	// Use this for initialization
	void Start()
	{
		ProgramNameForWinMan = "FileBrow";
		ContextMenuName = "Clock Context Menu";

		Puter = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");
		com = Puter.GetComponent<Computer>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

//		fp = Puter.GetComponent<FileExplorer>();
		appman = Puter.GetComponent<AppMan>();

		winman = WindowHandel.GetComponent<WindowManager>();

		CurrentPath = "C:/";

		DriveSelected = false;
		TurnOn = true;
		FileOn = true;
	}

	// Update is called once per frame
	void Update()
	{
		if(TurnOn == true)
		{
			setDirectory(CurrentPath);
			getFileList(currentDirectory);
			TurnOn = false;
		}
		if(TurnOn == false && SortByName == true)
        {
			//var sortedList = .OrderBy(go => go.name).ToList();
			DirectoryFiles.Sort();
			DirectoryNames.Sort();
			SortByName = false;
        }
	}

	public void setDirectory(string dir) 
	{ 
		currentDirectory = new DirectoryInfo(dir); 
	}

	void SelectWindowID(int WindowID)
	{
		if (Input.GetMouseButtonDown(0))
		{
			SelectedWindowID = WindowID;
			Registry.SetIntData("Player", "WindowManager", "SelectedWindow", WindowID);
		}
	}

	void Close(int ID)
	{
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						if (pwinman.RunningPrograms[i].WID == ID)
						{
							if(pwinman.RunningPrograms.Count == 1)
                            {
								quit = false;
								appman.LaunchRequest = Program.Run("File Browser", ProgramSystemv2.ProgramTypes.FileBrow, "Player");
								pwinman.RunningPrograms.RemoveAt(i);
								SetID();
							}
							else
                            {
								quit = true;
								appman.LaunchRequest = Program.Run("File Browser", ProgramSystemv2.ProgramTypes.FileBrow, "Player");
								pwinman.RunningPrograms.RemoveAt(i);
								SetID();
							}
						}
					}
				}
			}

		}
	}

	void CloseContextMenu()
	{
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ContextMenuName)
					{
						ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
						pwinman.RunningPrograms.RemoveAt(i);
						SelectedOption = "";
					}
				}
			}

		}
	}

	void OnGUI()
	{
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));
					}
					if (pwinman.RunningPrograms[i].ProgramName == ContextMenuName)
					{
						pwinman.RunningPrograms[i].windowRect.height = 21 * ContextMenuOptions.Count + 2;
						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyContextWindow, ""));
					}
				}
			}
		}
	}

	void SetID()
	{
		int count = 0;
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			for (int j = 0; j < pwinman.RunningPrograms.Count; j++)
			{
				if (pwinman.RunningPrograms[j].ProgramName == ProgramNameForWinMan)
				{
					count++;
					pwinman.RunningPrograms[j].PID = count - 1;
				}
			}

		}
	}

	void DoMyWindow(int WindowID)
	{
		SelectWindowID(WindowID);

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				//winman.WindowResize(SelectedWindowID);

				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						if (pwinman.RunningPrograms[i].WID == SelectedWindowID)
						{
							SelectedProgram = pwinman.RunningPrograms[i].PID;
						}

						if (WindowID == pwinman.RunningPrograms[i].WID)
						{
							CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 23, 2, 21, 21);
							if (CloseButton.Contains(Event.current.mousePosition))
							{
								if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
								{
									Close(SelectedWindowID);
								}

								GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
								GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
							}
							else
							{
								GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
								GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

								if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]))
								{
									Close(SelectedWindowID);
								}
							}

							GUI.Box(new Rect(40, 2, CloseButton.x - 41, 21), "File Browser");

							GUI.DragWindow(new Rect(40, 2, CloseButton.x - 41, 21));
							//winman.WindowDragging(SelectedWindowID, new Rect(40, 2, CloseButton.x - 41, 21));

							if (GUI.Button(new Rect(2, 2, 37, 21), "[---]"))
							{
								DriveSelected = false;
								//if (new Rect(2, 2, 37, 21).Contains(Event.current.mousePosition))
								//{
								//	CreateContextWindow(pwinman.RunningPrograms[i].windowRect.x + Event.current.mousePosition.x, pwinman.RunningPrograms[i].windowRect.y + Event.current.mousePosition.y);
								//}
							}

							DisplayFileUI(i);
						}
					}
				}
			}

		}
	}

	void DisplayFileUI(int i)
	{
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			CurrentTimeRect = new Rect(2, 25, pwinman.RunningPrograms[i].windowRect.width - 5, pwinman.RunningPrograms[i].windowRect.height - 66);

			if (DriveSelected == false)
			{
				if (drives.Length > 0)
				{
					scrollpos = GUI.BeginScrollView(CurrentTimeRect, scrollpos, new Rect(0, 0, 0, scrollsize * 22));
					for (scrollsize = 0; scrollsize < drives.Length; scrollsize++)
					{
						if (GUI.Button(new Rect(0, scrollsize * 22, pwinman.RunningPrograms[i].windowRect.width - 75, 21), drives[scrollsize].di.ToString()))
						{
							CurrentPath = drives[scrollsize].di.ToString();
							setDirectory(drives[scrollsize].di.ToString());
							DriveSelected = true;
							TurnOn = true;
						}
					}
					GUI.EndScrollView();
				}
			}
			else
			{

				if (DirectoryFiles.Count > 0)
				{
					scrollpos = GUI.BeginScrollView(CurrentTimeRect, scrollpos, new Rect(0, 0, 0, scrollsize * 22));
					for (scrollsize = 0; scrollsize < DirectoryFiles.Count; scrollsize++)
					{
						if (!DirectoryNames[scrollsize].Contains("."))
						{
							GUI.backgroundColor = Color.yellow;
							GUI.Box(new Rect(0, scrollsize * 22, 21, 21), "▶");
							GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
						}
						else
                        {
							GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
						}
						if (GUI.Button(new Rect(22, scrollsize * 22, pwinman.RunningPrograms[i].windowRect.width - 25, 21), DirectoryNames[scrollsize]))
						{
							//string CurrentFileExtension = Path.GetExtension(DirectoryNames[scrollsize]).ToLower();
							if (DirectoryNames[scrollsize].Contains("."))
							{
								SelectedFile = DirectoryFiles[scrollsize];
							}
							else
							{
								CurrentPath = DirectoryFiles[scrollsize];
								setDirectory(DirectoryFiles[scrollsize]);
								TurnOn = true;
							}
						}
					}
					GUI.EndScrollView();
				}
			}

			if (SelectedFile != "")
			{
				if (GUI.Button(new Rect(pwinman.RunningPrograms[i].windowRect.width - 125, pwinman.RunningPrograms[i].windowRect.height - 23, 75, 21), "Select File"))
				{
					FilePathData.SelectFile(SelectedFile);
					SelectedFile = "";
					Close(SelectedWindowID);
				}
			}

			//GUI.Box(CurrentTimeRect, PersonController.control.Global.DateTime.CurrentTime + "\n" + PersonController.control.Global.DateTime.TodaysDate);

			if (pwinman.RunningPrograms[i].Resize == true)
			{
				pwinman.RunningPrograms[i].WindowResizeRect = new Rect(pwinman.RunningPrograms[i].windowRect.x, pwinman.RunningPrograms[i].windowRect.y, pwinman.RunningPrograms[i].windowRect.width - 4, pwinman.RunningPrograms[i].windowRect.height - 27);
			}

			GUI.Box(new Rect(pwinman.RunningPrograms[i].ResizeRect), "");

		}
	}

	public void getFileList(DirectoryInfo di)
	{
		DirectoryFiles.RemoveRange(0, DirectoryFiles.Count);
		DirectoryNames.RemoveRange(0, DirectoryNames.Count);
		//set current directory
		currentDirectory = di;
		//get parent
		parentDir = (di.Parent == null) ? new DirectoryInformation(di) : new DirectoryInformation(di.Parent);

		//get drives
		string[] drvs = System.IO.Directory.GetLogicalDrives();
		drives = new DirectoryInformation[drvs.Length];
		for (int v = 0; v < drvs.Length; v++)
		{
			drives[v] = new DirectoryInformation(new DirectoryInfo(drvs[v]));
		}

		//get directories
		DirectoryInfo[] dia = di.GetDirectories();
		directories = new DirectoryInformation[dia.Length];
		for (int d = 0; d < dia.Length; d++)
		{
			directories[d] = new DirectoryInformation(dia[d]);
			if (!DirectoryFiles.Contains(directories[d].di.ToString()))
			{
				DirectoryFiles.Add(directories[d].di.ToString());
			}
		}

		//get files
		FileInfo[] fia = di.GetFiles();
		//FileInfo[] fia = searchDirectory(di,searchPattern);
		files = new FileInformation[fia.Length];
		for (int f = 0; f < fia.Length; f++)
		{
			files[f] = new FileInformation(fia[f]);
			if (!DirectoryFiles.Contains(files[f].fi.ToString()))
			{
				DirectoryFiles.Add(files[f].fi.ToString());
			}
		}

		for(int i = 0; i < DirectoryFiles.Count; i++)
		{
			DisplayName = DirectoryFiles[i];
			SplitPath = DisplayName.Split(Path.DirectorySeparatorChar);
			DirectoryNames.Add(SplitPath[SplitPath.Length - 1]);
		}
	}



	void CreateContextWindow(float x, float y)
	{
		winman.ProgramName = ContextMenuName;
		winman.windowRect = new Rect(x, y, 100 * Customize.cust.UIScale, 300 * Customize.cust.UIScale);
		winman.AddProgramWindow();
	}

	void AddContextOptions()
	{
		ContextMenuOptions.Add("Volumes");
	}

	void DoMyContextWindow(int WindowID)
	{
		SelectWindowID(WindowID);
		//GUI.Box (new Rect (Input.mousePosition.x, Input.mousePosition.y, 100, 200), "");
		GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

		if (ContextMenuOptions.Count <= 0)
		{
			AddContextOptions();
		}

		if (ContextMenuOptions.Count > 0)
		{
			for (int i = 0; i < ContextMenuOptions.Count; i++)
			{
				if (GUI.Button(new Rect(1, 1 + 21 * i, 100 - 2, 21), ContextMenuOptions[i]))
				{
					SelectedOption = ContextMenuOptions[i];
				}
			}
		}

		switch (SelectedOption)
		{
			case "Volumes":
				DriveSelected = false;
				CloseContextMenu();
				break;
		}
	}
}
