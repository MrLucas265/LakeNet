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
    public ProgramSystem TempFiles1;
    public List<Texture2D> Faces = new List<Texture2D>();

    public List<string> CollageClasses = new List<string>();
    public List<string> CollageGrades = new List<string>();

    public List<string> UniClasses = new List<string>();
    public List<string> UniGrades = new List<string>();

    public List<CollageSystem> TempCollage = new List<CollageSystem>();
    public List<UniversitySystem> TempUni = new List<UniversitySystem>();

    public CollageSystem TempCollage1;
    public UniversitySystem TempUni1;

    public List<WebSecSystem> websec = new List<WebSecSystem>();

    public int Ammount;

    public bool AddNewPeople;
    public bool WipeData;

    public int Count;

    public NamesList NamesList;

    public int SelectedFirstName;
    public int SelectedLastName;

    public string SelectedClass;
    public string SelectedGrade;

    public int RandomFileCount;
    public int RandomFileCount1;

    public int For1;
    public int For2;

    // Use this for initialization
    void Start()
    {
        //PopulateCollages();
        //PopulateUni();
        NamesList = GetComponent<NamesList>();
        NamesList.NameListResource();
        Ammount = 10;
        PopulateMotherboard();
    }

    // Update is called once per frame
    void Update()
    {
        if (AddNewPeople == true)
        {
            for (int i = 0; i <= Ammount; i++)
            {
                For1 = i;
                PopulateBasicInformation();
                PopulateDateOfBirth();
                PopulateBankInformation();
                if (PersonController.control.Orgnizations[i].Server.Count == 0)
                {
                    PopulateFiles();
                    PopulateServerInfo();
                }
                //PopulateAcademicInformation();
                //Photo = Faces[Random.Range(0, 800)];
                TempGateway.Name = Name + "'s Gateway";
                //TempBankDetails

                PersonController.control.Orgnizations.Add(new OrgnizationSystem(Name,"",StringGenerator.RandomNumberChar(9,9),TempDOB,OrgnizationSystem.OrgType.Medical, TempServer));

                //PersonController.control.PeoplesName.Add(Name);
                Count = i;
                ResetAllInformation();
            }

        }

        //if(Count >= PersonController.control.Orgnizations.Count-1)
        //{
        //    PopulateRandomFiles();
        //    //if (RandomFileCount >= TempFiles.Count - 1)
        //    //{
        //    //    TempFiles.RemoveRange(0, TempFiles.Count);
        //    //}
        //}

        if (WipeData == true)
        {
            WipeDatabase();
        }


        if (Count >= Ammount)
        {
            AddNewPeople = false;
            //PopulateRandomFiles();
            Count = 0;
            PersonController.control.People.RemoveAt(PersonController.control.People.Count - 1);
            for (int i = 0; i < PersonController.control.People.Count; i++)
            {
                if (PersonController.control.People[i].Name != PersonController.control.People[i].BankDetails.AccountName)
                {
                    PersonController.control.People[i].BankDetails.AccountName = PersonController.control.People[i].Name;
                }
            }
        }
    }

    void PopulateServerInfo()
    {
        websec.Add(new WebSecSystem(WebSecSystem.Server.Unicom, "Password", 1, "", 0, 0, WebSecSystem.SecType.UAC));
        TempServer.Add(new ServerSystem("Test", TempGateway, websec, ServerSystem.ServerType.Medical));
    }

    void PopulateBasicInformation()
    {
        PID = StringGenerator.RandomNumberChar(8, 8);
        SelectedFirstName = Random.Range(0, NamesList.Names.Count - 1);
        SelectedLastName = Random.Range(0, NamesList.Names.Count - 1);

        Name = NamesList.Names[SelectedFirstName].Trim() + " " + NamesList.Names[SelectedLastName].Trim();
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

    void PopulateFiles()
    {
        TempGateway.SelectedOS.Name = OperatingSystems.OSName.AppatureOS;
        TempFiles.Add(new ProgramSystem("C:/", "System", "", "", "Gateway", "C:/", 0, 0, 0, 60, 100, 0, false, ProgramSystem.ProgramType.Dir));
        TempFiles.Add(new ProgramSystem("Downloads", "", "", "", "C:/", "C:/Downloads", 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
        TempFiles.Add(new ProgramSystem("Documents", "", "", "", "C:/", "C:/Documents", 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
        TempFiles.Add(new ProgramSystem("Program Files", "", "", "", "C:/", "C:/Programs", 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
        TempFiles.Add(new ProgramSystem("System Files", "", "", "", "C:/", "C:/System", 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
        TempFiles.Add(new ProgramSystem("" + TempGateway.SelectedOS.Name, "", "", "", "C:/System", "", 0, 0, 10, 0, 100, 1, false, ProgramSystem.ProgramType.OS));
        TempGateway.Motherboard.InstalledStorageDevice[0].UsedSpace += 60;
        TempGateway.Motherboard.InstalledStorageDevice[0].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[0].Capacity - GameControl.control.Gateway.InstalledStorageDevice[0].UsedSpace;

        for (int i = 0; i < PersonController.control.Orgnizations.Count; i++)
        {
            for (int j = 0; j < PersonController.control.Orgnizations[i].Server.Count; j++)
            {
                if (TempFiles.Count > 0)
                {
                    for (int l = 0; l < TempFiles.Count; l++)
                    {
                        RandomFileCount = l;
                        PersonController.control.Orgnizations[i].Server[j].Gateway.Files.Files.Add(TempFiles[l]);
                    }
                }
            }
        }
    }

    void PopulateMotherboard()
    {
        TempGateway.Motherboard.InstalledCPU.Add(new CPUSystem("Zion Z-14", "Zion", "Z-14", "140", 32, 1, 0.5f, 0.1f, 0.5f, 0, 0, 0, 0, 0, 1, 0.01f, 0, 100, 100, 0, "", 0, 20, 0.0025f,0,0, 0));
        TempGateway.Motherboard.InstalledGPU.Add(new GPUSystem("Qividia 970", "PCI-E", 970, 2, 0.1f, 2f, 256, 0, 0, 0, 1, 0.01f, 100, 100, 0, 120, 0.0025f));
        TempGateway.Motherboard.InstalledRAM.Add(new RamSystem("Vortex 2GB", "DDR1", 0, 2048, 0, 0, 100, 0.01f, 100, 100, 0, 0.0025f,0));
        TempGateway.Motherboard.InstalledStorageDevice.Add(new StorageDevice("Server 9001", "", "", "", 0.133f, 0, 9001, 9001, 15, 0.001f, 100, 100, 0, 0.0025f, 0.14f, StorageDevice.StorageType.HDD,0,0,0));
        TempGateway.Motherboard.InstalledPSU.Add(new PowerSupplySystem("Toughpower-2pack", "", 450, 0, 0, 0.01f, 100, 100, 0, 0.0025f));
        TempGateway.Motherboard.InstalledModem.Add(new ModemSystem("TUGs Basic Modem", "", "", "", 0.56f, 0.56f, 0.28f, 0, 15, 0.01f, 100, 100, 0, 0.0025f, 50, 25, ModemSystem.ModemConnectionType.DialUp));
    }

    void PopulateBankInformation()
    {
        TempBankDetails.AccountName = Name;
        TempBankDetails.AccountNumber = StringGenerator.RandomNumberChar(9, 9);
        TempBankDetails.AccountPass = StringGenerator.RandomMixedChar(6, 12);
        TempBankDetails.BankIP = "127.0.0.1";
        TempBankDetails.BankName = "LEC Bank";
        TempBankDetails.CreditRating = 1;
        TempBankDetails.Loan = 0;
        TempBankDetails.LoanIntrest = 1;
        TempBankDetails.AccountIntrest = Random.Range(0, 5);
        TempBankDetails.AccountBalance = Random.Range(0, 50000);
        TempBankDetails = new BankSystem(TempBankDetails.AccountName, TempBankDetails.AccountNumber, TempBankDetails.AccountPass, TempBankDetails.BankIP, TempBankDetails.BankName,
            TempBankDetails.CreditRating, TempBankDetails.Loan, TempBankDetails.LoanIntrest, TempBankDetails.AccountIntrest, TempBankDetails.AccountBalance);
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

        TempBankDetails.AccountName = "";
        TempBankDetails.AccountNumber = "";
        TempBankDetails.AccountPass = "";
        TempBankDetails.BankIP = "";
        TempBankDetails.BankName = "";
        TempBankDetails.CreditRating = 0;
        TempBankDetails.Loan = 0;
        TempBankDetails.LoanIntrest = 0;
        TempBankDetails.AccountIntrest = 0;
        TempBankDetails.AccountBalance = 0;

        //TempAcademicDetails1.CollageQualifications.Clear();
        //TempAcademicDetails1.UniversityQualifications.Clear();
        //TempAcademicDetails.OtherQualifications = "";
        //TempMotherboard = null;
    }
}
