using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLIV4 : MonoBehaviour
{
	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool show;
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public bool terminal;

	public int TempValue;
	public int PastCommandSelect;

	public bool minimize;
	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

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

	public string playerinput;

	Boot boot;

	public string User;

	public int Zc;

	public float HMod;
	public float SMod;

	public float ScrollValue;

	private WindowManager winman;


	// Use this for initialization
	void Start()
	{
		prompt = GameObject.Find("Prompts");
		system = GameObject.Find("System");

		HMod = 20;

		windowID = 55;

		AfterStart();
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

		PosCheck();
	}

	public void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0)
		{
			if (Customize.cust.windowy[windowID] == 0)
			{
				Customize.cust.windowx[windowID] = Screen.width / 2;
				Customize.cust.windowy[windowID] = Screen.height / 2;
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

		CloseButton = new Rect(windowRect.width - 22, 1, 21, 21);
		MiniButton = new Rect(windowRect.width - 44, 1, 21, 21);
		DefaltSetting = new Rect(0, 1, windowRect.width, windowRect.height);
		DefaltBoxSetting = new Rect(1, 1, windowRect.width - 46, 21);
	}

	void Minimize()
	{
		if (minimize == true)
		{
			if (Registry.GetBoolData("Player", "CLI", "Pinned") == false)
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

	void OnGUI()
	{
		Skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
		GUI.skin = Skin;

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if (show == true)
		{
			GUI.color = Color.black;
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
		}

		if (GameControl.control.LCDPage > 100 && GameControl.control.LCDPage < 150)
		{
			LogitechGSDK.LogiLcdColorSetTitle("CLI", 0, 255, 0);
			LogitechGSDK.LogiLcdColorSetText(7, "> " + cli.Parse, 0, 255, 0);

			if (cli.PastCommands.Count > 0)
			{
				LogitechGSDK.LogiLcdColorSetText(6, cli.PastCommands[cli.PastCommands.Count - 1], 0, 255, 0);
			}
			if (cli.PastCommands.Count > 1)
			{
				LogitechGSDK.LogiLcdColorSetText(5, cli.PastCommands[cli.PastCommands.Count - 2], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(6, cli.PastCommands[cli.PastCommands.Count - 1], 0, 255, 0);
			}
			if (cli.PastCommands.Count > 2)
			{
				LogitechGSDK.LogiLcdColorSetText(4, cli.PastCommands[cli.PastCommands.Count - 3], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(5, cli.PastCommands[cli.PastCommands.Count - 2], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(6, cli.PastCommands[cli.PastCommands.Count - 1], 0, 255, 0);
			}
			if (cli.PastCommands.Count > 4)
			{
				LogitechGSDK.LogiLcdColorSetText(3, cli.PastCommands[cli.PastCommands.Count - 4], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(4, cli.PastCommands[cli.PastCommands.Count - 3], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(5, cli.PastCommands[cli.PastCommands.Count - 2], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(6, cli.PastCommands[cli.PastCommands.Count - 1], 0, 255, 0);
			}
			if (cli.PastCommands.Count > 5)
			{
				LogitechGSDK.LogiLcdColorSetText(2, cli.PastCommands[cli.PastCommands.Count - 5], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(3, cli.PastCommands[cli.PastCommands.Count - 4], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(4, cli.PastCommands[cli.PastCommands.Count - 3], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(5, cli.PastCommands[cli.PastCommands.Count - 2], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(6, cli.PastCommands[cli.PastCommands.Count - 1], 0, 255, 0);
			}
			if (cli.PastCommands.Count > 6)
			{
				LogitechGSDK.LogiLcdColorSetText(1, cli.PastCommands[cli.PastCommands.Count - 6], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(2, cli.PastCommands[cli.PastCommands.Count - 5], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(3, cli.PastCommands[cli.PastCommands.Count - 4], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(4, cli.PastCommands[cli.PastCommands.Count - 3], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(5, cli.PastCommands[cli.PastCommands.Count - 2], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(6, cli.PastCommands[cli.PastCommands.Count - 1], 0, 255, 0);
			}
			if (cli.PastCommands.Count > 7)
			{
				LogitechGSDK.LogiLcdColorSetText(0, cli.PastCommands[cli.PastCommands.Count - 7], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(1, cli.PastCommands[cli.PastCommands.Count - 6], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(2, cli.PastCommands[cli.PastCommands.Count - 5], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(3, cli.PastCommands[cli.PastCommands.Count - 4], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(4, cli.PastCommands[cli.PastCommands.Count - 3], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(5, cli.PastCommands[cli.PastCommands.Count - 2], 0, 255, 0);
				LogitechGSDK.LogiLcdColorSetText(6, cli.PastCommands[cli.PastCommands.Count - 1], 0, 255, 0);
			}
		}
	}

	void DoMyWindow(int WindowID)
	{

		if (cli.PastCommands.Count > Customize.cust.DeletionAmt)
		{
			cli.PastCommands.RemoveAt(0);
		}

		if (cli.AutoScroll == true)
		{
			scrollpos.y = scrollsize * 20;
			cli.AutoScroll = false;
		}

		if (cli.SetScrollPos == true)
		{
			scrollpos.y = scrollsize * 20 / ScrollValue;
			cli.SetScrollPos = false;
		}

		Style.fontSize = Customize.cust.TerminalFontSize;

		//if (Event.current.type == EventType.KeyDown) 
		//{
		//	AudioSoucres.pitch = Random.Range (0.96f, 1.04f);
		//	AudioSoucres.PlayOneShot (AudioClips);
		//	//AudioSoucres.pitch = 1;
		//}

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
		{
			//cli.PastCommands.Add(cli.Parse);
			//cli.CommandCheck();
			//cli.Parse = "";
			//cli.SetScrollPos = true;

			for (int i = 0; i < 8; i++)
			{
				LogitechGSDK.LogiLcdColorSetText(i, "", 0, 0, 0);
			}

			LogitechGSDK.LogiLcdUpdate();
		}

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
		{
			if (PastCommandSelect < scrollsize - 1)
			{
				PastCommandSelect++;
				cli.Parse = cli.PastCommands[PastCommandSelect];
			}
		}

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
		{
			if (PastCommandSelect >= 1)
			{
				PastCommandSelect--;
				cli.Parse = cli.PastCommands[PastCommandSelect];
			}
		}

		//Customize.cust.TerminalTextPosMod = SMod * User.Length;


		if (GameControl.control.GatewayStatus.Terminal == true)
		{
			if (MiniButton.Contains(Event.current.mousePosition))
			{
				if (GUI.Button(new Rect(MiniButton), "-", Skin.customStyles[2]))
				{
					minimize = !minimize;
					Minimize();
				}
			}
			else
			{
				GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
				GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
				if (GUI.Button(new Rect(MiniButton), "-", Skin.customStyles[2]))
				{
					minimize = !minimize;
					Minimize();
				}
			}

			//GUI.DragWindow(new Rect(DefaltBoxSetting));
			GUI.Box(new Rect(DefaltBoxSetting), "CLI Terminal - Safemode");

			TerminalModeV2();
		}
		else
		{
			if (CloseButton.Contains(Event.current.mousePosition))
			{
				if (GUI.Button(new Rect(CloseButton), "X", Skin.customStyles[0]))
				{
					appman.SelectedApp = "CLI";
				}
			}
			else
			{
				GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
				GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
				GUI.Button(new Rect(CloseButton), "X", Skin.customStyles[1]);
			}

			if (MiniButton.Contains(Event.current.mousePosition))
			{
				if (GUI.Button(new Rect(MiniButton), "-", Skin.customStyles[2]))
				{
					minimize = !minimize;
					Minimize();
				}
			}
			else
			{
				GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
				GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
				if (GUI.Button(new Rect(MiniButton), "-", Skin.customStyles[2]))
				{
					minimize = !minimize;
					Minimize();
				}
			}

			if (Registry.GetBoolData("Player", "CLI", "Pinned") == false)
			{
				GUI.DragWindow(new Rect(DefaltBoxSetting));
			}
			GUI.Box(new Rect(DefaltBoxSetting), "Command-Line Interface");

			GUI.contentColor = Color.green;


			scrollpos = GUI.BeginScrollView(new Rect(2, 25, windowRect.width - 4, windowRect.height - 40), scrollpos, new Rect(0, 0, 0, scrollsize * 24));

			//GUI.Label (new Rect (2, windowRect.height - 50, windowRect.width-2, Customize.cust.FontSize+2),User,Style);

			//cli.Parse = GUI.TextField(new Rect(User.Length + Customize.cust.TerminalTextPosMod * Customize.cust.TerminalFontSize, scrollsize*20-1*SMod, windowRect.width-84, 23), cli.Parse, 500,Style);
			//cli.Parse = GUI.TextField(new Rect(User.Length + Customize.cust.TerminalTextPosMod * Customize.cust.TerminalFontSize, windowRect.height - 50, windowRect.width-84, Customize.cust.FontSize+2), cli.Parse, 500,Style);

			for (scrollsize = 0; scrollsize < cli.PastCommands.Count; scrollsize++)
			{
				GUI.Label(new Rect(2, scrollsize * 24, windowRect.width - 2, 20), "" + cli.PastCommands[scrollsize], Style);
			}

			GUI.EndScrollView();
			GUI.Label(new Rect(2, windowRect.height - 20, windowRect.width - 2, Customize.cust.FontSize + 2), ">", Style);

			playerinput = GUI.TextField(new Rect(12, windowRect.height - 20, windowRect.width - 84, Customize.cust.FontSize + 2), playerinput, 500, Style);
		}
	}

	void TerminalModeV1()
	{

		User = "" + GameControl.control.ProfileName + "@" + Customize.cust.GatewayName + ">";

		SMod = Customize.cust.TerminalFontSize / 2 * 0.1f;

		GUI.contentColor = Color.white;

		scrollpos = GUI.BeginScrollView(new Rect(2, 2, windowRect.width - 4, windowRect.height - HMod), scrollpos, new Rect(0, 0, 0, scrollsize * 22));

		GUI.Label(new Rect(2, scrollsize * 20 * SMod, windowRect.width - 2, Customize.cust.FontSize + 2), User, Style);

		//cli.Parse = GUI.TextField(new Rect(User.Length + Customize.cust.TerminalTextPosMod * Customize.cust.TerminalFontSize, scrollsize*20-1*SMod, windowRect.width-84, 23), cli.Parse, 500,Style);
		//cli.Parse = GUI.TextField(new Rect(User.Length + Customize.cust.TerminalTextPosMod * Customize.cust.TerminalFontSize, scrollsize * 20 * SMod, windowRect.width - 84, Customize.cust.FontSize + 2), cli.Parse, 500, Style);

		for (scrollsize = 0; scrollsize < cli.PastCommands.Count; scrollsize++)
		{
			GUI.Label(new Rect(2, scrollsize * 20 * SMod, windowRect.width - 2, 25), "" + cli.PastCommands[scrollsize], Style);
		}

		GUI.EndScrollView();
	}

	void TerminalModeV2()
	{
		GUI.contentColor = Color.white;

		scrollpos = GUI.BeginScrollView(new Rect(2, 25, windowRect.width - 4, windowRect.height - 40), scrollpos, new Rect(0, 0, 0, scrollsize * 24));

		//GUI.Label (new Rect (2, windowRect.height - 50, windowRect.width-2, Customize.cust.FontSize+2),User,Style);

		//cli.Parse = GUI.TextField(new Rect(User.Length + Customize.cust.TerminalTextPosMod * Customize.cust.TerminalFontSize, scrollsize*20-1*SMod, windowRect.width-84, 23), cli.Parse, 500,Style);
		//cli.Parse = GUI.TextField(new Rect(User.Length + Customize.cust.TerminalTextPosMod * Customize.cust.TerminalFontSize, windowRect.height - 50, windowRect.width-84, Customize.cust.FontSize+2), cli.Parse, 500,Style);

		for (scrollsize = 0; scrollsize < cli.PastCommands.Count; scrollsize++)
		{
			GUI.Label(new Rect(2, scrollsize * 24, windowRect.width - 2, 20), "" + cli.PastCommands[scrollsize], Style);
		}

		GUI.EndScrollView();
		GUI.Label(new Rect(2, windowRect.height - 20, windowRect.width - 2, Customize.cust.FontSize + 2), ">", Style);

		cli.Parse = GUI.TextField(new Rect(12, windowRect.height - 20, windowRect.width - 84, Customize.cust.FontSize + 2), cli.Parse, 500, Style);
	}
}
