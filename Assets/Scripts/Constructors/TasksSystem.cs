using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TasksSystem
{
    public string ProgramName;
    public string RunningApplications;
    public int RunningApplicationsWindowID;
    public float CPU;
    public float Memory;
    public float Graphics;
    public float Disk;
    public float Network;

    public TasksSystem(string programname, string runningapplications, int runningapplicationswindowid, float cpu, float memory, float graphics, float disk, float network)
    {
        ProgramName = programname;
        RunningApplications = runningapplications;
        RunningApplicationsWindowID = runningapplicationswindowid;
        CPU = cpu;
        Memory = memory;
        Graphics = graphics;
        Disk = disk;
        Network = network;
    }
}