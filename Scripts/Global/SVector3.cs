using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TreeEditor;
using UnityEngine;

[Serializable]
public class SVector3
{
    public float x;
    public float y;
    public float z;

    public SVector3() { }

    public SVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public SVector3(Vector3 MyRect)
    {
        x = MyRect.x;
        y = MyRect.y;
        z = MyRect.z;

    }
    public override string ToString()
    {
        return $"{x},{y},{z}";
    }

    public static SVector3 ParseString(string value)
    {
        string[] parts = value.Split(',');
        if (parts.Length != 3)
        {
            throw new FormatException($"Cannot convert '{value}' to SRect. Expected format: 'x,y,width,height'");
        }

        return new SVector3
        {
            x = Convert.ToSingle(parts[0].Trim()),
            y = Convert.ToSingle(parts[1].Trim()),
            z = Convert.ToSingle(parts[2].Trim())
        };
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

    #region IConvertible
    public TypeCode GetTypeCode() => TypeCode.Object;
    public Boolean ToBoolean(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Boolean");
    public Byte ToByte(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Byte");
    public Char ToChar(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Char");
    public DateTime ToDateTime(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to DateTime");
    public Decimal ToDecimal(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Decimal");
    public Double ToDouble(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Double");
    public Int16 ToInt16(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Int16");
    public Int32 ToInt32(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Int32");
    public Int64 ToInt64(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Int64");
    public SByte ToSByte(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to SByte");
    public Single ToSingle(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to Single");
    public string ToString(IFormatProvider? provider) => $"{x},{y},{z}";
    public UInt16 ToUInt16(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to UInt16");
    public UInt32 ToUInt32(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to UInt32");
    public UInt64 ToUInt64(IFormatProvider? provider) => throw new InvalidCastException("Cannot convert SVector3 to UInt64");

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType == typeof(SVector3))
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