using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthStatSystem
{
    public float DegredationRate;
    public float DegredationRateMod;
    public float Max;
    public float Current;
    public float Remaining;
    public float Percentage;
    public float Timer;
    public float StartTime;

    public HealthStatSystem(float degredationrate, float degredationratemode,float max,float current,float remaining,float percentage,float timer,float starttime)
	{
        DegredationRate = degredationrate;
        DegredationRateMod = degredationratemode;
        Max = max;
        Current = current;
        Remaining = remaining;
        Percentage = percentage;
        Timer = timer;
        StartTime = starttime;
    }

    public HealthStatSystem()
    {

    }
}

