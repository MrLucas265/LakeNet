using UnityEngine;
using System.Collections;

public class ShutdownProm : MonoBehaviour
{
	private GameObject Puter;
    private GameObject Signout;
    public  AudioSource AS;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public Texture2D sdIcon;
	public Texture2D restartIcon;
	public Texture2D logIcon;
	public bool show;

	public bool Game;

	public bool shutdown;
	public bool restart;

	public GUISkin Skin;

	public Rect CloseButton;

	public Color32 ShutdownColor = new Color32(0,0,0,0);
	public Color32 RestartColor = new Color32(0,0,0,0);
	public Color32 LogOffColor = new Color32(0,0,0,0);

	//private HomePage hp;
	//private Notepad note;
	//private Progtive pro;
	//private Tracer trace;
	//private MissionBrow mb;
	//private CurContracts cc;
	//private SiteList sl;
	//private AccLog al;
	//private Desktop dsk;
	//private Tourtial tut;
	//private DirSearch ds;
	//private TreeView tv;
	private Clock clk;
	private CD cd;
    //private JailDew jd;
    //private Unicom uc;
    //private Test test;
    private SignoutMan SignMan;
    private Boot boot;
	private OS os;
	private Defalt def;
	private Computer com;
	private SoundControl sc;
	int DesktopStyle = 3;
	// Use this for initialization

	void Start () 
	{
		Puter = GameObject.Find("System");
        Signout = GameObject.Find("Signout");
        //jd = GetComponent<JailDew>();
        //uc = GetComponent<Unicom>();
        //test = GetComponent<Test>();
        cd = GetComponent<CD>();
		//dsk = GetComponent<Desktop>();
		//hp = GetComponent<HomePage>();
		com = Puter.GetComponent<Computer>();
		//note = GetComponent<Notepad>();
		//pro = GetComponent<Progtive>();
		//trace = GetComponent<Tracer>();
		//mb = GetComponent<MissionBrow>();
		//cc = GetComponent<CurContracts>();
		//sl = GetComponent<SiteList>();
		//al = GetComponent<AccLog>();
		//tut = GetComponent<Tourtial>();
		//ds = GetComponent<DirSearch>();
		//tv = GetComponent<TreeView>();
		clk = Puter.GetComponent<Clock>();
		boot = Puter.GetComponent<Boot>();
		os = Puter.GetComponent<OS>();
		sc = Puter.GetComponent<SoundControl>();
		def = Puter.GetComponent<Defalt>();
        SignMan = Signout.GetComponent<SignoutMan>();
        if (Game == true) 
		{
			native_height = Customize.cust.native_height;
			native_width = Customize.cust.native_width;
		}

		AfterStart();

		CloseButton = new Rect (252,2,21,21);
	}

	void AfterStart()
	{
		windowRect = new Rect(100, 100, 275, 100);
		ShutdownColor = Color.red;
		RestartColor = Color.green;
		LogOffColor = Color.yellow;
	}

	// Update is called once per frame
	void Update () 
	{
		if (shutdown && !AS.isPlaying)
		{
			Application.Quit();
		}

		if (restart == true) 
		{
			boot.rebooting = true;
			os.SetOSUsage = true;
			//hd.Installed = true;
			if (Game == true) 
			{
			} 
			else 
			{
				show = false;
			}
			restart = false;
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = com.Skin[GameControl.control.GUIID];

		//set up scaling
		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				show = false;
				this.enabled = false;
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1]);
		}

		Render();
	}

	void Render()
	{
		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow (new Rect (2, 2, 249, 21));
		GUI.Box (new Rect (2, 2, 249, 21), "Turn Off System");

		GUI.backgroundColor = LogOffColor;
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];
		GUI.Label (new Rect (210, 75, 100, 32), "Log Off");
		GUI.contentColor = Color.white;
		if (GUI.Button (new Rect (215, 40, 32, 32), logIcon, com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle])) 
		{
            sc.SoundSelect = 2;
            sc.PlaySound();
            SignMan.enabled = true;
            this.enabled = false;
		}
		GUI.backgroundColor = RestartColor;
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];
		GUI.Label (new Rect (120, 75, 100, 32), "Restart");
		GUI.contentColor = Color.white;
		if (GUI.Button (new Rect (125, 40, 32, 32), restartIcon, com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle])) 
		{
			GameControl.control.Booted = false;
			Application.LoadLevel (1);
		}
		GUI.backgroundColor = ShutdownColor;
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];
		GUI.Label (new Rect (30, 75, 100, 32), "Turn Off");
		GUI.contentColor = Color.white;
		if (GUI.Button (new Rect (35, 40, 32, 32), sdIcon, com.Skin [GameControl.control.GUIID].customStyles [DesktopStyle]))
		{
            //SignMan.enabled = true;
            //this.show = false;
            //SignMan.SignoutMessage = "Shutting Down.";
            if (!AS.isPlaying)
			{
				sc.SoundSelect = 2;
				sc.PlaySound();
                GameControl.control.Shutdown = true;
				shutdown = true;
			}
		}
	}
}
