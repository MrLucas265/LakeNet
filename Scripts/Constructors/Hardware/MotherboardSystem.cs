using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotherboardSystem
{
    //Idenity of the board
    public string Manufacture;
    public string Brand;
    public string Chipset;
    public string Socket;
    public string ProductName;
    public string Description;

    //The stats of the board
    public int SelectedMotherboardImage;
    public int MaxCPUSockets;
    public int MaxStorageSlots;
    public int MaxMemorySlots;
    public int MaxExpansionSlots;
    public int MaxInstalledMemory;

    //The slot positions for the pics
    public List<SocketSystem> ExpansionSockets = new List<SocketSystem>();
    public List<SocketSystem> MemorySockets = new List<SocketSystem>();
    public List<SocketSystem> StorageSockets = new List<SocketSystem>();
    public List<SocketSystem> CPUSockets = new List<SocketSystem>();

    //What compoents are installed
    public List<RamSystem> InstalledRAM = new List<RamSystem>();
    public List<CPUSystem> InstalledCPU = new List<CPUSystem>();
    public List<StorageDevice> InstalledStorageDevice = new List<StorageDevice>();
    public List<PowerSupplySystem> InstalledPSU = new List<PowerSupplySystem>();
    public List<GPUSystem> InstalledGPU = new List<GPUSystem>();
    public List<ModemSystem> InstalledModem = new List<ModemSystem>();

    //What Slots are avaible
    public List<ConnectorSystem> ExpansionSlots = new List<ConnectorSystem>();
    public List<ConnectorSystem> StoragePorts = new List<ConnectorSystem>();

    public BandwidthSystem Bandwidth;
    public HealthStatSystem Health;
    public ValueSystem Value;

    public GatewayStatusSystem Status;

    public MotherboardSystem (string manufacture,string brand, string chipset,string socket,string productname,string description,int selectedmotherboardimage,int maxcpusockets,int maxstorageslots,int maxmemoryslots,int maxexpansionslots,int maxinstalledmemory, List<SocketSystem> expansionsockets, List<SocketSystem> memorysockets, List<SocketSystem> storagesockets, List<SocketSystem> cpusockets, List<RamSystem> installedram, List<CPUSystem> installedcpu, List<StorageDevice> installedstoragedevice, List<PowerSupplySystem> installedpsu, List<GPUSystem> installedgpu, List<ModemSystem> installedmodem, List<ConnectorSystem> expansionslots, List<ConnectorSystem> storageports,BandwidthSystem bandwidth,ValueSystem value,HealthStatSystem health, GatewayStatusSystem status) //,Texture2D icon)
	{
        Manufacture = manufacture;
        Brand = brand;
        Chipset = chipset;
        Socket = socket;
        ProductName = productname;
        Description = description;

        ExpansionSockets = expansionsockets;
        MemorySockets = memorysockets;
        StorageSockets = storagesockets;
        CPUSockets = cpusockets;
        MaxInstalledMemory = maxinstalledmemory;

        SelectedMotherboardImage = selectedmotherboardimage;
        MaxCPUSockets = maxcpusockets;
        MaxStorageSlots = maxstorageslots;
        MaxMemorySlots = maxmemoryslots;
        MaxExpansionSlots = maxexpansionslots;

        InstalledRAM = installedram;
        InstalledCPU = installedcpu;
        InstalledStorageDevice = installedstoragedevice;
        InstalledPSU = installedpsu;
        InstalledGPU = installedgpu;
        InstalledModem = installedmodem;

        ExpansionSlots = expansionslots;
        StoragePorts = storageports;
        Bandwidth = bandwidth; 
        Value = value;
        Health = health;

        Status = status;
    }
}
