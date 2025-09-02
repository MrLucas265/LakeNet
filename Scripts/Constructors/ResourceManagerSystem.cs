using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceManagerSystem
{
    public float CPUUsage;
    public float MemoryUsage;
    public float GraphicsUsage;
    public float DiskUsage;
    public float NetworkUsage;

    public ResourceManagerSystem()
    {
    }

    public ResourceManagerSystem(float cpuusage)
    {
        CPUUsage = cpuusage;
    }

    public ResourceManagerSystem(float cpuusage,float memoryusage)
    {
        CPUUsage = cpuusage;
        MemoryUsage = memoryusage;
    }

    public ResourceManagerSystem(float cpuusage, float memoryusage,float graphicsusage)
    {
        CPUUsage = cpuusage;
        MemoryUsage = memoryusage;
        GraphicsUsage = graphicsusage;
    }


    public ResourceManagerSystem(float cpuusage, float memoryusage, float graphicsusage,float diskusage)
    {
        CPUUsage = cpuusage;
        MemoryUsage = memoryusage;
        GraphicsUsage = graphicsusage;
        DiskUsage = diskusage;
    }
    public ResourceManagerSystem(float cpuusage, float memoryusage, float graphicsusage, float diskusage, float networkusage)
    {
        CPUUsage = cpuusage;
        MemoryUsage = memoryusage;
        GraphicsUsage = graphicsusage;
        DiskUsage = diskusage;
        NetworkUsage = networkusage;
    }
}
