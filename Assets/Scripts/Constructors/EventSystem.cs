using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventSystem
{
    public string Name;
    public string Information;
    public int Day;
    public int Month;
    public int Year;

    public EventSystem(string name,string information,int day,int month,int year)
    {
        Name = name;
        Information = information;
        Day = day;
        Month = month;
        Year = year;
    }
}