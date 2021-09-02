using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoredMotherboardSystem
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

    public StoredMotherboardSystem(string name, string description, string storagetype, int maxpower, int powersupply, float cost, float health, int selectedmotherboardimage, int maxcpusockets, int maxstorageslots, int maxmemoryslots) //,Texture2D icon)
    {
        Name = name;
        Description = description;
        StorageType = storagetype;
        MaxPower = maxpower;
        PowerSupply = powersupply;
        Cost = cost;
        Health = health;
        SelectedMotherboardImage = selectedmotherboardimage;
        MaxCPUSockets = maxcpusockets;
        MaxStorageSlots = maxstorageslots;
        MaxMemorySlots = maxmemoryslots;

    }
}
