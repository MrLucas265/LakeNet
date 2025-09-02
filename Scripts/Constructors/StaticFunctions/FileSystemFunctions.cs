using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSystemFunctions
{
    public static int GetFileDriveParitionLocation(string PersonsName, string FileName, string FilePath)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.StorageDevices.Count; j++)
                {
                    for (int k = 0; k < people[i].Gateway.StorageDevices[j].OS.Count; k++)
                    {
                        if (people[i].Gateway.StorageDevices[j].OS[k].Name == people[i].Gateway.CurrentOS.Name)
                        {
                            for(int l = 0; l < people[i].Gateway.StorageDevices[j].OS[k].Partitions.Count;l++)
                            {
                                for (int m = 0; m < people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files.Count; m++)
                                {
                                    var File = people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files[m];
                                    if(File.Name == FileName && File.Location == FilePath)
                                    {
                                        Test = l;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static int GetPartition(string PersonsName, string DriveLetter)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.StorageDevices.Count; j++)
                {
                    for (int k = 0; k < people[i].Gateway.StorageDevices[j].OS.Count; k++)
                    {
                        if (people[i].Gateway.StorageDevices[j].OS[k].Name == people[i].Gateway.CurrentOS.Name)
                        {
                            for (int l = 0; l < people[i].Gateway.StorageDevices[j].OS[k].Partitions.Count; l++)
                            {
                                if (DriveLetter[0].ToString() == people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].DriveLetter)
                                {
                                    Test = l;
                                }
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static string GetFilesAtLocation(string PersonsName, string Location)
    {
        var people = PersonController.control.People;

        string Test = "";

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.StorageDevices.Count; j++)
                {
                    for (int k = 0; k < people[i].Gateway.StorageDevices[j].OS.Count; k++)
                    {
                        if (people[i].Gateway.StorageDevices[j].OS[k].Name == people[i].Gateway.CurrentOS.Name)
                        {
                            for (int l = 0; l < people[i].Gateway.StorageDevices[j].OS[k].Partitions.Count; l++)
                            {
                                for (int m = 0; m < people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files.Count; m++)
                                {
                                    var File = people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files[m];
                                    if (File.Location == Location)
                                    {
                                        Test = File.Name;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static int GetDrive(string PersonsName, string DriveLetter)
    {
        var people = PersonController.control.People;

        int Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.StorageDevices.Count; j++)
                {
                    for (int k = 0; k < people[i].Gateway.StorageDevices[j].OS.Count; k++)
                    {
                        if (people[i].Gateway.StorageDevices[j].OS[k].Name == people[i].Gateway.CurrentOS.Name)
                        {
                            for (int l = 0; l < people[i].Gateway.StorageDevices[j].OS[k].Partitions.Count; l++)
                            {
                                if (DriveLetter[0].ToString() == people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].DriveLetter)
                                {
                                    Test = j;
                                }
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }

    public static double GetDriveFreeSpace(string PersonsName,string FilePath)
    {
        var people = PersonController.control.People;

        int Drive = GetPartition(PersonsName, FilePath);

        double Test = 0;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                Test = people[i].Gateway.StorageDevices[Drive].FreeSpace;
            }
        }
        return Test;
    }

    public static bool ContainsFileAtLocation(string PersonsName, string FileName, string FilePath)
    {
        var people = PersonController.control.People;

        bool Test = false;

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.StorageDevices.Count; j++)
                {
                    for (int k = 0; k < people[i].Gateway.StorageDevices[j].OS.Count; k++)
                    {
                        if (people[i].Gateway.StorageDevices[j].OS[k].Name == people[i].Gateway.CurrentOS.Name)
                        {
                            for (int l = 0; l < people[i].Gateway.StorageDevices[j].OS[k].Partitions.Count; l++)
                            {
                                for (int m = 0; m < people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files.Count; m++)
                                {
                                    var File = people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files[m];
                                    if (File.Name == FileName && File.Location == FilePath)
                                    {
                                        Test = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return Test;
    }


    public static void AddFile(string PersonsName, ProgramSystemv2 File)
    {
        var people = PersonController.control.People;

        int Partition = GetFileDriveParitionLocation(PersonsName, File.Location, File.Location);

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.StorageDevices.Count; j++)
                {
                    for (int k = 0; k < people[i].Gateway.StorageDevices[j].OS.Count; k++)
                    {
                        if (people[i].Gateway.StorageDevices[j].OS[k].Name == people[i].Gateway.CurrentOS.Name)
                        {
                            for (int l = 0; l < people[i].Gateway.StorageDevices[j].OS[k].Partitions.Count; l++)
                            {
                                if(l == Partition)
                                {
                                    people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files.Add(File);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public static void RemoveFile(string PersonsName, ProgramSystemv2 File)
    {
        var people = PersonController.control.People;

        int Partition = GetFileDriveParitionLocation(PersonsName, File.Location, File.Location);

        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].Name == PersonsName)
            {
                for (int j = 0; j < people[i].Gateway.StorageDevices.Count; j++)
                {
                    for (int k = 0; k < people[i].Gateway.StorageDevices[j].OS.Count; k++)
                    {
                        if (people[i].Gateway.StorageDevices[j].OS[k].Name == people[i].Gateway.CurrentOS.Name)
                        {
                            for (int l = 0; l < people[i].Gateway.StorageDevices[j].OS[k].Partitions.Count; l++)
                            {
                                if (l == Partition)
                                {
                                    for (int m = 0; m < people[i].Gateway.StorageDevices[j].OS[k].Partitions.Count; m++)
                                    {
                                        if (people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files[m] == File)
                                        {
                                            people[i].Gateway.StorageDevices[j].OS[k].Partitions[l].Files.RemoveAt(m);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
