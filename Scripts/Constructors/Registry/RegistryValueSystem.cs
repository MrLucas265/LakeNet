using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RegistryValueSystem
{
    public string ValueName;
    public string DataTypeString;
    public string DataString;

    public RegistryValueSystem(string valuename)
    {
        ValueName = valuename;
    }

    public RegistryValueSystem()
    {

    }
}