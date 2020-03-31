using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskViewer : MonoBehaviour 
{
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect SettingsButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	public bool minimize;

	private Computer com;

	public bool show;

    public List<TasksSystem> RunningTasks = new List<TasksSystem>();

    public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public bool ShowApplications;
	public bool showStartUp;
	public bool showDetails;
	public bool showServices;
	public bool showPerformance;
	public bool showUsers;
	public bool showHistory;
	public bool showSettings;
	public bool showSpecs;

	public bool showCPU;
	public bool showRAM;
	public bool showPSU;
	public bool showDisk;
	public bool showNET;
	public bool showGPU;
	public bool AnyShowen;

	public Texture2D[] Pics;

	public string Title;

	public string hint;

	public bool CompactMode;
	public bool ChangeLayout;
	public string CompactModeName;

	private GameObject Hardware;
	private CPU cpu;
	private RAM ram;
	private PSU psu;
	private HardDrives hdd;
	private Networks net;

	public int Index;

	private SoundControl sc;
	private AppMan appman;

	//CONTEXT MENU
	public Rect ContextwindowRect = new Rect (100, 100, 100, 200);
	public bool ShowContext;
	public int ContextMenuID;
	public int SelectedProgram;
	public List<string> ContextMenuOptions = new List<string>();
	public string SelectedOption;
	public float LastClick;
	public ProgramSystem SelectedTask;

    public int Selected;

	// Use this for initialization
	void Start ()
	{
		Hardware = GameObject.Find("Hardware");

		cpu = Hardware.GetComponent<CPU>();
		ram = Hardware.GetComponent<RAM>();
		psu = Hardware.GetComponent<PSU>();
		com = GetComponent<Computer>();
		sc = GetComponent<SoundControl>();
		appman = GetComponent<AppMan>();

		CloseButton = new Rect(441, 1, 21, 21);
		MiniButton = new Rect(419, 1, 21, 21);
		SettingsButton = new Rect(397, 1, 21, 21);

		PosCheck ();

		ContextwindowRect.width = 100;

		if (CompactMode == true)
		{
			minimize = true;
			Minimize();
			showSpecs = true;
		}
	}

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0) 
		{
			Customize.cust.windowx [windowID] = Screen.width / 2;
		}
		if (Customize.cust.windowy[windowID] == 0) 
		{
			Customize.cust.windowy [windowID] = Screen.height / 2;
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}

	void Minimize()
	{
		CloseAllMainMenus();

		if (minimize == true && CompactMode == false) 
		{
			ShowApplications = true;
			Title = " - Applications";
			CloseButton = new Rect(441, 1, 21, 21);
			MiniButton = new Rect(419, 1, 21, 21);
			SettingsButton = new Rect(397, 1, 21, 21);
			ChangeLayout = false;
			windowRect = (new Rect(windowRect.x,windowRect.y,463,23));
		}
		if (minimize == false && CompactMode == false) 
		{
			ShowApplications = true;
			Title = " - Applications";
			CloseButton = new Rect(441, 1, 21, 21);
			MiniButton = new Rect(419, 1, 21, 21);
			SettingsButton = new Rect(397, 1, 21, 21);
			CompactModeName = "";
			ChangeLayout = false;
			windowRect = (new Rect(windowRect.x,windowRect.y,510,200));
		}
		if (minimize == true && CompactMode == true) 
		{
			CloseButton = new Rect(122, 24, 21, 21);
			MiniButton = new Rect(100, 24, 21, 21);
			SettingsButton = new Rect(78, 24, 21, 21);
			ChangeLayout = true;
			showSpecs = true;
			windowRect = (new Rect(windowRect.x,windowRect.y,145,173));
		}
		if (minimize == false && CompactMode == true) 
		{
			ShowApplications = true;
			Title = " - Applications";
			CloseButton = new Rect(441, 1, 21, 21);
			MiniButton = new Rect(419, 1, 21, 21);
			SettingsButton = new Rect(397, 1, 21, 21);
			CompactModeName = "";
			ChangeLayout = false;
			windowRect = (new Rect(windowRect.x,windowRect.y,463,200));
		}
	}

	void CloseAllMainMenus()
	{
		ShowApplications = false;
		showStartUp = false;
		showDetails = false;
		showServices = false;
		showPerformance = false;
		showUsers = false;
		showHistory = false;
		showSettings = false;
		showSpecs = false;
		CloseAllMainMenuSubMenus();
	}

	void CloseAllMainMenuSubMenus()
	{
		showCPU = false;
		showRAM = false;
		showPSU = false;
		showGPU = false;
		showDisk = false;
		showNET = false;
		AnyShowen = false;
        Selected = -1;
		Index = 0;
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = com.Skin[GameControl.control.GUIID];

		//set up scaling
		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}

		if (ShowContext == true) 
		{
			ContextwindowRect.height = 21*ContextMenuOptions.Count+2;
			GUI.skin = com.Skin[GameControl.control.GUIID];
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			ContextwindowRect = WindowClamp.ClampToScreen(GUI.Window(ContextMenuID,ContextwindowRect,DoMyContextWindow,""));
		}

		if (Input.GetMouseButtonUp (0) || Input.GetMouseButtonUp (1) || Input.GetMouseButtonUp (2))
		{
			if (!ContextwindowRect.Contains (Event.current.mousePosition)) 
			{
				ShowContext = false;
			} 
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.DragWindow (new Rect (1, 1, 395, 21));

		if (CompactMode == true) 
		{
			CompactModeName = " - Compact";
		} 
		else 
		{
			CompactModeName = " - Normal";
		}

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
                appman.SelectedApp = "Task Viewer";

            }
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
			{
                appman.SelectedApp = "Task Viewer";
            }
		}

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		if (MiniButton.Contains (Event.current.mousePosition)) 
		{
			if (CompactMode == true) 
			{
				if (GUI.Button (new Rect (MiniButton), "-", com.Skin [GameControl.control.GUIID].customStyles [2])) {
					minimize = !minimize;
					Minimize ();
				}
			} 
			else 
			{
				if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
				{
					minimize = !minimize;
					Minimize();
				}
			}
		} 
		else
		{
			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		}

		if (CompactMode) 
		{
			GUI.Box (new Rect (2, 2, 141, 21), "Application Manager");
		} 
		else 
		{
			GUI.Box (new Rect (1, 1, 395, 21), "Application Manager" + Title);
		}

		GUI.contentColor = Color.white;
		if (GUI.Button (new Rect (SettingsButton), Pics [3]))
		{
			Title = " - Settings";
			CloseAllMainMenus();
			showSettings = true;
		}

		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		MenuSystem();

		if (showPerformance == true)
		{
			GUI.Button (new Rect (2, 50, 40, 40),Pics[1]);
			GUI.Button (new Rect (2, 100, 40, 40),Pics[2]);
			//GUI.Button (new Rect (2, 50, 60, 20),"Memory");
			//GUI.Button (new Rect (2, 50, 60, 20),"GPU");
			//GUI.Button (new Rect (2, 50, 60, 20),"Disk");
			//GUI.Button (new Rect (2, 50, 60, 20),"Network");

//			scrollpos = GUI.BeginScrollView(new Rect(2, 71, 500, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
//			for (scrollsize = 0; scrollsize < RunningApplications.Count; scrollsize++)
//			{
//				//GUI.Button (new Rect (2, scrollsize * 20, 20, 20),"" + scrollsize);
//				GUI.Button (new Rect (0, scrollsize * 20, 150, 20), RunningApplications [scrollsize]);
//				GUI.Button (new Rect (151, scrollsize * 20, 60, 20),"" + CPU [scrollsize]);
//				GUI.Button (new Rect (212, scrollsize * 20, 60, 20),"" + Memory [scrollsize]);
//				GUI.Button (new Rect (273, scrollsize * 20, 60, 20),"" + Graphics [scrollsize]);
//				GUI.Button (new Rect (334, scrollsize * 20, 60, 20),"" + Disk [scrollsize]);
//				GUI.Button (new Rect (395, scrollsize * 20, 60, 20),"" + Network [scrollsize]);
//			}
//			GUI.EndScrollView();
		}

		if (ShowApplications == true) 
		{
			GUI.Button (new Rect (1, 42 + 2, 150, 20),"Application Name");
			GUI.Button (new Rect (152, 42 + 2, 60, 20),"CPU");
			GUI.Button (new Rect (213, 42 + 2, 60, 20),"Memory");
			GUI.Button (new Rect (274, 42 + 2, 60, 20),"GPU");
			GUI.Button (new Rect (335, 42 + 2, 60, 20),"Disk");
			GUI.Button (new Rect (396, 42 + 2, 66, 20),"Network");

			scrollpos = GUI.BeginScrollView(new Rect(1, 65 + 0, 500, 125), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < RunningTasks.Count; scrollsize++)
			{
				//GUI.Button (new Rect (2, scrollsize * 20, 20, 20),"" + scrollsize);
				if (GUI.Button(new Rect(0, scrollsize * 21, 150, 20), RunningTasks[scrollsize].ProgramName))
				{
					if (Input.GetMouseButtonUp (0)) 
					{
						if (Time.time - LastClick < Customize.cust.DoubleClickDelayMenu) 
						{
							PlayClickSound ();
							SelectedProgram = scrollsize;
							//OpenFld();
						} 
						else 
						{
							PlayClickSound();
							SelectedProgram = scrollsize;
						}
						LastClick = Time.time;
						ShowContext = false;
					}
					if (Input.GetMouseButtonUp (1)) 
					{
						ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
						SelectedProgram = scrollsize;
						if (new Rect (0, scrollsize * 21, 150, 20).Contains (Event.current.mousePosition))
						{
							for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
							{
								if (GameControl.control.ProgramFiles[i].Target == RunningTasks[SelectedProgram].RunningApplications)
								{
									SelectedTask = GameControl.control.ProgramFiles[i];
								}
							}
							ContextwindowRect.x = Input.mousePosition.x;
							ContextwindowRect.y = Screen.height - Input.mousePosition.y;
							ShowContext = true;
							GUI.BringWindowToFront(ContextMenuID);
						}
					}
				}

                GUI.Button(new Rect(151, scrollsize * 21, 60, 20), "" + RunningTasks[scrollsize].CPU.ToString("F2") + "%");
                GUI.Button(new Rect(212, scrollsize * 21, 60, 20), "" + RunningTasks[scrollsize].Memory.ToString("F2") + "%");
                GUI.Button(new Rect(273, scrollsize * 21, 60, 20), "" + RunningTasks[scrollsize].Graphics.ToString("F2") + "%");
                GUI.Button(new Rect(334, scrollsize * 21, 60, 20), "" + RunningTasks[scrollsize].Disk.ToString("F2") + "%");
                GUI.Button(new Rect(395, scrollsize * 21, 60, 20), "" + RunningTasks[scrollsize].Network.ToString("F2") + "%");

                //            GUI.Button (new Rect (151, scrollsize * 21, 60, 20),"" + CPUList [scrollsize].ToString("F2") + "%");
                //GUI.Button (new Rect (212, scrollsize * 21, 60, 20),"" + MemoryList [scrollsize].ToString("F2") + "%");
                //GUI.Button (new Rect (273, scrollsize * 21, 60, 20),"" + GraphicsList [scrollsize].ToString("F2") + "%");
                //GUI.Button (new Rect (334, scrollsize * 21, 60, 20),"" + DiskList [scrollsize].ToString("F2") + "%");
                //GUI.Button (new Rect (395, scrollsize * 21, 66, 20),"" + NetworkList [scrollsize].ToString("F2") + "%");
            }
			GUI.EndScrollView();
		}

		if (showSettings == true) 
		{
			GUI.Label (new Rect (1,170,200,21),hint);

			if (CompactMode) 
			{
				if (GUI.Button (new Rect (1, 44 + 2, 150, 20), "Normal Mode")) 
				{
					CompactMode = false;
					showSettings = false;
					minimize = false;
					Minimize();
				}
			} 
			else 
			{
				if (GUI.Button (new Rect (1, 44 + 2, 150, 20), "Compact Mode")) 
				{
					CompactMode = true;
					showSpecs = true;
					showSettings = false;
					minimize = true;
					Minimize();
				}
			}
		}

		if (ChangeLayout == true) 
		{
			if (AnyShowen == false)
			{
				if (GUI.Button (new Rect (2, 24, 75, 21), "Specs")) 
				{
					showSettings = false;
					showSpecs = true;
				}
			}
			if (AnyShowen == true)
			{
				if (GUI.Button (new Rect (2, 24, 74, 21), "Back")) 
				{
					CloseAllMainMenuSubMenus();
				}
			}
		}

		if (showSpecs == true && ChangeLayout == true) 
		{
			showSettings = false;

            if (showCPU == true)
            {
                for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
                {
                    if (Selected == -1)
                    {
                        if (GUI.Button(new Rect(1, 46 + 22 * i, 150, 22), "Slot" + i + " - " + GameControl.control.Gateway.InstalledCPU[i].Name))
                        {
                            Selected = i;
                        }
                    }

                    if (Selected != -1)
                    {
                        for (Index = 0; Index < GameControl.control.Gateway.InstalledCPU[Selected].Cores; Index++)
                        {
                            GUI.Label(new Rect(1, 66 + 22 * Index, 150, 22), "Core" + Index + "-" + GameControl.control.Gateway.InstalledCPU[Selected].Cores + GameControl.control.SpaceName + "s");
                        }

                        GUI.Label(new Rect(1, 84 + 22 * Index, 150, 22), "Core Voltage: " + GameControl.control.Gateway.InstalledCPU[Selected].Voltage.ToString("F2"));

                        GUI.Label(new Rect(1, 84 + 44 * Index, 150, 22), "Health: %" + GameControl.control.Gateway.InstalledCPU[Selected].HealthPercentage.ToString("F0"));
                    }
                }
                //for (Index = 0; Index < cpu.CPUSpeed.Count; Index++)
                //{
                //    GUI.Label(new Rect(1, 66 + 22 * Index, 150, 22), "Core" + Index + "-" + cpu.CPUSpeed[Index] + GameControl.control.SpaceName + "s");
                //}
                ////GUI.Label(new Rect (1, 66 + 22 * Index, 150, 22), "Core Temp: " + cpu.CPUTemp.ToString("F0"));
                //GUI.Label(new Rect(1, 84 + 22 * Index, 150, 22), "Core Voltage: " + cpu.Voltage.ToString("F2"));
            }
            if (showGPU == true)
            {
                for (int i = 0; i < GameControl.control.Gateway.InstalledGPU.Count; i++)
                {
                    if (Selected == -1)
                    {
                        if (GUI.Button(new Rect(1, 46 + 22 * i, 150, 22), "Slot" + i + " - " + GameControl.control.Gateway.InstalledGPU[i].Name))
                        {
                            Selected = i;
                        }
                    }

                    if (Selected != -1)
                    {
                        GUI.Label(new Rect(1, 22 + 22, 250, 22), "Total Memory" + "-" + GameControl.control.Gateway.InstalledGPU[Selected].MaxMemory + GameControl.control.SpaceName + "s");

                        GUI.Label(new Rect(1, 44 + 22, 250, 22), "Used Memory" + "-" + GameControl.control.Gateway.InstalledGPU[Selected].MemoryUsage + GameControl.control.SpaceName + "s");

                        GUI.Label(new Rect(1, 66 + 22, 150, 22), "GPU Cores" + "-" + GameControl.control.Gateway.InstalledGPU[Selected].Cores);

                        GUI.Label(new Rect(1, 84 + 22, 150, 22), "GPU Voltage: " + GameControl.control.Gateway.InstalledGPU[Selected].Voltage.ToString("F2"));

                        GUI.Label(new Rect(1, 84 + 44, 150, 22), "Speed: " + GameControl.control.Gateway.InstalledGPU[Selected].MaxSpeed + GameControl.control.SpaceName + "s");

                        GUI.Label(new Rect(1, 84 + 66, 150, 22), "Health: %" + GameControl.control.Gateway.InstalledGPU[Selected].HealthPercentage.ToString("F0"));
                    }
                }
                //for (Index = 0; Index < cpu.CPUSpeed.Count; Index++)
                //{
                //    GUI.Label(new Rect(1, 66 + 22 * Index, 150, 22), "Core" + Index + "-" + cpu.CPUSpeed[Index] + GameControl.control.SpaceName + "s");
                //}
                ////GUI.Label(new Rect (1, 66 + 22 * Index, 150, 22), "Core Temp: " + cpu.CPUTemp.ToString("F0"));
                //GUI.Label(new Rect(1, 84 + 22 * Index, 150, 22), "Core Voltage: " + cpu.Voltage.ToString("F2"));
            }

            if (showDisk == true)
            {
                for (int i = 0; i < GameControl.control.Gateway.InstalledStorageDevice.Count; i++)
                {
                    if (Selected == -1)
                    {
                        if (GUI.Button(new Rect(1, 46 + 22 * i, 150, 22), "" + i + " - " + GameControl.control.Gateway.InstalledStorageDevice[i].Name))
                        {
                            Selected = i;
                        }
                    }

                    if (Selected != -1)
                    {
                        GUI.Label(new Rect(1, 22 + 22, 250, 22), "Capacity: " + GameControl.control.Gateway.InstalledStorageDevice[Selected].Capacity + GameControl.control.SpaceName + "s");

                        GUI.Label(new Rect(1, 44 + 22, 250, 22), "Free: " + GameControl.control.Gateway.InstalledStorageDevice[Selected].FreeSpace + GameControl.control.SpaceName + "s");

                        GUI.Label(new Rect(1, 66 + 22, 250, 22), "Power Usage: " + GameControl.control.Gateway.InstalledStorageDevice[Selected].PowerUsage);

                        GUI.Label(new Rect(1, 84 + 22, 150, 22), "Type: " + GameControl.control.Gateway.InstalledStorageDevice[Selected].Type);

                        GUI.Label(new Rect(1, 84 + 44, 150, 22), "Speed: " + GameControl.control.Gateway.InstalledStorageDevice[Selected].Speed + GameControl.control.SpaceName + "s");

                        GUI.Label(new Rect(1, 84 + 66, 150, 22), "Health: %" + GameControl.control.Gateway.InstalledStorageDevice[Selected].HealthPercentage.ToString("F0"));
                    }
                }
            }

            if (AnyShowen == false) 
			{
				if(GUI.Button(new Rect (2, 46, 141, 20), "CPU:" + cpu.MaxCPUSpeed + GameControl.control.SpaceName + "s"))
				{
					CloseAllMainMenuSubMenus();
					showCPU = true;
					AnyShowen = true;
				}

				float RemainingRAMUI = ram.RemainingRAM / 1024;
				float MaxRAMUI = ram.MaxRAM / 1024;

				if(GUI.Button(new Rect (2, 67, 141, 20), "RAM:" + RemainingRAMUI.ToString("F2") + " / " + MaxRAMUI.ToString("F2")))
				{
					CloseAllMainMenuSubMenus();
					showRAM = true;
					AnyShowen = true;
				}

				if(GUI.Button(new Rect (2, 88, 141, 20), "Disk:" + HardwareController.hdcon.HDDFreeSpace + GameControl.control.SpaceName + "/" + HardwareController.hdcon.HDDMaxSpace + GameControl.control.SpaceName))
				{
					CloseAllMainMenuSubMenus();
					showDisk = true;
					AnyShowen = true;
				}

				if(GUI.Button(new Rect (2, 109, 141, 20), "Network:" + HardwareController.hdcon.networkspeed + GameControl.control.SpaceName + "s" + "/" + HardwareController.hdcon.Maxsnetworkspeed + GameControl.control.SpaceName + "s"))
				{
					CloseAllMainMenuSubMenus();
					showNET = true;
					AnyShowen = true;
				}

				if(GUI.Button(new Rect (2, 130, 141, 20), "GPU:" + GameControl.control.Gateway.InstalledGPU[0].MemoryUsage + " / " + GameControl.control.Gateway.InstalledGPU[0].MaxMemory))
				{
					CloseAllMainMenuSubMenus();
					showGPU = true;
					AnyShowen = true;
				}

				if(GUI.Button(new Rect (2, 151, 141, 20), "PSU:" + GameControl.control.Gateway.InstalledPSU[0].Remaining + " / " + GameControl.control.Gateway.InstalledPSU[0].Max))
				{
					CloseAllMainMenuSubMenus();
					showPSU = true;
					AnyShowen = true;
				}
			}
		}
	}

	void AddContextOptions()
	{
		//if (GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Exe)
		//{
		//	ContextMenuOptions.Add ("Open");
		//	//ContextMenuOptions.Add ("Copy");
		//	ContextMenuOptions.Add ("Rename");
		//	if (!GameControl.control.QuickLaunchNames.Contains (GameControl.control.DesktopIconList [SelectedProgram].Name)) 
		//	{
		//		ContextMenuOptions.Add ("Pin to QL");
		//	}
		//	else
		//	{
		//		ContextMenuOptions.Add ("Unpin from QL");
		//	}
		//	ContextMenuOptions.Add ("Delete");
		//	//ContextMenuOptions.Add ("Create Icon");
		//	ContextMenuOptions.Add ("Details");
		//}
		//else if (GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Dir || GameControl.control.DesktopIconList [SelectedProgram].Type == ProgramSystem.ProgramType.Fdl)
		//{
		//	ContextMenuOptions.Add ("Open");
		//	//ContextMenuOptions.Add ("Create Icon");
		//	ContextMenuOptions.Add ("Delete");
		//	ContextMenuOptions.Add ("Details");
		//}
		//else 
		//{
		//	ContextMenuOptions.Add ("Open");
		//	//ContextMenuOptions.Add ("Copy");
		//	ContextMenuOptions.Add ("Rename");
		//	ContextMenuOptions.Add ("Delete");
		//	//ContextMenuOptions.Add ("Create Icon");
		//	ContextMenuOptions.Add ("Details");
		//}
			ContextMenuOptions.Add ("Kill");
			//ContextMenuOptions.Add ("Copy");
			ContextMenuOptions.Add ("Location");
			//ContextMenuOptions.Add ("Create Icon");
			ContextMenuOptions.Add ("Details");
	}

	void PlayClickSound()
	{
		sc.SoundSelect = 3;
		sc.PlaySound();
	}

	void DoMyContextWindow(int WindowID)
	{
		//GUI.Box (new Rect (Input.mousePosition.x, Input.mousePosition.y, 100, 200), "");
		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		if (ContextMenuOptions.Count <= 0) 
		{
			AddContextOptions();
		}

		if (ContextMenuOptions.Count > 0) 
		{
			for (int i = 0; i < ContextMenuOptions.Count; i++) 
			{
				if (GUI.Button (new Rect (1, 1+21*i, ContextwindowRect.width - 2, 21), ContextMenuOptions[i])) 
				{
					SelectedOption = ContextMenuOptions [i];
				}
			}
		}

		switch (SelectedOption) 
		{
		case "Kill":
			switch (SelectedTask.Type) 
			{
			case ProgramSystem.ProgramType.Txt:
				//EndProgram();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Fdl:
				//OpenFld();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Ins:
				//OpenIns();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Exe:
				EndProgram();
				CloseContextMenu();
				break;
			case ProgramSystem.ProgramType.Dir:
				//OpenFld();
				CloseContextMenu();
				break;
			}
			break;
		case "Location":
			ProgramLocation();
			PlayClickSound();
			CloseContextMenu();
			break;
		case "Details":
			//DeleteSystem();
			PlayClickSound();
			CloseContextMenu();
			break;
		}
	}

	void ProgramLocation()
	{
		PlayClickSound ();
		if(com.show == true)
		{
			com.ComAddress = SelectedTask.Location;
		}
		else
		{
			appman.SelectedApp = "Computer";
			com.ComAddress = SelectedTask.Location;
		}
//		SelectedProgram = -1;
//		com.MenuSelected = 0;
	}

	public void EndProgram()
	{
        PlayClickSound();
        appman.SelectedApp = SelectedTask.Target;
        SelectedProgram = -1;
        com.MenuSelected = 0;
    }


	void CloseContextMenu()
	{
		ContextMenuOptions.RemoveRange (0, ContextMenuOptions.Count);
		SelectedOption = "";
		ShowContext = false;
	}

	void MenuSystem()
	{
		if (ChangeLayout == false) 
		{
			if (GUI.Button (new Rect (1, 23, 85, 20), "Applications"))
			{
				Title = " - Applications";
				CloseAllMainMenus();
				ShowApplications = true;
			}

			if (GUI.Button (new Rect (87, 23, 60, 20), "Start Up")) 
			{
				Title = " - Start Up";
				CloseAllMainMenus();
				showStartUp = true;
			}

			if (GUI.Button (new Rect (148, 23, 50, 20), "Details")) 
			{
				Title = " - Details";
				CloseAllMainMenus();
				showDetails = true;
			}

			if (GUI.Button (new Rect (199, 23, 60, 20), "Services")) 
			{
				Title = " - Details";
				CloseAllMainMenus();
				showServices = true;
			}

			if (GUI.Button (new Rect (260, 23, 90, 20), "Performance"))
			{
				Title = " - Performance";
				CloseAllMainMenus();
				showPerformance = true;
			}

			if (GUI.Button (new Rect (351, 23, 50, 20), "Users"))
			{
				Title = " - Users";
				CloseAllMainMenus();
				showUsers = true;
			}

			if (GUI.Button (new Rect (402, 23, 60, 20), "History")) 
			{
				Title = " - History";
				CloseAllMainMenus();
				showHistory = true;
			}
		}
	}
}