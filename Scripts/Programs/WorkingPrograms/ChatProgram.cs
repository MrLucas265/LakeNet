//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChatProgram : MonoBehaviour
//{
//	public bool quit;

//	private GameObject Puter;

//	private GameObject WindowHandel;
//	private WindowManager winman;

//	private Computer com;
//	private SoundControl sc;
//	private FileExplorer fp;
//	private AppMan appman;

//	private GameObject diagparseObject;

//	private DialogueParser diagparse;
//	private DialogueManager diagman;

//	public Vector2 scrollpos = Vector2.zero;
//	public Vector2 scrollpos1 = Vector2.zero;
//	public int scrollsize;
//	public int scrollsize1;

//	public float native_width = 1920;
//	public float native_height = 1080;

//	public string ProgramNameForWinMan;

//	public int SelectedWindowID;
//	public int SelectedProgram;

//	private Rect CloseButton;
//	public Rect CurrentTimeRect;
//	public Rect CurrentDateRect;

//	public bool ShowSettings;

//	// Vars for context menu
//	public List<string> ContextMenuOptions = new List<string>();
//	public string SelectedOption;
//	public string ContextMenuName;

//	public List<string> Messages = new List<string>();
//	public List<string> Pos = new List<string>();

//	public string PlayerMessageBox;

//	// Use this for initialization
//	void Start()
//	{
//		ProgramNameForWinMan = "ICQ";
//		ContextMenuName = "ICQ Context Menu";

//		Puter = GameObject.Find("System");
//		WindowHandel = GameObject.Find("WindowHandel");
//		diagparseObject = GameObject.Find("DialogueParser");
//		com = Puter.GetComponent<Computer>();
//		sc = Puter.GetComponent<SoundControl>();

//		diagman = diagparseObject.GetComponent<DialogueManager>();
//		diagparse = diagparseObject.GetComponent<DialogueParser>();

//		native_height = Customize.cust.native_height;
//		native_width = Customize.cust.native_width;

//		fp = Puter.GetComponent<FileExplorer>();
//		appman = Puter.GetComponent<AppMan>();

//		winman = WindowHandel.GetComponent<WindowManager>();

//		PlayerMessageBox = "";
//	}

//	// Update is called once per frame
//	void Update()
//	{

//	}

//	void SelectWindowID(int WindowID)
//	{
//		if (Input.GetMouseButtonDown(0))
//		{
//			SelectedWindowID = WindowID;
//			Registry.SetIntData("Player", "WindowManager", "SelectedWindow", WindowID);
//		}
//	}

//	void Close(int ID)
//	{
//		if (winman.RunningPrograms.Count > 0)
//		{
//			for (int i = 0; i < winman.RunningPrograms.Count; i++)
//			{
//				if (winman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
//				{
//					if (winman.RunningPrograms[i].WID == ID)
//					{

//						quit = true;
//						appman.SelectedApp = "ICQ";
//						winman.RunningPrograms.RemoveAt(i);
//						SetID();
//					}
//				}
//			}
//		}
//	}

//	void CloseContextMenu()
//	{
//		if (winman.RunningPrograms.Count > 0)
//		{
//			for (int i = 0; i < winman.RunningPrograms.Count; i++)
//			{
//				if (winman.RunningPrograms[i].ProgramName == ContextMenuName)
//				{
//					ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
//					winman.RunningPrograms.RemoveAt(i);
//					SelectedOption = "";
//				}
//			}
//		}
//	}

//	void OnGUI()
//	{
//		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

//		if (winman.RunningPrograms.Count > 0)
//		{
//			for (int i = 0; i < winman.RunningPrograms.Count; i++)
//			{
//				if (winman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
//				{
//					GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
//					winman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(winman.RunningPrograms[i].WID, winman.RunningPrograms[i].windowRect, DoMyWindow, ""));
//				}
//				if (winman.RunningPrograms[i].ProgramName == ContextMenuName)
//				{
//					winman.RunningPrograms[i].windowRect.height = 21 * ContextMenuOptions.Count + 2;
//					winman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(winman.RunningPrograms[i].WID, winman.RunningPrograms[i].windowRect, DoMyContextWindow, ""));
//				}
//			}
//		}
//	}

//	void SetID()
//	{
//		int count = 0;
//		for (int j = 0; j < winman.RunningPrograms.Count; j++)
//		{
//			if (winman.RunningPrograms[j].ProgramName == ProgramNameForWinMan)
//			{
//				count++;
//				winman.RunningPrograms[j].PID = count - 1;
//			}
//		}
//	}

//	void DoMyWindow(int WindowID)
//	{
//		SelectWindowID(WindowID);

//		if (winman.RunningPrograms.Count > 0)
//		{
//			winman.WindowResize(SelectedWindowID);

//			for (int i = 0; i < winman.RunningPrograms.Count; i++)
//			{
//				if (winman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
//				{
//					if (winman.RunningPrograms[i].WID == SelectedWindowID)
//					{
//						SelectedProgram = winman.RunningPrograms[i].PID;
//					}

//					if (WindowID == winman.RunningPrograms[i].WID)
//					{
//						CloseButton = new Rect(winman.RunningPrograms[i].windowRect.width - 23, 2, 21, 21);
//						if (CloseButton.Contains(Event.current.mousePosition))
//						{
//							if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
//							{
//								Close(SelectedWindowID);
//							}

//							GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
//							GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
//						}
//						else
//						{
//							GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
//							GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

//							if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]))
//							{
//								Close(SelectedWindowID);
//							}
//						}

//						GUI.DragWindow(new Rect(40, 2, CloseButton.x - 41, 21));
//						winman.WindowDragging(SelectedWindowID, new Rect(40, 2, CloseButton.x - 41, 21));

//						if (GUI.Button(new Rect(2, 2, 37, 21), "[---]"))
//						{
//							if (new Rect(2, 2, 37, 21).Contains(Event.current.mousePosition))
//							{
//								CreateContextWindow(winman.RunningPrograms[i].windowRect.x + Event.current.mousePosition.x, winman.RunningPrograms[i].windowRect.y + Event.current.mousePosition.y);
//							}
//						}

//						GUI.Box(new Rect(40, 2, CloseButton.x - 41, 21), "ICQ");

//						if (ShowSettings == true)
//						{
//							DisplaySettings(i);
//						}
//						else
//						{
//							DisplayMessageArea(i);
//							DisplayTextArea(i);
//							ContactsList(i);
//						}
//						ResizeWindowStuff(i);
//					}
//				}
//			}
//		}
//	}

//	void DisplayMessageArea(int i)
//	{ 
//		float Math = winman.RunningPrograms[i].windowRect.width / 3.5f;

//		CurrentTimeRect = new Rect(Math, 25, winman.RunningPrograms[i].windowRect.width / 1.5f, winman.RunningPrograms[i].windowRect.height - 75);


//		if (GameControl.control.Contacts.Count > 0)
//		{
//			for (int j = 0; j < GameControl.control.Contacts.Count; j++)
//			{
//				if (GameControl.control.Contacts[j].CharacterID == diagparse.Scene)
//				{
//					if (GameControl.control.Contacts[j].Messages.Count > 0)
//					{
//						scrollpos = GUI.BeginScrollView(new Rect(CurrentTimeRect), scrollpos, new Rect(0, 0, 0, scrollsize * 51));
//						for (scrollsize = 0; scrollsize < GameControl.control.Contacts[j].Messages.Count; scrollsize++)
//						{
//							if (GameControl.control.Contacts[j].Messages[scrollsize].Pos == "L")
//							{
//								GUI.TextArea(new Rect(0, scrollsize * 51, winman.RunningPrograms[i].windowRect.width / 4, 50), GameControl.control.Contacts[j].Messages[scrollsize].DialogueMessage);
//							}
//							if (GameControl.control.Contacts[j].Messages[scrollsize].Pos == "R")
//							{
//								GUI.TextArea(new Rect(Math, scrollsize * 51, winman.RunningPrograms[i].windowRect.width / 4, 50), GameControl.control.Contacts[j].Messages[scrollsize].DialogueMessage);
//							}
//						}
//						GUI.EndScrollView();
//					}
//				}
//			}
//		}
//	}

//	void DisplayTextArea(int i)
//	{
//		//float Math = winman.RunningPrograms[i].windowRect.width / 3.5f;
//		//CurrentTimeRect = new Rect(Math, 25, winman.RunningPrograms[i].windowRect.width / 1.5f, winman.RunningPrograms[i].windowRect.height - 52);

//		PlayerMessageBox = GUI.TextArea(new Rect(winman.RunningPrograms[i].windowRect.width / 3.5f, winman.RunningPrograms[i].windowRect.height - 40, winman.RunningPrograms[i].windowRect.width / 2f, 40), PlayerMessageBox);
//	}

//	void ResizeWindowStuff(int i)
//	{
//		if (winman.RunningPrograms[i].Resize == true)
//		{
//			winman.RunningPrograms[i].WindowResizeRect = new Rect(winman.RunningPrograms[i].windowRect.x, winman.RunningPrograms[i].windowRect.y, winman.RunningPrograms[i].windowRect.width - 4, winman.RunningPrograms[i].windowRect.height - 27);
//		}

//		GUI.Box(new Rect(winman.RunningPrograms[i].ResizeRect), "");
//	}

//	void DisplaySettings(int i)
//	{

//	}

//	void ContactsList(int WindowID)
//	{
//		GUI.Box(new Rect(2, 24, winman.RunningPrograms[WindowID].windowRect.width / 4, 21), "Contacts");
//		if(GameControl.control.Contacts.Count > 0)
//		{
//			scrollpos1 = GUI.BeginScrollView(new Rect(2, 46, winman.RunningPrograms[WindowID].windowRect.width / 4, winman.RunningPrograms[WindowID].windowRect.height - 42), scrollpos1, new Rect(0, 0, 0, scrollsize1 * 22));
//			for (scrollsize1 = 0; scrollsize1 < GameControl.control.Contacts.Count; scrollsize1++)
//			{
//				if (GUI.Button(new Rect(0, scrollsize1 * 22, winman.RunningPrograms[WindowID].windowRect.width / 4, 21), GameControl.control.Contacts[scrollsize1].CharacterName))
//				{
//					UpdateDiagData();
//				}
//			}
//			GUI.EndScrollView();
//		}
//	}

//	void UpdateDiagData()
//	{
//		diagparse.Scene = GameControl.control.Contacts[scrollsize1].CharacterID;
//		diagparse.UpdateDialog = true;
//		diagman.dialogue = diagparse.GetContent(0);
//	}



//	void CreateContextWindow(float x, float y)
//	{
//		winman.ProgramName = ContextMenuName;
//		winman.windowRect = new Rect(x, y, 100 * Customize.cust.UIScale, 300 * Customize.cust.UIScale);
//		winman.AddProgramWindow();
//	}

//	void AddContextOptions()
//	{
//		if (ShowSettings == true)
//		{
//			ContextMenuOptions.Add("Display Time");
//		}
//		else
//		{
//			ContextMenuOptions.Add("Settings");
//		}
//	}

//	void DoMyContextWindow(int WindowID)
//	{
//		SelectWindowID(WindowID);
//		//GUI.Box (new Rect (Input.mousePosition.x, Input.mousePosition.y, 100, 200), "");
//		GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
//		GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

//		if (ContextMenuOptions.Count <= 0)
//		{
//			AddContextOptions();
//		}

//		if (ContextMenuOptions.Count > 0)
//		{
//			for (int i = 0; i < ContextMenuOptions.Count; i++)
//			{
//				if (GUI.Button(new Rect(1, 1 + 21 * i, 100 - 2, 21), ContextMenuOptions[i]))
//				{
//					SelectedOption = ContextMenuOptions[i];
//				}
//			}
//		}

//		switch (SelectedOption)
//		{
//			case "Display Time":
//				ShowSettings = false;
//				CloseContextMenu();
//				break;
//			case "Settings":
//				ShowSettings = true;
//				CloseContextMenu();
//				break;
//		}
//	}
//}
