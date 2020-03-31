using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCreator : MonoBehaviour 
{
	public List<RemoteFileSystem> PingWebPages = new List<RemoteFileSystem>();
	public List<RemoteFileSystem> JaildewWebPages = new List<RemoteFileSystem>();
	public List<RemoteFileSystem> JaildewFiles = new List<RemoteFileSystem>();
	// Use this for initialization
	void Start ()
	{
		if(GameControl.control.CompanyServers.Count == 0)
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
		PingWebPages.Add(new RemoteFileSystem("Jaildew Corp", "", "", "", FileLocation, "www.jaildew.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.File));
		PingWebPages.Add(new RemoteFileSystem("Becas Systems", "", "", "", FileLocation, "www.becassystems.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.File));
		PingWebPages.Add(new RemoteFileSystem("TUG", "", "", "", FileLocation, "www.tugs.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.File));
		PingWebPages.Add(new RemoteFileSystem("Store", "", "", "", FileLocation, "www.store.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.File));
		PingWebPages.Add(new RemoteFileSystem("International Social Database", "", "", "", FileLocation, "www.isd.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.File));
		PingWebPages.Add(new RemoteFileSystem("Melvena", "", "", "", FileLocation, "www.melvena.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.File));
		PingWebPages.Add(new RemoteFileSystem("Unicom LTD", "", "", "", FileLocation, "www.unicom.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.File));
		PingWebPages.Add(new RemoteFileSystem("LEC Bank", "", "", "", FileLocation, "www.lecbank.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.File));
		GameControl.control.CompanyServers.Add(new CompanyServerSystem("Ping", "123.456.789", null, PingWebPages, null, null, CompanyServerSystem.ServerType.FileServer));
	}

	void AddJaildewServer()
	{
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/public", "www.jaildew.com", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("< Back", "", "", "", "www.jaildew.com/tempfiles", "www.jaildew.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Public", "", "", "", "www.jaildew.com", "www.jaildew.com/public", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Sign in", "", "", "", "www.jaildew.com", "www.jaildew.com/login", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));
		JaildewWebPages.Add(new RemoteFileSystem("Temp Files", "", "", "", "www.jaildew.com/public", "www.jaildew.com/tempfiles", 0, 0, 0, 0, 0, 0, false, RemoteFileSystem.ProgramType.Fdl));

		GameControl.control.CompanyServers.Add(new CompanyServerSystem("Jaildew", "123.456.789", JaildewWebPages, null, null, null, CompanyServerSystem.ServerType.FileServer));
	}


	void AddServers()
	{
		AddPingServer();
		AddJaildewServer();
	}
}
