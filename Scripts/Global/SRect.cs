using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters;

[Serializable]
public class SRect
{
    public float x;

    public float y;

    public float width;

    public float height;


    public float xMin
    {
        get
        {
            return x;
        }
        set
        {
            float xMax = this.xMax;
            x = value;
            width = xMax - x;
        }
    }

    //
    // Summary:
    //     The minimum Y coordinate of the rectangle.
    public float yMin
    {
        get
        {
            return y;
        }
        set
        {
            float yMax = this.yMax;
            y = value;
            height = yMax - y;
        }
    }

    //
    // Summary:
    //     The maximum X coordinate of the rectangle.
    public float xMax
    {
        get
        {
            return width + x;
        }
        set
        {
            width = value - x;
        }
    }

    //
    // Summary:
    //     The maximum Y coordinate of the rectangle.
    public float yMax
    {
        get
        {
            return height + y;
        }
        set
        {
            height = value - y;
        }
    }

    public SRect(Rect MyRect)
    {
        x = MyRect.x;
        y = MyRect.y;
        width = MyRect.width;
        height = MyRect.height;

    }
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}, {3}]", x, y, width, height);
    }


    /// Automatic conversion from SerializableRect to Rect
    public static implicit operator Rect(SRect vRect)
    {
        return new Rect(vRect.x, vRect.y, vRect.width, vRect.height);
    }


    /// Automatic conversion from Rect to SerializableRect
    public static implicit operator SRect(Rect vRect)
    {
        return new SRect(vRect);
    }

    public bool Contains(Vector2 point)
    {
        return point.x >= xMin && point.x < xMax && point.y >= yMin && point.y < yMax;
    }

}