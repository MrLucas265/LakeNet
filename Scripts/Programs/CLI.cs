//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class CLI : MonoBehaviour
//{
//	public int windowID;
//	public Rect windowRect = new Rect(100, 100, 200, 200);
//	public float native_width = 1920;
//	public float native_height = 1080;
//	public bool show;
//	public Vector2 scrollpos = Vector2.zero;
//	public int scrollsize;

//	public bool terminal;

//	public int TempValue;
//	public int PastCommandSelect;

//	public bool minimize;
//	public Rect CloseButton;
//	public Rect MiniButton;
//	public Rect DefaltSetting;
//	public Rect DefaltBoxSetting;

//	private Defalt def;
//	private CLICommands cli;
//	private SoundControl sc;
//	private Computer com;
//	private AppMan appman;

//	private GameObject prompt;
//	private GameObject system;

//	public bool KeyPressed;
//	public string KeyName;

//	public AudioClip AudioClips;
//	public AudioSource AudioSoucres;



//	// Use this for initialization
//	void Start () 
//	{
//		prompt = GameObject.Find("Prompts");
//		system = GameObject.Find("System");
//		AfterStart();
//	}

//	void AfterStart()
//	{
//		def = system.GetComponent<Defalt>();
//		com = system.GetComponent<Computer>();
//		sc = system.GetComponent<SoundControl>();
//		cli = GetComponent<CLICommands>();
//		appman = GetComponent<AppMan>();

//		native_height = Customize.cust.native_height;
//		native_width = Customize.cust.native_width;

//		PosCheck();
//	}

//	void PosCheck()
//	{
//		if (Customize.cust.windowx[windowID] == 0) 
//		{
//			if (Customize.cust.windowy[windowID] == 0) 
//			{
//				Customize.cust.windowx [windowID] = Screen.width / 2;
//				Customize.cust.windowy [windowID] = Screen.height / 2;
//			}
//		}

//		windowRect.x = Customize.cust.windowx[windowID];
//		windowRect.y = Customize.cust.windowy[windowID];

//		SetPos();
//	}

//	void SetPos()
//	{
//		CloseButton = new Rect (328,1,21,21);
//		MiniButton = new Rect (307,1,21,21);
//		DefaltSetting = new Rect (0,1,350,235);
//		DefaltBoxSetting = new Rect (1,1,307,21);
//	}

//	void Minimize()
//	{
//		if (minimize == true) 
//		{
//			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,23));
//		}
//		else
//		{
//			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,DefaltSetting.height));
//		}
//	}

//	void OnGUI()
//	{
//		if (terminal == false)
//		{
//			GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
//		}

//		Customize.cust.windowx[windowID] = windowRect.x;
//		Customize.cust.windowy[windowID] = windowRect.y;

//		if(show == true)
//		{
//			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
//			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
//		}
//	}

//	void DoMyWindow(int WindowID)
//	{
//		if (CloseButton.Contains (Event.current.mousePosition)) 
//		{
//			if (GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [0])) 
//			{
//				appman.SelectedApp = "Command Line";
//			}
//		} 
//		else
//		{
//			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
//			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
//			GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [1]);
//		}

//		if (MiniButton.Contains (Event.current.mousePosition)) 
//		{
//			if (GUI.Button (new Rect (MiniButton), "-",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
//			{
//				minimize = !minimize;
//				Minimize();
//			}
//		} 
//		else
//		{
//			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
//			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
//			if (GUI.Button (new Rect (MiniButton), "-",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
//			{
//				minimize = !minimize;
//				Minimize();
//			}
//		}

//		if (cli.PastCommands.Count > 200) 
//		{
//			cli.PastCommands.RemoveAt (0);
//		}

//		if (cli.AutoScroll == true) 
//		{
//			scrollpos.y = scrollsize*20;
//			cli.AutoScroll = false;
//		}

//		if (Event.current.type == EventType.KeyDown) 
//		{
//			AudioSoucres.pitch = Random.Range (0.96f, 1.04f);
//			AudioSoucres.PlayOneShot (AudioClips);
//			//AudioSoucres.pitch = 1;
//		}

//		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return) 
//		{
//			cli.PastCommands.Add (cli.Parse);
//			cli.CommandCheck();
//			cli.Parse = "";
//			cli.AutoScroll = true;
//		}

//		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.DownArrow) 
//		{
//			if (PastCommandSelect < scrollsize - 1)
//			{
//				PastCommandSelect++;
//				cli.Parse = cli.PastCommands[PastCommandSelect];
//			}
//		}

//		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.UpArrow) 
//		{
//			if (PastCommandSelect >= 1)
//			{
//				PastCommandSelect--;
//				cli.Parse = cli.PastCommands[PastCommandSelect];
//			}
//		}

//		scrollpos = GUI.BeginScrollView(new Rect(2, 25, 344, 180), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
//		for (scrollsize = 0; scrollsize < cli.PastCommands.Count; scrollsize++)
//		{
//			GUI.Label (new Rect (2, scrollsize * 20, 325, 25), "" + cli.PastCommands [scrollsize]);
//		}
//		GUI.EndScrollView();

//		cli.Parse = GUI.TextField(new Rect(5, 207, 340, 23), cli.Parse, 500);



//		GUI.DragWindow(new Rect(DefaltBoxSetting));
//		GUI.Box(new Rect(DefaltBoxSetting), "Command-Line Interface V2.0");
//	}
//}
