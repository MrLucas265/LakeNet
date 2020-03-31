using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JailDew : MonoBehaviour 
{
	public int StartCount;
	public List<string> EmailSubject = new List<string>();
	public List<string> NoteTitle = new List<string>();

	public bool logged;
	public bool showMenu;

	public int Select;

	public string UsrName;
	public string password;
	public string SiteAdminPass;

	private GameObject Computer;
	private GameObject Prompts;
	private GameObject Applications;
	private GameObject Hacking;
	private GameObject System;

	private InternetBrowser ib;
	private Computer com;
	private ErrorProm ep;
	private Tracer trace;
	private SystemMap sm;
	private TextReader tr;
	private Progtive prog;
	private Defalt def;

	private WebSec ws;
	private PasswordList pl;
	private CLICommandsV2 clic;

    public WebSecSystem WebSec;

	public Color32 buttonColor = new Color32(0,0,0,0);
	public Color32 fontColor = new Color32(0,0,0,0);

	public List<ProgramSystem> PageFile = new List<ProgramSystem>();

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int MaxPublicFiles;
	public int MaxPrivateFiles;

	public int PublicCount;
	public int PrivateCount;

	public int PublicFileCount;
	public int PrivateFileCount;

	public int WebsiteCount;

	public bool GenFiles;

    public List<UACSystem> Accounts = new List<UACSystem>();

	public List<RemoteFileSystem> PageFile1 = new List<RemoteFileSystem>();
	public List<RemoteFileSystem> PageFile2 = new List<RemoteFileSystem>();

	void Start()
	{
		Computer = GameObject.Find("Computer");
		Prompts = GameObject.Find("Prompts");
		Applications = GameObject.Find("Applications");
		Hacking = GameObject.Find("Hacking");
		System = GameObject.Find("System");
		StartCount = Random.Range(25,100);

		MaxPublicFiles = Random.Range(25,50);
		MaxPrivateFiles = Random.Range(25,50);

        LoadPresetColors();
		FileSystemGenerator();
		Documents();
		WebSearch();
		//Directories();
	}

    void SetWebSec()
    {
        ib.ConntectedWebSec = WebSec;
    }


	void LoadPresetColors()
	{
		//rgb1.r = 255;
		//rgb1.g = 255;
		//rgb1.b = 255;
		//rgb1.a = 255;

		buttonColor.r = 255;
		buttonColor.g = 0;
		buttonColor.b = 0;
		buttonColor.a = 255;

		fontColor.r = 0;
		fontColor.g = 255;
		fontColor.b = 0;
		fontColor.a = 255;
	}


	void WebSearch()
	{
		// APPLICATIONS
		ws = Applications.GetComponent<WebSec>();
		sm = Applications.GetComponent<SystemMap>();
		tr = Applications.GetComponent<TextReader>();
		ib = Applications.GetComponent<InternetBrowser>();
		// PROMPTS
		ep = Prompts.GetComponent<ErrorProm>();
		// HACKING
		trace = Hacking.GetComponent<Tracer>();
		prog = Hacking.GetComponent<Progtive>();
		// SYSTEM
		com = System.GetComponent<Computer>();
		def = System.GetComponent<Defalt>();
		pl = System.GetComponent<PasswordList>();
		clic = System.GetComponent<CLICommandsV2>();
	}

	void Update()
	{
		if (Accounts.Count == 0) 
		{
            PasswordSetup();
        }

		if (WebsiteCount <= GameControl.control.WebsiteFiles.Count)
		{
			for (int i = 0; i < GameControl.control.WebsiteFiles.Count; i++) 
			{
				if (GameControl.control.WebsiteFiles [i].Location == "Jaildew Public") 
				{
					PublicFileCount++;
				}

				if (GameControl.control.WebsiteFiles [i].Location == "Jaildew Private") 
				{
					PrivateFileCount++;
				}
				WebsiteCount++;
			}
		}

		if (WebsiteCount == GameControl.control.WebsiteFiles.Count)
		{
			GenFiles = true;
		}

		if (PublicFileCount <= MaxPublicFiles || PrivateFileCount <= MaxPrivateFiles) 
		{
			if (GenFiles == true)
			{
				FileSystemGenerator();
			}
		}

		if (PublicCount == MaxPublicFiles && PrivateCount == MaxPrivateFiles)
		{
			GenFiles = false;
		}
	}

	void FileCheck()
	{
		PageFile.RemoveRange(0, PageFile.Count);

		for (int i = 0; i < GameControl.control.WebsiteFiles.Count; i++)
		{
			if (GameControl.control.WebsiteFiles[i].Location == ib.CurrentLocation)
			{
				PageFile.Add(GameControl.control.WebsiteFiles[i]);
			}
		}
	}

	void PasswordSetup()
	{
        for(int i = 0; i < ib.CurrentSecurity.Count; i++)
        {
            if(ib.CurrentSecurity[i].Type == WebSecSystem.SecType.UAC)
            {
                if(ib.CurrentSecurity[i].Level > 3)
                {
                    SiteAdminPass = StringGenerator.RandomMixedChar(8, 8);
                    Accounts.Add(new UACSystem("Admin", SiteAdminPass, "", "123.456.789", UACSystem.AccountType.Admin));

                }
                else
                {
                    int Index = Random.Range(0, pl.PasswordWords.Count);
                    SiteAdminPass = pl.PasswordWords[Index].Trim();
                    Accounts.Add(new UACSystem("Admin", SiteAdminPass, "", "123.456.789", UACSystem.AccountType.Admin));
                }
            }
        }
        //if (ws.SecLevel > 3)
        //{
        //	SiteAdminPass = GetRandomString (8, 8);
        //}
        //else
        //{
        //	int Index = Random.Range(0, pl.Words1.Count);
        //	SiteAdminPass = pl.Words1[Index];
        //}
    }

	public void FileSystemGenerator()
	{
		for(int i=0; i<1; i++)
		{
			string FileName = StringGenerator.RandomMixedChar(4, 4);
			float FileSize = Random.Range (1, 10);
			float FileTypePick = Random.Range (1, 10);
			if (FileTypePick <= 5) 
			{
				if (PublicCount <= MaxPublicFiles)
				{
					GameControl.control.WebsiteFiles.Add (new ProgramSystem (FileName, "", "", "", "Jaildew Public", "", 0, 0, FileSize, 0, 100, 0, false, ProgramSystem.ProgramType.File));
					PublicCount++;
				}
			}
			if (FileTypePick > 5) 
			{
				if (PrivateCount <= MaxPrivateFiles)
				{
					GameControl.control.WebsiteFiles.Add (new ProgramSystem (FileName, "", "", "", "Jaildew Private", "", 0, 0, FileSize, 0, 100, 0, false, ProgramSystem.ProgramType.File));
					PrivateCount++;
				}
			}
		}
	}

	public void Documents()
	{
		EmailSubject.Add("Secuirty Loophole");
		EmailSubject.Add("Test 1");
		EmailSubject.Add("Test 2");
		NoteTitle.Add("Important Note");
	}

	public void Logs()
	{

	}

    public void SignOut()
    {
        trace.stopping = true;
        ib.Username = "";
        ib.showAddressBar = true;
        ib.AddressBar = "www.jaildew.com";
        logged = false;
        UsrName = "";
        password = "";
        PasswordSetup();
        sm.Disconnect();
    }

    public void FileDownload()
    {
        clic.CommandLine = "dl▓" + PageFile[Select].Name;
        clic.CheckInput();
        clic.CommandLine = "";
        Select = -1;
        showMenu = false;
    }

    public void FileDelete()
    {
        clic.CommandLine = "-r▓rm▓" + PageFile[Select].Name;
        clic.CheckInput();
        clic.CommandLine = "";
        Select = -1;
        showMenu = false;
    }

	public void RenderSite()
	{
		GUI.backgroundColor = buttonColor;
		GUI.contentColor = fontColor;
		WebsiteStuff1();
		WebsiteStuff();
	}

	void Refresh()
	{
		PageFile1.RemoveRange(0, PageFile1.Count);
		for (int Index = 0; Index < GameControl.control.CompanyServers.Count; Index++)
		{
			if(GameControl.control.CompanyServers[Index].Name == "Jaildew")
			{
				for (int i = 0; i < GameControl.control.CompanyServers[Index].WebPages.Count; i++)
				{
					if (GameControl.control.CompanyServers[Index].WebPages[i].Location == ib.AddressBar)
					{
						if (!PageFile1.Contains(GameControl.control.CompanyServers[Index].WebPages[i]))
						{
							PageFile1.Add(GameControl.control.CompanyServers[Index].WebPages[i]);
						}
					}
				}
			}
		}
	}

	void RefreshFiles()
	{
		PageFile2.RemoveRange(0, PageFile2.Count);
		for (int Index = 0; Index < GameControl.control.CompanyServers.Count; Index++)
		{
			if (GameControl.control.CompanyServers[Index].Name == "Jaildew")
			{
				for (int i = 0; i < GameControl.control.CompanyServers[Index].Files.Count; i++)
				{
					if (GameControl.control.CompanyServers[Index].Files[i].Location == ib.AddressBar)
					{
						if (!PageFile2.Contains(GameControl.control.CompanyServers[Index].Files[i]))
						{
							PageFile2.Add(GameControl.control.CompanyServers[Index].Files[i]);
						}
					}
				}
			}
		}
	}

	void WebsiteStuff1()
	{
		Refresh();
		RefreshFiles();
		if (PageFile1.Count > 0)
		{
			for (int i = 0; i < PageFile1.Count; i++)
			{
				if (GUI.Button(new Rect(10, 35 + 30 * i, 100, 22), PageFile1[i].Name))
				{
					//ib.Inputted = PageFile1[i].Target;
					ib.AddressBar = PageFile1[i].Target;
				}
			}
		}

		OldCheck();
	}

	void OldCheck()
	{
		if (ib.AddressBar == "www.jaildew.com/tempfiles")
		{
			ib.CurrentLocation = "Jaildew Public";
			FileCheck();

			GUI.Label(new Rect(115, 50, 500, 500), "File Name");
			GUI.Label(new Rect(200, 50, 500, 500), "Size");

			if (showMenu == true)
			{
				if (GUI.Button(new Rect(10, 105, 100, 20), "Delete " + PageFile[Select].Name))
				{
					FileDelete();
				}
				if (GUI.Button(new Rect(10, 145, 100, 20), "Download " + PageFile[Select].Name))
				{
					FileDownload();
				}
			}
			scrollpos = GUI.BeginScrollView(new Rect(130, 75, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));

			if (scrollsize > PageFile.Count)
			{
				scrollsize = 0;
			}

			if (PageFile.Count > 0)
			{
				for (scrollsize = 0; scrollsize < PageFile.Count; scrollsize++)
				{
					if (PageFile[scrollsize].Location == "Jaildew Public")
					{
						if (GUI.Button(new Rect(3, scrollsize * 20, 80, 20), "" + PageFile[scrollsize].Name))
						{
							showMenu = true;
							Select = scrollsize;
						}
						GUI.Button(new Rect(85, scrollsize * 20, 40, 20), "" + PageFile[scrollsize].Used);
					}
				}

			}

			GUI.EndScrollView();
		}
	}

	void WebsiteStuff()
	{
		switch (ib.AddressBar)
		{
			case "www.jaildew.com/filesystem":
				if (logged == true)
				{
					FileCheck();
					if (GUI.Button(new Rect(5, 55, 100, 20), "Back"))
					{
						ib.AddressBar = "www.jaildew.com/internal";
					}

					ib.CurrentLocation = "Jaildew Private";

					if (ib.Request == true)
					{
						int FileCount;
						//string FileLocation = "becassystems Private";
						for (FileCount = 0; FileCount < PageFile.Count; FileCount++)
						{
							//ib.DirContents.Add (GameControl.control.becassystemsPublicFileSystem [FileCount].Name);
							if (PageFile[FileCount].Location == ib.CurrentLocation)
							{
								clic.PastCommands.Add(PageFile[FileCount].Name);
							}
						}

						if (FileCount >= PageFile.Count)
						{
							ib.Request = false;
						}
					}

					GUI.Label(new Rect(115, 50, 500, 500), "File Name");
					GUI.Label(new Rect(200, 50, 500, 500), "Size");

					if (showMenu == true)
					{
						//					if(GUI.Button(new Rect(10, 105, 100, 20), "Delete " + GameControl.control.JaildewPublicFileName[Select].Name))
						//					{
						//
						//					}
						//					if(GUI.Button(new Rect(10,145,100,20),"Copy " + GameControl.control.JaildewPublicFileName[Select].Name))
						//					{
						//
						//					}
					}
					scrollpos = GUI.BeginScrollView(new Rect(130, 75, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
					for (scrollsize = 0; scrollsize < PageFile.Count; scrollsize++)
					{
						if (GUI.Button(new Rect(3, scrollsize * 20, 80, 20), "" + PageFile[scrollsize].Name))
						{
							showMenu = true;
							Select = scrollsize;
						}
						GUI.Button(new Rect(85, scrollsize * 20, 40, 20), "" + PageFile[scrollsize].Used);
					}
					GUI.EndScrollView();
				}
				break;

			case "www.jaildew.com/login":

				for (int i = 0; i < Accounts.Count; i++)
				{
					if (UsrName == Accounts[i].UserName)
					{
						ib.Username = UsrName;
					}
				}

				UsrName = GUI.TextField(new Rect(85, 55, 120, 20), UsrName, 500);
				password = GUI.TextField(new Rect(85, 75, 120, 20), password, 500);

				GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
				GUI.Label(new Rect(3, 75, 500, 500), "Password: ");
				ib.showAddressBar = false;

				if (prog.Running == false)
				{
					if (GUI.Button(new Rect(245, 30, 50, 20), "Back"))
					{
						trace.UpdateTimer = false;
						ib.showAddressBar = true;
						logged = false;
						UsrName = "";
						password = "";
						//sm.BounceIPs.Remove(sm.JaildewIP);
						//sm.BouncedConnections.Remove(sm.JaildewPos);
						ib.AddressBar = "www.jaildew.com";
					}
				}

				for (int i = 0; i < Accounts.Count; i++)
				{
					if (UsrName == Accounts[i].UserName && password == Accounts[i].Password)
					{
						if (GUI.Button(new Rect(10, 125, 100, 20), "Login"))
						{
							ib.showAddressBar = false;
							logged = true;
							ib.AddressBar = "www.jaildew.com/internal";
							trace.UpdateTimer = true;
							//log.log.Add(GameControl.control.fullip);
						}
					}
				}
				break;

			case "www.jaildew.com/documents/emails":
				if (logged == true)
				{
					scrollpos = GUI.BeginScrollView(new Rect(115, 75, 125, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
					for (scrollsize = 0; scrollsize < EmailSubject.Count; scrollsize++)
					{
						if (GUI.Button(new Rect(3, scrollsize * 20, 120, 20), "" + EmailSubject[scrollsize]))
						{
							tr.show = true;
							tr.Title = EmailSubject[scrollsize];
						}
					}
					GUI.EndScrollView();

					if (GUI.Button(new Rect(245, 30, 50, 20), "Back"))
					{
						ib.AddressBar = "www.jaildew.com/internal";
					}
				}
				break;

			case "www.jaildew.com/documents":
				if (logged == true)
				{
					if (GUI.Button(new Rect(10, 75, 100, 20), "Emails"))
					{
						ib.AddressBar = "www.jaildew.com/documents/emails";
					}
					if (GUI.Button(new Rect(10, 100, 100, 20), "Notes"))
					{
						ib.AddressBar = "www.jaildew.com/documents/notes";
					}
					if (GUI.Button(new Rect(10, 150, 100, 20), "Back"))
					{
						ib.AddressBar = "www.jaildew.com/internal";
					}
				}
				break;

			case "www.jaildew.com/internal":
				if (logged == true)
				{
					if (GUI.Button(new Rect(10, 75, 100, 20), "File System"))
					{
						ib.AddressBar = "www.jaildew.com/filesystem";
					}
					if (GUI.Button(new Rect(10, 100, 100, 20), "Documents"))
					{
						ib.AddressBar = "www.jaildew.com/documents";
					}
					if (GUI.Button(new Rect(10, 125, 100, 20), "Logs"))
					{
						ib.AddressBar = "www.jaildew.com/logs";
					}
					if (GUI.Button(new Rect(10, 150, 100, 20), "Sign Out"))
					{
						SignOut();
					}
				}
				break;
		}
	}
}