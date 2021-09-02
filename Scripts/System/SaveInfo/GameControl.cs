using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class GameControl : MonoBehaviour
{
	public static GameControl control;

    public int LCDPage;

    public bool ChangeColor;
    public int Red;
    public int Green;
    public int Blue;

	public float wh;

	public int TimeMulti;

	public int Health;

	public bool HDDLoopCheck;

	public int m_focusingID;

	public string ProfilePath;
	public int ProfilePicID;
	public int ProfileID;
    public int GUIID;
	public string ProfileName;
    public string Hover;
	public List<Texture2D> UserPic = new List<Texture2D>();
    public bool NewAccount;
	public bool[] StoryMis;

    public List<string> Sites = new List<string>();
	public List<string> FavSites = new List<string>();

    // Mission Block
    // End Mission Block

	// Bank Info
	public BankSystem SelectedBank;

	// IP Block
	// End IP Block

	public int Fines;

	public float BootTime;

	public string SpaceName;

	// Share Market


	public List<string> QuickLaunchNames = new List<string>();

	//Location
	public string GatewayLocation;
	public float GatewayPosX;
	public float GatewayPosY;

	public int SelectedOSInt;

	// NEW SAVE SYSTEMS
	//COMMAND
	public List<CLICMDS> Commands = new List<CLICMDS>();
	// WEBSITES BLOCK
	public List<LoginSystem> StoredLogins = new List<LoginSystem>();
    public List<WebSecSystem> WebsiteSecurity = new List<WebSecSystem>();
    // WEBSITES END BLOCK
    public List<CHMSystem> ProgramInfo = new List<CHMSystem>();
	public List<BankSystem> BankData = new List<BankSystem>();
	public List<EmailSystem> EmailData = new List<EmailSystem>();
	public List<MissionSystem> Contracts = new List<MissionSystem>();
	public List<ProgramSystem> ProgramFiles = new List<ProgramSystem>();
	public List<ProgramSystem> QuickProgramList = new List<ProgramSystem>();
	public List<ProgramSystem> DesktopIconList = new List<ProgramSystem>();
	public MotherboardSystem Gateway;
	public List<WarehouseSystem> StoredParts = new List<WarehouseSystem>();

	//Stocks
	public List<StockPortfolioSystem> TransactionHistory = new List<StockPortfolioSystem>();
    public List<StockPortfolioSystem> Portfolio = new List<StockPortfolioSystem>();
	public List<StockExchangeShareSystem> Exchanges = new List<StockExchangeShareSystem>();

    //OS
    public List<OperatingSystems> OSName = new List<OperatingSystems>();
	public OperatingSystems SelectedOS;


    public List<NotificationSystem> Notifications = new List<NotificationSystem>();
    public List<ReminderSystem> Reminders = new List<ReminderSystem>();

    public List<PlanSystem> Plans = new List<PlanSystem>();

    public DateSystem Time;

    //REP
    public List<RepSystem> Rep = new List<RepSystem>();

	public string SerialKey;

    public bool ShortCommands;

	public List<string> GameVersion = new List<string>();


	public List<CompanyServerSystem> CompanyServerData = new List<CompanyServerSystem>();

	public List<ProgramSystem> DefaultLaunchedPrograms = new List<ProgramSystem>();

	// Use this for initialization

	void Start()
	{
		ProfilePath = ProfileController.procon.FilePath;
		//ProfilePath = "";
	}

	void Awake ()
	{
		Awake1();
	}

	public void DeleteFile ()
	{
		File.Delete(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber+ "/profiles/" + ProfileName + ".dat");
	}

	void Awake1()
	{
		if(control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control != this)
		{
			Destroy(gameObject);
		}
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber+ "/profiles/" + ProfileName + ".dat");
		//Debug.Log(Application.persistentDataPath);
		ComputerData data = new ComputerData();

        data.NewAccount = NewAccount;
        data.ProfileName = ProfileName;
		data.StoryMis = StoryMis;
        data.ProfileID = ProfileID;
        data.GUIID = GUIID;
		data.Rep = Rep;
        data.Sites = Sites;
		data.FavSites = FavSites;
        data.Fines = Fines;
		data.SpaceName = SpaceName;
		data.StoredLogins = StoredLogins;
        data.WebsiteSecurity = WebsiteSecurity;
		data.ProgramInfo = ProgramInfo;
		data.BankData = BankData;
		data.EmailData = EmailData;
		data.Contracts = Contracts;
		data.ProgramFiles = ProgramFiles;
		data.QuickProgramList = QuickProgramList;
		data.Gateway = Gateway;
		data.StoredParts = StoredParts;
        data.TransactionHistory = TransactionHistory;
        data.Portfolio = Portfolio;
		data.Exchanges = Exchanges;
		data.QuickLaunchNames = QuickLaunchNames;
		data.DesktopIconList = DesktopIconList;
		data.SelectedOS = SelectedOS;
        data.Notifications = Notifications;
        data.Reminders = Reminders;
        data.Plans = Plans;
        data.Time = Time;
		data.OSName = OSName;
		data.GatewayLocation = GatewayLocation;
		data.GatewayPosX = GatewayPosX;
		data.GatewayPosY = GatewayPosY;
		data.Commands = Commands;
		data.SelectedOSInt = SelectedOSInt;
		data.Serialkey = SerialKey;
        data.ShortCommands = ShortCommands;
		data.GameVersion = GameVersion;
		data.DefaultLaunchedPrograms = DefaultLaunchedPrograms;
		data.CompanyServerData = CompanyServerData;

		bf.Serialize (file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber+ "/profiles/" + ProfileName + ".dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber+ "/profiles/" + ProfileName + ".dat", FileMode.Open);
			//Debug.Log(Application.persistentDataPath);
			ComputerData data = (ComputerData)
			bf.Deserialize (file);
			file.Close ();

            NewAccount = data.NewAccount;
			ProfileName = data.ProfileName;
			ProfileID = data.ProfileID;
			StoryMis = data.StoryMis;
            GUIID = data.GUIID;
			Rep = data.Rep;
            Sites = data.Sites;
			FavSites = data.FavSites;
            Fines = data.Fines;
			SpaceName = data.SpaceName;
			StoredLogins = data.StoredLogins;
            WebsiteSecurity = data.WebsiteSecurity;
			ProgramInfo = data.ProgramInfo;
			BankData = data.BankData;
			EmailData = data.EmailData;
			Contracts = data.Contracts;
			ProgramFiles = data.ProgramFiles;
			QuickProgramList = data.QuickProgramList;
			DesktopIconList = data.DesktopIconList;
			Gateway = data.Gateway;
			StoredParts = data.StoredParts;
			TransactionHistory = data.TransactionHistory;
            Portfolio = data.Portfolio;
			Exchanges = data.Exchanges;
			QuickLaunchNames = data.QuickLaunchNames;
			SelectedOS = data.SelectedOS;
            Notifications = data.Notifications;
            Reminders = data.Reminders;
            Plans = data.Plans;
            Time = data.Time;
			OSName = data.OSName;
			GatewayLocation = data.GatewayLocation;
			GatewayPosX = data.GatewayPosX;
			GatewayPosY = data.GatewayPosY;
			Commands = data.Commands;
			SelectedOSInt = data.SelectedOSInt;
			SerialKey = data.Serialkey;
            ShortCommands = data.ShortCommands;
			GameVersion = data.GameVersion;
			DefaultLaunchedPrograms = data.DefaultLaunchedPrograms;
			CompanyServerData = data.CompanyServerData;
		}
	}
}

[Serializable]
class ComputerData
{
	public List<string> MissionType = new List<string>();

    public string ProfilePath;
    public int ProfileID;
    public int GUIID;
    public string ProfileName;
    public bool NewAccount;


	public bool[] StoryMis;

    public List<string> Sites = new List<string>();
	public List<string> FavSites = new List<string>();
    // Mission Block
	public List<float> SoftwareVersion = new List<float>();
    public List<int> Hardware = new List<int>();
    // End Mission Block

    public List<string> AcaName = new List<string>();
    public List<string> AcaDegree = new List<string>();
	public List<string> StudentDegree = new List<string>();

    // IP Block

    public int Fines;

	public string SpaceName;

	public List<string> QuickLaunchNames = new List<string>();

	// Location
	public string GatewayLocation;
	public float GatewayPosX;
	public float GatewayPosY;

	public int SelectedOSInt;

	// NEW SAVE SYSTEMS
	//COMMAND
	public List<CLICMDS> Commands = new List<CLICMDS>();
	// WEBSITES BLOCK
	public List<LoginSystem> StoredLogins = new List<LoginSystem>();
    public List<WebSecSystem> WebsiteSecurity = new List<WebSecSystem>();
    // WEBSITES END BLOCK
    public List<CHMSystem> ProgramInfo = new List<CHMSystem>();
	public List<BankSystem> BankData = new List<BankSystem>();
	public List<EmailSystem> EmailData = new List<EmailSystem>();
	public List<MissionSystem> Contracts = new List<MissionSystem>();
	public List<ProgramSystem> ProgramFiles = new List<ProgramSystem>();
	public List<ProgramSystem> QuickProgramList = new List<ProgramSystem>();
	public List<ProgramSystem> DesktopIconList = new List<ProgramSystem>();
	public MotherboardSystem Gateway;
	public List<WarehouseSystem> StoredParts = new List<WarehouseSystem>();

	//Stocks
	public List<StockPortfolioSystem> TransactionHistory = new List<StockPortfolioSystem>();
    public List<StockPortfolioSystem> Portfolio = new List<StockPortfolioSystem>();
	public List<StockExchangeShareSystem> Exchanges = new List<StockExchangeShareSystem>();

	//OS
	public List<OperatingSystems> OSName = new List<OperatingSystems>();
	public OperatingSystems SelectedOS;

    public List<NotificationSystem> Notifications = new List<NotificationSystem>();
    public List<ReminderSystem> Reminders = new List<ReminderSystem>();

    public List<PlanSystem> Plans = new List<PlanSystem>();

    public DateSystem Time;

    //REP
    public List<RepSystem> Rep = new List<RepSystem>();

	public string Serialkey;

    public bool ShortCommands;

	public List<string> GameVersion = new List<string>();

	public List<CompanyServerSystem> CompanyServerData = new List<CompanyServerSystem>();

	public List<ProgramSystem> DefaultLaunchedPrograms = new List<ProgramSystem>();
}