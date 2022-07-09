using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OSOptionsSystem
{
	public bool DisableColourOption;
	public bool Selected;
	public bool FirstBoot;
	public bool Safemode;
	public bool DataSuffix;
	public bool EnableDesktopEnviroment;
	public bool StoragePartition;

	public OSOptionsSystem()
	{

	}
	public OSOptionsSystem(bool storagepartition)
	{
		StoragePartition = storagepartition;
	}
}