using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RegistrySystem
{
    public string KeyName;
    //public KeyNames KeyNameEnum;
    public List<RegistryDataSystem> Values = new List<RegistryDataSystem>();
    public List<RegistryValueSystem> Valuesv2 = new List<RegistryValueSystem>();
    //public enum KeyNames
    //{
    //    Core,
    //    CLI,
    //    FileManager,
    //    OS,
    //    MediaPlayer,
    //    System,
    //    ControlPanel,
    //    Network,
    //    Calculator,
    //    Notepad,
    //    FileUtility,
    //    WindowManager,
    //    PlayerData,
    //    Discord
    //}

    public RegistrySystem(string name)
    {
        KeyName = name;
    }

    //public RegistrySystem(KeyNames keynameenum)
    //{
    //    KeyNameEnum = keynameenum;
    //}

    public RegistrySystem()
    {

    }

    public RegistrySystem(string name, List<RegistryDataSystem> values)
    {
        KeyName = name;
        Values = values;
    }

    public RegistrySystem(string name, List<RegistryValueSystem> values)
    {
        KeyName = name;
        Valuesv2 = values;
    }
}