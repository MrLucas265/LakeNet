using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

public class POST : MonoBehaviour
{
	public Texture2D PBootScreenPIC;

	public List<string> BootInfo = new List<string>();
	public List<ProgramSystem> BootableOS = new List<ProgramSystem>();
	public float cd;
	public float cooldown;

	public float cd1;
	public float cooldown1;

	public bool wait;
	public bool pause;
	public bool showBIOS;
	public bool booting;
    public bool booted;

	public bool rebooting;

	public GUISkin POSTSkin;
	public GUISkin BIOSSkin;

	public AudioSource AS;

	public int[] Index;


	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool show;
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	private GameObject bios;
	private GameObject hardware;
	private RAM ram;
	private Computer com;
	private ProfileUI pui;
	private LoginBackground lbg;
	private AccSetup accset;
	private Boot boot;
	private BIOSSelect biosselect;
	private GameObject Crash;
	private SysCrashMan SCM;
	private BootLoader bootloader;
	//private CPU cpu;
	private HardDrives harddrives;

	public bool MemoryCheck;
	public float TestedMemory;

	public float MemoryTimer;
	public float MemoryStart;

	public float MemoryCheckSpeed;

    // Use this for initialization
    void Start ()
	{
        LoadSkins();

        bios = GameObject.Find("BIOS");
		hardware = GameObject.Find("Hardware");

		windowRect = new Rect(0, 0, Customize.cust.RezX, Customize.cust.RezY);
		//pui = GetComponent<ProfileUI>();
		//accset = GetComponent<AccSetup>();
		//lbg = GetComponent<LoginBackground>();
		biosselect = bios.GetComponent<BIOSSelect>();
		boot = GetComponent<Boot>();
		bootloader = GetComponent<BootLoader>();
		ram = hardware.GetComponent<RAM>();
		//cpu = hardware.GetComponent<CPU>();
		Crash = GameObject.Find("Crash");
		SCM = Crash.GetComponent<SysCrashMan>();
		cd = GameControl.control.BootTime;
		cooldown = 0.256f;
		cooldown1 = 0.256f;
		Screen.SetResolution (Customize.cust.RezX, Customize.cust.RezY, Customize.cust.FullScreen);
		PersonController.control.Global.DateTime.TimeMulti = 1;

		if (Application.isEditor == true) 
		{
			windowRect.width = Screen.width;
			windowRect.height = Screen.height;
		} 
		else 
		{
			windowRect.width = Customize.cust.RezX;
			windowRect.height = Customize.cust.RezY;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

		//if (GameControl.control.Gateway.InstalledRAM.Count > 0)
		//{
		//	ram.enabled = true;
		//}
		//if (GameControl.control.Gateway.InstalledStorageDevice.Count > 0)
		//{
		//	harddrives.enabled = true;
		//}

		if (GameControl.control.GatewayStatus.Booting == true) 
		{
			cooldown = 0.16f;
			cooldown1 = 0.16f;
		}
		if (booted == true)
		{
			show = false;
		}

		if (Input.GetKeyDown (KeyCode.Delete)) 
		{
			BIOS();
			GameControl.control.GatewayStatus.BIOS = true;
		}

		if (Input.GetKeyDown (KeyCode.Alpha8)) 
		{
			bootloader.ChangeOS = true;
		}

		if(GameControl.control.SelectedOS.Name == OperatingSystems.OSName.SafeMode && GameControl.control.GatewayStatus.Terminal == false)
		{
			bootloader.ChangeOS = true;
		}

		if (rebooting == true)
		{
			show = true;
			Index[0] = 0;
			Index[1] = 0;
			MemoryCheck = false;
			TestedMemory = 0;
			BootInfo.RemoveRange (0, BootInfo.Count);
			pause = false;
			booting = true;
			rebooting = false;
		}

		if (show == false)
		{
			booting = false;
		}

		if (MemoryCheck == false) 
		{
			if (booting == true)
			{
				if (pause == false) 
				{
					cd -= 1 * Time.deltaTime;

					if (cd <= 0) 
					{
						Index[0]++;
						cd = cooldown;
						if (booted == false) 
						{
							Kernal ();
						}
					}
				}

				if (pause == true) 
				{
					cd1 -= 1 * Time.deltaTime;

					if (cd1 <= 0) 
					{
						Index[1]++;
						cd1 = cooldown1;
						if (booted == false) 
						{
							Kernal ();
						}
					}
				}
			}
		}

		if (MemoryCheck == true) 
		{
			if (TestedMemory <= ram.MaxRAM)
			{
				MemoryTimer -= 1 * Time.deltaTime;

				MemoryCheckSpeed = person.Gateway.RAM[0].Speed;

				if (MemoryTimer <= 0) 
				{
					MemoryTimer = MemoryStart;
					TestedMemory +=MemoryCheckSpeed*1;
					float past = TestedMemory -MemoryCheckSpeed*1;
					BootInfo.Remove ("MEMORY TEST: " + past);
					BootInfo.Add ("MEMORY TEST: " + TestedMemory);
				}
			}
			if (TestedMemory >= ram.MaxRAM)
			{
				BootInfo.Remove("MEMORY TEST: " + TestedMemory);
				BootInfo.Add ("MEMORY TEST: " + TestedMemory + " OK");
				MemoryCheck = false;
			}
		}
	}

	void ForceCrash()
	{
		SCM.enabled = true;
		SCM.Type = "Test";
		this.enabled = false;
	}

	void BIOS()
	{
		booting = false;
		showBIOS = true;
	}

	void StatusReset()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");
		person.Gateway.Status.Active = false;
		person.Gateway.Status.BIOS = false;
		person.Gateway.Status.Booted = false;
		person.Gateway.Status.Booting = false;
		person.Gateway.Status.LoggedIn = false;
		person.Gateway.Status.Off = false;
		person.Gateway.Status.On = false;
		person.Gateway.Status.POST = false;
		person.Gateway.Status.SafeMode = false;
		person.Gateway.Status.Shutdown = false;
		person.Gateway.Status.SigningIn = false;
		person.Gateway.Status.SigningOut = false;
		person.Gateway.Status.Sleep = false;
		person.Gateway.Status.Terminal = false;
		person.Gateway.Status.Restart = false;
	}

	void LoadSkins()
	{
        GameControl.control.Skins = Resources.LoadAll<GUISkin>("Skins");
    }

	void Kernal()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

		switch (Index[0]) 
		{
		case 0:
				break;
		case 1:
				if (person.Gateway.Status.BIOS == true)
				{
					BIOS();
				}
				break;
		case 2:
				StatusReset();
				person.Gateway.Status.On = true;
				person.Gateway.Status.POST = true;
				BootInfo.Add ("IMABIOS(C)2017 REVA. LTD.");
				RegistryLoader.RegLoad.RunRegLoad = true;
				//ProfileController.procon.Load ();
				//GameControl.control.Load ();
				//Customize.cust.Load();
				ProfileController.procon.ShowTOS = false;
			break;
		case 3:
			//BootInfo.Add (HardwareController.hdcon.Motherboard [0] + " BIOS Version 0.1");
			BootInfo.Add ("");
				person.Gateway.RunningPrograms.RemoveRange(0, person.Gateway.RunningPrograms.Count);
				person.Gateway.RunningTasks.RemoveRange(0, person.Gateway.RunningTasks.Count);
				//HardwareController.hdcon.CPUCheck = true;
				//ProfileController.procon.Save ();
				//GameControl.control.Save ();
				//HardwareController.hdcon.Save();
				break;
		case 4:
				BootInfo.Add("CPU: " + person.Gateway.CPU[0].Name + "CPU @ " + person.Gateway.CPU[0].MaxSpeed.ToString("F2"));
                break;
		case 5:
            //for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
            //{
            //    GameControl.control.Gateway.InstalledCPU[i].Usage = 0;
            //}
            //for (int i = 0; i < GameControl.control.Gateway.InstalledGPU.Count; i++)
            //{
            //    GameControl.control.Gateway.InstalledGPU[i].Usage = 0;
            //}
            //for (int i = 0; i < GameControl.control.Gateway.InstalledRAM.Count; i++)
            //{
            //    GameControl.control.Gateway.InstalledRAM[i].Used = 0;
            //}
            //for (int i = 0; i < GameControl.control.Gateway.InstalledPSU.Count; i++)
            //{
            //    GameControl.control.Gateway.InstalledRAM[i].Used = 0;
            //}
            break;
		case 6:
			MemoryCheck = true;
			BootInfo.Add ("");
			break;
		case 7:
			BootInfo.Add ("Memory Frequency is at 200MHz, Dual Channel Mode");
			BootInfo.Add ("");
			break;
		case 8:
			BootInfo.Add ("Detecting IDE Drives.");
			break;
		case 9:
			BootInfo.Remove("Detecting IDE Drives.");
			BootInfo.Add ("Detecting IDE Drives..");
			break;
		case 10:
			BootInfo.Remove("Detecting IDE Drives..");
			BootInfo.Add ("Detecting IDE Drives...");
			break;
		case 11:
			BootInfo.Add("Primary Master : ");
			break;
		case 12:
			pause = true;
			break;
		case 13:
			booting = false;
			show = false;
			break;
		}

		if (pause == true)
		{
			switch (Index[1]) 
			{
			case 0:
				break;
			case 1:
				BootInfo.Remove("Primary Master : ");
				BootInfo.Add("Primary Master : " + person.Gateway.StorageDevices[0].Name);
				break;
			case 2:
				BootInfo.Add("Primary Slave : ");
				break;
			case 3:
				BootInfo.Remove("Primary Slave : ");
				BootInfo.Add("Primary Slave :  None");
				break;
			case 4:
				BootInfo.Add("Secondary Master : ");
				break;
			case 5:
				BootInfo.Remove("Secondary Master : ");
				BootInfo.Add("Secondary Master :  None");
				break;
			case 6:
				BootInfo.Add("Secondary Slave : ");
				break;
			case 7:
				BootInfo.Remove("Secondary Slave : ");
				BootInfo.Add("Secondary Slave : None");
				break;
			case 8:
					SaveFunction();
				break;
			case 9:
				break;
			case 10:
				AS.PlayOneShot (AS.clip);
				pause = false;
                booted = true;

				bootloader.show = true;
				this.enabled = false;
				bootloader.enabled = true;

				GameControl.control.GatewayStatus.POST = false;

//				if (ProfileController.procon.Profiles.Count <= 1)
//				{
//					accset.enabled = true;
//					pui.enabled = false;
//				} 
//				else 
//				{
//					accset.enabled = false;
//					pui.enabled = true;
//				}
				break;
			}
		}
	}

	void SaveFunction()
	{
		GameControl.control.Save();
		ProfileController.procon.Save();
		Customize.cust.Save();
		PersonController.control.Save();
	}

	void OnGUI()
	{
		GUI.depth = -30;
		if (showBIOS == false) 
		{
			GUI.skin = POSTSkin;
		}
		else 
		{
			GUI.skin = BIOSSkin;
		}
			
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1));

		if(show == true)
		{
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (showBIOS == false) 
		{
			if (pause == false) 
			{
				if (Index[0] >= 2) 
				{
					//GUI.DrawTexture (new Rect (700, 0, 200, 200), PBootScreenPIC);
					GUI.Label (new Rect (10, Screen.height-25, 300, 20), "Press DEL to Enter BIOS");
					GUI.Label (new Rect (210, Screen.height-25, 300, 20), "Press 8 to Enter Boot Menu");
				}
			} 
			else 
			{
				if (Index[0] >= 2) 
				{
					//GUI.DrawTexture (new Rect (700, 0, 200, 200), PBootScreenPIC);
					GUI.Label (new Rect (10, Screen.height-25, 300, 20), "Press DEL to Enter BIOS");
					GUI.Label (new Rect (210, Screen.height-25, 300, 20), "Press 8 to Enter Boot Menu");
				}
			}
		}
		if (showBIOS == true)
		{
			biosselect.RenderBios();
		}
		else 
		{
			scrollpos = GUI.BeginScrollView(new Rect(5, 5, 920, 540), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
			for (scrollsize = 0; scrollsize < BootInfo.Count; scrollsize++)
			{
				//GUI.DrawTexture(new Rect(1, 1, 1, 1),FAN);
				GUI.Label (new Rect (10, scrollsize * 20, 300, 21), "" + BootInfo[scrollsize]);
			}
			GUI.EndScrollView();
		}
	}
}