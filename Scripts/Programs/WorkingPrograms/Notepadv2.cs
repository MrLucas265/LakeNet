using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Notepadv2 : MonoBehaviour
{
	public GameObject SysSoftware;
	public bool show;
	private Computer com;
	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	private FileExplorer fp;

	private AppMan appman;

	public float DiskUsage;

	private Defalt defalt;

	public string TypedText;
	public string CurrentWorkingTitle;
	public string TypedTitle;
	public string SaveLocation;

	public int SelectedDocument;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int FoundAt;

	public bool ShowFileNameMaker;
	public bool ShowFileContent;
	public bool ShowFileOpen;

	public bool showSave;

	public Texture2D Icon;

	public float FileSize;

	public Rect TextAreaRect;

	public int SelectedMenu;

	public List<string> Name = new List<string>();
	public List<string> Location = new List<string>();
	public List<int> FileIndex = new List<int>();

	public List<ProgramSystem> Files = new List<ProgramSystem>();

	private Rect CloseButton;

	public int ContextMenuID;
	public Rect ContextwindowRect = new Rect(100, 100, 100, 200);
	public bool ShowContext;
	public List<string> ContextMenuOptions = new List<string>();
	public string SelectedOption;
	public Vector2 Scroll;

	public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
	public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

	void Start()
	{
		SysSoftware = GameObject.Find("System");
		com = SysSoftware.GetComponent<Computer>();
		defalt = SysSoftware.GetComponent<Defalt>();
		fp = SysSoftware.GetComponent<FileExplorer>();
		appman = SysSoftware.GetComponent<AppMan>();

		PosCheck();

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		windowRect.width = 300;
		windowRect.height = 300;

		ContextwindowRect.width = 100;

		CloseButton = new Rect (windowRect.width-23,2,21,21);
	}

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0)
		{
			Customize.cust.windowx[windowID] = Screen.width / 2;
		}
		if (Customize.cust.windowy[windowID] <= 35)
		{
			Customize.cust.windowy[windowID] = 35;
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];

			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
			{
				if (!new Rect (windowRect).Contains (Event.current.mousePosition))
				{
					ShowContext = false;
				}
			}

		if (show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
			// windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}

		if (ShowContext == true)
		{
			ContextwindowRect.height = 21 * ContextMenuOptions.Count + 2;
			GUI.skin = com.Skin[GameControl.control.GUIID];
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			ContextwindowRect = WindowClamp.ClampToScreen(GUI.Window(ContextMenuID, ContextwindowRect, DoMyContextWindow, ""));
		}
	}

	public void Open()
	{
		CurrentWorkingTitle = GameControl.control.ProgramFiles[SelectedDocument].Name;
		TypedText = GameControl.control.ProgramFiles[SelectedDocument].Content;
		TypedTitle = GameControl.control.ProgramFiles[SelectedDocument].Name;
		SaveLocation = GameControl.control.ProgramFiles[SelectedDocument].Location;
		showSave = true;
		ShowFileContent = true;
		ShowFileNameMaker = false;
		ShowFileOpen = false;
		SelectedMenu = 0;
	}

	public void Save()
	{
		FileSize = 0;
		FileSize = TypedTitle.Length + TypedText.Length;
		FileSize = FileSize / 100;
		if (GameControl.control.ProgramFiles.Count > 0)
		{
			if (CurrentWorkingTitle == GameControl.control.ProgramFiles[SelectedDocument].Name)
			{
                if (GameControl.control.ProgramFiles[SelectedDocument].Name != TypedTitle)
                {
					GameControl.control.ProgramFiles.Insert(0, new ProgramSystem(TypedTitle, "", "", GameControl.control.Time.DayName, TypedText, "", SaveLocation, "", "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
                    GameControl.control.ProgramFiles[0].Used = FileSize;
                }
                else
                {
                    GameControl.control.ProgramFiles[SelectedDocument].Content = TypedText;
                    GameControl.control.ProgramFiles[SelectedDocument].Used = FileSize;
                }
			}
			else
			{
				if (GameControl.control.ProgramFiles[SelectedDocument].Name != TypedTitle)
				{
					GameControl.control.ProgramFiles.Insert(0, new ProgramSystem(TypedTitle, "", "", GameControl.control.Time.DayName, TypedText, "", SaveLocation, "", "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
					GameControl.control.ProgramFiles[0].Used = FileSize;
				}
			}
		}
		else
		{
			GameControl.control.ProgramFiles.Insert(0, new ProgramSystem(TypedTitle, "", "", GameControl.control.Time.DayName, TypedText, "", SaveLocation, "", "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
			GameControl.control.ProgramFiles[0].Used = FileSize;
		}
	}

	void DiskCheck()
	{
		Name.RemoveRange(0, Name.Count);
		Location.RemoveRange(0, Location.Count);
		FileIndex.RemoveRange(0, FileIndex.Count);
		Files.RemoveRange(0, Files.Count);
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.Txt)
			{
				if (Name.Contains(GameControl.control.ProgramFiles[i].Name))
				{
					if (!Location.Contains(GameControl.control.ProgramFiles[i].Location))
					{
						Name.Add(GameControl.control.ProgramFiles[i].Name);
						Location.Add(GameControl.control.ProgramFiles[i].Location);
						FileIndex.Add(i);
						Files.Add(GameControl.control.ProgramFiles[i]);
					}
				}
				else
				{
					Name.Add(GameControl.control.ProgramFiles[i].Name);
					Location.Add(GameControl.control.ProgramFiles[i].Location);
					FileIndex.Add(i);
					Files.Add(GameControl.control.ProgramFiles[i]);
				}
			}
		}
	}

	void NewFile()
	{	
			ShowFileNameMaker = false;
			ShowFileContent = false;
			ShowFileOpen = false;
			CurrentWorkingTitle = "Untitled";
			SaveLocation = "";
			TypedTitle = "";
			TypedText = "";
			SelectedMenu = 0;
	}

	void AddContextOptions()
	{
			ContextMenuOptions.Add ("New");
			//ContextMenuOptions.Add ("Copy");
			ContextMenuOptions.Add ("Save As");
			ContextMenuOptions.Add ("Open");
			//ContextMenuOptions.Add ("Create Icon");
			ContextMenuOptions.Add ("Save");
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
		case "New":
			NewFile();
			CloseContextMenu();
			break;
		case "Open":
            if(fp.enabled == true)
            {
				fp.SetFileExplorerData("Text", "Open", "Notepad");
                CloseContextMenu();
            }
            else
            {
                appman.SelectedApp = "File Explorer";
				fp.SetFileExplorerData("Text", "Open", "Notepad");
                CloseContextMenu();
            }
			break;
		case "Save As":
            if (fp.enabled == true)
            {
				fp.SetFileExplorerData("Text", "Save As", "Notepad");
				ShowFileNameMaker = false;
                ShowFileContent = false;
                ShowFileOpen = false;
                SelectedMenu = 0;
                CloseContextMenu();
            }
            else
            {
                appman.SelectedApp = "File Explorer";
				fp.SetFileExplorerData("Text", "Save As", "Notepad");
				ShowFileNameMaker = false;
                ShowFileContent = false;
                ShowFileOpen = false;
                SelectedMenu = 0;
                CloseContextMenu();
            }
			break;
		case "Save":
			if (TypedTitle != "")
			{
				if (CurrentWorkingTitle != "")
				{
                    if (SaveLocation != "")
                    {
                        Save();
                    }
				}
			}
			else
			{
                if (fp.enabled == true)
                {
					fp.SetFileExplorerData("Text", "Save As", "Notepad");
                    ShowFileNameMaker = false;
                    ShowFileContent = false;
                    ShowFileOpen = false;
                    SelectedMenu = 0;
                }
                else
                {
                    appman.SelectedApp = "File Explorer";
					fp.SetFileExplorerData("Text", "Save As", "Notepad");
                    ShowFileNameMaker = false;
                    ShowFileContent = false;
                    ShowFileOpen = false;
                    SelectedMenu = 0;
                }
			}
			CloseContextMenu();
			break;
		}
	}

	void CloseContextMenu()
	{
		ContextMenuOptions.RemoveRange (0, ContextMenuOptions.Count);
		SelectedOption = "";
		ShowContext = false;
	}

	void DoMyWindow(int windowID)
	{
		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
			{
				appman.SelectedApp = "Notepadv2";
			}

			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];

			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]))
			{
				appman.SelectedApp = "Notepadv2";
			}
		}

		GUI.DragWindow(new Rect(40,2,236,21));
		GUI.Box(new Rect(40,2,236,21), "" + CurrentWorkingTitle + " - Notepad");

		switch (SelectedMenu)
		{
		case 0:
			//string[] lines = TypedText.Split('\n');
			//scrollpos = GUI.BeginScrollView(new Rect(2, 25,windowRect.width-2, windowRect.height-25), scrollpos, new Rect(0, 0, 0, lines.Length*20));

			//TextAreaRect = new Rect (0, 0, windowRect.width-2, windowRect.height-25);
			//TypedText = GUI.TextArea(new Rect(TextAreaRect), TypedText, 25000);

			//GUI.EndScrollView();
			TextAreaRect = new Rect (2, 25, windowRect.width-4, windowRect.height-27);
			TypedText = GUI.TextArea(new Rect(TextAreaRect), TypedText, 25000);
			break;
		case 1:
			TextAreaRect = new Rect (115, 25, 150, 128);
			//TypedText = GUI.TextArea(new Rect(TextAreaRect), TypedText, 500);
			break;
		case 2:
			TextAreaRect = new Rect (windowRect.width-151, 47, 150, windowRect.height-48);
			TypedText = GUI.TextArea(new Rect(TextAreaRect), TypedText, 500);
			break;
		case 3:
			TextAreaRect = new Rect (115, 25, 150, 128);
			break;
		}

		if (CurrentWorkingTitle == "") 
		{
			CurrentWorkingTitle = "Untitled";
		}

		if (fp.enabled == false)
		{
			ShowFileNameMaker = false;
			ShowFileContent = false;
			ShowFileOpen = false;
			SelectedMenu = 0;
		}

		if (ShowFileNameMaker == true) 
		{
			SaveLocation = fp.SelectedFolderLocation;

			GUI.Label (new Rect (5, 50, 150, 21), "File Name");
			TypedTitle = GUI.TextField (new Rect (5, 100, 140, 21), TypedTitle);

			GUI.Label (new Rect (5, 150, 150, 21), "File Location");
			SaveLocation = GUI.TextField (new Rect (5, 200, 140, 21), SaveLocation);

			//if (TypedTitle != "")
			//{
			//	if (SaveLocation != "")
			//	{
			//		if(GUI.Button(new Rect(50,250,80,21),"Save"))
			//		{
			//			CurrentWorkingTitle = TypedTitle;
			//			FileSize = 0;
			//			FileSize = TypedTitle.Length + TypedText.Length;
			//			FileSize = FileSize / 100;
			//			Save();

			//			ShowFileContent = true;
			//			ShowFileNameMaker = false;
			//			ShowFileOpen = false;
			//			SelectedMenu = 0;

			//			appman.SelectedApp = "File Explorer";
			//		}
			//	}
			//}
		}

		//if(ShowFileContent == true) 
		//{
		//	if(GUI.Button(new Rect(144,24,37,21),"Save"))
		//	{
		//		Save();
		//	}

		//	if (SelectedMenu > 0)
		//	{
		//		if(GUI.Button(new Rect(182,24,50,21),"Cancel"))
		//		{
		//			if (showSave == true)
		//			{
		//				ShowFileContent = true;
		//			}
		//			ShowFileNameMaker = false;
		//			ShowFileOpen = false;
		//			SelectedMenu = 0;
		//		}	
		//	}
		//}
		//else
		//{
		//	if (SelectedMenu > 0) 
		//	{
		//		if(GUI.Button(new Rect(144,24,50,21),"Cancel"))
		//		{
		//			if (showSave == true)
		//			{
		//				ShowFileContent = true;
		//			}
		//			ShowFileNameMaker = false;
		//			ShowFileOpen = false;
		//			SelectedMenu = 0;
		//		}
		//	}
		//}

		//if(GUI.Button(new Rect(83,24,60,21),"Save As"))
		//{
		//	appman.SelectedApp = "File Explorer";
		//	fp.SelectedFileType = "Text";
		//	ShowFileNameMaker = true;
		//	ShowFileContent = false;
		//	ShowFileOpen = false;
		//	SelectedMenu = 2;
		//}

		if(GUI.Button(new Rect(2,2,37,21),"File"))
		{
			if (new Rect (2,2,37,21).Contains (Event.current.mousePosition))
			{
				ContextwindowRect.x = Input.mousePosition.x;
				ContextwindowRect.y = Screen.height - Input.mousePosition.y;
				ShowContext = true;
				GUI.BringWindowToFront(ContextMenuID);
			}
		}

		//if(GUI.Button(new Rect(2,24,37,21),"New"))
		//{
		//	NewFile();
		//}
			
		//if(GUI.Button(new Rect(40,24,42,21),"Open"))
		//{
		//	appman.SelectedApp = "File Explorer";
		//	fp.SelectedFileType = "Text";
		//}

		if (ShowFileOpen == true)
		{
			DiskCheck();
			if (Files.Count > 0) 
			{
				scrollpos = GUI.BeginScrollView(new Rect(2, 50, 120, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 24));
				for (scrollsize = 0; scrollsize < Files.Count; scrollsize++)
				{
					if(GUI.Button(new Rect(0,24*scrollsize,100,21),Files[scrollsize].Name))
					{
						SelectedDocument = FileIndex[scrollsize];
						Open();
					}
				}
				GUI.EndScrollView();
			}
		}
	}
}
