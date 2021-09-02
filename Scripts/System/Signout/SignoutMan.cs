using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignoutMan : MonoBehaviour 
{
	public string Type;
	public string SignoutMessage;

    private GameObject Software;
    private GameObject System;

    private IceOSLogout IceOSLog;
	private EthelOSLogout EthelOSLog;

	private AppMan appman;
    private TaskViewer tv;

    private SoundControl sc;

    public float RemoveTimer;

	// Use this for initialization
	void Start ()
	{
		IceOSLog = GetComponent<IceOSLogout>();
        EthelOSLog = GetComponent<EthelOSLogout>();
        Software = GameObject.Find("Software");
        System = GameObject.Find("System");
        sc = System.GetComponent<SoundControl>();
        appman = System.GetComponent<AppMan>();
        tv = System.GetComponent<TaskViewer>();
        RemoveTimer = 0.25f;
    }
	
	// Update is called once per frame
	void Update () 
	{
		switch (GameControl.control.SelectedOS.Name)
		{
		case OperatingSystems.OSName.FluidicIceOS:
                if (tv.RunningTasks.Count > 0)
                {
                    RemoveTimer -= Time.deltaTime;
                    if(RemoveTimer <= 0)
                    {
                        appman.SelectedApp = tv.RunningTasks[0].RunningApplications;
                        tv.RunningTasks.RemoveAt(0);
                        RemoveTimer = 0.25f;
                    }
                }
                if (tv.RunningTasks.Count <= 0)
                {
                    if (IceOSLog.Timer == 0)
                    {
                        IceOSLog.Timer = 2;
                    }
                    //IceOSLog = StopCodeWord;
                    //IceOSLog.StopCodeNumber = StopCodeNumber;
                    //IceOSLog.CodeDetail = CodeDetail;
                    IceOSLog.SignoutMessage = SignoutMessage;
                    IceOSLog.enabled = true;
                    Software.SetActive(false);
                    IceOSLog.Timers();
                }
	    break;

		case OperatingSystems.OSName.EthelOS:
            if (tv.RunningTasks.Count > 0)
            {
                RemoveTimer -= Time.deltaTime;
                if(RemoveTimer <= 0)
                {
                    appman.SelectedApp = tv.RunningTasks[0].RunningApplications;
                    tv.RunningTasks.RemoveAt(0);
                    RemoveTimer = 0.25f;
                }
            }
            if (tv.RunningTasks.Count <= 0)
            {
                if (EthelOSLog.Timer == 0)
                {
                    EthelOSLog.Timer = 2;
                }
                //IceOSLog = StopCodeWord;
                //IceOSLog.StopCodeNumber = StopCodeNumber;
                //IceOSLog.CodeDetail = CodeDetail;
                EthelOSLog.SignoutMessage = SignoutMessage;
                EthelOSLog.enabled = true;
                Software.SetActive(false);
                EthelOSLog.Timers();
            }
	    break;

		case OperatingSystems.OSName.AppatureOS:
			if (tv.RunningTasks.Count > 0)
			{
				RemoveTimer -= Time.deltaTime;
				if(RemoveTimer <= 0)
				{
					appman.SelectedApp = tv.RunningTasks[0].RunningApplications;
					tv.RunningTasks.RemoveAt(0);
					RemoveTimer = 0.25f;
				}
			}
			if (tv.RunningTasks.Count <= 0)
			{
				if (IceOSLog.Timer == 0)
				{
					IceOSLog.Timer = 2;
				}
				//IceOSLog = StopCodeWord;
				//IceOSLog.StopCodeNumber = StopCodeNumber;
				//IceOSLog.CodeDetail = CodeDetail;
				IceOSLog.SignoutMessage = SignoutMessage;
				IceOSLog.enabled = true;
				Software.SetActive(false);
				IceOSLog.Timers();
			}
			break;

		case OperatingSystems.OSName.TreeOS:
			if (tv.RunningTasks.Count > 0)
			{
				RemoveTimer -= Time.deltaTime;
				if(RemoveTimer <= 0)
				{
					appman.SelectedApp = tv.RunningTasks[0].RunningApplications;
					tv.RunningTasks.RemoveAt(0);
					RemoveTimer = 0.25f;
				}
			}
			if (tv.RunningTasks.Count <= 0)
			{
				if (IceOSLog.Timer == 0)
				{
					IceOSLog.Timer = 2;
				}
				//IceOSLog = StopCodeWord;
				//IceOSLog.StopCodeNumber = StopCodeNumber;
				//IceOSLog.CodeDetail = CodeDetail;
				IceOSLog.SignoutMessage = SignoutMessage;
				IceOSLog.enabled = true;
				Software.SetActive(false);
				IceOSLog.Timers();
			}
			break;

        //case OperatingSystems.OSName.TreeOS:
        //    if (bc.Timer == 0)
        //    {
        //        bc.Timer = 10;
        //    }
        //    bc.StopCodeWord = StopCodeWord;
        //    bc.StopCodeNumber = StopCodeNumber;
        //    bc.CodeDetail = CodeDetail;
        //    bc.ExtraDetail = ExtraDetail;
        //    bc.enabled = true;
        //    bc.Timers();
        // break;
        }
	}

//	void SystemCrashSelector()
//	{
//		switch (Type)
//		{
//		case "Test":
//			bc.enabled = true;
//			break;
//		}
//	}
}
