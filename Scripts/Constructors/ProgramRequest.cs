using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgramRequest
{
    public string ProgramName;
    public string ProgramTarget;
    public string PersonName;

    public ProgramRequest(string programname, string programtarget, string personname)
    {
        ProgramName = programname;
        ProgramTarget = programtarget;
        PersonName = personname;
    }

    public ProgramRequest(string programname, string personname)
    {
        ProgramName = programname;
        PersonName = personname;
    }

    public ProgramRequest()
    {
    }
}