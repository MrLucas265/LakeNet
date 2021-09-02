using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WindowColorSystem
{
    public float Red;
    public float Green;
    public float Blue;
    public float Alpha;

    public WindowColorSystem(float red, float green, float blue, float alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

}