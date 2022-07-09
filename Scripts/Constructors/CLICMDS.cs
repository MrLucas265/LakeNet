using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CLICMDS 
{
	public string Name;
	public string ShortHand;
	public string Func;
	public string Desc;

	public CLICMDS(string name,string shorthand, string func,string desc)
	{
		Name = name;
		ShortHand = shorthand;
		Func = func;
		Desc = desc;
	}
}