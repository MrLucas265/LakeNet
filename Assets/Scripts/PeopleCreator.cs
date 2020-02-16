using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCreator : MonoBehaviour
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
    public GatewaySystem TempGateway;
    public MotherboardSystem TempMotherboard;
    public List<ProgramSystem> TempFiles = new List<ProgramSystem>();
    public List<Texture2D> Faces = new List<Texture2D>();

    public List<string> CollageClasses = new List<string>();
    public List<string> CollageGrades = new List<string>();

    public List<string> UniClasses = new List<string>();
    public List<string> UniGrades = new List<string>();

    public List<CollageSystem> TempCollage = new List<CollageSystem>();
    public List<UniversitySystem> TempUni = new List<UniversitySystem>();

    public CollageSystem TempCollage1;
    public UniversitySystem TempUni1;

    const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    const string AccNo = "1234567890";

    public int Ammount;

    public bool AddNewPeople;
    public bool WipeData;

    public int Count;

    public NamesList NamesList;

    public int SelectedFirstName;
    public int SelectedLastName;

    public string SelectedClass;
    public string SelectedGrade;

    // Use this for initialization
    void Start ()
    {
        PopulateCollages();
        PopulateUni();
        NamesList = GetComponent<NamesList>();
        NamesList.NameListResource();
        Ammount = 10;
        PopulatePhoto();
        PopulateMotherboard();
        PopulateFiles();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(AddNewPeople == true)
        {
            for (int i = 0; i <= Ammount; i++)
            {
                PopulateBasicInformation();
                PopulateDateOfBirth();
                PopulateBankInformation();
                PopulateAcademicInformation();
                Photo = Faces[Random.Range(0, 800)];
                TempGateway.Name = Name + "'s Gateway";
                PersonController.control.People.Add(new PeopleSystem(Name, PID, PhoneNumber, IPAddress, MaritalStatus, PersonalStatus, IQ, Photo, TempDOB, TempCollage1, TempUni1, TempBankDetails,TempGateway));
                PersonController.control.PeoplesName.Add(Name);
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
            PersonController.control.People.RemoveAt(PersonController.control.People.Count - 1);
            for(int i = 0; i < PersonController.control.People.Count;i++)
            {
                if(PersonController.control.People[i].Name != PersonController.control.People[i].BankDetails.AccountName)
                {
                    PersonController.control.People[i].BankDetails.AccountName = PersonController.control.People[i].Name;
                }
            }
        }
    }

    void PopulateBasicInformation()
    {
        PID = GetRandomNumber(8, 8);
        SelectedFirstName = Random.Range(0, NamesList.Names.Count - 1);
        SelectedLastName = Random.Range(0, NamesList.Names.Count - 1);

        Name = NamesList.Names[SelectedFirstName].Trim() + " " + NamesList.Names[SelectedLastName].Trim();
        PhoneNumber = "12345678";
        IPAddress = "127.0.0.1";
        IQ = Random.Range(0, 100);
    }

    void PopulateCollages()
    {
        //string[] CollageClasses = { "Math", "Science", "Technology", "Engineering" };
        // string[] CollageGrades = { "A", "B", "C", "D", "E" };

        PersonController.control.CollageClasses.Add("Math");
        PersonController.control.CollageClasses.Add("Science");
        PersonController.control.CollageClasses.Add("Technology");
        PersonController.control.CollageClasses.Add("Engineering");
        PersonController.control.CollageClasses.Add("");

        PersonController.control.CollageGrades.Add("A");
        PersonController.control.CollageGrades.Add("B");
        PersonController.control.CollageGrades.Add("C");
        PersonController.control.CollageGrades.Add("D");
        PersonController.control.CollageGrades.Add("E");
        PersonController.control.CollageGrades.Add("");
    }

    void PopulateUni()
    {
        PersonController.control.UniClasses.Add("Mathmatics");
        PersonController.control.UniClasses.Add("Biology");
        PersonController.control.UniClasses.Add("Physics");
        PersonController.control.UniClasses.Add("Chemistry");
        PersonController.control.UniClasses.Add("Robotics");
        PersonController.control.UniClasses.Add("");


        PersonController.control.UniGrades.Add("High Distiction");
        PersonController.control.UniGrades.Add("Distiction");
        PersonController.control.UniGrades.Add("Credit");
        PersonController.control.UniGrades.Add("Pass");
        PersonController.control.UniGrades.Add("Fail");
        PersonController.control.UniGrades.Add("");
    }

    void PopulateAcademicInformation()
    {
        PopulateUniInfo();
        PopulateCollageInfo();
    }

    void PopulateUniInfo()
    {
        SelectedClass = PersonController.control.UniClasses[Random.Range(0, PersonController.control.UniClasses.Count)];
        TempUni1.Qualifications = SelectedClass;

        SelectedGrade = PersonController.control.UniGrades[Random.Range(0, PersonController.control.UniGrades.Count)];
        if (SelectedClass == "")
        {
            SelectedGrade = "";
        }
        if(SelectedClass!= "" && SelectedGrade == "")
        {
            SelectedGrade = PersonController.control.UniGrades[Random.Range(0, PersonController.control.UniGrades.Count)];
        }
        if(SelectedClass != "" && SelectedGrade != "" || SelectedClass == "" && SelectedGrade == "")
        {
            TempUni1.Grade = SelectedGrade;
            TempUni1 = new UniversitySystem("", SelectedClass, SelectedGrade);

            SelectedClass = "";
            SelectedGrade = "";
        }
    }

    void PopulateCollageInfo()
    {
        SelectedClass = PersonController.control.CollageClasses[Random.Range(0, PersonController.control.CollageClasses.Count)];
        TempCollage1.Qualifications = SelectedClass;

        SelectedGrade = PersonController.control.CollageGrades[Random.Range(0, PersonController.control.CollageGrades.Count)];
        if (SelectedClass == "")
        {
            SelectedGrade = "";
        }
        TempCollage1.Grade = SelectedGrade;

        TempCollage1 = new CollageSystem("", SelectedClass, SelectedGrade);

        SelectedClass = "";
        SelectedGrade = "";
    }

    void PopulatePhoto()
    {
        for(int PhotoNumber = 0; PhotoNumber < 799; PhotoNumber++)
        {
            Faces.Add(Resources.Load<Texture2D>("Faces/" + PhotoNumber));
        }
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

        TempDOB = new DOBSystem(TempDOB.Day, TempDOB.Month, TempDOB.Year,TempDOB.Age);
    }

    void PopulateFiles()
    {
        TempGateway.SelectedOS.Name = OperatingSystems.OSName.AppatureOS;
        TempGateway.Files.Files.Add(new ProgramSystem("C:/", "System", "", "", "Gateway", "C:/", 0, 0, 0, 60, 100, 0, false, ProgramSystem.ProgramType.Dir));
        TempGateway.Files.Files.Add(new ProgramSystem("Downloads", "", "", "", "C:/", "C:/Downloads", 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
        TempGateway.Files.Files.Add(new ProgramSystem("Documents", "", "", "", "C:/", "C:/Documents", 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
        TempGateway.Files.Files.Add(new ProgramSystem("Program Files", "", "", "", "C:/", "C:/Programs", 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
        TempGateway.Files.Files.Add(new ProgramSystem("System Files", "", "", "", "C:/", "C:/System", 0, 0, 0, 0, 0, 0, false, ProgramSystem.ProgramType.Fdl));
        TempGateway.Files.Files.Add(new ProgramSystem("" + TempGateway.SelectedOS.Name, "", "", "", "C:/System", "", 0, 0, 10, 0, 100, 1, false, ProgramSystem.ProgramType.OS));
        //TempGateway.Add(new FileSystem(TempFiles, null,null));
        TempGateway.Motherboard.InstalledStorageDevice[0].UsedSpace += 60;
        TempGateway.Motherboard.InstalledStorageDevice[0].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[0].Capacity - GameControl.control.Gateway.InstalledStorageDevice[0].UsedSpace;
    }

    void PopulateMotherboard()
    {
        TempGateway.Motherboard.InstalledCPU.Add(new CPUSystem("Zion Z-14", "Zion", "Z-14", "140", 32, 1, 0.5f, 0.1f, 0.5f, 0, 0, 0, 0, 0, 1, 0.01f, 0, 100, 100, 0, "", 0, 20, 0.0025f, 0, 0, 0));
        TempGateway.Motherboard.InstalledGPU.Add(new GPUSystem("Qividia 970", "PCI-E", 970, 2, 0.1f, 2f, 256, 0, 0, 0, 1, 0.01f, 100, 100, 0, 120, 0.0025f));
        TempGateway.Motherboard.InstalledRAM.Add(new RamSystem("Vortex 2GB", "DDR1", 0, 2048, 0, 0, 100, 0.01f, 100, 100, 0, 0.0025f,0));
        TempGateway.Motherboard.InstalledStorageDevice.Add(new StorageDevice("EasternVirtual 128", "", "", "", 0.133f, 0, 128, 128, 15, 0.001f, 100, 100, 0, 0.0025f, 0.14f, StorageDevice.StorageType.HDD,0,0,0));
        TempGateway.Motherboard.InstalledPSU.Add(new PowerSupplySystem("Toughpower-2pack", "", 450, 0, 0, 0.01f, 100, 100, 0, 0.0025f));
        TempGateway.Motherboard.InstalledModem.Add(new ModemSystem("TUGs Basic Modem", "", "", "", 0.56f, 0.56f, 0.28f, 0, 15, 0.01f, 100, 100, 0, 0.0025f, 50, 25, ModemSystem.ModemConnectionType.DialUp));
    }

    void PopulateBankInformation()
    {
        TempBankDetails.AccountName = Name;
        TempBankDetails.AccountNumber = GetRandomNumber(9,9);
        TempBankDetails.AccountPass = GetRandomString(6,12);
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

        //TempFiles.RemoveRange(0, TempFiles.Count);
        //TempGateway = null;
        //TempMotherboard = null;
    }

    public string GetRandomString(int min, int max)
    {
        int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
        string retMe = "";
        for (int i = 0; i < charAmount; i++)
        {
            retMe += glyphs[Random.Range(0, glyphs.Length)];
            //            for(int l=0; l<SiteAdminPass.Length; l++)
            //            {
            //                retMe += glyphs[Random.Range(0, glyphs.Length)];
            //            }
        }
        return retMe;
    }

    public string GetRandomNumber(int min, int max)
    {
        int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
        string retMe = "";
        for (int i = 0; i < charAmount; i++)
        {
            retMe += AccNo[Random.Range(0, AccNo.Length)];
            //            for(int l=0; l<SiteAdminPass.Length; l++)
            //            {
            //                retMe += AccNo[Random.Range(0, AccNo.Length)];
            //            }
        }
        return retMe;
    }
}
