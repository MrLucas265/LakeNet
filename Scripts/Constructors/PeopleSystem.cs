using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml.Serialization;

[XmlRoot("People")]

[System.Serializable]

public class PeopleSystem
{
    public string Name;
    public string PersonID;
    public string PhoneNumber;
    public string IPAddress;
    public string MaritalStatus;
    public string PersonalStatus;
    public int IQ;
    public int PhotoID;
    public DOBSystem DOB;
    public CollageSystem Collage;
    public UniversitySystem University;
    public BankSystem BankDetails;
    public GatewaySystem Gateway = new GatewaySystem();
    public List<MissionSystem> Contracts = new List<MissionSystem>();
    public List<RepSystem> Reputation = new List<RepSystem>();
    public string Action;

    public PeopleSystem(string name,string personid,string phonenumber,string ipaddress,string maritalstatus,string personalstatus,int iq,int photoid)
    {
        Name = name;
        PersonID = personid;
        PhoneNumber = phonenumber;
        IPAddress = ipaddress;
        MaritalStatus = maritalstatus;
        PersonalStatus = personalstatus;
        IQ = iq;
        PhotoID = photoid;
    }

}