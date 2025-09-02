using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FileMangementUI : MonoBehaviour
{
	public bool quit;

	private GameObject Puter;

	private GameObject WindowHandel;
	private WindowManager winman;

	private Computer com;
	private SoundControl sc;
	//private FileExplorer fp;
	private AppMan appman;

	public float native_width = 1920;
	public float native_height = 1080;

	public string ProgramNameForWinMan;

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

		//fp = Puter.GetComponent<FileExplorer>();
		appman = Puter.GetComponent<AppMan>();

		winman = WindowHandel.GetComponent<WindowManager>();

		ProgramName = "FileManager";
		PersonName = "Player";
		//LocalRegistry.AddNewKey(PersonName, 1, "Test");
	}

    void SelectWindowID(int WindowID)
	{
		if (Input.GetMouseButtonDown(0))
		{
			Registry.SetIntData("Player", "WindowManager", "SelectedWindow", WindowID);
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

	void GUIControls(int PID)
	{
		if (GUIKeyDown(KeyCode.LeftArrow))
		{
			HistorySystem("Back", PID);
			PlayClickSound();
		}

		if (GUIKeyDown(KeyCode.DownArrow))
		{
            if (LocalRegistry.GetProgramDataCount(PersonName, PID, ProgramName, "Test 1") > 0)
			{
				if (LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") < LocalRegistry.GetProgramDataCount(PersonName, PID, ProgramName, "Test 1") - 1)
				{
					if (LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") >= 0)
					{
						LocalRegistry.SetVector2Data(PersonName, PID, ProgramName, "MainScrollPos",new Vector2(0, LocalRegistry.GetVector2Data(PersonName, PID, ProgramName, "MainScrollPos").y + 21));
						LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") + 1);
					}
				}
				if (LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") < 0)
				{
					LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") + 1);
				}
			}
			//DisplayedItems = PageFile.Count / 21;
		}

		if (GUIKeyDown(KeyCode.UpArrow))
		{
			if (LocalRegistry.GetProgramDataCount(PersonName, PID, ProgramName, "Test 1") > 0)
			{
				if (LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") < 0)
				{
					LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", LocalRegistry.GetProgramDataCount(PersonName, PID, ProgramName, "Test 1"));
					//LocalRegistry.GetRectData(PersonName, PID, ProgramName, "MainScrollPos").y = 21 * LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile");
					LocalRegistry.SetVector2Data(PersonName, PID, ProgramName, "MainScrollPos", new Vector2(0, 21 * LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")));
				}
				if (LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") >= 1)
				{
					LocalRegistry.SetVector2Data(PersonName, PID, ProgramName, "MainScrollPos", new Vector2(0, LocalRegistry.GetVector2Data(PersonName, PID, ProgramName, "MainScrollPos").y - 21));
					LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") - 1);
				}
			}

		}

		if (GUIKeyDown(KeyCode.Delete))
		{
			////LocalRegistry.SetIntData(PersonName,1, ProgramName,"Health",100);
			//if (FMS[SelectedProgramID].SelectedFile >= 0)
			//{
			//	//DeleteSystem();
			//}
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


	void ColorUI(int WPN)
	{
		LocalRegistry.SetColorData(PersonName, WPN, ProgramName, "FontColor", new SColor(new Color32(0, 0, 0, 255)));

		LocalRegistry.SetColorData(PersonName, WPN, ProgramName, "WindowColor", new SColor(new Color32(0, 0, 0, 255)));
	}

	void OnGUI()
	{
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
		GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						//ColorUI(pwinman.RunningPrograms[i].WPN);
						//GUI.color = new Color32(LocalRegistry.GetRedColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
						//	LocalRegistry.GetGreenColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
						//	LocalRegistry.GetBlueColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
						//	LocalRegistry.GetAlphaColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"));

						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));

						LocalRegistry.SetRectData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowRect", pwinman.RunningPrograms[i].windowRect);

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
		winman.WindowDragging(Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"), new Rect(2, 2, CloseButton.x - 41, 21));
		GUI.Box(new Rect(2, 2, CloseButton.x - 41, 20), ProgramNameForWinMan + "-" + LocalRegistry.GetStringData(PersonName, PID, ProgramName, "CurrentDirectory"));

		if (LocalRegistry.GetStringListDataCount(PersonName, PID, ProgramName, "PageHistory") <= 1)
		{
			LocalRegistry.SetRectData(PersonName, PID, ProgramName, "TypedDirectory", new SRect(new Rect(
				2,
				LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").y,
				LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").width,
				LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").height)));

			LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory", GUI.TextField(new Rect(LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory")), LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedDirectory"), 400));

			if (GUI.Button(new Rect(LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").x + LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").width + 1, LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").y, 20, 20), "⏎"))
			{
				LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedDirectory"));
				//HistorySystem("Add");
			}
		}
		else
		{
			LocalRegistry.SetRectData(PersonName, PID, ProgramName, "TypedDirectory", new SRect(new Rect(
				23,
				LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").y,
				LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").width,
				LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").height)));

			LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory", GUI.TextField(new Rect(LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory")), LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedDirectory"), 400));

			if (GUI.Button(new Rect(2, LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").y, 20, 20), "<-"))
			{
				HistorySystem("Back",PID);
				PlayClickSound();
			}

			if (GUI.Button(new Rect(LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").x + LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").width+1, LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").y, 20, 20), "⏎"))
			{
				LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedDirectory"));
				//HistorySystem("Add");
			}
		}
	}

	void CheckInitalRun(int WindowID)
    {
		if (LocalRegistry.GetBoolData(PersonName, WindowID, ProgramName, "InitalRun") == false)
		{
			LocalRegistry.SetIntData(PersonName, WindowID, ProgramName, "SelectedFile", -1);
			LocalRegistry.SetBoolData(PersonName, WindowID, ProgramName, "InitalRun", true);
		}

	}

	void Refresh(int WindowID)
	{
		CheckInitalRun(WindowID);

		if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramName, "CurrentDirectory") == "")
		{
			LocalRegistry.SetStringData(PersonName, WindowID, ProgramName, "CurrentDirectory", "Gateway");
			LocalRegistry.SetStringData(PersonName, WindowID, ProgramName, "TypedDirectory", Registry.GetStringData(PersonName, "OS", "DefaultVolumeAddress"));
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

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				if(WindowID == Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"))
                {
					winman.WindowResize(PersonName, Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"));
				}

				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						if (pwinman.RunningPrograms[i].WID == Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"))
						{
							GUIControls(pwinman.RunningPrograms[i].WPN);
							SelectedProgramID = pwinman.RunningPrograms[i].PID;
						}
						Refresh(pwinman.RunningPrograms[i].WPN);

						if (WindowID == pwinman.RunningPrograms[i].WID)
						{
							CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 23, 2, 21, 21);
							if (CloseButton.Contains(Event.current.mousePosition))
							{
								if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
								{
									WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
								}

								GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
								GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
							}
							else
							{
								GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
								GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

								if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]))
								{
									WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
								}
							}

                            TitleBarStuff(pwinman.RunningPrograms[i].WPN);

							RenderFileUI(pwinman.RunningPrograms[i].WPN);

							RenderRibbonUI(pwinman.RunningPrograms[i].WPN);

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

    void HistorySystem(string Action, int PID)
    {

		//PageHistory

		//LocalRegistry.SetStringData(PersonName, PID, ProgramName, "PageHistory", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "CurrentDirectory"));

		if (Action == "Add")
        {
			if (LocalRegistry.GetStringListDataCount(PersonName, PID, ProgramName, "PageHistory") == 0)
			{
				LocalRegistry.AddStringListData(PersonName, PID, ProgramName, "PageHistory", "");
			}

			LocalRegistry.AddStringListData(PersonName, PID, ProgramName, "PageHistory", LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Target);

			//if (!FMS[SelectedProgramID].History.Contains("Gateway"))
           // {
           //     FMS[SelectedProgramID].History.Insert(0, "Gateway");
           // }

           // FMS[SelectedProgramID].History.Add(FMS[SelectedProgramID].CurrentDirectory);
            //FMS[SelectedProgramID].HistoryPosition = FMS[SelectedProgramID].History.Count - 1;
        }


		if (Action == "Back")
		{

			if(LocalRegistry.GetStringListDataCount(PersonName, PID, ProgramName, "PageHistory")>0)
            {
				LocalRegistry.RemoveAtStringListData(PersonName, PID, ProgramName, "PageHistory",
					LocalRegistry.GetLastStringListData(PersonName, PID, ProgramName, "PageHistory"));
			}

			if(LocalRegistry.GetStringListDataCount(PersonName, PID, ProgramName, "PageHistory")==0)
            {
				LocalRegistry.AddStringListData(PersonName, PID, ProgramName, "PageHistory", "");
			}

			LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", -1);

			LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory",
				LocalRegistry.GetStringListData(PersonName, PID, ProgramName, "PageHistory",
				LocalRegistry.GetLastStringListData(PersonName, PID, ProgramName, "PageHistory")));

			LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory",
				LocalRegistry.GetStringData(PersonName, PID, ProgramName, "CurrentDirectory"));
		}

		/*if (Action == "Back")
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
        }*/
	}

	void RenderRibbonUI(int PID)
    {
		//LocalRegistry.SetRectData(PersonName, PID, ProgramName, "Ribbon", new SRect(new Rect(200,20,100,20)));

        LocalRegistry.SetRectData(PersonName, PID, ProgramName, "TypedDirectory", new SRect(new Rect(
			LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").x,
			23,
			LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").width,
			LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").height)));

        int ButtonSize = 21;

        if (LocalRegistry.GetMenuButtonCountData(PersonName, PID, ProgramName, "RenderedRibbon") == 0)
        {
            switch (LocalRegistry.GetStringData(PersonName, PID, ProgramName, "Ribbon"))
            {
                case "":
                    if (LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") >= 0)
                    {
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Unselect", "", 70, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Edit", "Edit", 40, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Copy Path", "", 80, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Properties", "", 90, ButtonSize));
                    }
                    else
                    {
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("New Folder", "", 90, ButtonSize));
                    }
                    break;
                case "Edit":
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Back", "Home", 50, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Copy", "", 50, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Paste", "", 50, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Cut", "", 40, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Pin", "Pin", 40, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Rename", "", 70, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Delete", "", 60, ButtonSize));
                    break;
                case "Pin":
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("Back", "Edit", 40, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("To Desktop", "", 90, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("To Taskbar", "", 90, ButtonSize));
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", new MenuButtonSystem("To Quicklaunch", "", 110, ButtonSize));
                    break;
                case "Home":
                    LocalRegistry.SetStringData(PersonName, PID, ProgramName, "Ribbon", "");
                    break;
            }
        }

        for (int i = 0; i < LocalRegistry.GetMenuButtonCountData(PersonName, PID, ProgramName, "RenderedRibbon"); i++)
        {
            if (i == 0)
            {
                LocalRegistry.SetMenuButtonPosXData(PersonName, PID, ProgramName, "RenderedRibbon", i, 2);

            }
            if (i >= 1)
            {
                LocalRegistry.SetMenuButtonPosXData(PersonName, PID, ProgramName, "RenderedRibbon", i,
                    LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i - 1).PosX +
                    LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i - 1).Width + 1);
            }

            if (GUI.Button(new Rect(LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i).PosX, LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").y + 21, LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i).Width, ButtonSize), LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i).Name))
            {
				if(LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i).Menu != "")
				{
                    LocalRegistry.SetStringData(PersonName, PID, ProgramName, "Ribbon", LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i).Menu);
                    LocalRegistry.RemoveAllMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon");
                }
				else
				{
                    LocalRegistry.SetStringData(PersonName, PID, ProgramName, "SelectedButton", LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i).Name);
					LocalRegistry.SetBoolData(PersonName, PID, ProgramName, "SelectedButton", true);
                    //TestCode.KeywordCheck(PersonName,
                    //Tasks.RunNewTaskWithProgramData(PersonName, LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon", i).Name, 
                    //	LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1",
                    //	LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")));
                }
            }
        }


        LocalRegistry.SetRectData(PersonName, PID, ProgramName, "TypedDirectory", new SRect(new Rect(
			LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").x,
			LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").y,CloseButton.x - 83,20)));

		if(LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile") == -1)
        {
			LocalRegistry.SetStringData(PersonName, PID, ProgramName, "RenderedRibbon", "");
			LocalRegistry.RemoveAllMenuButtonData(PersonName, PID, ProgramName, "RenderedRibbon");
		}
	}

    void OpenFld(int PID)
	{
		PlayClickSound();

        HistorySystem("Add",PID);

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
		//var PTarget = LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Target;
		//var PUsage = LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).FileUsage;

		TestCode.KeywordCheck(PersonName, "Run:" + PName + ";");

		//appman.ProgramRequest(PName, PTarget, PersonName);
		LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", -1);

		//FMS[PID].PageFile.RemoveRange(0, FMS[PID].PageFile.Count);
		//FMS[PID].SelectedFile = -1;
		//FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
		//HistorySystem("Add");
		//MenuSelected = 0;
	}

    void OpenTxt(int PID)
    {
        PlayClickSound();

        //ProgramSystemv2 Test = LocalRegistry.GetProgramData(PersonName, PID, ProgramName,"Test 1",0);

        //LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", Test.Target);

        //var ProgramSystemV2 test = new ProgramSystemv2();

        var PName = LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Name;
        //var PTarget = LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Target;
        //var PUsage = LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).FileUsage;

        TestCode.KeywordCheck(PersonName, "Run:" + PName + ";");

        //appman.ProgramRequest(PName, PTarget, PersonName);
        LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", -1);

        //FMS[PID].PageFile.RemoveRange(0, FMS[PID].PageFile.Count);
        //FMS[PID].SelectedFile = -1;
        //FMS[SelectedProgramID].TypedDirectory = FMS[SelectedProgramID].CurrentDirectory;
        //HistorySystem("Add");
        //MenuSelected = 0;
    }

    void RenderFileUI(int ProgramID)
	{

		if (LocalRegistry.GetProgramDataCount(PersonName, ProgramID, ProgramName, "Test 1") > 0)
		{
			LocalRegistry.SetVector2Data(PersonName, ProgramID, ProgramName, "MainScrollPos", GUI.BeginScrollView(new Rect(3, 85, LocalRegistry.GetRectData(PersonName,ProgramID,ProgramName,"WindowRect").width, 106), LocalRegistry.GetVector2Data(PersonName, ProgramID, ProgramName, "MainScrollPos"), new Rect(0, 0, 0, LocalRegistry.GetIntData(PersonName, ProgramID, ProgramName, "MainScrollPos") * 21)));

			LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "MainScrollPos", LocalRegistry.GetProgramDataCount(PersonName, ProgramID, ProgramName, "Test 1"));

			for (int m = 0; m < LocalRegistry.GetProgramDataCount(PersonName, ProgramID, ProgramName, "Test 1"); m++)
			{
				if (LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Location == LocalRegistry.GetStringData(PersonName, ProgramID, ProgramName, "CurrentDirectory"))
				{
					switch (LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Extension)
					{
						case ProgramSystemv2.FileExtension.dir:
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

							if (GUI.Button(new Rect(21, 21 * m, 250, 20), "" + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Name + " " + "(" + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Description + ")" + " " + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Free.ToString("F2") + " free of " + LocalRegistry.GetProgramData(PersonName, ProgramID, ProgramName, "Test 1", m).Capacity))
							{
								if (Input.GetMouseButtonUp(0))
								{
									if (Time.time - LocalRegistry.GetFloatData(PersonName, ProgramID, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, "System", "DoubleClickSpeed"))
									{
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
										OpenFld(ProgramID);
									}
									else
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									}
									LocalRegistry.SetFloatData(PersonName, ProgramID, ProgramName, "LastClick", Time.time);
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

						case ProgramSystemv2.FileExtension.fdl:
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
								if (Input.GetMouseButtonUp(0))
								{
									if (Time.time - LocalRegistry.GetFloatData(PersonName, ProgramID, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, "System", "DoubleClickSpeed"))
									{
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
										OpenFld(ProgramID);
									}
									else
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									}
									LocalRegistry.SetFloatData(PersonName, ProgramID, ProgramName, "LastClick", Time.time);
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

                        case ProgramSystemv2.FileExtension.txt:
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
                                if (Input.GetMouseButtonUp(0))
                                {
                                    if (Time.time - LocalRegistry.GetFloatData(PersonName, ProgramID, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, "System", "DoubleClickSpeed"))
                                    {
                                        LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
                                        OpenTxt(ProgramID);
                                    }
                                    else
                                    {
                                        PlayClickSound();
                                        LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
                                    }
                                    LocalRegistry.SetFloatData(PersonName, ProgramID, ProgramName, "LastClick", Time.time);
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

                        case ProgramSystemv2.FileExtension.exe:
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
								if (Input.GetMouseButtonUp(0))
								{
									if (Time.time - LocalRegistry.GetFloatData(PersonName, ProgramID, ProgramName, "LastClick") < Registry.GetFloatData(PersonName, "System", "DoubleClickSpeed"))
									{
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
										OpenExe(ProgramID);
									}
									else
									{
										PlayClickSound();
										LocalRegistry.SetIntData(PersonName, ProgramID, ProgramName, "SelectedFile", m);
									}
									LocalRegistry.SetFloatData(PersonName, ProgramID, ProgramName, "LastClick", Time.time);
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

			GUI.EndScrollView();
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
		GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

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
