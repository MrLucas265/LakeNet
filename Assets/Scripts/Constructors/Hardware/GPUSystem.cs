using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GPUSystem
{
    public string Name;
    public string Socket;
    public int Cores;
    public float MaxSpeed;
    public float MinSpeed;
    public float FactoryMaxSpeed;
    public float MaxMemory;
    public float MemoryUsage;
    public float CurrentSpeed;
    public float Usage;
    public float Voltage;
    public float DegredationRate;
    public float MaxHealth;
    public float CurrentHealth;
    public float HealthPercentage;
    public float PowerDraw;
    public float PowerEff;

    public GPUSystem(string name, string socket, int cores, float maxspeed, float minspeed, float factorymaxspeed,float maxmemory,float memoryusage, float currentspeed, float usage, float voltage, float degredationrate, float maxhealth, float currenthealth, float healthpercentage, float powerdraw, float powereff)
    {
        Name = name;
        Socket = socket;
        Cores = cores;
        MaxSpeed = maxspeed;
        MinSpeed = minspeed;
        FactoryMaxSpeed = factorymaxspeed;
        MaxMemory = maxmemory;
        MemoryUsage = memoryusage;
        CurrentSpeed = currentspeed;
        Usage = usage;
        Voltage = voltage;
        DegredationRate = degredationrate;
        MaxHealth = maxhealth;
        CurrentHealth = currenthealth;
        HealthPercentage = healthpercentage;
        PowerDraw = powerdraw;
        PowerEff = powereff;
    }
}