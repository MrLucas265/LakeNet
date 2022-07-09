using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileMangementUI : MonoBehaviour
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
	public int SelectedWPN;

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

		//LocalRegistry.AddNewKey(PersonName, 1, "Test");
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

	void SelectWPN(int WPN)
	{
		if (Input.GetMouseButtonDown(0))
		{
			SelectedWindowID = WPN;
		}
	}

	bool GUIKeyDown(KeyCode key)
	{
		if (Event.current.type == EventType.KeyDown)
			return (Event.current.keyCode == key);
		return false;

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
				LocalRegistry.SetStringData(PersonName,1, ProgramName, "CurrentDirectory", FMS[1].TypedDirectory);
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
				//LocalRegistry.SetIntData(PersonName,1, ProgramName,"Health",100);
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
						GUI.color = new Color32(Registry.GetRedColorData(PersonName, ProgramName, "WindowColor"), Registry.GetGreenColorData(PersonName, ProgramName, "WindowColor"), Registry.GetBlueColorData(PersonName, ProgramName, "WindowColor"), Registry.GetAlphaColorData(PersonName, ProgramName, "WindowColor"));
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

	void TitleBarStuff(int PID)
	{
		GUI.DragWindow(new Rect(2, 2, CloseButton.x - 41, 20));
		winman.WindowDragging(SelectedWindowID, new Rect(2, 2, CloseButton.x - 41, 21));
		GUI.Box(new Rect(2, 2, CloseButton.x - 41, 20), ProgramNameForWinMan);

		if (LocalRegistry.GetStringDataCount(PersonName, PID, ProgramName, "PageHistory") <= 0)
		{
			LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory", GUI.TextField(new Rect(2, CloseButton.y + CloseButton.height, 200, 20), LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedDirectory"), 100));

			if (LocalRegistry.GetStringData(PersonName, PID, ProgramName, "CurrentDirectory") == "")
			{
				LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory", "Gateway");
				LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", "Gateway");
			}

			if (GUI.Button(new Rect(250, CloseButton.y + CloseButton.height, 20, 20), "⏎"))
			{
				LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedDirectory"));
				HistorySystem("Add");
			}
		}
		else
		{
			LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory", GUI.TextField(new Rect(2, CloseButton.y + CloseButton.height, 200, 20), LocalRegistry.GetStringData(PersonName, 1, ProgramName, "TypedDirectory"), 100));

			if (GUI.Button(new Rect(2, CloseButton.y + CloseButton.height, 20, 20), "<-"))
			{
				HistorySystem("Back");
				PlayClickSound();
			}

			if (GUI.Button(new Rect(250, CloseButton.y + CloseButton.height, 20, 20), "⏎"))
			{
				LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedDirectory"));
				HistorySystem("Add");
			}
		}
	}

	void Refresh(int WindowID)
	{
		if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramName, "CurrentDirectory") == "")
		{
			LocalRegistry.SetStringData(PersonName, WindowID, ProgramName, "CurrentDirectory", "Gateway");
		}

		LocalRegistry.RemoveAllProgramData(PersonName, WindowID, ProgramName, "Test 1");

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount];

			if (pwinman.Name == PersonName)
			{
				for (int i = 0; i < pwinman.Gateway.StorageDevices.Count; i++)
				{
					for (int j = 0; j < pwinman.Gateway.StorageDevices[i].OS.Count; j++)
					{
						if(pwinman.Gateway.StorageDevices[i].OS[j].Name == pwinman.Gateway.CurrentOS.Name)
                        {
							for (int k = 0; k < pwinman.Gateway.StorageDevices[i].OS[j].Partitions.Count; k++)
							{
								for (int Index = 0; Index < pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files.Count; Index++)
								{
									if (pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files[Index].Location == LocalRegistry.GetStringData(PersonName, WindowID, ProgramName,"CurrentDirectory"))
									{
										if (!LocalRegistry.ProgramDataContains(PersonName, WindowID, ProgramName, "Test 1",pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files[Index]))
										{
											LocalRegistry.AddProgramData(PersonName, WindowID, ProgramName, "Test 1", pwinman.Gateway.StorageDevices[i].OS[j].Partitions[k].Files[Index]);
										}
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

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				winman.WindowResize(PersonName, SelectedWindowID);

				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						if (pwinman.RunningPrograms[i].WID == SelectedWindowID)
						{
							SelectedProgramID = pwinman.RunningPrograms[i].PID;
						}

						Refresh(pwinman.RunningPrograms[i].WPN);

						if (WindowID == pwinman.RunningPrograms[i].WID)
						{
							CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 23, 2, 21, 21);
							if (CloseButton.Contains(Event.current.mousePosition))
							{
								if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
								{
									WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
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
									WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
								}
							}

							TitleBarStuff(pwinman.RunningPrograms[i].WPN);

							RenderFileUI(pwinman.RunningPrograms[i].WPN);

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

		//ProgramSystemv2 Test = LocalRegistry.GetProgramData(PersonName, PID, ProgramName,"Test 1",0);

		//LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", Test.Target);

		//var ProgramSystemV2 test = new ProgramSystemv2();

		LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Target);
		//LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", local);
		LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "CurrentDirectory"));
		LocalRegistry.RemoveAllProgramData(PersonName,PID, ProgramName,"Test 1");
		LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile",-1);

		//FMS[PID].PageFile.RemoveRange(0, FMS[PID].PageFile.Count);
		//FMS[PID].SelectedFile = -1;
		//FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
		//HistorySystem("Add");
		//MenuSelected = 0;
	}

	void OpenExe(int PID)
	{
		PlayClickSound();

		//ProgramSystemv2 Test = LocalRegistry.GetProgramData(PersonName, PID, ProgramName,"Test 1",0);

		//LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", Test.Target);

		//var ProgramSystemV2 test = new ProgramSystemv2();

		var PName = LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Name;
		var PTarget = LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Target;
		appman.ProgramRequest(PName, PTarget, PersonName);
		LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", -1);

		//FMS[PID].PageFile.RemoveRange(0, FMS[PID].PageFile.Count);
		//FMS[PID].SelectedFile = -1;
		//FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
		//HistorySystem("Add");
		//MenuSelected = 0;
	}

	void RenderFileUI(int ProgramID)
	{

		Registry.SetVector2Data(PersonName, ProgramName, "MainScrollPos", GUI.BeginScrollView(new Rect(3, 85, 290, 106), Registry.GetVector2Data(PersonName, ProgramName, "MainScrollPos"), new Rect(0, 0, 0, Registry.GetIntData(PersonName, ProgramName, "MainScrollSize") * 21)));

		if (LocalRegistry.GetProgramDataCount(PersonName, ProgramID, ProgramName, "Test 1") > 0)
        {
			for (int m = 0; m < LocalRegistry.GetProgramDataCount(PersonName, ProgramID, ProgramName, "Test 1"); m++)
			{
				if (LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Location == LocalRegistry.GetStringData(PersonName, ProgramID, ProgramName, "CurrentDirectory"))
				{
					switch (LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Extension)
					{
						case ProgramSystemv2.FileExtension.Dir:
							if (LocalRegistry.GetIntData(PersonName, ProgramID, ProgramName, "SelectedFile") == m)
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

							if (GUI.Button(new Rect(21, 21 * m, 250, 20), "" + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Name + " " + "(" + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Description + ")" + " " + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Free.ToString("F2") + " free of " + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Capacity) || GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.RightArrow))
							{
								if (GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.RightArrow))
								{
									PlayClickSound();
									//FMS[j].SelectedFile = m;
									LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									OpenFld(ProgramID);
								}
								if (Input.GetMouseButtonUp(0))
								{
									if (Time.time - Registry.GetFloatData(PersonName, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, ProgramName, "LastClickThreshold"))
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
										OpenFld(ProgramID);
									}
									else
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									}
									Registry.SetFloatData(PersonName, ProgramName, "LastClick", Time.time);
								}
								if (Input.GetMouseButtonUp(1))
								{
									LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									if (new Rect(21, 21 * LocalRegistry.GetIntData(PersonName, ProgramID, ProgramName, "SelectedFile"), 200, 20).Contains(Event.current.mousePosition))
									{
										//ContextPos();
									}
								}
							}
							break;

						case ProgramSystemv2.FileExtension.Fdl:
							if (LocalRegistry.GetIntData(PersonName, ProgramID, ProgramName, "SelectedFile") == m)
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

							if (GUI.Button(new Rect(21, 21 * m, 250, 20), "" + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Name) || GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.RightArrow))
							{
								if (GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.RightArrow))
								{
									PlayClickSound();
									//FMS[j].SelectedFile = m;
									LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									OpenFld(ProgramID);
								}
								if (Input.GetMouseButtonUp(0))
								{
									if (Time.time - Registry.GetFloatData(PersonName, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, ProgramName, "LastClickThreshold"))
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
										OpenFld(ProgramID);
									}
									else
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									}
									Registry.SetFloatData(PersonName, ProgramName, "LastClick", Time.time);
								}
								if (Input.GetMouseButtonUp(1))
								{
									LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									if (new Rect(21, 21 * LocalRegistry.GetIntData(PersonName, ProgramID, ProgramName, "SelectedFile"), 200, 20).Contains(Event.current.mousePosition))
									{
										//ContextPos();
									}
								}
							}
							break;

						case ProgramSystemv2.FileExtension.Exe:
							if (LocalRegistry.GetIntData(PersonName, ProgramID, ProgramName, "SelectedFile") == m)
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

							if (GUI.Button(new Rect(21, 21 * m, 250, 20), "" + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Name) || GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.RightArrow))
							{
								if (GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.RightArrow))
								{
									PlayClickSound();
									//FMS[j].SelectedFile = m;
									LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									OpenExe(ProgramID);
								}
								if (Input.GetMouseButtonUp(0))
								{
									if (Time.time - Registry.GetFloatData(PersonName, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, ProgramName, "LastClickThreshold"))
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
										OpenExe(ProgramID);
									}
									else
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									}
									Registry.SetFloatData(PersonName, ProgramName, "LastClick", Time.time);
								}
								if (Input.GetMouseButtonUp(1))
								{
									LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									if (new Rect(21, 21 * LocalRegistry.GetIntData(PersonName, ProgramID, ProgramName, "SelectedFile"), 200, 20).Contains(Event.current.mousePosition))
									{
										//ContextPos();
									}
								}
							}
							break;
					}
				}
			}
		}

		GUI.EndScrollView();
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
