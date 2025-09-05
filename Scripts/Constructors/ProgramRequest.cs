using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgramRequest
{
    public string ProgramName;
    public string ProgramTarget;
    public string PersonName;
    public ProgramSystemv2.ProgramTypes ProgramType;

    public ProgramRequest(string programname, ProgramSystemv2.ProgramTypes programtype, string personname)
    {
        ProgramName = programname;
        ProgramType = programtype;
        PersonName = personname;
    }

    public ProgramRequest(string programname, string programtarget, string personname, ProgramSystemv2.ProgramTypes programtype)
    {
        ProgramName = programname;
        ProgramTarget = programtarget;
        PersonName = personname;
        ProgramType = programtype;
    }

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