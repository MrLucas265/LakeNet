using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UACSystem
{

    public string UserName;
    public string Password;
    public string AccountHolder;
    public string IP;
    public AccountType Type;

    public enum AccountType
    {
        AccessPrivilages0,
        AccessPrivilages1,
        AccessPrivilages2,
        AccessPrivilages3,
        AccessPrivilages4,
        AccessPrivilages5,
        Admin,
        User,
        Guest,
        Read,
        ReadWrite
    }

    public UACSystem(string username, string password, string accountholder, string ip, AccountType type)
    {
        UserName = username;
        Password = password;
        AccountHolder = accountholder;
        IP = ip;
        Type = type;
    }
}