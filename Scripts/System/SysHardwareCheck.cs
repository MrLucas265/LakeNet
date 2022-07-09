using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysHardwareCheck : MonoBehaviour
{
	private GameObject Desktops;
	private GameObject Software;
	private GameObject Crash;
	private GameObject prompt;
	private GameObject System;

	private SysCrashMan SCM;

	private TaskViewer tv;

	private NotfiPrompt noti;

	public float RemoveTimer;

	private AppMan appman;

	public bool crashmsg;
	// Use this for initialization
	void Start ()
	{
		Crash = GameObject.Find("Crash");
		Desktops = GameObject.Find("Desktops");
		Software = GameObject.Find("Software");
		prompt = GameObject.Find("Prompts");
		System = GameObject.Find("System");

		noti = prompt.GetComponent<NotfiPrompt>();
		SCM = Crash.GetComponent<SysCrashMan> ();
		tv = System.GetComponent<TaskViewer>();

		appman = System.GetComponent<AppMan>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (GameControl.control.Gateway.InstalledCPU [0].UsagePercent > 100) 
		//{
		//	noti.ForcedMusicSetting = true;
		//	noti.ForcedMusicOption = false;
		//	noti.NewNotification("Critical Error", "OS Kernal Panic", "Insuffcient CPU resource pool.");
		//	KernalPanic ();
		//}
		//if (GameControl.control.Gateway.InstalledPSU [0].Remaining < 0) 
		//{
		//	noti.ForcedMusicSetting = true;
		//	noti.ForcedMusicOption = false;
		//	noti.NewNotification("Critical Error", "OS Kernal Panic", "Insuffcient PSU resource pool.");
		//	KernalPanic ();
		//}
		//if (GameControl.control.Gateway.InstalledRAM [0].Remaining < 0) 
		//{
		//	noti.ForcedMusicSetting = true;
		//	noti.ForcedMusicOption = false;
		//	noti.NewNotification("Critical Error", "OS Kernal Panic", "Insuffcient RAM resource pool.");
		//	KernalPanic ();
		//}
	}

	void ClosePrograms()
	{
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
			KernalPanic();
		}
	}

	void KernalPanic()
	{
		SCM.StopCodeWord = "AUTO_SYSTEM_CRASH";
		SCM.StopCodeNumber = "0xD34DD13D";
		SCM.CodeDetail = "K3RN31-94N1C-C9U-2Y2-CR2";
		SCM.ExtraDetail = "14M-7H3-D327R0Y3R-0F-7H12-02";
		SCM.enabled = true;
		Desktops.SetActive (false);
		Software.SetActive(false);
	}
}
