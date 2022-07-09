using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
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

	public string ProgramNameForWinMan;

	public int SelectedWindowID;
	public int SelectedProgram;

	private Rect CloseButton;
	public Rect CurrentTimeRect;
	public Rect CurrentDateRect;

	public string PersonName;

	public float IconWidth;
	public float IconHeight;
	public int MaxIconsPerRow;

	public List<ControlPanelSystem> AllDirectoryFilePaths = new List<ControlPanelSystem>();
	public List<ControlPanelSystem> CurDirectoryFilePaths = new List<ControlPanelSystem>();
	public string CurrentPath;

	public Vector2 scrollpos;
	public int scrollsize;

	public string SelectedMenu;

	// Use this for initialization
	void Start()
	{
		ProgramNameForWinMan = "Control Panel";

		Puter = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");
		com = Puter.GetComponent<Computer>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		fp = Puter.GetComponent<FileExplorer>();
		appman = Puter.GetComponent<AppMan>();

		winman = WindowHandel.GetComponent<WindowManager>();

		PersonName = "Player";

		AllDirectoryFilePaths.Add(new ControlPanelSystem("Display", "home", "display",ControlPanelSystem.Menu.Display));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Notification", "home", "notifications", ControlPanelSystem.Menu.Notification));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Account", "home", "account", ControlPanelSystem.Menu.Account));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Mouse", "home", "mouse", ControlPanelSystem.Menu.Mouse));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Web Browser", "home", "web_browsers", ControlPanelSystem.Menu.WebBrowser));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Quick Launch", "home", "quick_launch", ControlPanelSystem.Menu.QuickLaunch));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Commands", "home", "commands", ControlPanelSystem.Menu.Commands));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Soundtrack", "home", "soundtrack", ControlPanelSystem.Menu.Soundtrack));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Download", "home", "download", ControlPanelSystem.Menu.Download));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Default Programs", "home", "default_programs", ControlPanelSystem.Menu.DefaultPrograms));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Autosave", "home", "autosave", ControlPanelSystem.Menu.Autosave));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Dev Settings", "home", "dev", ControlPanelSystem.Menu.DevSettings));

		AllDirectoryFilePaths.Add(new ControlPanelSystem("<- Back", "display", "home", ControlPanelSystem.Menu.Home));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Backgrounds", "display", "display/backgrounds", ControlPanelSystem.Menu.Backgrounds));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Settings", "display", "display/settings", ControlPanelSystem.Menu.DisplaySettings));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Scaling", "display", "display/scaling", ControlPanelSystem.Menu.Scaling));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Screen Saver", "display", "display/screensaver", ControlPanelSystem.Menu.ScreenSaver));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Font", "display", "display/font", ControlPanelSystem.Menu.Font));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Color", "display", "display/color", ControlPanelSystem.Menu.Color));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Desktop", "display", "display/desktop", ControlPanelSystem.Menu.Desktop));

		AllDirectoryFilePaths.Add(new ControlPanelSystem("<- Back", "display/color", "display", ControlPanelSystem.Menu.Home));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Font", "display/color", "display/color/font", ControlPanelSystem.Menu.FontColor));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Button", "display/color", "display/color/button", ControlPanelSystem.Menu.ButtonColor));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Window", "display/color", "display/color/window", ControlPanelSystem.Menu.WindowColor));

		AllDirectoryFilePaths.Add(new ControlPanelSystem("<- Back", "account", "home", ControlPanelSystem.Menu.Home));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Change Profile Pic", "account", "account/picture", ControlPanelSystem.Menu.ProfilePic));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Change Password", "account", "account/password", ControlPanelSystem.Menu.Password));
		AllDirectoryFilePaths.Add(new ControlPanelSystem("Change Hint", "account", "account/hint", ControlPanelSystem.Menu.Hint));

		AllDirectoryFilePaths.Add(new ControlPanelSystem("Documents", "default_programs", "default_programs/docs", ControlPanelSystem.Menu.DefaultDocuments));

		AllDirectoryFilePaths.Add(new ControlPanelSystem("Themes", "dev", "dev/themes", ControlPanelSystem.Menu.Themes));
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
			winman.SelectedWID = WindowID;
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

						if (!pwinman.RunningPrograms[i].windowRect.Contains(Event.current.mousePosition))
						{
							if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
							{

							}
						}
					}
				}
			}
		}

		if(CurrentPath == "")
        {
			CurrentPath = "home";
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
				winman.WindowResize(PersonName, SelectedWindowID);

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
							LocalRegistry.SetRectData(PersonName, pwinman.RunningPrograms[i].WPN, "Control Panel", "Window", new SRect(pwinman.RunningPrograms[i].windowRect));
							RenderTitle(pwinman.RunningPrograms[i].WPN);
							RenderGrid(pwinman.RunningPrograms[i].WPN);
						}
					}
				}
			}
		}
	}

	void RenderTitle(int WPN)
    {
		Rect closebuttonpos = new Rect(LocalRegistry.GetRectData(PersonName, WPN, "Control Panel", "Window").width - 23, 2, 21, 21);
		Rect addressbarbox = new Rect(153, 2, LocalRegistry.GetRectData(PersonName, WPN, "Control Panel", "Window").width-177, 21);
		GUI.DragWindow(new Rect(2, 2, 150, 21));
		//winman.WindowDragging(SelectedWindowID, new Rect(40, 2, CloseButton.x - 41, 21));

		GUI.Box(new Rect(2, 2, 150, 21), ProgramNameForWinMan);

		CurrentPath = GUI.TextField(addressbarbox, CurrentPath);

		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(closebuttonpos, "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
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

			if (GUI.Button(closebuttonpos, "X", com.Skin[GameControl.control.GUIID].customStyles[1]))
			{
				Close(SelectedWindowID);
			}
		}
	}

	void RenderGrid(int WPN)
	{
		int rows = 0;
		float x = 0;
		float y = 0;

		for (int i = 0; i < AllDirectoryFilePaths.Count; i++)
		{
			if (AllDirectoryFilePaths[i].Location == CurrentPath)
			{
				CurDirectoryFilePaths.Add(AllDirectoryFilePaths[i]);
			}
		}

        scrollpos = GUI.BeginScrollView(new Rect(1, 25, LocalRegistry.GetRectData(PersonName, WPN, "Control Panel", "Window").width - 3, LocalRegistry.GetRectData(PersonName, WPN, "Control Panel", "Window").height - 32), scrollpos, new Rect(0, 0, 0, scrollsize * 24));
        for (scrollsize = 0; scrollsize < CurDirectoryFilePaths.Count; scrollsize++)
        {
            MaxIconsPerRow = ((int)LocalRegistry.GetRectData(PersonName, WPN, "Control Panel", "Window").width) / (120 + 1);
            if (MaxIconsPerRow <= 1)
            {
                MaxIconsPerRow = 1;
                if (GUI.Button(new Rect(1 + x, y, LocalRegistry.GetRectData(PersonName, WPN, "Control Panel", "Window").width - 15, 23), CurDirectoryFilePaths[scrollsize].Name))
                {
					CurrentPath = CurDirectoryFilePaths[scrollsize].Target;
				}
            }
            else
            {
                if (GUI.Button(new Rect(1 + x,y, 120, 23), CurDirectoryFilePaths[scrollsize].Name))
                {
                    CurrentPath = CurDirectoryFilePaths[scrollsize].Target;
                }
            }
            rows++;
            x += 120 + 1;
            if (rows == MaxIconsPerRow)
            {
                rows = 0;
                x = 0;
                y += 1 + 23;
            }
        }
        GUI.EndScrollView();

        //for (int i = 0; i < CurDirectoryFilePaths.Count; i++)
        //{
        //    MaxIconsPerRow = ((int)LocalRegistry.GetRectData(PersonName, WPN, "Control Panel", "Window").width) / (120 + 1);
        //    if (MaxIconsPerRow <= 1)
        //    {
        //        MaxIconsPerRow = 1;
        //        if (GUI.Button(new Rect(2 + x, y + 30, LocalRegistry.GetRectData(PersonName, WPN, "Control Panel", "Window").width - 15, 24), CurDirectoryFilePaths[i].Name))
        //        {
        //            CurrentPath = CurDirectoryFilePaths[i].Target;
        //        }
        //    }
        //    else
        //    {
        //        if (GUI.Button(new Rect(2 + x, y + 30, 120, 24), CurDirectoryFilePaths[i].Name))
        //        {
        //            CurrentPath = CurDirectoryFilePaths[i].Target;
        //        }
        //    }
        //    rows++;
        //    x += 120 + 1;
        //    if (rows == MaxIconsPerRow)
        //    {
        //        rows = 0;
        //        x = 0;
        //        y += 1 + 24;
        //    }
        //}

        CurDirectoryFilePaths.RemoveRange(0, CurDirectoryFilePaths.Count);
	}

	void RenderMenu()
    {
		SelectedMenu = AllDirectoryFilePaths[0].TargetMenu.ToString();
    }

	void DisplaySettings(int i)
	{

	}
}
