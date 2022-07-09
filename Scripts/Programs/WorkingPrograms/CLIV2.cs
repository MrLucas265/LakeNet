using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLIV2 : MonoBehaviour
{
	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool show;
	//public Vector2 scrollpos = Vector2.zero;
	//public int scrollsize;

	public bool terminal;

	public int TempValue;
	//public int PastCommandSelect;

	public bool minimize;
	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	//public Rect DefaltBoxSetting;

	private Defalt def;
	private CLICommandsV2 cli;
	private SoundControl sc;
	private Computer com;
	private AppMan appman;

	private GameObject WindowHandel;
	private GameObject prompt;
	private GameObject system;

	public bool KeyPressed;
	public string KeyName;

	public AudioClip AudioClips;
	public AudioSource AudioSoucres;

	public GUISkin Skin;
	public GUIStyle Style;
	public string Mode;

	Boot boot;

	public string User;

	public int Zc;

	public float HMod;
	public float SMod;

    public float ScrollValue;

	private WindowManager winman;

	public string PersonName;
	public string ProgramName;
	public string ProgramNameForWinMan;
	public string ContextMenuName;
	public List<string> ContextMenuOptions = new List<string>();
	public string SelectedOption;
	public int SelectedProgramID;
	public int SelectedWindowID;
	public bool quit;

	// Use this for initialization
	void Start () 
	{
		prompt = GameObject.Find("Prompts");
		system = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");

		HMod = 20;
		
		AfterStart();

		ProgramNameForWinMan = "CLI";
		ContextMenuName = "CLI Context Menu";

		ProgramName = "CLI";
		PersonName = "Player";
    }

	void AfterStart()
	{
		def = system.GetComponent<Defalt>();
		com = system.GetComponent<Computer>();
		sc = system.GetComponent<SoundControl>();
		cli = GetComponent<CLICommandsV2>();
		appman = GetComponent<AppMan>();
		boot = GetComponent<Boot>();

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		winman = WindowHandel.GetComponent<WindowManager>();

		PosCheck();
	}

	public void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0) 
		{
			if (Customize.cust.windowy[windowID] == 0) 
			{
				Customize.cust.windowx [windowID] = Screen.width / 2;
				Customize.cust.windowy [windowID] = Screen.height / 2;
			}
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		SetPos();
	}

	void SetPos()
	{
		switch (Customize.cust.Mode)
		{
			case "Small":
				windowRect.width = 350;
				windowRect.height = 235;
				break;
			case "Medium":
				windowRect.width = 450;
				windowRect.height = 350;
				break;
			case "Large":
				windowRect.width = 600;
				windowRect.height = 500;
				break;
			case "Terminal":
				windowRect.width = Screen.width;
				windowRect.height = Screen.height;
				break;
		}

		//if (terminal == true)
		//{
		//	windowRect.width = Screen.width;
		//	windowRect.height = Screen.height;
		//}

		CloseButton = new Rect (windowRect.width-22,1,21,21);
		MiniButton = new Rect (windowRect.width-44,1,21,21);
		DefaltSetting = new Rect(0, 1, windowRect.width, windowRect.height);
	}

	void OnGUI()
	{
		GUI.skin = com.Skin[Registry.GetIntData(PersonName, ProgramName, "Skin")];

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						ColorUI(pwinman.RunningPrograms[i].WPN);
						GUI.color = new Color32(LocalRegistry.GetRedColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"), LocalRegistry.GetGreenColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"), LocalRegistry.GetBlueColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"), LocalRegistry.GetAlphaColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"));
						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));

						if (!pwinman.RunningPrograms[i].windowRect.Contains(Event.current.mousePosition))
						{
							if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
							{
								CloseContextMenu();
							}
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
							SelectedProgramID = pwinman.RunningPrograms[i].PID;
						}

						if (WindowID == pwinman.RunningPrograms[i].WID)
						{
							CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 22, 1, 21, 21);
							if (CloseButton.Contains(Event.current.mousePosition))
							{
								GUI.backgroundColor = Color.red;
								GUI.contentColor = Color.white;

								if (GUI.Button(new Rect(CloseButton), "X", com.Skin[Registry.GetIntData(PersonName,ProgramName,"Skin")].customStyles[0]))
								{
									WindowManager.QuitProgram(PersonName,ProgramName, pwinman.RunningPrograms[i].WPN);
								}

								GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
								GUI.contentColor = com.colors[Customize.cust.FontColorInt];
							}
							else
							{
								GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
								GUI.contentColor = com.colors[Customize.cust.FontColorInt];

								if (GUI.Button(new Rect(CloseButton), "X", com.Skin[Registry.GetIntData(PersonName, ProgramName, "Skin")].customStyles[1]))
								{
									WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
								}
							}
							RenderTitleBar(pwinman.RunningPrograms[i].WPN, pwinman.RunningPrograms[i].WindowName);
							RenderUI(pwinman.RunningPrograms[i].WPN);
						}
					}
				}
			}
		}
	}

	void RenderTitleBar(int WindowID,string WindowName)
    {
		GUI.contentColor = new Color32(LocalRegistry.GetRedColorData(PersonName, WindowID, ProgramName, "FontColor"), LocalRegistry.GetGreenColorData(PersonName, WindowID, ProgramName, "FontColor"), LocalRegistry.GetBlueColorData(PersonName, WindowID, ProgramName, "FontColor"), LocalRegistry.GetAlphaColorData(PersonName, WindowID, ProgramName, "FontColor"));

		if (Registry.GetBoolData("Player", "CLI", "Pinned") == false)
		{
			GUI.DragWindow(new Rect(1, 1, LocalRegistry.GetRectData(PersonName, WindowID, ProgramName, "TextFieldPos").width-22, 21));
		}
		GUI.Box(new Rect(1, 1, LocalRegistry.GetRectData(PersonName, WindowID, ProgramName, "TextFieldPos").width-22, 21), WindowName);
	}

	void ColorUI(int WPN)
	{
		LocalRegistry.SetColorData(PersonName, WPN, ProgramName, "FontColor", new SColor(new Color32(0, 255, 0, 255)));
		LocalRegistry.SetFloatColorData(PersonName, WPN, ProgramName, "FontColor", new ColorSystem(0, 255, 0, 255));

		LocalRegistry.SetColorData(PersonName, WPN, ProgramName, "WindowColor", new SColor(new Color32(0, 0, 0, 255)));
		LocalRegistry.SetFloatColorData(PersonName, WPN, ProgramName, "WindowColor",new ColorSystem(0, 0, 0, 255));
	}

	void RenderUI(int WindowID)
	{
		//GUI.contentColor = new Color32(Registry.GetRedColorData(PersonName, ProgramName, "FontColor"), Registry.GetGreenColorData(PersonName, ProgramName, "FontColor"), Registry.GetBlueColorData(PersonName, ProgramName, "FontColor"), Registry.GetAlphaColorData(PersonName, ProgramName, "FontColor"));

		if (LocalRegistry.GetStringDataCount(PersonName, WindowID, ProgramName, "Input") > Customize.cust.DeletionAmt)
		{
			LocalRegistry.RemoveAtStringListData(PersonName, WindowID, ProgramName, "CommandHistory",0);
		}

		if (cli.AutoScroll == true)
		{
			LocalRegistry.SetVector2Data(PersonName, WindowID, ProgramName, "ScrollPos", new SVector2(new Vector2(0, LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "Scrollsize") * 20)));
			//localr.y = scrollsize * 20;
			cli.AutoScroll = false;
		}

		if (cli.SetScrollPos == true)
		{
			LocalRegistry.SetVector2Data(PersonName, WindowID, ProgramName, "ScrollPos", new SVector2(new Vector2(0, LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "Scrollsize") * 20) / ScrollValue));
			cli.SetScrollPos = false;
		}

		Style.fontSize = Customize.cust.TerminalFontSize;
		//if (Event.current.type == EventType.KeyDown) 
		//{
		//	AudioSoucres.pitch = Random.Range (0.96f, 1.04f);
		//	AudioSoucres.PlayOneShot (AudioClips);
		//	//AudioSoucres.pitch = 1;
		//}

		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return)
		{
			//cli.PastCommands.Add (cli.Parse);
			//cli.CommandCheck();
			//cli.Parse = "";
			//pastcommands.Add();
			if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramName, "Input") != "")
			{
				LocalRegistry.AddStringData(PersonName, WindowID, ProgramName, "CommandHistory", LocalRegistry.GetStringData(PersonName, WindowID, ProgramName, "Input"));
				GlobalStuff.RunCommand(PersonName, WindowID, ProgramName, "Input", LocalRegistry.GetStringData(PersonName, WindowID, ProgramName, "Input"));
			}
			if(LocalRegistry.GetStringData(PersonName, WindowID, ProgramName, "Output") != "")
            {
				LocalRegistry.AddStringData(PersonName, WindowID, ProgramName, "CommandHistory", LocalRegistry.GetStringData(PersonName, WindowID, ProgramName, "Output"));
			}
			LocalRegistry.SetStringData(PersonName, WindowID, ProgramName, "Input", "");
			LocalRegistry.SetStringData(PersonName, WindowID, ProgramName, "Output", "");

			for (int i = 0; i < 8; i++)
			{
				LogitechGSDK.LogiLcdColorSetText(i, "", 0, 0, 0);
			}

			LogitechGSDK.LogiLcdUpdate();
		}

		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.DownArrow)
		{
			if (LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "SelectedPastCommand") < LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "Scrollsize") - 1)
			{
				LocalRegistry.SetIntData(PersonName, WindowID, ProgramName, "SelectedPastCommand", LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "SelectedPastCommand") +1);
				LocalRegistry.SetStringData(PersonName, WindowID, ProgramName, "Input",LocalRegistry.GetStringListData(PersonName, WindowID, ProgramName, "CommandHistory", LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "SelectedPastCommand")));
			}
		}

        if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.UpArrow)
        {
            if (LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "SelectedPastCommand") >= 1)
            {
				LocalRegistry.SetIntData(PersonName, WindowID, ProgramName, "SelectedPastCommand", LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "SelectedPastCommand") - 1);
				LocalRegistry.SetStringData(PersonName, WindowID, ProgramName, "Input", LocalRegistry.GetStringListData(PersonName, WindowID, ProgramName, "CommandHistory", LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "SelectedPastCommand")));
			}
        }

        //Customize.cust.TerminalTextPosMod = SMod * User.Length;


		if(LocalRegistry.GetStringDataCount(PersonName, WindowID, ProgramName, "CommandHistory") > 0)
        {
			LocalRegistry.SetVector2Data(PersonName, WindowID, ProgramName, "ScrollPos", GUI.BeginScrollView(new Rect(2, 25, winman.GetWindowInfo(PersonName, WindowID).width - 20, winman.GetWindowInfo(PersonName, WindowID).height - (Customize.cust.FontSize + 10) * 2), LocalRegistry.GetVector2Data(PersonName, WindowID, ProgramName, "ScrollPos"), new Rect(0, 0, 0, LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "Scrollsize") * 24)));

			//GUI.Label (new Rect (2, windowRect.height - 50, windowRect.width-2, Customize.cust.FontSize+2),User,Style);

			//cli.Parse = GUI.TextField(new Rect(User.Length + Customize.cust.TerminalTextPosMod * Customize.cust.TerminalFontSize, scrollsize*20-1*SMod, windowRect.width-84, 23), cli.Parse, 500,Style);
			//cli.Parse = GUI.TextField(new Rect(User.Length + Customize.cust.TerminalTextPosMod * Customize.cust.TerminalFontSize, windowRect.height - 50, windowRect.width-84, Customize.cust.FontSize+2), cli.Parse, 500,Style);

			for (LocalRegistry.SetIntData(PersonName, WindowID, ProgramName, "Scrollsize", 0); LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "Scrollsize") < LocalRegistry.GetStringDataCount(PersonName, WindowID, ProgramName, "CommandHistory"); LocalRegistry.SetIntData(PersonName, WindowID, ProgramName, "Scrollsize", LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "Scrollsize") + 1))
			{
				GUI.Label(new Rect(2, LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "Scrollsize") * 24, windowRect.width - 2, 20), "" + LocalRegistry.GetStringListData(PersonName, WindowID, ProgramName, "CommandHistory", LocalRegistry.GetIntData(PersonName, WindowID, ProgramName, "Scrollsize")), Style);
			}

			GUI.EndScrollView();
		}


		LocalRegistry.SetRectData(PersonName, WindowID, ProgramName, "TextFieldPos", new Rect(2, winman.GetWindowInfo(PersonName, WindowID).height-20, winman.GetWindowInfo(PersonName, WindowID).width-2, 22));
		GUI.Label(new Rect(2, LocalRegistry.GetRectData(PersonName, WindowID, ProgramName, "TextFieldPos").y, LocalRegistry.GetRectData(PersonName, WindowID, ProgramName, "TextFieldPos").width, Customize.cust.FontSize + 2), ">", Style);

		LocalRegistry.SetRectData(PersonName, WindowID, ProgramName, "TextFieldPos", new Rect(12, winman.GetWindowInfo(PersonName, WindowID).height-20, winman.GetWindowInfo(PersonName, WindowID).width-2, 22));
		//LocalRegistry.SetRectData(PersonName, WindowID, ProgramName, "TextFieldPos", new Rect(12, winman.GetWindowInfo(PersonName, WindowID).height / 2, winman.GetWindowInfo(PersonName, WindowID).height - 2, 23));

		LocalRegistry.SetStringData(PersonName, WindowID, ProgramName, "Input", GUI.TextField(new Rect(12, LocalRegistry.GetRectData(PersonName, WindowID, ProgramName, "TextFieldPos").y, LocalRegistry.GetRectData(PersonName, WindowID, ProgramName, "TextFieldPos").width, Customize.cust.FontSize + 2), LocalRegistry.GetStringData(PersonName, WindowID, ProgramName, "Input"), 500, Style));
	}

	void Minimize()
	{
		if (minimize == true)
		{
			if(Registry.GetBoolData("Player","CLI","Pinned") == false)
            {
				Registry.SetRectData("Player", "CLI", "CustomPos", new SRect(windowRect));
			}
			Registry.SetBoolData("Player", "CLI", "Pinned", minimize);
			//windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,23));
			windowRect = Registry.GetRectData("Player", "CLI", "Pinned");
		}
		else
		{
			Registry.SetBoolData("Player", "CLI", "Pinned", minimize);
			windowRect = Registry.GetRectData("Player", "CLI", "CustomPos");
			//windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,DefaltSetting.height));
		}
	}

	//void OnGUI()
	//{
	//	Skin = com.Skin[GameControl.control.GUIID];
	//	GUI.skin = Skin;

	//	Customize.cust.windowx[windowID] = windowRect.x;
	//	Customize.cust.windowy[windowID] = windowRect.y;

	//	if(show == true)
	//	{
	//		GUI.color = Color.black;
	//		windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
	//	}
	//   }

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
}
