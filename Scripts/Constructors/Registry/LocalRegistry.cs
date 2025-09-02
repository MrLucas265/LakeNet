using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRegistry
{
	public static void AddNewKey(string PersonsName, int PID, string KeyName)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count;i++)
		{
			if (people[i].Name == PersonsName)
			{
                people[i].Gateway.RunningPrograms[PID].LocalRegister.Add(new RegistrySystem(KeyName));
            }
		}
	}

	public static void AddNewValue(string PersonsName, int PID, string KeyName, string ValueName)
	{
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
		{
			if (people[i].Name == PersonsName)
			{
				for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
				{
					if(people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
					{
						people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Add(new RegistryDataSystem(ValueName));
                    }
				}
            }
        }
	}

    public static void SetFMSData(string PersonsName, int PID, string KeyName, string ValueName, FileUtilitySystem FMS)
    {
        var people = PersonController.control.People;
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];

                                regdata.FMS = FMS;
                            }
                        }
                    }
                }
            }
        }
    }

    public static FileUtilitySystem GetFMSData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        FileUtilitySystem Test = null;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.FMS;
                                if (DataType != null)
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static int GetFMSListDataCount(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.FMSList;

                                Test = DataType.Count;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }


    public static void SetDoubleData(string PersonsName, int PID, string KeyName, string ValueName, double DoubleData)
	{
		var people = PersonController.control.People;
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];

                                regdata.DataDouble = DoubleData;
                            }
                        }
                    }
                }
            }
        }
	}

	public static double GetDoubleData(string PersonsName, int PID, string KeyName, string ValueName)
	{
        var people = PersonController.control.People;

        double Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.DataDouble;
                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetColorData(string PersonsName, int PID, string KeyName, string ValueName, SColor color)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
								regdata.ColorData = color;
                            }
                        }
                    }
                }
            }
        }
    }

    public static SColor GetColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        SColor Test = null;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorData;
                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static Color32 Get32ColorData(string PersonsName, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        Color32 Test = new Color32();

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.Registry.Count; j++)
                {
                    if (people[i].Gateway.Registry[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.Registry[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.Registry[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.Registry[j].Values[k];
                                var DataType = regdata.ColorData;
                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetRedColorData(string PersonsName, int PID, string KeyName, string ValueName, byte FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorData.r = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetGreenColorData(string PersonsName, int PID, string KeyName, string ValueName, byte FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorData.g = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetBlueColorData(string PersonsName, int PID, string KeyName, string ValueName, byte FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorData.b = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetAlphaColorData(string PersonsName, int PID, string KeyName, string ValueName, byte FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorData.a = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static byte GetRedColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        byte Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorData.r;
                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static byte GetGreenColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        byte Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorData.g;
                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static byte GetBlueColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        byte Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorData.b;
                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static byte GetAlphaColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        byte Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorData.a;
                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetAlphaFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorFloatData.Alpha = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, ColorSystem FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorFloatData = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetRedFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorFloatData.Red = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetGreenFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorFloatData.Green = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }
    public static void SetBlueFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.ColorFloatData.Blue = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static float GetAlphaFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        float Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorFloatData.Alpha;
                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static float GetRedFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        float Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorFloatData.Red;

                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static float GetGreenFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        float Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorFloatData.Green;

                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }
    public static float GetBlueFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        float Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorFloatData.Blue;

                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static ColorSystem GetFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        ColorSystem Test = new ColorSystem();

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ColorFloatData;

                                if (DataType != null)
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void AddStringListData(string PersonsName, int PID, string KeyName, string ValueName, string StringData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.StringListData;

                                DataType.Add(StringData);
                            }
                        }
                    }
                }
            }
        }
    }

    public static string GetStringListData(string PersonsName, int PID, string KeyName, string ValueName, int Array)
    {
        var people = PersonController.control.People;

        string Test = "";

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.StringListData;

                                if (DataType != null)
                                    Test = DataType[Array];
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void RemoveAtStringListData(string PersonsName, int PID, string KeyName, string ValueName, int Array)
    {
        var people = PersonController.control.People;

        //string Test = "";

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.StringListData;

                                DataType.RemoveAt(Array);
                            }
                        }
                    }
                }
            }
        }
        //return Test;
    }

    public static bool StringDataListContains(string PersonsName, int PID, string KeyName, string ValueName, string StringData)
    {
        var people = PersonController.control.People;

        bool Test = false;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.StringListData;

                                Test = DataType.Contains(StringData);
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void RemoveAllStringListData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        //bool Test = false;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.StringListData;

                                DataType.Clear();
                            }
                        }
                    }
                }
            }
        }
        //return Test;
    }

    public static int GetStringListDataCount(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.StringListData;

                                Test = DataType.Count;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static int GetLastStringListData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.StringListData;

                                Test = DataType.Count-1;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void AddMenuButtonData(string PersonsName, int PID, string KeyName, string ValueName, MenuButtonSystem MenuButttonData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.MenuButtonData;

                                DataType.Add(MenuButttonData);
                            }
                        }
                    }
                }
            }
        }
    }

    public static void RemoveMenuButtonData(string PersonsName, int PID, string KeyName, string ValueName, string MenuData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                for (int l = 0; l < regdata.MenuButtonData.Count; l++)
                                {
                                    if (regdata.MenuButtonData[l].Name == MenuData)
                                    {
                                        regdata.MenuButtonData.RemoveAt(l);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public static bool MenuDataListContains(string PersonsName, int PID, string KeyName, string ValueName, string MenuData)
    {
        var people = PersonController.control.People;

        bool Test = false;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                for(int l = 0; l < regdata.MenuButtonData.Count;l++)
                                {
                                    if (regdata.MenuButtonData[l].Name == MenuData)
                                    {
                                        Test = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static MenuButtonSystem GetMenuButtonData(string PersonsName, int PID, string KeyName, string ValueName, int Array)
    {
        var people = PersonController.control.People;

        MenuButtonSystem Test = new MenuButtonSystem();

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.MenuButtonData;

                                if (DataType != null)
                                    Test = DataType[Array];
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static int GetMenuButtonCountData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.MenuButtonData;

                                Test = DataType.Count;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }


    public static void SetMenuButtonPosXData(string PersonsName, int PID, string KeyName, string ValueName, int array, float PosX)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.MenuButtonData;

                                DataType[array].PosX = PosX;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void RemoveAllMenuButtonData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.MenuButtonData;

                                DataType.Clear();
                            }
                        }
                    }
                }
            }
        }
    }

    public static ProgramSystemv2 GetProgramData(string PersonsName, int PID, string KeyName, string ValueName, int Array)
    {
        var people = PersonController.control.People;

        ProgramSystemv2 Test = new ProgramSystemv2();

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ProgramData;

                                if (DataType != null)
                                    Test = DataType[Array];
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static int GetProgramDataCount(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ProgramData;

                                Test = DataType.Count;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }


    public static void AddProgramData(string PersonsName, int PID, string KeyName, string ValueName,ProgramSystemv2 ProgramData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ProgramData;

                                DataType.Add(ProgramData);
                            }
                        }
                    }
                }
            }
        }
    }

    public static void RemoveAllProgramData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ProgramData;

                                DataType.Clear();
                            }
                        }
                    }
                }
            }
        }
    }

    public static void RemoveAtProgramData(string PersonsName, int PID, string KeyName, string ValueName, int Array)
    {
        var people = PersonController.control.People;

        //string Test = "";

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ProgramData;

                                DataType.RemoveAt(Array);
                            }
                        }
                    }
                }
            }
        }
        //return Test;
    }

    public static bool ProgramDataContains(string PersonsName, int PID, string KeyName, string ValueName, ProgramSystemv2 ProgramData)
    {
        var people = PersonController.control.People;

        bool Test = false;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ProgramData;

                                Test = DataType.Contains(ProgramData);
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static int GetLastProgramDataCount(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.ProgramData;

                                Test = DataType.Count - 1;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static string GetStringData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        string Test = "";

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.DataString;

                                if (DataType != null)
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetStringData(string PersonsName, int PID, string KeyName, string ValueName, string StringData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataString = StringData;
                            }
                        }
                    }
                }
            }
        }
    }
    public static int GetIntData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.DataInt;

                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetIntData(string PersonsName, int PID, string KeyName, string ValueName, int IntData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataInt = IntData;
                            }
                        }
                    }
                }
            }
        }
    }


    public static bool GetBoolData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        bool Test = false;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.DataBool;

                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetBoolData(string PersonsName, int PID, string KeyName, string ValueName, bool BoolData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataBool = BoolData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static SRect GetRectData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        SRect Test = new SRect(new Rect());

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.DataRect;

                                if (DataType != null)
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetRectData(string PersonsName, int PID, string KeyName, string ValueName, SRect RectData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataRect = RectData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetXRectData(string PersonsName, int PID, string KeyName, string ValueName, float x)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataRect.x = x;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetYRectData(string PersonsName, int PID, string KeyName, string ValueName, float y)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataRect.y = y;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetWidthRectData(string PersonsName, int PID, string KeyName, string ValueName, float Width)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataRect.width = Width;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetHeightRectData(string PersonsName, int PID, string KeyName, string ValueName, float Height)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataRect.height = Height;
                            }
                        }
                    }
                }
            }
        }
    }

    public static SVector3 GetVector3Data(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        SVector3 Test = new SVector3(new Vector3());

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.DataVector3;

                                if (DataType != null)
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetVector3Data(string PersonsName, int PID, string KeyName, string ValueName, SVector3 Vector3Data)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataVector3 = Vector3Data;
                            }
                        }
                    }
                }
            }
        }
    }

    public static SVector2 GetVector2Data(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        SVector2 Test = new SVector2(new Vector2());

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.DataVector2;

                                if (DataType != null)
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetVector2Data(string PersonsName, int PID, string KeyName, string ValueName, SVector2 Vector2Data)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataVector2 = Vector2Data;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetVector2XData(string PersonsName, int PID, string KeyName, string ValueName, float x)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataVector2.x = x;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetVector2YData(string PersonsName, int PID, string KeyName, string ValueName, float y)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataVector2.y = y;
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetFloatData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
    {
        var people = PersonController.control.People;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                regdata.DataFloat = FloatData;
                            }
                        }
                    }
                }
            }
        }
    }

    public static float GetFloatData(string PersonsName, int PID, string KeyName, string ValueName)
    {
        var people = PersonController.control.People;

        float Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Values[k];
                                var DataType = regdata.DataFloat;

                                if (!DataType.Equals(null))
                                    Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }
}
