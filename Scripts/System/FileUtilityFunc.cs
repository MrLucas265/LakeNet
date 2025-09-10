using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FileUtilityFunc
{
    public static void CheckList()
    {
        if (PersonController.control.People.Count > 0)
        {
            for (int i = 0; i < PersonController.control.People.Count; i++)
            {
                for (int j = 0; j < PersonController.control.People[i].Gateway.RunningPrograms.Count; j++)
                {
                    if (Registry.GetStringData(PersonController.control.People[i].Name, "Core", "Action").Contains(":"))
                    {
                        string[] ParseArray = Registry.GetStringData(PersonController.control.People[i].Name, "Core", "Action").Split(':');
                        if (ParseArray[0] == "Quit")
                        {
                            if (ParseArray[1] == PersonController.control.People[i].Gateway.RunningPrograms[j].ProgramName)
                            {
                                WindowManager.QuitProgram(PersonController.control.People[i].Name, ParseArray[1], PersonController.control.People[i].Gateway.RunningPrograms[j].WPN);
                                Registry.SetStringData(PersonController.control.People[i].Name, "Core", "Action", "");
                            }
                        }
                        if (ParseArray[0] == "Show" || ParseArray[0] == "Hide")
                        {
                            if (ParseArray[1] == PersonController.control.People[i].Gateway.RunningPrograms[j].ProgramName)
                            {
                                PersonController.control.People[i].Gateway.RunningPrograms[j].show = !PersonController.control.People[i].Gateway.RunningPrograms[j].show;
                                Registry.SetStringData(PersonController.control.People[i].Name, "Core", "Action", "");
                            }
                        }
                    }

                    if (PersonController.control.People[i].Gateway.RunningPrograms[j].ProcessName == "FileUtility")
                    {
                        if (LocalRegistry.GetFMSData(PersonController.control.People[i].Name, PersonController.control.People[i].Gateway.RunningPrograms[j].WPN, "FileUtility", "CurrentFile") != null)
                        {
                            if(LocalRegistry.GetFMSData(PersonController.control.People[i].Name, PersonController.control.People[i].Gateway.RunningPrograms[j].WPN, "FileUtility", "CurrentFile").Name != "")
                            {
                                Math(i, j);
                            }
                        }
                    }
                }

                if (Registry.GetStringData(PersonController.control.People[i].Name, "Core", "Action") != "")
                {
                    if (!Registry.FMSDataListContains(PersonController.control.People[i].Name, "Core", "Action", Registry.GetFMSData(PersonController.control.People[i].Name, "Core", "Action")))
                    {
                        TestCode.KeywordCheck(PersonController.control.People[i].Name, "Run:" + "FileUtility" + ";");
                        Registry.AddFUSListData(PersonController.control.People[i].Name, "Core", "Action", Registry.GetFMSData(PersonController.control.People[i].Name, "Core", "Action"));
                        Registry.SetStringData(PersonController.control.People[i].Name, "Core", "Action", "");
                    }
                }

                if (Registry.GetFUSListDataCount(PersonController.control.People[i].Name, "Core", "Action") > 0)
                {
                    if (PersonController.control.People[i].Gateway.RunningPrograms.Count > 0)
                    {
                        for (int j = 0; j < PersonController.control.People[i].Gateway.RunningPrograms.Count; j++)
                        {
                            if (PersonController.control.People[i].Gateway.RunningPrograms[j].ProcessName == "FileUtility")
                            {
                                if(LocalRegistry.GetStringData(PersonController.control.People[i].Name, j, "FileUtility", "CurrentFile") == "")
                                {
                                    LocalRegistry.SetStringData(PersonController.control.People[i].Name, j, "FileUtility", "CurrentFile", 
                                        Registry.GetFUSListData(PersonController.control.People[i].Name, "Core", "Action", 0).Name);

                                    LocalRegistry.SetFMSData(PersonController.control.People[i].Name, j, "FileUtility", "CurrentFile",
                                         Registry.GetFUSListData(PersonController.control.People[i].Name, "Core", "Action", 0));

                                   Registry.RemoveAtFUSListData(PersonController.control.People[i].Name, "Core", "Action", 0);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    static void Math(int i, int j)
    {
        var Data = LocalRegistry.GetFMSData(PersonController.control.People[i].Name, j, "FileUtility", "CurrentFile");

        //NumberFormat.Data(Data.Speed = 0.133f);
        Data.Speed = 20;

        Data.OurFileSize += Data.Speed;

        Data.Percentage = Data.OurFileSize / Data.ProgramFile.Used * 100;
        Data.ItemRemain = Data.ProgramFile.Used - Data.OurFileSize;
        Data.TimeRemainSeconds = Data.ItemRemain / Data.Speed / PersonController.control.Global.DateTime.TimeMulti;
        Data.TimeRemainSeconds = Mathf.Round(Data.TimeRemainSeconds - 1);
        Data.TimeRemainUISeconds = (int)(Data.TimeRemainSeconds % 60); // return the remainder of the seconds divide by 60 as an int
        Data.TimeRemainSeconds /= 60; // divide current time y 60 to get minutes
        Data.TimeRemainMin = (int)(Data.TimeRemainSeconds % 60); //return the remainder of the minutes divide by 60 as an int
        Data.TimeRemainSeconds /= 60; // divide by 60 to get hours
        Data.TimeRemainHour = (int)(Data.TimeRemainSeconds % 60); // return the remainder of the hours divided by 60 as an int
        Data.TimeRemainSeconds /= 24; // divide by 60 to get hours
        Data.TimeRemainDays = (int)(Data.TimeRemainSeconds % 24); // return the remainder of the hours divided by 60 as an int

        NumberFormat.Data(Data.OurFileSizeDbl = Data.ItemRemain);
        NumberFormat.Data(Data.SpeedDbl = Data.Speed);

        if (Data.ProgramFile.Used <= 0)
        {

        }
        else
        {
            if (Data.OurFileSize >= Data.ProgramFile.Used || Data.Speed >= Data.ProgramFile.Used)
            {
                switch(Data.Type)
                {
                    case FileUtilitySystem.ProgramType.Save:
                        FileSystemFunctions.AddFile(PersonController.control.People[i].Name, Data.ProgramFile);
                        WindowManager.QuitProgram(PersonController.control.People[i].Name, "FileUtility", PersonController.control.People[i].Gateway.RunningPrograms[j].WPN);
                        break;
                }
                //Done
                //C:/Documents
            }
        }

    }

    void DiskCheck(int i, int j)
    {

    }
}
