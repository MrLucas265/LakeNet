using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValueSystem
{
    public int NewPrice;
    public int BoughtPrice;
    public int CurrentValue;
    public float Depreciation;

    public ValueSystem(int newprice,int boughtprice,int currentvalue,float depreciation)
    {
        NewPrice = newprice;
        BoughtPrice = boughtprice;
        CurrentValue = currentvalue;
        Depreciation = depreciation;
    }

    public ValueSystem()
    {
    }
}
