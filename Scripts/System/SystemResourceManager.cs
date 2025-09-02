using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemResourceManager : MonoBehaviour
{
	public float CPUUsage;
	public float MemoryUsage;
	public float GraphicsUsage;
	public float DiskUsage;
	public float NetworkUsage;

    public string ProgramName;
	public string ApplicationName;

	public int SelectedProgram;
	public int SelectedProgramsWindowID;

	public static void CalculateProgramUsage()
    {
		for (int i = 0; i < PersonController.control.People.Count;i++)
        {
			PersonController.control.People[i].Gateway.TotalUsedSystemResources.CPUUsage = 0;
			PersonController.control.People[i].Gateway.TotalUsedSystemResources.DiskUsage = 0;
			PersonController.control.People[i].Gateway.TotalUsedSystemResources.GraphicsUsage = 0;
			PersonController.control.People[i].Gateway.TotalUsedSystemResources.MemoryUsage = 0;
			PersonController.control.People[i].Gateway.TotalUsedSystemResources.NetworkUsage = 0;

			for (int j = 0; j < PersonController.control.People[i].Gateway.RunningPrograms.Count; j++)
			{
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.CPUUsage = 0;
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.DiskUsage = 0;
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.GraphicsUsage = 0;
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.MemoryUsage = 0;
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.NetworkUsage = 0;
			}

			for (int j = 0; j < PersonController.control.People[i].Gateway.RunningPrograms.Count; j++)
			{
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.CPUUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].CurrentRMS.CPUUsage + PersonController.control.People[i].Gateway.RunningPrograms[j].InitalRMS.CPUUsage;
                PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.DiskUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].CurrentRMS.DiskUsage + PersonController.control.People[i].Gateway.RunningPrograms[j].InitalRMS.DiskUsage;
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.GraphicsUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].CurrentRMS.GraphicsUsage + PersonController.control.People[i].Gateway.RunningPrograms[j].InitalRMS.GraphicsUsage;
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.MemoryUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].CurrentRMS.MemoryUsage + PersonController.control.People[i].Gateway.RunningPrograms[j].InitalRMS.MemoryUsage;
				PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.NetworkUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].CurrentRMS.NetworkUsage + PersonController.control.People[i].Gateway.RunningPrograms[j].InitalRMS.NetworkUsage;

				PersonController.control.People[i].Gateway.TotalUsedSystemResources.CPUUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.CPUUsage;
				PersonController.control.People[i].Gateway.TotalUsedSystemResources.DiskUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.DiskUsage;
				PersonController.control.People[i].Gateway.TotalUsedSystemResources.GraphicsUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.GraphicsUsage;
				PersonController.control.People[i].Gateway.TotalUsedSystemResources.MemoryUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.MemoryUsage;
				PersonController.control.People[i].Gateway.TotalUsedSystemResources.NetworkUsage += PersonController.control.People[i].Gateway.RunningPrograms[j].TotalRMS.NetworkUsage;
			}
		}
    }
}
