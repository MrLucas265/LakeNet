using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorSystem
{
    public float Red;
    public float Green;
    public float Blue;
    public float Alpha;

    public ColorSystem(float red, float green, float blue, float alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public ColorSystem() { }

}