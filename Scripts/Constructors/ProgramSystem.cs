using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgramSystem
{
	public string Name;
	public string Sender;
	public string Creator;
	public string Date;
	public string Content;
	public string Description;
	public string Location;
	public string Target;
	public string IconLocation;
	public string PictureLocation;
	public FileExtension Extension;
	public FileExtension FileInstallExtension;
	public float Encryption;
	public float Free;
	public float Used;
	public float Capacity;
	public float GPUUsage;
	public float CPUUsage;
	public float RAMUsage;
	public float Health;
	public float Version;
	public float Price;
	public float Value;
	public int PermisionLevel;
	public int ProgramID; // Could be useful at somepoint
	public int Int1; // could use int 1 to be program id if we need it
	public int Int2;
	public int Int3;
	public int IntState;
	public bool Infected;
	public bool BoolState; // Might need later
	public bool Bool1;
	public bool Bool2;
	public bool Bool3;
	public List<InfectionSystem> Infections = new List<InfectionSystem>();
	public List<FileType> Type = new List<FileType>();

	public enum FileExtension
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
		Voice,
		Database,
		Black,
		White
	}

	public enum FileType
	{
		Null,
		Legal,
		Illegal,
		Hacks,
		Research,
		Data,
		Program,
		System,
		Antivirus,
		Virus,
		DocumentReaders,
	}

	public ProgramSystem(string name, string sender, string creator, string date, string content, string description, string location, string target, string iconlocation, string picturelocation,
	FileExtension extension, FileExtension fileinstallextension, float encryption, float free, float used, float capacity, float gpuusage, float cpuusage, float ramusage, float health,
	float version, float price, float value, int permissionlevel, int int1, int int2, int int3, int intstate, bool infected, bool bool1, bool bool2, bool bool3, List<InfectionSystem> infections,
	List<FileType> type) //,Texture2D icon)
	{
		Name = name;
		Sender = sender;
		Creator = creator;
		Date = date;
		Content = content;
		Description = description;
		Location = location;
		Target = target;
		IconLocation = iconlocation;
		PictureLocation = picturelocation;
		Extension = extension;
		FileInstallExtension = fileinstallextension;
		Encryption = encryption;
		Free = free;
		Used = used;
		Capacity = capacity;
		GPUUsage = gpuusage;
		CPUUsage = cpuusage;
		RAMUsage = ramusage;
		Health = health;
		Version = version;
		Price = price;
		Value = value;
		PermisionLevel = permissionlevel;
		Int1 = int1;
		Int2 = int2;
		Int3 = int3;
		IntState = intstate;
		Infected = infected;
		Bool1 = bool1;
		Bool2 = bool2;
		Bool3 = bool3;
		Infections = infections;
		Type = type;
	}
}