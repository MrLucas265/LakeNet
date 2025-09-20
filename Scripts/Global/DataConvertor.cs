using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class DataConverter1
{
    public static TTo StringTo<TTo>(string value)
    {
        try
        {
            if (typeof(TTo) == typeof(SRect))
            {
                return (TTo)(object)SRect.ParseString(value);
            }
            if (typeof(TTo) == typeof(SVector3))
            {
                return (TTo)(object)SVector3.ParseString(value);
            }
            if (typeof(TTo) == typeof(SVector2))
            {
                return (TTo)(object)SVector2.ParseString(value);
            }
            if (typeof(TTo) == typeof(SColor))
            {
                return (TTo)(object)SColor.ParseString(value);
            }
            if (typeof(TTo) == typeof(string[]))
            {
                if (string.IsNullOrEmpty(value))
                {
                    return (TTo)(object)new string[0];
                }

                // Remove trailing comma if present
                string trimmedValue = value.TrimEnd(',', ' ');

                // Split and clean up each element
                string[] parsedArray = trimmedValue.Split(',');
                for (int i = 0; i < parsedArray.Length; i++)
                {
                    parsedArray[i] = parsedArray[i].Trim();
                }

                return (TTo)(object)parsedArray;
            }
            return (TTo)Convert.ChangeType(value, typeof(TTo));
        }
        catch
        {
            // maybe log an error here
            return default(TTo);
        }
    }

    public static int StringListCount(string value)
    {
        return StringTo<string[]>(value).Length;
    }
}

public class DataConverter
{
    public static byte FloatToByte(float value)
    {
        return (byte)value;
    }
    public static float ByteToFloat(byte value)
    {
        return (float)value;
    }
    public static int ByteToInt(byte value)
    {
        return (int)value;
    }
    public static byte IntToByte(int value)
    {
        return (byte)value;
    }
    public static float IntToFloat(int value)
    {
        return (float)value;
    }
    public static bool StringToBool(string value)
    {
        bool tempVal = false;
        bool.TryParse(value, out tempVal);
        return tempVal;
    }
    public static int StringToInt(string value)
    {
        int tempVal = 0;
        int.TryParse(value, out tempVal);
        return tempVal;
    }
    public static float StringToFloat(string value)
    {
        float tempVal = 0;
        float.TryParse(value, out tempVal);
        return tempVal;
    }

    public static string IntToString(int value)
    {
        return value.ToString();
    }

    public static byte StringToFloatToByte(string value)
    {
        float tempVal = 0;
        float.TryParse(value, out tempVal);
        return (byte)tempVal;
    }

    public static byte StringToIntToByte(string value)
    {
        int tempVal = 0;
        int.TryParse(value, out tempVal);
        return (byte)tempVal;
    }
}
