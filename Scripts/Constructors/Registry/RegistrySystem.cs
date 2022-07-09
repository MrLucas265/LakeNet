using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RegistrySystem
{
    public string KeyName;
    public List<RegistryDataSystem> Values = new List<RegistryDataSystem>();
    public RegistrySystem(string name)
    {
        KeyName = name;
    }
    public RegistrySystem()
    {

    }

    public RegistrySystem(string name, List<RegistryDataSystem> values)
    {
        KeyName = name;
        Values = values;
    }
}