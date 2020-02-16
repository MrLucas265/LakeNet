using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanSystem
{
    public string Company;
    public string URL;
    public string Name;
    public string Note;
    public int Price;
    public DateSystem Inital;
    public DateSystem Due;

    public PlanSystem(string company,string url,string name,string note,int price,DateSystem inital,DateSystem due)
    {
        Company = company;
        URL = url;
        Name = name;
        Note = note;
        Price = price;
        Inital = inital;
        Due = due;
    }
}