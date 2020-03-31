using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DOBSystem
{
    public int Day;
    public int Month;
    public int Year;
    public int Age;

    public DOBSystem(int day,int month,int year,int age)
    {
        Day = day;
        Month = month;
        Year = year;
        Age = age;
    }
}