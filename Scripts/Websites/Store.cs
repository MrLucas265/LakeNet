using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour 
{
	private GameObject Missions;
	private GameObject AppsSoftware;
	private GameObject SysSoftware;
	private GameObject Prompts;
	private GameObject Computer;
	private GameObject HackingSoftware;

	private InternetBrowser ib;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public bool RemeberMe;

	public string UsrName;
	public string password;
	public string SiteAdminPass;

	public bool logged;

	private MissionGen misgen;
	private MissionBrow brow;
	private ErrorProm ep;
	private Upgrade upg;
	private Defalt defalt;
	private PurchasePrompt pp;
	private SystemMap sm;

	public int Select;

	public float revax;
	public float revay;
	public float revaw;
	public float revah;

	public float revatx;
	public float revaty;
	public float revatw;
	public float revath;

	private Progtive prog;
	private Tracer trace;

	public List<string> RevaSoftware = new List<string>();
	public List<int> Cost = new List<int>();

	public string SelectedProgram;
	public List<string> ListOfSoftware = new List<string>();
	public int Price;
	public float Size;
	public float Version;
	public string Desc;
	public int MaxProgramVersion;
	public int SelectedVersion;
	public string ProgramName;
    public string SystemProgramName;
    public string ProgramType;
	public string ProgramTarget;
	public string Cat;
	public CHMSystem ProgramInfo;
	public int ProgramID;
	public bool Buying;

	public string Username;
	public string Password;
	public bool ShowPassword;

    private FileUtility fu;

    private DiskMan dskman;

    void Start () 
	{
		SysSoftware = GameObject.Find("System");
		Missions = GameObject.Find("Missions");
		AppsSoftware = GameObject.Find("Applications");
		Prompts = GameObject.Find("Prompts");
		Computer = GameObject.Find("Computer");
		HackingSoftware = GameObject.Find("Hacking");

        WebSearch();
		UpdateProgramList();
	}

	void WebSearch()
	{
		ib = AppsSoftware.GetComponent<InternetBrowser>();
		misgen = Missions.GetComponent<MissionGen>();
		ep = Prompts.GetComponent<ErrorProm>();
		upg = Computer.GetComponent<Upgrade>();
		defalt = SysSoftware.GetComponent<Defalt>();
		pp = Prompts.GetComponent<PurchasePrompt>();
		prog = HackingSoftware.GetComponent<Progtive>();
		trace = HackingSoftware.GetComponent<Tracer>();
		brow = Missions.GetComponent<MissionBrow>();
		sm = AppsSoftware.GetComponent<SystemMap>();
        fu = SysSoftware.GetComponent<FileUtility>();
        dskman = SysSoftware.GetComponent<DiskMan>();

    }

	void UpdateProgramList()
	{
		ListOfSoftware.Add("Notepad");
		//ListOfSoftware.Add("Music Player");
		//ListOfSoftware.Add("Paint");
		//ListOfSoftware.Add("Background Downloader");
		ListOfSoftware.Add("NotepadV2");
		ListOfSoftware.Add("TreeOS");
		ListOfSoftware.Add("FluidicIceOS");
		ListOfSoftware.Add("AppatureOS");
	}

	void UpdateUI()
	{
		switch (ProgramName) 
		{
		case "Notepad":
			Price = 250;
			Size = 2;
			Desc = "The password breaker is slower and more expensive then the dircrk but eventually will match the password.";
			Version = 1;
			ProgramTarget = "Notepad";
            SystemProgramName = "Notepad";
			ProgramType = "Exe";
			break;
		case "Music Player":
			Price = 150;
			Size = 1;
			Desc = "Music Player allows the user to play custom .wav music on thier desktop.";
			Version = 1;
			ProgramTarget = "Trace Tracker";
			ProgramType = "Exe";
			break;
		case "Paint":
			Price = 150;
			Size = 5;
			Desc = "Paint is a artistic canvas editor that allows.";
			ProgramTarget = "Paint";
			Version = 1;
			ProgramType = "Exe";
			break;
		case "Background Downloader":
			Price = 500 ;
			Size = 1;
			Desc = "Background Downloader is a program that allows the user to download more backgrounds.";
			Version = 1;
			ProgramTarget = "Background Downloader";
			ProgramType = "Exe";
			break;
		case "TreeOS":
			Price = 500;
			Size = 20;
			Desc = "TreeOS is a icon based shell for ease of use and understanding.";
			Version = 1;
			ProgramTarget = "TreeOS";
            SystemProgramName = "TreeOS";
            ProgramType = "OS";
			break;
		case "FluidicIceOS":
			Price = 500;
			Size = 20;
			Desc = "FluidicIceOS shell enviroment is very user friendly that uses a titlebar and icons.";
			Version = 1;
			ProgramTarget = "FluidicIceOS";
            SystemProgramName = "FluidicIceOS";
            ProgramType = "OS";
			break;
		case "AppatureOS":
			Price = 500;
			Size = 15;
			Desc = "AppatureOS is a light user friendly desktop enviroment to help maximize screen space.";
			Version = 1;
			ProgramTarget = "AppatureOS";
            SystemProgramName = "AppatureOS";
			ProgramType = "OS";
			break;
		case "NotepadV2":
			Price = 500;
			Size = 4;
			Desc = "Version 2 of the classic notepad.";
			Version = 2;
			ProgramTarget = "Notepadv2";
            SystemProgramName = "Notepadv2";
            ProgramType = "Exe";
			break;
		}
	}

	void Bought()
	{
        for (int i = 0; i < dskman.DriveLetter.Length; i++)
        {
            if (Customize.cust.DownloadPath[0] == dskman.DriveLetter[i])
            {
                if (dskman.FreeSpace[i] >= Size)
                {
                    if (GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance >= Price)
                    {
                        GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance -= Price;
                        if (fu.ProgramHandle.Count <= 0)
                        {
                            fu.ProgramHandle.Add(new FileUtilitySystem("Download", ProgramName, Customize.cust.DownloadPath, "", ProgramTarget, "", ProgramType, false, true, true, false, SelectedVersion, 0, 0, 0, 0, 0, 0, 0, Size, 0, 0, 0, FileUtilitySystem.ProgramType.DownloadProgram));
                            fu.AddWindow();
                        }
                        Buying = false;
                    }
                }
            }
        }
    }

	void VersionControl()
	{
		if (SelectedVersion < 1)
		{
			SelectedVersion = 1;
		}

		if (SelectedVersion >= MaxProgramVersion)
		{
			SelectedVersion = MaxProgramVersion;
		}
	}


	public void RenderSite()
	{
		switch (ib.AddressBar) 
		{

		case "www.store.com":
			scrollpos = GUI.BeginScrollView(new Rect(2, 25, 165, 240), scrollpos, new Rect(0, 0, 0, scrollsize*20));
			for (scrollsize = 0; scrollsize < ListOfSoftware.Count; scrollsize++)
			{
				if(GUI.Button(new Rect(0, scrollsize * 20, 149, 20), "" + ListOfSoftware[scrollsize]))
				{
					Select = scrollsize;
					ProgramName = ListOfSoftware[Select];
					SelectedVersion = 1;
					UpdateUI();
				}
			}
			GUI.EndScrollView();

			if (ProgramName != "")
			{
				GUI.Box((new Rect(168,25,330,240)),"");

				GUI.Label(new Rect(171,25,300,300), "Product Name: " + ProgramName);
				GUI.Label(new Rect(171,45,300,300), "Product Type: " + ProgramType);
				GUI.Label(new Rect(171,65,300,300), "Product Desc: " + Desc);
				GUI.Label(new Rect(171,160,300,300), "Product Cost: " + Price);
				GUI.Label(new Rect(171,175,300,300), "Product Size: " + Size);
				GUI.Label(new Rect(171,190,300,300), "Product Version: " + SelectedVersion);
				GUI.Label(new Rect(171,205,300,300), "----------------");


				if (GameControl.control.SoftwareVersion [ProgramID] != 0) 
				{
					GUI.Label(new Rect(171,243,500,500), "Current Product Version: " + Version);
				}

				if(GUI.Button(new Rect(300,275,65,20),"Purchase"))
				{
					Buying = true;
					Bought();
				}

				if(GUI.Button(new Rect(200,275,85,20),"Next Version"))
				{
					SelectedVersion++;
					VersionControl();
					UpdateUI();
				}

				if(GUI.Button(new Rect(100,275,85,20),"Prev Version"))
				{
					SelectedVersion--;
					VersionControl();
					UpdateUI();
				}
			}
			break;
		}
	}
}
