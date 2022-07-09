using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StorageDevice
{
	public string Name;
	public string Manufactor;
	public string Description;

	public string Connector;

	public int POSX;
	public int POSY;
	public int SelectedImageNumber;

	public string Size;
	public string Generation;

	public double Speed;
	public double UsedSpace;
	public double FreeSpace;
	public double Capacity;

	public float PowerUsage;

    public float DegradationRate;
    public float CurrentHealth;
    public float MaxHealth;
    public double HealthPercentage;
    public float PowerEff;

	public double FreeSpacePercentage;

    public float BootTime;

    public StorageType Type;

	public List<string> InstalledOS = new List<string>();
	public List<OperatingSystems> OS = new List<OperatingSystems>();

	//public BandwidthSystem Bandwidth;

	public enum StorageType
	{
		External,
		HDD,
		SSD
	}
		
	public StorageDevice(string name, string manufactor,string description, string connector, double speed,double usedspace, double freespace, double capacity,float powerusage,float degradationrate, float maxhealth, float currenthealth, float healthpercentage, float powereff,float boottime, StorageType type)//,BandwidthSystem bandwidth)
	{
		Name = name;
		Manufactor = manufactor;
		Description = description;
		Connector = connector;
		Speed = speed;
		UsedSpace = usedspace;
		FreeSpace = freespace;
		Capacity = capacity;
		PowerUsage = powerusage;
        DegradationRate = degradationrate;
        MaxHealth = maxhealth;
        CurrentHealth = currenthealth;
        HealthPercentage = healthpercentage;
        PowerEff = powereff;
        BootTime = boottime;
        Type = type;
		//Bandwidth = bandwidth;
	}
}