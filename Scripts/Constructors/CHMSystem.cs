using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CHMSystem 
{
	public string Name;
	public string Content;
	public string Location;
	public string TargetLocation;

	public FileType Type;

	public enum FileType
	{
		File,
		Folder,
		Directory
	}

	public CHMSystem(string name,string content, string location, string targetlocation,FileType type)
	{
		Name = name;
		Content = content;
		Location = location;
		TargetLocation = targetlocation;
		Type = type;
	}
}