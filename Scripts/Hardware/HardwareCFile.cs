using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareCFile : MonoBehaviour {

    public List<MotherboardSystem> ListOfMotherboards = new List<MotherboardSystem>();

    public List<CPUSystem> ListOfCPU = new List<CPUSystem>();

    public List<GPUSystem> ListOfGPU = new List<GPUSystem>();

    public List<RamSystem> ListOfMemory = new List<RamSystem>();

    public List<StorageDevice> ListOfStorage = new List<StorageDevice>();

    public List<PowerSupplySystem> ListOfPSU = new List<PowerSupplySystem>();

    public List<ModemSystem> ListOfModems = new List<ModemSystem>();

    public List<DiskPartSystem> Partitions = new List<DiskPartSystem>();

    //IMAGES
    public List<string> ListOfFilePaths = new List<string>();
    public List<Texture2D> ListOfCPUImages = new List<Texture2D>();
    public List<Texture2D> ListOfMotherboardImages = new List<Texture2D>();
    public List<Texture2D> ListOfStorageImages = new List<Texture2D>();
    public List<Texture2D> ListOfMemoryImages = new List<Texture2D>();


    //WebCog
    //

    // Use this for initialization
    void Start ()
    {
        AddFilePaths();

    }

    void AddFilePaths()
    {
        ListOfFilePaths.Add("Hardware/Motherboards/Gateway00");
        ListOfFilePaths.Add("Hardware/CPUs/blank_cpu_1");
        ListOfFilePaths.Add("Hardware/Memory/memory");
        ListOfFilePaths.Add("Hardware/Memory/memory_plugged");
        ListOfFilePaths.Add("Hardware/Memory/memory_2");
        ListOfFilePaths.Add("Hardware/Memory/memory_2_plugged");
        ListOfFilePaths.Add("Hardware/Memory/memory_3");
        ListOfFilePaths.Add("Hardware/Storage/storage");
        ListOfFilePaths.Add("Hardware/Storage/storage_plugged");
        ListOfFilePaths.Add("Hardware/Storage/storage1");
        ListOfFilePaths.Add("Hardware/Storage/storage2");
        ListOfFilePaths.Add("Hardware/Storage/storage3");
        ListOfFilePaths.Add("Hardware/Storage/storage4");

        LoadPics();
    }

    void LoadPics()
    {
        Texture2D LoadedImage;
        for (int i =0; i < ListOfFilePaths.Count; i++)
        {
            LoadedImage = Resources.Load<Texture2D>(ListOfFilePaths[i]);
            if (ListOfFilePaths[i].Contains("Motherboards"))
            {
                ListOfMotherboardImages.Add(LoadedImage);
            }
            if (ListOfFilePaths[i].Contains("CPUs"))
            {
                ListOfCPUImages.Add(LoadedImage);
            }
            if (ListOfFilePaths[i].Contains("Memory"))
            {
                ListOfMemoryImages.Add(LoadedImage);
            }
            if (ListOfFilePaths[i].Contains("Storage"))
            {
                ListOfStorageImages.Add(LoadedImage);
            }
        }

        UpdateLists();
    }

    void UpdateLists()
    {
        Motherboard();
        CPU();
        GPU();
        RAM();
        PSU();
        Storage();
        Modems();
    }


    void Motherboard()
    {
        string ManufactorName = "";
        string ProductName = "";
        string Name = "";
        string Socket = ""; // This is used to determine what motherboard it fits in.
        int Size = 0; // This is used for flavour text
        int Cores = 0; // This is for multitasking
        float MaxSpeed = 0; // This is the maxspeed defined by default or by overclocking or underclocking
        float MinSpeed = 0; // Minium requirement of speed before the system crashes
        float FactoryMaxSpeed = 0; // What the max speed is by default
        float Voltage = 0; // Overclocking Multi
        float DegredationRate = 0f; // The rate that health is lost
        float IdlePowerDraw = 0; // What the power draw is at idle
        float PowerEffRating = 0; // How effecient the power is at performance
        int UIPosX = 0;
        int UIPosY = 0;
        int Image = 0;
        int MaxCPUSockets = 0;
        int MaxStorageSlots = 0;
        int MaxMemorySlots = 0;

        ManufactorName = "ZettaByte";
        string BrandName = "BASIC";
        string Chipset = "B100";
        ProductName = "";
        Socket = "100";
        string Description = "";
        Image = 0;
        MaxCPUSockets = 1;
        MaxStorageSlots = 2;
        MaxMemorySlots = 1;
        int MaxExpansionSlots = 1;
        int MaxInstalledMemory = 8;

        HealthStatSystem Health = new HealthStatSystem(0.00001f,0,100,100,100,100,0,1);

        ListOfMotherboards.Add(new MotherboardSystem());
    }

    void CPU()
    {
        string ManufactorName = "";
        string ProductName = "";
        string Name = "";
        string Socket = ""; // This is used to determine what motherboard it fits in.
        int Size = 0; // This is used for flavour text
        int Cores = 0; // This is for multitasking
        float MaxSpeed = 0; // This is the maxspeed defined by default or by overclocking or underclocking
        float MinSpeed = 0; // Minium requirement of speed before the system crashes
        float FactoryMaxSpeed = 0; // What the max speed is by default
        float Voltage = 0; // Overclocking Multi
        float DegredationRate = 0f; // The rate that health is lost
        float MaxHealth = 0;
        float Health = MaxHealth;
        float IdlePowerDraw = 0; // What the power draw is at idle
        float PowerEffRating = 0; // How effecient the power is at performance
        int UIPosX = 0;
        int UIPosY = 0;
        int Image = 0;

        ManufactorName = "Zion";
        ProductName = "Z-14";
        Name = ManufactorName + " " + ProductName;
        Socket = "100";
        Size = 32;
        Cores = 1;
        MaxSpeed = 0.5f;
        MinSpeed = 0.1f;
        FactoryMaxSpeed = 0.5f;
        Voltage = 1;
        DegredationRate = 0.01f;
        MaxHealth = 100;
        Health = MaxHealth;
        IdlePowerDraw = 20;
        PowerEffRating = 0.00025f;
        UIPosX = 0;
        UIPosY = 0;
        Image = 0;

        ListOfCPU.Add(new CPUSystem(Name, ManufactorName, ProductName, Socket, Size, Cores, MaxSpeed, MinSpeed, FactoryMaxSpeed, 0, 0, 0, 0, 0, Voltage, DegredationRate, 0, MaxHealth, Health, 0, "", 0, IdlePowerDraw, PowerEffRating,UIPosX,UIPosY,Image));
    }

    void GPU()
    {
        string ManufactorName = "";
        string ProductName = "";
        string Name = "";
        string Socket = ""; // This is used to determine what motherboard it fits in.
        int Cores = 0; // This is for multitasking
        float MaxSpeed = 0; // This is the maxspeed defined by default or by overclocking or underclocking
        float MinSpeed = 0; // Minium requirement of speed before the system crashes
        float FactoryMaxSpeed = 0; // What the max speed is by default
        float MaxMemory = 0; // What the max memory is by default
        float Voltage = 0; // Overclocking Multi
        float DegredationRate = 0f; // The rate that health is lost
        float MaxHealth = 0;
        float Health = MaxHealth;
        float IdlePowerDraw = 0; // What the power draw is at idle
        float PowerEffRating = 0; // How effecient the power is at performance

        ManufactorName = "Qividia";
        ProductName = "970";
        Name = ManufactorName + " " + ProductName;
        Socket = "PCI";
        Cores = 970;
        MaxSpeed = 0.5f;
        MinSpeed = 0.1f;
        FactoryMaxSpeed = 0.5f;
        MaxMemory = 256;
        Voltage = 1;
        DegredationRate = 0.01f;
        MaxHealth = 100;
        Health = MaxHealth;
        IdlePowerDraw = 20;
        PowerEffRating = 0.00025f;

        ListOfGPU.Add(new GPUSystem(Name, Socket, Cores, MaxSpeed, MinSpeed, FactoryMaxSpeed, MaxMemory, 0, 0, 0, Voltage, DegredationRate, MaxHealth, Health, 0, IdlePowerDraw, PowerEffRating));
    }

    void RAM()
    {
        string ManufactorName = "";
        string ProductName = "";
        string MemorySizeName = "";
        string Socket = "";
        string Name = "";
        float MaxSpeed = 0; // This is the maxspeed defined by default or by overclocking or underclocking
        float MinSpeed = 0; // Minium requirement of speed before the system crashes
        float FactoryMaxSpeed = 0; // What the max speed is by default
        float MaxMemory = 0; // What the max memory is by default
        float Voltage = 0; // Overclocking Multi
        float DegredationRate = 0f; // The rate that health is lost
        float MaxHealth = 0;
        float Health = MaxHealth;
        float IdlePowerDraw = 0; // What the power draw is at idle
        float PowerEffRating = 0; // How effecient the power is at performance
        int SelectedImage = 0;

        ManufactorName = "Vortex";
        ProductName = "Basic ";
        Socket = "DDR1";
        MaxMemory = 128;
        MemorySizeName = MaxMemory + "MB";
        Name = ManufactorName + " " + ProductName + " " + Socket + " " + MemorySizeName;
        MaxSpeed = 0.1f;
        MinSpeed = 0.08f;
        FactoryMaxSpeed = 0.1f;
        Voltage = 1;
        DegredationRate = 0.01f;
        MaxHealth = 100;
        Health = MaxHealth;
        IdlePowerDraw = 20;
        PowerEffRating = 0.00025f;
        SelectedImage = 0;

        ListOfMemory.Add(new RamSystem(Name, Socket, IdlePowerDraw, MaxMemory, 0, 0, FactoryMaxSpeed, DegredationRate, MaxHealth, Health, 0, PowerEffRating, SelectedImage));

        ManufactorName = "Vortex";
        ProductName = "Basic+ ";
        Socket = "DDR1";
        MaxMemory = 256;
        MemorySizeName = MaxMemory + "MB";
        Name = ManufactorName + " " + ProductName + " " + Socket + " " + MemorySizeName;
        MaxSpeed = 0.2f;
        MinSpeed = 0.08f;
        FactoryMaxSpeed = 0.2f;
        Voltage = 1;
        DegredationRate = 0.01f;
        MaxHealth = 100;
        Health = MaxHealth;
        IdlePowerDraw = 20;
        PowerEffRating = 0.00025f;
        SelectedImage = 0;

        ListOfMemory.Add(new RamSystem(Name, Socket, IdlePowerDraw, MaxMemory, 0, 0, FactoryMaxSpeed, DegredationRate, MaxHealth, Health, 0, PowerEffRating, SelectedImage));
    }

    void Storage()
    {
        string ManufactorName = "";
        string ProductName = "";
        string MemorySizeName = "";
        string Connector = "";
        string Name = "";
        string Desc = "";
        long Speed = 0; // Speed at which local files transffer.
        long MaxMemory = 0; // What the max memory is by default
        float DegredationRate = 0f; // The rate that health is lost
        float MaxHealth = 0;
        float Health = MaxHealth;
        float PowerDraw = 0; // What the power draw is at idle
        float PowerEffRating = 0; // How effecient the power is at performance
        float BootTime = 0;
        float Timer = 0;
        float InitalTimer = 0;
        int UIPosX = 0;
        int UIPosY = 0;
        int Image = 0;

        ManufactorName = "EasternVirtual";
        ProductName = "Green";
        Connector = "SATA";
        MaxMemory = 128;
        MemorySizeName = MaxMemory + "";
        Name = ManufactorName + " " + ProductName + " " + Connector + " " + MemorySizeName;
        Desc = "";
        Speed = 3325;
        DegredationRate = 0.001f;
        MaxHealth = 100;
        Health = MaxHealth;
        PowerDraw = 5;
        PowerEffRating = 0.0025f;
        BootTime = 0.14f;
        InitalTimer = 1;
        Timer = InitalTimer;
        UIPosX = 0;
        UIPosY = 0;
        Image = 0;

        ListOfStorage.Add(new StorageDevice(Name, ManufactorName, Desc, Connector, Speed, 0, MaxMemory, MaxMemory, PowerDraw, DegredationRate, MaxHealth, Health, 0, PowerEffRating, BootTime, StorageDevice.StorageType.HDD));

        ManufactorName = "EasternVirtual";
        ProductName = "Red";
        Connector = "SATA";
        MaxMemory = 128;
        MemorySizeName = MaxMemory + "";
        Name = ManufactorName + " " + ProductName + " " + Connector + " " + MemorySizeName;
        Desc = "";
        Speed = 133;
        DegredationRate = 0.001f;
        MaxHealth = 100;
        Health = MaxHealth;
        PowerDraw = 15;
        PowerEffRating = 0.0025f;
        BootTime = 0.11f;
        InitalTimer = 1;
        Timer = InitalTimer;
        UIPosX = 0;
        UIPosY = 0;
        Image = 0;
        ListOfStorage.Add(new StorageDevice(Name, ManufactorName, Desc, Connector, Speed, 0, MaxMemory, MaxMemory, PowerDraw, DegredationRate, MaxHealth, Health, 0, PowerEffRating, BootTime, StorageDevice.StorageType.HDD));
    }

    void PSU()
    {
        string ManufactorName = "";
        string ProductName = "";
        string OutputName = "";
        string Name = "";
        float MaxOutput = 0; // What the max output is by default
        float DegredationRate = 0f; // The rate that health is lost
        float MaxHealth = 0;
        float Health = MaxHealth;
        float PowerEffRating = 0; // How effecient the power is at performance

        ManufactorName = "Toughpower";
        ProductName = "2pack";
        MaxOutput = 450;
        OutputName = MaxOutput + "W";
        Name = ManufactorName + " " + ProductName + " " + OutputName;
        DegredationRate = 0.01f;
        MaxHealth = 100;
        Health = MaxHealth;
        PowerEffRating = 0.0025f;

        ListOfPSU.Add(new PowerSupplySystem(Name, "", MaxOutput, 0, 0, DegredationRate, MaxHealth, Health, 0, PowerEffRating));

        ManufactorName = "Toughpower";
        ProductName = "4pack";
        MaxOutput = 550;
        OutputName = MaxOutput + "W";
        Name = ManufactorName + " " + ProductName + " " + OutputName;
        DegredationRate = 0.01f;
        MaxHealth = 100;
        Health = MaxHealth;
        PowerEffRating = 0.0025f;

        ListOfPSU.Add(new PowerSupplySystem(Name, "", MaxOutput, 0, 0, DegredationRate, MaxHealth, Health, 0, PowerEffRating));
    }

    void Modems()
    {
        string ManufactorName = "";
        string ProductName = "";
        string OutputName = "";
        string Name = "";
        float MaxSpeed = 0; // What the max speed is by default
        float MinSpeed = 0; // What the min speed is by default
        float DegredationRate = 0f; // The rate that health is lost
        float MaxHealth = 0;
        float Health = MaxHealth;
        float PowerUsage = 0;
        float PowerEffRating = 0; // How effecient the power is at performance

        ManufactorName = "TUGs";
        ProductName = "Basic Modem";
        MaxSpeed = 0.056f;
        MinSpeed = 0.029f;
        OutputName = MaxSpeed + "";
        Name = ManufactorName + " " + ProductName + " " + OutputName;
        DegredationRate = 0.01f;
        MaxHealth = 100;
        Health = MaxHealth;
        PowerUsage = 15;
        PowerEffRating = 0.0025f;

        ListOfModems.Add(new ModemSystem(Name, ManufactorName, "", "", MaxSpeed, MaxSpeed, MaxSpeed, MinSpeed, 0, PowerUsage, DegredationRate, MaxHealth, Health, 0, PowerEffRating, 0, 0,"", ModemSystem.ModemConnectionType.DialUp));

        ManufactorName = "TUGs";
        ProductName = "Advanced Modem";
        MaxSpeed = 0.256f;
        MinSpeed = 0.056f;
        OutputName = MaxSpeed + "";
        Name = ManufactorName + " " + ProductName + " " + OutputName;
        DegredationRate = 0.01f;
        MaxHealth = 100;
        Health = MaxHealth;
        PowerUsage = 15;
        PowerEffRating = 0.0025f;

        ListOfModems.Add(new ModemSystem(Name, ManufactorName, "", "", MaxSpeed, MaxSpeed, MaxSpeed, MinSpeed, 0, PowerUsage, DegredationRate, MaxHealth, Health, 0, PowerEffRating, 0, 0,"", ModemSystem.ModemConnectionType.DialUp));
    }
}
