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
    public Texture2D Photo;
    public DOBSystem DOB;
    public CollageSystem Collage;
    public UniversitySystem University;
    public BankSystem BankDetails;
    public GatewaySystem Gateway;

    public PeopleSystem(string name,string personid,string phonenumber,string ipaddress,string maritalstatus,string personalstatus,int iq,Texture2D photo,DOBSystem dob,CollageSystem collage, UniversitySystem university, BankSystem bankdetails, GatewaySystem gateway)
    {
        Name = name;
        PersonID = personid;
        PhoneNumber = phonenumber;
        IPAddress = ipaddress;
        MaritalStatus = maritalstatus;
        PersonalStatus = personalstatus;
        IQ = iq;
        Photo = photo;
        DOB = dob;
        Collage = collage;
        University = university;
        BankDetails = bankdetails;
        Gateway = gateway;
    }

}