using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GatewaySystem Gateway;
    public List<MissionSystem> Contracts = new List<MissionSystem>();
    public List<RepSystem> Reputation = new List<RepSystem>();

    public PeopleSystem(string name,string personid,string phonenumber,string ipaddress,string maritalstatus,string personalstatus,int iq,int photoid,DOBSystem dob,CollageSystem collage, UniversitySystem university, BankSystem bankdetails, GatewaySystem gateway, List<MissionSystem> contracts, List<RepSystem> reputation)
    {
        Name = name;
        PersonID = personid;
        PhoneNumber = phonenumber;
        IPAddress = ipaddress;
        MaritalStatus = maritalstatus;
        PersonalStatus = personalstatus;
        IQ = iq;
        PhotoID = photoid;
        DOB = dob;
        Collage = collage;
        University = university;
        BankDetails = bankdetails;
        Gateway = gateway;
        Contracts = contracts;
        Reputation = reputation;
    }

}