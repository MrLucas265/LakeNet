using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockProgram : MonoBehaviour
{
	public bool quit;

	private GameObject Puter;

	private GameObject WindowHandel;
	private WindowManager winman;

	private Computer com;
	private SoundControl sc;
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

	public string PersonName;
	public string ProgramName;

	// Use this for initialization
	void Start()
	{
		ProgramNameForWinMan = "Clock";
		ContextMenuName = "Clock Context Menu";

		Puter = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");
		com = Puter.GetComponent<Computer>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		appman = Puter.GetComponent<AppMan>();

		winman = WindowHandel.GetComponent<WindowManager>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void SelectWindowID(int WindowID)
	{
		if (Input.GetMouseButtonDown(0))
		{
			SelectedWindowID = WindowID;
			Registry.SetIntData("Player", "WindowManager", "SelectedWindow", WindowID);
		}
	}

	bool GUIKeyDown(KeyCode key)
	{
		if (Event.current.type == EventType.KeyDown)
			return (Event.current.keyCode == key);
		return false;

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
							appman.SelectedApp = ProgramNameForWinMan;
							pwinman.RunningPrograms.RemoveAt(i);
							SetID();
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

						if (!pwinman.RunningPrograms[i].windowRect.Contains(Event.current.mousePosition))
						{
							if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
							{
								CloseContextMenu();
							}
						}
					}
					if (pwinman.RunningPrograms[i].ProgramName == ContextMenuName)
					{
						pwinman.RunningPrograms[i].windowRect.height = 21 * ContextMenuOptions.Count + 2;
						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyContextWindow, ""));


						if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
						{
							if (!pwinman.RunningPrograms[i].windowRect.Contains(Event.current.mousePosition))
							{
								CloseContextMenu();
							}
						}
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
				if (WindowID == Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"))
				{
					winman.WindowResize("Player", Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"));
				}
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

							GUI.DragWindow(new Rect(40, 2, CloseButton.x - 41, 21));
							//winman.WindowDragging(SelectedWindowID, new Rect(40, 2, CloseButton.x - 41, 21));

							if (GUI.Button(new Rect(2, 2, 37, 21), "[---]"))
							{
								if (new Rect(2, 2, 37, 21).Contains(Event.current.mousePosition))
								{
									CreateContextWindow(pwinman.RunningPrograms[i].windowRect.x + Event.current.mousePosition.x, pwinman.RunningPrograms[i].windowRect.y + Event.current.mousePosition.y);
								}
							}

							if (ShowSettings == true)
							{
								DisplaySettings(i);
							}
							else
							{
								DisplayClock(i);
							}
						}
					}
				}
			}
		}
	}

	void DisplayClock(int i)
	{
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			CurrentTimeRect = new Rect(2, 25, pwinman.RunningPrograms[i].windowRect.width - 4, pwinman.RunningPrograms[i].windowRect.height - 27);

			GUI.Box(CurrentTimeRect, PersonController.control.Global.DateTime.CurrentTime + "\n" + PersonController.control.Global.DateTime.TodaysDate);

			GUI.Box(new Rect(40, 2, CloseButton.x - 41, 21), ProgramNameForWinMan);

			//if (pwinman.RunningPrograms[i].Resize == true)
			//{
			//	pwinman.RunningPrograms[i].WindowResizeRect = new Rect(pwinman.RunningPrograms[i].windowRect.x, pwinman.RunningPrograms[i].windowRect.y, pwinman.RunningPrograms[i].windowRect.width - 4, pwinman.RunningPrograms[i].windowRect.height - 27);
			//}

			GUI.Box(new Rect(pwinman.RunningPrograms[i].ResizeRect), "");
		}
	}

	void DisplaySettings(int i)
	{

	}



	void CreateContextWindow(float x, float y)
	{
		winman.ProgramName = ContextMenuName;
		winman.windowRect = new Rect(x, y, 100 * Customize.cust.UIScale, 300 * Customize.cust.UIScale);
		winman.AddProgramWindow();
	}

	void AddContextOptions()
	{
		if (ShowSettings == true)
		{
			ContextMenuOptions.Add("Display Time");
		}
		else
		{
			ContextMenuOptions.Add("Settings");
		}
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
			case "Display Time":
				ShowSettings = false;
				CloseContextMenu();
				break;
			case "Settings":
				ShowSettings = true;
				CloseContextMenu();
				break;
		}
	}
}
