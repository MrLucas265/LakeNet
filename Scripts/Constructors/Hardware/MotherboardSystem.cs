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

    //The slot positions for the pics
    public List<SocketSystem> ExpansionSockets = new List<SocketSystem>();
    public List<SocketSystem> RAMSlots = new List<SocketSystem>();
    public List<SocketSystem> CPUSockets = new List<SocketSystem>();
    public List<SocketSystem> StorageSlots = new List<SocketSystem>();

    //What compoents are installed

    public BandwidthSystem Bandwidth;
    public HealthStatSystem Health = new HealthStatSystem();
    public ValueSystem Value = new ValueSystem();

    public GatewayStatusSystem Status;

    public MotherboardSystem (string manufacture,string brand, string chipset,string socket,string productname,string description,int selectedmotherboardimage,int maxcpusockets,int maxstorageslots,int maxmemoryslots,int maxexpansionslots) //,Texture2D icon)
	{
        Manufacture = manufacture;
        Brand = brand;
        Chipset = chipset;
        Socket = socket;
        ProductName = productname;
        Description = description;

        SelectedMotherboardImage = selectedmotherboardimage;
        MaxCPUSockets = maxcpusockets;
        MaxStorageSlots = maxstorageslots;
        MaxMemorySlots = maxmemoryslots;
        MaxExpansionSlots = maxexpansionslots;
    }

    public MotherboardSystem() //,Texture2D icon)
    {
        Manufacture = "";
        Brand = "";
        Chipset = "";
        Socket = "";
        ProductName = "";
        Description = "";

        SelectedMotherboardImage = 0;
        MaxCPUSockets = 0;
        MaxStorageSlots = 0;
        MaxMemorySlots = 0;
        MaxExpansionSlots = 0;

    }
}
