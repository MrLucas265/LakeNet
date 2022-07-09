using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileMangementUIBackup : MonoBehaviour
{
	public bool quit;

	private GameObject Puter;

	private GameObject WindowHandel;
	private WindowManager winman;

	private Computer com;
	private SoundControl sc;
	private FileExplorer fp;
	private AppMan appman;

	public float native_width = 1920;
	public float native_height = 1080;

	public string ProgramNameForWinMan;

	public int SelectedWindowID;
	public int SelectedProgramID;

	private Rect CloseButton;
	public Rect CurrentTimeRect;
	public Rect CurrentDateRect;

	public bool ShowSettings;

	// Vars for context menu
	public List<string> ContextMenuOptions = new List<string>();
	public string SelectedOption;
	public string ContextMenuName;

	public List<FileMangementSystem> FMS = new List<FileMangementSystem>();

	public string PersonName;
	public string ProgramName;

	public float LastClick;

	// Use this for initialization
	void Start()
	{
		ProgramNameForWinMan = "File Manager";
		ContextMenuName = "File Manager Context Menu";

		Puter = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");
		com = Puter.GetComponent<Computer>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		fp = Puter.GetComponent<FileExplorer>();
		appman = Puter.GetComponent<AppMan>();

		winman = WindowHandel.GetComponent<WindowManager>();

		ProgramName = "FileManager";
		PersonName = "Player";
	}

	// Update is called once per frame
	void Update()
	{

	}

	void SelectWindowID(int WindowID)
	{
		if (Input.GetMouseButtonDown(0))
		{
			SelectedWindowID = WindowID;
			winman.SelectedWID = WindowID;
		}
	}

	bool GUIKeyDown(KeyCode key)
	{
		if (Event.current.type == EventType.KeyDown)
			return (Event.current.keyCode == key);
		return false;

	}

	void Close(int ID)
	{
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						if (pwinman.RunningPrograms[i].WID == ID)
						{
							FMS.RemoveAt(pwinman.RunningPrograms[i].PID);
							SelectedProgramID = 0;
							SelectedWindowID = 0;
							quit = true;
							appman.SelectedApp = "FileManager";
							if (FMS.Count == 0)
							{
								quit = false;
							}
							pwinman.RunningPrograms.RemoveAt(i);
							SetID();
						}
					}
				}
			}

		}
	}

	void CloseContextMenu()
	{
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ContextMenuName)
					{
						ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
						pwinman.RunningPrograms.RemoveAt(i);
						SelectedOption = "";
					}
				}
			}
		}
	}

	void GUIControls()
	{
		if (GUI.GetNameOfFocusedControl().Equals("Address"))
		{
			if (GUIKeyDown(KeyCode.DownArrow))
			{
				//DisplayedItems = PageFile.Count / 21;
			}

			if (GUIKeyDown(KeyCode.UpArrow))
			{

			}


			if (GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.KeypadEnter))
			{
				LocalRegistry.SetStringData(PersonName, 1, ProgramName, "CurrentDirectory", FMS[1].TypedDirectory);
			}
		}
		else
		{
			if (GUIKeyDown(KeyCode.LeftArrow) || GUIKeyDown(KeyCode.Backspace))
			{
				HistorySystem("Back");
			}

			if (GUIKeyDown(KeyCode.DownArrow))
			{
				if (FMS[SelectedProgramID].PageFile.Count > 0)
				{
					if (FMS[SelectedProgramID].SelectedFile < FMS[SelectedProgramID].PageFile.Count - 1)
					{
						if (FMS[SelectedProgramID].SelectedFile >= 0)
						{
							Registry.GetRectData(PersonName, ProgramName, "MainScrollPos").y += 21;
							FMS[SelectedProgramID].SelectedFile++;
						}
					}
					if (FMS[SelectedProgramID].SelectedFile < 0)
					{
						FMS[SelectedProgramID].SelectedFile++;
					}
				}
				//DisplayedItems = PageFile.Count / 21;
			}

			if (GUIKeyDown(KeyCode.UpArrow))
			{
				if (FMS[SelectedProgramID].PageFile.Count > 0)
				{
					if (FMS[SelectedProgramID].SelectedFile < 0)
					{
						FMS[SelectedProgramID].SelectedFile = FMS[SelectedProgramID].PageFile.Count;
						Registry.GetRectData(PersonName, ProgramName, "MainScrollPos").y = 21 * FMS[SelectedProgramID].SelectedFile;
					}
					if (FMS[SelectedProgramID].SelectedFile >= 1)
					{
						Registry.GetRectData(PersonName, ProgramName, "MainScrollPos").y -= 21;
						FMS[SelectedProgramID].SelectedFile--;
					}
				}
			}

			if (GUIKeyDown(KeyCode.Delete))
			{
				if (FMS[SelectedProgramID].SelectedFile >= 0)
				{
					//DeleteSystem();
				}
			}
		}

		if (GUIKeyDown(KeyCode.Tab))
		{
			if (GUI.GetNameOfFocusedControl().Equals("Address"))
			{
				GUI.FocusControl("None");
			}
			if (GUI.GetNameOfFocusedControl().Equals("None"))
			{
				if (Registry.GetIntData(PersonName, ProgramName, "SelectedMenu") <= 1)
				{
					GUI.FocusControl("Address");
				}
				if (Registry.GetIntData(PersonName, ProgramName, "SelectedMenu") == 2)
				{
					GUI.FocusControl("FolderName");
				}
				if (Registry.GetIntData(PersonName, ProgramName, "SelectedMenu") == 3)
				{
					GUI.FocusControl("RenameFile");
				}
			}
		}
	}

	void OnGUI()
	{
		GUI.skin = com.Skin[GameControl.control.GUIID];

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						GUI.color = com.colors[Customize.cust.WindowColorInt];
						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));

						if (!pwinman.RunningPrograms[i].windowRect.Contains(Event.current.mousePosition))
						{
							if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
							{
								CloseContextMenu();
							}
						}
					}
					if (pwinman.RunningPrograms[i].ProgramName == ContextMenuName)
					{
						pwinman.RunningPrograms[i].windowRect.height = 21 * ContextMenuOptions.Count + 2;
						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyContextWindow, ""));


						if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
						{
							if (!pwinman.RunningPrograms[i].windowRect.Contains(Event.current.mousePosition))
							{
								CloseContextMenu();
							}
						}
					}
				}
			}
		}
	}

	void SetID()
	{
		int count = 0;
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			for (int j = 0; j < pwinman.RunningPrograms.Count; j++)
			{
				if (pwinman.RunningPrograms[j].ProgramName == ProgramNameForWinMan)
				{
					count++; // 3
					pwinman.RunningPrograms[j].PID = count - 1;
				}
			}

		}
	}

	void TitleBarStuff(int PID)
	{
		GUI.DragWindow(new Rect(2, 2, CloseButton.x - 41, 20));
		//winman.WindowDragging(SelectedWindowID, new Rect(2, 2, CloseButton.x - 41, 21));
		GUI.Box(new Rect(2, 2, CloseButton.x - 41, 20), ProgramNameForWinMan);

		if (FMS[PID].History.Count <= 0)
		{
			LocalRegistry.SetStringData(PersonName, 1, ProgramName, "TypedDirectory", GUI.TextField(new Rect(2, CloseButton.y + CloseButton.height, 200, 20), LocalRegistry.GetStringData(PersonName, 1, ProgramName, "TypedDirectory"), 100));

			if (LocalRegistry.GetStringData(PersonName, 1, ProgramName, "CurrentDirectory") == "")
			{
				LocalRegistry.SetStringData(PersonName, 1, ProgramName, "TypedDirectory", "Gateway");
				LocalRegistry.SetStringData(PersonName, 1, ProgramName, "CurrentDirectory", "Gateway");
			}

			if (GUI.Button(new Rect(250, CloseButton.y + CloseButton.height, 20, 20), "⏎"))
			{
				LocalRegistry.SetStringData(PersonName, 1, ProgramName, "CurrentDirectory", FMS[PID].TypedDirectory);
				HistorySystem("Add");
			}
		}
		else
		{
			LocalRegistry.SetStringData(PersonName, 1, ProgramName, "TypedDirectory", GUI.TextField(new Rect(2, CloseButton.y + CloseButton.height, 200, 20), LocalRegistry.GetStringData(PersonName, 1, ProgramName, "TypedDirectory"), 100));

			if (GUI.Button(new Rect(2, CloseButton.y + CloseButton.height, 20, 20), "<-"))
			{
				HistorySystem("Back");
				PlayClickSound();
			}

			if (GUI.Button(new Rect(250, CloseButton.y + CloseButton.height, 20, 20), "⏎"))
			{
				LocalRegistry.SetStringData(PersonName, 1, ProgramName, "CurrentDirectory", LocalRegistry.GetStringData(PersonName, 1, ProgramName, "TypedDirectory"));
				HistorySystem("Add");
			}
		}
	}

	void Refresh()
	{
		if (FMS[SelectedProgramID].CurrentDirectory == "")
		{
			FMS[SelectedProgramID].CurrentDirectory = "Gateway";
		}

		FMS[SelectedProgramID].PageFile.RemoveRange(0, FMS[SelectedProgramID].PageFile.Count);

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount];

			if (pwinman.Name == "Player")
			{
				for (int i = 0; i < pwinman.Gateway.StorageDevices.Count; i++)
				{
					for (int j = 0; j < pwinman.Gateway.StorageDevices[i].OS.Count; j++)
					{
						for (int k = 0; k < pwinman.Gateway.StorageDevices[i].OS[j].Partitions.Count; k++)
						{
							for (int Index = 0; Index < pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files.Count; Index++)
							{
								if (pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files[Index].Location == FMS[SelectedProgramID].CurrentDirectory)
								{
									if (!FMS[SelectedProgramID].PageFile.Contains(pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files[Index]))
									{
										//print(pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files[Index].Name);
										FMS[SelectedProgramID].PageFile.Add(pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files[Index]);
									}
								}
							}
						}
					}
				}
			}
		}
	}

	void DoMyWindow(int WindowID)
	{
		SelectWindowID(WindowID);

		GUIControls();

		Refresh();

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				//winman.WindowResize(SelectedWindowID);

				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						if (pwinman.RunningPrograms[i].WID == SelectedWindowID)
						{
							SelectedProgramID = pwinman.RunningPrograms[i].PID;
						}


						if (WindowID == pwinman.RunningPrograms[i].WID)
						{
							CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 23, 2, 21, 21);
							if (CloseButton.Contains(Event.current.mousePosition))
							{
								if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
								{
									Close(SelectedWindowID);
								}

								GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
								GUI.contentColor = com.colors[Customize.cust.FontColorInt];
							}
							else
							{
								GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
								GUI.contentColor = com.colors[Customize.cust.FontColorInt];

								if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]))
								{
									Close(SelectedWindowID);
								}
							}

							TitleBarStuff(pwinman.RunningPrograms[i].PID);

							RenderFileUI(pwinman.RunningPrograms[i].PID);

							//LocalRegistry.SetStringData("Player", 1, "FileManager", "CurrentDirectory", "Test");//THIS WORKS ON A EPIC LEVEL
						}
					}
				}
			}
		}
	}

	void PlayClickSound()
	{
		sc.SoundSelect = 3;
		sc.PlaySound();
	}

	void HistorySystem(string Action)
	{
		if (Action == "Add")
		{

			if (!FMS[SelectedProgramID].History.Contains("Gateway"))
			{
				FMS[SelectedProgramID].History.Insert(0, "Gateway");
			}

			FMS[SelectedProgramID].History.Add(FMS[SelectedProgramID].CurrentDirectory);
			FMS[SelectedProgramID].HistoryPosition = FMS[SelectedProgramID].History.Count - 1;
		}
		if (Action == "Back")
		{
			if (FMS[SelectedProgramID].HistoryPosition <= 0)
			{
				FMS[SelectedProgramID].HistoryPosition = 0;
				FMS[SelectedProgramID].CurrentDirectory = FMS[SelectedProgramID].History[FMS[SelectedProgramID].HistoryPosition];
				FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
			}
			else
			{
				FMS[SelectedProgramID].HistoryPosition--;
				FMS[SelectedProgramID].CurrentDirectory = FMS[SelectedProgramID].History[FMS[SelectedProgramID].HistoryPosition];
				FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
			}
		}

		if (Action == "Foward")
		{
			if (FMS[SelectedProgramID].HistoryPosition >= FMS[SelectedProgramID].History.Count)
			{
				FMS[SelectedProgramID].HistoryPosition = FMS[SelectedProgramID].History.Count - 1;
				FMS[SelectedProgramID].CurrentDirectory = FMS[SelectedProgramID].History[FMS[SelectedProgramID].HistoryPosition];
				FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
			}
			else
			{
				FMS[SelectedProgramID].HistoryPosition++;
				FMS[SelectedProgramID].CurrentDirectory = FMS[SelectedProgramID].History[FMS[SelectedProgramID].HistoryPosition];
				FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
			}
		}
	}

	void OpenFld(int PID)
	{
		PlayClickSound();
		FMS[PID].CurrentDirectory = FMS[PID].PageFile[FMS[PID].SelectedFile].Target;
		FMS[PID].PageFile.RemoveRange(0, FMS[PID].PageFile.Count);
		FMS[PID].SelectedFile = -1;
		FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
		HistorySystem("Add");
		//MenuSelected = 0;
	}

	void RenderFileUI(int ProgramID)
	{
		if (FMS.Count > 0)
		{
			for (int j = 0; j < FMS.Count; j++)
			{
				if (j == ProgramID)
				{
					Registry.SetVector2Data(PersonName, ProgramName, "MainScrollPos", GUI.BeginScrollView(new Rect(3, 85, 290, 106), Registry.GetVector2Data(PersonName, ProgramName, "MainScrollPos"), new Rect(0, 0, 0, Registry.GetIntData(PersonName, ProgramName, "MainScrollSize") * 21)));

					for (int m = 0; m < FMS[j].PageFile.Count; m++)
					{
						if (FMS[j].PageFile[m].Location == FMS[j].CurrentDirectory)
						{
							switch (FMS[j].PageFile[m].Extension)
							{
								case ProgramSystemv2.FileExtension.Dir:
									if (FMS[j].SelectedFile == m)
									{
										GUI.DrawTexture(new Rect(0, 21 * m, 20, 20), com.IconHighlight[0]);
									}
									else
									{
										GUI.DrawTexture(new Rect(0, 21 * m, 20, 20), com.Icon[0]);
									}

									//if (dm.UsedSpace [scrollsize] == 0 && dm.FreeSpace [scrollsize] == 0)
									//{
									//	dm.FreeSpace [scrollsize] = dm.DriveCapacity[scrollsize];
									//}

									if (GUI.Button(new Rect(21, 21 * m, 250, 20), "" + FMS[j].PageFile[m].Name + " " + "(" + FMS[j].PageFile[m].Sender + ")" + " " + FMS[j].PageFile[m].Free.ToString("F2") + " free of " + FMS[j].PageFile[m].Capacity) || GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.RightArrow))
									{
										if (GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.RightArrow))
										{
											PlayClickSound();
											FMS[j].SelectedFile = m;
											OpenFld(j);
										}
										if (Input.GetMouseButtonUp(0))
										{
											if (Time.time - Registry.GetFloatData(PersonName, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, ProgramName, "LastClickThreshold"))
											{
												PlayClickSound();
												FMS[j].SelectedFile = m;
												OpenFld(j);
											}
											else
											{
												PlayClickSound();
												FMS[j].SelectedFile = m;
											}
											Registry.SetFloatData(PersonName, ProgramName, "LastClick", Time.time);
										}
										if (Input.GetMouseButtonUp(1))
										{
											FMS[j].SelectedFile = m;
											if (new Rect(21, 21 * FMS[j].SelectedFile, 200, 20).Contains(Event.current.mousePosition))
											{
												//ContextPos();
											}
										}
									}
									break;
								case ProgramSystemv2.FileExtension.Fdl:

									if (GUI.Button(new Rect(21, 21 * m, 100, 20), "" + FMS[j].PageFile[m].Name))
									{
										if (Input.GetMouseButtonUp(0))
										{
											if (Time.time - Registry.GetFloatData(PersonName, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, ProgramName, "LastClickThreshold"))
											{
												PlayClickSound();
												FMS[j].SelectedFile = m;
												OpenFld(j);
											}
											else
											{
												PlayClickSound();
												FMS[j].SelectedFile = m;
											}
											Registry.SetFloatData(PersonName, ProgramName, "LastClick", Time.time);
										}
										if (Input.GetMouseButtonUp(1))
										{
											FMS[j].SelectedFile = m;
											if (new Rect(21, 21 * j, 100, 20).Contains(Event.current.mousePosition))
											{
												//ContextPos();
											}
										}
									}

									if (FMS[j].SelectedFile == m)
									{
										GUI.DrawTexture(new Rect(0, 21 * m, 20, 20), com.IconHighlight[1]);
									}
									else
									{
										GUI.DrawTexture(new Rect(0, 21 * m, 20, 20), com.Icon[1]);
									}

									break;
							}
						}
					}
					GUI.EndScrollView();
				}
			}
		}
	}

	void CreateContextWindow(float x, float y)
	{
		winman.ProgramName = ContextMenuName;
		winman.windowRect = new Rect(x, y, 100 * Customize.cust.UIScale, 300 * Customize.cust.UIScale);
		winman.AddProgramWindow();
	}

	void AddContextOptions()
	{
		if (ShowSettings == true)
		{
			ContextMenuOptions.Add("Display Time");
		}
		else
		{
			ContextMenuOptions.Add("Settings");
		}
	}

	void DoMyContextWindow(int WindowID)
	{
		SelectWindowID(WindowID);
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
				if (GUI.Button(new Rect(1, 1 + 21 * i, 100 - 2, 21), ContextMenuOptions[i]))
				{
					SelectedOption = ContextMenuOptions[i];
				}
			}
		}

		switch (SelectedOption)
		{
			case "Display Time":
				ShowSettings = false;
				CloseContextMenu();
				break;
			case "Settings":
				ShowSettings = true;
				CloseContextMenu();
				break;
		}
	}
}
