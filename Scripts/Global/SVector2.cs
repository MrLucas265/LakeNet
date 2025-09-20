using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TreeEditor;
using UnityEngine;

[Serializable]
public class SVector2
{
    public float x;
    public float y;

    public SVector2() { }

    public SVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public SVector2(Vector2 MyRect)
    {
        x = MyRect.x;
        y = MyRect.y;

    }
    public override string ToString()
    {
        return $"{x},{y}";
    }

    public static SVector2 ParseString(string value)
    {
        string[] parts = value.Split(',');
        if (parts.Length != 2)
        {
            throw new FormatException($"Cannot convert '{value}' to SVector2. Expected format: 'x,y'");
        }

        return new SVector2
        {
            x = Convert.ToSingle(parts[0].Trim()),
            y = Convert.ToSingle(parts[1].Trim())
        };
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

    #region IConvertible
    public TypeCode GetTypeCode() => TypeCode.Object;
    public Boolean ToBoolean(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Boolean");
    public Byte ToByte(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Byte");
    public Char ToChar(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Char");
    public DateTime ToDateTime(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to DateTime");
    public Decimal ToDecimal(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Decimal");
    public Double ToDouble(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Double");
    public Int16 ToInt16(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Int16");
    public Int32 ToInt32(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Int32");
    public Int64 ToInt64(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Int64");
    public SByte ToSByte(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to SByte");
    public Single ToSingle(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to Single");
    public string ToString(IFormatProvider? provider) => $"{x},{y}";
    public UInt16 ToUInt16(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to UInt16");
    public UInt32 ToUInt32(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to UInt32");
    public UInt64 ToUInt64(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector2 to UInt64");

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType == typeof(SVector2))
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