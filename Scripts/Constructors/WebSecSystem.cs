using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WebSecSystem
{
    public Server ServerName;
    public string Name;
    public int Level;
    public string Status;
    public float Timer;
    public float Cooldown;
    public SecType Type;

    public enum Server
    {
        REVA,
        REVATest,
        BecasSystem,
        Unicom,
        Jaildew,
        Para,
        ISD,
        Academics
    }

    public enum SecType
    {
        UAC,
        Firewall,
        Proxy,
        AntiVirius,
        LogManagement,
        AutoBackup,
        IDS,
        Encrypter,
        Monitor
    }

    public WebSecSystem(Server servername, string name, int level, string status,float timer,float cooldown,SecType type)
    {
        ServerName = servername;
        Name = name;
        Level = level;
        Status = status;
        Timer = timer;
        Cooldown = cooldown;
        Type = type;
    }

} 