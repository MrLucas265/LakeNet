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

    public StorageType Type;

    public int UIPosX = 0;
    public int UIPosY = 0;
    public int Image = 0;

    public enum StorageType
	{
		External,
		HDD,
		SSD
	}
		
	public StorageDevice(string name, string manufactor,string description, string connector,float speed,float usedspace,float freespace,float capacity,float powerusage,float degradationrate, float maxhealth, float currenthealth, float healthpercentage, float powereff,float boottime, StorageType type,int uiposx,int uiposy,int image)
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
        UIPosX = uiposx;
        UIPosY = uiposy;
        Image = image;
	}
}