using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters;
using System.Runtime.InteropServices;

[Serializable]
public class STexture2D
{
    public int Width;

    public int Height;

    public STexture2D(Texture2D MyRect)
    {
        Width = MyRect.width;
        Height = MyRect.height;
    }

    public STexture2D(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public override string ToString()
    {
        return String.Format("[{0}, {1}]", Width, Height);
    }

    /// Automatic conversion from SerializableRect to Rect
    public static implicit operator Texture2D(STexture2D vRect)
    {
        return new Texture2D(vRect.Width,vRect.Height);
    }


    /// Automatic conversion from Rect to SerializableRect
    public static implicit operator STexture2D(Texture2D vRect)
    {
        return new STexture2D(vRect);
    }
}