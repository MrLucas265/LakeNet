using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UACStockSystem
{

    public string UserName;
    public string Password;
    public string AccountHolder;
    public string IP;
    public string LoggedInIP;
    public bool LoggedIn;
    public AccountType Type;
    public StockSystem UserStocks;

    public enum AccountType
    {
        AccessPrivilages0,
        AccessPrivilages1,
        AccessPrivilages2,
        AccessPrivilages3,
        AccessPrivilages4,
        AccessPrivilages5,
        Officer,
        SysAdmin,
        Admin,
        User,
        Guest,
        Read,
        ReadWrite,
        LoggedIn,
        Exchange
    }

    public UACStockSystem(string username, string password, string accountholder, string ip, string loggedinip, bool loggedin, AccountType type, StockSystem userstocks)
    {
        UserName = username;
        Password = password;
        AccountHolder = accountholder;
        IP = ip;
        LoggedInIP = loggedinip;
        Type = type;
        LoggedIn = loggedin;
        UserStocks = userstocks;
    }
}