using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProxySystem
{

    public string ServerName;
    public string ServerAbv;
    public string ServerIP;
    public string ServerAddress;
    public Vector2 Position;

    public ProxySystem(string servername, string serverabv, string serverip, string serveraddress, Vector2 postion)
    {
        ServerName = servername;
        ServerAbv = serverabv;
        ServerIP = serverip;
        ServerAddress = serveraddress;
        Position = postion;
    }
}