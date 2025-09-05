using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program 
{
    public static ProgramRequest Run(string ProgramName, ProgramSystemv2.ProgramTypes ProgramType, string PersonsName)
    {
        return new ProgramRequest(ProgramName, ProgramType, PersonsName);
    }
}
