using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatewaySystem
{
    public string Name;
    //public string Statusl
    public OperatingSystems SelectedOS;
    public MotherboardSystem Motherboard;
    public FileSystem Files;
    public List<TasksSystem> RunningTasks = new List<TasksSystem>();
    public List<EmailSystem> Emails = new List<EmailSystem>();

    public GatewaySystem(string name, OperatingSystems selectedos, MotherboardSystem motherboard, FileSystem files, List<TasksSystem> runningtasks, List<EmailSystem> emails) //,Texture2D icon)
    {
        Name = name;
        SelectedOS = selectedos;
        Motherboard = motherboard;
        Files = files;
        RunningTasks = runningtasks;
        Emails = emails;
    }
}
