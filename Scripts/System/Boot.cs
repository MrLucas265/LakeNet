﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Boot : MonoBehaviour
{
	public List<string> BootInfo = new List<string>();
	public float cd;

	public float cd1;

	public bool wait;
	public bool pause;
	public bool booting;

	public bool rebooting;

	public int Index;
	public int Index1;

	public float percentage;
	public float a;
	public float b;

	public float x;
	public float y;
	public float w;
	public float h;

	public float x1;
	public float y2;
	public float w3;
	public float h4;

	public float speedMod;

	public float moveAmt;
	public float MoveMod;
	public float MaxmoveAmt;

	public float BarMod1;
	public float BarMod2;

	public string loginString;

	public Texture2D BGTexture;

	private Computer com;

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool show;
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	private Notepad note;
	private Tracer trace;
	private MissionBrow mb;
	private CurContracts cc;
	private SiteList sl;
	private AccLog al;
	private DirSearch ds;
	private TreeView tv;
	private Clock clk;
	private CD disk;
	private JailDew jd;
	private Unicom uc;
	private Test test;
	private Defalt def;
	private MissionGen mg;
	private DesktopEnviroment os;
	private SystemMap sm;
	private WebSecViewer wsv;
	private ErrorProm ep;
	private ShutdownProm sdp;
	private SystemPanel sp;
	private SoundControl sc;
	private Desktop desk;
	private CustomTheme ct;
	private Mouse mouse;
	private ScreenSaver ss;
	private CLICommandsV2 clic;
	private NotfiPrompt noti;
	private NotificationPrompt notip;
	private SysHardwareCheck shc;

	private GStocks gstocks;

	private GameObject missions;

	private MissionGen missiongen;
	private MissionBrow missionbrow;
	private CurContracts currentcontracts;

	private GameObject Crash;
	private SysCrashMan SCM;

	private GameObject Prompt;

	public GameObject Database;

	private Rect Pos;

	public List<Color> Colors = new List<Color>();
	public Color32 buttonColor = new Color32(0,0,0,0);
	public Color32 fontColor = new Color32(0,0,0,0);
	public Color32 Color1 = new Color32(0,0,0,0);
	public Color32 Color2 = new Color32(0,0,0,0);
	public Color32 Color3 = new Color32(0,0,0,0);

	public bool ColourFade;
	public bool TakingAlpha;
	public bool BackgroundFade;
	public bool BGTakingAlpha;

	public float Timer;
	public float StartTimer;

	public float RotationAngle;
	public float RotationTimer;
	public float RotationCooldown;
	public bool RoationEnable;
	public string RoationPic;

	public bool ShowDesktop;

	public bool PlaySoundOnce;

    public bool TestMode;

    public GUISkin Skin;

	public List<float> LoadingBarXPos = new List<float>();
	public string LoadingBarInfo;

	public bool SetColor;

	public OperatingSystems SelectedOS;

	void Awake()
	{
		Customize.cust.Load();
	}

	//void SelectedOSBootDiskCheck()
	//{
	//	for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
	//	{
	//		if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.OS)
	//		{
	//			if (GameControl.control.ProgramFiles[i].Name == GameControl.control.SelectedOS.Name.ToString())
	//			{
	//				for (int j = 0; j < GameControl.control.Gateway.InstalledStorageDevice.Count; j++)
	//				{
	//					for (int k = 0; k < GameControl.control.Gateway.InstalledStorageDevice[j].Partitions.Count; k++)
	//					{
	//						if (GameControl.control.ProgramFiles[i].Location.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].DriveLetter))
	//						{
	//							GameControl.control.BootTime = GameControl.control.Gateway.InstalledStorageDevice[j].BootTime;
	//						}
	//					}
	//				}
	//			}
	//		}
	//	}

	//	if(GameControl.control.BootTime == 0)
	//	{
	//		GameControl.control.BootTime = GameControl.control.Gateway.InstalledStorageDevice[0].BootTime;
	//	}
	//}

	// Use this for initialization
	void Start ()
	{
		//SelectedOSBootDiskCheck();

		GameControl.control.BootTime = 0.133f;

		Kernal ();

		SetColor = true;

		missions = GameObject.Find ("Missions");
		Prompt = GameObject.Find("Prompts");
		Database = GameObject.Find("Database");

		jd = GetComponent<JailDew>();
		uc = GetComponent<Unicom>();
		test = GetComponent<Test>();
		disk = GetComponent<CD>();
		com = GetComponent<Computer>();
		note = GetComponent<Notepad>();
		trace = GetComponent<Tracer>();
		mb = GetComponent<MissionBrow>();
		cc = GetComponent<CurContracts>();
		sl = GetComponent<SiteList>();
		al = GetComponent<AccLog>();
		ds = GetComponent<DirSearch>();
		tv = GetComponent<TreeView>();
		clk = GetComponent<Clock>();
		def = GetComponent<Defalt>();
		mg = GetComponent<MissionGen>();
		os = GetComponent<DesktopEnviroment>();
		sm = GetComponent<SystemMap>();
		wsv = GetComponent<WebSecViewer>();
		ep = GetComponent<ErrorProm>();
		sp = GetComponent<SystemPanel>();
		sc = GetComponent<SoundControl>();
		desk = GetComponent<Desktop>();
		ct = GetComponent<CustomTheme>();
		mouse = GetComponent<Mouse>();
		ss = GetComponent<ScreenSaver>();
		clic = GetComponent<CLICommandsV2>();
		shc = GetComponent<SysHardwareCheck>();

		gstocks = Database.GetComponent<GStocks>();

		sdp = Prompt.GetComponent<ShutdownProm>();

		missiongen = missions.GetComponent<MissionGen>();
		missionbrow = missions.GetComponent<MissionBrow>();
		currentcontracts = missions.GetComponent<CurContracts>();

		Crash = GameObject.Find("Crash");
		SCM = Crash.GetComponent<SysCrashMan>();

		Prompt = GameObject.Find ("Prompts");
		noti = Prompt.GetComponent<NotfiPrompt>();
		notip = Prompt.GetComponent<NotificationPrompt>();

		RotationCooldown = 0.01f;

		if (Application.isEditor == true) 
		{
			windowRect.width = Screen.width;
			windowRect.height = Screen.height;
		} 
		else 
		{
			windowRect.width = Customize.cust.RezX;
			windowRect.height = Customize.cust.RezY;
		}

		Pos.width = windowRect.width / 2;
		Pos.height = windowRect.height / 2;

		windowRect.x = 0;
		windowRect.y = 0;

		x = Pos.width - 30;
		y = Pos.height;

		GameControl.control.GUIID = Customize.cust.GUIID;

		StartTimer = 0.01f;

		PlaySoundOnce = false;

		LoadPresetColors();

		cd = GameControl.control.BootTime;
	}

	void LoadPresetColors()
	{
		Color1.r = 0;
		Color1.g = 0;
		Color1.b = 0;
		Color1.a = 255;

		Color2.r = 0;
		Color2.g = 0;
		Color2.b = 0;
		Color2.a = 255;
		
		buttonColor.r = 255;
		buttonColor.g = 255;
		buttonColor.b = 255;
		buttonColor.a = 255;

		fontColor.r = 255;
		fontColor.g = 255;
		fontColor.b = 255;
		fontColor.a = 255;
	}


	void ForceCrash()
	{
		SCM.StopCodeWord = "MANUAL_LAUNCH_CRASH";
		SCM.StopCodeNumber = "0xD34DD13D";
		SCM.CodeDetail = "K3RN31-94N1C-8007-U23R-D3F";
		SCM.ExtraDetail = "14M-7H3-D327R0Y3R-0F-7H12-02";
		SCM.enabled = true;
		SCM.Type = "Test";
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		speedMod = Random.Range(0.01f, 2f);

		if (Input.GetKeyDown (KeyCode.End)) 
		{
			ForceCrash();
		}

		if (rebooting == true)
		{
			show = true;
			Index = 0;
			Index1 = 0;
			com.show = false;
			BootInfo.RemoveRange (0, BootInfo.Count);
			pause = false;
			booting = true;
			rebooting = false;
		}

		if (booting == true)
		{
			if (pause == false) 
			{
				cd -= speedMod * Time.deltaTime;

				if (cd <= 0) 
				{
					Index++;
					//sc.SoundSelect = 13;
					//sc.PlaySound();
					cd = GameControl.control.BootTime;
					Kernal();
					a += speedMod;
				}
			}

			if (pause == true) 
			{
				cd1 -= speedMod * Time.deltaTime;

				if (cd1 <= 0) 
				{
					Index1++;
					//sc.SoundSelect = 13;
					//sc.PlaySound();
					cd1 = GameControl.control.BootTime;
					Kernal ();
					a += speedMod;
				}
			}
		}

		if (ColourFade == true)
		{
			if (TakingAlpha == true) 
			{
				Timer -= 1 * Time.deltaTime;

				if (Timer <= 0) 
				{
					Color2.a--;
					Timer = StartTimer;
				}
			} 
			else 
			{
				Timer -= 1 * Time.deltaTime;

				if (Timer <= 0) 
				{
					Color2.a++;
					Timer = StartTimer;
				}
			}
		}

		if (BackgroundFade == true) 
		{
			if (BGTakingAlpha == true) 
			{
				Timer -= 1 * Time.deltaTime;

				if (TakingAlpha == true)
				{
					TakingAlpha = false;
				}

				if (Timer <= 0) 
				{
					if (Color1.a > 3) 
					{
						Color1.a--;
					}
					if (Color2.a > 3) 
					{
						Color2.a-=2;
					}
					Timer = StartTimer;
				}
			}
		}

		if (RoationEnable == true) 
		{
			RotationTimer -= 1 * Time.deltaTime;
			if (RotationTimer <= 0) 
			{
				RotationAngle+=0.2f;
				RotationTimer = 0.01f;
			}
			if (RotationAngle >= 360)
			{
				RotationAngle = 0;
			}
		}
	}

	void Kernal()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");
		switch (Index) 
		{
		case 0:
			BootInfo.Add ("Preparing systems check.");
			person.Gateway.Status.POST = false;
			person.Gateway.Status.Booting = true;
			//GameControl.control.Load();
			break;
		case 1:
			BootInfo.Remove ("Preparing systems check.");
			BootInfo.Add ("Preparing systems check..");
			os.SetOSUsage = true;
			GameControl.control.Gateway.Status.Booting = true;
			break;
		case 2:
			BootInfo.Remove ("Preparing systems check..");
			BootInfo.Add ("Preparing systems check...");
			if (Customize.cust.WebBrowserHomepage == "")
			{
				Customize.cust.WebBrowserHomepage = "www.ping.com";
			}
			break;
		case 3:
			if (GameControl.control.NewAccount == true) {
				rebooting = true;
				//Customize.cust.Volume = 1;
				GameControl.control.NewAccount = false;
			}
			BootInfo.Remove ("Preparing systems check...");
			BootInfo.Add ("Preparing systems check.");
			break;
		case 4:
			BootInfo.Remove ("Preparing systems check.");
			BootInfo.Add ("Preparing systems check..");
			break;
		case 5:
			BootInfo.Remove ("Preparing systems check..");
			BootInfo.Add ("Preparing systems check...");
//			if (GameControl.control.MyBankDetails.Count < 1)
//			{
//				GameControl.control.MyBankDetails.Add (new BankSystem("192.168.56.91","LEC Bank","Lucas",1981619,"1234",5000,1,0,0,1));
//			}
			break;
		case 6:
			BootInfo.Add ("[       ] Starting self diagonstics");
			if (Customize.cust.DownloadPath == "")
			{
				Customize.cust.DownloadPath = "C:/Downloads";
			}
			break;
		case 7:
			BootInfo.Remove ("[       ] Starting self diagonstics");
			BootInfo.Add ("[ OK ] Starting self diagonstics");
//			if (GameControl.control.Rep.Count <= 0) 
//			{
//				GameControl.control.Rep.Add(new RepSystem("REVA",0,0,0,0));
//			}
			break;
		case 8:
			BootInfo.Add("[       ] Starting login systems");
			if (Customize.cust.MusicPath == "")
			{
				Customize.cust.MusicPath = "E:/Music";
			}
			break;
		case 9:
			BootInfo.Remove ("[       ] Starting login systems");
			BootInfo.Add ("[ OK ] Starting login systems");
			sdp.enabled = true;
			break;
		case 10:
			BootInfo.Add ("[       ] Starting accounts service");
			if (Customize.cust.RezX == 0) 
			{
				Customize.cust.RezX = 940;
			}
			if (Customize.cust.RezY == 0) 
			{
				Customize.cust.RezX = 560;
			}
			break;
		case 11:
			BootInfo.Remove ("[       ] Starting accounts service");
			BootInfo.Add ("[ OK ] Starting accounts service");
			break;
		case 12:
			pause = true;
			break;
		case 13:
			if (BackgroundFade == true)
			{
				BGTakingAlpha = true;
			}
			def.enabled = true;
			if (GameControl.control.NewAccount == true)
			{
				GameControl.control.NewAccount = false;
			}
			break;
		}

		if (pause == true)
		{
			switch (Index1) 
			{
			case 0:
				BootInfo.Add ("[Initializing] System Commands");
				clic.SetSystemCommands();
				break;
			case 1:
				BootInfo.Remove ("[Initializing] System Commands");
				BootInfo.Add ("[Done] System Commands");
				BootInfo.Add ("[Initializing] Networking Soloutions");
				break;
			case 2:
				BootInfo.Remove ("[Initializing] Networking Soloutions");
				BootInfo.Add ("[Done] Networking Soloutions");
				BootInfo.Add ("[Initializing] Desktop Enviroment");
				break;
			case 3:
				BootInfo.Remove ("[Initializing] Desktop Enviroment");
				BootInfo.Add ("[Done] Desktop Enviroment");
				BootInfo.Add ("[Initializing] Installed Applications");
				break;
			case 4:
				BootInfo.Remove ("[Initializing] Installed Applications");
				BootInfo.Add ("[Done] Installed Applications");
				BootInfo.Add ("[Initializing] Database Soloutions.");
				break;
			case 5:
				BootInfo.Remove ("[Initializing] Database Soloutions.");
				BootInfo.Add ("[Initializing] Database Soloutions..");
				break;
			case 6:
				BootInfo.Remove ("[Initializing] Database Soloutions..");
				BootInfo.Add ("[Done] Database Soloutions");
				break;
			case 7:
				BootInfo.Add ("[Initializing] Connecting Contract Database.");
				break;
			case 8:
				BootInfo.Remove ("[Initializing] Connecting Contract Database.");
				BootInfo.Add ("[Initializing] Connecting Contract Database..");
				break;
			case 9:
				gstocks.enabled = true;
				BootInfo.Remove ("[Initializing] Connecting Contract Database..");
				BootInfo.Add ("[Done] Connecting Contract Database");
				break;
			case 10:
				BootInfo.Add ("[Initializing] Software.");
				break;
			case 11:
				BootInfo.Remove ("[Initializing] Software.");
				BootInfo.Add ("[Initializing] Software..");
				break;
			case 12:
				BootInfo.Remove ("[Initializing] Software..");
				BootInfo.Add ("[Initializing] Software...");
				break;
			case 13:
				BootInfo.Remove ("[Initializing] Software...");
				BootInfo.Add ("[Initializing] Software.");
				break;
			case 14:
				BootInfo.Remove ("[Initializing] Software.");
				BootInfo.Add ("[Initializing] Software..");
				missiongen.enabled = true;
				missionbrow.enabled = true;
				currentcontracts.enabled = true;
				break;
			case 15:
				BootInfo.Remove ("[Initializing] Software..");
				BootInfo.Add ("[Done] Software");
				break;
			case 16:
				notip.enabled = true;
				BootInfo.Remove ("[Done] Software");
				BootInfo.Add ("Loading Desktop.");
				break;
			case 17:
				pause = false;
				break;
			}
		}
	}

	void DesktopIni()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

		if (TestMode == false)
        {
			if (Customize.cust.ScreenSaverEnabled == true)
			{
				ss.enabled = true;
			}

			if (Registry.GetColorData("Player", "System", "WindowColor") == null)
            {
				Registry.SetColorData("Player", "System", "WindowColor",new SColor(new Color(255,255,255,255)));
				Registry.SetColorData("Player", "System", "FontColor", new SColor(new Color(0, 0, 0, 255)));
				Registry.SetColorData("Player", "System", "ButtonColor", new SColor(new Color(255, 255, 255, 255)));
            }

			Registry.SetBoolData("Player", "System", "WindowColor",true);
			Registry.SetBoolData("Player", "System", "FontColor", true);
			Registry.SetBoolData("Player", "System", "ButtonColor", true);
			GameControl.control.Gateway.Status.Booting = false;
			os.enabled = true;
            desk.enabled = true;
			noti.enabled = true;
			shc.enabled = true;
			notip.show = true;
			enabled = false;
            booting = false;
            show = false;
			person.Gateway.Status.Booting = false;
			person.Gateway.Status.Booted = true;
			GameControl.control.Gateway.Status.Booted = true;
        }
	}
		
	void OnGUI()
	{
		GUI.depth = -20;
		GUI.skin = Skin;
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1));

		if(show == true)
		{
			GUI.color = Color1;
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.backgroundColor = buttonColor;
		GUI.contentColor = fontColor;
		if (GameControl.control.Gateway.Status.Terminal == false)
		{
			var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

			for (int i = 0; i < person.Gateway.StorageDevices.Count; i++)
			{
				for (int j = 0; j < person.Gateway.StorageDevices[i].OS.Count; j++)
				{
					if(person.Gateway.StorageDevices[i].OS[j].Options.Selected == true)
					{
						SelectedOS = person.Gateway.StorageDevices[i].OS[j];
						person.Gateway.CurrentOS = person.Gateway.StorageDevices[i].OS[j];
						GameControl.control.SelectedOS = SelectedOS;
					}
				}
			}
			OSCheck();
		}
		else
		{
			TerminalMode();
		}
	}

	void TerminalMode()
	{
		BackgroundFade = false;
		ColourFade = false;

		//GUI.DrawTexture (new Rect (Pos.width-24, Pos.height-24, 48, 48), os.Icon[os.SelectedIcon]);

		GUI.color = Color2;
		Color2.r = 255;
		Color2.g = 255;
		Color2.b = 255;

		BootText();
	}

	void OSCheck()
	{
		switch (SelectedOS.Name) 
		{
		case OperatingSystems.OSName.FluidicIceOS:
			BackgroundFade = true;
			ColourFade = true;
			GUI.color = Color2;
			Color2.r = 255;
			Color2.g = 255;
			Color2.b = 255;

			GameControl.control.GUIID = 0;
            Customize.cust.CustomThemeColorEnabled = true;
			//GUI.Label (new Rect (Pos.width - 60, Pos.height + 50, 400, 22), "Starting IceOS");

			RoationPic = "|";
			BreathingLogo();

			//GameControl.control.Load();


			if (BGTakingAlpha == true)
			{
				if (PlaySoundOnce == false)
				{
					PlaySoundOnce = true;
					sc.SoundSelect = 8;
					sc.PlaySound ();
					DesktopIni();
				}
			}
			break;

            case OperatingSystems.OSName.QuantinitumOS:
                BackgroundFade = true;
                ColourFade = true;
                GUI.color = Color2;
                Color2.r = 255;
                Color2.g = 255;
                Color2.b = 255;

                //GUI.Label (new Rect (Pos.width - 60, Pos.height + 50, 400, 22), "Starting IceOS");

                RoationPic = "|";
                BreathingLogo();

                //GameControl.control.Load();


                if (BGTakingAlpha == true)
                {
                    if (PlaySoundOnce == false)
                    {
                        PlaySoundOnce = true;
                        sc.SoundSelect = 8;
                        sc.PlaySound();
                        DesktopIni();
                    }
                }
                break;

            case OperatingSystems.OSName.AppatureOS:
			BackgroundFade = true;
			ColourFade = true;
			GUI.color = Color2;
			Color2.r = 0;
			Color2.g = 255;
			Color2.b = 255;

			GameControl.control.GUIID = 0;
            Customize.cust.CustomThemeColorEnabled = true;

			GUI.Label (new Rect (Pos.width - 60, Pos.height + 50, 400, 22), "Starting AppatureOS");

			RoationPic = "|";
			Rotation2();

			if (BGTakingAlpha == true)
			{
				if (PlaySoundOnce == false)
				{
					PlaySoundOnce = true;
					sc.SoundSelect = 8;
					sc.PlaySound ();
					DesktopIni();
				}
			}
			break;

			case OperatingSystems.OSName.EthelOS:
			BackgroundFade = false;
			ColourFade = false;

			//GUI.DrawTexture (new Rect (Pos.width-24, Pos.height-24, 48, 48), os.Icon[os.SelectedIcon]);

			GUI.color = Color2;
			Color2.r = 255;
			Color2.g = 255;
			Color2.b = 0;

			if(SetColor == true)
			{
				GameControl.control.SelectedOS.Colour.Window.Red = 255;
				GameControl.control.SelectedOS.Colour.Window.Green = 217;
				GameControl.control.SelectedOS.Colour.Window.Blue = 0;
				GameControl.control.SelectedOS.Colour.Window.Alpha = 255;

				GameControl.control.SelectedOS.Colour.Button.Red = 255;
				GameControl.control.SelectedOS.Colour.Button.Green = 209;
				GameControl.control.SelectedOS.Colour.Button.Blue = 0;
				GameControl.control.SelectedOS.Colour.Button.Alpha = 255;

				GameControl.control.SelectedOS.Colour.Font.Red = 0;
				GameControl.control.SelectedOS.Colour.Font.Green = 0;
				GameControl.control.SelectedOS.Colour.Font.Blue = 0;
				GameControl.control.SelectedOS.Colour.Font.Alpha = 255;

				GameControl.control.GUIID = 2;
				SetColor = false;
			}

			//GUI.Label (new Rect (Pos.width-75, Pos.height+60, 400, 22),"" + BootInfo[BootInfo.Count-1]);

			//RoationPic = "E";
			DisplayText(0,0);
			LoadingBar1(30, windowRect.height - 30,windowRect.width - 60, 24,3,10);

			//if (BGTakingAlpha == true)
			//{
			//	if (PlaySoundOnce == false)
			//	{
			//		PlaySoundOnce = true;
			//		sc.SoundSelect = 8;
			//		sc.PlaySound ();
			//	}
			//}

			if (percentage >= 100)
			{
				if (PlaySoundOnce == false)
				{
					PlaySoundOnce = true;
					sc.SoundSelect = 1;
					sc.PlaySound();
					DesktopIni();
				}
			}
			break;

		case OperatingSystems.OSName.CSOSV1:
			BackgroundFade = true;
			ColourFade = true;

			GUI.DrawTexture (new Rect (Pos.width-24, Pos.height-24, 48, 48), os.Icon[os.SelectedIcon]);

			GUI.color = Color2;
			Color2.r = 0;
			Color2.g = 255;
			Color2.b = 0;

			//GUI.Label (new Rect (Pos.width-75, Pos.height+60, 400, 22),"" + BootInfo[BootInfo.Count-1]);

			RoationPic = "/";
			BootTextRotation();

			if (BGTakingAlpha == true)
			{
				if (PlaySoundOnce == false)
				{
					PlaySoundOnce = true;
					sc.SoundSelect = 8;
					sc.PlaySound ();
				}
			}
			break;

		case OperatingSystems.OSName.TreeOS:
			GUI.color = Color2;
			Color2.r = 255;
			Color2.g = 255;
			Color2.b = 255;

            os.SelectedIcon = 2;

			GameControl.control.GUIID = 0;
            Customize.cust.CustomThemeColorEnabled = true;

			GUI.DrawTexture (new Rect (Pos.width - 100, Pos.height - 100, 64, 64), os.Icon[os.SelectedIcon]);

			//RoationPic = "|";
			LoadingBar();

			if (Index>=13)
			{
				if (PlaySoundOnce == false)
				{
					PlaySoundOnce = true;
					sc.SoundSelect = 1;
					sc.PlaySound();
					DesktopIni();
				}
			}
			break;

		case OperatingSystems.OSName.HackLink:
			GUI.color = Color2;
			Color2.r = 255;
			Color2.g = 255;
			Color2.b = 255;

			GUI.DrawTexture (new Rect (Pos.width, Pos.height-64, 64, 64), os.Icon[os.SelectedIcon]);

			GUI.Label (new Rect (Pos.width-25, Pos.height+25, 400, 22),"" + BootInfo[BootInfo.Count-1]);

			//RoationPic = "|";
			LoadingBar();

			if (Index>=13)
			{
				if (PlaySoundOnce == false)
				{
					PlaySoundOnce = true;
					sc.SoundSelect = 1;
					sc.PlaySound();
					DesktopIni();
				}
			}
			break;

		case OperatingSystems.OSName.NOS:
			GUI.color = Color2;
			Color2.r = 128;
			Color2.g = 0;
			Color2.b = 0;

			//GUI.DrawTexture (new Rect (Pos.width, Pos.height - 64, 64, 64), os.Icon [os.SelectedIcon]);

			GUI.Label (new Rect (Pos.width - 25, Pos.height + 25, 400, 22), "" + BootInfo [BootInfo.Count - 1]);

			RoationEnable = true;
			RoationPic = "S";

			x = Pos.width + 25;
			y = Pos.height - 12;

			GUI.Label (new Rect (Pos.width + 25, Pos.height - 75, 400, 22), "PLEASE WAIT WERE");
			GUI.Label (new Rect (Pos.width + 25, Pos.height - 50, 400, 22), "ACTIVATING >> NOS");

			LoadingBar ();
			Rotation();

			if (Index>=13)
			{
				if (PlaySoundOnce == false)
				{
					PlaySoundOnce = true;
					sc.SoundSelect = 1;
					sc.PlaySound();
					DesktopIni();
				}
			}
			break;
		}
	}

	void Rotation()
	{
		RoationEnable = true;
		//GUI.Label (new Rect (Pos.width, Pos.height, 300, 30), "Logging In");
		//GUI.Label (new Rect (Pos.width, Pos.height, 300, 30), "%" + percentage.ToString("F0"));
		for (int i = 0; i < 25; i++)
		{
			GUIUtility.RotateAroundPivot (RotationAngle, new Vector2(Pos.width, Pos.height));
			GUI.Label (new Rect (Pos.width*1f, Pos.height*1f, 22, 22), RoationPic);
		}
		//GUI.DrawTexture (new Rect (Pos.width, Pos.height, 256, 256), os.Icon[os.SelectedIcon]);
	}

	void Rotation2()
	{
		RoationEnable = true;
		if (Color1.a >= 250) 
		{
			Color2.a = 255;
		}
		//GUI.Label (new Rect (Pos.width, Pos.height, 300, 30), "Logging In");
		//GUI.Label (new Rect (Pos.width, Pos.height, 300, 30), "%" + percentage.ToString("F0"));
		for (int i = 0; i < 25; i++)
		{
			GUIUtility.RotateAroundPivot (RotationAngle, new Vector2(Pos.width, Pos.height));
			GUI.Label (new Rect (Pos.width*1.04f, Pos.height*1.04f, 22, 22), RoationPic);
		}

		if (Color2.a <= 3)
		{
			DesktopIni();
		}
		//GUI.DrawTexture (new Rect (Pos.width, Pos.height, 256, 256), os.Icon[os.SelectedIcon]);
	}

	void BootText()
	{
		for (int i = 0; i < BootInfo.Count; i++)
		{
			GUI.Label (new Rect (0, 0 + 21 * i, 1000, 21), BootInfo[i]);
		}

		if (Index >= 13)
		{
			DesktopIni();
		}
	}

	void DisplayText(float x,float y)
	{
		for (int i = 0; i < BootInfo.Count; i++)
		{
			GUI.Label(new Rect(x, y + 21 * i, 1000, 21), BootInfo[i]);
		}
	}

	void BootTextLogo()
	{
		for (int i = 0; i < BootInfo.Count; i++)
		{
			GUI.Label (new Rect (0, 0 + 21 * i, 1000, 21), BootInfo[i]);
		}

		if (Index >= 13)
		{
			DesktopIni();
		}
		GUI.DrawTexture (new Rect (Pos.width - 100, Pos.height - 100, 256, 256), os.Icon[os.SelectedIcon]);
	}

	void BootTextRotation()
	{
		RoationEnable = true;

		for (int i = 0; i < BootInfo.Count; i++)
		{
			GUI.Label (new Rect (0, 0 + 21 * i, 1000, 21), BootInfo[i]);
		}

		if (Color1.a >= 250) 
		{
			Color2.a = 255;
		}
		//GUI.Label (new Rect (Pos.width, Pos.height, 300, 30), "Logging In");
		//GUI.Label (new Rect (Pos.width, Pos.height, 300, 30), "%" + percentage.ToString("F0"));
		for (int i = 0; i < 25; i++)
		{
			GUIUtility.RotateAroundPivot (RotationAngle, new Vector2(Pos.width, Pos.height));
			GUI.Label (new Rect (Pos.width*1.04f, Pos.height*1.04f, 22, 22), RoationPic);
		}

		if (Color2.a <= 3)
		{
			DesktopIni();
		}
	}

	void LoadingBar()
	{
		percentage = a / b * 100;
		MoveMod = 40;
		moveAmt += MoveMod * Time.deltaTime;

		if (moveAmt >= MaxmoveAmt) 
		{
			moveAmt = 0;
		}

		GUI.TextArea (new Rect (x, y, w3, h4),"");

		if (moveAmt < 125)
		{
			GUI.Box (new Rect (x + moveAmt, y, w, h),"");
		}
		if (moveAmt >= 15 && moveAmt < 135) 
		{
			GUI.Box (new Rect (x + BarMod1 + moveAmt, y, w, h),"");
		}
		if (moveAmt >= 30 && moveAmt < 155) 
		{
			GUI.Box (new Rect (x + BarMod2 + moveAmt, y, w, h),"");
		}
	}

	void LoadingBar1(float x,float y,float boxw, float boxh,int barcount,float spacing)
	{
		percentage = a / b * 100;
		b = 53;
		moveAmt = 0;
		MoveMod = speedMod / b;

		moveAmt += 1 * Time.deltaTime;

		GameControl.control.BootTime = 0.14f;

		if(moveAmt >= MoveMod)
		{
			LoadingBarInfo = LoadingBarInfo + "|";
			moveAmt = 0;
		}

		GUI.TextArea(new Rect(x, y, boxw, boxh),"" + LoadingBarInfo);

		//if (LoadingBarXPos.Count < barcount)
		//{
		//	LoadingBarXPos.Add(10);
		//}

		//if(LoadingBarXPos.Count > 0)
		//{
		//	for (int i = 0; i < LoadingBarXPos.Count; i++)
		//	{
		//		LoadingBarXPos[i] += MoveMod * Time.deltaTime;
		//		GUI.Box(new Rect(LoadingBarXPos[i] * 40, y, 10, boxh), "");

		//		if (LoadingBarXPos[i] >= 100)
		//		{
		//			LoadingBarXPos.RemoveAt(i);
		//		}
		//	}
		//}
		//if (moveAmt < 125)
		//{
		//	GUI.Box(new Rect(x + moveAmt, y, w, h), "");
		//}
		//if (moveAmt >= 15 && moveAmt < 135)
		//{
		//	GUI.Box(new Rect(x + BarMod1 + moveAmt, y, w, h), "");
		//}
		//if (moveAmt >= 30 && moveAmt < 155)
		//{
		//	GUI.Box(new Rect(x + BarMod2 + moveAmt, y, w, h), "");
		//}
	}

	void BreathingLogo()
	{
		Color1.r = 0;
		Color1.g = 0;
		Color1.b = 55;

		Color2.r = 255;
		Color2.g = 255;
		Color2.b = 255;

		GUI.color = Color2;
		if (BGTakingAlpha == false)
		{
			if (Color2.a <= 0)
			{
				TakingAlpha = false;
			}
			if (TakingAlpha == false) 
			{
				if (Color2.a >= 250)
				{
					TakingAlpha = true;
				}
			}
		}

		GUI.DrawTexture (new Rect (Pos.width - 100, Pos.height - 100, 256, 256), os.Icon[os.SelectedIcon]);

		if (Color1.a <= 3) 
		{
			enabled = false;
			booting = false;
			show = false;
		}
	}
}