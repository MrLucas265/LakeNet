using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModemSystem
{
    public string Name;
    public string Manufactor;
    public string Description;

    public string Connector;

    public float CurrentSpeed;
    public float MaxSpeed;
    public float NetPlanSpeed;
    public float ThrottleSpeed;
    public float Capacity;

    public float PowerUsage;

    public float DegradationRate;
    public float MaxHealth;
    public float CurrentHealth;
    public float HealthPercentage;
    public float PowerEff;

    public float ModemMonthlyPrice;
    public float PlanMonthlyPrice;
    public string ModemIP;

    public ModemConnectionType ConnectionType;

    public enum ModemConnectionType
    {
        DialUp,
        Leased,
        Cable,
        DSL,
        Fiber,
        Broadband,
        WirelessBroadband,
        SataliteBroadband,
        MobileBroadband,
    }

    public ModemSystem(string name, string manufactor, string description, string connector, float currentspeed, float maxspeed,float netplanspeed, float throttlespeed, float capacity, float powerusage, float degradationrate,float maxhealth, float currenthealth,float healthpercentage,  float powereff,float modemmonthlyprice, float planmonthlyprice,string modemip, ModemConnectionType connectiontype)
    {
        Name = name;
        Manufactor = manufactor;
        Description = description;
        Connector = connector;
        CurrentSpeed = currentspeed;
        MaxSpeed = maxspeed;
        NetPlanSpeed = netplanspeed;
        ThrottleSpeed = throttlespeed;
        Capacity = capacity;
        PowerUsage = powerusage;
        DegradationRate = degradationrate;
        MaxHealth = maxhealth;
        CurrentHealth = currenthealth;
        HealthPercentage = healthpercentage;
        PowerEff = powereff;
        ModemMonthlyPrice = modemmonthlyprice;
        PlanMonthlyPrice = planmonthlyprice;
        ModemIP = modemip;
        ConnectionType = connectiontype;
    }
}