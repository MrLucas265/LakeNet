using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unicom : MonoBehaviour 
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
	}


	void LoadPresetColors()
	{
		//rgb1.r = 255;
		//rgb1.g = 255;
		//rgb1.b = 255;
		//rgb1.a = 255;

		buttonColor.r = 0;
		buttonColor.g = 180;
		buttonColor.b = 0;
		buttonColor.a = 255;

		fontColor.r = 255;
		fontColor.g = 255;
		fontColor.b = 255;
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
		if (SiteAdminPass == "") 
		{
			SiteAdminPass = StringGenerator.RandomMixedChar(8, 8);
		}

		if (WebsiteCount <= GameControl.control.WebsiteFiles.Count)
		{
			for (int i = 0; i < GameControl.control.WebsiteFiles.Count; i++) 
			{
				if (GameControl.control.WebsiteFiles [i].Location == "Unicom Public") 
				{
					PublicFileCount++;
				}

				if (GameControl.control.WebsiteFiles [i].Location == "Unicom Private") 
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
		if (ws.SecLevel > 3)
		{
			SiteAdminPass = StringGenerator.RandomMixedChar(8, 8);
		}
		else
		{
			int Index = Random.Range(0, pl.Words1.Count);
			SiteAdminPass = pl.Words1[Index];
		}
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
					GameControl.control.WebsiteFiles.Add (new ProgramSystem (FileName, "", "", "", "Unicom Public", "", 0, 0, FileSize, 0, 100, 0, false, ProgramSystem.ProgramType.File));
					PublicCount++;
				}
			}
			if (FileTypePick > 5) 
			{
				if (PrivateCount <= MaxPrivateFiles)
				{
					GameControl.control.WebsiteFiles.Add (new ProgramSystem (FileName, "", "", "", "Unicom Private", "", 0, 0, FileSize, 0, 100, 0, false, ProgramSystem.ProgramType.File));
					PrivateCount++;
				}
			}
			//GameControl.control.becassystemsFileSystem.Sort();
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

	public void RenderSite()
	{
		GUI.backgroundColor = buttonColor;
		GUI.contentColor = fontColor;

		switch(ib.AddressBar)
		{
		case "www.unicom.com":
			if (GUI.Button (new Rect (10, 75, 100, 20), "Public")) 
			{
				ib.AddressBar = "www.unicom.com/public";
				ib.showAddressBar = false;
				ib.ClearDirContents ();
				ib.DirContents.Add ("Home");
				ib.DirContents.Add ("Files");
			}  
			if (GUI.Button (new Rect (10, 120, 100, 20), "Sign in")) 
			{
				ib.AddressBar = "www.unicom.com/login";
				ib.showAddressBar = false;
				ib.ClearDirContents ();
			}
			if (ib.Request == true)
			{
				ib.ClearDirContents();
				clic.PastCommands.Add ("www.unicom.com/public");
				clic.PastCommands.Add ("www.unicom.com/login");
				ib.Request = false;
			}
			break;

		case "www.unicom.com/public": 
			if(GUI.Button(new Rect(10,75,100,20),"Home"))
			{
				ib.ClearDirContents();
				trace.UpdateTimer = false;
				ib.showAddressBar = true;
				logged = false;
				UsrName = "";
				password = "";
				//sm.BounceIPs.Remove(sm.JaildewIP);
				//sm.BouncedConnections.Remove(sm.JaildewPos);
				ib.AddressBar = "www.unicom.com";
			}    

			if(GUI.Button(new Rect(10,100,100,20),"Temp Files"))
			{
				ib.ClearDirContents();
				ib.AddressBar = "www.unicom.com/tempfiles";
				//ib.DirContents.Add(GameControl.control.becassystemsPublicFileSize[scrollsize].ToString("F0"));
			}
			if (ib.Request == true)
			{
				ib.ClearDirContents();
				clic.PastCommands.Add ("www.unicom.com");
				clic.PastCommands.Add ("www.unicom.com/tempfiles");
				ib.Request = false;
			}
			break;

		case "www.unicom.com/tempfiles": 
			if (GUI.Button (new Rect (245, 30, 50, 20), "Back"))
			{
				ib.ClearDirContents ();
				ib.AddressBar = "www.unicom.com/public";
			}

			FileCheck();

			ib.CurrentLocation = "Unicom Public";

			if (ib.Request == true)
			{
				int FileCount;
				for (FileCount = 0; FileCount < GameControl.control.WebsiteFiles.Count; FileCount++)
				{
					//ib.DirContents.Add (GameControl.control.becassystemsPublicFileSystem [FileCount].Name);
					if (GameControl.control.WebsiteFiles [FileCount].Location == ib.CurrentLocation)
					{
						clic.PastCommands.Add (GameControl.control.WebsiteFiles[FileCount].Name);
					}
				}

				if (FileCount >= GameControl.control.WebsiteFiles.Count)
				{
					ib.Request = false;
				}
			}

			GUI.Label (new Rect (115, 50, 500, 500), "File Name");
			GUI.Label (new Rect (200, 50, 500, 500), "Size");

			if (showMenu == true) 
			{
				if (GUI.Button (new Rect (10, 105, 100, 20), "Delete " + PageFile[Select].Name)) 
				{
					clic.CommandLine = "-r▓rm▓" + PageFile[Select].Name;
					clic.CheckInput();
					clic.CommandLine = "";
					Select = -1;
					showMenu = false;
				}
				if (GUI.Button (new Rect (10, 145, 100, 20), "Download " + PageFile[Select].Name)) 
				{
					clic.CommandLine = "dl▓" + PageFile[Select].Name;
					clic.CheckInput();
					clic.CommandLine = "";
					Select = -1;
					showMenu = false;
				}
			}
			scrollpos = GUI.BeginScrollView (new Rect (130, 75, 150, 100), scrollpos, new Rect (0, 0, 0, scrollsize * 20));

			if (scrollsize > PageFile.Count)
			{
				scrollsize = 0;
			}

			if(PageFile.Count > 0)
			{
				for (scrollsize = 0; scrollsize < PageFile.Count; scrollsize++)
				{
					if (PageFile[scrollsize].Location == "Unicom Public")
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

			break;

		case "www.unicom.com/filesystem": 
			if (logged == true)
			{
				FileCheck();
				if (GUI.Button(new Rect(5, 55, 100, 20), "Back"))
				{
					ib.AddressBar = "www.unicom.com/internal";
				}

				ib.CurrentLocation = "Unicom Private";

				if (ib.Request == true)
				{
					int FileCount;
					//string FileLocation = "becassystems Private";
					for (FileCount = 0; FileCount < PageFile.Count; FileCount++)
					{
						//ib.DirContents.Add (GameControl.control.becassystemsPublicFileSystem [FileCount].Name);
						if (PageFile[FileCount].Location == ib.CurrentLocation)
						{
							clic.PastCommands.Add (PageFile[FileCount].Name);
						}
					}

					if (FileCount >= PageFile.Count)
					{
						ib.Request = false;
					}
				}

				GUI.Label(new Rect(115, 50, 500, 500), "File Name");
				GUI.Label(new Rect(200, 50, 500, 500), "Size");

				if(showMenu == true)
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

		case "www.unicom.com/login":

			if (UsrName == "Admin") 
			{
				ib.Username = UsrName;
			}

			UsrName = GUI.TextField(new Rect(85, 55, 120, 20), UsrName, 500);
			password = GUI.TextField(new Rect(85, 75, 120, 20), password, 500);

			GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
			GUI.Label(new Rect(3, 75, 500, 500), "Password: ");
			ib.showAddressBar = false;

			if(prog.Running == false)
			{
				if (GUI.Button(new Rect(245,30,50,20), "Back"))
				{
					trace.UpdateTimer = false;
					ib.showAddressBar = true;
					logged = false;
					UsrName = "";
					password = "";
					//sm.BounceIPs.Remove(sm.JaildewIP);
					//sm.BouncedConnections.Remove(sm.JaildewPos);
					ib.AddressBar = "www.unicom.com";
				} 
			}

			if(UsrName == "Admin" && password == SiteAdminPass)
			{
				if(GUI.Button(new Rect(10,125,100,20),"Login"))
				{
					ib.showAddressBar = false;
					logged = true;
					ib.AddressBar = "www.unicom.com/internal";
					trace.UpdateTimer = true;
					//log.log.Add(GameControl.control.fullip);
				}
			}
			break;

		case "www.unicom.com/documents/emails":
			if(logged == true)
			{
				scrollpos = GUI.BeginScrollView(new Rect(115, 75, 125, 100), scrollpos, new Rect(0, 0, 0, scrollsize*20));
				for (scrollsize = 0; scrollsize < EmailSubject.Count; scrollsize++)
				{
					if(GUI.Button(new Rect(3, scrollsize * 20, 120, 20), "" + EmailSubject[scrollsize]))
					{
						tr.show = true;
						tr.Title = EmailSubject[scrollsize];
					}
				}
				GUI.EndScrollView();

				if(GUI.Button(new Rect(245,30,50,20),"Back"))
				{
					ib.AddressBar = "www.unicom.com/internal";
				}
			}
			break;

		case "www.unicom.com/documents":
			if(logged == true)
			{
				if(GUI.Button(new Rect(10,75,100,20),"Emails"))
				{
					ib.AddressBar = "www.unicom.com/documents/emails";
				}
				if(GUI.Button(new Rect(10,100,100,20),"Notes"))
				{
					ib.AddressBar = "www.unicom.com/documents/notes";
				}
				if(GUI.Button(new Rect(10,150,100,20),"Back"))
				{
					ib.AddressBar = "www.unicom.com/internal";
				}
			}
			break;

		case "www.unicom.com/internal":
			if(logged == true)
			{
				if(GUI.Button(new Rect(10,75,100,20),"File System"))
				{
					ib.AddressBar = "www.unicom.com/filesystem";
				}
				if(GUI.Button(new Rect(10,100,100,20),"Documents"))
				{
					ib.AddressBar = "www.unicom.com/documents";
				}
				if(GUI.Button(new Rect(10,125,100,20),"Logs"))
				{
					ib.AddressBar = "www.unicom.com/logs";
				}
				if(GUI.Button(new Rect(10,150,100,20),"Sign Out"))
				{
					trace.stopping = true;
					ib.Username = "";
					ib.showAddressBar = true;
					logged = false;
					UsrName = "";
					password = "";
					PasswordSetup();
					sm.Disconnect();
					ib.AddressBar = "www.unicom.com";
				}
			}
			break;
		}
	}
}