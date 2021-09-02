using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrivePatSystem
{
	public string Label;
	public string DriveLetter;
	public float Size;
	public float Used;
	public float Free;
	public int DiskDriveID;

	public DrivePatSystem(string label, string driveletter, float size,float used,float free,int diskdriveid)
	{
		Label = label;
		DriveLetter = driveletter;
		Size = size;
		Used = used;
		Free = free;
		DiskDriveID = diskdriveid;
	}
}