using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMan : MonoBehaviour
{
	GameObject System;
	GameObject Applications;
	GameObject Other;
	GameObject Hacking;
	GameObject Computer;
	GameObject Prompts;
    GameObject WindowHandel;
    GameObject QA;
    GameObject VideoStuff;

    Defalt def;

    WindowManager winman;

	//MissionBrow mb;
	DirSearch dirsearch;
	TreeView treeview;
	SystemMap systemMap;
	WebSecViewer websecviewer;
	TextReader textreader;

	//DESKTOP ENVIROMENT
	CustomTheme customtheme;
	AppMenu startmenu;
	RezPrompt rezprompt;
	Desktop desktop;
    VolumeController vc;


    //HACKING SOFTWARE
    Progtive pro;
	Tracer trace;
	DicCrk dicCrk;
	PasswordCracker passwordcracker;

	//STOCK SYSTEMS
//	Portfolio port;
//	SharePrompt shareprompt;

	//OTHER PROMPTS
	PurchasePrompt purchaseprompt;

	//SYSTEM PROMPTS
	ErrorProm errorprompt;
	ShutdownProm shutdownprompt;
	InstallPrompt installprompt;

	//SYSTEM SOFTWARE
	Clock sysclock;
	CLIV2 cliv2;
	OS os;
	SystemPanel systempanel;
	TaskViewer tasks;
	Computer com;
    DiskManV2 diskmanv2;
    VersionViewer vv;
	FileExplorer fp;
	SystemResourceManager SRM;
    DeviceManager deviceman;
    Executor executor;
    GatewayViewer gatewayviewer;
    RealExeCreator realexecreator;

	//LEGAL APPLICATIONS
	EmailClient email;
	Calculator caluclator;
	Notepad notepad;
	Notepadv2 notepadv2;
    Notepadv3 notepadv3;
    AccLog accountlogs;
	MusicPlayer mp;
    NotificationViewer nv;
    PlanViewer pv;
    Calendar calendar;
    CalendarV2 calendarv2;
    EventViewer eventview;
	ExchangeViewer exchangeviewer;
    MediaPlayer media;

    BugReport qa;

	//INTERNET BROWSERS
	InternetBrowser internetbrowser;
	NetViewer edgebrowser;
	Firefox firefoxbrowser;

	//BROWSER STUFF
	SiteList history;

	//OTHER CONNECTION DEVICE
	RemoteView rv;
	VMDesigner vmd;

	Boot boot;

    //BugReport bugreport;

    public string PromptTitle;
    public string PromptMessage;
    public bool PlaySound;
    public int SoundSelect;

    public bool hotkey;

    public string ProgramName;
    public string SelectedApp;

    public string filename;
    public string content;
    public string location;
    public bool showfilecontent;
    public int selecteddocument;
    public bool isDocumentReadingRunning;

    public bool AppsMenu;

    public bool FakeApp;
    // Use this for initialization
    void Start ()
	{
		System = GameObject.Find("System");
		Applications = GameObject.Find("Applications");
		Hacking = GameObject.Find("Hacking");
		Other = GameObject.Find("Other");
		Computer = GameObject.Find("Computer");
		Prompts = GameObject.Find("Prompts");
        WindowHandel = GameObject.Find("WindowHandel");
        QA = GameObject.Find("QA");
        VideoStuff = GameObject.Find("Video Stuff");

        winman = WindowHandel.GetComponent<WindowManager>();

        history = Computer.GetComponent<SiteList>();
		websecviewer = Computer.GetComponent<WebSecViewer>();

        qa = QA.GetComponent<BugReport>();

        //DESKTOP ENVIROMENT
        customtheme = Computer.GetComponent<CustomTheme>();
		startmenu = System.GetComponent<AppMenu>();
		rezprompt = Prompts.GetComponent<RezPrompt>();
		desktop = System.GetComponent<Desktop>();
        vc = System.GetComponent<VolumeController>();

        //HACKING SOFTWARE
        pro = Hacking.GetComponent<Progtive>();
		trace = Hacking.GetComponent<Tracer>();
		dirsearch = Hacking.GetComponent<DirSearch>();
		dicCrk = Hacking.GetComponent<DicCrk>();
		passwordcracker = Hacking.GetComponent<PasswordCracker>();
		dirsearch = Hacking.GetComponent<DirSearch>();

		//STOCK SYSTEMS
//		port = Applications.GetComponent<Portfolio>();
//		shareprompt = Prompts.GetComponent<SharePrompt>();

		//OTHER PROMPTS
		purchaseprompt = Prompts.GetComponent<PurchasePrompt>();

        media = VideoStuff.GetComponent<MediaPlayer>();

        //SYSTEM PROMPTS
        errorprompt = Prompts.GetComponent<ErrorProm>();
		shutdownprompt = Prompts.GetComponent<ShutdownProm>();
		installprompt = Prompts.GetComponent<InstallPrompt>();

		//SYSTEM SOFTWARE
		sysclock = System.GetComponent<Clock>();
		cliv2 = System.GetComponent<CLIV2>();
		os = System.GetComponent<OS>();
		systempanel = System.GetComponent<SystemPanel>();
		tasks = System.GetComponent<TaskViewer>();
		com = System.GetComponent<Computer>();
        diskmanv2 = System.GetComponent<DiskManV2>();
        vv = System.GetComponent<VersionViewer>();
		fp = System.GetComponent<FileExplorer>();
		SRM = System.GetComponent<SystemResourceManager>();
		boot = System.GetComponent<Boot>();
        deviceman = System.GetComponent<DeviceManager>();
        executor = System.GetComponent<Executor>();
        gatewayviewer = System.GetComponent<GatewayViewer>();
        realexecreator = System.GetComponent<RealExeCreator>();

        //LEGAL APPLICATIONS
        email = Applications.GetComponent<EmailClient>();
		caluclator = Applications.GetComponent<Calculator>();
		notepad = Applications.GetComponent<Notepad>();
		notepadv2 = Applications.GetComponent<Notepadv2>();
        notepadv3 = Applications.GetComponent<Notepadv3>();
        accountlogs = Applications.GetComponent<AccLog>();
		mp = Applications.GetComponent<MusicPlayer>();
		treeview = Applications.GetComponent<TreeView>();
        nv = Applications.GetComponent<NotificationViewer>();
        pv = Applications.GetComponent<PlanViewer>();
        calendar = Applications.GetComponent<Calendar>();
        calendarv2 = Applications.GetComponent<CalendarV2>();
        eventview = Applications.GetComponent<EventViewer>();
		exchangeviewer = Applications.GetComponent<ExchangeViewer>();

        //INTERNET BROWSERS
        internetbrowser = Applications.GetComponent<InternetBrowser>();
		edgebrowser = Applications.GetComponent<NetViewer>();
		firefoxbrowser = Applications.GetComponent<Firefox>();

		//APPLICATIONS
		systemMap = Applications.GetComponent<SystemMap>();
		textreader = Applications.GetComponent<TextReader>();

		//BROWSER STUFF
		history = Computer.GetComponent<SiteList>();

		//OTHER CONNECTION DEVICE
		rv = Applications.GetComponent<RemoteView>();
		vmd = Other.GetComponent<VMDesigner>();

        filename = "";
        content = "";
        location = "";
        selecteddocument = 0;
    }

	public void Update()
	{
        if (GameControl.control.Gateway.Status.Terminal == true)
        {
            if(cliv2.enabled == false && boot.booting == false)
            {
                SelectedApp = "Command Line V3";
            }
        }

        switch (SelectedApp) 
		{
        case "Bug Report":
            if (qa.enabled == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Bug Report";
                SRM.CPUUsage = 0.00f;
                SRM.MemoryUsage = 0f;
                SRM.SelectedProgramsWindowID = qa.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                qa.enabled = true;
                qa.show = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Bug Report";
                SRM.CPUUsage = 0.00f;
                SRM.MemoryUsage = 0f;
                SRM.SelectedProgramsWindowID = qa.windowID;
                SRM.RemoveProgramUsage();
                qa.enabled = false;
                qa.show = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

         case "fakeapp":
                if(FakeApp == true)
                {
                    SRM.ProgramName = ProgramName;
                    SRM.ApplicationName = "";
                    SRM.CPUUsage = 0.00f;
                    SRM.MemoryUsage = 0f;
                    SRM.SelectedProgramsWindowID = -2;
                    SRM.AddProgramUsage(ProgramName, ProgramName);
                    FakeApp = false;
                }
                break;

            case "Notepad":
			if (notepad.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
				SRM.ApplicationName = "Notepad";
				SRM.CPUUsage = 0.05f;
				SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = notepad.windowID;
				SRM.AddProgramUsage(ProgramName, "Notepad");
				notepad.enabled = true;
				notepad.show = true;
                notepad.CurrentWorkingTitle = filename;
                notepad.TypedTitle = filename;
                notepad.TypedText = content;
                notepad.SaveLocation = location;
                notepad.ShowFileContent = true;
                notepad.SelectedDocument = selecteddocument;
                isDocumentReadingRunning = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Notepad";
				SRM.CPUUsage = 0.05f;
				SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = notepad.windowID;
				SRM.RemoveProgramUsage();
				notepad.enabled = false;
				notepad.show = false;
                filename = "";
                content = "";
                location = "";
                selecteddocument = 0;
                showfilecontent = false;
                isDocumentReadingRunning = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

        case "Device Manager":
            if (deviceman.enabled == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Device Manager";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = deviceman.windowID;
                SRM.AddProgramUsage(ProgramName, "Device Manager");
                deviceman.enabled = true;
                deviceman.show = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Device Manager";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = deviceman.windowID;
                SRM.RemoveProgramUsage();
                deviceman.enabled = false;
                deviceman.show = false;
                SelectedApp = ""; ProgramName = "";
            }
            break;

            case "Notepadv2":
			if (notepadv2.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
				SRM.ApplicationName = "Notepadv2";
				SRM.CPUUsage = 0.05f;
				SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = notepadv2.windowID;
				SRM.AddProgramUsage(ProgramName, "Notepadv2");
				notepadv2.enabled = true;
				notepadv2.show = true;
                notepadv2.CurrentWorkingTitle = filename;
                notepadv2.TypedTitle = filename;
                notepadv2.TypedText = content;
                notepadv2.SaveLocation = location;
                notepadv2.ShowFileContent = true;
                notepadv2.SelectedDocument = selecteddocument;
                isDocumentReadingRunning = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
				SRM.ApplicationName = "Notepadv2";
				SRM.CPUUsage = 0.05f;
				SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = notepadv2.windowID;
				SRM.RemoveProgramUsage();
				notepadv2.enabled = false;
				notepadv2.show = false;
                filename = "";
                content = "";
                location = "";
                selecteddocument = 0;
                showfilecontent = false;
                isDocumentReadingRunning = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

            case "Notepadv3":
			if (notepadv3.quit == false) 
			{ 
                winman.ProgramName = "NotepadV3";
                winman.windowRect = new Rect(200, 200, 300 * Customize.cust.UIScale, 300 * Customize.cust.UIScale);
                winman.AddProgramWindow();

                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Notepadv3";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = winman.TempWID;
                SRM.AddProgramUsage(ProgramName, "Notepadv3");

                notepadv3.enabled = true;
                notepadv3.AddNotepadWindow(filename, location, filename, content, "", selecteddocument);
                notepadv3.quit = false;

                SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Notepadv3";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = winman.TempWID;
                SRM.RemoveProgramUsage();
                filename = "";
                content = "";
                location = "";
                selecteddocument = 0;
                notepadv3.quit = false;
                SelectedApp = "";
                ProgramName = "";
            }
			break;

        case "Calculator":
            if (caluclator.quit == false)
            {
                SRM.ProgramName = ProgramName;
                winman.ProgramName = "Calculator";
                winman.windowRect = new Rect(200, 200, 127, 200);
                winman.AddProgramWindow();
                caluclator.AddNewItems();
                SRM.ApplicationName = "Calculator";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = winman.TempWID;
                SRM.AddProgramUsage(ProgramName, "Calculator");
                caluclator.enabled = true;
                caluclator.show = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                if (caluclator.ProgramCount > 0)
                {
                    SRM.ProgramName = ProgramName;
                    SRM.ApplicationName = "Calculator";
                    SRM.CPUUsage = 0.05f;
                    SRM.MemoryUsage = 256f;
                    SRM.SelectedProgramsWindowID = caluclator.SelectedWindowID;
                    SRM.RemoveProgramUsage();
                    caluclator.quit = false;
                    SelectedApp = ""; 
                    ProgramName = "";
                }
            }
            break;

		case"Account Tracker":
			if (accountlogs.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Account Tracker";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = accountlogs.windowID;
                SRM.AddProgramUsage(ProgramName, "Account Tracker");
                accountlogs.enabled = true;
				accountlogs.show = true;
				SelectedApp = ""; ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Account Tracker";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = accountlogs.windowID;
                SRM.RemoveProgramUsage();
                accountlogs.enabled = false;
				accountlogs.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

        case "Gateway Viewer":
            if (gatewayviewer.enabled == false)
            {
                SRM.ProgramName = "Gateway Viewer";
                SRM.ApplicationName = "Gateway Viewer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = gatewayviewer.windowID;
                SRM.AddProgramUsage(ProgramName, "Gateway Viewer");
                gatewayviewer.enabled = true;
                gatewayviewer.show = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = "Gateway Viewer";
                SRM.ApplicationName = "Gateway Viewer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = gatewayviewer.windowID;
                SRM.RemoveProgramUsage();
                gatewayviewer.enabled = false;
                gatewayviewer.show = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

            case "Error Prompt":
            if (errorprompt.quit == false)
            {
                winman.ProgramName = "Error Prompt";
                winman.windowRect = new Rect(200, 200, 400, 200);
                winman.AddProgramWindow();
                for (int i = 0; i < winman.RunningPrograms.Count; i++)
                {
                    if (winman.RunningPrograms[i].ProgramName == "Error Prompt")
                    {
                        winman.RunningPrograms[i].windowRect = new Rect(20 * i, 30 + 60 * i, 400, 200);
                    }
                }
                SRM.ProgramName = errorprompt.ErrorTitle;
                SRM.ApplicationName = "Error Prompt";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = winman.TempWID;
                errorprompt.ErrorWindowID = SRM.SelectedProgramsWindowID;
                SRM.AddProgramUsage(ProgramName, "Error Prompt");
                errorprompt.AddNewError();
                errorprompt.enabled = true;
                errorprompt.show = true;
                errorprompt.ErrorTitle = PromptTitle;
                errorprompt.ErrorMsg = PromptMessage;


                if (PlaySound == true)
                {
                    errorprompt.playsound = true;
                    errorprompt.SoundSelect = SoundSelect;
                }
                else
                {
                    errorprompt.playsound = false;
                }
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Error Prompt";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = errorprompt.SelectedWindowID;
                SRM.RemoveProgramUsage();
                errorprompt.quit = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;


            case "File Explorer":
			if (fp.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "File Explorer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = fp.windowID;
                SRM.AddProgramUsage(ProgramName, "File Explorer");
                fp.enabled = true;
				fp.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "File Explorer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = fp.windowID;
                SRM.RemoveProgramUsage();
                fp.enabled = false;
				fp.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Email":
			if (email.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Email";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = email.windowID;
                SRM.AddProgramUsage(ProgramName, "Email");
				email.enabled = true;
				email.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Email";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = email.windowID;
                SRM.RemoveProgramUsage();
                email.enabled = false;
				email.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

            case "Media Player":
                if (media.enabled == false)
                {
                    SRM.ProgramName = ProgramName;
                    SRM.ApplicationName = "Media Player";
                    SRM.CPUUsage = 0.05f;
                    SRM.MemoryUsage = 256f;
                    SRM.SelectedProgramsWindowID = media.windowID;
                    SRM.AddProgramUsage(ProgramName, SelectedApp);
                    media.enabled = true;
                    media.show = true;
                    SelectedApp = ""; 
                    ProgramName = "";
                }
                else
                {
                    SRM.ProgramName = ProgramName;
                    SRM.ApplicationName = "Media Player";
                    SRM.CPUUsage = 0.05f;
                    SRM.MemoryUsage = 256f;
                    SRM.SelectedProgramsWindowID = media.windowID;
                    SRM.RemoveProgramUsage();
                    media.enabled = false;
                    media.show = false;
                    SelectedApp = ""; 
                    ProgramName = "";
                }
                break;

            case "Executor":
                if (executor.enabled == false)
                {
                    executor.ProgramName = ProgramName;
                    SRM.ProgramName = ProgramName;
                    SRM.ApplicationName = "Executor";
                    SRM.CPUUsage = 0.05f;
                    SRM.MemoryUsage = 256f;
                    SRM.SelectedProgramsWindowID = executor.windowID;
                    SRM.AddProgramUsage(ProgramName, SelectedApp);
                    executor.enabled = true;
                    executor.show = true;
                    SelectedApp = ""; 
                    ProgramName = "";
                }
                else
                {
                    executor.ProgramName = ProgramName;
                    SRM.ProgramName = ProgramName;
                    SRM.ApplicationName = "Executor";
                    SRM.CPUUsage = 0.05f;
                    SRM.MemoryUsage = 256f;
                    SRM.SelectedProgramsWindowID = executor.windowID;
                    SRM.RemoveProgramUsage();
                    executor.enabled = false;
                    executor.show = false;
                    SelectedApp = ""; 
                    ProgramName = "";
                }
                break;

            case "Volume Controller":
            if (vc.enabled == false)
            {
                SRM.ProgramName = "Audio Settings";
                SRM.ApplicationName = "Audio Settings";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = vc.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                vc.enabled = true;
                vc.show = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = "Audio Settings";
                SRM.ApplicationName = "Audio Settings";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.SelectedProgramsWindowID = vc.windowID;
                SRM.RemoveProgramUsage();
                vc.enabled = false;
                vc.show = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

            case "Accounts":
			if (accountlogs.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Accounts";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = accountlogs.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                accountlogs.enabled = true;
				accountlogs.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Accounts";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = accountlogs.windowID;
                SRM.RemoveProgramUsage();
				accountlogs.enabled = false;
				accountlogs.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Net Viewer":
			if (edgebrowser.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Net Viewer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = edgebrowser.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                edgebrowser.enabled = true;
				edgebrowser.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Net Viewer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = edgebrowser.windowID;
                SRM.RemoveProgramUsage();
				edgebrowser.enabled = false;
				edgebrowser.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Exchange Viewer":
			if (exchangeviewer.enabled == false) 
			{
				SRM.ProgramName = ProgramName;
				SRM.ApplicationName = "Exchange Viewer";
				SRM.CPUUsage = 0.05f;
				SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = exchangeviewer.windowID;
				SRM.AddProgramUsage(ProgramName, SelectedApp);
				exchangeviewer.enabled = true;
				exchangeviewer.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
				SRM.ProgramName = ProgramName;
				SRM.ApplicationName = "Exchange Viewer";
				SRM.CPUUsage = 0.05f;
				SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = exchangeviewer.windowID;
				SRM.RemoveProgramUsage();
				exchangeviewer.enabled = false;
				exchangeviewer.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Password Cracker":
			if (passwordcracker.enabled == false) 
			{
				passwordcracker.enabled = true;
				passwordcracker.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
				passwordcracker.enabled = false;
				passwordcracker.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Trace Tracker":
			if (trace.show == false) 
			{
				trace.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
				trace.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"CHM":
			if (treeview.show == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "CHM";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = treeview.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
				treeview.enabled = true;
				treeview.show = true;
				SelectedApp = "";
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "CHM";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = treeview.windowID;
                SRM.RemoveProgramUsage();
				treeview.enabled = false;
				treeview.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Task Viewer":
			if (tasks.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Task Viewer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = tasks.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
				tasks.enabled = true;
				tasks.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Task Viewer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
				SRM.SelectedProgramsWindowID = tasks.windowID;
                SRM.RemoveProgramUsage();
				tasks.enabled = false;
				tasks.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Dictionary Cracker":
			if (dicCrk.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Dictionary Cracker";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
				dicCrk.enabled = true;
				dicCrk.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Dictionary Cracker";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.RemoveProgramUsage();
			    dicCrk.enabled = false;
				dicCrk.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Directory Searcher":
			if (dirsearch.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Directory Searcher";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
				dirsearch.enabled = true;
				dirsearch.show = true;
				SelectedApp = ""; ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Directory Searcher";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 256f;
                SRM.RemoveProgramUsage();
				dirsearch.enabled = false;
				dirsearch.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Command Line V3":

			if (cliv2.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "CLIv3";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
				SRM.SelectedProgramsWindowID = cliv2.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
				cliv2.enabled = true;
				cliv2.show = true;
				SelectedApp = "";
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "CLIv3";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
				SRM.SelectedProgramsWindowID = cliv2.windowID;
                SRM.RemoveProgramUsage();
				cliv2.enabled = false;
				cliv2.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Remote Viewer":
			if (rv.enabled == false) 
			{
				rv.enabled = true;
				rv.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
				rv.enabled = false;
				rv.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"System Map":
            if (systemMap.show == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Command Line";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = systemMap.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                systemMap.show = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Command Line";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = systemMap.windowID;
                SRM.RemoveProgramUsage();
                systemMap.show = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
			break;

        case "Notification Viewer":
            if (nv.enabled == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Notification Viewer";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = nv.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                nv.show = true;
                nv.enabled = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Notification Viewer";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = nv.windowID;
                SRM.RemoveProgramUsage();
                nv.show = false;
                nv.enabled = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

        case "Plan Viewer":
            if (pv.enabled == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Plan Viewer";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = pv.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                pv.show = true;
                pv.enabled = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Plan Viewer";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = pv.windowID;
                SRM.RemoveProgramUsage();
                pv.show = false;
                pv.enabled = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

            case "Calendar":
            if (calendar.enabled == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Calendar";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = calendar.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                calendar.show = true;
                calendar.enabled = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Calendar";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = calendar.windowID;
                SRM.RemoveProgramUsage();
                calendar.show = false;
                calendar.enabled = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

        case "Calendar v2":
            if (calendarv2.enabled == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Calendar v2";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = calendarv2.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                calendarv2.show = true;
                calendarv2.enabled = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Calendar v2";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = calendarv2.windowID;
                SRM.RemoveProgramUsage();
                calendarv2.show = false;
                calendarv2.enabled = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

        case "Event Viewer":
            if (eventview.enabled == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Event Viewer";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = eventview.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                eventview.show = true;
                eventview.enabled = true;
                SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Event Viewer";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = eventview.windowID;
                SRM.RemoveProgramUsage();
                eventview.show = false;
                eventview.enabled = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

            case "Start Menu":
			if (startmenu.show == false) 
			{
				startmenu.show = true;
                AppsMenu = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
				startmenu.show = false;
                AppsMenu = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Shutdown":
            if(shutdownprompt.enabled == false)
            {
                shutdownprompt.enabled = true;
            }
			if (shutdownprompt.show == false) 
			{
				shutdownprompt.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
				shutdownprompt.show = false;
				SelectedApp = "";
                ProgramName = "";
			}
			break;

		case"System Panel":
			if (systempanel.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "System Panel";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = systempanel.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                systempanel.enabled = true;
                systempanel.show = true;
                SelectedApp = ""; 
                ProgramName = "";
            } 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "System Panel";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = systempanel.windowID;
                SRM.RemoveProgramUsage();
                systempanel.show = false;
                systempanel.enabled = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
            break;

            case "Real Exe Creator":
                if (realexecreator.enabled == false)
                {
                    SRM.ProgramName = ProgramName;
                    SRM.ApplicationName = "Real Exe Creator";
                    SRM.CPUUsage = 0.01f;
                    SRM.MemoryUsage = 4f;
                    SRM.SelectedProgramsWindowID = realexecreator.windowID;
                    SRM.AddProgramUsage(ProgramName, SelectedApp);
                    realexecreator.enabled = true;
                    realexecreator.show = true;
                    SelectedApp = ""; 
                    ProgramName = "";
                }
                else
                {
                    SRM.ProgramName = ProgramName;
                    SRM.ApplicationName = "Real Exe Creator";
                    SRM.CPUUsage = 0.01f;
                    SRM.MemoryUsage = 4f;
                    SRM.SelectedProgramsWindowID = realexecreator.windowID;
                    SRM.RemoveProgramUsage();
                    realexecreator.show = false;
                    realexecreator.enabled = false;
                    SelectedApp = ""; 
                    ProgramName = "";
                }
                break;

            case "Music Player":
			if (mp.enabled == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Music Player";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = mp.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                mp.enabled = true;
                mp.show = true;
                SelectedApp = ""; 
                ProgramName = "";
            } 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Music Player";
                SRM.CPUUsage = 0.01f;
                SRM.MemoryUsage = 4f;
                SRM.SelectedProgramsWindowID = mp.windowID;
                SRM.RemoveProgramUsage();
                mp.show = false;
                mp.enabled = false;
                SelectedApp = ""; 
                ProgramName = "";
            }
			break;

		case"Version":
			if (vv.enabled == false) 
			{
				vv.enabled = true;
				vv.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
				vv.enabled = false;
				vv.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

        case "Disk Manager":
            if (diskmanv2.show == false)
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Disk Manager";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 58f;
				SRM.SelectedProgramsWindowID = diskmanv2.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
                diskmanv2.show = true;
				SelectedApp = ""; 
                ProgramName = "";
            }
            else
            {
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Disk Manager";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 58f;
				SRM.SelectedProgramsWindowID = diskmanv2.windowID;
                SRM.RemoveProgramUsage();
                diskmanv2.show = false;
				SelectedApp = ""; 
                ProgramName = "";
            }
            break;

            case "Computer":
			if (com.show == false) 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Computer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 58f;
				SRM.SelectedProgramsWindowID = com.windowID;
                SRM.AddProgramUsage(ProgramName, SelectedApp);
				com.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
                SRM.ProgramName = ProgramName;
                SRM.ApplicationName = "Computer";
                SRM.CPUUsage = 0.05f;
                SRM.MemoryUsage = 58f;
				SRM.SelectedProgramsWindowID = com.windowID;
                SRM.RemoveProgramUsage();
				com.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;

		case"Store":
			if (edgebrowser.enabled == false) 
			{
				edgebrowser.enabled = true;
				edgebrowser.show = true;
				SelectedApp = ""; 
                ProgramName = "";
			} 
			else 
			{
				edgebrowser.enabled = false;
				edgebrowser.show = false;
				SelectedApp = ""; 
                ProgramName = "";
			}
			break;
		}
	}
}
