using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CPUSystem
{
    public string Name;
    public string Manufacturer;
    public string ProductName;
    public string Socket;
    public int Size;
    public int Cores;
    public float MaxSpeed;
    public float MinSpeed;
    public float FactoryMaxSpeed;
    public float SpeedDiffrence;
    public float SpeedBoostMod;
    public float CurrentSpeed;
    public float Usage;
    public float UsagePercent;
    public float Voltage;
    public float DegredationRate;
    public float DegredationRateMod;
    public float MaxHealth;
    public float CurrentHealth;
    public float HealthPercentage;
    public string Status;
    public float PowerDraw;
    public float IdlePowerDraw;
    public float PowerEff;
    public int UIPosX;
    public int UIPosY;
    public int SelectedCPUImage;

    public CPUSystem(string name,string manufacturer, string productname, string socket,int size,int cores,float maxspeed,float minspeed,float factorymaxspeed,float speeddiffrence,float speedboostmod,float currentspeed,float usage,float usagepercent,float voltage,float degredationrate, float degredationratemod, float maxhealth, float currenthealth, float healthpercentage,string status,float powerdraw,float idlepowerdraw,float powereff,int uiposx,int uiposy,int selectedcpuimage)
    {
        Name = name;
        Manufacturer = manufacturer;
        ProductName = productname;
        Socket = socket;
        Size = size;
        Cores = cores;
        MaxSpeed = maxspeed;
        MinSpeed = minspeed;
        FactoryMaxSpeed = factorymaxspeed;
        SpeedDiffrence = speeddiffrence;
        SpeedBoostMod = speedboostmod;
        CurrentSpeed = currentspeed;
        Usage = usage;
        UsagePercent = usagepercent;
        Voltage = voltage;
        DegredationRate = degredationrate;
        DegredationRateMod = degredationratemod;
        MaxHealth = maxhealth;
        CurrentHealth = currenthealth;
        HealthPercentage = healthpercentage;
        Status = status;
        PowerDraw = powerdraw;
        IdlePowerDraw = idlepowerdraw;
        PowerEff = powereff;
        UIPosX = uiposx;
        UIPosY = uiposy;
        SelectedCPUImage = selectedcpuimage;
}
}