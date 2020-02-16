using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatewaySystem
{
    public string Name;
    //public string Statusl
    public OperatingSystems SelectedOS;
    public MotherboardSystem Motherboard;
    public FileSystem Files;

    public GatewaySystem(string name, OperatingSystems selectedos, MotherboardSystem motherboard, FileSystem files) //,Texture2D icon)
    {
        Name = name;
        SelectedOS = selectedos;
        Motherboard = motherboard;
        Files = files;
    }
}
