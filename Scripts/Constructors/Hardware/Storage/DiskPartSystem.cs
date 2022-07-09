using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiskPartSystem
{
	public string Label;
	public string DriveLetter;
	public long Size;
	public long Used;
	public long Free;
	public int DiskDriveID;
	public List<ProgramSystemv2> Files = new List<ProgramSystemv2>();

	public DiskPartSystem(string label, string driveletter, long size,long used,long free,int diskdriveid)
	{
		Label = label;
		DriveLetter = driveletter;
		Size = size;
		Used = used;
		Free = free;
		DiskDriveID = diskdriveid;
	}
}