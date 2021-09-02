using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Defalt : MonoBehaviour 
{
	private GameObject Prompts;
	private GameObject Icon;
	private GameObject HackingSoftware;
	private GameObject AppSoftware;
	private GameObject SysSoftware;
	private GameObject Computer;
	private GameObject Other;
	private GameObject Missions;
    private GameObject QA;
	private GameObject VideoStuff;

	private Computer com;
    private Notepad note;
    private Notepadv2 notev2;
    private Progtive pro;
    private Tracer trace;
    //private MissionBrow mb;
	private EmailClient cc;
    private SiteList sl;
    private AccLog al;
    private Desktop dsk;
	private DirSearch ds;
	private TreeView tv;
	private Clock clk;
	private CLIV2 cmd2;
	private OS os;
	private SystemMap sm;
	private WebSecViewer wsv;
	private ErrorProm ep;
	private ShutdownProm sdp;
//	private TextReader tr;
	private PurchasePrompt pp;
	private ScreenSaver ss;
	private SystemPanel sp;
	private AppMenu am;
	private InstallPrompt ip;
	private RezPrompt rp;
	private TaskViewer tasks;
//	private Portfolio port;
//	private SharePrompt shareprompt;
	private CustomTheme ct;
	private DicCrk dc;
	private Calculator cal;
	private VMDesigner vmd;
	private NotfiPrompt notiprompt;
	private PasswordCracker passcrk;
	private MusicPlayer mp;
	private DiskManV2 dmv2;
	private Mouse mouse;
	private FileExplorer fp;
    private DeviceManager dem;
    private BugReport qa;
    private NotificationViewer nv;
    private PlanViewer pv;
    private Calendar calendar;
    private CalendarV2 calendarv2;
    private EventViewer eventview;
	private ExchangeViewer exchangeview;
    private VolumeController vc;
	private MediaPlayer media;
	private VersionViewer version;

	private MissionGen misgen;

	private InternetBrowser ib;
	private NetViewer eib;
	private Firefox fib;
	private RemoteView rv;

	private RealExeCreator rec;

	public bool godemode;
	public bool ScriptHandelActive;

	public List<int> OpenwindowID = new List<int>();

	bool Accounts;

	//timer
	public float Cooldown;
	public float Timer;

	public int SelectedWindowID;

	// Use this for initialization

	void Start ()
    {
		Prompts = GameObject.Find("Prompts");
		Icon = GameObject.Find("IconObject");

		Screen.SetResolution (Customize.cust.RezX, Customize.cust.RezY, Customize.cust.FullScreen);
		QualitySettings.vSyncCount = Customize.cust.VSync;
		QualitySettings.antiAliasing = Customize.cust.AA;

        //GameControl.control.Load();
		ProgramSetup();
		AfterStart();
		AccountSetup();
		SetProgramID ();
    }

	void ProgramSetup()
	{
		AppSoftware = GameObject.Find("Applications");
        QA = GameObject.Find("QA");
        HackingSoftware = GameObject.Find("Hacking");
		SysSoftware = GameObject.Find("System");
		Computer = GameObject.Find("Computer");
		Other = GameObject.Find("Other");
		Missions = GameObject.Find("Missions");
		VideoStuff = GameObject.Find("Video Stuff");

		qa = QA.GetComponent<BugReport>();
		media = VideoStuff.GetComponent<MediaPlayer>();

		//mb = GetComponent<MissionBrow>();
		sl = Computer.GetComponent<SiteList>();
		wsv = Computer.GetComponent<WebSecViewer>();

		//Hacking Software
		pro = HackingSoftware.GetComponent<Progtive>();
		trace = HackingSoftware.GetComponent<Tracer>();
		ds = HackingSoftware.GetComponent<DirSearch>();
		dc = HackingSoftware.GetComponent<DicCrk>();
		passcrk = HackingSoftware.GetComponent<PasswordCracker>();

		//System Sofware
		dsk = SysSoftware.GetComponent<Desktop>();
		com = SysSoftware.GetComponent<Computer>();
		ss = SysSoftware.GetComponent<ScreenSaver>();
		sp = SysSoftware.GetComponent<SystemPanel>();
		am = SysSoftware.GetComponent<AppMenu>();
		tasks = SysSoftware.GetComponent<TaskViewer>();
		clk = SysSoftware.GetComponent<Clock>();
		cmd2 = SysSoftware.GetComponent<CLIV2>();
		os = SysSoftware.GetComponent<OS>();
		dmv2 = SysSoftware.GetComponent<DiskManV2>();
		mouse = SysSoftware.GetComponent<Mouse>();
		fp = SysSoftware.GetComponent<FileExplorer>();
        dem = SysSoftware.GetComponent<DeviceManager>();
        vc = SysSoftware.GetComponent<VolumeController>();
		version = SysSoftware.GetComponent<VersionViewer>();
		rec = SysSoftware.GetComponent<RealExeCreator>();

		//Application Softwate
		//        port = AppSoftware.GetComponent<Portfolio>();
		//        tr = AppSoftware.GetComponent<TextReader>();
		sm = AppSoftware.GetComponent<SystemMap>();
		al = AppSoftware.GetComponent<AccLog>();
		note = AppSoftware.GetComponent<Notepad>();
		notev2 = AppSoftware.GetComponent<Notepadv2>();
		cc = AppSoftware.GetComponent<EmailClient>();
		tv = AppSoftware.GetComponent<TreeView>();
        nv = AppSoftware.GetComponent<NotificationViewer>();
        pv = AppSoftware.GetComponent<PlanViewer>();
        //calendar = AppSoftware.GetComponent<Calendar>();
        calendarv2 = AppSoftware.GetComponent<CalendarV2>();
        eventview = AppSoftware.GetComponent<EventViewer>();
		exchangeview = AppSoftware.GetComponent<ExchangeViewer>();

        // Application Browsers
        ib = AppSoftware.GetComponent<InternetBrowser>();
		eib = AppSoftware.GetComponent<NetViewer>();
		fib = AppSoftware.GetComponent<Firefox>();
		rv = AppSoftware.GetComponent<RemoteView>();
		cal = AppSoftware.GetComponent<Calculator>();
		mp = AppSoftware.GetComponent<MusicPlayer>();



		// Prompts
		ip = Prompts.GetComponent<InstallPrompt>();
		pp = Prompts.GetComponent<PurchasePrompt>();
		ep = Prompts.GetComponent<ErrorProm>();
		sdp = Prompts.GetComponent<ShutdownProm>();
		rp = Prompts.GetComponent<RezPrompt>();
//		shareprompt = Prompts.GetComponent<SharePrompt>();
		notiprompt = Prompts.GetComponent<NotfiPrompt>();

		//OTHER
		vmd = Other.GetComponent<VMDesigner>();

		// Computer
		ct = Computer.GetComponent<CustomTheme>();

		//Missions
		misgen = Missions.GetComponent<MissionGen>();
	}

	void AfterStart()
	{
		if (Customize.cust.CustomTexFileNames [5] != "")
		{
			ct.enabled = true;
			ct.Once = false;
			ct.UpdatePics();
			ss.ScreensaverBackGround = ct.tex1[5];
			ct.enabled = false;
		}

		if (Customize.cust.CustomTexFileNames[6] != "")
		{
			ct.enabled = true;
			ct.Once = false;
			ct.UpdatePics();
			ss.ScreensaverPicture = ct.tex1[6];
			ct.enabled = false;
		}

		if (Customize.cust.CustomTexFileNames [4] != "")
		{
			ct.enabled = true;
			ct.Once = false;
			ct.UpdatePics ();
			os.pic [2] = ct.tex1 [4];
			ct.enabled = false;
		} 
		else
		{
			//if (Customize.cust.SelectedBackground >= sp.BackgroundPics.Count)
			//{
			//	Customize.cust.SelectedBackground = 0;
			//}
			//os.pic[2] = sp.BackgroundPics[Customize.cust.SelectedBackground];
		}

		if (Customize.cust.CustomTexFileNames [3] != "")
		{
			ct.enabled = true;
			ct.Once = false;
			ct.UpdatePics ();
			mouse.cursorImage = ct.tex1 [3];
			ct.enabled = false;
		}
	}

	void SetProgramID()
	{
		//icon.windowID = 1;
		com.windowID = 2;
		note.windowID = 3;
		pro.windowID = 4;
		trace.windowID = 5;
		//mb.windowID = 6;
		cc.windowID = 7;
		sl.windowID = 8;
		al.windowID = 9;
		//tut.windowID = 10;
		ds.windowID = 11;
		tv.windowID = 12;
		clk.windowID = 13;
		//hd.windowID = 14;
//		cmd.windowID = 15;
		os.windowID = 16;
		sm.windowID = 17;
		wsv.windowID = 18;
		//ep.windowID = 19;
		sdp.windowID = 20;
//		tr.windowID = 21;
		pp.windowID = 22;
//		cf.windowID = 23;
//		df.windowID = 24;
		ss.windowID = 25;
		sp.windowID = 26;
		ip.windowID = 27;
		am.windowID = 28;
		rp.windowID = 29;
//		cal.windowID = 31;
		com.windowID = 32;
//		installer.windowID = 33;
		//icon.windowID = 34;
		//icon.ConwindowID = 35;
		tasks.windowID = 36;
//		port.windowID = 37;
//		shareprompt.windowID = 38;
		dc.windowID = 39;
		//internet browsers
		eib.windowID = 40;
		fib.windowID = 41;
		rv.windowID = 42;
//		uf.windowID = 43;
		vmd.windowID = 44;
		notiprompt.windowID = 45;
		passcrk.windowID = 46;
		mp.windowID = 47;
//		dm.windowID = 48;
		fp.windowID = 49;
		fp.ContextMenuID = 50;
		tasks.ContextMenuID = 51;
		notev2.windowID = 52;
		notev2.ContextMenuID = 53;
		com.ContextMenuID = 54;
		cmd2.windowID = 55;
        dem.windowID = 56;
        qa.windowID = 57;
        qa.ContextMenuID = 58;
        nv.windowID = 59;
        nv.ContextMenuID = 60;
        rec.windowID = 61;

        calendarv2.windowID = 62;
        eventview.windowID = 63;
		exchangeview.windowID = 64;
        pv.windowID = 65;
        vc.windowID = 66;
		dmv2.windowID = 67;
		media.windowID = 68;
		version.windowID = 69;


        for (int i = 0; i < 100; i++)
		{
			OpenwindowID.Add(i);
		}

	}
	//void NewHardware()
	//{
	//	HardwareController.hdcon.CPU[0] = "Zion Z-14";
	//	HardwareController.hdcon.AirFlow = 2;
	//	HardwareController.hdcon.MaxCPUSpeed = 1f;
	//	HardwareController.hdcon.Cores = 1;
	//	HardwareController.hdcon.MaxTEMP = 70;
	//	HardwareController.hdcon.PowerEff = 0.035f;
	//	HardwareController.hdcon.ThrottleTEMP = 70;
	//	HardwareController.hdcon.CPUEff = 10;
	//	HardwareController.hdcon.CPUVoltage = 1;
	//	HardwareController.hdcon.Save();
	//}

	void AccountSetup()
	{
		//GameControl.control.LoginUser.Add(GetRandomNumber(6,6));
		//GameControl.control.LoginPass.Add(GetRandomString(12,12));
		//GameControl.control.LoginDesc.Add("Bank Account Details");
		//GameControl.control.LoginSite.Add("www.lecbank.com");
	}

	// Update is called once per frame
	void Update ()
    {
        //ScriptHandel();
        if (Customize.cust.ScreenSaverEnabled == true && ss.enabled == false)
		{
			ss.enabled = true;
		}
			
		if(trace.timer <= 0 && trace.startTrace == true)
		{
			GameControl.control.Fines++;
			//GameControl.control.Balance[GameControl.control.SelectedBank] -= 500 * GameControl.control.Fines;
			trace.startTrace = false;
			ib.showAddressBar = true;
			pro.Password = "";
			ib.Username = "";
		}

        //if (GameControl.control.RepLevel [0] == 0) 
        //{
        //	GameControl.control.Contracts.Add (new MissionSystem ("REVA Test","Test Data","REVA Test Server","www.jaildew.com","Welcome to your new gateway before we make you an agent we need you to do a task for us delete the test file and we will make you an offical memeber","Test",0,Random.Range (0, 0),Random.Range (200, 200),MissionSystem.MissionType.TDelete));
        //}

        //if (HardwareController.hdcon.networkspeed == 0) 
		//{
		//	HardwareController.hdcon.networkspeed = 0.25f;
		//}

		if (Customize.cust.SSActiveTime == 0)
		{
			Customize.cust.SSActiveTime = 60;
		}
	}
}