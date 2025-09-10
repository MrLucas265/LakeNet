using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetBrowser : MonoBehaviour
{
	private GameObject SysSoftware;
	private GameObject AppSoftware;

	//public bool show;
	public bool showAddressBar;

	public string AddressBar;
	public string Inputted;
	public string DisplayAddress;
	public string CurrentPage;

	private GameObject Database;
	private GameObject Minigames;

	public bool Search;


	public string SiteAdminPass;
	public string Username;

    public WebSecSystem ConntectedWebSec;

	// collection of scripts
	private Becas becas;
	private Test RevaTest;
	private Test1 test1;
	private Ping ping;
    //private Aki aki;
    private Unicom uc;
	private JailDew jd;
	private LECBank LEC;
	private Reva reva;
	private MiniGameWeb mgw;
	private ShareTrades st;
	private TUG tug;
	private WebSec ws;
	private SystemMap sm;
	private ServerHost sh;
	private GStocks gstocks;
	private HardwareSite hs;
	private RevaTest revatest;
//	private Para para;
	private CLICommandsV2 clic;
	private Store store;
    //private MelvenaUniversity melvenauni;
    //private ISD isd;
	private DiskManV2 dskmanv2;
	private FileUtility fu;

	private DragRacer dr;

	public string SiteName;

    public List<WebSecSystem> CurrentSecurity = new List<WebSecSystem>();
    public List<UACSystem> CurrentAccounts = new List<UACSystem>();

    public List<string> DirContents = new List<string>();
	public string CurrentDir;
	public bool Request;

	public bool connected;
	public bool FinishedConnecting;

	public string CurrentLocation;

	public List<string> TempHistory = new List<string>();
	public int SelectedPage;

    public string ErrorCode;
    public string ErrorDesc;
    public string ErrorSoloution;

	public bool Connecting;
	public bool ConnectedViaCLI;

	public ProgramSystem FUFile;

	public float Timer;
	public float InitalTime;
	public bool AllowedToUploadHere;

	public void ClearDirContents()
	{
		DirContents.RemoveRange(0,DirContents.Count);
	}

	void Start()
	{
		SysSoftware = GameObject.Find("System");
		AppSoftware = GameObject.Find("Applications");
		Database = GameObject.Find("Database");
		Minigames = GameObject.Find("MiniGames");

		clic = SysSoftware.GetComponent<CLICommandsV2>();


		ws = AppSoftware.GetComponent<WebSec>();
		sm = AppSoftware.GetComponent<SystemMap>();

		becas = Database.GetComponent<Becas>();
        //aki = Database.GetComponent<Aki>();
        RevaTest = Database.GetComponent<Test>();
		revatest = Database.GetComponent<RevaTest>();
		ping = Database.GetComponent<Ping>();
		uc = Database.GetComponent<Unicom>();
		jd = Database.GetComponent<JailDew>();
//		para = Database.GetComponent<Para>();
		LEC = Database.GetComponent<LECBank>();
		reva = Database.GetComponent<Reva>();
		st = Database.GetComponent<ShareTrades>();
		mgw = Minigames.GetComponent<MiniGameWeb>();
		tug = Database.GetComponent<TUG>();
		sh = Database.GetComponent<ServerHost>();
		test1 = Database.GetComponent<Test1>();
		gstocks = Database.GetComponent<GStocks>();
		hs = Database.GetComponent<HardwareSite>();
		store = Database.GetComponent<Store>();
        //melvenauni = Database.GetComponent<MelvenaUniversity>();
        //isd = Database.GetComponent<ISD>();
		dskmanv2 = SysSoftware.GetComponent<DiskManV2>();
		fu = SysSoftware.GetComponent<FileUtility>();
        //cc = Database.GetComponent<CabbageCorp>();

        dr = Minigames.GetComponent<DragRacer>();

		InitalTime = 1;
		Timer = InitalTime;
	}

    public void Foward()
	{
		if (TempHistory.Count > 0) 
		{
			if (SelectedPage <= 0)
			{
				SelectedPage = 0;
			} 
			else
			{
				SelectedPage--;
			}
			AddressBar = TempHistory[SelectedPage];
			if (!AddressBar.Contains ("/")) 
			{
				Inputted = TempHistory[SelectedPage];
			}
		}
	}

	public void Back()
	{
		if (TempHistory.Count > 0) 
		{
			if (SelectedPage >= TempHistory.Count-1)
			{
				SelectedPage = TempHistory.Count-1;
			} 
			else 
			{
				SelectedPage++;
			}

			AddressBar = TempHistory[SelectedPage];

			if (!AddressBar.Contains ("/")) 
			{
				Inputted = TempHistory[SelectedPage];
			}
		}
	}

    public void ConnectionError()
    {
        ErrorCode = "ERROR 404";
        ErrorDesc = "The website that you are trying to reach is unavalible";
        ErrorSoloution = "Check your network or contact the owners of the website";
    }

	public void ConnectingText()
	{
		ErrorCode = "";
		ErrorDesc = "Currently Loading webpage";
		ErrorSoloution = "Please wait while were connecting you.";
	}

	public void StartConnectionProcess()
	{
	}

	public void SiteConnection()
	{
		Timer = InitalTime;
		ConnectingText();

		if (FinishedConnecting == true)
		{
			SiteConnected();
		}
	}

	public void ClearCurrentConnectionStuff()
	{
		CurrentSecurity.RemoveRange(0, CurrentSecurity.Count);
		CurrentAccounts.RemoveRange(0, CurrentAccounts.Count);
		Username = "";
	}

	public void SiteConnectingStuff()
	{
		connected = false;
		InitalTime = 1;
		Inputted = AddressBar;
		SiteConnection();
	}

	public void DownloadManager(ProgramSystemv2 file)
	{
		string SelectedOSPath = "";

		if (PersonController.control.People.Count > 0)
		{
			for (int i = 0; i < PersonController.control.People.Count; i++)
			{
				if(PersonController.control.People[i].Action == "Download")
				{
					if (PersonController.control.People[i].Gateway.StorageDevices.Count > 0)
					{
						for (int j = 0; j < PersonController.control.People[i].Gateway.StorageDevices.Count; j++)
						{
							if (PersonController.control.People[i].Gateway.StorageDevices[j].OS.Count > 0)
							{
								for (int k = 0; k < PersonController.control.People[i].Gateway.StorageDevices[j].OS.Count; k++)
								{
									if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[k].Options.Selected == true)
									{
										SelectedOSPath = PersonController.control.People[i].Gateway.StorageDevices[j].OS[k].FPC.DownloadPath;
									}

									if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[k].Partitions.Count > 0)
									{
										for (int l = 0; l < PersonController.control.People[i].Gateway.StorageDevices[j].OS[k].Partitions.Count; l++)
										{
											if (SelectedOSPath.StartsWith(PersonController.control.People[i].Gateway.StorageDevices[j].OS[k].Partitions[l].DriveLetter))
											{
												if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Free >= file.Used)
												{
													if (fu.ProgramHandle.Count <= 0)
													{
														fu.ProgramHandle.Add(new FileUtilitySystem("Download", SelectedOSPath, false, true, true, false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, FileUtilitySystem.ProgramType.DownloadProgram, i, 0, file));
														file.Location = SelectedOSPath;
														//fu.ProgramHandle.Add(new FUSv2("Download","", FileOrigin, false,true,true,false,0,0,0,0,0,0,0,0,0,0,FUSv2.UtilityType.DownloadProgram,file));
														fu.AddWindow();
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	public void UploadManager()
	{

	}

	public void SiteConnected()
	{
		Timer = InitalTime;
		Inputted = AddressBar;
		connected = true;
		FinishedConnecting = false;
		AddHistory();
    }

	public void AddHistory()
	{
		GameControl.control.Sites.Add (AddressBar);
		TempHistory.Insert(0,AddressBar);

		if (SelectedPage > 0)
		{
			TempHistory.RemoveRange (1, SelectedPage);
			SelectedPage = 0;
		}
	}

	public void Home()
	{
		AddressBar = Customize.cust.WebBrowserHomepage;
		Inputted = Customize.cust.WebBrowserHomepage;
		AddHistory();
	}

	public void WebSiteInfo()
	{
		InstantConnection();
		//NormalConnection();
	}

	public void InstantConnection()
	{
		if (Timer > 0)
		{
			FinishedConnecting = true;

			Domains();

			if (connected == false)
			{
				ConnectionError();
			}
		}
		else
		{
			//Timer -= GameControl.control.Gateway.InstalledModem[0].CurrentSpeed * Time.deltaTime;
			FinishedConnecting = false;
		}
	}

	public void NormalConnection()
	{
		if (Timer <= 0)
		{
			FinishedConnecting = true;

			Domains();

			if (connected == false)
			{
				ConnectionError();
			}
		}
		else
		{
			//Timer -= GameControl.control.Gateway.InstalledModem[0].CurrentSpeed * Time.deltaTime;
			FinishedConnecting = false;
		}
	}

	public void Domains()
	{
		switch (Inputted)
		{
			case "www.becassystems.com":
				SiteName = "Becas";
				ws.UpdateSecCheck = true;
				becas.RenderSite();
				CurrentAccounts = becas.Accounts;
				sm.Connect();
				clic.storedConnection = "www.becassystems.com";
				connected = true;
				break;
			case "www.reva.com/test":
				SiteName = "Reva Test";
				ws.UpdateSecCheck = true;
				revatest.RenderSite();
				CurrentAccounts = revatest.Accounts;
				connected = true;
				clic.storedConnection = "www.reva.com/test";
				sm.Connect();
				break;
			case "www.ping.com":
				ping.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.ping.com";
				connected = true;
				break;
			case "www.unicom.com":
				SiteName = "Unicom";
				ws.UpdateSecCheck = true;
				uc.RenderSite();
				CurrentAccounts = uc.Accounts;
				sm.Connect();
				clic.storedConnection = "www.unicom.com";
				connected = true;
				break;
			case "www.jaildew.com":
				SiteName = "Jaildew";
				ws.UpdateSecCheck = true;
				jd.RenderSite();
				for(int i = 0; i < GameControl.control.CompanyServerData.Count; i++)
				{
					for (int j = 0; j < GameControl.control.CompanyServerData.Count; j++)
					{
						if(GameControl.control.CompanyServerData[i].Name == "Jaildew")
						{
							CurrentAccounts = GameControl.control.CompanyServerData[i].Accounts;
						}
					}
				}
				sm.Connect();
				connected = true;
				clic.storedConnection = "www.jaildew.com";
				break;
			case "www.uplink.com":
				SiteName = "Uplink";
				ws.UpdateSecCheck = true;
				//Uplink.RenderSite();
				for (int i = 0; i < GameControl.control.CompanyServerData.Count; i++)
				{
					for (int j = 0; j < GameControl.control.CompanyServerData.Count; j++)
					{
						if (GameControl.control.CompanyServerData[i].Name == "Jaildew")
						{
							CurrentAccounts = GameControl.control.CompanyServerData[i].Accounts;
						}
					}
				}
				sm.Connect();
				connected = true;
				clic.storedConnection = "www.jaildew.com";
				break;
			//case "www.aki.com":
			//	SiteName = "Aki";
			//	ws.UpdateSecCheck = true;
			//	aki.RenderSite();
			//	//SiteAdminPass = jd.SiteAdminPass;
			//	sm.Connect();
			//	connected = true;
			//	clic.storedConnection = "www.aki.com";
			//	break;
			//		case "www.para.com":
			//			SiteName = "Para";
			//			ws.UpdateSecCheck = true;
			//			para.RenderSite ();
			//			SiteAdminPass = para.ЫшеуФвьштЗфыы;
			//			sm.Connect ();
			//			connected = true;
			//			clic.storedConnection = "www.para.com";
			//			break;
			//		case "www.cabbagecorp.com":
			//			ib.SiteName = "Cabbage Corp";
			//			ws.UpdateSecCheck = true;
			//			cc.RenderSite ();
			//			SiteAdminPass = cc.SiteAdminPass;
			//			sm.Connect ();
			//			break;
			case "www.reva.com":
				SiteName = "Reva";
				ws.UpdateSecCheck = true;
				reva.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.reva.com";
				connected = true;
				break;
			case "www.lecbank.com":
				SiteName = "LEC";
				ws.UpdateSecCheck = true;
				LEC.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.lecbank.com";
				connected = true;
				break;
			case "www.games.com":
				mgw.RenderSite();
				mgw.showMenu = true;
				sm.Connect();
				clic.storedConnection = "www.game.com";
				connected = true;
				break;
			case "www.melvena.com":
				//melvenauni.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.melvena.com";
				connected = true;
				break;
			case "shares":
				st.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.shares.com";
				connected = true;
				break;
			case "servers":
				sh.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.servers.com";
				connected = true;
				break;
			case "www.tugs.com":
				tug.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.tugs.com";
				connected = true;
				break;
			case "www.isd.com":
				//isd.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.isd.com";
				connected = true;
				break;
			case "drag":
				dr.GameRender();
				sm.Connect();
				clic.storedConnection = "www.drag.com";
				connected = true;
				break;
			case "www.stock.com":
				st.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.stock.com";
				connected = true;
				break;
			case "test1":
				test1.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.test1.com";
				connected = true;
				break;
			case "test2":
				hs.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.test2.com";
				connected = true;
				break;
			case "www.gstocks.com":
				gstocks.RenderSite();
				sm.Connect();
				clic.storedConnection = "www.gstocks.com";
				connected = true;
				break;
			case "www.store.com":
				store.RenderSite();
				clic.storedConnection = "www.store.com";
				connected = true;
				break;
		}
	}
}
