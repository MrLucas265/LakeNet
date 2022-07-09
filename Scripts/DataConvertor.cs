using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static int StringToInt(string value)
    {
        int tempVal = 0;
       int.TryParse(value,out tempVal);
        return tempVal;
    }
    public static float StringToFloat(string value)
    {
        float tempVal = 0;
        float.TryParse(value, out tempVal);
        return tempVal;
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
