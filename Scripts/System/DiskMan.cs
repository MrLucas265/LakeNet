//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;

//public class DiskMan : MonoBehaviour 
//{
//	public GameObject SysSoftware;
//	public bool show;
//	private Computer com;    
//	public int windowID;
//	public Rect windowRect;
//	public float native_width = 1920;
//	public float native_height = 1080;
//	public bool Drag;

//	private Defalt defalt;

//	public int SelectedDocument;

//	public Vector2 scrollpos = Vector2.zero;
//	public int scrollsize;

//	public Rect CloseButton;
//	public Rect MiniButton;
//	public Rect DefaltSetting;
//	public Rect DefaltBoxSetting;

//	private SoundControl sc;

//	public bool minimize;

//	public List<string> CachedDrives = new List<string>();
//	public List<string> CachedDriveName = new List<string>();
//	public List<float> UsedSpace = new List<float>();
//	public List<float> DriveCapacity = new List<float>();
//	public List<float> FreeSpace = new List<float>();
//	public List<float> DriveHealth = new List<float>();
//	public string DriveLetter;
//	public string DriveLabel;
//	public float DiskCapacity;
//	public float AllocatedSpace;
//    public string AllocatedSpaceString;
//    public bool ShowDriveMan;
//	public int Selected;
//	const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
//	public List<string> AvalibleDriveLetters = new List<string>();
//	public int Index;
//	public int Select;
//	public int SelectedPartition;

//	void Start ()
//	{
//		SysSoftware = GameObject.Find("System");
//		com = SysSoftware.GetComponent<Computer>();
//		defalt = SysSoftware.GetComponent<Defalt>();

//		native_height = Customize.cust.native_height;
//		native_width = Customize.cust.native_width;

//		PosCheck ();

//		windowRect = new Rect(windowRect.x,windowRect.y,361,200);

//		CloseButton = new Rect(338,2,21,21);
//		MiniButton = new Rect(316,2,21,21);

//		DefaltSetting.width = 361;
//		DefaltSetting.height = 200;
//	}

//	void PosCheck()
//	{
//		if (Customize.cust.windowx[windowID] == 0) 
//		{
//			Customize.cust.windowx [windowID] = Screen.width / 2;
//		}
//		if (Customize.cust.windowy[windowID] == 0) 
//		{
//			Customize.cust.windowy [windowID] = Screen.height / 2;
//		}

//		windowRect.x = Customize.cust.windowx[windowID];
//		windowRect.y = Customize.cust.windowy[windowID];
//	}


//	void DiskCheck()
//	{		
//		CachedDrives.RemoveRange(0, CachedDrives.Count);
//		CachedDriveName.RemoveRange(0, CachedDriveName.Count);
//		UsedSpace.RemoveRange(0, UsedSpace.Count);
//		DriveCapacity.RemoveRange(0, DriveCapacity.Count);
//		DriveHealth.RemoveRange(0, DriveHealth.Count);
//		FreeSpace.RemoveRange(0, FreeSpace.Count);

//		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
//		{
//			if (GameControl.control.ProgramFiles[i].Type == ProgramSystem.ProgramType.Dir)
//			{
//				if (!CachedDrives.Contains (GameControl.control.ProgramFiles [i].Name))
//				{
//					CachedDrives.Add(GameControl.control.ProgramFiles[i].Name);
//					CachedDriveName.Add (GameControl.control.ProgramFiles [i].Sender);
//					DriveCapacity.Add (GameControl.control.ProgramFiles [i].Capacity);
//					DriveHealth.Add (GameControl.control.ProgramFiles [i].Health);
//					UsedSpace.Add(GameControl.control.ProgramFiles [i].Used);
//					FreeSpace.Add(GameControl.control.ProgramFiles [i].Free);
//				}
//			}
//		}
//	}

//	public void SpaceCheck()
//	{
//		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
//		{
//			for(int j = 0; j < CachedDrives.Count; j++)
//			{
//				if (GameControl.control.ProgramFiles[i].Type == ProgramSystem.ProgramType.Exe || GameControl.control.ProgramFiles[i].Type == ProgramSystem.ProgramType.Txt || GameControl.control.ProgramFiles[i].Type == ProgramSystem.ProgramType.File || GameControl.control.ProgramFiles[i].Type == ProgramSystem.ProgramType.OS || GameControl.control.ProgramFiles[i].Type == ProgramSystem.ProgramType.Ins)
//				{
//					if (GameControl.control.ProgramFiles[i].Location.StartsWith(CachedDrives[j]))
//					{
//						if (UsedSpace.Count > 0)
//						{
//							UsedSpace[j] += GameControl.control.ProgramFiles[i].Used;
//							FreeSpace[j] = DriveCapacity[j] - UsedSpace[j];
//						}
//					}
//				}
//			}
//		}
//	}

//	public void NewDrive()
//	{
//		if (DriveLetter != "") 
//		{
//			if (glyphs.Contains(DriveLetter))
//			{
//				if (DriveLetter.Length == 1)
//				{
//					if (!CachedDrives.Contains("" + DriveLetter + ":/"))
//					{
//                        GameControl.control.ProgramFiles.Add (new ProgramSystem ("" + DriveLetter + ":/", DriveLabel, "", "", "Gateway", DriveLetter + ":/", 0,0,0,AllocatedSpace,100,0, false, ProgramSystem.ProgramType.Dir));
//                        GameControl.control.Gateway.InstalledStorageDevice[Selected].UsedSpace += AllocatedSpace;
//                        GameControl.control.Gateway.InstalledStorageDevice[Selected].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[Selected].Capacity - GameControl.control.Gateway.InstalledStorageDevice[Selected].UsedSpace;
//                        DriveLetter = "";
//						DriveLabel = "";
//						AllocatedSpace = 0;
//					} 
//				}
//			}
//		}
//	}

//    public void RemoveDrive()
//    {

//    }

//    void Update()
//	{
//		DiskCheck();
//		SpaceCheck();
//	}

//	void OnGUI()
//	{
//		Customize.cust.windowx[windowID] = windowRect.x;
//		Customize.cust.windowy[windowID] = windowRect.y;
//		GUI.skin = com.Skin[GameControl.control.GUIID];

//		if(show == true)
//		{
//			GUI.color = com.colors[Customize.cust.WindowColorInt];
//			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
//		}
//	}

//	void Minimize()
//	{
//		if (minimize == true) 
//		{
//			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,25));
//		}
//		else
//		{
//			//private Rect DefaltSetting = new Rect (windowRect.x,windowRect.y,300,205);
//			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,DefaltSetting.height));
//		}
//	}

//	void DoMyWindow(int windowID)
//	{
//		if (CloseButton.Contains (Event.current.mousePosition))
//		{
//			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0]))
//			{
//				show = false;
//			}
//		} 
//		else 
//		{
//			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
//			GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1]);
//		}

//		if (MiniButton.Contains (Event.current.mousePosition)) 
//		{
//			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
//			{
//				minimize = !minimize;
//				Minimize();
//			}
//		} 
//		else
//		{
//			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
//			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
//			{
//				minimize = !minimize;
//				Minimize();
//			}
//		}

//		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

//		GUI.DragWindow(new Rect(2,2,312,21));
//		GUI.Box (new Rect (2, 2, 313, 21), "Disk Management");

//		Render();
//	}

//	void Render()
//	{
//		if (ShowDriveMan == true)
//		{
//			if (GUI.Button (new Rect (2, 24, 40, 20), "Back"))
//			{
//				ShowDriveMan = !ShowDriveMan;
//			}

//			if (GUI.Button (new Rect (43, 24, 70, 20), "New Drive"))
//			{
//                AllocatedSpace = float.Parse(AllocatedSpaceString);
//                float Math = GameControl.control.Gateway.InstalledStorageDevice[Selected].FreeSpace - AllocatedSpace;
//                if (Math >= 0)
//                {
//                    NewDrive();
//                }
//			}

//            if(Selected > 0)
//            {
//                if (GUI.Button(new Rect(115, 24, 100, 20), "Remove Drive"))
//                {
//                    RemoveDrive();
//                }
//            }

//            //			if (Select >= 1) 
//            //			{
//            //				if (GUI.Button (new Rect (42, 23, 70, 20), "Delete Drive"))
//            //				{
//            //					GameControl.control.ProgramFiles.Remove (CachedDrives[Select]);
//            //				}
//            //			}

//            GUI.Button(new Rect (2, 45, 20, 20), "ID");
//			GUI.Button(new Rect (23, 45, 150, 20), "Unallocated Space");
//			scrollpos = GUI.BeginScrollView(new Rect(1, 66, 200, 63), scrollpos, new Rect(0, 0, 0, scrollsize*21));
//			for (scrollsize = 0; scrollsize < GameControl.control.Gateway.InstalledStorageDevice.Count; scrollsize++)
//			{
//				GUI.Button (new Rect (1, 21 * scrollsize, 20, 20), "" + scrollsize);
//				if (GUI.Button (new Rect (22, 21 * scrollsize, 150, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[scrollsize].FreeSpace)) 
//				{
//					Selected = scrollsize;
//				}
//                if (Selected == scrollsize)
//                {
//                    GUI.Button(new Rect(173, 21 * scrollsize, 20, 20), "☒");
//                }
//                else
//                {
//                    GUI.Button(new Rect(173, 21 * scrollsize, 20, 20), "☐");
//                }

//            }
//			GUI.EndScrollView();

//			if (Selected != -1)
//			{
//				GUI.Label (new Rect (102, 125, 21, 21), "" + Selected);

//				DriveLetter = GUI.TextField (new Rect (2, 125, 50, 21),"" + DriveLetter);
//				DriveLabel = GUI.TextField (new Rect (75, 125, 50, 21),"" + DriveLabel);

//                AllocatedSpaceString = GUI.TextField(new Rect(2, 150, 100, 21), "" + AllocatedSpaceString);
//                AllocatedSpaceString = Regex.Replace(AllocatedSpaceString, @"[^0-9]", "");
//            }

//		} 
//		else 
//		{
//			if (GUI.Button (new Rect (2, 24, 70, 20), "New Drive"))
//			{
//				ShowDriveMan = !ShowDriveMan;
//			}
//			GUI.Button(new Rect (2, 45, 25, 20), "ID");
//			GUI.Button(new Rect (28, 45, 74, 20), "Name");
//			GUI.Button(new Rect (103, 45, 50, 20), "Used");
//			GUI.Button(new Rect (154, 45, 50, 20), "Free");
//			GUI.Button(new Rect (205, 45, 50, 20), "Total");
//			GUI.Button(new Rect (256, 45, 50, 20), "Status");
//			GUI.Button(new Rect (307, 45, 52, 20), "Free %");
//			if (CachedDrives.Count > 0)
//			{
//				scrollpos = GUI.BeginScrollView(new Rect(0, 66, 360, 100), scrollpos, new Rect(0, 0, 0, scrollsize*21));
//				for (scrollsize = 0; scrollsize < CachedDrives.Count; scrollsize++) 
//				{
//					if(GUI.Button(new Rect(2,21 * scrollsize,25,20),"" + CachedDrives[scrollsize])) 
//					{
//						Select = scrollsize;
//					}

//					if(GUI.Button(new Rect(28,21 * scrollsize,74,20),"" + CachedDriveName[scrollsize])) 
//					{
//						Select = scrollsize;
//					}

//					if(GUI.Button(new Rect(103,21 * scrollsize,50,20),"" + UsedSpace[scrollsize])) 
//					{

//					}

//					if(GUI.Button(new Rect(205,21 * scrollsize,50,20),"" + DriveCapacity[scrollsize])) 
//					{

//					}

//					if (UsedSpace [scrollsize] == 0) 
//					{
//						if (GUI.Button (new Rect (154, 21 * scrollsize, 50, 20), "" + DriveCapacity [scrollsize]))
//						{

//						}

//						if(GUI.Button(new Rect(307,21 * scrollsize,52,20),"%100")) 
//						{

//						}
//					} 
//					else 
//					{
//						if(GUI.Button(new Rect(154,21 * scrollsize,50,20),"" + FreeSpace[scrollsize])) 
//						{

//						}

//						if(GUI.Button(new Rect(307,21 * scrollsize,52,20),"%" + FreeSpace[scrollsize] / DriveCapacity[scrollsize] * 100)) 
//						{

//						}
//					}

//					if(GUI.Button(new Rect(256,21 * scrollsize,50,20),"%" + DriveHealth[scrollsize])) 
//					{

//					}
//				}
//				GUI.EndScrollView();
//			}
//		}
//	}
//}
