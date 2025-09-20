using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRegistryv2
{
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = StringData;
                            }
                        }
                    }
                }
            }
        }
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //regdata.DataString = DataConverter.SRectToString(RectData);
                                regdata.DataString = Vector3Data.ToString();
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //var DataType = DataConverter.StringToSRect(regdata.DataString);
                                var DataType = DataConverter1.StringTo<SVector3>(regdata.DataString);

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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //regdata.DataString = DataConverter.SRectToString(RectData);
                                regdata.DataString = Vector2Data.ToString();
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

        SVector2 Test = new SVector2(new SVector2(new Vector3()));

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //var DataType = DataConverter.StringToSRect(regdata.DataString);
                                var DataType = DataConverter1.StringTo<SVector2>(regdata.DataString);

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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //regdata.DataString = DataConverter.SRectToString(RectData);
                                regdata.DataString = RectData.ToString();
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

        SRect Test = new SRect(new SRect(new Rect()));

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //var DataType = DataConverter.StringToSRect(regdata.DataString);
                                var DataType = DataConverter1.StringTo<SRect>(regdata.DataString);

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

    public static void AddStringListData(string PersonsName, int PID, string KeyName, string ValueName,string StringData)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = $"{regdata.DataString}{StringData},";

                            }
                        }
                    }
                }
            }
        }
    }

    public static void RemoveAllStringListData(string PersonsName, int PID, string KeyName, string ValueName)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = "";

                            }
                        }
                    }
                }
            }
        }
    }

    public static void AddIntListData(string PersonsName, int PID, string KeyName, string ValueName, int StringData)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = $"{regdata.DataString}{StringData},";

                            }
                        }
                    }
                }
            }
        }
    }

    public static void AddFloatListData(string PersonsName, int PID, string KeyName, string ValueName, float StringData)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = $"{regdata.DataString}{StringData},";

                            }
                        }
                    }
                }
            }
        }
    }

    public static void AddBoolListData(string PersonsName, int PID, string KeyName, string ValueName, bool StringData)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = $"{regdata.DataString}{StringData},";

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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                var DataType = DataConverter1.StringTo<string[]>(regdata.DataString);

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

    public static bool GetBoolListData(string PersonsName, int PID, string KeyName, string ValueName, int Array)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                var DataType = DataConverter1.StringTo<string[]>(regdata.DataString);

                                if (DataType != null)
                                    Test = DataConverter.StringToBool(DataType[Array]);
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static float GetFloatListData(string PersonsName, int PID, string KeyName, string ValueName, int Array)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                var DataType = DataConverter1.StringTo<string[]>(regdata.DataString);

                                if (DataType != null)
                                    Test = DataConverter.StringToFloat(DataType[Array]);
                            }
                        }
                    }
                }
            }
        }
        return Test;
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                var DataType = DataConverter1.StringListCount(regdata.DataString);
                                Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetBoolData(string PersonsName, int PID, string KeyName, string ValueName, bool StringData)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = StringData.ToString();
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //var DataType = regdata.DataString;
                                var DataType = DataConverter1.StringTo<bool>(regdata.DataString);
                                Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetIntData(string PersonsName, int PID, string KeyName, string ValueName, int StringData)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = StringData.ToString();
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //var DataType = regdata.DataString;
                                var DataType = DataConverter1.StringTo<int>(regdata.DataString);
                                Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void SetFloatData(string PersonsName, int PID, string KeyName, string ValueName, float StringData)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataString = StringData.ToString();
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                //var DataType = regdata.DataString;
                                var DataType = DataConverter1.StringTo<float>(regdata.DataString);
                                Test = DataType;
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static void RemoveAtListData(string PersonsName, int PID, string KeyName, string ValueName, int Array)
    {
        var people = PersonController.control.People;

        List<string> Test1 = new List<string>();

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.RunningPrograms[PID].LocalRegister.Count; j++)
                {
                    if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].KeyName == KeyName)
                    {
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                var DataType = DataConverter1.StringTo<string[]>(regdata.DataString);

                                for (int l = 0; l < DataConverter1.StringListCount(regdata.DataString); l++)
                                {
                                    Test1.Add(DataType[l]);
                                }

                                if (Test1.Count >= DataConverter1.StringListCount(regdata.DataString))
                                {
                                    regdata.DataString = "";
                                    Test1.RemoveAt(Array);
                                }

                                for (int m = 0; m < Test1.Count; m++)
                                {
                                    regdata.DataString = $"{regdata.DataString}{Test1[m]},";
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SetStringDataType(string PersonsName, int PID, string KeyName, string ValueName, string StringData)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                regdata.DataTypeString = StringData;
                            }
                        }
                    }
                }
            }
        }
    }
    public static string GetStringDataType(string PersonsName, int PID, string KeyName, string ValueName)
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
                        for (int k = 0; k < people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2.Count; k++)
                        {
                            if (people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k].ValueName == ValueName)
                            {
                                var regdata = people[i].Gateway.RunningPrograms[PID].LocalRegister[j].Valuesv2[k];
                                var DataType = regdata.DataTypeString;

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
}
