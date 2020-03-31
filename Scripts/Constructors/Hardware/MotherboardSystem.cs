using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotherboardSystem
{
	public string Name;
	public string Description;
	public string StorageType;
	public int MaxPower;
	public int PowerSupply;
	public float Cost;
	public float Health;
    public int SelectedMotherboardImage;
    public int MaxCPUSockets;
    public int MaxStorageSlots;
    public int MaxMemorySlots;
    public List<SocketSystem> GPUSlots = new List<SocketSystem>();
    public List<SocketSystem> MemorySlots = new List<SocketSystem>();
    public List<SocketSystem> StorageSlots = new List<SocketSystem>();
    public List<SocketSystem> CPUSockets = new List<SocketSystem>();
    public List<RamSystem> InstalledRAM = new List<RamSystem>();
    public List<CPUSystem> InstalledCPU = new List<CPUSystem>();
    public List<StorageDevice> InstalledStorageDevice = new List<StorageDevice>();
    public List<PowerSupplySystem> InstalledPSU = new List<PowerSupplySystem>();
    public List<GPUSystem> InstalledGPU = new List<GPUSystem>();
    public List<ModemSystem> InstalledModem = new List<ModemSystem>();

    public MotherboardSystem (string name, string description,string storagetype,int maxpower, int powersupply, float cost, float health,int selectedmotherboardimage,int maxcpusockets,int maxstorageslots,int maxmemoryslots, List<SocketSystem> memoryslots, List<SocketSystem> storageslots, List<SocketSystem> cpusockets, List<RamSystem> installedram, List<CPUSystem> installedcpu, List<StorageDevice> installedstoragedevice, List<PowerSupplySystem> installedpsu, List<GPUSystem> installedgpu, List<ModemSystem> installedmodem) //,Texture2D icon)
	{
		Name = name;
		Description = description;
		StorageType = storagetype;
		MaxPower = maxpower;
        MemorySlots = memoryslots;
        StorageSlots = storageslots;
		PowerSupply = powersupply;
		Cost = cost;
		Health = health;
        SelectedMotherboardImage = selectedmotherboardimage;
        MaxCPUSockets = maxcpusockets;
        MaxStorageSlots = maxstorageslots;
        MaxMemorySlots = maxmemoryslots;
        CPUSockets = cpusockets;
        InstalledRAM = installedram;
        InstalledCPU = installedcpu;
        InstalledStorageDevice = installedstoragedevice;
        InstalledPSU = installedpsu;
        InstalledGPU = installedgpu;
        InstalledModem = installedmodem;

    }
}
