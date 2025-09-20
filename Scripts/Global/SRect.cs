using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TreeEditor;
using UnityEngine;

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

    public SRect() { }

    public SRect(float x, float y, float width, float height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
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
        return $"{x},{y},{width},{height}";
    }

    public static SRect ParseString(string value)
    {
        string[] parts = value.Split(',');
        if (parts.Length != 4)
        {
            throw new FormatException($"Cannot convert '{value}' to SRect. Expected format: 'x,y,width,height'");
        }

        return new SRect
        {
            x = Convert.ToSingle(parts[0].Trim()),
            y = Convert.ToSingle(parts[1].Trim()),
            width = Convert.ToSingle(parts[2].Trim()),
            height = Convert.ToSingle(parts[3].Trim())
        };
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

    #region IConvertible
    public TypeCode GetTypeCode() => TypeCode.Object;
    public Boolean ToBoolean(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Boolean");
    public Byte ToByte(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Byte");
    public Char ToChar(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Char");
    public DateTime ToDateTime(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to DateTime");
    public Decimal ToDecimal(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Decimal");
    public Double ToDouble(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Double");
    public Int16 ToInt16(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Int16");
    public Int32 ToInt32(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Int32");
    public Int64 ToInt64(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Int64");
    public SByte ToSByte(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to SByte");
    public Single ToSingle(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to Single");
    public string ToString(IFormatProvider? provider) => $"{x},{y},{width},{height}";
    public UInt16 ToUInt16(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to UInt16");
    public UInt32 ToUInt32(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to UInt32");
    public UInt64 ToUInt64(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SRect to UInt64");

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType == typeof(SRect))
        {
            return this;
        }
        if (conversionType == typeof(string))
        {
            return ToString(provider);
        }
        throw new InvalidCastException($"Cannot convert SRect to {conversionType}");
    }
    #endregion
}