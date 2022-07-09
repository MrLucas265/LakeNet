using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

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
	public long AllocatedSpace;
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

	public bool ShowAllPart;
	public int SelectedDevice;
	public int SelectedStorageSlot;

	public List<Rect> ItemsToRender = new List<Rect>();

	void Start()
	{
		SysSoftware = GameObject.Find("System");
		com = SysSoftware.GetComponent<Computer>();
		defalt = SysSoftware.GetComponent<Defalt>();
		appman = SysSoftware.GetComponent<AppMan>();

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		PosCheck();

		windowRect = new Rect(windowRect.x, windowRect.y, 550, 200);

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

	public void RemoveDrive()
	{
		for (int i = 0; i < PersonController.control.People.Count; i++)
		{
			for (int j = 0; j < PersonController.control.People[i].Gateway.StorageDevices.Count; j++)
			{
				for (int l = 0; l < PersonController.control.People[i].Gateway.StorageDevices[j].OS.Count; l++)
				{
					for (int m = 0; m < PersonController.control.People[i].Gateway.StorageDevices[j].OS[l].Partitions.Count; m++)
					{
						return;
					}
				}
			}
		}
	}

	void PartitionCheck(int PersonID)
    {
		if(PersonController.control.People[PersonID].Gateway.PartitionList.Count > 0)
        {
			for (int i = 0; i < PersonController.control.People[PersonID].Gateway.StorageDevices.Count; i++)
			{
				for (int j = 0; j < PersonController.control.People[PersonID].Gateway.StorageDevices[i].OS.Count; j++)
				{
					if (PersonController.control.People[PersonID].Gateway.StorageDevices[i].OS[j].Name == PersonController.control.People[PersonID].Gateway.CurrentOS.Name)
					{
						for (int k = 0; k < PersonController.control.People[PersonID].Gateway.StorageDevices[i].OS[j].Partitions.Count; k++)
						{
							if (!PersonController.control.People[PersonID].Gateway.PartitionList.Contains(DriveLetter))
							{
								PartitionCompleteV2(PersonID, j-1, new DiskPartSystem(DriveLabel, DriveLetter, AllocatedSpace, 0, AllocatedSpace, Selected),k);
								PartitionScan(PersonID);
							}
						}
					}
				}
			}
		}
		else
        {
			PartitionScan(PersonID);
		}
		//for (int l = 0; l < PersonController.control.People[PersonID].Gateway.StorageDevices[SelectedDevice].OS.Count; l++)
		//{
		//	PartitionCompleteV2(PersonID, l, new DiskPartSystem(DriveLabel, DriveLetter, AllocatedSpace, 0, AllocatedSpace, Selected));
		//}
	}

	void PartitionScan(int PersonID)
    {
		for (int i = 0; i < PersonController.control.People[PersonID].Gateway.StorageDevices.Count; i++)
		{
			for (int j = 0; j < PersonController.control.People[PersonID].Gateway.StorageDevices[i].OS.Count; j++)
			{
				if (PersonController.control.People[PersonID].Gateway.StorageDevices[i].OS[j].Name == PersonController.control.People[PersonID].Gateway.CurrentOS.Name)
				{
					for (int k = 0; k < PersonController.control.People[PersonID].Gateway.StorageDevices[i].OS[j].Partitions.Count; k++)
					{
						if(!PersonController.control.People[PersonID].Gateway.PartitionList.Contains(PersonController.control.People[PersonID].Gateway.StorageDevices[i].OS[j].Partitions[k].DriveLetter))
                        {
							PersonController.control.People[PersonID].Gateway.PartitionList.Add(PersonController.control.People[PersonID].Gateway.StorageDevices[i].OS[j].Partitions[k].DriveLetter);
						}
					}
				}
			}
		}
	}

	void PartitionCompleteV2(int PersonID,int SelectedOS,DiskPartSystem DiskInfo, int k)
    {
		PersonController.control.People[PersonID].Gateway.StorageDevices[SelectedDevice].OS[SelectedOS].Partitions.Add(DiskInfo);


		if (PersonController.control.People[PersonID].Gateway.StorageDevices[SelectedDevice].OS[SelectedOS].Partitions[k].Files.Count == 0)
		{
			PersonController.control.People[PersonID].Gateway.StorageDevices[SelectedDevice].OS[SelectedOS].Partitions[k].Files.Add(new ProgramSystemv2(DriveLetter+":/", "System", "", "", "", DriveLabel, "Gateway", DriveLetter+":/", "", "", ProgramSystemv2.FileExtension.Dir, ProgramSystemv2.FileExtension.Null, 0, 0, 60, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, false, false, false));
		}

		DriveLetter = "";
		DriveLabel = "";
		AllocatedSpaceString = "";
	}

	public void NewParition(int personID)
	{
		if (DriveLetter != "")
		{
			if (glyphs.Contains(DriveLetter))
			{
				if (DriveLetter.Length == 1)
				{
					if(!PersonController.control.People[personID].Gateway.StorageDevices[SelectedDevice].InstalledOS.Contains(PersonController.control.People[personID].Gateway.CurrentOS.Title))
                    {
						PersonController.control.People[personID].Gateway.StorageDevices[SelectedDevice].OS.Add(new OperatingSystems(PersonController.control.People[personID].Gateway.CurrentOS.Title, PersonController.control.People[personID].Gateway.CurrentOS.Name, new OSOptionsSystem(true)));
						PersonController.control.People[personID].Gateway.StorageDevices[SelectedDevice].InstalledOS.Add(PersonController.control.People[personID].Gateway.CurrentOS.Title);
					}
					PartitionCheck(personID);
				}
			}
		}
		if (DriveLetter == "add new os")
		{
			if (!PersonController.control.People[personID].Gateway.StorageDevices[SelectedDevice].InstalledOS.Contains(PersonController.control.People[personID].Gateway.CurrentOS.Title))
			{
				PersonController.control.People[personID].Gateway.StorageDevices[SelectedDevice].OS.Add(new OperatingSystems(PersonController.control.People[personID].Gateway.CurrentOS.Title, PersonController.control.People[personID].Gateway.CurrentOS.Name, new OSOptionsSystem(true)));
				PersonController.control.People[personID].Gateway.StorageDevices[SelectedDevice].InstalledOS.Add(PersonController.control.People[personID].Gateway.CurrentOS.Title);
			}
			PartitionCheck(personID);
			DriveLetter = "";
		}
	}

	void Update()
	{
		//ResetPartitionUsed();
		//FileSizesCheck();
		//UpdateDirSize();
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

		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

		for (int l = 0; l < person.Gateway.StorageDevices[SelectedDevice].OS.Count; l++)
		{
			if (ShowAllPart == false)
			{
				if(person.Gateway.StorageDevices[SelectedDevice].OS[l].Name == person.Gateway.CurrentOS.Name)
				{
					if (person.Gateway.StorageDevices[SelectedDevice].OS[l].Partitions.Count > 0)
					{
						scrollpos = GUI.BeginScrollView(new Rect(0, 66, 360, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
						for (scrollsize = 0; scrollsize < person.Gateway.StorageDevices[SelectedDevice].OS[l].Partitions.Count; scrollsize++)
						{
							var DisplayedDisk = person.Gateway.StorageDevices[SelectedDevice].OS[l].Partitions;

							if (GUI.Button(new Rect(2, 21 * scrollsize, 25, 20), "" + DisplayedDisk[scrollsize].DriveLetter))
							{
								Select = scrollsize;
							}

							if (GUI.Button(new Rect(28, 21 * scrollsize, 125, 20), "" + DisplayedDisk[scrollsize].Label))
							{
								Select = scrollsize;
							}

							if (GUI.Button(new Rect(205, 21 * scrollsize, 50, 20), "" + NumberFormat.Data(DisplayedDisk[scrollsize].Used)))
							{

							}

							if (GUI.Button(new Rect(154, 21 * scrollsize, 50, 20), "" + NumberFormat.Data(DisplayedDisk[scrollsize].Size)))
							{

							}

							if (DisplayedDisk[scrollsize].Size == 0)
							{
								if (GUI.Button(new Rect(256, 21 * scrollsize, 50, 20), "" + NumberFormat.Data(DisplayedDisk[scrollsize].Size)))
								{

								}

								if (GUI.Button(new Rect(307, 21 * scrollsize, 50, 20), "%100"))
								{

								}
							}
							else
							{
								if (GUI.Button(new Rect(256, 21 * scrollsize, 50, 20), "" + NumberFormat.Data(DisplayedDisk[scrollsize].Free)))
								{

								}

								float Percentage = DisplayedDisk[scrollsize].Free / DisplayedDisk[scrollsize].Size * 100;

								if (GUI.Button(new Rect(307, 21 * scrollsize, 52, 20), "%" + Percentage.ToString("F2")))
								{

								}
							}
						}
						GUI.EndScrollView();
					}
				}
			}
			else
			{
				if (person.Gateway.StorageDevices[SelectedDevice].OS[l].Partitions.Count > 0)
				{
					scrollpos = GUI.BeginScrollView(new Rect(0, 66, 360, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
					for (scrollsize = 0; scrollsize < person.Gateway.StorageDevices[SelectedDevice].OS[l].Partitions.Count; scrollsize++)
					{
						var DisplayedDisk = person.Gateway.StorageDevices[SelectedDevice].OS[l].Partitions;

						if (GUI.Button(new Rect(2, 21 * scrollsize, 25, 20), "" + DisplayedDisk[scrollsize].DriveLetter))
						{
							Select = scrollsize;
						}

						if (GUI.Button(new Rect(28, 21 * scrollsize, 125, 20), "" + DisplayedDisk[scrollsize].Label))
						{
							Select = scrollsize;
						}

						if (GUI.Button(new Rect(205, 21 * scrollsize, 50, 20), " " + NumberFormat.Data(DisplayedDisk[scrollsize].Used)))
						{
									
						}

						if (GUI.Button(new Rect(154, 21 * scrollsize, 50, 20), "" + NumberFormat.Data(DisplayedDisk[scrollsize].Size)))
						{

						}

						if (DisplayedDisk[scrollsize].Size == 0)
						{
							if (GUI.Button(new Rect(256, 21 * scrollsize, 50, 20), "" + NumberFormat.Data(DisplayedDisk[scrollsize].Size)))
							{

							}

							if (GUI.Button(new Rect(307, 21 * scrollsize, 50, 20), "%100"))
							{

							}
						}
						else
						{
							if (GUI.Button(new Rect(256, 21 * scrollsize, 50, 20), "" + NumberFormat.Data(DisplayedDisk[scrollsize].Free)))
							{

							}

							float Percentage = DisplayedDisk[scrollsize].Free / DisplayedDisk[scrollsize].Size * 100;

							if (GUI.Button(new Rect(307, 21 * scrollsize, 52, 20), "%" + Percentage.ToString("F2")))
							{

							}
						}
					}
					GUI.EndScrollView();
				}
			}
		}
	}

	void DriveStats()
	{
		if(ItemsToRender.Count > 0)
        {
			ItemsToRender.RemoveRange(0, ItemsToRender.Count);
		}

		ItemsToRender.Add(new Rect(windowRect.width-54-2, 24, 54, 20));
		ItemsToRender.Add(new Rect(ItemsToRender[0].x - 60 - 1, 24, 60, 20));
		ItemsToRender.Add(new Rect(ItemsToRender[1].x - ItemsToRender[1].width - 1, 24, 60, 20));
		ItemsToRender.Add(new Rect(ItemsToRender[2].x - ItemsToRender[2].width - 1, 24, 60, 20));
		ItemsToRender.Add(new Rect(ItemsToRender[3].x - ItemsToRender[3].width - 1, 24, 60, 20));
		ItemsToRender.Add(new Rect(2, 24, 20, 20));
		ItemsToRender.Add(new Rect(ItemsToRender[5].x + ItemsToRender[5].width + 1, 24, ItemsToRender[4].x-1- ItemsToRender[5].x - ItemsToRender[5].width - 1, 20));

		GUI.Button(ItemsToRender[5], "ID");
		GUI.Button(ItemsToRender[6], "Name");
		GUI.Button(ItemsToRender[4], "Status");
		GUI.Button(ItemsToRender[3], "Total");
		GUI.Button(ItemsToRender[2], "Used");
		GUI.Button(ItemsToRender[1], "Free");
		GUI.Button(ItemsToRender[0], "Free %");

		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

		if(person.Gateway.StorageDevices.Count > 0)
		{
			scrollpos = GUI.BeginScrollView(new Rect(0, ItemsToRender[0].y+ ItemsToRender[0].height+ 1, windowRect.width, windowRect.height), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < person.Gateway.StorageDevices.Count; scrollsize++)
			{

				if (GUI.Button(new Rect(ItemsToRender[5].x, 21 * scrollsize, ItemsToRender[5].width, 20), "" + scrollsize))
				{
					Select = -1;
					SelectedDevice = scrollsize;
					SelectedMenu = 1;
				}

				if (GUI.Button(new Rect(ItemsToRender[6].x, 21 * scrollsize, ItemsToRender[6].width, 20), "" + person.Gateway.StorageDevices[scrollsize].Name))
				{
					Select = -1;
					SelectedDevice = scrollsize;
					SelectedMenu = 1;
				}

				if (GUI.Button(new Rect(ItemsToRender[2].x, 21 * scrollsize, ItemsToRender[1].width, 20), "" + NumberFormat.Data(person.Gateway.StorageDevices[scrollsize].UsedSpace)))
				{

				}

                if (GUI.Button(new Rect(ItemsToRender[3].x, 21 * scrollsize, ItemsToRender[3].width, 20), "" + NumberFormat.Data(person.Gateway.StorageDevices[scrollsize].Capacity)))
                {

                }

                if (person.Gateway.StorageDevices[scrollsize].UsedSpace == 0)
				{
					if (GUI.Button(new Rect(ItemsToRender[1].x, 21 * scrollsize, ItemsToRender[2].width, 20), "" + NumberFormat.Data(person.Gateway.StorageDevices[scrollsize].Capacity)))
					{

					}

					if (GUI.Button(new Rect(ItemsToRender[0].x, 21 * scrollsize, ItemsToRender[0].width, 20), "%100"))
					{

					}
				}
				else
				{
					if (GUI.Button(new Rect(ItemsToRender[1].x, 21 * scrollsize, ItemsToRender[2].width, 20), "" + NumberFormat.Data(person.Gateway.StorageDevices[scrollsize].FreeSpace)))
					{

					}

					if (GUI.Button(new Rect(ItemsToRender[0].x, 21 * scrollsize, ItemsToRender[0].width, 20), "%" + person.Gateway.StorageDevices[scrollsize].FreeSpacePercentage.ToString("F2")))
					{

					}
				}

				if (GUI.Button(new Rect(ItemsToRender[4].x, 21 * scrollsize, ItemsToRender[4].width, 20), "%" + person.Gateway.StorageDevices[scrollsize].HealthPercentage.ToString("F2")))
				{

				}
			}
			GUI.EndScrollView();
		}
	}

	void AddVolume()
    {
		DriveLetter = "add new os";
		if (GUI.Button(new Rect(43, 24, 70, 20), "Add Volume"))
		{
			for (int i = 0; i < PersonController.control.People.Count; i++)
			{
				if (PersonController.control.People[i].Name == "Player")
				{
					NewParition(i);
					//print(i);
				}
			}
		}
	}

	void NewPartitionMenu()
	{
		double FreeSpace = 0;

		if (AllocatedSpaceString != "")
		{
			AllocatedSpace = long.Parse(AllocatedSpaceString);
		}
		else
		{
			AllocatedSpace = 0;
			//AllocatedSpaceString = "0";
		}
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");
		FreeSpace = person.Gateway.StorageDevices[SelectedDevice].FreeSpace;

		double Math = FreeSpace - AllocatedSpace;

		if (GUI.Button(new Rect(2, 24, 40, 20), "Back"))
		{
			Select = -1;
			SelectedMenu = 1;
		}

		if(person.Gateway.StorageDevices[SelectedDevice].InstalledOS.Count > 0)
        {
			if(person.Gateway.StorageDevices[SelectedDevice].InstalledOS.Contains(person.Gateway.CurrentOS.Title))
            {
				if (GUI.Button(new Rect(43, 24, 70, 20), "Add Partition"))
				{
					if (Math >= 0)
					{
						for (int i = 0; i < PersonController.control.People.Count; i++)
						{
							if (PersonController.control.People[i].Name == "Player")
							{
								NewParition(i);
								//print(i);
							}
						}
					}
				}

				if (Selected != -1)
				{
					//GUI.Label(new Rect(102, 125, 21, 21), "" + Selected);

					if (Math >= 0)
					{
						GUI.Box(new Rect(250, 25, 100, 21), "" + AllocatedSpace + " / " + NumberFormat.Data(FreeSpace));
					}
					else
					{
						GUI.Box(new Rect(250, 25, 100, 21), "" + AllocatedSpace + "! / " + NumberFormat.Data(FreeSpace));
					}

					GUI.Box(new Rect(2, 50, 100, 21), "Drive Letter: ");
					DriveLetter = GUI.TextField(new Rect(103, 50, 50, 21), "" + DriveLetter.ToUpper(), 1);

					GUI.Box(new Rect(2, 100, 100, 21), "Drive Label: ");
					DriveLabel = GUI.TextField(new Rect(103, 100, 100, 21), "" + DriveLabel);

					GUI.Box(new Rect(2, 150, 100, 21), "Drive Size: ");
					AllocatedSpaceString = GUI.TextField(new Rect(103, 150, 50, 21), "" + AllocatedSpaceString);
					AllocatedSpaceString = Regex.Replace(AllocatedSpaceString, @"[^0-9]", "");
				}
			}
			else
            {
				AddVolume();
			}
        }
		else
		{
			AddVolume();
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
