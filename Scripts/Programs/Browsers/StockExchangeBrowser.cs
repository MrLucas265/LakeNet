using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockExchangeBrowser : MonoBehaviour
{
	private GameObject SysSoftware;
	private GameObject AppSoftware;

	//public bool show;
	public bool showAddressBar;

	public string AddressBar;
	public string Inputted;

	private GameObject Database;

	public bool Search;


	public string SiteAdminPass;
	public string Username;

	public WebSecSystem ConntectedWebSec;

	// collection of scripts
	private WebSec ws;
	private SystemMap sm;
	//	private Para para;
	private CLICommandsV2 clic;

	//STOCK EXCHANGES
	private StockExchange stockexchange;
	private ShareTrades st;
	private GStocks gstocks;
	private Portfolio port;
    private StockTransactionHistory history;

    public string SiteName;

	public List<WebSecSystem> CurrentSecurity = new List<WebSecSystem>();
	public List<UACSystem> CurrentAccounts = new List<UACSystem>();

	public List<string> DirContents = new List<string>();
	public string CurrentDir;
	public bool Request;

	public bool connected;

	public bool MainPage;

	public string CurrentLocation;

	public List<string> TempHistory = new List<string>();
	public int SelectedPage;

	public string ErrorCode;
	public string ErrorDesc;
	public string ErrorSoloution;

	public void ClearDirContents()
	{
		DirContents.RemoveRange(0,DirContents.Count);
	}

	void Start()
	{
		SysSoftware = GameObject.Find("System");
		AppSoftware = GameObject.Find("Applications");
		Database = GameObject.Find("Database");

		clic = SysSoftware.GetComponent<CLICommandsV2>();


		ws = AppSoftware.GetComponent<WebSec>();
		sm = AppSoftware.GetComponent<SystemMap>();

		//		para = Database.GetComponent<Para>();
		st = Database.GetComponent<ShareTrades>();
		gstocks = Database.GetComponent<GStocks>();
		stockexchange = Database.GetComponent<StockExchange>();
		port = Database.GetComponent<Portfolio>();
        history = Database.GetComponent<StockTransactionHistory>();
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

	public void SiteConnection()
	{
		if (connected == false)
		{
			ConnectionError();
		}
		SiteConnected();
	}

	public void SiteConnected()
	{
		Inputted = AddressBar;
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
		AddressBar = "www.stockexchange.com";
		Inputted = "www.stockexchange.com";
		AddHistory();
	}

	public void WebSiteInfo()
	{
		switch(Inputted)
		{

		case "www.stockexchange.com":
			stockexchange.RenderSite ();
			sm.Connect ();
			clic.storedConnection = "www.stockexchange.com";
			connected = true;
			MainPage = true;
			break;

		case "www.stockexchange.com/properexchange":
			st.RenderSite ();
			sm.Connect ();
			clic.storedConnection = "www.stockexchange.com/properexchange";
			connected = true;
			MainPage = false;
			break;

		case "www.stockexchange.com/memeexchange":
			gstocks.RenderSite ();
			sm.Connect ();
			clic.storedConnection = "www.stockexchange.com/memeexchange";
			connected = true;
			MainPage = false;
			break;

        case "www.stockexchange.com/history":
            history.RenderSite();
            sm.Connect();
            clic.storedConnection = "www.stockexchange.com/history";
            connected = true;
            MainPage = false;
            break;

        case "www.stockexchange.com/portfolio":
			port.RenderSite ();
			sm.Connect ();
			clic.storedConnection = "www.stockexchange.com/portfolio";
			connected = true;
			MainPage = false;
			break;
		}
	}

}
