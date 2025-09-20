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
	//private FileExplorer fp;
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

	public List<ControlPanelSystem> AllDirectoryFilePaths = new List<ControlPanelSystem>();
	public List<ControlPanelSystem> CurDirectoryFilePaths = new List<ControlPanelSystem>();
	public string CurrentPath;

	public string SelectedMenu;

	public int PosModX;

	public string ProgramName;
	// Use this for initialization
	void Start()
	{
		ProgramNameForWinMan = "ControlPanel";
		//ProgramName = "FileManager";

		Puter = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");
		com = Puter.GetComponent<Computer>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		//fp = Puter.GetComponent<FileExplorer>();
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

	void StringNullCheck(int WPN)
    {
		LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "Window", LocalRegistry.GetStringData(PersonName, WPN, "ControlPanel", "Window"));
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
					if (pwinman.RunningPrograms[i].ProcessName == ProgramNameForWinMan)
					{
						GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));


						StringNullCheck(pwinman.RunningPrograms[i].WPN);

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
	}

	void SetID()
	{
		int count = 0;
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			for (int j = 0; j < pwinman.RunningPrograms.Count; j++)
			{
				if (pwinman.RunningPrograms[j].ProcessName == ProgramNameForWinMan)
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
                if (WindowID == Registry.GetIntData(PersonName, "WindowManager", "SelectedWindow"))
                {
                    winman.WindowResize(PersonName, Registry.GetIntData(PersonName, "WindowManager", "SelectedWindow"));
                }

                for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProcessName == ProgramNameForWinMan)
					{
						if (pwinman.RunningPrograms[i].WID == SelectedWindowID)
						{
							SelectedProgram = i;
						}

						if (WindowID == pwinman.RunningPrograms[i].WID)
						{
							if(Registry.GetBoolData("Player", pwinman.RunningPrograms[i].ProcessName, "ShowResize") == true)
							{
                                GUI.Box(LocalRegistry.GetRectData("Player", pwinman.RunningPrograms[i].WPN, pwinman.RunningPrograms[i].ProcessName, "Resize"), "");
                            }
                            LocalRegistry.SetRectData(PersonName, pwinman.RunningPrograms[i].WPN, pwinman.RunningPrograms[i].ProcessName, "Window", new SRect(pwinman.RunningPrograms[i].windowRect));
							RenderTitle(pwinman.RunningPrograms[i].ProgramName, pwinman.RunningPrograms[i].WPN, pwinman.RunningPrograms[i].WID);
							RenderGrid(pwinman.RunningPrograms[i].WPN);
						}
					}
				}
			}
		}
	}

	void RenderTitle(string Name,int WPN,int WID)
    {
        if(LocalRegistry.GetBoolData(PersonName, WPN, "ControlPanel", "InitalRun") == false)
		{
			LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "TypedAddress", "home");
            LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "Window", LocalRegistry.GetStringData(PersonName, WPN, "ControlPanel", "TypedAddress"));
			LocalRegistry.SetBoolData(PersonName, WPN, "ControlPanel", "InitalRun", true);
        }

        Rect WindowRectInfo = LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Window");
		float Math = WindowRectInfo.width / 3f;
		Rect titlebar = new Rect(2, 2, Math, 21);
		Rect closebuttonpos = new Rect(WindowRectInfo.width - 23, 2, 21, 21);
		Rect AddressBarEnterBox = new Rect(closebuttonpos.x - 22, 2, 21, 21);
        Rect AddressBarHomeBox = new Rect(AddressBarEnterBox.x - 22, 2, 21, 21);
        float StartPos = 2 + titlebar.width + 1;
		Rect addressbarbox = new Rect(StartPos, 2, AddressBarEnterBox.x-StartPos-24, 21);
		GUI.DragWindow(titlebar);
		//winman.WindowDragging(SelectedWindowID, new Rect(40, 2, CloseButton.x - 41, 21));
		if (Math < 200)
        {
			GUI.Box(new Rect(titlebar),LocalRegistry.GetStringData(PersonName, WPN, "ControlPanel", "Window"));
		}
		else
        {
			GUI.Box(new Rect(titlebar), Name + "-" + LocalRegistry.GetStringData(PersonName, WPN, "ControlPanel", "Window"));
		}

		LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "TypedAddress",GUI.TextField(addressbarbox, LocalRegistry.GetStringData(PersonName, WPN, "ControlPanel", "TypedAddress")));

        if (GUI.Button(AddressBarEnterBox, "E"))
        {
            LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "Window", LocalRegistry.GetStringData(PersonName, WPN, "ControlPanel", "TypedAddress"));
        }

        if (GUI.Button(AddressBarHomeBox, "H"))
		{
            LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "Window", "home");
            LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "TypedAddress", "home");
		}

		if (closebuttonpos.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(closebuttonpos, "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
			{
				WindowManager.QuitProgram(PersonName, ProgramNameForWinMan, WPN);
			}

			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
		}
		else
		{
			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

			GUI.Box(closebuttonpos, "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]);
		}
	}

	void RenderGrid(int WPN)
	{
		LocalRegistry.SetIntData(PersonName, WPN, "ControlPanel", "RowCount", 0);
		LocalRegistry.SetVector2Data(PersonName, WPN, "ControlPanel", "RowCount", new SVector2(new Vector2(0, 0)));
		LocalRegistry.SetFloatData(PersonName, WPN, "ControlPanel", "RowCount", 0);

        for (int i = 0; i < AllDirectoryFilePaths.Count; i++)
		{
			if (AllDirectoryFilePaths[i].Location == LocalRegistry.GetStringData(PersonName, WPN, "ControlPanel", "Window"))
			{
				CurDirectoryFilePaths.Add(AllDirectoryFilePaths[i]);
			}
		}

        LocalRegistry.SetVector2Data(PersonName, WPN, "ControlPanel", "Window", GUI.BeginScrollView(new Rect(1, 25, LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Window").width - 3, LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Window").height - 32 - LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Resize").height), LocalRegistry.GetVector2Data(PersonName, WPN, "ControlPanel", "Window"), new Rect(0, 0, 0, LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "Window") * 24)));

        //LocalRegistry.SetVector2Data(PersonName, WPN, "ControlPanel", "Window",GUI.BeginScrollView(new Rect(1, 25, LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Window").width - 3, LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Window").height - 32 - LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Resize").height), LocalRegistry.GetVector2Data(PersonName, WPN, "ControlPanel", "Window"), new Rect(0, 0, 0, LocalRegistry.GetFloatData(PersonName, WPN, "ControlPanel", "ScrollRect"))));
		for (LocalRegistry.SetIntData(PersonName, WPN, "ControlPanel", "Window", 0); LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "Window") < CurDirectoryFilePaths.Count; LocalRegistry.SetIntData(PersonName, WPN, "ControlPanel", "Window", LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "Window")+1))
        {
			LocalRegistry.SetIntData(PersonName, WPN, "ControlPanel", "MaxPerRowGrid", ((int)LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Window").width) / (120 + 1));
            if (LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "MaxPerRowGrid") <= 1)
            {
				LocalRegistry.SetIntData(PersonName, WPN, "ControlPanel", "MaxPerRowGrid", 1);
                if (GUI.Button(new Rect(1 + LocalRegistry.GetVector2Data(PersonName, WPN, "ControlPanel", "RowCount").x, LocalRegistry.GetVector2Data(PersonName, WPN, "ControlPanel", "RowCount").y, LocalRegistry.GetRectData(PersonName, WPN, "ControlPanel", "Window").width - 15, 23), CurDirectoryFilePaths[LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "Window")].Name))
                {
					LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "Window",CurDirectoryFilePaths[LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "Window")].Target);
					LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "TypedAddress", CurDirectoryFilePaths[LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "TypedAddress")].Target);
				}
            }
            else
            {
                if (GUI.Button(new Rect(1 + LocalRegistry.GetVector2Data(PersonName, WPN, "ControlPanel", "RowCount").x, LocalRegistry.GetVector2Data(PersonName, WPN, "ControlPanel", "RowCount").y, 120, 23), CurDirectoryFilePaths[LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "Window")].Name))
                {
					LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "Window", CurDirectoryFilePaths[LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "Window")].Target);
					LocalRegistry.SetStringData(PersonName, WPN, "ControlPanel", "TypedAddress", CurDirectoryFilePaths[LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "TypedAddress")].Target);
				}
            }
            LocalRegistry.SetIntData(PersonName, WPN, "ControlPanel", "RowCount", LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "RowCount") + 1);
            LocalRegistry.SetVector2XData(PersonName, WPN, "ControlPanel", "RowCount", LocalRegistry.GetVector2Data(PersonName, WPN, "ControlPanel", "RowCount").x += 120 + 1);
            if (LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "RowCount") == LocalRegistry.GetIntData(PersonName, WPN, "ControlPanel", "MaxPerRowGrid"))
            {
                LocalRegistry.SetIntData(PersonName, WPN, "ControlPanel", "RowCount", 0);
                LocalRegistry.SetVector2XData(PersonName, WPN, "ControlPanel", "RowCount", 0);
                LocalRegistry.SetVector2YData(PersonName, WPN, "ControlPanel", "RowCount", LocalRegistry.GetVector2Data(PersonName, WPN, "ControlPanel", "RowCount").y += 1 + 23);
            }
        }

        GUI.EndScrollView();

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
