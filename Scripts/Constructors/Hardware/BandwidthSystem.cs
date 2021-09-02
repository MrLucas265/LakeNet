using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BandwidthSystem
{
    public float Max;
    public float Current;
    public float Remaining;
    public float Percentage;

    public BandwidthSystem(float max,float current,float remaining,float percentage)
    {
        Max = max;
        Current = current;
        Remaining = remaining;
        Percentage = percentage;
    }
}
