using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public List<WindowConSys> RunningPrograms = new List<WindowConSys>();

    public string WindowName;
    public string ProgramName;
    public string ProcessName;
    public string Status;
    public string ProcessType;
    public int WID;
    public int PID;
    public Rect windowRect;
    public Rect TitleBoxRect;
    public List<Rect> windowButtons = new List<Rect>();
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

    // Use this for initialization
    void Start ()
    {
        SelectedWID = -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (AddDebug == true)
        {
            Debug();
        }
        if (AddPDebug == true)
        {
            Debug1();
            Debug2();
            Debug3();
            AddPDebug = false;
        }
        if (RemoveDebug == true)
        {
            RemoveProgramWindow();
            RemoveDebug = false;
        }
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
                    else
                    {
                        if (RunningPrograms[Index].PID == PID)
                        {
                            PID++;
                        }
                    }
                }
            }

            if (Index == RunningPrograms.Count)
            {
                RunningPrograms.Add(new WindowConSys(WindowName, ProgramName, ProcessName, Status, ProcessType, WID, PID, windowRect,windowButtons, TitleBoxRect, Resize, ResizeRect, WindowResizeRect));
            }
        }
        else
        {
            RunningPrograms.Add(new WindowConSys(WindowName, ProgramName, ProcessName, Status, ProcessType, WID, PID, windowRect, windowButtons, TitleBoxRect, Resize, ResizeRect, WindowResizeRect));
        }
        TempWID = WID;
        ResetValues();
    }


    public void WindowDragging(int WID,Rect rect)
    {
        if (RunningPrograms.Count > 0)
        {
            for (int i = 0; i < RunningPrograms.Count; i++)
            {
                if (RunningPrograms[i].WID == WID)
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


    public void WindowResize(int WID)
    {
        if (RunningPrograms.Count > 0 && Dragging == false)
        {
            for (int i = 0; i < RunningPrograms.Count; i++)
            {
                if(RunningPrograms[i].WID == WID)
                {
                    if(SelectedWID != -1)
                    {
                        RunningPrograms[i].ResizeRect = new Rect(RunningPrograms[i].windowRect.width - 16, RunningPrograms[i].windowRect.height - 16, 16, 16);
                    }

                    if (Input.GetMouseButton(0))
                    {
                        if (RunningPrograms[i].ResizeRect.Contains(Event.current.mousePosition))
                        {
                            RunningPrograms[i].Resize = true;
                        }
                    }
                    else
                    {
                        if (RunningPrograms[i].Resize == true)
                        {
                            WindowResizeChecks(i);
                            RunningPrograms[i].Resize = false;
                            SelectedWID = -1;
                        }
                    }

                    if (RunningPrograms[i].Resize == true)
                    {
                        RunningPrograms[i].WindowResizeRect.width = Event.current.mousePosition.x + Event.current.delta.x;
                        RunningPrograms[i].WindowResizeRect.height = Event.current.mousePosition.y + Event.current.delta.y;

                        RunningPrograms[i].WindowResizeRect = new Rect(RunningPrograms[i].windowRect.x, RunningPrograms[i].windowRect.y, RunningPrograms[i].WindowResizeRect.width, RunningPrograms[i].WindowResizeRect.height);
                    }
                }
            }
        }
    }

    void WindowResizeChecks(int i)
    {
        RunningPrograms[i].windowRect.width = Event.current.mousePosition.x + Event.current.delta.x;
        RunningPrograms[i].windowRect.height = Event.current.mousePosition.y + Event.current.delta.y;

        if (RunningPrograms[i].windowRect.height > Screen.height - 50)
        {
            RunningPrograms[i].windowRect.height = Screen.height - 50;
        }
        if (RunningPrograms[i].windowRect.width > Screen.width - 50)
        {
            RunningPrograms[i].windowRect.width = Screen.width - 50;
        }
        if (RunningPrograms[i].windowRect.width < 18)
        {
            RunningPrograms[i].windowRect.width = 18;
        }
        if (RunningPrograms[i].windowRect.height < 30)
        {
            RunningPrograms[i].windowRect.height = 30;
        }
    }

    void RemoveProgramWindow()
    {
        string SelectedWindowProgram = RunningPrograms[SelectedWID].ProgramName;
        for (int i = 0; i < RunningPrograms.Count; i++)
        {
            if (RunningPrograms[i].ProgramName == SelectedWindowProgram)
            {
                if (RunningPrograms[i].WID != SelectedWID)
                {
                    RunningPrograms[i].PID--;
                }
            }
            if (i >= RunningPrograms.Count)
            {
                RunningPrograms.RemoveAt(SelectedWID);
            }
        }
    }

    void Debug()
    {
        AddProgramWindow();
        AddDebug = false;
    }

    void Debug1()
    {
        WindowName = "Debug Test";
        ProgramName = "Debug";
        ProcessName = "Debug";
        Status = "Testing";
        ProcessType = "Test";
        WID = 0;
        PID = 0;
        windowRect = new Rect(200, 200, 400, 200);
        AddProgramWindow();
    }

    void Debug2()
    {
        WindowName = "Debug Test";
        ProgramName = "Debug1";
        ProcessName = "Debug";
        Status = "Testing";
        ProcessType = "Test";
        WID = 0;
        PID = 0;
        windowRect = new Rect(200, 200, 400, 200);
        AddProgramWindow();
    }

    void Debug3()
    {
        WindowName = "Debug Test";
        ProgramName = "Debug";
        ProcessName = "Debug";
        Status = "Testing";
        ProcessType = "Test";
        WID = 0;
        PID = 0;
        windowRect = new Rect(200, 200, 400, 200);
        AddProgramWindow();
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

    public void RenderWindows()
    {

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