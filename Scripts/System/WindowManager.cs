using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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

    public int SelectedWID;

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

    public static void QuitProgram(string PersonName,string ProgramName,int WPN)
    {
        var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonName);

        person.Gateway.RunningPrograms.RemoveAt(WPN);

        var pwinman = person.Gateway;

        for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
        {
            pwinman.RunningPrograms[i].WPN = i;
            //for (int j = 0; j < pwinman.RunningPrograms.Count; j++)
            //{
            //    if (pwinman.RunningPrograms[j].ProgramName == ProgramName)
            //    {
            //        pwinman.RunningPrograms[j].PID++;
            //        pwinman.RunningPrograms[j].PID = pwinman.RunningPrograms[j].PID - 1;
            //    }
            //}
        }
    }

    // Use this for initialization
    void Start ()
    {
        SelectedWID = -1;
    }

    void OnGUI()
    {
        
    }

    public void AddProgramWindow()
    {
        int MaxWindowValue = 99999;
        int MinWindowValue = 500;
        WID = Random.Range(MinWindowValue, MaxWindowValue);

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
                                    WID = Random.Range(MinWindowValue, MaxWindowValue);
                                }
                                if (RunningPrograms[Index].PID == PID)
                                {
                                    PID++;
                                }
                            }
                        }

                        if (Index == RunningPrograms.Count)
                        {
                            RunningPrograms.Add(new WindowConSys(WindowName, ProgramName, ProcessName, Status, ProcessType, WID, PID,Index, windowRect, windowButtons, TitleBoxRect, Resize, ResizeRect, WindowResizeRect));
                        }
                    }
                    else
                    {
                        RunningPrograms.Add(new WindowConSys(WindowName, ProgramName, ProcessName, Status, ProcessType, WID, PID,Index, windowRect, windowButtons, TitleBoxRect, Resize, ResizeRect, WindowResizeRect));
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
                                    //if (RunningPrograms[j].LocalRegister.Count == 0)
                                    //{
                                    //    RunningPrograms[j].LocalRegister[m].KeyName = Registry[k].KeyName;
                                    //    RunningPrograms[j].LocalRegister[m].Values.Add(new RegistryDataSystem(RegVal.ValueName,RegVal.DataString,RegVal.DataInt,
                                    //        RegVal.DataBool,RegVal.DataFloat,RegVal.DataRect,RegVal.DataVector3,RegVal.DataVector2));
                                    //}
                                    //else
                                    //{
                                    //    if(RunningPrograms[j].LocalRegister.Count < Registry[k].Values.Count)
                                    //    {
                                    //        RunningPrograms[j].LocalRegister[m].Values.Add(new RegistryDataSystem(RegVal.ValueName, RegVal.DataString, RegVal.DataInt,
                                    //        RegVal.DataBool, RegVal.DataFloat, RegVal.DataRect, RegVal.DataVector3, RegVal.DataVector2));
                                    //    }
                                    //}

                                    if (RunningPrograms[j].LocalRegister[m].Values.Count < Registry[k].Values.Count)
                                    {
                                        //RunningPrograms[j].LocalRegister[m].KeyName = Registry[k].KeyName;
                                        RunningPrograms[j].LocalRegister[m].Values.Add(new RegistryDataSystem(RegVal.ValueName, RegVal.DataString, RegVal.DataInt,
                                        RegVal.DataBool, RegVal.DataFloat, RegVal.DataRect, RegVal.DataVector3, RegVal.DataVector2));
                                    }


                                    if (RunningPrograms[j].LocalRegister[m].Values[0].ValueName != Registry[k].Values[0].ValueName)
                                    {
                                        RunningPrograms[j].LocalRegister[m].Values.Insert(0, new RegistryDataSystem(Registry[k].Values[0].ValueName, Registry[k].Values[0].DataString, Registry[k].Values[0].DataInt,
Registry[k].Values[0].DataBool, Registry[k].Values[0].DataFloat, Registry[k].Values[0].DataRect, Registry[k].Values[0].DataVector3, Registry[k].Values[0].DataVector2));
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
                            if (rect.Contains(Event.current.mousePosition))
                            {
                                Dragging = true;
                            }
                        }
                        else
                        {
                            if (Dragging == true)
                            {
                                SelectedWID = -1;
                                Dragging = false;
                            }
                        }
                    }
                }
            }

        }
    }


    public void WindowResize(string PersonName,int WID)
    {
        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            if(PersonController.control.People[i].Name == PersonName)
            {
                var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

                if (RunningPrograms.Count > 0 && Dragging == false)
                {
                    for (int j = 0; j < RunningPrograms.Count; j++)
                    {
                        if (RunningPrograms[j].WID == WID)
                        {
                            if (SelectedWID != -1)
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
                                    WindowResizeChecks(i, j);
                                    RunningPrograms[j].Resize = false;
                                    SelectedWID = -1;
                                }
                            }

                            if (RunningPrograms[j].Resize == true)
                            {
                                RunningPrograms[j].WindowResizeRect.width = Event.current.mousePosition.x + Event.current.delta.x;
                                RunningPrograms[j].WindowResizeRect.height = Event.current.mousePosition.y + Event.current.delta.y;

                                RunningPrograms[j].WindowResizeRect = new Rect(RunningPrograms[j].windowRect.x, RunningPrograms[j].windowRect.y, RunningPrograms[j].WindowResizeRect.width, RunningPrograms[j].WindowResizeRect.height);
                            }
                        }
                    }
                }
            }
        }
    }

    void WindowResizeChecks(int i,int ProgramID)
    {
        var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

        RunningPrograms[ProgramID].windowRect.width = Event.current.mousePosition.x + Event.current.delta.x;
        RunningPrograms[ProgramID].windowRect.height = Event.current.mousePosition.y + Event.current.delta.y;

        if (RunningPrograms[ProgramID].windowRect.height > Screen.height - 50)
        {
            RunningPrograms[ProgramID].windowRect.height = Screen.height - 50;
        }
        if (RunningPrograms[ProgramID].windowRect.width > Screen.width - 50)
        {
            RunningPrograms[ProgramID].windowRect.width = Screen.width - 50;
        }
        if (RunningPrograms[ProgramID].windowRect.width < 18)
        {
            RunningPrograms[ProgramID].windowRect.width = 18;
        }
        if (RunningPrograms[ProgramID].windowRect.height < 30)
        {
            RunningPrograms[ProgramID].windowRect.height = 30;
        }
    }

    //void RemoveProgramWindow()
    //{
    //    for (int i = 0; i < PersonController.control.People.Count; i++)
    //    {
    //        var RunningPrograms = PersonController.control.People[i].Gateway.RunningPrograms;

    //        string SelectedWindowProgram = RunningPrograms[SelectedWID].ProgramName;
    //        for (int j = 0; j < RunningPrograms.Count; j++)
    //        {
    //            if (RunningPrograms[j].ProgramName == SelectedWindowProgram)
    //            {
    //                if (RunningPrograms[j].WID != SelectedWID)
    //                {
    //                    RunningPrograms[j].PID--;
    //                }
    //            }
    //            if (j >= RunningPrograms.Count)
    //            {
    //                RunningPrograms.RemoveAt(SelectedWID);
    //            }
    //        }
    //    }
    //}

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