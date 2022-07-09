using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemResourceManager : MonoBehaviour
{
	private GameObject SysSoftware;
	private GameObject Hardware;

	private RAM ram;
	//private  gpu;

	private TaskViewer tv;


	private GameObject Crash;
	private SysCrashMan SCM;

	public float CPUUsage;
	public float MemoryUsage;
	public float GraphicsUsage;
	public float DiskUsage;
	public float NetworkUsage;

    public string ProgramName;
	public string ApplicationName;

	public int SelectedProgram;
	public int SelectedProgramsWindowID;

	// Use this for initialization
	void Start () 
	{
		SysSoftware = GameObject.Find("System");
		Hardware = GameObject.Find("Hardware");


		Crash = GameObject.Find("Crash");
		SCM = Crash.GetComponent<SysCrashMan>();

		ram = Hardware.GetComponent<RAM>();
		tv = SysSoftware.GetComponent<TaskViewer>();
	}

	void ForceCrash()
	{
		SCM.StopCodeWord = "MANUAL_LAUNCH_CRASH";
		SCM.StopCodeNumber = "0xD34DD13D";
		SCM.CodeDetail = "K3RN31-94N1C-8007-U23R-D3F";
		SCM.ExtraDetail = "14M-7H3-D327R0Y3R-0F-7H12-02";
		SCM.enabled = true;
		SCM.Type = "Test";
		this.enabled = false;
	}

	public void AddProgramUsage(string ProgramName, string ApplicationName)
	{
		float CPUUsagePercent = 0;
		//CPUUsagePercent = CPUUsage / GameControl.control.Gateway.InstalledCPU[0].MaxSpeed * 100;

		float RAMUsagePercent = 0;
		RAMUsagePercent = MemoryUsage / ram.MaxRAM * 100;

		//float GPUUsagePercent = 0;
		//GPUUsagePercent = GraphicsUsage / gpu.MaxRAM * 100;

        //for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
        //{
        //    GameControl.control.Gateway.InstalledCPU[i].Usage += CPUUsage;
        //}
		ram.UsedRAM += MemoryUsage;
        tv.RunningTasks.Add(new TasksSystem(ProgramName, ApplicationName, SelectedProgramsWindowID, CPUUsagePercent, RAMUsagePercent, GraphicsUsage, DiskUsage, NetworkUsage));
		//tv.RunningApplications.Add(ApplicationName);
		//tv.CPUList.Add(CPUUsagePercent);
		//tv.MemoryList.Add(RAMUsagePercent);
		//tv.GraphicsList.Add(GraphicsUsage);
		//tv.DiskList.Add(DiskUsage);
		//tv.NetworkList.Add(NetworkUsage);
		//tv.RunningApplicationsWindowID.Add(SelectedProgramsWindowID);
		ResetProgramUsage();
	}

	void Update()
	{
		//if(!tv.RunningTasks.Contains(ApplicationName))
		//{
		//	ApplicationName = "";
		//}
		if(SelectedProgram > tv.RunningTasks.Count-1)
		{
			SelectedProgram = 0;
		}
		//if(tv.RunningApplications.Contains("System OS"))
		//{
		//	if(!tv.RunningApplications.Contains("System OS"))
		//	{
		//		ForceCrash();
		//	}
		//}
	}

	public void UpdateProgramUsage()
	{
		float CPUUsagePercent = 0;
		//CPUUsagePercent = CPUUsage / cpu.MaxCPUSpeed * 100;

		float RAMUsagePercent = 0;
		RAMUsagePercent = MemoryUsage / ram.MaxRAM * 100;

		for (int i = 0; i < tv.RunningTasks.Count; i++) 
		{
			if (tv.RunningTasks[i].RunningApplications == ApplicationName)
			{
				tv.RunningTasks[i].CPU = CPUUsagePercent;
				tv.RunningTasks[i].Memory = RAMUsagePercent;
				//tv.GraphicsList.Add(GraphicsUsage);
				//tv.DiskList.Add(DiskUsage);
				//tv.NetworkList.Add(NetworkUsage);
			}
		}
	}

	public void ResetProgramUsage()
	{
		CPUUsage = 0;
		MemoryUsage = 0;
		GraphicsUsage = 0;
		DiskUsage = 0;
		NetworkUsage = 0;
		SelectedProgramsWindowID = 0;
		SelectedProgram = 0;
		ProgramName = "";
		ApplicationName = "";

	}

	public void RemoveProgramUsage()
	{
		for (int i = 0; i < tv.RunningTasks.Count; i++) 
		{
			if (tv.RunningTasks[i].RunningApplications == ApplicationName)
			{
				SelectedProgram = i;
			}
		}
        //for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
        //{
        //    GameControl.control.Gateway.InstalledCPU[i].Usage -= CPUUsage;
        //}
        ram.UsedRAM -= MemoryUsage;
        tv.RunningTasks.RemoveAt(SelectedProgram);
		ResetProgramUsage();
		return;
	}
}
