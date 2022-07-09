using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters;

[Serializable]
public class SColor
{
    public byte r;
    public byte g;
    public byte b;
    public byte a;

    public SColor(Color32 MyRect)
    {
        r = MyRect.r;
        g = MyRect.g;
        b = MyRect.b;
        a = MyRect.a;

    }
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}, {3}]", r, g, b, a);
    }


    /// Automatic conversion from SerializableRect to Rect
    public static implicit operator Color32(SColor vRect)
    {
        return new Color32(vRect.r, vRect.g, vRect.b, vRect.a);
    }


    /// Automatic conversion from Rect to SerializableRect
    public static implicit operator SColor(Color32 vRect)
    {
        return new SColor(vRect);
    }

}