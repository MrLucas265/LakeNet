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

	public float Speed;
	public float UsedSpace;
	public float FreeSpace;
	public float Capacity;

	public float PowerUsage;

    public float DegradationRate;
    public float CurrentHealth;
    public float MaxHealth;
    public float HealthPercentage;
    public float PowerEff;

    public float BootTime;

	public float Timer;
	public float InitalTimer;

    public StorageType Type;

    public int UIPosX = 0;
    public int UIPosY = 0;
    public int Image = 0;

	public List<DrivePatSystem> Partitions = new List<DrivePatSystem>();

	//public BandwidthSystem Bandwidth;

	public enum StorageType
	{
		External,
		HDD,
		SSD
	}
		
	public StorageDevice(string name, string manufactor,string description, string connector,float speed,float usedspace,float freespace,float capacity,float powerusage,float degradationrate, float maxhealth, float currenthealth, float healthpercentage, float powereff,float boottime,float timer,float initaltimer, StorageType type,int uiposx,int uiposy,int image, List<DrivePatSystem> part)//,BandwidthSystem bandwidth)
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
		Timer = timer;
		InitalTimer = initaltimer;
        Type = type;
        UIPosX = uiposx;
        UIPosY = uiposy;
        Image = image;
		Partitions = part;
		//Bandwidth = bandwidth;
	}
}