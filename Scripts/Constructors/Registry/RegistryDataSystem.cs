using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RegistryDataSystem
{
    public string ValueName;
    public string DataString;
    public int DataInt;
    public bool DataBool;
    public float DataFloat;
    public SRect DataRect;
    public SVector3 DataVector3;
    public SVector2 DataVector2;
    public List<ProgramSystemv2> ProgramData = new List<ProgramSystemv2>();
    public List<string> StringListData = new List<string>();
    public SColor ColorData;
    public ColorSystem ColorFloatData;
    public ProgramRequest Request;

    public RegistryDataSystem(string valuename, string datastring, int dataint,bool databool,float datafloat, SRect datarect, SVector3 datavector3, SVector2 datavector2)
    {
        ValueName = valuename;
        DataString = datastring;
        DataInt = dataint;
        DataBool = databool;
        DataFloat = datafloat;
        DataRect = datarect;
        DataVector3 = datavector3;
        DataVector2 = datavector2;
    }

    public RegistryDataSystem()
    { 

    }

    public RegistryDataSystem(string valuename)
    {
        ValueName = valuename;
    }
}