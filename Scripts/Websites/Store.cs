using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
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

	private Tracer trace;

	public List<string> RevaSoftware = new List<string>();
	public List<int> Cost = new List<int>();

	public string SelectedProgram;

	public List<ProgramSystemv2> Catalog = new List<ProgramSystemv2>();

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
	public string Cat;
	public CHMSystem ProgramInfo;
	public int ProgramID;
	public bool Buying;

	public string Username;
	public string Password;
	public bool ShowPassword;

	public string OSName;


	public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
	public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

	void Start()
	{
		SysSoftware = GameObject.Find("System");
		Missions = GameObject.Find("Missions");
		AppsSoftware = GameObject.Find("Applications");
		Prompts = GameObject.Find("Prompts");
		Computer = GameObject.Find("Computer");
		HackingSoftware = GameObject.Find("Hacking");

		WebSearch();
		UpdateCatalog();
	}

	void WebSearch()
	{
		ib = AppsSoftware.GetComponent<InternetBrowser>();
		misgen = Missions.GetComponent<MissionGen>();
		ep = Prompts.GetComponent<ErrorProm>();
		defalt = SysSoftware.GetComponent<Defalt>();
		pp = Prompts.GetComponent<PurchasePrompt>();
		trace = HackingSoftware.GetComponent<Tracer>();
		brow = Missions.GetComponent<MissionBrow>();
		sm = AppsSoftware.GetComponent<SystemMap>();

	}

	void UpdateCatalog()
	{
		Catalog.Add(new ProgramSystemv2("Notepad", "", "", "", "", "The notepad for all your typing needs.", "www.store.com", "Notepad", "", "", ProgramSystemv2.FileExtension.Ins, ProgramSystemv2.FileExtension.Exe, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 250, 0, 0, 0, 0, 0, 0, false, false, false, false,false,false,false));

		Catalog.Add(new ProgramSystemv2("Notepad v2", "", "", "", "", "The notepad for all your typing needs.", "www.store.com", "Notepadv2", "", "", ProgramSystemv2.FileExtension.Ins, ProgramSystemv2.FileExtension.Exe, 0, 0, 4, 0, 0, 0, 0, 100, 2.0f, 500, 0, 0, 0, 0, 0, 0, false, false, false, false, false, false, false));

		OSName = OperatingSystems.OSName.FluidicIceOS.ToString();
		Catalog.Add(new ProgramSystemv2(OSName, "", "", "", "", "FluidicIceOS", "www.store.com", OSName, "", "", ProgramSystemv2.FileExtension.Ins, ProgramSystemv2.FileExtension.Exe, 0, 0, 20, 0, 0, 0, 0, 100, 1.0f, 500, 0, 0, 0, 0, 0, 0, false, false, false, false, false, false, false));

		OSName = OperatingSystems.OSName.AppatureOS.ToString();
		Catalog.Add(new ProgramSystemv2(OSName, "", "", "", "", "The notepad for all your typing needs.", "www.store.com", OSName, "", "", ProgramSystemv2.FileExtension.Ins, ProgramSystemv2.FileExtension.OS, 0, 0, 15, 0, 0, 0, 0, 100, 1.0f, 500, 0, 0, 0, 0, 0, 0, false, false, false, false, false, false, false));

		OSName = OperatingSystems.OSName.TreeOS.ToString();
		Catalog.Add(new ProgramSystemv2(OSName, "", "", "", "", "The notepad for all your typing needs.", "www.store.com", OSName, "", "", ProgramSystemv2.FileExtension.Ins, ProgramSystemv2.FileExtension.OS, 0, 0, 20, 0, 0, 0, 0, 100, 1.0f, 500, 0, 0, 0, 0, 0, 0, false, false, false, false, false, false, false));
	}

	void Bought()
	{
		ib.DownloadManager(Catalog[Select]);
		Buying = false;
	}

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

			case "www.store.com":
				scrollpos = GUI.BeginScrollView(new Rect(2, 25, 165, 240), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
				for (scrollsize = 0; scrollsize < Catalog.Count; scrollsize++)
				{
					if (GUI.Button(new Rect(0, scrollsize * 20, 149, 20), "" + Catalog[scrollsize].Name))
					{
						Select = scrollsize;
					}
				}
				GUI.EndScrollView();

				GUI.Box((new Rect(168, 25, 330, 240)), "");

				GUI.Label(new Rect(171, 25, 300, 300), "Product Name: " + Catalog[Select].Name);
				GUI.Label(new Rect(171, 45, 300, 300), "Product Type: " + Catalog[Select].Extension.ToString());
				GUI.Label(new Rect(171, 65, 300, 300), "Product Desc: " + Catalog[Select].Description);
				GUI.Label(new Rect(171, 160, 300, 300), "Product Cost: " + Catalog[Select].Price);
				GUI.Label(new Rect(171, 175, 300, 300), "Product Size: " + Catalog[Select].Used);
				GUI.Label(new Rect(171, 190, 300, 300), "Product Version: " + Catalog[Select].Version);
				GUI.Label(new Rect(171, 205, 300, 300), "----------------");


				//if (GameControl.control.SoftwareVersion[ProgramID] != 0)
				//{
				//	GUI.Label(new Rect(171, 243, 500, 500), "Current Product Version: " + Version);
				//}

				//if (GUI.Button(new Rect(300, 275, 65, 20), "Purchase"))
				//{
				//	if (GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance >= Catalog[Select].Price)
				//	{
				//		GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance -= Catalog[Select].Price;
				//		Buying = true;
				//		Bought();
				//	}
				//}

				//if (GUI.Button(new Rect(200, 275, 85, 20), "Next Version"))
				//{
				//	SelectedVersion++;
				//	VersionControl();
				//}

				//if (GUI.Button(new Rect(100, 275, 85, 20), "Prev Version"))
				//{
				//	SelectedVersion--;
				//	VersionControl();
				//}
				break;
		}
	}
}
