using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerSystem
{
    public string Name;
    public string Address;
    public GatewaySystem Gateway;
    public List<WebSecSystem> Security = new List<WebSecSystem>();
    public ServerType Type;


    public enum ServerType
    {
        Backup,
        Medical,
        Web,
        Storage,
        Research

    }



    public ServerSystem(string name, GatewaySystem gateway, List<WebSecSystem> secuirty, ServerType type)
    {
        Name = name;
        Gateway = gateway;
        Security = secuirty;
        Type = type;
    }
}
