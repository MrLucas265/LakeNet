using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TitleBarSystem
{
    public string Name;
    public Rect Rect = new Rect();

    public TitleBarSystem(string name,Rect rect = new Rect())
    {
        Name = name;
        Rect = rect;
    }
}
