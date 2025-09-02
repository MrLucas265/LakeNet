using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

	public static void CheckAllDrives()
	{
        if (PersonController.control.People.Count > 0)
        {
            for (int i = 0; i < PersonController.control.People.Count; i++)
            {
                for (int j = 0; j < PersonController.control.People[i].Gateway.StorageDevices.Count; j++)
                {
                    if (PersonController.control.People[i].Gateway.Status.On == true)
                    {
                        if (PersonController.control.People[i].Gateway.Timer.TimeRemain <= 0)
                        {
                            Health(i, j);
                            FileSizesCheck(i, j);
                        }
                    }
                }
            }
        }
    }

	static void Health(int PeopleIndex, int StorageDevice)
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

	static void FileSizesCheck(int i, int j)
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
                    var programname = PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Location
								+ "/" + PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Name +
								"." + PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension;

                    if (!Registry.StringDataListContains(PersonController.control.People[i].Name, "Core", "InstalledPrograms", programname))
                    {
                        Registry.AddStringListData(PersonController.control.People[i].Name, "Core", "InstalledPrograms", programname);
                    }

                    //if (!Registry.ProgramDataContains(PersonController.control.People[i].Name, "Core", "InstalledPrograms", PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n]))
                    //{
                    //    Registry.AddProgramData(PersonController.control.People[i].Name, "Core", "InstalledPrograms",
                    //        PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n]);
                    //}


                    if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.exe ||
						PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.txt ||
						PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.file ||
						PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.os ||
						PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.ins)
					{
						if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Location.StartsWith(PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].DriveLetter))
						{
							PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Used += PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Used;
							PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Free = PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Size - PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Used;
						}
					}

					if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Extension == ProgramSystemv2.FileExtension.dir)
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
