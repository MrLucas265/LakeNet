using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters;

[Serializable]
public class SVector2
{
    public float x;

    public float y;

    public SVector2(Vector2 MyRect)
    {
        x = MyRect.x;
        y = MyRect.y;
    }
    public override string ToString()
    {
        return String.Format("[{0}, {1}]", x, y);
    }


    /// Automatic conversion from SerializableRect to Rect
    public static implicit operator Vector2(SVector2 vRect)
    {
        return new Vector2(vRect.x, vRect.y);
    }


    /// Automatic conversion from Rect to SerializableRect
    public static implicit operator SVector2(Vector2 vRect)
    {
        return new SVector2(vRect);
    }
}