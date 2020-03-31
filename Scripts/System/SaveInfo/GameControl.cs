using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class GameControl : MonoBehaviour
{
	public static GameControl control;

    public List<string> MissionType = new List<string>();

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

	public bool Booted;

    public bool Shutdown;

    public List<string> DocumentContent = new List<string>();
	public List<string> DocumentName = new List<string>();
    public List<string> MyFiles = new List<string>();
	public List<float> FileSize = new List<float>();
	public List<string> HackingSoftware = new List<string>();
	public List<string> BypassSoftware = new List<string>();
	public List<string> OtherSoftware = new List<string>();
	public List<string> SecuirtySoftware = new List<string>();
    public List<string> GateWayFiles = new List<string>();
    public List<string> Sites = new List<string>();
	public List<string> FavSites = new List<string>();

    // Mission Block
	public List<float> SoftwareVersion = new List<float>();
    public List<int> Hardware = new List<int>();
    // End Mission Block

	public bool logged;

	// Bank Info
	public List<string> AccountNumber = new List<string>();
	public List<string> Bank = new List<string>();
	public List<int> Balance = new List<int>();
	public List<bool> RegiBank = new List<bool>();
	public List<string> AccountName = new List<string>();
	public List<string> AccountPass = new List<string>();
	public List<float> AccountBalance = new List<float>();
	public List<float> CreditRating = new List<float>();
	public List<float> Loan = new List<float>();
	public List<float> LoanIntrest = new List<float>();
	public List<float> AccountIntrest = new List<float>();
	public int SelectedBank;

    public List<string> AcaName = new List<string>();
    public List<string> AcaDegree = new List<string>();
	public List<string> StudentDegree = new List<string>();

    // IP Block
    public int ip1;
    public int ip2;
    public int ip3;
    public int ip4;
    public string fullip;
    // End IP Block

    public int Fines;

	public float score;
	public int Clicks;
	public List<float> Upgrades = new List<float>();
	public List<float> Cost = new List<float>();

	public float BootTime;

	public string SpaceName;

	// Share Market
	public List<string> SharesBoughtOffName = new List<string>();
	public List<int> SharesBrought = new List<int>();
	public List<int> SharesBoughtPrice = new List<int>();


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
	public List<ProgramSystem> WebsiteFiles = new List<ProgramSystem>();
	public List<LoginSystem> StoredLogins = new List<LoginSystem>();
    public List<WebSecSystem> WebsiteSecurity = new List<WebSecSystem>();
    // WEBSITES END BLOCK
    public List<CHMSystem> ProgramInfo = new List<CHMSystem>();
	public List<BankSystem> MyBankDetails = new List<BankSystem>();
	public List<EmailSystem> EmailData = new List<EmailSystem>();
	public List<MissionSystem> Contracts = new List<MissionSystem>();
	public List<ProgramSystem> ProgramFiles = new List<ProgramSystem>();
	public List<ProgramSystem> QuickProgramList = new List<ProgramSystem>();
	public List<ProgramSystem> DesktopIconList = new List<ProgramSystem>();
	public MotherboardSystem Gateway;
    public MotherboardSystem PickedParts;

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


	public List<CompanyServerSystem> CompanyServers = new List<CompanyServerSystem>();

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
		data.WebsiteFiles = WebsiteFiles;
        data.GUIID = GUIID;
		data.DocumentContent = DocumentContent;
		data.DocumentName = DocumentName;
        data.MyFiles = MyFiles;
		data.FileSize = FileSize;
        data.fullip = fullip;
		data.Rep = Rep;
        data.Sites = Sites;
		data.FavSites = FavSites;
        data.SoftwareVersion = SoftwareVersion;
        data.Hardware = Hardware;
        data.ip1 = ip1;
        data.ip2 = ip2;
        data.ip3 = ip3;
        data.ip4 = ip4;
		data.StudentDegree = StudentDegree;
        data.AcaDegree = AcaDegree;
        data.AcaName = AcaName;
        data.Fines = Fines;
		data.Clicks = Clicks;
		data.score = score;
		data.Upgrades = Upgrades;
		data.Cost = Cost;
		data.SpaceName = SpaceName;
		data.SharesBoughtOffName = SharesBoughtOffName;
		data.SharesBoughtPrice = SharesBoughtPrice;
		data.SharesBrought = SharesBrought;
		data.AccountNumber = AccountNumber;
		data.Bank = Bank;
		data.Balance = Balance;
		data.SelectedBank = SelectedBank;
		data.StoredLogins = StoredLogins;
        data.WebsiteSecurity = WebsiteSecurity;
		data.ProgramInfo = ProgramInfo;
		data.MyBankDetails = MyBankDetails;
		data.EmailData = EmailData;
		data.Contracts = Contracts;
		data.ProgramFiles = ProgramFiles;
		data.QuickProgramList = QuickProgramList;
		data.Gateway = Gateway;
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
			WebsiteFiles = data.WebsiteFiles;
			StoryMis = data.StoryMis;
            GUIID = data.GUIID;
			DocumentContent = data.DocumentContent;
			DocumentName = data.DocumentName;
            MyFiles = data.MyFiles;
			FileSize = data.FileSize;
            fullip = data.fullip;
			Rep = data.Rep;
            Sites = data.Sites;
			FavSites = data.FavSites;
            SoftwareVersion = data.SoftwareVersion;
            Hardware = data.Hardware;
            ip1 = data.ip1;
            ip2 = data.ip2;
            ip3 = data.ip3;
            ip4 = data.ip4;
            AcaDegree = data.AcaDegree;
			StudentDegree = data.StudentDegree;
            AcaName = data.AcaName;
            Fines = data.Fines;
			Clicks = data.Clicks;
			score = data.score;
			Upgrades = data.Upgrades;
			Cost = data.Cost;
			SpaceName = data.SpaceName;
			SharesBoughtOffName = data.SharesBoughtOffName;
			SharesBoughtPrice = data.SharesBoughtPrice;
			SharesBrought = data.SharesBrought;
			AccountNumber = data.AccountNumber;
			Bank = data.Bank;
			Balance = data.Balance;
			SelectedBank = data.SelectedBank;
			StoredLogins = data.StoredLogins;
            WebsiteSecurity = data.WebsiteSecurity;
			ProgramInfo = data.ProgramInfo;
			MyBankDetails = data.MyBankDetails;
			EmailData = data.EmailData;
			Contracts = data.Contracts;
			ProgramFiles = data.ProgramFiles;
			QuickProgramList = data.QuickProgramList;
			DesktopIconList = data.DesktopIconList;
			Gateway = data.Gateway;
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

	public List<string> DocumentContent = new List<string>();
	public List<string> DocumentName = new List<string>();
    public List<string> MyFiles = new List<string>();
	public List<float> FileSize = new List<float>();
    public List<string> Sites = new List<string>();
	public List<string> FavSites = new List<string>();
    // Mission Block
	public List<float> SoftwareVersion = new List<float>();
    public List<int> Hardware = new List<int>();
    // End Mission Block

    public List<string> AcaName = new List<string>();
    public List<string> AcaDegree = new List<string>();
	public List<string> StudentDegree = new List<string>();

	// Bank Info
	public List<string> AccountNumber = new List<string>();
	public List<string> Bank = new List<string>();
	public List<int> Balance = new List<int>();
	public int SelectedBank;

    // IP Block
    public int ip1;
    public int ip2;
    public int ip3;
    public int ip4;
    public string fullip;

    public int Fines;

	public float score;
	public int Clicks;
	public List<float> Upgrades = new List<float>();
	public List<float> Cost = new List<float>();

	public string SpaceName;

	public List<string> SharesBoughtOffName = new List<string>();
	public List<int> SharesBrought = new List<int>();
	public List<int> SharesBoughtPrice = new List<int>();

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
	public List<ProgramSystem> WebsiteFiles = new List<ProgramSystem>();
	public List<LoginSystem> StoredLogins = new List<LoginSystem>();
    public List<WebSecSystem> WebsiteSecurity = new List<WebSecSystem>();
    // WEBSITES END BLOCK
    public List<CHMSystem> ProgramInfo = new List<CHMSystem>();
	public List<BankSystem> MyBankDetails = new List<BankSystem>();
	public List<EmailSystem> EmailData = new List<EmailSystem>();
	public List<MissionSystem> Contracts = new List<MissionSystem>();
	public List<ProgramSystem> ProgramFiles = new List<ProgramSystem>();
	public List<ProgramSystem> QuickProgramList = new List<ProgramSystem>();
	public List<ProgramSystem> DesktopIconList = new List<ProgramSystem>();
	public MotherboardSystem Gateway;
    public MotherboardSystem PickedParts;

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
}