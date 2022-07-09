using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffectSystem
{
    public string Name;
    public float Attribute;
    public int TurnsEffected;
    public string TexturePath;

    public StatusEffectSystem(string name, float attribute, int turnseffected, string texturepath)
    {
        Name = name;
        Attribute = attribute;
        TurnsEffected = turnseffected;
        TexturePath = texturepath;
    }

    public StatusEffectSystem()
    {

    }
}