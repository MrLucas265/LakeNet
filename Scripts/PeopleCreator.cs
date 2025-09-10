//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PeopleCreator : MonoBehaviour
//{
//    public string Name;
//    public string PID;
//    public string PhoneNumber;
//    public string IPAddress;
//    public string MaritalStatus;
//    public string PersonalStatus;
//    public int IQ;
//    public BankSystem TempBankDetails;
//    public MotherboardSystem TempGatewayDetails;
//    public DOBSystem TempDOB;
//    public GatewaySystem TempGateway;
//    public GatewaySystem BlankGateway;
//    public MotherboardSystem TempMotherboard;
//    public List<ProgramSystem> TempFiles = new List<ProgramSystem>();
//    public List<Texture2D> Faces = new List<Texture2D>();

//    public List<DiskPartSystem> BlankDrivePat = new List<DiskPartSystem>();

//    public List<string> CollageClasses = new List<string>();
//    public List<string> CollageGrades = new List<string>();

//    public List<string> UniClasses = new List<string>();
//    public List<string> UniGrades = new List<string>();

//    public List<CollageSystem> TempCollage = new List<CollageSystem>();
//    public List<UniversitySystem> TempUni = new List<UniversitySystem>();

//    public CollageSystem TempCollage1;
//    public UniversitySystem TempUni1;

//    public int Ammount;

//    public bool AddNewPeople;
//    public bool WipeData;

//    public int Count;

//    public NamesList NamesList;

//    public int SelectedFirstName;
//    public int SelectedLastName;

//    public string SelectedClass;
//    public string SelectedGrade;

//    public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
//    public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();
//    public List<string> ListOfFilePaths = new List<string>();

//    public bool UpdateGatewayInfo;

//    // Use this for initialization
//    void Start()
//    {
//        PopulateCollages();
//        PopulateUni();
//        NamesList = GetComponent<NamesList>();
//        NamesList.NameListResource();
//        Ammount = 2;
//        PopulatePhoto();
//    }

//    // Update is called once per frame
//    void Update ()
//    {
//        if(UpdateGatewayInfo)
//        {
//            PopulateMotherboard();
//        }
//        if(PersonController.control.People.Count <= 0)
//        {
//            AddNewPeople = true;
//        }
//		if(AddNewPeople)
//        {
//            for (int i = 0; i <= Ammount; i++)
//            {
//                PopulateBasicInformation();
//                PopulateDateOfBirth();
//                PopulateBankInformation();
//                PopulateAcademicInformation();
//                int Photo = Random.Range(0, 800);
//                PersonController.control.People.Add(new PeopleSystem(Name, PID, PhoneNumber, IPAddress, MaritalStatus, PersonalStatus, IQ, Photo));
//                PersonController.control.PeoplesName.Add(Name);
//                Count = i;
//                ResetAllInformation();
//            }

//        }

//        if(Count == Ammount)
//        {
//            UpdateGatewayInfo = true;
//        }

//        if (WipeData == true)
//        {
//            WipeDatabase();
//        }


//        if (Count >= Ammount)
//        {
//            AddNewPeople = false;
//            Count = 0;
//            PersonController.control.People.RemoveAt(PersonController.control.People.Count - 1);
//            for(int i = 0; i < PersonController.control.People.Count;i++)
//            {
//                //if(PersonController.control.People[i].Name != PersonController.control.People[i].BankDetails.AccountName)
//                //{
//                //    PersonController.control.People[i].BankDetails.AccountName = PersonController.control.People[i].Name;
//                //}
//            }
//        }
//    }

//    void PopulateBasicInformation()
//    {
//        PID = StringGenerator.RandomNumberChar(8, 8);
//        SelectedFirstName = Random.Range(0, NamesList.Names.Count - 1);
//        SelectedLastName = Random.Range(0, NamesList.Names.Count - 1);

//        Name = NamesList.Names[SelectedFirstName].Trim() + " " + NamesList.Names[SelectedLastName].Trim();
//        PhoneNumber = "12345678";
//        IPAddress = "127.0.0.1";
//        IQ = Random.Range(0, 100);
//    }

//    void PopulateCollages()
//    {
//        //string[] CollageClasses = { "Math", "Science", "Technology", "Engineering" };
//        // string[] CollageGrades = { "A", "B", "C", "D", "E" };

//        PersonController.control.CollageClasses.Add("Math");
//        PersonController.control.CollageClasses.Add("Science");
//        PersonController.control.CollageClasses.Add("Technology");
//        PersonController.control.CollageClasses.Add("Engineering");
//        PersonController.control.CollageClasses.Add("");

//        PersonController.control.CollageGrades.Add("A");
//        PersonController.control.CollageGrades.Add("B");
//        PersonController.control.CollageGrades.Add("C");
//        PersonController.control.CollageGrades.Add("D");
//        PersonController.control.CollageGrades.Add("E");
//        PersonController.control.CollageGrades.Add("");
//    }

//    void PopulateUni()
//    {
//        PersonController.control.UniClasses.Add("Mathmatics");
//        PersonController.control.UniClasses.Add("Biology");
//        PersonController.control.UniClasses.Add("Physics");
//        PersonController.control.UniClasses.Add("Chemistry");
//        PersonController.control.UniClasses.Add("Robotics");
//        PersonController.control.UniClasses.Add("");


//        PersonController.control.UniGrades.Add("High Distiction");
//        PersonController.control.UniGrades.Add("Distiction");
//        PersonController.control.UniGrades.Add("Credit");
//        PersonController.control.UniGrades.Add("Pass");
//        PersonController.control.UniGrades.Add("Fail");
//        PersonController.control.UniGrades.Add("");
//    }

//    void PopulateAcademicInformation()
//    {
//        PopulateUniInfo();
//        PopulateCollageInfo();
//    }

//    void PopulateUniInfo()
//    {
//        SelectedClass = PersonController.control.UniClasses[Random.Range(0, PersonController.control.UniClasses.Count)];
//        TempUni1.Qualifications = SelectedClass;

//        SelectedGrade = PersonController.control.UniGrades[Random.Range(0, PersonController.control.UniGrades.Count)];
//        if (SelectedClass == "")
//        {
//            SelectedGrade = "";
//        }
//        if(SelectedClass!= "" && SelectedGrade == "")
//        {
//            SelectedGrade = PersonController.control.UniGrades[Random.Range(0, PersonController.control.UniGrades.Count)];
//        }
//        if(SelectedClass != "" && SelectedGrade != "" || SelectedClass == "" && SelectedGrade == "")
//        {
//            TempUni1.Grade = SelectedGrade;
//            TempUni1 = new UniversitySystem("", SelectedClass, SelectedGrade);

//            SelectedClass = "";
//            SelectedGrade = "";
//        }
//    }

//    void PopulateCollageInfo()
//    {
//        SelectedClass = PersonController.control.CollageClasses[Random.Range(0, PersonController.control.CollageClasses.Count)];
//        TempCollage1.Qualifications = SelectedClass;

//        SelectedGrade = PersonController.control.CollageGrades[Random.Range(0, PersonController.control.CollageGrades.Count)];
//        if (SelectedClass == "")
//        {
//            SelectedGrade = "";
//        }
//        TempCollage1.Grade = SelectedGrade;

//        TempCollage1 = new CollageSystem("", SelectedClass, SelectedGrade);

//        SelectedClass = "";
//        SelectedGrade = "";
//    }

//    void PopulatePhoto()
//    {
//        for(int PhotoNumber = 0; PhotoNumber < 799; PhotoNumber++)
//        {
//            Faces.Add(Resources.Load<Texture2D>("Faces/" + PhotoNumber));
//        }
//    }

//    void PopulateDateOfBirth()
//    {
//        TempDOB.Year = Random.Range(1900, 1980);

//        TempDOB.Month = Random.Range(1, 13);

//        if (TempDOB.Month != 2)
//        {
//            if (TempDOB.Month == 4 || TempDOB.Month == 6 || TempDOB.Month == 9 || TempDOB.Month == 11)
//            {
//                TempDOB.Day = Random.Range(1, 31);
//            }
//            else
//            {
//                TempDOB.Day = Random.Range(1, 32);
//            }
//        }
//        else
//        {
//            TempDOB.Day = Random.Range(1, 27);
//        }

//        TempDOB.Age = 1980 - TempDOB.Year;

//        TempDOB = new DOBSystem(TempDOB.Day, TempDOB.Month, TempDOB.Year,TempDOB.Age);
//    }

//    void PopulateDrives(int i)
//    {
//        //for(int j = 0; j < PersonController.control.People[i].Gateway.Motherboard.StorageSlots.Count; j++)
//        //{
//        //    PersonController.control.People[i].Gateway.Motherboard.StorageSlots[j].Devices[0].Partitions = new List<DiskPartSystem>();
//        //    PersonController.control.People[i].Gateway.Motherboard.StorageSlots[j].Devices[0].Partitions.Add(new DiskPartSystem("System", "C", 128, 0, 0, 0));
//        //    PopulateFiles(i, j);
//        //}
//    }

//    void PopulateFiles(int i, int j)
//    {
//        //for (int k = 0; k < PersonController.control.People[i].Gateway.Motherboard.StorageSlots[j].Devices[0].Partitions.Count; k++)
//        //{
//        //    if (PersonController.control.People[i].Gateway.Motherboard.StorageSlots[j].Devices[0].Partitions[k].DriveLetter == "C")
//        //    {
//        //        //for (int l = 0; l < PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.OSList.Count; l++)
//        //        //{
//        //        //    PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.OSList[l].FPC.BackgroundAddress = "";
//        //        //}
//        //        //PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.FileList.Add(new ProgramSystem("C:/", "System", "", "", "", "", "Gateway", "C:/", "", "", ProgramSystem.FileExtension.Dir, ProgramSystem.FileExtension.Null, 0, 0, 60, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
//        //        //PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.FileList.Add(new ProgramSystem("Downloads", "", "", "", "", "", "C:/", "C:/Downloads", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
//        //        //PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.FileList.Add(new ProgramSystem("Documents", "", "", "", "", "", "C:/", "C:/Documents", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
//        //        //PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.FileList.Add(new ProgramSystem("Programs", "", "", "", "", "", "C:/", "C:/Programs", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
//        //        //PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.FileList.Add(new ProgramSystem("System", "", "", "", "", "", "C:/", "C:/System", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
//        //        //PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.FileList.Add(new ProgramSystem("" + TempGateway.SelectedOS.Name, "", "", "", "", "", "C:/System", "", "", "", ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null, 0, 0, 10, 0, 0, 0, 0, 100, 1, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
//        //    }

//        //    PersonController.control.People[i].Gateway.Motherboard.StorageSlots[j].Devices[0].UsedSpace += 60;
//        //    PersonController.control.People[i].Gateway.Motherboard.StorageSlots[j].Devices[0].FreeSpace = PersonController.control.People[i].Gateway.Motherboard.StorageSlots[j].Devices[0].Capacity - PersonController.control.People[i].Gateway.Motherboard.StorageSlots[j].Devices[0].UsedSpace;
//        //}

//        //for (int j = 0; j < TempGateway.Motherboard.InstalledStorageDevice.Count; j++)
//        //{
//        //    for (int k = 0; k < TempGateway.Motherboard.InstalledStorageDevice[j].Partitions.Count; k++)
//        //    {
//        //        if(TempGateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.FileList.Count == 0)
//        //        {
//        //            for (int l = 0; l < BlankFiles.FileList.Count; l++)
//        //            {
//        //                TempGateway.Motherboard.InstalledStorageDevice[j].Partitions[k].Files.FileList.Add(BlankFiles.FileList[l]);
//        //            }
//        //        }
//        //    }
//        //}

//        UpdateGatewayInfo = false;
//    }

//    void PopulateMotherboard()
//    {
//        string IPAddress = StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3);
//        if(PersonController.control.People.Count > 0)
//        {
//            for (int i = 0; i < PersonController.control.People.Count; i++)
//            {
//                PersonController.control.People[i].Gateway.Name = PersonController.control.People[i].Name + "'s Gateway";
//                PersonController.control.People[i].Gateway.CPU.Add(new CPUSystem("Zion Z-14", "Zion", "Z-14", "140", 32, 1, 0.5f, 0.1f, 0.5f, 0, 0, 0, 0, 0, 1, 0.01f, 0, 100, 100, 0, "", 0, 20, 0.0025f, 0, 0, 0));
//                PersonController.control.People[i].Gateway.GPU.Add(new GPUSystem("Qividia 970", "PCI-E", 970, 2, 0.1f, 2f, 256, 0, 0, 0, 1, 0.01f, 100, 100, 0, 120, 0.0025f));
//                PersonController.control.People[i].Gateway.RAM.Add(new RamSystem("Vortex 2GB", "DDR1", 0, 2048, 0, 0, 100, 0.01f, 100, 100, 0, 0.0025f, 0));
//                PersonController.control.People[i].Gateway.StorageDevices.Add(new StorageDevice("EasternVirtual 128", "", "", "", 133, 0, 128, 128, 15, 0.001f, 100, 100, 0, 0.0025f, 0.14f, StorageDevice.StorageType.HDD));
//                PersonController.control.People[i].Gateway.PSU.Add(new PowerSupplySystem("Toughpower-2pack", "", 450, 0, 0, 0.01f, 100, 100, 0, 0.0025f));
//                PersonController.control.People[i].Gateway.Modem.Add(new ModemSystem("TUGs Basic Modem", "", "", "", 0.56f, 0.56f, 0.56f, 0.28f, 0, 15, 0.01f, 100, 100, 0, 0.0025f, 50, 25, IPAddress, ModemSystem.ModemConnectionType.DialUp));

//                PopulateDrives(i);
//            }
//        }
//    }

//    void PopulateBankInformation()
//    {
//        //TempBankDetails.AccountName = Name;
//        //TempBankDetails.AccountNumber = StringGenerator.RandomNumberChar(9,9);
//        //TempBankDetails.AccountPass = StringGenerator.RandomMixedChar(6,12);
//        //TempBankDetails.BankIP = "127.0.0.1";
//        //TempBankDetails.BankName = "LEC Bank";
//        //TempBankDetails.CreditRating = 1;
//        //TempBankDetails.Loan = 0;
//        //TempBankDetails.LoanIntrest = 1;
//        //TempBankDetails.AccountIntrest = Random.Range(0, 5);
//        //TempBankDetails.AccountBalance = Random.Range(0, 50000);
//        //TempBankDetails = new BankSystem(TempBankDetails.AccountName, TempBankDetails.AccountNumber, TempBankDetails.AccountPass, TempBankDetails.BankIP, TempBankDetails.BankName,
//        //    TempBankDetails.CreditRating, TempBankDetails.Loan, TempBankDetails.LoanIntrest, TempBankDetails.AccountIntrest, TempBankDetails.AccountBalance);
//    }

//    void PopulateGatewayInformation()
//    {
//        //TempGatewayDetails.InstalledCPU.;
//    }

//    void WipeDatabase()
//    {
//        PersonController.control.People.RemoveRange(0, PersonController.control.People.Count);
//        WipeData = false;
//    }

//    void ResetAllInformation()
//    {
//        Name = "";
//        PID = "";
//        PhoneNumber = "";
//        IPAddress = "";

//        //TempBankDetails.AccountName = "";
//        //TempBankDetails.AccountNumber = "";
//        //TempBankDetails.AccountPass = "";
//        //TempBankDetails.BankIP = "";
//        //TempBankDetails.BankName = "";
//        //TempBankDetails.CreditRating = 0;
//        //TempBankDetails.Loan = 0;
//        //TempBankDetails.LoanIntrest = 0;
//        //TempBankDetails.AccountIntrest = 0;
//        //TempBankDetails.AccountBalance = 0;

//        //TempAcademicDetails1.CollageQualifications.Clear();
//        //TempAcademicDetails1.UniversityQualifications.Clear();
//        //TempAcademicDetails.OtherQualifications = "";

//        //TempFiles.RemoveRange(0, TempFiles.Count);
//        //TempGateway = null;
//        //TempMotherboard = null;
//    }
//}
