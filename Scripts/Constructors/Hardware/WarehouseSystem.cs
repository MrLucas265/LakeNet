using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WarehouseSystem
{
    public string StorageName;
    public string StorageLocation;
    public int MaxStorage;
    public List<RamSystem> StoredRAM = new List<RamSystem>();
    public List<CPUSystem> StoredCPU = new List<CPUSystem>();
    public List<StorageDevice> StoredStorageDevice = new List<StorageDevice>();
    public List<PowerSupplySystem> StoredPSU = new List<PowerSupplySystem>();
    public List<GPUSystem> StoredGPU = new List<GPUSystem>();
    public List<ModemSystem> StoredModem = new List<ModemSystem>();
    public List<StoredMotherboardSystem> StoredMotherboard = new List<StoredMotherboardSystem>();

    public WarehouseSystem(string storagename,string storagelocation,int maxstorage,List<RamSystem> installedram, List<CPUSystem> installedcpu, List<StorageDevice> installedstoragedevice, List<PowerSupplySystem> installedpsu, List<GPUSystem> installedgpu, List<ModemSystem> installedmodem, List<StoredMotherboardSystem> installedmotherboard) //,Texture2D icon)
    {
        StorageName = storagename;
        StorageLocation = storagelocation;
        MaxStorage = maxstorage;
        StoredRAM = installedram;
        StoredCPU = installedcpu;
        StoredStorageDevice = installedstoragedevice;
        StoredPSU = installedpsu;
        StoredGPU = installedgpu;
        StoredModem = installedmodem;
        StoredMotherboard = installedmotherboard;
    }
}
