using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RegistryDataSystem
{
    public string ValueName;
    public string DataString;
    public string DataTypeString;
    public int DataInt;
    public bool DataBool;
    public float DataFloat;
    public double DataDouble;
    public SRect DataRect;
    public SVector3 DataVector3;
    public SVector2 DataVector2;
    public List<ProgramSystemv2> ProgramData = new List<ProgramSystemv2>();
    public List<string> StringListData = new List<string>();
    public List<MenuButtonSystem> MenuButtonData = new List<MenuButtonSystem>();
    public SColor ColorData;
    public ColorSystem ColorFloatData;
    public ProgramRequest Request;
    public NetworkSystem NetworkData;
    public FileUtilitySystem FMS;
    public List<FileUtilitySystem> FMSList = new List<FileUtilitySystem>();
    public PlayerDataSystem PlayerData;

    public List<string> DataType = new List<string>();
    public List<string> DataValue = new List<string>();

    public RegistryDataSystem(string valuename)
    {
        ValueName = valuename;
    }

    public RegistryDataSystem(string valuename, string valuedatatype)
    {
        ValueName = valuename;
        DataTypeString = valuedatatype;
    }

    public RegistryDataSystem()
    { 

    }
}