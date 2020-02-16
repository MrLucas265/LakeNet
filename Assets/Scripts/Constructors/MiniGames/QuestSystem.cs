using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestSystem
{
	public string Name;
	public string File;
	public string Target;
	public string Address;
	public string Desc;
	public string Cat;
	public int Cash;
	public int Rep;
	public QuestType Type;

	public enum QuestType
	{
		JDelete,
		PJDelete,
		PJCopy,
		JCopy,
		UDelete,
		PUDelete,
		PTCopy,
		TCopy,
		PTDelete,
		TDelete,
		AEdit
	}

	public QuestSystem(string name,string file,string target,string address,string desc,string cat,int cash,int rep, QuestType type)
	{
		Name = name;
		File = file;
		Target = target;
		Address = address;
		Desc = desc;
		Cat = cat;
		Cash = cash;
		Rep = rep;
		Type = type;
	}
}