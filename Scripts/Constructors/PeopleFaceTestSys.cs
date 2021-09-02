using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PeopleFaceTestSys
{
    public string Name;
    public int Photo;

    public PeopleFaceTestSys(string name, int photo)
    {
        Name = name;
        Photo = photo;
    }
}