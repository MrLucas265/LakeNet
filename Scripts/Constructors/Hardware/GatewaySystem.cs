using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatewaySystem
{
    public string Name;
    public List<RegistrySystem> Registry = new List<RegistrySystem>();
    public List<TasksSystem> RunningTasks = new List<TasksSystem>();
    public List<WindowConSys> RunningPrograms = new List<WindowConSys>();
    public List<EmailSystem> Emails = new List<EmailSystem>();
    public List<string> PartitionList = new List<string>();
    public OperatingSystems CurrentOS = new OperatingSystems();
    public GatewayStatusSystem Status = new GatewayStatusSystem();
    public TimerSystem Timer = new TimerSystem();
    public DateSystem DateTime;

    public MotherboardSystem Motherboard = new MotherboardSystem();
    public List<StorageDevice> StorageDevices = new List<StorageDevice>();
    public List<RamSystem> RAM = new List<RamSystem>();
    public List<PowerSupplySystem> PSU = new List<PowerSupplySystem>();
    public List<GPUSystem> GPU = new List<GPUSystem>();
    public List<ModemSystem> Modem = new List<ModemSystem>();
    public List<CPUSystem> CPU = new List<CPUSystem>();

    public ResourceManagerSystem TotalUsedSystemResources;
}
