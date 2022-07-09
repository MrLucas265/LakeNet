using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notepadv3 : MonoBehaviour {

	public List<NotepadSystem> NotepadData = new List<NotepadSystem>();
	public bool quit;

	private GameObject Puter;

	private GameObject WindowHandel;
	private WindowManager winman;

	private Computer com;
	private SoundControl sc;
	private FileExplorer fp;
	private AppMan appman;

	public float native_width = 1920;
	public float native_height = 1080;

	public Rect TextAreaRect;

	public string ProgramNameForWinMan;

	public int SelectedWindowID;
	public int SelectedProgram;

	public bool RunDebugTest;

	private Rect CloseButton;

	// Vars for context menu
	public List<string> ContextMenuOptions = new List<string>();
	public string SelectedOption;
	public string ContextMenuName;

	// Use this for initialization
	void Start()
	{
		ProgramNameForWinMan = "NotepadV3";

		Puter = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");
		com = Puter.GetComponent<Computer>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		fp = Puter.GetComponent<FileExplorer>();
		appman = Puter.GetComponent<AppMan>();

		winman = WindowHandel.GetComponent<WindowManager>();

		ContextMenuName = "NotepadV3 Context Menu";
	}

	// Update is called once per frame
	void Update () 
	{
		if(RunDebugTest == true)
		{
			AddNotepadWindow("Test","","Test","This is a debug test","Home",0);
			RunDebugTest = false;
		}
	}

	public void AddNotepadWindow(string CurrentWorkingTitle, string SaveLocation, string TypedTitle, string TypedText, string CurrentMenu,int SelectedDocument)
	{
		NotepadData.Add(new NotepadSystem(CurrentWorkingTitle, SaveLocation, TypedTitle, TypedText, CurrentMenu,SelectedDocument));
	}

	public void RemoveNotepadData(int ID)
	{
		NotepadData.RemoveAt(ID);
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

							quit = true;
							appman.SelectedApp = "Notepadv3";
							RemoveNotepadData(pwinman.RunningPrograms[i].PID);
							pwinman.RunningPrograms.RemoveAt(i);
							SetID();
						}
					}
				}
			}
		}
	}

	void SelectWindowID(int WindowID)
	{
		if (Input.GetMouseButtonDown(0))
		{
			SelectedWindowID = WindowID;
			winman.SelectedWID = WindowID;
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
						pwinman.RunningPrograms.RemoveAt(i);
						SelectedOption = "";
					}
				}
			}
		}
	}

	void OnGUI()
	{
		GUI.skin = com.Skin[GameControl.control.GUIID];

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						GUI.color = com.colors[Customize.cust.WindowColorInt];
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
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			int count = 0;
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
								if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
								{
									Close(SelectedWindowID);
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
									Close(SelectedWindowID);
								}
							}

							GUI.DragWindow(new Rect(40, 2, CloseButton.x - 41, 21));
							//winman.WindowDragging(SelectedWindowID, new Rect(40, 2, CloseButton.x - 41, 21));

							if (GUI.Button(new Rect(2, 2, 37, 21), "[---]"))
							{
								CloseContextMenu();
								if (new Rect(2, 2, 37, 21).Contains(Event.current.mousePosition))
								{
									CreateContextWindow(pwinman.RunningPrograms[i].windowRect.x + Event.current.mousePosition.x, pwinman.RunningPrograms[i].windowRect.y + Event.current.mousePosition.y);
								}
							}

							TextAreaRect = new Rect(2, 25, pwinman.RunningPrograms[i].windowRect.width - 4, pwinman.RunningPrograms[i].windowRect.height - 27);

							if (pwinman.RunningPrograms[i].Resize == true)
							{
								pwinman.RunningPrograms[i].WindowResizeRect = new Rect(pwinman.RunningPrograms[i].windowRect.x, pwinman.RunningPrograms[i].windowRect.y, pwinman.RunningPrograms[i].windowRect.width - 4, pwinman.RunningPrograms[i].windowRect.height - 27);
							}

							if (NotepadData.Count > 0)
							{
								for (int j = 0; j < NotepadData.Count; j++)
								{
									if (j == pwinman.RunningPrograms[i].PID)
									{
										if (NotepadData[j].CurrentWorkingTitle == "")
										{
											NotepadData[j].CurrentWorkingTitle = "Untitled";
										}

										GUI.Box(new Rect(40, 2, CloseButton.x - 41, 21), "" + NotepadData[j].CurrentWorkingTitle + " - Notepad");

										NotepadData[j].TypedText = GUI.TextArea(TextAreaRect, NotepadData[j].TypedText);
									}
								}
							}

							GUI.Box(new Rect(pwinman.RunningPrograms[i].ResizeRect), "");
						}
					}
				}
			}
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
		ContextMenuOptions.Add("New");
		//ContextMenuOptions.Add ("Copy");
		ContextMenuOptions.Add("Save As");
		ContextMenuOptions.Add("Open");
		//ContextMenuOptions.Add ("Create Icon");
		ContextMenuOptions.Add("Save");
	}

	void NewFile()
	{
		NotepadData[SelectedProgram].CurrentWorkingTitle = "Untitled";
		NotepadData[SelectedProgram].SaveLocation = "";
		NotepadData[SelectedProgram].TypedTitle = "";
		NotepadData[SelectedProgram].TypedText = "";
	}

	public void Open()
	{
		NotepadData[SelectedProgram].CurrentWorkingTitle = GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Name;
		NotepadData[SelectedProgram].TypedText = GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Content;
		NotepadData[SelectedProgram].TypedTitle = GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Name;
		NotepadData[SelectedProgram].SaveLocation = GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Location;
	}

	public void Save()
	{
		float FileSize = 0;
		FileSize = NotepadData[SelectedProgram].TypedTitle.Length + NotepadData[SelectedProgram].TypedText.Length;
		FileSize = FileSize / 100;
		if (GameControl.control.ProgramFiles.Count > 0)
		{
			if (NotepadData[SelectedProgram].CurrentWorkingTitle == GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Name)
			{
				if (GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Name != NotepadData[SelectedProgram].TypedTitle)
				{
					GameControl.control.ProgramFiles.Insert(0, new ProgramSystem(NotepadData[SelectedProgram].TypedTitle, "", "", GameControl.control.Time.DayName, NotepadData[SelectedProgram].TypedText, "", NotepadData[SelectedProgram].SaveLocation, "", "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
					GameControl.control.ProgramFiles[0].Used = FileSize;
				}
				else
				{
					GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Content = NotepadData[SelectedProgram].TypedText;
					GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Used = FileSize;
				}
			}
			else
			{
				if (GameControl.control.ProgramFiles[NotepadData[SelectedProgram].SelectedDocument].Name != NotepadData[SelectedProgram].TypedTitle)
				{
					GameControl.control.ProgramFiles.Insert(0, new ProgramSystem(NotepadData[SelectedProgram].TypedTitle, "", "", GameControl.control.Time.DayName, NotepadData[SelectedProgram].TypedText, "", NotepadData[SelectedProgram].SaveLocation, "", "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
					GameControl.control.ProgramFiles[0].Used = FileSize;
				}
			}
		}
		else
		{
			GameControl.control.ProgramFiles.Insert(0, new ProgramSystem(NotepadData[SelectedProgram].TypedTitle, "", "", GameControl.control.Time.DayName, NotepadData[SelectedProgram].TypedText, "", NotepadData[SelectedProgram].SaveLocation, "", "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
			GameControl.control.ProgramFiles[0].Used = FileSize;
		}
	}

	void DoMyContextWindow(int WindowID)
	{
		SelectWindowID(WindowID);
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
				if (GUI.Button(new Rect(1, 1 + 21 * i, 100 - 2, 21), ContextMenuOptions[i]))
				{
					SelectedOption = ContextMenuOptions[i];
				}

				if (Input.GetMouseButtonDown(0) && winman.SelectedWID != WindowID)
				{
					if (!new Rect(1, 1 + 21 * i, 100 - 2, 21).Contains(Event.current.mousePosition))
					{
						CloseContextMenu();
					}
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
				if (fp.enabled == true)
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
					CloseContextMenu();
				}
				else
				{
					appman.SelectedApp = "File Explorer";
					fp.SetFileExplorerData("Text", "Save As", "Notepad");
					CloseContextMenu();
				}
				break;
			case "Save":
				if (NotepadData[SelectedProgram].TypedTitle != "")
				{
					if (NotepadData[SelectedProgram].CurrentWorkingTitle != "")
					{
						if (NotepadData[SelectedProgram].SaveLocation != "")
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
					}
					else
					{
						appman.SelectedApp = "File Explorer";
						fp.SetFileExplorerData("Text", "Save As", "Notepad");
					}
				}
				CloseContextMenu();
				break;
		}
	}
}
