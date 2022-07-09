using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCreator : MonoBehaviour 
{
	public List<ProgramSystem> PingWebPages = new List<ProgramSystem>();

	public List<RemoteFileSystem> JaildewWebPages = new List<RemoteFileSystem>();
	public List<ProgramSystem> JaildewFiles = new List<ProgramSystem>();
	public List<UACSystem> JaildewAccounts = new List<UACSystem>();


	public List<RemoteFileSystem> BecasWebPages = new List<RemoteFileSystem>();
	public List<ProgramSystem> BecasFiles = new List<ProgramSystem>();
	public List<UACSystem> BecasAccounts = new List<UACSystem>();

	public List<RemoteFileSystem> UnicomWebPages = new List<RemoteFileSystem>();
	public List<ProgramSystem> UnicomFiles = new List<ProgramSystem>();
	public List<UACSystem> UnicomAccounts = new List<UACSystem>();

	public List<RemoteFileSystem> RevaTestWebPages = new List<RemoteFileSystem>();
	public List<ProgramSystem> RevaTestFiles = new List<ProgramSystem>();
	public List<UACSystem> RevaTestAccounts = new List<UACSystem>();

	//public List<RemoteFileSystem> RevaTestWebPages = new List<RemoteFileSystem>();
	//public List<ProgramSystem> RevaTestFiles = new List<ProgramSystem>();
	public List<UACSystem> GStocksAccounts = new List<UACSystem>();
	public StockTradeSys GStocks;
	public List<StockInfoSys> GStocksStocks = new List<StockInfoSys>();
	public List<UACStockSystem> GStockUAC = new List<UACStockSystem>();

	public List<RemoteFileSystem> LECBankPages = new List<RemoteFileSystem>();
	public List<BankAccountsSystem> LECBankDetails = new List<BankAccountsSystem>();

	// Use this for initialization
	void Start ()
	{
		if(GameControl.control.CompanyServerData.Count == 0)
		{
			AddServers();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void AddPingServer()
	{
		string FileLocation = "www.ping.com";
		PingWebPages.Add(new ProgramSystem("Jaildew Corp", "", "", "", "", "", FileLocation, "www.jaildew.com", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
		PingWebPages.Add(new ProgramSystem("Becas Systems", "", "", "", "", "", FileLocation, "www.becassystems.com", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
		PingWebPages.Add(new ProgramSystem("TUG", "", "", "", "", "", FileLocation, "www.tugs.com", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
		PingWebPages.Add(new ProgramSystem("Store", "", "", "", "", "", FileLocation, "www.store.com", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
		PingWebPages.Add(new ProgramSystem("International Social Database", "", "", "", "", "", FileLocation, "www.isd.com", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
		PingWebPages.Add(new ProgramSystem("Melvena", "", "", "", "", "", FileLocation, "www.melvena.com", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
		PingWebPages.Add(new ProgramSystem("Unicom LTD", "", "", "", "", "", FileLocation, "www.unicom.com", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
		PingWebPages.Add(new ProgramSystem("LEC Bank", "", "", "", "", "", FileLocation, "www.lecbank.com", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("Ping", "123.456.789", null, PingWebPages, null, null, CompanyServerSystem.ServerType.FileServer, null, null,null));
	}

	void AddJaildewServer()
	{
		//Public
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/public", "www.jaildew.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/tempfiles", "www.jaildew.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Public", "", "", "", "www.jaildew.com", "www.jaildew.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Sign in", "", "", "", "www.jaildew.com", "www.jaildew.com/login", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Temp Files", "", "", "", "www.jaildew.com/public", "www.jaildew.com/tempfiles", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Internal/Private
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/internal/files", "www.jaildew.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Internal Files", "", "", "", "www.jaildew.com/internal", "www.jaildew.com/internal/files", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Documents", "", "", "", "www.jaildew.com/internal", "www.jaildew.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Logs", "", "", "", "www.jaildew.com/internal", "www.jaildew.com/internal/logs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Signout", "", "", "", "www.jaildew.com/internal", "www.jaildew.com/signout", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Private Documents
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/internal/docs", "www.jaildew.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/internal/docs/emails", "www.jaildew.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/internal/docs/notes", "www.jaildew.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Emails", "", "", "", "www.jaildew.com/internal/docs", "www.jaildew.com/internal_docs/emails", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Notes", "", "", "", "www.jaildew.com/internal/docs", "www.jaildew.com/internal_docs/notes", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Private Logs
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/internal/logs", "www.jaildew.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("Jaildew", "123.456.789", JaildewWebPages, JaildewFiles, null, null, CompanyServerSystem.ServerType.FileServer,JaildewAccounts,null,null));
	}

	void AddBecasServer()
	{
		//Public
		BecasWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.becassystems.com/public", "www.becassystems.com/home", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.becassystems.com/tempfiles", "www.becassystems.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Public", "", "", "", "www.becassystems.com/home", "www.becassystems.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Sign in", "", "", "", "www.becassystems.com/home", "www.becassystems.com/login", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Temp Files", "", "", "", "www.becassystems.com/public", "www.becassystems.com/tempfiles", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Internal/Private
		BecasWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.becassystems.com/internal/files", "www.becassystems.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Internal Files", "", "", "", "www.becassystems.com/internal", "www.becassystems.com/internal/files", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Documents", "", "", "", "www.becassystems.com/internal", "www.becassystems.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Logs", "", "", "", "www.becassystems.com/internal", "www.becassystems.com/internal/logs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Signout", "", "", "", "www.becassystems.com/internal", "www.becassystems.com/signout", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Private Documents
		BecasWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.becassystems.com/internal/docs", "www.becassystems.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.becassystems.com/internal/docs/emails", "www.becassystems.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.becassystems.com/internal/docs/notes", "www.becassystems.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Emails", "", "", "", "www.becassystems.com/internal/docs", "www.becassystems.com/internal_docs/emails", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		BecasWebPages.Add(new RemoteFileSystem("Notes", "", "", "", "www.becassystems.com/internal/docs", "www.becassystems.com/internal_docs/notes", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Private Logs
		BecasWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.becassystems.com/internal/logs", "www.becassystems.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("Becas", "123.456.789", BecasWebPages, BecasFiles, null, null, CompanyServerSystem.ServerType.FileServer,BecasAccounts, null, null));
	}

	void AddUnicomServer()
	{
		//Public
		UnicomWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.unicom.com/public", "www.unicom.com/home", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.unicom.com/tempfiles", "www.unicom.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Public", "", "", "", "www.unicom.com/home", "www.unicom.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Sign in", "", "", "", "www.unicom.com/home", "www.unicom.com/login", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Temp Files", "", "", "", "www.unicom.com/public", "www.unicom.com/tempfiles", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Internal/Private
		UnicomWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.unicom.com/internal/files", "www.unicom.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Internal Files", "", "", "", "www.unicom.com/internal", "www.unicom.com/internal/files", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Documents", "", "", "", "www.unicom.com/internal", "www.unicom.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Logs", "", "", "", "www.unicom.com/internal", "www.unicom.com/internal/logs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Signout", "", "", "", "www.unicom.com/internal", "www.unicom.com/signout", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Private Documents
		UnicomWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.unicom.com/internal/docs", "www.unicom.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.unicom.com/internal/docs/emails", "www.unicom.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.unicom.com/internal/docs/notes", "www.unicom.com/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Emails", "", "", "", "www.unicom.com/internal/docs", "www.unicom.com/internal_docs/emails", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		UnicomWebPages.Add(new RemoteFileSystem("Notes", "", "", "", "www.unicom.com/internal/docs", "www.unicom.com/internal_docs/notes", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Private Logs
		UnicomWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.unicom.com/internal/logs", "www.unicom.com/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("Unicom", "123.456.789", UnicomWebPages, UnicomFiles, null, null, CompanyServerSystem.ServerType.FileServer,UnicomAccounts, null, null));
	}

	void AddRevaTestServer()
	{
		//Public
		RevaTestWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.reva.com/test/public", "www.reva.com/test/home", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.reva.com/test/tempfiles", "www.reva.com/test/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Public", "", "", "", "www.reva.com/test/home", "www.reva.com/test/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Sign in", "", "", "", "www.reva.com/test/home", "www.reva.com/test/login", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Temp Files", "", "", "", "www.reva.com/test/public", "www.reva.com/test/tempfiles", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Internal/Private
		RevaTestWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.reva.com/test/internal/files", "www.reva.com/test/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Internal Files", "", "", "", "www.reva.com/test/internal", "www.reva.com/test/internal/files", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Documents", "", "", "", "www.reva.com/test/internal", "www.reva.com/test/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Logs", "", "", "", "www.reva.com/test/internal", "www.reva.com/test/internal/logs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Signout", "", "", "", "www.reva.com/test/internal", "www.reva.com/test/signout", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Private Documents
		RevaTestWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.reva.com/test/internal/docs", "www.reva.com/test/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.reva.com/test/internal/docs/emails", "www.reva.com/test/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.reva.com/test/internal/docs/notes", "www.reva.com/test/internal/docs", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Emails", "", "", "", "www.reva.com/test/internal/docs", "www.reva.com/test/internal_docs/emails", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		RevaTestWebPages.Add(new RemoteFileSystem("Notes", "", "", "", "www.reva.com/test/internal/docs", "www.reva.com/test/internal_docs/notes", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		//Private Logs
		RevaTestWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.reva.com/test/internal/logs", "www.reva.com/test/internal", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("RevaTest", "123.456.789", RevaTestWebPages, RevaTestFiles, null, null, CompanyServerSystem.ServerType.FileServer,RevaTestAccounts, null, null));
	}

	void AddGStocks()
	{
		GStocks.ExchangeName = "Memes Exchange";

		GStocksStocks.Add(new StockInfoSys("Dat Boii Pepe", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Nyan Cat", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Trolol", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("MLG", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Vine", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Star Wars Kid", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Clippy", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Cheeki Breeki", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Remove Kebab", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Air Horn Remixes", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Harambe", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("DANK", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("EMOJI", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Doge", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Cats", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Leave Birtany Alone", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Ennuya Guy", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Hey Hey Hey", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Overwatch Memes", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));
		GStocksStocks.Add(new StockInfoSys("Lemme Smash", GStocks.ExchangeName, "", "", "", "", "", Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), Random.Range(1, 300), 0, 0));

		GStocks = new StockTradeSys(GStocks.ExchangeName, GStockUAC, GStocksStocks);







		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("GStocks", "123.456.789", null, null, null, null, CompanyServerSystem.ServerType.StockServer, GStocksAccounts, null, GStocks));
	}

	void AddLECBank()
	{
		//Home Page
		LECBankPages.Add(new RemoteFileSystem("Sign in", "", "", "", "www.lecbank.com", "www.lecbank.com/accountlogin", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		LECBankPages.Add(new RemoteFileSystem("Create Account", "", "", "", "www.lecbank.com", "www.lecbank.com/createaccount", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		LECBankPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.lecbank.com/accountlogin", "www.lecbank.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		LECBankPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.lecbank.com/createaccount", "www.lecbank.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));

		//Account Page
		LECBankPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.lecbank.com/accountinfo", "www.lecbank.com/account", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		LECBankPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.lecbank.com/transfer", "www.lecbank.com/account", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		LECBankPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.lecbank.com/loans", "www.lecbank.com/account", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		LECBankPages.Add(new RemoteFileSystem("Account Info", "", "", "", "www.lecbank.com/account", "www.lecbank.com/accountinfo", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		LECBankPages.Add(new RemoteFileSystem("Transfer", "", "", "", "www.lecbank.com/account", "www.lecbank.com/transfer", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		LECBankPages.Add(new RemoteFileSystem("Loans", "", "", "", "www.lecbank.com/account", "www.lecbank.com/loans", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("LEC Bank", "123.456.789", LECBankPages, null, null, null, CompanyServerSystem.ServerType.BankServer, null, LECBankDetails, null));
	}


	void AddServers()
	{
		AddPingServer();
		AddJaildewServer();
		AddBecasServer();
		AddUnicomServer();
		AddRevaTestServer();
		AddGStocks();
		AddLECBank();
	}
}
