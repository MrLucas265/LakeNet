using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class DiskManV2 : MonoBehaviour
{
	public GameObject SysSoftware;
	public bool show;
	private Computer com;
	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	private Defalt defalt;

	public int SelectedDocument;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	private SoundControl sc;

	public bool minimize;

	public string DriveLetter;
	public string DriveLabel;
	public float DiskCapacity;
	public float AllocatedSpace;
	public string AllocatedSpaceString;
	public bool ShowDriveMan;
	public int Selected;
	const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public List<string> AvalibleDriveLetters = new List<string>();
	public int Index;
	public int Select;
	public int SelectedPartition;
	public int SelectedMenu;

	public bool TestDone;

	public bool RemovingFiles;

	public List<ProgramSystem> TempPrograms = new List<ProgramSystem>();

	public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
	public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

	private AppMan appman;

	void Start()
	{
		SysSoftware = GameObject.Find("System");
		com = SysSoftware.GetComponent<Computer>();
		defalt = SysSoftware.GetComponent<Defalt>();
		appman = SysSoftware.GetComponent<AppMan>();

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		PosCheck();

		windowRect = new Rect(windowRect.x, windowRect.y, 450, 200);

		CloseButton = new Rect(windowRect.width-23, 2, 21, 21);
		MiniButton = new Rect(CloseButton.x-22, 2, 21, 21);

		DefaltSetting.width = 450;
		DefaltSetting.height = 200;
	}

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0)
		{
			Customize.cust.windowx[windowID] = Screen.width / 2;
		}
		if (Customize.cust.windowy[windowID] == 0)
		{
			Customize.cust.windowy[windowID] = Screen.height / 2;
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}

	public void ResetPartitionUsed()
	{
		for (int j = 0; j < GameControl.control.Gateway.InstalledStorageDevice.Count; j++)
		{
			for (int k = 0; k < GameControl.control.Gateway.InstalledStorageDevice[j].Partitions.Count; k++)
			{
				GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Used = 0;
			}
		}
	}

	public void FileSizesCheck()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			for (int j = 0; j < GameControl.control.Gateway.InstalledStorageDevice.Count; j++)
			{
				for (int k = 0; k < GameControl.control.Gateway.InstalledStorageDevice[j].Partitions.Count; k++)
				{
					if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.Exe || GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.Txt || GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.File || GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.OS || GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.Ins)
					{
						if (GameControl.control.ProgramFiles[i].Location.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].DriveLetter))
						{

							GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Used += GameControl.control.ProgramFiles[i].Used;
							GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Free = GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Size - GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Used;
						}
					}
				}
			}
		}
	}

	public void UpdateDirSize()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			for (int j = 0; j < GameControl.control.Gateway.InstalledStorageDevice.Count; j++)
			{
				for (int k = 0; k < GameControl.control.Gateway.InstalledStorageDevice[j].Partitions.Count; k++)
				{
					if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.Dir)
					{
						if (GameControl.control.ProgramFiles[i].Name.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].DriveLetter))
						{
							GameControl.control.ProgramFiles[i].Used = GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Used;
							GameControl.control.ProgramFiles[i].Free = GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Size - GameControl.control.ProgramFiles[i].Used;
							GameControl.control.ProgramFiles[i].Capacity = GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Size;
						}
					}
				}
			}
		}
	}

	public void UpdateDirSize1()
	{
		for (int i = 0; i < PersonController.control.People.Count; i++)
		{
			for (int j = 0; j < PersonController.control.People[i].Gateway.Files.FileList.Count; j++)
			{
				for (int k = 0; k < PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice.Count; k++)
				{
					for (int l = 0; l < PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions.Count; l++)
					{
						if (PersonController.control.People[i].Gateway.Files.FileList[j].Extension == ProgramSystem.FileExtension.Dir)
						{
							if (GameControl.control.ProgramFiles[i].Name.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].DriveLetter))
							{
								PersonController.control.People[i].Gateway.Files.FileList[j].Used = PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[l].Used;
								PersonController.control.People[i].Gateway.Files.FileList[j].Free = PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[l].Size - PersonController.control.People[i].Gateway.Files.FileList[j].Used;
								PersonController.control.People[i].Gateway.Files.FileList[j].Capacity = PersonController.control.People[i].Gateway.Motherboard.InstalledStorageDevice[j].Partitions[l].Size;
							}
						}
					}
				}
			}
		}
	}

	public void NewDrive()
	{
		if (DriveLetter != "")
		{
			if (glyphs.Contains(DriveLetter))
			{
				if (DriveLetter.Length == 1)
				{
					for (int j = 0; j < GameControl.control.Gateway.InstalledStorageDevice.Count; j++)
					{
						for (int k = 0; k < GameControl.control.Gateway.InstalledStorageDevice[j].Partitions.Count; k++)
						{
							if (!GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].DriveLetter.Contains("" + DriveLetter + ":/"))
							{
								GameControl.control.ProgramFiles.Add(new ProgramSystem("" + DriveLetter + ":/", DriveLabel, "", "", "", "", "Gateway", DriveLetter + ":/", "", "", ProgramSystem.FileExtension.Dir, ProgramSystem.FileExtension.Null, 0, 0, AllocatedSpace, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
								GameControl.control.Gateway.InstalledStorageDevice[Selected].UsedSpace += AllocatedSpace;
								GameControl.control.Gateway.InstalledStorageDevice[Selected].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[Selected].Capacity - GameControl.control.Gateway.InstalledStorageDevice[Selected].UsedSpace;
								DriveLetter = "";
								DriveLabel = "";
								AllocatedSpace = 0;
							}
						}
					}
				}
			}
		}
	}

	void ParitionComplete()
	{

		GameControl.control.ProgramFiles.Add(new ProgramSystem("" + DriveLetter + ":/", DriveLabel, "", "", "", "", "Gateway", DriveLetter + ":/", "", "", ProgramSystem.FileExtension.Dir, ProgramSystem.FileExtension.Null, 0, 0, AllocatedSpace, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
		GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions.Add(new DrivePatSystem(DriveLabel, DriveLetter, AllocatedSpace, 0, AllocatedSpace, Selected));
		GameControl.control.Gateway.InstalledStorageDevice[Selected].UsedSpace += AllocatedSpace;
		GameControl.control.Gateway.InstalledStorageDevice[Selected].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[Selected].Capacity - GameControl.control.Gateway.InstalledStorageDevice[Selected].UsedSpace;
		DriveLetter = "";
		DriveLabel = "";
		AllocatedSpace = 0;
		TestDone = false;
	}

	public void NewParition()
	{
		if (DriveLetter != "")
		{
			if (glyphs.Contains(DriveLetter))
			{
				if (DriveLetter.Length == 1)
				{
					for (int j = 0; j < GameControl.control.Gateway.InstalledStorageDevice.Count; j++)
					{
						for (int k = 0; k < GameControl.control.Gateway.InstalledStorageDevice[j].Partitions.Count; k++)
						{
							if (!GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].DriveLetter.Contains("" + DriveLetter + ":/"))
							{
								TestDone = true;
							}
						}
					}
				}
			}
		}

		if(TestDone == true)
		{
			ParitionComplete();
		}
	}

	public void RemoveDrive()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (GameControl.control.ProgramFiles[i].Location.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[SelectedPartition].DriveLetter))
			{
				TempPrograms.Add(GameControl.control.ProgramFiles[i]);
				//GameControl.control.ProgramFiles.RemoveAt(i);
			}
		}

		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (GameControl.control.ProgramFiles[i].Location.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[SelectedPartition].DriveLetter))
			{
				TempPrograms.Add(GameControl.control.ProgramFiles[i]);
				//GameControl.control.ProgramFiles.RemoveAt(i);
			}
		}

		for (int i = 0; i < TempPrograms.Count; i++)
		{

			if (GameControl.control.ProgramFiles.Contains(TempPrograms[i]))
			{
				GameControl.control.ProgramFiles.Remove(TempPrograms[i]);
			}
		}

		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.Dir)
			{
				if (GameControl.control.ProgramFiles[i].Name.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[SelectedPartition].DriveLetter))
				{
					GameControl.control.ProgramFiles.RemoveAt(i);
				}
			}
		}

		if (com.ComAddress.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[SelectedPartition].DriveLetter))
		{
			com.ComAddress = "Gateway";
		}

		GameControl.control.Gateway.InstalledStorageDevice[Selected].UsedSpace -= GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[SelectedPartition].Size;
		GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions.RemoveAt(SelectedPartition);


		TempPrograms.RemoveRange(0, TempPrograms.Count);
		RemovingFiles = false;
	}

	void Update()
	{
		ResetPartitionUsed();
		FileSizesCheck();
		UpdateDirSize();

		if(RemovingFiles == true)
		{
			RemoveDrive();
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];

		if (show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
		}
	}

	void Minimize()
	{
		if (minimize == true)
		{
			windowRect = (new Rect(windowRect.x, windowRect.y, DefaltSetting.width, 25));
		}
		else
		{
			//private Rect DefaltSetting = new Rect (windowRect.x,windowRect.y,300,205);
			windowRect = (new Rect(windowRect.x, windowRect.y, DefaltSetting.width, DefaltSetting.height));
		}
	}

	void DoMyWindow(int windowID)
	{
		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
			{
				appman.SelectedApp = "Disk Manager";
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
		}

		if (MiniButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				minimize = !minimize;
				Minimize();
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				minimize = !minimize;
				Minimize();
			}
		}

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow(new Rect(2, 2, windowRect.width-48, 21));
		GUI.Box(new Rect(2, 2, windowRect.width-48, 21), "Disk Management");

		Render();
	}

	void ParitionStats()
	{
		if (GUI.Button(new Rect(43, 24, 90, 20), "New Partition"))
		{
			Select = -1;
			SelectedMenu = 2;
		}

		if (Select > -1)
		{

			if (GUI.Button(new Rect(134, 24, 120, 20), "Exetend Partition"))
			{

			}

			if (GUI.Button(new Rect(255, 24, 120, 20), "Remove Partition"))
			{
				SelectedPartition = Select;
				RemovingFiles = true;
			}
		}
		else
		{
			if (GUI.Button(new Rect(134, 24, 120, 20), "Exetend Partition"))
			{

			}
		}

		if (GUI.Button(new Rect(2, 24, 40, 20), "Back"))
		{
			Select = -1;
			SelectedMenu = 0;
		}

		GUI.Button(new Rect(2, 45, 25, 20), "DL");
		GUI.Button(new Rect(2, 45, 25, 20), "ID");
		GUI.Button(new Rect(28, 45, 125, 20), "Name");
		GUI.Button(new Rect(154, 45, 50, 20), "Total");
		GUI.Button(new Rect(205, 45, 50, 20), "Used");
		GUI.Button(new Rect(256, 45, 50, 20), "Free");
		GUI.Button(new Rect(307, 45, 52, 20), "Free %");

		if (GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions.Count > 0)
		{
			scrollpos = GUI.BeginScrollView(new Rect(0, 66, 360, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions.Count; scrollsize++)
			{
				if (GUI.Button(new Rect(2, 21 * scrollsize, 25, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].DriveLetter))
				{
					Select = scrollsize;
				}

				if (GUI.Button(new Rect(28, 21 * scrollsize, 125, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].Label))
				{
					Select = scrollsize;
				}

				if (GUI.Button(new Rect(205, 21 * scrollsize, 50, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].Used))
				{

				}

				if (GUI.Button(new Rect(154, 21 * scrollsize, 50, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].Size))
				{

				}

				if (GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].Size == 0)
				{
					if (GUI.Button(new Rect(256, 21 * scrollsize, 50, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].Size))
					{

					}

					if (GUI.Button(new Rect(307, 21 * scrollsize, 50, 20), "%100"))
					{

					}
				}
				else
				{
					if (GUI.Button(new Rect(256, 21 * scrollsize, 50, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].Free))
					{

					}

					float Percentage = GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].Free / GameControl.control.Gateway.InstalledStorageDevice[Selected].Partitions[scrollsize].Size * 100;

					if (GUI.Button(new Rect(307, 21 * scrollsize, 52, 20), "%" + Percentage.ToString("F2")))
					{

					}
				}
			}
			GUI.EndScrollView();
		}
	}

	void DriveStats()
	{
		GUI.Button(new Rect(2, 45, 20, 20), "ID");
		GUI.Button(new Rect(23, 45, 168, 20), "Name");
		GUI.Button(new Rect(192, 45, 50, 20), "Used");
		GUI.Button(new Rect(243, 45, 50, 20), "Free");
		GUI.Button(new Rect(294, 45, 50, 20), "Total");
		GUI.Button(new Rect(345, 45, 50, 20), "Status");
		GUI.Button(new Rect(396, 45, 52, 20), "Free %");
		if (GameControl.control.Gateway.InstalledStorageDevice.Count > 0)
		{
			scrollpos = GUI.BeginScrollView(new Rect(0, 66, 447, 126), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < GameControl.control.Gateway.InstalledStorageDevice.Count; scrollsize++)
			{

				if (GUI.Button(new Rect(2, 21 * scrollsize, 20, 20), "" + scrollsize))
				{
					Select = -1;
					Selected = scrollsize;
					SelectedMenu = 1;
				}

				if (GUI.Button(new Rect(23, 21 * scrollsize, 168, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[scrollsize].Name))
				{
					Select = -1;
					Selected = scrollsize;
					SelectedMenu = 1;
				}

				if (GUI.Button(new Rect(192, 21 * scrollsize, 50, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[scrollsize].UsedSpace))
				{

				}

				if (GUI.Button(new Rect(294, 21 * scrollsize, 50, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[scrollsize].Capacity))
				{

				}

				if (GameControl.control.Gateway.InstalledStorageDevice[scrollsize].UsedSpace == 0)
				{
					if (GUI.Button(new Rect(243, 21 * scrollsize, 50, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[scrollsize].Capacity))
					{

					}

					if (GUI.Button(new Rect(396, 21 * scrollsize, 52, 20), "%100"))
					{

					}
				}
				else
				{
					if (GUI.Button(new Rect(243, 21 * scrollsize, 50, 20), "" + GameControl.control.Gateway.InstalledStorageDevice[scrollsize].FreeSpace))
					{

					}

					if (GUI.Button(new Rect(396, 21 * scrollsize, 52, 20), "%" + GameControl.control.Gateway.InstalledStorageDevice[scrollsize].FreeSpace / GameControl.control.Gateway.InstalledStorageDevice[scrollsize].Capacity * 100))
					{

					}
				}

				if (GUI.Button(new Rect(345, 21 * scrollsize, 50, 20), "%" + GameControl.control.Gateway.InstalledStorageDevice[scrollsize].CurrentHealth))
				{

				}
			}
			GUI.EndScrollView();
		}
	}

	void NewPartitionMenu()
	{
		if(AllocatedSpaceString != "")
		{
			AllocatedSpace = float.Parse(AllocatedSpaceString);
		}
		else
		{
			AllocatedSpace = 0;
			//AllocatedSpaceString = "0";
		}
		float FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[Selected].FreeSpace;
		float Math = FreeSpace - AllocatedSpace;

		if (GUI.Button(new Rect(2, 24, 40, 20), "Back"))
		{
			Select = -1;
			SelectedMenu = 1;
		}

		if (GUI.Button(new Rect(43, 24, 70, 20), "Add Partition"))
		{
			if (Math >= 0)
			{
				NewParition();
			}
		}

		if (Selected != -1)
		{
			//GUI.Label(new Rect(102, 125, 21, 21), "" + Selected);

			if(Math >= 0)
			{
				GUI.Box(new Rect(250, 25, 100, 21), "" + AllocatedSpace + " / " + FreeSpace);
			}
			else
			{
				GUI.Box(new Rect(250, 25, 100, 21), "" + AllocatedSpace + "! / " + FreeSpace);
			}

			GUI.Box(new Rect(2, 50, 100, 21), "Drive Letter: ");
			DriveLetter = GUI.TextField(new Rect(103, 50, 50, 21), "" + DriveLetter.ToUpper(),1);

			GUI.Box(new Rect(2, 100, 100, 21), "Drive Label: ");
			DriveLabel = GUI.TextField(new Rect(103, 100, 100, 21), "" + DriveLabel);

			GUI.Box(new Rect(2, 150, 100, 21), "Drive Size: ");
			AllocatedSpaceString = GUI.TextField(new Rect(103, 150, 50, 21), "" + AllocatedSpaceString);
			AllocatedSpaceString = Regex.Replace(AllocatedSpaceString, @"[^0-9]", "");
		}
	}

	void Render()
	{
		switch(SelectedMenu)
		{
			case 0:
				DriveStats();
				break;

			case 1:
				ParitionStats();
				break;

			case 2:
				NewPartitionMenu();
				break;
		}
	}
}
