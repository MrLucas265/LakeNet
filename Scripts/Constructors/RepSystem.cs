using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RepSystem
{
	public string Name;
	public float CurrentRep;
	public float RepLevel;
	public float RepLevelRequirement;
	public float RepLevelMod;
	public float Standings;
	public int Privileges;

	public RepSystem(string name, float currentrep, float replevel,float replevelreq,float replevelmod, float standings,int privileges)
	{
		Name = name;
		CurrentRep = currentrep;
		RepLevel = replevel;
		RepLevelRequirement = replevelreq;
		RepLevelMod = replevelmod;
		Standings = standings;
		Privileges = privileges;
	}
}