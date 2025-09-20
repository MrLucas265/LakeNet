using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TreeEditor;
using UnityEngine;

[Serializable]
public class SColor
{
    public byte r;
    public byte g;
    public byte b;
    public byte a;

    public SColor() { }

    public SColor(byte r, byte g, byte b, byte a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public SColor(Color32 MyRect)
    {
        r = MyRect.r;
        g = MyRect.g;
        b = MyRect.b;
        a = MyRect.a;

    }
    public override string ToString()
    {
        return $"{r},{g},{b},{a}";
    }

    public static SRect ParseString(string value)
    {
        string[] parts = value.Split(',');
        if (parts.Length != 4)
        {
            throw new FormatException($"Cannot convert '{value}' to SColor. Expected format: 'r,g,b,a'");
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
    public static implicit operator Color32(SColor vRect)
    {
        return new Color32(vRect.r, vRect.g, vRect.b, vRect.a);
    }


    /// Automatic conversion from Rect to SerializableRect
    public static implicit operator SColor(Color32 vRect)
    {
        return new SColor(vRect);
    }

    #region IConvertible
    public TypeCode GetTypeCode() => TypeCode.Object;
    public Boolean ToBoolean(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Boolean");
    public Byte ToByte(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Byte");
    public Char ToChar(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Char");
    public DateTime ToDateTime(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to DateTime");
    public Decimal ToDecimal(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Decimal");
    public Double ToDouble(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Double");
    public Int16 ToInt16(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Int16");
    public Int32 ToInt32(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Int32");
    public Int64 ToInt64(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Int64");
    public SByte ToSByte(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to SByte");
    public Single ToSingle(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to Single");
    public string ToString(IFormatProvider? provider) => $"{r},{g},{b},{a}";
    public UInt16 ToUInt16(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to UInt16");
    public UInt32 ToUInt32(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to UInt32");
    public UInt64 ToUInt64(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SColor to UInt64");

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType == typeof(SColor))
        {
            return this;
        }
        if (conversionType == typeof(string))
        {
            return ToString(provider);
        }
        throw new InvalidCastException($"Cannot convert SColor to {conversionType}");
    }
    #endregion
}