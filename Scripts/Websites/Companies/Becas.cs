using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Becas : MonoBehaviour
{
	public int StartCount;
	public List<string> EmailSubject = new List<string>();
	public List<string> NoteTitle = new List<string>();

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
	private Defalt def;

	private WebSec ws;
	private PasswordList pl;
	private CLICommandsV2 clic;

	public WebSecSystem WebSec;

	public Color32 buttonColor = new Color32(0, 0, 0, 0);
	public Color32 fontColor = new Color32(0, 0, 0, 0);

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
	public List<ProgramSystem> PageFile2 = new List<ProgramSystem>();

	public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
	public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

	public UACSystem LoggedInAs;

	void Start()
	{
		Computer = GameObject.Find("Computer");
		Prompts = GameObject.Find("Prompts");
		Applications = GameObject.Find("Applications");
		Hacking = GameObject.Find("Hacking");
		System = GameObject.Find("System");
		StartCount = Random.Range(25, 100);

		MaxPublicFiles = Random.Range(25, 50);
		MaxPrivateFiles = Random.Range(25, 50);

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

		buttonColor.r = 0;
		buttonColor.g = 100;
		buttonColor.b = 100;
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

		if (GameControl.control.CompanyServerData.Count > 0)
		{
			for (int Index = 0; Index < GameControl.control.CompanyServerData.Count; Index++)
			{
				if (GameControl.control.CompanyServerData[Index].Name == "Becas")
				{
					PublicFileCount = 0;
					PrivateFileCount = 0;
					if (GameControl.control.CompanyServerData[Index].Files.Count > 0)
					{
						for (int i = 0; i < GameControl.control.CompanyServerData[Index].Files.Count; i++)
						{
							if (GameControl.control.CompanyServerData[Index].Files[i].Description == "Public")
							{
								PublicFileCount++;
							}
							if (GameControl.control.CompanyServerData[Index].Files[i].Description == "Private")
							{
								PrivateFileCount++;
							}
						}
					}
				}
			}
		}

		if (PublicFileCount <= MaxPublicFiles || PrivateFileCount <= MaxPrivateFiles)
		{
			GenFiles = true;
		}

		if (GenFiles == true)
		{
			FileSystemGenerator();
		}

		if (PublicCount == MaxPublicFiles || PrivateCount == MaxPrivateFiles)
		{
			GenFiles = false;
		}
	}

	void PasswordSetup()
	{
		if (Accounts.Count > 0)
		{
			for (int j = 0; j < Accounts.Count; j++)
			{
				if (Accounts[j].UserName == LoggedInAs.UserName)
				{
					if (ib.CurrentSecurity.Count > 0)
					{
						for (int i = 0; i < ib.CurrentSecurity.Count; i++)
						{
							if (ib.CurrentSecurity[i].Type == WebSecSystem.SecType.UAC)
							{
								if (ib.CurrentSecurity[i].Level > 3)
								{
									SiteAdminPass = StringGenerator.RandomMixedChar(8, 8);
									Accounts[j].Password = SiteAdminPass;

								}
								else
								{
									int Index = Random.Range(0, pl.PasswordWords.Count);
									SiteAdminPass = pl.PasswordWords[Index].Trim();
									Accounts[j].Password = SiteAdminPass;
								}
							}
						}
					}
				}
				else
				{
					if (ib.CurrentSecurity.Count > 0)
					{
						for (int i = 0; i < ib.CurrentSecurity.Count; i++)
						{
							if (ib.CurrentSecurity[i].Type == WebSecSystem.SecType.UAC)
							{
								if (ib.CurrentSecurity[i].Level > 3)
								{
									SiteAdminPass = StringGenerator.RandomMixedChar(8, 8);
									Accounts.Add(new UACSystem("Admin", SiteAdminPass, "", "123.456.789", "", false, UACSystem.AccountType.Admin));

								}
								else
								{
									int Index = Random.Range(0, pl.PasswordWords.Count);
									SiteAdminPass = pl.PasswordWords[Index].Trim();
									Accounts.Add(new UACSystem("Admin", SiteAdminPass, "", "123.456.789", "", false, UACSystem.AccountType.Admin));
								}
							}
						}
					}
				}
			}
		}
		else
		{
			if (ib.CurrentSecurity.Count > 0)
			{
				for (int i = 0; i < ib.CurrentSecurity.Count; i++)
				{
					if (ib.CurrentSecurity[i].Type == WebSecSystem.SecType.UAC)
					{
						if (ib.CurrentSecurity[i].Level > 3)
						{
							SiteAdminPass = StringGenerator.RandomMixedChar(8, 8);
							Accounts.Add(new UACSystem("Admin", SiteAdminPass, "", "123.456.789", "", false, UACSystem.AccountType.Admin));

						}
						else
						{
							int Index = Random.Range(0, pl.PasswordWords.Count);
							SiteAdminPass = pl.PasswordWords[Index].Trim();
							Accounts.Add(new UACSystem("Admin", SiteAdminPass, "", "123.456.789", "", false, UACSystem.AccountType.Admin));
						}
					}
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
		for (int i = 0; i < 1; i++)
		{
			string FileName = StringGenerator.RandomMixedChar(4, 4);
			float FileSize = Random.Range(1, 10);
			float FileTypePick = Random.Range(1, 10);
			if (FileTypePick <= 5)
			{
				if (PublicCount <= MaxPublicFiles)
				{
					for (int CompanyIndex = 0; CompanyIndex < GameControl.control.CompanyServerData.Count; CompanyIndex++)
					{
						switch (GameControl.control.CompanyServerData[CompanyIndex].Name)
						{
							case "Becas":
								for (int CompanyPageIndex = 0; CompanyPageIndex < GameControl.control.CompanyServerData[CompanyIndex].WebPages.Count; CompanyPageIndex++)
								{
									switch (GameControl.control.CompanyServerData[CompanyIndex].WebPages[CompanyPageIndex].Name)
									{
										case "Temp Files":
											string FileLocation = GameControl.control.CompanyServerData[CompanyIndex].WebPages[CompanyPageIndex].Target;
											//GameControl.control.WebsiteFiles.Add(new ProgramSystem(FileName, "", "", "", "", "", "Becas Public", "", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
											GameControl.control.CompanyServerData[CompanyIndex].Files.Add(new ProgramSystem(FileName, "", "", "", "", "Public", FileLocation, "", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
											break;
									}
								}
								break;
						}
					}
					PublicCount++;
				}
			}
			if (FileTypePick > 5)
			{
				if (PrivateCount <= MaxPrivateFiles)
				{
					for (int CompanyIndex = 0; CompanyIndex < GameControl.control.CompanyServerData.Count; CompanyIndex++)
					{
						switch (GameControl.control.CompanyServerData[CompanyIndex].Name)
						{
							case "Becas":
								for (int CompanyPageIndex = 0; CompanyPageIndex < GameControl.control.CompanyServerData[CompanyIndex].WebPages.Count; CompanyPageIndex++)
								{
									switch (GameControl.control.CompanyServerData[CompanyIndex].WebPages[CompanyPageIndex].Name)
									{
										case "Internal Files":
											string FileLocation = GameControl.control.CompanyServerData[CompanyIndex].WebPages[CompanyPageIndex].Target;
											GameControl.control.CompanyServerData[CompanyIndex].Files.Add(new ProgramSystem(FileName, "", "", "", "", "Private", FileLocation, "", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
											break;
									}
								}
								break;
						}
					}
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
		PasswordSetup();
		if (Accounts.Count > 0)
		{
			for (int i = 0; i < Accounts.Count; i++)
			{
				if (LoggedInAs.UserName == Accounts[i].UserName)
				{
					Accounts[i].LoggedIn = false;
					Accounts[i].LoggedInIP = "";
					LoggedInAs = null;
				}
			}
		}
		//trace.stopping = true;
		ib.Username = "";
		ib.showAddressBar = true;
		ib.AddressBar = "www.becassystems.com/home";
		UsrName = "";
		password = "";
		//sm.Disconnect();
	}

	public void FileDownload()
	{
		clic.CommandLine = "dl▓" + PageFile2[Select].Name;
		clic.CheckInput();
		clic.CommandLine = "";
		Select = -1;
		showMenu = false;
	}

	public void FileDelete()
	{
		clic.CommandLine = "-r▓rm▓" + PageFile2[Select].Name;
		clic.CheckInput();
		clic.CommandLine = "";
		Select = -1;
		showMenu = false;
	}

	public void RenderSite()
	{
		GUI.backgroundColor = buttonColor;
		GUI.contentColor = fontColor;
		NewWebsiteStuff();
	}

	void RefreshPage()
	{
		PageFile1.RemoveRange(0, PageFile1.Count);
		for (int Index = 0; Index < GameControl.control.CompanyServerData.Count; Index++)
		{
			if (GameControl.control.CompanyServerData[Index].Name == "Becas")
			{
				for (int i = 0; i < GameControl.control.CompanyServerData[Index].WebPages.Count; i++)
				{
					if (GameControl.control.CompanyServerData[Index].WebPages[i].Location == ib.AddressBar)
					{
						if (!PageFile1.Contains(GameControl.control.CompanyServerData[Index].WebPages[i]))
						{
							PageFile1.Add(GameControl.control.CompanyServerData[Index].WebPages[i]);
						}
					}
				}
			}
		}
	}

	void RefreshFiles()
	{
		PageFile2.RemoveRange(0, PageFile2.Count);
		for (int Index = 0; Index < GameControl.control.CompanyServerData.Count; Index++)
		{
			if (GameControl.control.CompanyServerData[Index].Name == "Becas")
			{
				if (GameControl.control.CompanyServerData[Index].Files.Count > 0)
				{
					for (int i = 0; i < GameControl.control.CompanyServerData[Index].Files.Count; i++)
					{
						if (GameControl.control.CompanyServerData[Index].Files[i].Location == ib.AddressBar)
						{
							if (!PageFile2.Contains(GameControl.control.CompanyServerData[Index].Files[i]))
							{
								PageFile2.Add(GameControl.control.CompanyServerData[Index].Files[i]);
							}
						}
					}
				}
			}
		}
	}

	void Request()
	{
		if (PageFile1.Count > 0)
		{
			for (int i = 0; i < PageFile1.Count; i++)
			{
				clic.PastCommands.Add("#" + i + " " + PageFile1[i].Name);
			}
		}

		if (PageFile2.Count > 0)
		{
			for (int i = 0; i < PageFile2.Count; i++)
			{
				clic.PastCommands.Add("#" + i + " " + PageFile2[i].Name);
			}
		}

		ib.Request = false;
	}

	void NewWebsiteStuff()
	{
		Home();
		RefreshPage();
		RefreshFiles();
		if (PageFile1.Count > 0)
		{
			if (ib.Request == true)
			{
				Request();
			}


			for (int i = 0; i < PageFile1.Count; i++)
			{
				if (GUI.Button(new Rect(10, 35 + 30 * i, 100, 22), PageFile1[i].Name))
				{
					//ib.Inputted = PageFile1[i].Target;
					ib.AddressBar = PageFile1[i].Target;
				}
			}
		}

		NewCheck();
		WebsiteStuff();
	}

	void Home()
	{
		if (ib.AddressBar == "www.becassystems.com")
		{
			ib.AddressBar = "www.becassystems.com/home";
		}
	}

	void NewCheck()
	{
		if (ib.AddressBar == "www.becassystems.com/tempfiles")
		{
			ib.CurrentLocation = "Becas";

			if (ib.Request == true)
			{
				Request();
			}

			GUI.Label(new Rect(115, 50, 500, 500), "File Name");
			GUI.Label(new Rect(200, 50, 500, 500), "Size");

			if (showMenu == true)
			{
				if (GUI.Button(new Rect(10, 105, 100, 20), "Delete " + PageFile2[Select].Name))
				{
					FileDelete();
				}
				if (GUI.Button(new Rect(10, 145, 100, 20), "Download " + PageFile2[Select].Name))
				{
					FileDownload();
				}
			}
			scrollpos = GUI.BeginScrollView(new Rect(130, 75, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));

			if (scrollsize > PageFile2.Count)
			{
				scrollsize = 0;
			}

			if (PageFile2.Count > 0)
			{
				for (scrollsize = 0; scrollsize < PageFile2.Count; scrollsize++)
				{
					if (GUI.Button(new Rect(3, scrollsize * 20, 80, 20), "" + PageFile2[scrollsize].Name))
					{
						showMenu = true;
						Select = scrollsize;
					}
					GUI.Button(new Rect(85, scrollsize * 20, 40, 20), "" + PageFile2[scrollsize].Used);
				}

			}

			GUI.EndScrollView();
		}
	}

	void WebsiteStuff()
	{
		switch (ib.AddressBar)
		{
			case "www.becassystems.com/internal/files":
				if (LoggedInAs.LoggedIn == true)
				{
					ib.CurrentLocation = "Becas";

					if (ib.Request == true)
					{
						Request();
					}

					GUI.Label(new Rect(115, 50, 500, 500), "File Name");
					GUI.Label(new Rect(200, 50, 500, 500), "Size");

					if (showMenu == true)
					{
						if (GUI.Button(new Rect(10, 105, 100, 20), "Delete " + PageFile2[Select].Name))
						{
							FileDelete();
						}
						if (GUI.Button(new Rect(10, 145, 100, 20), "Download " + PageFile2[Select].Name))
						{
							FileDownload();
						}
					}
					scrollpos = GUI.BeginScrollView(new Rect(130, 75, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));

					if (scrollsize > PageFile2.Count)
					{
						scrollsize = 0;
					}

					if (PageFile2.Count > 0)
					{
						for (scrollsize = 0; scrollsize < PageFile2.Count; scrollsize++)
						{
							if (GUI.Button(new Rect(3, scrollsize * 20, 80, 20), "" + PageFile2[scrollsize].Name))
							{
								showMenu = true;
								Select = scrollsize;
							}
							GUI.Button(new Rect(85, scrollsize * 20, 40, 20), "" + PageFile2[scrollsize].Used);
						}

					}

					GUI.EndScrollView();
				}
				break;

			case "www.becassystems.com/login":

				for (int i = 0; i < Accounts.Count; i++)
				{
					if (UsrName == Accounts[i].UserName)
					{
						ib.Username = UsrName;
						ib.SiteAdminPass = Accounts[i].Password;
					}
				}

				UsrName = GUI.TextField(new Rect(85, 55, 120, 20), UsrName, 500);
				password = GUI.TextField(new Rect(85, 75, 120, 20), password, 500);

				GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
				GUI.Label(new Rect(3, 75, 500, 500), "Password: ");
				ib.showAddressBar = false;

				//if (prog.Running == false)
				//{
				//	if (GUI.Button(new Rect(245, 30, 50, 20), "Back"))
				//	{
				//		//trace.UpdateTimer = false;
				//		ib.showAddressBar = true;
				//		UsrName = "";
				//		password = "";
				//		//sm.BounceIPs.Remove(sm.BecasIP);
				//		//sm.BouncedConnections.Remove(sm.BecasPos);
				//		ib.AddressBar = "www.becassystems.com";
				//	}
				//}

				for (int i = 0; i < Accounts.Count; i++)
				{
					if (UsrName == Accounts[i].UserName && password == Accounts[i].Password)
					{
						if (GUI.Button(new Rect(10, 125, 100, 20), "Login"))
						{
							//Accounts[i].LoggedInIP = GameControl.control.Gateway.InstalledModem[0].ModemIP;
							Accounts[i].LoggedIn = true;
							LoggedInAs = Accounts[i];
							//trace.UpdateTimer = true;
							ib.showAddressBar = false;
							ib.AddressBar = "www.becassystems.com/internal";
							//log.log.Add(GameControl.control.fullip);
						}
					}
				}
				break;

			case "www.becassystems.com/signout":
				SignOut();
				break;
		}
	}
}