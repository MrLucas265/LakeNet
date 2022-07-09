using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompanyServerSystem
{
    public string Name;
    public string IP;
    public List<RemoteFileSystem> WebPages = new List<RemoteFileSystem>();
    public List<ProgramSystem> Files = new List<ProgramSystem>();
    public List<ProgramSystem> QuickList = new List<ProgramSystem>();
    public List<ProgramSystem> DesktopList = new List<ProgramSystem>();
    public List<UACSystem> Accounts = new List<UACSystem>();
    public List<BankAccountsSystem> BankDetails = new List<BankAccountsSystem>();
    public StockTradeSys StockExchange;
    public ServerType Type;

    public enum ServerType
    {
        PC,
        Webserver,
        FileServer,
        EmailServer,
        PhoneServer,
        BankServer,
        StockServer,
        StoreServer
    }

    public CompanyServerSystem(string name,string ip, List<RemoteFileSystem> webpages, List<ProgramSystem> files, List<ProgramSystem> quicklist, List<ProgramSystem> desktoplist, ServerType type, List<UACSystem> accounts, List<BankAccountsSystem> bank, StockTradeSys stockexchange)
    {
        Name = name;
        IP = ip;
        WebPages = webpages;
        Files = files;
        QuickList = quicklist;
        DesktopList = desktoplist;
        Type = type;
        Accounts = accounts;
        BankDetails = bank;
        StockExchange = stockexchange;
    }
}
