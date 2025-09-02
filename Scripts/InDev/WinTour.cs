using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTour : MonoBehaviour
{
	private GameObject Puter;

	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public AudioSource AS;

	public bool show;

	public bool playsound;

	public int SoundSelect;

	private Computer com;
	private Defalt def;
	private SoundControl sc;
	private Desktop desk;

	private Rect CloseButton;

	public Texture2D VirtualMouse;
	public Rect VMouse;

	public Rect Target;

	public float MouseSpeed;

	public int TargetSelector;

	public List<Rect> DesktopIcons = new List<Rect>();

	public Rect AppMenuRect;
	public bool ShowAppMenu;

	public Rect ProgramAppRect;
	public Rect ProgramWindowRect;
	public bool ShowProgram;
	public bool DragProgramWindow;

	public GUISkin skin;

	// Use this for initialization
	void Start () 
	{
		Puter = GameObject.Find("System");
		com = Puter.GetComponent<Computer>();
		def = Puter.GetComponent<Defalt>();
		sc = Puter.GetComponent<SoundControl>();
		desk = Puter.GetComponent<Desktop>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		CloseButton = new Rect (378, 1, 21, 21);
		windowRect = new Rect(100, 100, 400, 300);
		MouseSpeed = 1f;
		VMouse = new Rect (0, 0, 16, 16);
		AppMenuRect = new Rect (5, 150, 80, 120);
		ProgramAppRect = new Rect (5, 170, 60, 20);
		ProgramWindowRect = new Rect (100, 100, 100, 100);
		IconPos();
	}

	void IconPos()
	{
		DesktopIcons.Add (new Rect(5, 270, 25, 25));
		DesktopIcons.Add (new Rect(35, 270, 25, 25));
		DesktopIcons.Add (new Rect(65, 270, 25, 25));
	}
	// Update is called once per frame
	void Update () 
	{
		Math();
		Targets ();
	}

	void Targets()
	{
		switch (TargetSelector)
		{
		case 1:
			Target.x = DesktopIcons [0].x;
			Target.y = DesktopIcons [0].y;
			break;
		case 2:
			Target.x = DesktopIcons[1].x;
			Target.y = DesktopIcons[1].y;
			break;
		case 3:
			Target.x = DesktopIcons[2].x;
			Target.y = DesktopIcons[2].y;
			break;
		case 4:
			Target.x = AppMenuRect.x;
			Target.y = AppMenuRect.y;
			break;
		case 5:
			Target.x = ProgramAppRect.x;
			Target.y = ProgramAppRect.y;
			break;
		case 6:
			Target.x = ProgramWindowRect.x;
			Target.y = ProgramWindowRect.y;
			break;
		case 7:
			Target.x = 250;
			Target.y = 100;
			break;
		case 8:
			Target.x = 100;
			Target.y = 200;
			break;
		}
	}

	void Math()
	{
		if (VMouse.x < Target.x)
		{
			VMouse.x += MouseSpeed;
		}
		if (VMouse.x > Target.x)
		{
			VMouse.x -= MouseSpeed;
		}

		if (VMouse.y < Target.y)
		{
			VMouse.y += MouseSpeed;
		}
		if (VMouse.y > Target.y)
		{
			VMouse.y -= MouseSpeed;
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		if (playsound == true)
		{
			playsound = false;
			sc.SoundSelect = SoundSelect;
			sc.PlaySound();
		}

		if(show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,Window,""));
		}
	}

	void Window(int WindowID)
	{
		if (DesktopIcons.Count > 0) 
		{
			Desktop();
		}
		VirtualMouseRender();
	}

	void Desktop()
	{
		GUI.Box (new Rect (DesktopIcons[0]), desk.ApplicationsIcon);
		GUI.Box (new Rect (DesktopIcons[1]), desk.GatewayIcon);
		GUI.Box (new Rect (DesktopIcons[2]), desk.EmailIcon);

		if (ShowAppMenu == false) 
		{
			if (DesktopIcons [0].Contains (VMouse.position)) 
			{
				ShowAppMenu = true;
			}
		}

		if (ShowAppMenu == true) 
		{
			AppMenu();
		}

		if (ShowProgram == true) 
		{
			VirtualWindow();
		}
	}

	void AppMenu()
	{
		GUI.Box (new Rect (AppMenuRect), "");
		GUI.Box (new Rect (ProgramAppRect), "Program",skin.button);

		if (!AppMenuRect.Contains (VMouse.position))
		{
			if (!DesktopIcons [0].Contains (VMouse.position))
			{
				ShowAppMenu = false;
			}
		}

		if (ShowProgram == false) 
		{
			if (ProgramAppRect.Contains (VMouse.position)) 
			{
				ShowProgram = true;
			}
		}
	}

	void VirtualMouseRender()
	{
		GUI.DrawTexture (new Rect (VMouse), VirtualMouse);
	}

	void VirtualWindow()
	{
		GUI.Box (new Rect (ProgramWindowRect), "");
		GUI.Box (new Rect (ProgramWindowRect.x,ProgramWindowRect.y,60,21), "Program",skin.button);

		if (DragProgramWindow == true)
		{
			ProgramWindowRect.position = VMouse.position;
		}
	}
}
