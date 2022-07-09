using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardDrives : MonoBehaviour 
{
	public float PowerUsage;
	public float DriveSpeed;

	public bool check;

	public float UsedSpace;

	public float CustomSpeed;

    public float Timer;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(PersonController.control.People.Count > 0)
		{
			for (int i = 0; i < PersonController.control.People.Count; i++)
			{
				for (int j = 0; j < PersonController.control.People[i].Gateway.StorageDevices.Count; j++)
				{
					if (PersonController.control.People[i].Gateway.Status.On == true)
					{
						if (PersonController.control.People[i].Gateway.Timer.TimeRemain <= 0)
						{
							Health(i,j);
							FileSizesCheck(i, j);
						}
					}
				}
			}
		}
	}

	void Health(int PeopleIndex, int StorageDevice)
	{
		if (PersonController.control.People[PeopleIndex].Gateway.StorageDevices[StorageDevice].CurrentHealth > 0)
		{
			PersonController.control.People[PeopleIndex].Gateway.StorageDevices[StorageDevice].CurrentHealth -= PersonController.control.People[PeopleIndex].Gateway.StorageDevices[StorageDevice].DegradationRate;
			PersonController.control.People[PeopleIndex].Gateway.StorageDevices[StorageDevice].HealthPercentage = ((double)PersonController.control.People[PeopleIndex].Gateway.StorageDevices[StorageDevice].CurrentHealth * 100) / PersonController.control.People[PeopleIndex].Gateway.StorageDevices[StorageDevice].MaxHealth;
		}
		else
		{
			PersonController.control.People[PeopleIndex].Gateway.StorageDevices[StorageDevice].CurrentHealth = 0;
		}
	}

	public void FileSizesCheck(int i, int j)
	{
		PersonController.control.People[i].Gateway.StorageDevices[j].UsedSpace = 0;
		PersonController.control.People[i].Gateway.StorageDevices[j].FreeSpacePercentage = ((double)PersonController.control.People[i].Gateway.StorageDevices[j].FreeSpace * 100) / PersonController.control.People[i].Gateway.StorageDevices[j].Capacity;

		for (int l = 0; l < PersonController.control.People[i].Gateway.StorageDevices[j].OS.Count; l++)
		{
			for (int m = 0; m < PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions.Count; m++)
			{
				PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Used = 0;

				PersonController.control.People[i].Gateway.StorageDevices[j].UsedSpace += PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Size;
				PersonController.control.People[i].Gateway.StorageDevices[j].FreeSpace = PersonController.control.People[i].Gateway.StorageDevices[j].Capacity - PersonController.control.People[i].Gateway.StorageDevices[j].UsedSpace;

				for (int n = 0; n < PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files.Count; n++)
				{
					if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.Exe ||
						PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.Txt ||
						PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.File ||
						PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.OS ||
						PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.Ins)
					{
						if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Location.StartsWith(PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].DriveLetter))
						{
							PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Used += PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Used;
							PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Free = PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Size - PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Used;
						}
					}

					if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.Dir)
					{
						if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Name.StartsWith(PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].DriveLetter))
						{
							PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Used = PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Used;
							PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Free = PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Size - PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Used;
							PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Capacity = PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Size;
						}
					}
				}
			}
		}
	}
}
