using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextReader : MonoBehaviour
{
	public GameObject SysSoftware;
	public bool show;
	private Computer com;
	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	private AppMan appman;

	public float DiskUsage;

	private Defalt defalt;

	public string TypedText;
	public string CurrentWorkingTitle;
	public string TypedTitle;
	public string SaveLocation;

	public int SelectedDocument;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int FoundAt;

	public bool ShowFileNameMaker;
	public bool ShowFileContent;
	public bool ShowFileOpen;

	public bool showSave;

	public Texture2D Icon;

	public float FileSize;

	public Rect TextAreaRect;

	public int SelectedMenu;

	public List<string> Name = new List<string>();
	public List<string> Location = new List<string>();
	public List<int> FileIndex = new List<int>();

	public List<ProgramSystem> Files = new List<ProgramSystem>();

	public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
	public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

	void Start()
	{
		SysSoftware = GameObject.Find("System");
		com = SysSoftware.GetComponent<Computer>();
		defalt = SysSoftware.GetComponent<Defalt>();

		appman = SysSoftware.GetComponent<AppMan>();

		PosCheck();

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		windowRect.width = 300;
		windowRect.height = 300;
	}

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0)
		{
			Customize.cust.windowx[windowID] = Screen.width / 2;
		}
		if (Customize.cust.windowy[windowID] <= 35)
		{
			Customize.cust.windowy[windowID] = 35;
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		if (show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
		}
	}


	void DiskCheck()
	{
		Name.RemoveRange(0, Name.Count);
		Location.RemoveRange(0, Location.Count);
		FileIndex.RemoveRange(0, FileIndex.Count);
		Files.RemoveRange(0, Files.Count);
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.Txt)
			{
				if (Name.Contains(GameControl.control.ProgramFiles[i].Name))
				{
					if (!Location.Contains(GameControl.control.ProgramFiles[i].Location))
					{
						Name.Add(GameControl.control.ProgramFiles[i].Name);
						Location.Add(GameControl.control.ProgramFiles[i].Location);
						FileIndex.Add(i);
						Files.Add(GameControl.control.ProgramFiles[i]);
					}
				}
				else
				{
					Name.Add(GameControl.control.ProgramFiles[i].Name);
					Location.Add(GameControl.control.ProgramFiles[i].Location);
					FileIndex.Add(i);
					Files.Add(GameControl.control.ProgramFiles[i]);
				}
			}
		}
	}

	void DoMyWindow(int windowID)
	{
		GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

		GUI.DragWindow(new Rect(2, 2, 271, 21));
		GUI.Box(new Rect(2, 2, 271, 21), "" + CurrentWorkingTitle + " - Notepad");

		if (GUI.Button(new Rect(windowRect.width - 23, 2, 21, 21), "X"))
		{
			appman.SelectedApp = "Text Reader";
		}

		TextAreaRect = new Rect(windowRect.width - 151, 47, 150, windowRect.height - 48);
		GUI.TextArea(new Rect(TextAreaRect), TypedText, 500);
	}
}
