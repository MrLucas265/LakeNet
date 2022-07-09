using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BIOS1 : MonoBehaviour
{
	public GUIStyle ClockFont;
	public GUISkin BIOSSkin;

	private GameObject system;
	private POST post;

	private GameObject hardware;
	private Mouse mouse;

	public float CPUSimUsage;

	public string Menu;
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int SelectedPort = -1;
	public int SelectedStorageDevice = -1;
	public int ChangeSelectedDevice = -1;

	public bool ShowBootOrder;

	public StorageDevice HeldDevice;

	// Use this for initialization
	void Start()
	{
		system = GameObject.Find("System");
		hardware = GameObject.Find("Hardware");

		AfterStart();
	}

	void AfterStart()
	{
		post = system.GetComponent<POST>();
		mouse = system.GetComponent<Mouse>();

		//cpu.MaxCPUSpeed = HardwareController.hdcon.MaxCPUSpeed;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void MenuSystem()
	{
		switch(Menu)
		{
			case "Disk Info":
				DiskInfoMenu();
			break;
			case "Boot":
				Boot();
				break;

		}
	}

	public void Boot()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");
		var player = person.Gateway;

		scrollpos = GUI.BeginScrollView(new Rect(3, 300, 290, 106), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
		for (scrollsize = 0; scrollsize < player.StorageDevices.Count; scrollsize++)
		{
			if (GUI.Button(new Rect(0, 21 * scrollsize, 200, 20), "" + player.StorageDevices[scrollsize].Name))
			{
				SelectedStorageDevice = scrollsize;
				ShowBootOrder = true;
			}
		}
		GUI.EndScrollView();

		if(ShowBootOrder == true)
		{
			var BoxRect = new Rect(400, 100, 250, 250);
			GUI.Box(new Rect(BoxRect), "");
			GUI.Label(new Rect(BoxRect.x+BoxRect.width/3f, 110, 300, 300), "Boot Option #" + SelectedStorageDevice);

			scrollpos = GUI.BeginScrollView(new Rect(425, 150, 250, 250), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < player.StorageDevices.Count; scrollsize++)
			{
				if (GUI.Button(new Rect(0, 21 * scrollsize, 200, 20), "" + player.StorageDevices[scrollsize].Name))
				{
					ChangeSelectedDevice = scrollsize;
					ShowBootOrder = false;
				}
			}
			GUI.EndScrollView();
		}

		if(ChangeSelectedDevice > -1)
		{
			var selectedStorageDevice = player.StorageDevices[SelectedStorageDevice];

			HeldDevice = selectedStorageDevice;
			player.StorageDevices.RemoveAt(SelectedStorageDevice);
			player.StorageDevices.Insert(ChangeSelectedDevice, HeldDevice);
			SelectedPort = -1;
			SelectedStorageDevice = -1;
			ChangeSelectedDevice = -1;
		}
	}

	public void DiskInfoMenu()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");
		var player = person.Gateway;

		if (SelectedStorageDevice == -1)
		{
			scrollpos = GUI.BeginScrollView(new Rect(3, 300, 290, 106), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < player.StorageDevices.Count; scrollsize++)
			{
				if (GUI.Button(new Rect(0, 21 * scrollsize, 200, 20), "" + player.StorageDevices[scrollsize].Name))
				{
					SelectedStorageDevice = scrollsize;
				}
			}
			GUI.EndScrollView();
		}
		if (SelectedStorageDevice > -1)
		{
			var selectedStorageDevice = player.StorageDevices[SelectedStorageDevice];

			scrollpos = GUI.BeginScrollView(new Rect(3, 300, 290, 106), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < player.StorageDevices.Count; scrollsize++)
			{
				if (GUI.Button(new Rect(0, 21 * scrollsize, 200, 20), "" + player.StorageDevices[scrollsize].Name))
				{
					SelectedStorageDevice = scrollsize;
				}
			}
			GUI.EndScrollView();

			GUI.Label(new Rect(0, 100, 300, 300), "Selected Storage Device: " + selectedStorageDevice.Name);
			GUI.Label(new Rect(0, 120, 300, 300), "Disk Size: " + selectedStorageDevice.Capacity);
			GUI.Label(new Rect(0, 140, 300, 300), "Boot Order: " + SelectedStorageDevice);
		}
	}

	public void BIOSGUI()
	{
		GUI.skin = BIOSSkin;
		mouse.ShowMouse = true;
		ClockFont.normal.textColor = Color.white;
		BIOSSkin.button.normal.textColor = Color.white;
		BIOSSkin.label.normal.textColor = Color.white;
		GUI.Label(new Rect(0, 0, 300, 20), "BIOS Name + Mode + Build Date + BIOS Version + Motherboard Name");
		GUI.Button(new Rect(0, 0, 0, 0), "Default Settings");

		if (GUI.Button(new Rect(500, Screen.height - 25, 100, 20), "Save & Exit"))
		{
			var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");
			person.Gateway.Status.BIOS = false;
			//mouse.ShowMouse = false;
			post.showBIOS = false;
			this.enabled = false;
			post.rebooting = true;
		}

		if (GUI.Button(new Rect(5, 5, 100, 100), "Disk Info"))
		{
			Menu = "Disk Info";
		}

		if (GUI.Button(new Rect(105, 5, 100, 100), "Boot"))
		{
			Menu = "Boot";
		}

		MenuSystem();

		//if (GUI.Button(new Rect(600, Screen.height - 25, 100, 20), "Load"))
		//{

		//}

		//GUI.Button(new Rect(0, 0, 0, 0), "Advanced Mode");

		//GUI.Label (new Rect (0, 0, 0, 0), "" + ProfileController.procon.Hour.ToString("00") + " : " +  ProfileController.procon.Min.ToString("00"),ClockFont);

		//if (ProfileController.procon.USADate == true)
		//{
		//	GUI.Label (new Rect (0, 500, 500, 500), "" + ProfileController.procon.Month + " / " + ProfileController.procon.Day + " / " + ProfileController.procon.CurYear);
		//}
		//else 
		//{
		//	GUI.Label (new Rect (0, 500, 100, 100), "" + ProfileController.procon.Day + " / " + ProfileController.procon.Month + " / " + ProfileController.procon.CurYear);
		//}

		// CPU Information

		//GUI.Box(new Rect(0, 100, 250, 250), "CPU");
		//GUI.Label(new Rect(35, 100, 200, 20), "" + GameControl.control.Gateway.CPUSockets[0].Chip[0].Name + " " + GameControl.control.Gateway.CPUSockets[0].Chip[0].MaxSpeed.ToString("F2") + "" + GameControl.control.SpaceName + "s");

		//for (int i = 0; i < cpu.Cores; i++)
		//{
		//	GUI.Label(new Rect(5, 120 + i * 20, 100, 21), "Core " + i + ": " + cpu.RemainingCPUUsage.ToString("F2") + GameControl.control.SpaceName + "s");
		//}

		//GUI.Label(new Rect(5, 200, 200, 21), "Current Temp " + cpu.CPUTemp.ToString("F1"));
		//GUI.Label(new Rect(5, 220, 100, 21), "Max Temp " + cpu.MaxTEMP.ToString("F2"));

		//GUI.Label(new Rect(105, 240, 100, 21), "Sim Usage " + CPUSimUsage.ToString("F2"));

		//CPUSimUsage = GUI.HorizontalSlider(new Rect(5, 240, 100, 21), CPUSimUsage, 0, cpu.MaxCPUSpeed);

		//if (GUI.Button(new Rect(5, 260, 100, 21), "Start Sim"))
		//{
		//	cpu.Usage = CPUSimUsage;
		//	cpu.setSpeed = true;
		//	cpu.SetSpeeds();
		//}

		//// OVERCLOCK STUFF
		//GUI.Label(new Rect(140, 280, 100, 21), "OC MAX CPU");

		//if (GUI.Button(new Rect(140, 300, 50, 21), "- 0.05"))
		//{
		//	cpu.OverclockMath();
		//}

		//if (GUI.Button(new Rect(195, 300, 50, 21), "+ 0.05"))
		//{
		//	cpu.OverclockMath();
		//}

		//// DEMO STUFF
		//// MAX TEMP STUFF
		//GUI.Label(new Rect(5, 280, 100, 21), "MAX Temp");

		//if (GUI.Button(new Rect(5, 300, 50, 21), "- 1"))
		//{
		//	cpu.MaxTempSetting -= 1f;
		//	//			cpu.HardwareCheck();
		//}

		//if (GUI.Button(new Rect(60, 300, 50, 21), "+ 1"))
		//{
		//	cpu.MaxTempSetting += 1f;
		//	//	cpu.HardwareCheck ();
		//}

		//// POWER EFF STUFF
		//GUI.Label(new Rect(5, 325, 200, 21), "POWER EFF " + cpu.PowerEffSetting.ToString("F3"));

		//if (GUI.Button(new Rect(130, 325, 55, 21), "- 0.001"))
		//{
		//	cpu.PowerEffSetting += 0.001f;
		//	//	cpu.HardwareCheck();
		//}

		//if (GUI.Button(new Rect(190, 325, 55, 21), "+ 0.001"))
		//{
		//	cpu.PowerEffSetting += 0.001f;
		//	//cpu.HardwareCheck ();
		//}

		//// End of CPU

		//GUI.Label (new Rect (0, 400, 300, 300), "" + ProfileController.procon.DayName);
		//GUI.Label (new Rect (0, 20, 300, 300), "CPU Name" + " CPU Speed + Voltage");
		//GUI.Label (new Rect (0, 40, 300, 300), "DRAM Info, List of RAM Names + Speeds + Slot Number + Ram Size + Voltages");
		//GUI.Label (new Rect (0, 60, 300, 300), "List of Sata Connections" + "");
		//GUI.Label (new Rect (0, 80, 300, 300), "List of Boot Devices" + "");
		//GUI.Label (new Rect (0, 100, 300, 300), "CPU Temp" + "");
		//GUI.Label (new Rect (0, 120, 300, 300), "Motherboard Temp" + "");
		//GUI.Label (new Rect (0, 140, 300, 300), "CPU Voltages" + "");
		//GUI.Label (new Rect (0, 160, 300, 300), "FAN Speed Changes" + "");
		//GUI.Label (new Rect (0, 180, 300, 300), "Performance Modes" + "");
		//GUI.Label (new Rect (0, 200, 300, 300), "GPU Information + Name + Voltages + Speed" + "GPU Temp");
	}
}
