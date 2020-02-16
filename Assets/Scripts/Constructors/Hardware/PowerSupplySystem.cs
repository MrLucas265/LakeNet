using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerSupplySystem
{
    public string Name;
    public string Type;
    public float Max;
    public float Remaining;
    public float Used;
    public float DegradationRate;
    public float MaxHealth;
    public float CurrentHealth;
    public float HealthPercentage;
    public float PowerEff;

    public PowerSupplySystem(string name, string type, float max, float remaining, float used, float degradationrate, float maxhealth, float currenthealth, float healthpercentage, float powereff) //,Texture2D icon)
    {
        Name = name;
        Type = type;
        Max = max;
        Remaining = remaining;
        Used = used;
        DegradationRate = degradationrate;
        MaxHealth = maxhealth;
        CurrentHealth = currenthealth;
        HealthPercentage = healthpercentage;
        PowerEff = powereff;
    }
}
