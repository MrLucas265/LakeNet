using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DomainSystem
{
    public string Name;
    public string Abv;
    public string IP;
    public string Address;
    public Vector2 Cords;

    public DomainSystem(string name,string abv,string ip,string address,Vector2 cords)
    {
        Name = name;
        Abv = abv;
        IP = ip;
        Address = address;
        Cords = cords;
    }
}