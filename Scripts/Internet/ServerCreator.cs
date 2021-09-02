using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCreator : MonoBehaviour 
{
	public List<ProgramSystem> PingWebPages = new List<ProgramSystem>();

	public List<RemoteFileSystem> JaildewWebPages = new List<RemoteFileSystem>();
	public List<ProgramSystem> JaildewFiles = new List<ProgramSystem>();


	public List<RemoteFileSystem> BecasWebPages = new List<RemoteFileSystem>();
	public List<ProgramSystem> BecasFiles = new List<ProgramSystem>();

	public List<RemoteFileSystem> UnicomWebPages = new List<RemoteFileSystem>();
	public List<ProgramSystem> UnicomFiles = new List<ProgramSystem>();

	public List<RemoteFileSystem> RevaTestWebPages = new List<RemoteFileSystem>();
	public List<ProgramSystem> RevaTestFiles = new List<ProgramSystem>();

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

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("Ping", "123.456.789", null, PingWebPages, null, null, CompanyServerSystem.ServerType.FileServer));
	}

	void AddJaildewServer()
	{
		//Public
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/public", "www.jaildew.com/home", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/tempfiles", "www.jaildew.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Public", "", "", "", "www.jaildew.com/home", "www.jaildew.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Sign in", "", "", "", "www.jaildew.com/home", "www.jaildew.com/login", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
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

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("Jaildew", "123.456.789", JaildewWebPages, JaildewFiles, null, null, CompanyServerSystem.ServerType.FileServer));
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

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("Becas", "123.456.789", BecasWebPages, BecasFiles, null, null, CompanyServerSystem.ServerType.FileServer));
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

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("Unicom", "123.456.789", UnicomWebPages, UnicomFiles, null, null, CompanyServerSystem.ServerType.FileServer));
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

		GameControl.control.CompanyServerData.Add(new CompanyServerSystem("RevaTest", "123.456.789", RevaTestWebPages, RevaTestFiles, null, null, CompanyServerSystem.ServerType.FileServer));
	}


	void AddServers()
	{
		AddPingServer();
		AddJaildewServer();
		AddBecasServer();
		AddUnicomServer();
		AddRevaTestServer();
	}
}
