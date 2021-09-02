using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillSystem
{
	public string Name;
	public string DisplayName;
	public int CurrentXP;
	public int CurrentLevel;
	public int LevelRequirement;
	public float LevelMod;

	public SkillSystem(string name,string displayname,int curentxp,int currentlevel,int levelrequirement,float levelmod)
	{
		Name = name;
		DisplayName = displayname;
		CurrentXP = curentxp;
		CurrentLevel = currentlevel;
		LevelRequirement = levelrequirement;
		LevelMod = levelmod;
	}
}