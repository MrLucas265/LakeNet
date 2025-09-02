using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program 
{
	public static ProgramRequest Run(string ProgramName, string ProgramTarget, string PersonsName)
	{
		return new ProgramRequest(ProgramName, ProgramTarget, PersonsName);
	}
}
