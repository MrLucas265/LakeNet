using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour 
{
//	public int StartCount;
//	public List<string> EmailSubject = new List<string>();
//	public List<string> NoteTitle = new List<string>();
//
//	const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//	const string AccNo = "1234567890";
//
//	public bool logged;
//	public bool showMenu;
//
//	public int Select;
//
//	public string UsrName;
//	public string password;
//	public string SiteAdminPass;
//
//	private GameObject Computer;
//	private GameObject Prompts;
//	private GameObject Applications;
//	private GameObject Hacking;
//	private GameObject System;
//
//	private InternetBrowser ib;
//	private Computer com;
//	private ErrorProm ep;
//	private Tracer trace;
//	private SystemMap sm;
//	private TextReader tr;
//	private Progtive prog;
//	private Defalt def;
//
//	public Vector2 scrollpos = Vector2.zero;
//	public int scrollsize;
//
//	public float SecLevel;
//
//	public int MonitorLevel;
//	public bool Monitor;
//
//	public int ProxyLevel;
//	public bool Proxy;
//
//	public int FirewallLevel;
//	public bool Firewall;
//
//	void Start()
//	{
//		Computer = GameObject.Find("Computer");
//		Prompts = GameObject.Find("Prompts");
//		Applications = GameObject.Find("Applications");
//		Hacking = GameObject.Find("Hacking");
//		System = GameObject.Find("System");
//		StartCount = 100;
//		fileGenPrivate ();
//		fileGenPublic ();
//		Documents();
//		WebSearch();
//		SecuirtySystems();
//	}
//
//	void SecuirtySystems()
//	{
//		SecLevel = 1;
//		Proxy = false;
//		Firewall = false;
//		Monitor = false;
//	}
//
//	void WebSearch()
//	{
//		// APPLICATIONS
//		sm = Applications.GetComponent<SystemMap>();
//		tr = Applications.GetComponent<TextReader>();
//		ib = Applications.GetComponent<InternetBrowser>();
//		// PROMPTS
//		ep = Prompts.GetComponent<ErrorProm>();
//		// HACKING
//		trace = Hacking.GetComponent<Tracer>();
//		prog = Hacking.GetComponent<Progtive>();
//		// SYSTEM
//		com = System.GetComponent<Computer>();
//		def = System.GetComponent<Defalt>();
//
//
//	}
//
//	public string GetRandomString(int min, int max)
//	{
//		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
//		string retMe = "";
//		for(int i=0; i<charAmount; i++)
//		{
//			retMe += glyphs[Random.Range(0, glyphs.Length)];
//		}
//		return retMe;
//	}
//
//	public string GetRandomNumber(int min, int max)
//	{
//		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
//		string retMe = "";
//		for(int i=0; i<charAmount; i++)
//		{
//			retMe += AccNo[Random.Range(0, AccNo.Length)];
//		}
//		return retMe;
//	}
//
//	void Update()
//	{
//		if (SiteAdminPass == "") 
//		{
//			SiteAdminPass = GetRandomString (8, 8);
//		}
//
//		if (GameControl.control.TestFileName.Count < StartCount || GameControl.control.TestFileSize.Count < StartCount) 
//		{
//			fileGenPrivate ();
//		}
//		if (GameControl.control.TestPublicFileName.Count < StartCount || GameControl.control.TestPublicFileSize.Count < StartCount) 
//		{
//			fileGenPublic ();
//		}
//	}
//
//	public void fileGenPublic()
//	{
//		for(int i=0; i<1; i++)
//		{
//			GameControl.control.TestPublicFileName.Add(GetRandomString(4,4));
//			GameControl.control.TestPublicFileName.Sort();
//			GameControl.control.TestPublicFileSize.Add (Random.Range (1, 100));
//		}
//	}
//
//	public void fileGenPrivate()
//	{
//		for(int i=0; i<1; i++)
//		{
//			GameControl.control.TestFileName.Add(GetRandomString(4,4));
//			GameControl.control.TestFileName.Sort();
//			GameControl.control.TestFileSize.Add (Random.Range (1, 1000));
//		}
//	}
//
//	public void Documents()
//	{
//		EmailSubject.Add("Secuirty Loophole");
//		EmailSubject.Add("Test 1");
//		EmailSubject.Add("Test 2");
//		NoteTitle.Add("Important Note");
//	}
//
//	public void Logs()
//	{
//
//	}
//
//	public void RenderSite()
//	{
//		switch(ib.AddressBar)
//		{
//		case "www.revatest.com": 
//			if(GUI.Button(new Rect(10,75,100,20),"Public"))
//			{
//				ib.AddressBar = "www.revatest.com/public";
//				ib.showAddressBar = false;
//			}  
//			if(GUI.Button(new Rect(10,120,100,20),"Sign in"))
//			{
//				ib.AddressBar = "www.revatest.com/login";
//				ib.showAddressBar = false;
//			}    
//			if(!GameControl.control.Sites.Contains("www.revatest.com"))
//			{
//				GameControl.control.Sites.Add("www.revatest.com");
//			}
//			break;
//
//		case "www.revatest.com/public": 
//			if(GUI.Button(new Rect(10,75,100,20),"Home"))
//			{
//				trace.UpdateTimer = false;
//				ib.showAddressBar = true;
//				logged = false;
//				UsrName = "";
//				password = "";
//				sm.BounceIPs.Remove(sm.RevaTestIP);
//				sm.BouncedConnections.Remove(sm.RevaTestPos);
//				ib.AddressBar = "www.revatest.com";
//			}    
//
//			if(GUI.Button(new Rect(10,100,100,20),"files"))
//			{
//				ib.AddressBar = "www.revatest.com/publicfilesystem";
//			}    
//
//			if(!GameControl.control.Sites.Contains("www.revatest.com"))
//			{
//				GameControl.control.Sites.Add("www.revatest.com");
//			}
//			break;
//
//		case "www.revatest.com/publicfilesystem": 
//			if (GUI.Button(new Rect(245,30,50,20), "Back"))
//			{
//				ib.AddressBar = "www.revatest.com/public";
//			} 
//			if(!GameControl.control.Sites.Contains("www.revatest.com"))
//			{
//				GameControl.control.Sites.Add("www.revatest.com");
//			}
//
//			GUI.Label(new Rect(115, 50, 500, 500), "File Name");
//			GUI.Label(new Rect(200, 50, 500, 500), "Size");
//
//			if(showMenu == true)
//			{
//				if(GUI.Button(new Rect(10, 105, 100, 20), "Delete " + GameControl.control.TestPublicFileName[Select]))
//				{
//					com.Copy = false;
//					def.showdf = true;
//					com.Delete = true;
//				}
//				if(GUI.Button(new Rect(10,145,100,20),"Copy " + GameControl.control.TestPublicFileName[Select]))
//				{
//					com.Copy = true;
//					def.showcf = true;
//					com.Delete = false;
//				}
//			}
//			scrollpos = GUI.BeginScrollView(new Rect(130, 75, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
//			for (scrollsize = 0; scrollsize < StartCount; scrollsize++)
//			{
//				if (GUI.Button(new Rect(3, scrollsize * 20, 80, 20), "" + GameControl.control.TestPublicFileName[scrollsize]))
//				{
//					showMenu = true;
//					Select = scrollsize;
//				}
//				GUI.Button(new Rect(85, scrollsize * 20, 40, 20), "" + GameControl.control.TestPublicFileSize[scrollsize]);
//			}
//			GUI.EndScrollView();
//			break;
//
//		case "www.revatest.com/filesystem": 
//			if (logged == true)
//			{
//				if(!GameControl.control.Sites.Contains("www.revatest.com"))
//				{
//					GameControl.control.Sites.Add("www.revatest.com");
//				}
//				if (GUI.Button(new Rect(5, 55, 100, 20), "Back"))
//				{
//					ib.AddressBar = "www.revatest.com/internal";
//				}
//
//				GUI.Label(new Rect(115, 50, 500, 500), "File Name");
//				GUI.Label(new Rect(200, 50, 500, 500), "Size");
//
//				if(showMenu == true)
//				{
//					if(GUI.Button(new Rect(10, 105, 100, 20), "Delete " + GameControl.control.TestFileName[Select]))
//					{
//						com.Copy = false;
//						def.showdf = true;
//						com.Delete = true;
//					}
//					if(GUI.Button(new Rect(10,145,100,20),"Copy " + GameControl.control.TestFileName[Select]))
//					{
//						com.Copy = true;
//						def.showcf = true;
//						com.Delete = false;
//					}
//				}
//				scrollpos = GUI.BeginScrollView(new Rect(130, 75, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
//				for (scrollsize = 0; scrollsize < StartCount; scrollsize++)
//				{
//					if (GUI.Button(new Rect(3, scrollsize * 20, 80, 20), "" + GameControl.control.TestFileName[scrollsize]))
//					{
//						showMenu = true;
//						Select = scrollsize;
//					}
//					GUI.Button(new Rect(85, scrollsize * 20, 40, 20), "" + GameControl.control.TestFileSize[scrollsize]);
//				}
//				GUI.EndScrollView();
//			}
//			break;
//
//		case "www.revatest.com/login":
//
//			if (UsrName == "Admin") 
//			{
//				ib.Username = UsrName;
//			}
//
//			UsrName = GUI.TextField(new Rect(85, 55, 120, 20), UsrName, 500);
//			password = GUI.TextField(new Rect(85, 75, 120, 20), password, 500);
//
//			GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
//			GUI.Label(new Rect(3, 75, 500, 500), "Password: ");
//			ib.showAddressBar = false;
//
//			if(prog.Running == false)
//			{
//				if (GUI.Button(new Rect(245,30,50,20), "Back"))
//				{
//					trace.UpdateTimer = false;
//					ib.showAddressBar = true;
//					logged = false;
//					UsrName = "";
//					password = "";
//					sm.BounceIPs.Remove(sm.RevaTestIP);
//					sm.BouncedConnections.Remove(sm.RevaTestPos);
//					ib.AddressBar = "www.revatest.com";
//				} 
//			}
//
//			if(UsrName == "Admin" && password == SiteAdminPass)
//			{
//				if(GUI.Button(new Rect(10,125,100,20),"Login"))
//				{
//					ib.showAddressBar = false;
//					logged = true;
//					ib.AddressBar = "www.revatest.com/internal";
//					trace.UpdateTimer = true;
//					//log.log.Add(GameControl.control.fullip);
//					GameControl.control.Sites.Add("www.revatest.com/login");
//				}
//			}
//			break;
//
//		case "www.revatest.com/documents/emails":
//			if(logged == true)
//			{
//				scrollpos = GUI.BeginScrollView(new Rect(115, 75, 125, 100), scrollpos, new Rect(0, 0, 0, scrollsize*20));
//				for (scrollsize = 0; scrollsize < EmailSubject.Count; scrollsize++)
//				{
//					if(GUI.Button(new Rect(3, scrollsize * 20, 120, 20), "" + EmailSubject[scrollsize]))
//					{
//						tr.show = true;
//						tr.Title = EmailSubject[scrollsize];
//					}
//				}
//				GUI.EndScrollView();
//
//				if(GUI.Button(new Rect(245,30,50,20),"Back"))
//				{
//					ib.AddressBar = "www.revatest.com/internal";
//				}
//			}
//			break;
//
//		case "www.revatest.com/documents":
//			if(logged == true)
//			{
//				if(!GameControl.control.Sites.Contains("www.revatest.com"))
//				{
//					GameControl.control.Sites.Add("www.revatest.com");
//				}
//				if(GUI.Button(new Rect(10,75,100,20),"Emails"))
//				{
//					ib.AddressBar = "www.revatest.com/documents/emails";
//				}
//				if(GUI.Button(new Rect(10,100,100,20),"Notes"))
//				{
//					ib.AddressBar = "www.jaildew.com/documents/notes";
//				}
//				if(GUI.Button(new Rect(10,150,100,20),"Back"))
//				{
//					ib.AddressBar = "www.revatest.com/internal";
//				}
//			}
//			break;
//
//		case "www.revatest.com/internal":
//			if(logged == true)
//			{
//				if(!GameControl.control.Sites.Contains("www.revatest.com"))
//				{
//					GameControl.control.Sites.Add("www.revatest.com");
//				}
//				if(GUI.Button(new Rect(10,75,100,20),"File System"))
//				{
//					ib.AddressBar = "www.revatest.com/filesystem";
//				}
//				if(GUI.Button(new Rect(10,100,100,20),"Documents"))
//				{
//					ib.AddressBar = "www.revatest.com/documents";
//				}
//				if(GUI.Button(new Rect(10,125,100,20),"Logs"))
//				{
//					ib.AddressBar = "www.jaildew.com/logs";
//				}
//				if(GUI.Button(new Rect(10,150,100,20),"Sign Out"))
//				{
//					trace.stopping = true;
//					ib.Username = "";
//					ib.showAddressBar = true;
//					logged = false;
//					UsrName = "";
//					password = "";
//					ib.AddressBar = "www.revatest.com/login";
//					SiteAdminPass = GetRandomString (8, 8);
//				}
//			}
//			break;
//		}
//	}
}