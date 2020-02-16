﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrgnizationSystem
{
    public string Name;
    public string Abv;
    public string ORGNum;
    public DOBSystem FoundingDate;
    public OrgType Type;
    public List<ServerSystem> Server = new List<ServerSystem>();

    public enum OrgType
    {
        Medical,
        Finnicheal,

    }

    public enum CompanyName
    {
        Medical,
        Finnicheal,

    }



    public OrgnizationSystem(string name, string abv, string orgnum,DOBSystem dob, OrgType type, List<ServerSystem> server)
    {
        Name = name;
        Abv = abv;
        ORGNum = orgnum;
        FoundingDate = dob;
        Type = type;
        Server = server;
        //Month = month;
        //Year = year;
        //Age = age;
    }
}
