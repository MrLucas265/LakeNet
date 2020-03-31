using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CLICMDS 
{
	public string Name;
	public string Func;

	public CLICMDS(string name, string func)
	{
		Name = name;
		Func = func;
	}
}