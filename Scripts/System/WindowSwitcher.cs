using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSwitcher : MonoBehaviour
 {
	public int SelectedWindowID;
	public int SelectedProgram;
	public bool changeWindow;
	private TaskViewer tasks;
    private OS os;
    private AppMan appman;
    public string CurrentSelectedProgramName;
    // Use this for initialization
    void Start ()
	{
		tasks = GetComponent<TaskViewer>();
        os = GetComponent<OS>();
        appman = GetComponent<AppMan>();
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.C))
		{
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (SelectedProgram >= tasks.RunningTasks.Count - 1)
                {
                    SelectedProgram = 0;
                }
                else
                {
                    SelectedProgram++;
                }
                SelectedWindowID = tasks.RunningTasks[SelectedProgram].RunningApplicationsWindowID;
                changeWindow = true;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                os.RunTerminal();
            }

            //if (Input.GetKeyDown(KeyCode.End))
            //{
            //    appman.SelectedApp = tasks.RunningApplications[SelectedProgram];
            //}

            if (Input.GetKeyDown(KeyCode.P))
            {
                os.RunProgramManager();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                os.RunDeviceManager();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                os.RunGateway();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                os.RunProgramExecutor();
            }

			if (Input.GetKeyDown(KeyCode.A))
			{
				os.RunStartMenu();
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				os.RunQABugReport();
			}
        }
	}

	void OnGUI()
	{
		if (changeWindow == true)
		{
			GUI.FocusWindow(SelectedWindowID);
			GUI.BringWindowToFront(SelectedWindowID);
			changeWindow = false;
		}
	}
}
