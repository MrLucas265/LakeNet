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

    public float Speed;
    public float MaxSpeed;
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

    public ModemSystem(string name, string manufactor, string description, string connector, float speed, float maxspeed, float throttlespeed, float capacity, float powerusage, float degradationrate,float maxhealth, float currenthealth,float healthpercentage,  float powereff,float modemmonthlyprice, float planmonthlyprice, ModemConnectionType connectiontype)
    {
        Name = name;
        Manufactor = manufactor;
        Description = description;
        Connector = connector;
        Speed = speed;
        MaxSpeed = maxspeed;
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
        ConnectionType = connectiontype;
    }
}