using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RamSystem
{
    public string Name;
    public string Type;
    public float PowerUsage;
    public float Max;
    public float Remaining;
    public float Used;
    public float Speed;
    public float DegradationRate;
    public float MaxHealth;
    public float CurrentHealth;
    public float HealthPercentage;
    public float PowerEff;
    public int SelectedImage;

    public RamSystem(string name,string type, float powerusage,float max,float remaining,float used,float speed,float degradationrate, float maxhealth, float currenthealth, float healthpercentage, float powereff,int selectedimage) //,Texture2D icon)
    {
        Name = name;
        Type = type;
        PowerUsage = powerusage;
        Max = max;
        Remaining = remaining;
        Used = used;
        Speed = speed;
        DegradationRate = degradationrate;
        MaxHealth = maxhealth;
        CurrentHealth = currenthealth;
        HealthPercentage = healthpercentage;
        PowerEff = powereff;
        SelectedImage = selectedimage;
    }
}
