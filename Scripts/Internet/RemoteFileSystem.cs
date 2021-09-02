using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RemoteFileSystem
{
	public string Name;
	public string Sender;
	public string Date;
	public string Content;
	public string Location;
	public string Target;
	public ProgramType Type;
	public float Encryption;
	public float Free;
	public float Used;
	public float Capacity;
	public float Health;
	public float Version;
	public bool Infected;

	public enum ProgramType
	{
		Null,
		Exe,
		Ins,
		Rar,
		Zip,
		Txt,
		Fdl,
		File,
		Dir,
		OS,
		Web,
		RealWeb,
		Real,
		Voice
	}

	public RemoteFileSystem(string name, string sender, string date, string content, string location, string target, float encryption, float free, float used, float capacity, float health, float version, bool infected, ProgramType type) //,Texture2D icon)
	{
		Name = name;
		Sender = sender;
		Date = date;
		Content = content;
		Location = location;
		Target = target;
		Type = type;
		Encryption = encryption;
		Free = free;
		Used = used;
		Capacity = capacity;
		Health = health;
		Version = version;
		Infected = infected;
		//DisplayPos = displaypos;
	}
}