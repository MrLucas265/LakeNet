using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyCreator : MonoBehaviour
{
    public string Name;
    public string PID;
    public string PhoneNumber;
    public string IPAddress;
    public string MaritalStatus;
    public string PersonalStatus;
    public int IQ;
    public Texture2D Photo;
    public BankSystem TempBankDetails;
    public MotherboardSystem TempGatewayDetails;
    public DOBSystem TempDOB;
    public List<ServerSystem> TempServer = new List<ServerSystem>();
    public GatewaySystem TempGateway;
    public MotherboardSystem TempMotherboard;
    public List<ProgramSystem> TempFiles = new List<ProgramSystem>();
    public List<BankSystem> BankAccounts = new List<BankSystem>();
    public ProgramSystem TempFiles1;
    public List<Texture2D> Faces = new List<Texture2D>();
    public ServerSystem TempServerInfo;
    public List<WebSecSystem> websec = new List<WebSecSystem>();

    public int Ammount;

    public bool AddNewPeople;
    public bool WipeData;

    public int Count;

    public NamesList NamesList;

    public int SelectedName;

    public int RandomFileCount;
    public int RandomFileCount1;

    public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
    public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

    public int RandomAmountOfServers;

    // Use this for initialization
    void Start()
    {
        //PopulateCollages();
        //PopulateUni();
        NamesList = GetComponent<NamesList>();
        NamesList.NameListResource();
        Ammount = 10;
        PopulateMotherboard();
        PopulateFiles();
        websec.Add(new WebSecSystem(WebSecSystem.Server.Unicom, "Password", 1, "", 0, 0, WebSecSystem.SecType.UAC));
    }

    // Update is called once per frame
    void Update()
    {

        if (PersonController.control.People.Count <= 0)
        {
            AddNewPeople = true;
        }
        if (AddNewPeople == true)
        {
            for (int i = 0; i <= Ammount; i++)
            {
                PopulateBasicInformation();
                PopulateDateOfBirth();
                PopulateBankInformation();
                PopulateServerInfo();
                TempGateway.Name = Name + "'s Gateway";
                PersonController.control.Orgnizations.Add(new OrgnizationSystem(Name, "", StringGenerator.RandomNumberChar(9, 9), TempDOB, OrgnizationSystem.OrgType.Medical, TempServer, TempBankDetails));
                Count = i;
                ResetAllInformation();
            }
        }

        if (WipeData == true)
        {
            WipeDatabase();
        }


        if (Count >= Ammount)
        {
            AddNewPeople = false;
            Count = 0;
            PersonController.control.Orgnizations.RemoveAt(PersonController.control.Orgnizations.Count - 1);
        }
    }

    void PopulateServerInfo()
    {
        for (int i = 0; i < RandomAmountOfServers;i++)
        {
            TempServerInfo.Name = "Test " + Random.Range(0, 100);
            TempServerInfo.Address = "";
            TempServerInfo.Gateway = TempGateway;
            TempServerInfo.Security = websec;
            TempServerInfo.Type = ServerSystem.ServerType.Backup;
            TempServer.Add(new ServerSystem(TempServerInfo.Name, TempServerInfo.Gateway, TempServerInfo.Security, TempServerInfo.Type));
        }
    }

    void PopulateBasicInformation()
    {
        PID = StringGenerator.RandomNumberChar(8, 8);
        //SelectedName = Random.Range(0, WebSecSystem.Server.Academics);

        //Name = NamesList.Names[SelectedFirstName].Trim() + " " + NamesList.Names[SelectedLastName].Trim();
        PhoneNumber = "12345678";
        IPAddress = "127.0.0.1";
    }

    void PopulateDateOfBirth()
    {
        TempDOB.Year = Random.Range(1900, 1980);

        TempDOB.Month = Random.Range(1, 13);

        if (TempDOB.Month != 2)
        {
            if (TempDOB.Month == 4 || TempDOB.Month == 6 || TempDOB.Month == 9 || TempDOB.Month == 11)
            {
                TempDOB.Day = Random.Range(1, 31);
            }
            else
            {
                TempDOB.Day = Random.Range(1, 32);
            }
        }
        else
        {
            TempDOB.Day = Random.Range(1, 27);
        }

        TempDOB.Age = 1980 - TempDOB.Year;

        TempDOB = new DOBSystem(TempDOB.Day, TempDOB.Month, TempDOB.Year, TempDOB.Age);
    }

    void PopulateBankInformation()
    {
        //TempBankDetails.AccountName = Name;
        //TempBankDetails.AccountNumber = StringGenerator.RandomNumberChar(9,9);
        //TempBankDetails.AccountPass = StringGenerator.RandomMixedChar(6,12);
        //TempBankDetails.BankIP = "127.0.0.1";
        //TempBankDetails.BankName = "LEC Bank";
        //TempBankDetails.CreditRating = 1;
        //TempBankDetails.Loan = 0;
        //TempBankDetails.LoanIntrest = 1;
        //TempBankDetails.AccountIntrest = Random.Range(0, 5);
        //TempBankDetails.AccountBalance = Random.Range(0, 50000);
        //TempBankDetails = new BankSystem(TempBankDetails.AccountName, TempBankDetails.AccountNumber, TempBankDetails.AccountPass, TempBankDetails.BankIP, TempBankDetails.BankName,
        //    TempBankDetails.CreditRating, TempBankDetails.Loan, TempBankDetails.LoanIntrest, TempBankDetails.AccountIntrest, TempBankDetails.AccountBalance);
    }

    void PopulateFiles()
    {
        TempGateway.SelectedOS.Name = OperatingSystems.OSName.AppatureOS;
        TempGateway.Files.FileList.Add(new ProgramSystem("C:/", "System", "", "", "", "", "Gateway", "C:/", "", "", ProgramSystem.FileExtension.Dir, ProgramSystem.FileExtension.Null, 0, 0, 60, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        TempGateway.Files.FileList.Add(new ProgramSystem("Downloads", "", "", "", "", "", "C:/", "C:/Downloads", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        TempGateway.Files.FileList.Add(new ProgramSystem("Documents", "", "", "", "", "", "C:/", "C:/Documents", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        TempGateway.Files.FileList.Add(new ProgramSystem("Programs", "", "", "", "", "", "C:/", "C:/Programs", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        TempGateway.Files.FileList.Add(new ProgramSystem("System", "", "", "", "", "", "C:/", "C:/System", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        TempGateway.Files.FileList.Add(new ProgramSystem("" + TempGateway.SelectedOS.Name, "", "", "", "", "", "C:/System", "", "", "", ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null, 0, 0, 10, 0, 0, 0, 0, 100, 1, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        //TempGateway.Add(new FileSystem(TempFiles, null,null));
        TempGateway.Motherboard.InstalledStorageDevice[0].UsedSpace += 60;
        TempGateway.Motherboard.InstalledStorageDevice[0].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[0].Capacity - GameControl.control.Gateway.InstalledStorageDevice[0].UsedSpace;
    }

    void PopulateMotherboard()
    {
        string IPAddress = StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3);

        TempGateway.Motherboard.InstalledCPU.Add(new CPUSystem("Zion Z-14", "Zion", "Z-14", "140", 32, 1, 0.5f, 0.1f, 0.5f, 0, 0, 0, 0, 0, 1, 0.01f, 0, 100, 100, 0, "", 0, 20, 0.0025f, 0, 0, 0));
        TempGateway.Motherboard.InstalledGPU.Add(new GPUSystem("Qividia 970", "PCI-E", 970, 2, 0.1f, 2f, 256, 0, 0, 0, 1, 0.01f, 100, 100, 0, 120, 0.0025f));
        TempGateway.Motherboard.InstalledRAM.Add(new RamSystem("Vortex 2GB", "DDR1", 0, 2048, 0, 0, 100, 0.01f, 100, 100, 0, 0.0025f, 0));
        TempGateway.Motherboard.InstalledStorageDevice.Add(new StorageDevice("EasternVirtual 128", "", "", "", 0.133f, 0, 128, 128, 15, 0.001f, 100, 100, 0, 0.0025f, 0.14f, 0, 1, StorageDevice.StorageType.HDD, 0, 0, 0, null));
        TempGateway.Motherboard.InstalledPSU.Add(new PowerSupplySystem("Toughpower-2pack", "", 450, 0, 0, 0.01f, 100, 100, 0, 0.0025f));
        TempGateway.Motherboard.InstalledModem.Add(new ModemSystem("TUGs Basic Modem", "", "", "", 0.56f, 0.56f, 0.56f, 0.28f, 0, 15, 0.01f, 100, 100, 0, 0.0025f, 50, 25, IPAddress, ModemSystem.ModemConnectionType.DialUp));
    }

    void PopulateGatewayInformation()
    {
        //TempGatewayDetails.InstalledCPU.;
    }

    void WipeDatabase()
    {
        PersonController.control.People.RemoveRange(0, PersonController.control.People.Count);
        WipeData = false;
    }

    void ResetAllInformation()
    {
        Name = "";
        PID = "";
        PhoneNumber = "";
        IPAddress = "";

        //TempBankDetails.AccountName = "";
        //TempBankDetails.AccountNumber = "";
        //TempBankDetails.AccountPass = "";
        //TempBankDetails.BankIP = "";
        //TempBankDetails.BankName = "";
        //TempBankDetails.CreditRating = 0;
        //TempBankDetails.Loan = 0;
        //TempBankDetails.LoanIntrest = 0;
        //TempBankDetails.AccountIntrest = 0;
        //TempBankDetails.AccountBalance = 0;

        TempServerInfo.Name = "";
        TempServerInfo.Address = "";
        TempServerInfo.Gateway = null;
        TempServerInfo.Security = null;
        TempServerInfo.Type = ServerSystem.ServerType.Backup;

        //TempAcademicDetails1.CollageQualifications.Clear();
        //TempAcademicDetails1.UniversityQualifications.Clear();
        //TempAcademicDetails.OtherQualifications = "";
        //TempMotherboard = null;
    }
}
