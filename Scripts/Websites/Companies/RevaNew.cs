using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevaNew : MonoBehaviour
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
	public List<ProgramSystem> ListOfSoftware = new List<ProgramSystem>();
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
	public CHMSystem ProgramInfo;
	public int ProgramID;
	public bool Buying;

	public string Username;
	public string Password;
	public bool ShowPassword;

	public float x;
	public float y;
	public float width;
	public float height;

	private FileUtility fu;

	void Start()
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

	// Update is called once per frame
	void Update()
	{

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

	}

	void UpdateProgramList()
	{
		//ListOfSoftware.Add("Password Breaker");
		//ListOfSoftware.Add("Dictionary Cracker");
		//ListOfSoftware.Add("Trace Tracker");
		//ListOfSoftware.Add("System Map");
		//ListOfSoftware.Add("WebSec Viewer");
		//ListOfSoftware.Add("Monitor Bypass");
		//ListOfSoftware.Add("DirSearch");

	//	ListOfSoftware.Add(new ProgramSystem("Password Breaker", "Reva", "", "exe", "www.reva.com/software", "Password Cracker", 0, 0, 10, 0, 100, 0.1f, false, ProgramSystem.ProgramType.Ins));
	//	ListOfSoftware.Add(new ProgramSystem("Dictionary Cracker", "Reva", "", "exe", "www.reva.com/software", "Dictionary Cracker", 0, 0, 10, 0, 100, 0.1f, false, ProgramSystem.ProgramType.Ins));
	}

	void UpdateUI()
	{
		switch (ProgramName)
		{
			case "Password Breaker":
				ProgramID = 2;
				MaxProgramVersion = 10;
				Price = 1500 * SelectedVersion;
				Size = 10;
				Desc = "The password breaker is slower and more expensive then the dircrk but eventually will match the password.";
				//Version = GameControl.control.SoftwareVersion[ProgramID];
				ProgramTarget = "Password Cracker";
				SystemProgramName = "Password_Cracker";
				ProgramType = "Exe";
				break;
			case "Trace Tracker":
				ProgramID = 3;
				MaxProgramVersion = 1;
				Price = 750 * SelectedVersion;
				Size = 10;
				Desc = "A software kit that detects active direct traces";
				//Version = GameControl.control.SoftwareVersion[ProgramID];
				ProgramTarget = "Trace Tracker";
				SystemProgramName = "Trace_Tracker";
				ProgramType = "Exe";
				break;
			case "System Map":
				ProgramID = 4;
				MaxProgramVersion = 1;
				Price = 1000 * SelectedVersion;
				Size = 5;
				Desc = "A map that allows bouncing off proxys";
				ProgramTarget = "System Map";
				SystemProgramName = "";
				SystemProgramName = "System_Map";
				Version = 1;
				ProgramType = "Exe";

				break;
			case "Dictionary Cracker":
				ProgramID = 5;
				MaxProgramVersion = 10;
				Price = 500 * SelectedVersion;
				Size = 5;
				Desc = "The directory cracker is cheaper and faster then the password breaker but wont always work.";
				//Version = GameControl.control.SoftwareVersion[ProgramID];
				ProgramTarget = "Dictionary Cracker";
				SystemProgramName = "Dictionary_Cracker";
				ProgramType = "Exe";
				break;
		}
	}

	//void Bought()
	//{
	//	for (int i = 0; i < dskman.DriveLetter.Length; i++)
	//	{
	//		if (Customize.cust.DownloadPath[0] == dskman.DriveLetter[i])
	//		{
	//			if (dskman.FreeSpace[i] >= Size)
	//			{
	//				if (GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance >= Price)
	//				{
	//					GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance -= Price;
	//					if (fu.ProgramHandle.Count <= 0)
	//					{
	//						fu.ProgramHandle.Add(new FileUtilitySystem("Download", ProgramName, Customize.cust.DownloadPath, "", ProgramTarget, "", ProgramType, false, true, true, false, SelectedVersion, 0, 0, 0, 0, 0, 0, 0, Size, 0, 0, 0, FileUtilitySystem.ProgramType.DownloadProgram));
	//						fu.AddWindow();
	//					}
	//					Buying = false;
	//				}
	//			}
	//		}
	//	}
	//}

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
			case "www.reva.com/login":

				UsrName = GUI.TextField(new Rect(85, 55, 120, 21), UsrName, 500);
				if (ShowPassword == true)
				{
					password = GUI.TextField(new Rect(85, 80, 120, 21), password, 500);
					if (GUI.Button(new Rect(210, 80, 23, 21), "(-)"))
					{
						ShowPassword = false;
					}
				}
				else
				{
					password = GUI.PasswordField(new Rect(85, 80, 120, 21), password, "*"[0], 500);
					if (GUI.Button(new Rect(210, 80, 23, 21), "(O)"))
					{
						ShowPassword = true;
					}
				}
				GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
				GUI.Label(new Rect(3, 78, 500, 500), "Password: ");
				int SelectedName = 0;
				if (UsrName == "Admin" && password == SiteAdminPass)
				{
					if (GUI.Button(new Rect(10, 125, 100, 20), "Login"))
					{
						logged = true;

						ib.showAddressBar = false;
						ib.AddressBar = "www.reva.com/internal";
						ib.AddHistory();
					}
				}
				for (int i = 0; i < GameControl.control.StoredLogins.Count; i++)
				{
					if (GameControl.control.StoredLogins[i].Name == "REVA")
					{
						SelectedName = i;
					}
				}
				if (GameControl.control.StoredLogins.Count > 0)
				{
					if (UsrName == GameControl.control.StoredLogins[SelectedName].Username && password == GameControl.control.StoredLogins[SelectedName].Password)
					{
						if (GUI.Button(new Rect(10, 125, 120, 20), "Login"))
						{
							logged = true;

							ib.showAddressBar = false;
							ib.AddressBar = "www.reva.com/internal";
							ib.AddHistory();
						}
					}

					if (GUI.Button(new Rect(10, 150, 120, 20), "Auto Login " + RemeberMe))
					{
						RemeberMe = !RemeberMe;
					}

					if (RemeberMe == true)
					{
						UsrName = GameControl.control.StoredLogins[SelectedName].Username;
						password = GameControl.control.StoredLogins[SelectedName].Password;
					}
				}

				if (UsrName == "Dev" && password == "a")
				{
					if (GUI.Button(new Rect(10, 125, 120, 20), "Login"))
					{
						logged = true;

						ib.showAddressBar = false;
						ib.AddressBar = "www.reva.com/internal";
						ib.AddHistory();
					}
				}

				if (GUI.Button(new Rect(245, 30, 50, 20), "Back"))
				{

					ib.AddressBar = "www.reva.com";
				}
				break;

			case "www.reva.com":

				if (GameControl.control.StoryMis[1] == true)
				{
					if (GUI.Button(new Rect(10, 75, 200, 20), "Login"))
					{
						ib.AddressBar = "www.reva.com/login";
						ib.AddHistory();
					}
				}
				else
				{
					GUI.Button(new Rect(10, 75, 200, 20), "You dont have an account");
				}
				break;

			case "www.reva.com/register":

				if (GUI.Button(new Rect(270, 275, 70, 20), "Home"))
				{
					ib.AddressBar = "www.reva.com";
					ib.AddHistory();
				}
				break;

			case "www.reva.com/contracts":

				if (logged == true)
				{
					if (GUI.Button(new Rect(225, 275, 70, 20), "Home"))
					{
						ib.AddressBar = "www.reva.com/internal";
						ib.AddHistory();
					}

					if (brow.Select <= 0)
					{
						brow.Select = 0;
					}

					if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.UpArrow)
					{
						if (brow.Select >= 1)
						{
							scrollpos.y -= 30;
							brow.Select--;
						}

						if (scrollpos.y < brow.Select * 30)
						{
							scrollpos.y = brow.Select * 30;
						}

						if (scrollpos.y > brow.Select * 30)
						{
							scrollpos.y = brow.Select * 30;
						}
					}

					if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.DownArrow)
					{
						if (brow.Select < misgen.MissionTotal - 1)
						{
							scrollpos.y += 30;
							brow.Select++;
						}

						if (scrollpos.y < brow.Select * 30)
						{
							scrollpos.y = brow.Select * 30;
						}

						if (scrollpos.y > brow.Select * 30)
						{
							scrollpos.y = brow.Select * 30;
						}
					}


					if (brow.Select > -1)
					{
						if (misgen.MissionList.Count > 0)
						{
							if (misgen.MissionList[brow.Select].LevelRequirement <= GameControl.control.Rep[0].RepLevel)
							{
								GUI.Label(new Rect(180, 180, 300, 300), "Mission Name: " + misgen.MissionList[brow.Select].Name);
								GUI.Label(new Rect(180, 200, 300, 300), "Mission Reward: " + misgen.MissionList[brow.Select].Cash);
								//GUI.Label (new Rect (180, 220, 300, 300), "Mission Target: " + misgen.MissionList [brow.Select].Target);
								//GUI.Label (new Rect (180, 240, 300, 300), "Mission File: " + misgen.MissionList [brow.Select].File);

								GUI.TextArea(new Rect(180, 35, 300, 150), "" + misgen.MissionList[brow.Select].MDesc);
							}
							else
							{
								GUI.TextArea(new Rect(180, 60, 300, 100), "Your level is too low to accept this mission currently.");
							}
						}
					}

					if (GameControl.control.Contracts.Count <= 12 && brow.Select > -1 && misgen.MissionList[brow.Select].LevelRequirement <= GameControl.control.Rep[0].RepLevel)
					{
						if (GUI.Button(new Rect(425, 275, 70, 20), "Accept"))
						{
							brow.Accept();
						}

						if (GUI.Button(new Rect(325, 275, 70, 20), "Contact"))
						{
							//brow.Accept();
						}
					}

					if (misgen.MissionList.Count > 0)
					{
						scrollpos = GUI.BeginScrollView(new Rect(3, 35, 170, 240), scrollpos, new Rect(0, 0, 0, scrollsize * 30));
						for (scrollsize = 0; scrollsize < misgen.MissionList.Count; scrollsize++)
						{
							if (brow.Select == scrollsize)
							{
								if (misgen.MissionList[scrollsize].LevelRequirement <= GameControl.control.Rep[0].RepLevel)
								{
									if (GUI.Button(new Rect(0, scrollsize * 30, 150, 29), "☒" + misgen.MissionList[scrollsize].Name))
									{
										brow.Select = scrollsize;
										brow.showAccept = true;
									}
								}
								else
								{
									if (GUI.Button(new Rect(0, scrollsize * 30, 150, 29), "☒" + "Insuffcient Level"))
									{
										brow.Select = scrollsize;
										//brow.showAccept = true;
									}
								}
							}
							else
							{
								if (misgen.MissionList[scrollsize].LevelRequirement <= GameControl.control.Rep[0].RepLevel)
								{
									if (GUI.Button(new Rect(0, scrollsize * 30, 150, 29), "☐" + misgen.MissionList[scrollsize].Name))
									{
										brow.Select = scrollsize;
										//brow.showAccept = true;
									}
								}
								else
								{
									if (GUI.Button(new Rect(0, scrollsize * 30, 150, 29), "☐" + "Insuffcient Level"))
									{
										brow.Select = scrollsize;
										// brow.showAccept = true;
									}
								}
							}
						}
						GUI.EndScrollView();
					}
				}
				break;

			case "www.reva.com/news":

				if (logged == true)
				{
					if (GUI.Button(new Rect(245, 30, 50, 20), "Home"))
					{
						ib.AddressBar = "www.reva.com/internal";
						ib.AddHistory();
					}

				}
				break;

			case "www.reva.com/hardware":

				if (logged == true)
				{
					upg.Display = false;
					upg.Desc = "";
					upg.Cost = 0;
					upg.Selected = "";
					upg.Specs = "";

					if (GUI.Button(new Rect(245, 30, 50, 20), "Home"))
					{
						ib.AddressBar = "www.reva.com/internal";
						ib.AddHistory();
					}

					if (GUI.Button(new Rect(5, 70, 100, 20), "CPU"))
					{
						ib.AddressBar = "www.reva.com/hardware/cpu";
						ib.AddHistory();
					}

					if (GUI.Button(new Rect(5, 100, 100, 20), "Power Supply"))
					{
						ib.AddressBar = "www.reva.com/hardware/power-supply";
						ib.AddHistory();
					}

					if (GUI.Button(new Rect(5, 130, 100, 20), "RAM"))
					{
						ib.AddressBar = "www.reva.com/hardware/ram";
						ib.AddHistory();
					}

					if (GUI.Button(new Rect(5, 160, 100, 20), "Hard Drives/SSD"))
					{
						ib.AddressBar = "www.reva.com/hardware/hdd-ssd";
						ib.AddHistory();
					}

					if (GUI.Button(new Rect(5, 190, 100, 20), "Motherboards"))
					{
						//ib.AddressBar = "www.reva.com/hardware/motherboards";
						ep.playsound = true;
						ep.ErrorTitle = "Error 400 - C# Button Null Refrence";
						ep.ErrorMsg = "The button you clicked is outputting a null value.";
					}

					if (GUI.Button(new Rect(5, 220, 100, 20), "GPU"))
					{
						ib.AddressBar = "www.reva.com/hardware/gpu";
						ib.AddHistory();
					}

					if (GUI.Button(new Rect(5, 250, 100, 20), "Fans"))
					{
						//ib.AddressBar = "www.reva.com/hardware/fan";
						ep.playsound = true;
						ep.ErrorTitle = "Error 400 - C# Button Null Refrence";
						ep.ErrorMsg = "The button you clicked is outputting a null value.";
					}
				}
				break;

			case "www.reva.com/software":
				scrollpos = GUI.BeginScrollView(new Rect(2, 25, 165, 240), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
				for (scrollsize = 0; scrollsize < ListOfSoftware.Count; scrollsize++)
				{
					if (GUI.Button(new Rect(0, scrollsize * 20, 149, 20), "" + ListOfSoftware[scrollsize]))
					{
						Select = scrollsize;
						ProgramName = ListOfSoftware[Select].Name;
						SelectedVersion = 1;
						UpdateUI();
					}
				}
				GUI.EndScrollView();

				if (ProgramName != "")
				{
					GUI.Box((new Rect(168, 25, 330, 240)), "");

					GUI.Label(new Rect(171, 25, 300, 300), "Product Name: " + ListOfSoftware[Select].Name);
					GUI.Label(new Rect(171, 45, 300, 300), "Product Type: " + ListOfSoftware[Select].Type.ToString());
					GUI.Label(new Rect(171, 65, 300, 300), "Product Desc: " + Desc);
					GUI.Label(new Rect(171, 160, 300, 300), "Product Cost: " + Price);
					GUI.Label(new Rect(171, 175, 300, 300), "Product Size: " + Size);
					GUI.Label(new Rect(171, 190, 300, 300), "Product Version: " + SelectedVersion);
					GUI.Label(new Rect(171, 205, 300, 300), "----------------");


					//if (GameControl.control.SoftwareVersion[ProgramID] != 0)
					//{
					//	GUI.Label(new Rect(171, 243, 500, 500), "Current Product Version: " + Version);
					//}

					if (GUI.Button(new Rect(300, 275, 65, 20), "Purchase"))
					{
						Buying = true;
						//Bought();
					}

					if (GUI.Button(new Rect(200, 275, 85, 20), "Next Version"))
					{
						SelectedVersion++;
						VersionControl();
						UpdateUI();
					}

					if (GUI.Button(new Rect(100, 275, 85, 20), "Prev Version"))
					{
						SelectedVersion--;
						VersionControl();
						UpdateUI();
					}

					if (GUI.Button(new Rect(50, 275, 50, 20), "Home"))
					{
						ib.AddressBar = "www.reva.com/internal";
					}
				}
				break;

			//-- dont touch--
			case "www.reva.com/internal":
				if (logged == true)
				{
					if (GUI.Button(new Rect(10, 55, 100, 20), "News"))
					{
						ib.AddressBar = "www.reva.com/news";
						ib.AddHistory();
					}
					if (GUI.Button(new Rect(10, 75, 100, 20), "Contracts"))
					{
						ib.AddressBar = "www.reva.com/contracts";
						ib.AddHistory();
					}
					if (GUI.Button(new Rect(10, 95, 100, 20), "Hardware"))
					{
						ib.AddressBar = "www.reva.com/hardware";
						ib.AddHistory();
					}
					if (GUI.Button(new Rect(10, 115, 100, 20), "Software"))
					{
						ib.AddressBar = "www.reva.com/software";
						ib.AddHistory();
					}
					if (GUI.Button(new Rect(10, 150, 100, 20), "Sign Out"))
					{
						trace.startTrace = false;

						logged = false;
						ib.AddressBar = "www.reva.com/login";
						ib.AddHistory();
						ib.showAddressBar = true;
					}
				}
				break;
		}
	}
}
