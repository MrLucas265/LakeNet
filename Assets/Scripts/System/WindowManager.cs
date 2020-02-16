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
    public string Type;
    public int WID;
    public int PID;
    public Rect windowRect;
    public List<Rect> windowButtons = new List<Rect>();
    public Rect titleBox;

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



    // Use this for initialization
    void Start ()
    {
		
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
        WID = Random.Range(1000, 9999);
        if(RunningPrograms.Count > 0)
        {
            for (Index = 0; Index < RunningPrograms.Count; Index++)
            {
                if (RunningPrograms[Index].ProgramName == ProgramName)
                {
                    if (RunningPrograms[Index].WID == WID)
                    {
                        WID = Random.Range(1000, 9999);
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
                RunningPrograms.Add(new WindowConSys(WindowName, ProgramName, ProcessName, Status, Type, WID, PID, windowRect,windowButtons,titleBox));
            }
        }
        else
        {
            RunningPrograms.Add(new WindowConSys(WindowName, ProgramName, ProcessName, Status, Type, WID, PID, windowRect, windowButtons, titleBox));
        }
        TempWID = WID;
        ResetValues();
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
        Type = "Test";
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
        Type = "Test";
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
        Type = "Test";
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
        Type = "";
        WID = 0;
        PID = 0;
        windowRect = new Rect(0, 0, 0, 0);
        windowButtons.RemoveRange(0, windowButtons.Count);
        titleBox = new Rect(0, 0, 0, 0);
    }

    public void RenderWindows()
    {

    }

    void CreateProgramWindow()
    {

    }
}
