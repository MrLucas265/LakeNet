using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindowManager : MonoBehaviour
{
    public string WindowName;
    public string ProgramName;
    public string ProcessName;
    public string Status;
    public string ProcessType;
    public int WID;
    public int PID;
    public Rect windowRect;
    public Rect TitleBoxRect;
    public List<SRect> windowButtons = new List<SRect>();
    public TitleBarSystem titleBox;
    public bool Dragging;

    public int TempWID;

    public bool AddDebug;
    public bool AddPDebug;
    public bool RemoveDebug;
    public int Index;

    public bool AddClose;
    public bool AddMinimize;
    public bool AddSettings;
    public bool AddScreenMode;

    public Rect ResizeRect;
    public bool Resize;
    public Rect WindowResizeRect;

    public string Name;

    public ResourceManagerSystem RMS;

    public static void QuitProgram(string PersonName,string ProgramName,int WPN)
    {
        for (int i = 0; i < PersonController.control.People.Count;i++)
        {
            var person = PersonController.control.People[i];
            if (person.Name == PersonName)
            {
                person.Gateway.RunningPrograms.RemoveAt(WPN);
            }
        }
    }

    public static void CheckProgramID(string PersonName, string ProgramName, int WPN)
    {
        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            var person = PersonController.control.People[i];
            if (person.Name == PersonName)
            {
                person.Gateway.RunningPrograms.RemoveAt(WPN);
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        Registry.SetIntData("Player", "WindowManager", "SelectedWindow", -1);
    }

    void OnGUI()
    {
        
    }

    public void ThreadedLoop()
    {
        if (PersonController.control.People.Count > 0)
        {
            for (int i = 0; i < PersonController.control.People.Count; i++)
            {
                if (PersonController.control.People[i].Name == Name)
                {
                    for (int j = 0; j < PersonController.control.People[i].Gateway.StorageDevices.Count; j++)
                    {
                        for (int l = 0; l < PersonController.control.People[i].Gateway.StorageDevices[j].OS.Count; l++)
                        {
                            for (int m = 0; m < PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions.Count; m++)
                            {
                                for (int n = 0; n < PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files.Count; n++)
                                {
                                    if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Name == ProgramName)
                                    {
                                        if (PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].Target == ProcessName)
                                        {
                                            RMS = PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions[m].Files[n].FileUsage;
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

    public void AddProgramWindow()
    {
        QThread.MakeThread(ThreadedLoop);
        //ThreadedLoop();

        int MaxWindowValue = 99999;
        int MinWindowValue = 500;
        WID = UnityEngine.Random.Range(MinWindowValue, MaxWindowValue);

        OSTitleBarSelector();

        if(PersonController.control.People.Count > 0)
        {
            for(int i = 0; i < PersonController.control.People.Count;i++)
            {
                if(PersonController.control.People[i].Name == Name)
                {
                    var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

                    if (RunningPrograms.Count > 0)
                    {
                        for (Index = 0; Index < RunningPrograms.Count; Index++)
                        {
                            if (RunningPrograms[Index].ProgramName == ProgramName)
                            {
                                if (RunningPrograms[Index].WID == WID)
                                {
                                    WID = UnityEngine.Random.Range(MinWindowValue, MaxWindowValue);
                                }
                                if (RunningPrograms[Index].PID == PID)
                                {
                                    PID++;
                                }
                            }
                        }

                        if (Index == RunningPrograms.Count)
                        {
                            RunningPrograms.Add(new WindowConSys(WindowName, ProgramName, ProcessName, Status, ProcessType, WID, PID,Index, windowRect, windowButtons, TitleBoxRect, Resize, ResizeRect, WindowResizeRect,RMS));
                        }
                    }
                    else
                    {
                        RunningPrograms.Add(new WindowConSys(WindowName, ProgramName, ProcessName, Status, ProcessType, WID, PID,Index, windowRect, windowButtons, TitleBoxRect, Resize, ResizeRect, WindowResizeRect, RMS));
                    }
                    SetLocalRegistry();
                    TempWID = WID;
                    //ProgramLocalRegisterCheck();
                    ResetValues();
                }
            }
        }
    }

    public void SetLocalRegistry()
    {
        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;
            var Registry = PersonController.control.People[i].Gateway.Registry;

            for (int j = 0; j < RunningPrograms.Count; j++)
            {
                for (int k = 0; k < Registry.Count; k++)
                {
                    if (RunningPrograms[j].ProcessName == Registry[k].KeyName)
                    {
                        for (int l = 0; l < Registry[k].Values.Count; l++)
                        {
                            var RegVal = Registry[k].Values[l];

                            if (RunningPrograms[j].LocalRegister.Count == 0)
                            {
                                RunningPrograms[j].LocalRegister.Add(new RegistrySystem("" + Registry[k].KeyName, new List<RegistryDataSystem>()));
                            }
                            else
                            {
                                for (int m = 0; m < RunningPrograms[j].LocalRegister.Count; m++)
                                {
                                    if (RunningPrograms[j].LocalRegister[m].Values.Count < Registry[k].Values.Count)
                                    {
                                        //RunningPrograms[j].LocalRegister[m].KeyName = Registry[k].KeyName;
                                        RunningPrograms[j].LocalRegister[m].Values.Add(new RegistryDataSystem(RegVal.ValueName));
                                    }


                                    if (RunningPrograms[j].LocalRegister[m].Values[0].ValueName != Registry[k].Values[0].ValueName)
                                    {
                                        RunningPrograms[j].LocalRegister[m].Values.Insert(0, new RegistryDataSystem(Registry[k].Values[0].ValueName));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public Rect GetWindowInfo(string PersonName,int WPN)
    {
        var Rect = new Rect(0,0,0,0);
        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            if(PersonController.control.People[i].Name == PersonName)
            {
                var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

                if (RunningPrograms.Count > 0)
                {
                    for (int j = 0; j < RunningPrograms.Count; j++)
                    {
                        if (j == WPN)
                        {
                            return RunningPrograms[j].windowRect;
                        }
                    }
                }
            }
        }
        return Rect;
     }


    public void WindowDragging(int WID,Rect rect)
    {
        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

            if (RunningPrograms.Count > 0)
            {
                for (int j= 0; j < RunningPrograms.Count; j++)
                {
                    if (RunningPrograms[j].WID == WID)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            if(Registry.GetIntData("Player","WindowManager","SelectedWindow") != -1)
                            {
                                if (rect.Contains(Event.current.mousePosition))
                                {
                                    Dragging = true;
                                }
                            }
                        }
                        else
                        {
                            if (Dragging == true)
                            {
                                Registry.SetIntData("Player", "WindowManager", "SelectedWindow", -1);
                                Dragging = false;
                            }
                        }
                    }
                }
            }

        }
    }

    public void WindowResize(string PersonName, int WID)
    {
        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            if (PersonController.control.People[i].Name == PersonName)
            {
                var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

                if (RunningPrograms.Count > 0 && Dragging == false)
                {
                    for (int j = 0; j < RunningPrograms.Count; j++)
                    {
                        if (RunningPrograms[j].WID == WID)
                        {
                            if (Registry.GetIntData("Player", "WindowManager", "SelectedWindow") != -1)
                            {
                                RunningPrograms[j].ResizeRect = new Rect(RunningPrograms[j].windowRect.width - 16, RunningPrograms[j].windowRect.height - 16, 16, 16);
                            }

                            if (Input.GetMouseButton(0))
                            {
                                if (RunningPrograms[j].ResizeRect.Contains(Event.current.mousePosition))
                                {
                                    RunningPrograms[j].Resize = true;
                                }
                            }
                            else
                            {
                                if (RunningPrograms[j].Resize == true)
                                {
                                    RunningPrograms[j].Resize = false;
                                    Registry.SetIntData("Player", "WindowManager", "SelectedWindow", -1);
                                }
                            }

                            if (RunningPrograms[j].Resize == true)
                            {
                                if(!Input.GetMouseButtonDown(0))
                                {
                                    RunningPrograms[j].WindowResizeRect = new Rect(RunningPrograms[j].windowRect.x, RunningPrograms[j].windowRect.y, RunningPrograms[j].WindowResizeRect.width, RunningPrograms[j].WindowResizeRect.height);

                                    WindowResize(i, RunningPrograms[j].WPN);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void RegistryWindowResize(string PersonName, int WID)
    {
        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            if (PersonController.control.People[i].Name == PersonName)
            {
                var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

                if (RunningPrograms.Count > 0 && Dragging == false)
                {
                    for (int j = 0; j < RunningPrograms.Count; j++)
                    {
                        if (RunningPrograms[j].WID == WID)
                        {
                            if (Registry.GetIntData("Player", "WindowManager", "SelectedWindow") != -1)
                            {
                                var ResizePos = new Rect(RunningPrograms[j].windowRect.width - 16, RunningPrograms[j].windowRect.height - 16, 16, 16);
                                LocalRegistry.SetRectData("Player", RunningPrograms[j].WPN, RunningPrograms[j].ProcessName, "Resize", ResizePos);
                            }

                            if (Input.GetMouseButton(0))
                            {
                                if (LocalRegistry.GetRectData("Player", RunningPrograms[j].WPN, RunningPrograms[j].ProcessName, "Resize").Contains(Event.current.mousePosition))
                                {
                                    LocalRegistry.SetBoolData("Player", RunningPrograms[j].WPN, RunningPrograms[j].ProcessName, "Resize",true);
                                }
                            }
                            else
                            {
                                if (LocalRegistry.GetBoolData("Player", RunningPrograms[j].WPN, RunningPrograms[j].ProcessName, "Resize") == true)
                                {
                                    LocalRegistry.SetBoolData("Player", RunningPrograms[j].WPN, RunningPrograms[j].ProcessName, "Resize", false);
                                    Registry.SetIntData("Player", "WindowManager", "SelectedWindow", -1);
                                }
                            }

                            if (LocalRegistry.GetBoolData("Player", RunningPrograms[j].WPN, RunningPrograms[j].ProcessName, "Resize") == true)
                            {
                                if (!Input.GetMouseButtonDown(0))
                                {
                                    RunningPrograms[j].WindowResizeRect = new Rect(RunningPrograms[j].windowRect.x, RunningPrograms[j].windowRect.y, RunningPrograms[j].WindowResizeRect.width, RunningPrograms[j].WindowResizeRect.height);

                                    WindowResize(i, RunningPrograms[j].WPN);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void ForceWindowResize(string PersonName, int WID)
    {
        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            if (PersonController.control.People[i].Name == PersonName)
            {
                var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

                if (RunningPrograms.Count > 0 && Dragging == false)
                {
                    for (int j = 0; j < RunningPrograms.Count; j++)
                    {
                        if (RunningPrograms[j].WID == WID)
                        {
                            if (RunningPrograms[j].windowRect.y < 50)
                            {
                                RunningPrograms[j].windowRect.y = 50;
                            }
                            if (RunningPrograms[j].windowRect.height > Screen.height - 50)
                            {
                                RunningPrograms[j].windowRect.height = Screen.height - 50;
                            }
                            if (RunningPrograms[j].windowRect.width > Screen.width - 50)
                            {
                                RunningPrograms[j].windowRect.width = Screen.width - 50;
                            }
                            if (RunningPrograms[j].windowRect.width < 100)
                            {
                                RunningPrograms[j].windowRect.width = 100;
                            }
                            if (RunningPrograms[j].windowRect.height < 30)
                            {
                                RunningPrograms[j].windowRect.height = 30;
                            }
                        }
                    }
                }
            }
        }
    }

    void WindowResize(int i, int ProgramID)
    {
        var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

        var CurrentMousePosX = Event.current.mousePosition.x + Event.current.delta.x;
        var CurrentMousePosY = Event.current.mousePosition.y + Event.current.delta.y;

        RunningPrograms[ProgramID].windowRect.width = CurrentMousePosX;
        RunningPrograms[ProgramID].windowRect.height = CurrentMousePosY;
    }

    void ResetValues()
    {
        WindowName = "";
        ProgramName = "";
        ProcessName = "";
        Status = "";
        ProcessType = "";
        WID = 0;
        PID = 0;
        windowRect = new Rect(0, 0, 0, 0);
        windowButtons.RemoveRange(0, windowButtons.Count);
        //titleBox.Name = "";
        //titleBox.Rect = new Rect(0, 0, 0, 0);
    }

    public void OSTitleBarSelector()
    {
        switch (GameControl.control.SelectedOS.Name)
        {
            case OperatingSystems.OSName.FluidicIceOS:
                float Math = windowRect.width - 64;
                TitleBoxRect = new Rect(40, 2, Math, 21);
                break;
        }
    }

    void CreateProgramWindow()
    {

    }
}