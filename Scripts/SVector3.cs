using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters;

[Serializable]
public class SVector3
{
    public float x;

    public float y;

    public float z;

    public SVector3(Vector3 MyRect)
    {
        x = MyRect.x;
        y = MyRect.y;
        z = MyRect.z;
    }
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }


    /// Automatic conversion from SerializableRect to Rect
    public static implicit operator Vector3(SVector3 vRect)
    {
        return new Vector3(vRect.x, vRect.y, vRect.z);
    }


    /// Automatic conversion from Rect to SerializableRect
    public static implicit operator SVector3(Vector3 vRect)
    {
        return new SVector3(vRect);
    }
}