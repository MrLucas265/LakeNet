using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConnectorSystem
{
    public string Type;
    public string Size;
    public string Generation;

    public ConnectorSystem(string type,string size, string generation)
    {
        Type = type;
        Size = size;
        Generation = generation;
    }
}
