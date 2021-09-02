using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileUtility : MonoBehaviour
{
	public GameObject SysSoftware;
	public GameObject WindowHandel;
	private Computer com;
	private WindowManager winman;
	public List<Rect> windowRect = new List<Rect>();
	public List<float> Timers = new List<float>();
	public List<string> TempFileNames = new List<string>();
	public int TempFileIndex;
	public List<FileUtilitySystem> ProgramHandle = new List<FileUtilitySystem>();
	public List<int> ID = new List<int>();
	public int windowID;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	private Defalt defalt;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	private SoundControl sc;

	public string ProgramTitle;
	public string FileName;
	public float FileSize;
	public int FileIndex;

	public int selectedID;
	public int CurrentID;

	public float Timer;

	public bool ForceDone;

	//WEBSITE STUFF
	public GameObject apps;
	private GameObject db;
	private InternetBrowser ib;
	private JailDew jd;
	private Unicom uc;
	private Test test;
	private CLICommandsV2 clic;

	public bool Local;
	public float Speed;

	public int SelectedProgram;

	public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
	public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

	public bool IsEnoughSpace;
	public bool CheckedDisk;
	public bool DownloadingOrInstalling;

	private ErrorProm ep;
	private GameObject prompt;
	private AppMan appman;

	public float matha;

	void Start()
	{
		db = GameObject.Find("Database");
		WindowHandel = GameObject.Find("WindowHandel");
		SysSoftware = GameObject.Find("System");
		apps = GameObject.Find("Applications");
		prompt = GameObject.Find("Prompts");
		appman = SysSoftware.GetComponent<AppMan>();
		com = SysSoftware.GetComponent<Computer>();
		winman = SysSoftware.GetComponent<WindowManager>();
		defalt = SysSoftware.GetComponent<Defalt>();
		clic = SysSoftware.GetComponent<CLICommandsV2>();
		ep = prompt.GetComponent<ErrorProm>();
		AfterStart();
	}

	void AfterStart()
	{
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		//WEBSITE STUFF
		ib = apps.GetComponent<InternetBrowser>();
		jd = db.GetComponent<JailDew>();
	}

	public void AddWindow()
	{
		DefaltSetting = new Rect(Customize.cust.windowx[windowID], Customize.cust.windowy[windowID], 310, 200);
		windowRect.Add(new Rect(DefaltSetting));
		if (windowRect.Count > 0)
		{
			for (int i = 0; i < windowRect.Count; i++)
			{
				if (!ID.Contains(defalt.OpenwindowID.Count + i))
				{
					ID.Add(defalt.OpenwindowID.Count + i);
					Timers.Add(0);
					FileName = "";
					FileSize = 0;
				}
			}
		}
	}

	void OnGUI()
	{
		GUI.skin = com.Skin[GameControl.control.GUIID];

		if (windowRect.Count > 0)
		{
			for (int i = 0; i < windowRect.Count; i++)
			{

				Customize.cust.windowx[windowID] = windowRect[0].x;
				Customize.cust.windowy[windowID] = windowRect[0].y;

				CloseButton = new Rect(windowRect[i].width - 22, 1, 21, 21);
				MiniButton = new Rect(CloseButton.x - 21, 1, 21, 21);
				DefaltBoxSetting = new Rect(1, 1, MiniButton.x - 1, 21);

				if (ProgramHandle.Count > 0)
				{
					if (ProgramHandle[i].Show == true)
					{
						GUI.color = com.colors[Customize.cust.WindowColorInt];
						windowRect[i] = WindowClamp.ClampToScreen(GUI.Window(ID[i], windowRect[i], DoMyWindow, ""));
					}
				}
			}
		}
	}

	void Minimize()
	{
		for (int i = 0; i < windowRect.Count; i++)
		{
			if (ProgramHandle[selectedID].Minimize == true)
			{
				windowRect[i] = (new Rect(windowRect[i].x, windowRect[i].y, DefaltSetting.width, 23));
			}
			else
			{
				//private Rect DefaltSetting = new Rect (windowRect.x,windowRect.y,300,205);
				windowRect[i] = (new Rect(windowRect[i].x, windowRect[i].y, DefaltSetting.width, DefaltSetting.height));
			}
		}
	}

	void Update()
	{
		if (Local == false)
		{
			Speed = GameControl.control.Gateway.InstalledModem[0].CurrentSpeed;
		}
		else
		{
			Speed = GameControl.control.Gateway.InstalledStorageDevice[0].Speed;
		}
		if (selectedID < 0)
		{
			selectedID = 0;
		}

		if (ProgramHandle.Count > 0)
		{
			for (selectedID = 0; selectedID < ProgramHandle.Count; selectedID++)
			{
				if (Speed >= ProgramHandle[selectedID].ItemRemain)
				{
					ProgramHandle[selectedID].Percentage = 100;
				}

				if (ProgramHandle[selectedID].Start == true && ProgramHandle[selectedID].OurFileSize <= ProgramHandle[selectedID].FileSize)
				{
					ProgramHandle[selectedID].Percentage = ProgramHandle[selectedID].OurFileSize / ProgramHandle[selectedID].FileSize * 100;
					ProgramHandle[selectedID].ItemRemain = ProgramHandle[selectedID].FileSize - ProgramHandle[selectedID].OurFileSize;
					ProgramHandle[selectedID].TimeRemainSeconds = ProgramHandle[selectedID].ItemRemain / Speed / GameControl.control.TimeMulti;
					ProgramHandle[selectedID].TimeRemainSeconds = Mathf.RoundToInt(ProgramHandle[selectedID].TimeRemainSeconds-1);
					ProgramHandle[selectedID].TimeRemainUISeconds = (int)(ProgramHandle[selectedID].TimeRemainSeconds % 60); // return the remainder of the seconds divide by 60 as an int
					ProgramHandle[selectedID].TimeRemainSeconds /= 60; // divide current time y 60 to get minutes
					ProgramHandle[selectedID].TimeRemainMin = (int)(ProgramHandle[selectedID].TimeRemainSeconds % 24); //return the remainder of the minutes divide by 60 as an int
					ProgramHandle[selectedID].TimeRemainSeconds /= 24; // divide by 60 to get hours
					ProgramHandle[selectedID].TimeRemainHour = (int)(ProgramHandle[selectedID].TimeRemainSeconds % 24); // return the remainder of the hours divided by 60 as an int


					if (Speed >= ProgramHandle[selectedID].FileSize)
					{
						ProgramHandle[selectedID].OurFileSize += Speed;
					}

					if (ProgramHandle[selectedID].OurFileSize >= ProgramHandle[selectedID].FileSize)
					{
						if (ForceDone == true)
						{
							Done();
						}
					}

					if (ProgramHandle[selectedID].FileSize <= 0)
					{
						if (ForceDone == true)
						{
							Done();
						}
					}

					if (ProgramHandle[selectedID].FileSize <= Speed)
					{
						Done();
					}
				}

				if (ProgramHandle[selectedID].Percentage < 100)
				{
					if (Timers[selectedID] > 0)
					{
						Timers[selectedID] -= Time.deltaTime;
					}

					if (Timers[selectedID] <= 0)
					{
						Timers[selectedID] = 1;
						ProgramHandle[selectedID].OurFileSize += Speed;
					}
				}

				if (ProgramHandle[selectedID].Percentage >= 100)
				{
					Done();
				}
			}
		}
	}

	public void CheckDiskSpace()
	{
		for (int i = 0; i < ProgramHandle.Count; i++)
		{
			for (int j = 0; j < GameControl.control.Gateway.InstalledStorageDevice.Count; j++)
			{
				for (int k = 0; k < GameControl.control.Gateway.InstalledStorageDevice[j].Partitions.Count; k++)
				{
					if (ProgramHandle[i].Location.StartsWith(GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].DriveLetter))
					{
						if (GameControl.control.Gateway.InstalledStorageDevice[j].Partitions[k].Free >= ProgramHandle[i].FileSize)
						{
							IsEnoughSpace = true;
						}
						else
						{
							IsEnoughSpace = false;
						}
					}
				}
			}
		}
		CheckedDisk = true;
	}

	void DoMyWindow(int windowID)
	{
		CurrentID = ID.IndexOf(windowID);
		selectedID = ID.IndexOf(windowID);

		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
			{
				Cancel();
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
				ProgramHandle[selectedID].Minimize = !ProgramHandle[selectedID].Minimize;
				Minimize();
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				ProgramHandle[selectedID].Minimize = !ProgramHandle[selectedID].Minimize;
				Minimize();
			}
		}

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting), "System Action " + ProgramHandle[selectedID].Type.ToString() + " " + ProgramHandle[selectedID].FileName);

		if (ProgramHandle.Count > 0)
		{
			Render();
		}
	}

	void Close()
	{
		if (ProgramHandle[selectedID].OurFileSize >= ProgramHandle[selectedID].FileSize)
		{
			ID.RemoveAt(selectedID);
			ProgramHandle.RemoveAt(selectedID);
			windowRect.RemoveAt(selectedID);
			Timers.RemoveAt(selectedID);
			IsEnoughSpace = false;
			CheckedDisk = false;
		}
	}

	void Cancel()
	{
		ID.RemoveAt(selectedID);
		ProgramHandle.RemoveAt(selectedID);
		windowRect.RemoveAt(selectedID);
		Timers.RemoveAt(selectedID);
		clic.FileIndex = -1;
		FileIndex = -1;
		IsEnoughSpace = false;
		CheckedDisk = false;

	}

	void Done()
	{
		string CurrentDateTime = GameControl.control.Time.FullDate + " " + GameControl.control.Time.CurrentTime;

		switch (ProgramHandle[selectedID].Type)
		{
			case FileUtilitySystem.ProgramType.RemoteDelete:

				for (int Index = 0; Index < GameControl.control.CompanyServerData.Count; Index++)
				{
					if(GameControl.control.CompanyServerData[Index].Name == ProgramHandle[selectedID].ServerLocationName)
					{
						if (GameControl.control.CompanyServerData[Index].Files.Count > 0)
						{
							for (int i = 0; i < GameControl.control.CompanyServerData[Index].Files.Count; i++)
							{
								if (GameControl.control.CompanyServerData[Index].Files[i].Name == ProgramHandle[selectedID].FileName)
								{
									if (clic.FileIndex != -1)
									{
										GameControl.control.CompanyServerData[Index].Files.RemoveAt(clic.FileIndex);
										clic.FileIndex = -1;
										FileIndex = -1;
										Close();
									}
								}
							}
						}
					}
				}
				break;
			case FileUtilitySystem.ProgramType.LocalFolderDelete:
				//if (FileIndex > 0)
				//{
				//	if (GameControl.control.ProgramFiles[FileIndex].Location.Contains(ProgramHandle[selectedID].FileName))
				//	{
				//		ProgramHandle[selectedID].FileSize += GameControl.control.ProgramFiles[FileIndex].Used;
				//		GameControl.control.ProgramFiles.RemoveAt (FileIndex);
				//		FileIndex--;
				//	}
				//	if (!GameControl.control.ProgramFiles[FileIndex].Location.Contains(ProgramHandle[selectedID].FileName))
				//	{
				//		FileIndex--;
				//	}
				//}
				//for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
				//{
				//	if (GameControl.control.ProgramFiles[i].Name == ProgramHandle[selectedID].FileName) 
				//	{
				//		if (GameControl.control.ProgramFiles[i].Location == ProgramHandle[selectedID].Location)
				//		{
				//			clic.FileIndex = i;
				//			if (clic.FileIndex != -1 && FileIndex <= 0)
				//			{
				//				GameControl.control.ProgramFiles.RemoveAt (clic.FileIndex);
				//				clic.FileIndex = -1;
				//				FileIndex = -1;
				//				ForceDone = false;
				//				Close();
				//			}
				//		}
				//	}
				//}
				for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
				{
					if (GameControl.control.ProgramFiles[i].Name == ProgramHandle[selectedID].FileName)
					{
						if (GameControl.control.ProgramFiles[i].Location == ProgramHandle[selectedID].Location)
						{
							clic.FileIndex = i;
							if (clic.FileIndex != -1)
							{
								GameControl.control.ProgramFiles.RemoveAt(clic.FileIndex);
								clic.FileIndex = -1;
								FileIndex = -1;
								Local = false;
								Close();
							}
						}
					}
				}
				//if(FileIndex <= 0)
				//{
				//	//GameControl.control.ProgramFiles.RemoveAt (FileIndex);
				//	clic.FileIndex = -1;
				//	FileIndex = -1;
				//	ForceDone = false;
				//	Close ();
				//}
				//for (int Index = 0; Index < GameControl.control.ProgramFiles.Count; Index++)
				//{
				//	TempFileIndex = Index;
				//	if (GameControl.control.ProgramFiles[Index].Location.Contains(ProgramHandle[selectedID].FileName))
				//	{
				//		if (!TempFileNames.Contains(GameControl.control.ProgramFiles[Index].Name))
				//		{
				//			TempFileNames.Add(GameControl.control.ProgramFiles[Index].Name);
				//		}
				//	}
				//}
				//if (TempFileIndex >= GameControl.control.ProgramFiles.Count)
				//{
				//	for (int Index = 0; Index < TempFileNames.Count; Index++)
				//	{
				//		if (GameControl.control.ProgramFiles[Index].Location.Contains(TempFileNames[Index]))
				//		{
				//			GameControl.control.ProgramFiles.RemoveAt (Index);
				//			TempFileNames.RemoveAt (Index);
				//		}
				//	}
				//}
				break;
			case FileUtilitySystem.ProgramType.LocalDelete:
				for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
				{
					if (GameControl.control.ProgramFiles[i].Name == ProgramHandle[selectedID].FileName)
					{
						if (GameControl.control.ProgramFiles[i].Location == ProgramHandle[selectedID].Location)
						{
							clic.FileIndex = i;
							if (clic.FileIndex != -1)
							{
								GameControl.control.ProgramFiles.RemoveAt(clic.FileIndex);
								clic.FileIndex = -1;
								FileIndex = -1;
								Local = false;
								Close();
							}
						}
					}
				}

				break;
			case FileUtilitySystem.ProgramType.Download:
				if (clic.FileIndex != -1)
				{
					GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, "", "", ProgramHandle[selectedID].Location, "", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
					//GameControl.control.ProgramFiles.Add (new ProgramSystem (ProgramHandle[selectedID].FileName, "", CurrentDateTime, "", ProgramHandle[selectedID].Location, "", 0, 0, ProgramHandle[selectedID].FileSize, 0, 100, 0, false, ProgramSystem.ProgramType.File));
					clic.FileIndex = -1;
					FileIndex = -1;
					Close();
				}
				break;
			case FileUtilitySystem.ProgramType.Download1:
				if (clic.FileIndex != -1)
				{
					GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, "", "", ProgramHandle[selectedID].Location, "", "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
					//GameControl.control.ProgramFiles.Add (new ProgramSystem (ProgramHandle[selectedID].FileName, "", CurrentDateTime, "", ProgramHandle[selectedID].Location, "", 0, 0, ProgramHandle[selectedID].FileSize, 0, 100, 0, false, ProgramSystem.ProgramType.File));
					clic.FileIndex = -1;
					FileIndex = -1;
					Close();
				}
				break;
			case FileUtilitySystem.ProgramType.DownloadProgram:

				GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, ProgramHandle[selectedID].FileType, "", ProgramHandle[selectedID].Location, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.Ins, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				clic.FileIndex = -1;
				FileIndex = -1;
				Close();

				break;
			case FileUtilitySystem.ProgramType.Installer:

				if (ProgramHandle[selectedID].FileType == "Exe")
				{
					GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, "", "", ProgramHandle[selectedID].Location, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				}
				if (ProgramHandle[selectedID].FileType == "OS")
				{
					GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, "", "", ProgramHandle[selectedID].Location, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				}
				clic.FileIndex = -1;
				FileIndex = -1;
				Local = false;
				Close();

				break;
			case FileUtilitySystem.ProgramType.Upload:
				//GameControl.control.WebsiteFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, "", "", ib.CurrentLocation, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
				FileIndex = -1;
				clic.FileIndex = -1;
				Close();
				break;
			case FileUtilitySystem.ProgramType.Paste:
				Local = false;
				switch (ProgramHandle[selectedID].FileType)
				{
					case "File":
						GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, ProgramHandle[selectedID].FileContent, "", ProgramHandle[selectedID].Location, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.File, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
						FileIndex = -1;
						clic.FileIndex = -1;
						Close();
						break;
					case "Ins":
						GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, ProgramHandle[selectedID].FileContent, "", ProgramHandle[selectedID].Location, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.Ins, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
						FileIndex = -1;
						clic.FileIndex = -1;
						Close();
						break;
					case "Exe":
						GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, ProgramHandle[selectedID].FileContent, "", ProgramHandle[selectedID].Location, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
						FileIndex = -1;
						clic.FileIndex = -1;
						Close();
						break;
					case "Real":
						GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, ProgramHandle[selectedID].FileContent, "", ProgramHandle[selectedID].Location, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
						FileIndex = -1;
						clic.FileIndex = -1;
						Close();
						break;
					case "Txt":
						GameControl.control.ProgramFiles.Add(new ProgramSystem(ProgramHandle[selectedID].FileName, "", "", CurrentDateTime, ProgramHandle[selectedID].FileContent, "", ProgramHandle[selectedID].Location, ProgramHandle[selectedID].FileTarget, "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, ProgramHandle[selectedID].FileSize, 0, 0, 0, 0, 100, ProgramHandle[selectedID].FileVersion, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
						FileIndex = -1;
						clic.FileIndex = -1;
						Close();
						break;
				}
				break;
		}
	}

	void Render()
	{
		if(ProgramHandle[selectedID].Type == FileUtilitySystem.ProgramType.Download || ProgramHandle[selectedID].Type == FileUtilitySystem.ProgramType.Download1 || ProgramHandle[selectedID].Type == FileUtilitySystem.ProgramType.Installer)
		{
			if (CheckedDisk == false)
			{
				CheckDiskSpace();
			}
			else
			{
				if (IsEnoughSpace == false)
				{
					ep.playsound = true;
					ep.enabled = true;
					ep.show = true;
					appman.SelectedApp = "Error Prompt";
					ep.ErrorTitle = "File Utility Error - 4";
					ep.ErrorMsg = "Not enough free disk space.";
					Cancel();
				}
			}
		}

		if (ProgramHandle[selectedID].Type == FileUtilitySystem.ProgramType.LocalFolderDelete)
		{
			GUI.Label(new Rect(5, 30, 300, 200), "File Name: " + GameControl.control.ProgramFiles[FileIndex].Name);
			GUI.Label(new Rect(5, 50, 300, 200), "From: " + GameControl.control.ProgramFiles[FileIndex].Location);
		}
		else if (ProgramHandle[selectedID].Type == FileUtilitySystem.ProgramType.RemoteDelete)
		{
			GUI.Label(new Rect(5, 30, 300, 200), "File Name: " + ProgramHandle[selectedID].FileName);
			GUI.Label(new Rect(5, 50, 300, 200), "From: " + ProgramHandle[selectedID].Location);
		}
		else if (ProgramHandle[selectedID].Type == FileUtilitySystem.ProgramType.LocalDelete)
		{
			GUI.Label(new Rect(5, 30, 300, 200), "File Name: " + ProgramHandle[selectedID].FileName);
			GUI.Label(new Rect(5, 50, 300, 200), "From: " + ProgramHandle[selectedID].Location);
		}
		else
		{
			GUI.Label(new Rect(5, 30, 300, 200), "File Name: " + ProgramHandle[selectedID].FileName);
			GUI.Label(new Rect(5, 50, 300, 200), "To: " + ProgramHandle[selectedID].Location);
			GUI.Label(new Rect(5, 70, 300, 200), "From: " + ProgramHandle[selectedID].Target);
		}
		//GUI.Label (new Rect (5, 30, 200, 200), "File Name: " + ProgramHandle[selectedID].FileName);
		//GUI.Label (new Rect (5, 50, 200, 200), "From: " + ProgramHandle[selectedID].Location);
		//GUI.Label (new Rect (5, 70, 200, 200), "To: " + ProgramHandle[selectedID].Target);

		//GUI.Label(new Rect(5, 30, 200, 200), "File Name: " + GameControl.control.ProgramFiles[FileIndex].Name);
		// GUI.Label(new Rect(5, 50, 200, 200), "From: " + GameControl.control.ProgramFiles[FileIndex].Location);

		int TimeRemainingPosY = 100;

		string hoursString = ProgramHandle[selectedID].TimeRemainHour.ToString("0");
		string minutesString = ProgramHandle[selectedID].TimeRemainMin.ToString("0");
		string secondsString = ProgramHandle[selectedID].TimeRemainUISeconds.ToString("0");
		//string fraction = ((ProgramHandle[selectedID].TimeRemainSeconds * 100) % 100).ToString("000");


		//HOURS = DAYS
		//Minutes = HOURS
		//seconds = Minutes
		string daytag = "";
		string hourtag = "";
		string mintag = "";

		if (ProgramHandle[selectedID].TimeRemainHour > 1)
		{
			GUI.Label(new Rect(2, TimeRemainingPosY, 310, 200), "Time Remaining: More than " + ProgramHandle[selectedID].TimeRemainHour + " Days");
		}
		else
		{
			if(ProgramHandle[selectedID].TimeRemainHour > 0)
			{
				GUI.Label(new Rect(2, TimeRemainingPosY, 310, 200), "Time Remaining: More than " + ProgramHandle[selectedID].TimeRemainHour + " Day");
			}
			else
			{
				if(ProgramHandle[selectedID].TimeRemainMin <= 0)
				{
					if(ProgramHandle[selectedID].TimeRemainUISeconds > 1)
					{
						GUI.Label(new Rect(2, TimeRemainingPosY, 310, 200), "Time Remaining: " + secondsString + " Minutes");
					}
					else
					{
						GUI.Label(new Rect(2, TimeRemainingPosY, 310, 200), "Time Remaining: " + secondsString + " Minute");
					}
				}
				else
				{
					if(ProgramHandle[selectedID].TimeRemainMin > 1)
					{
						if (ProgramHandle[selectedID].TimeRemainUISeconds > 1)
						{
							GUI.Label(new Rect(2, TimeRemainingPosY, 310, 200), "Time Remaining: About " + minutesString + " Hours and " + secondsString + " Minutes");
						}
						else
						{
							GUI.Label(new Rect(2, TimeRemainingPosY, 310, 200), "Time Remaining: About " + minutesString + " Hours and " + secondsString + " Minute");
						}
					}
					else
					{
						if (ProgramHandle[selectedID].TimeRemainUISeconds > 1)
						{
							GUI.Label(new Rect(2, TimeRemainingPosY, 310, 200), "Time Remaining: About " + minutesString + " Hour and " + secondsString + " Minutes");
						}
						else
						{
							GUI.Label(new Rect(2, TimeRemainingPosY, 310, 200), "Time Remaining: About " + minutesString + " Hour and " + secondsString + " Minute");
						}
					}
				}
			}
		}

		//GUI.Label(new Rect(2, 130, 200, 200), "Items Remaining: About" + ProgramHandle[selectedID].ItemRemain.ToString("F2") + GameControl.control.SpaceName);

		GUI.Label(new Rect(2, 150, 200, 200), "Speed: " + Speed + GameControl.control.SpaceName + "/Second");
		GUI.Label(new Rect(DefaltBoxSetting.width - 15, 172.5f, 100, 100), "%" + ProgramHandle[selectedID].Percentage.ToString("F2"));
		GUI.backgroundColor = Color.black;
		GUI.contentColor = Color.black;
		GUI.Box(new Rect(2, 170, 10 + 100 * 2.25f, 27), "");
		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];
		GUI.Box(new Rect(3, 171, 10 + ProgramHandle[selectedID].Percentage * 2.25f, 25), "");
	}
}
