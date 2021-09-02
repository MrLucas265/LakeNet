using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BugReport : MonoBehaviour
{
	public string Subject;
	public string Content;
	public string InformationType;

	public bool CreateInformation;

	public string Date;
	public string Time;
	public string WrittenTime;

	private AppMan appman;

	private GameObject SysSoftware;
	private Computer com;

	public Rect CloseButton = new Rect(177, 2, 21, 21);

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);

	public bool show;

	public int ContextMenuID;
	public Rect ContextwindowRect = new Rect(100, 100, 100, 200);
	public bool ShowContext;
	public List<string> ContextMenuOptions = new List<string>();
	public string SelectedOption;

	private GameObject Camera;
	private ScreenShot sh;

	public string ActualVersion = "3902";

	public enum InformationTypeEnum
	{
		Critical,
		Spelling,
		Report,
		Log,
		Other
	}
	// Use this for initialization
	void Start ()
	{
		SysSoftware = GameObject.Find("System");
		Camera = GameObject.Find("Camera");
		sh = Camera.GetComponent<ScreenShot>();
		com = SysSoftware.GetComponent<Computer>();
		appman = SysSoftware.GetComponent<AppMan>();

		ContextwindowRect = new Rect(2, 49, 65, 22);
	}

	// Update is called once per frame
	void Update ()
	{
		if(CreateInformation == true)
		{
			//switch(InformationTypeEnum)
			//{
			//    case InformationTypeEnum.Critical:
			//        InformationType = "Critical";
			//        break;
			//    case InformationTypeEnum.Spelling:
			//        InformationType = "Spelling";
			//        break;
			//    case InformationTypeEnum.Report:
			//        InformationType = "Report";
			//        break;
			//    case InformationTypeEnum.Log:
			//        InformationType = "Log";
			//        break;
			//    case InformationTypeEnum.Other:
			//        InformationType = "Other";
			//        break;
			//}
			CreateFile();
			CreateInformation = false;
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

		if (ShowContext == true)
		{
			ContextwindowRect.height = 21 * ContextMenuOptions.Count + 2;
			GUI.skin = com.Skin[GameControl.control.GUIID];
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			ContextwindowRect = WindowClamp.ClampToScreen(GUI.Window(ContextMenuID, ContextwindowRect, DoMyContextWindow, ""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
			{
				appman.SelectedApp = "Bug Report";
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]))
			{
				appman.SelectedApp = "Bug Report";
			}
		}

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.TextField(new Rect(2, 25, 65, 22), "Subject: ", 500);

		Subject = GUI.TextField(new Rect(68, 25, 130, 22), Subject, 500);

		InformationType = GUI.TextField(new Rect(68, 49, 130, 22), InformationType, 500);

		GUI.TextField(new Rect(2, 49, 65, 22), "Fld Name: ");

		//GUI.TextField(new Rect(3, 50, 100, 22), InformationType, 500);

		//if (GUI.Button(new Rect(107, 50, 65, 22), "File Type"))
		//{
		//    if (new Rect(110, 55, 65, 21).Contains(Event.current.mousePosition))
		//    {
		//        ContextwindowRect.x = Input.mousePosition.x;
		//        ContextwindowRect.y = Screen.height - Input.mousePosition.y;
		//        ShowContext = true;
		//        GUI.BringWindowToFront(ContextMenuID);
		//    }
		//}

		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
		{
			if (!ContextwindowRect.Contains(Event.current.mousePosition))
			{
				CloseContextMenu();
			}
		}

		Content = GUI.TextArea(new Rect(2, 73, 196, 125), Content, 500);

		GUI.DragWindow(new Rect(53, 2, 123, 21));
		GUI.Box(new Rect(53, 2, 123, 21), "QA Report System");

		if (GUI.Button(new Rect(2, 2, 50, 21), "Submit"))
		{
			CreateFile();
		}
	}

	void AddContextOptions()
	{
		ContextMenuOptions.Add("Note");
		ContextMenuOptions.Add("Grammer");
		ContextMenuOptions.Add("Critical");
		ContextMenuOptions.Add("Bug");
		ContextMenuOptions.Add("Log");
		ContextMenuOptions.Add("Other");
		//ContextMenuOptions.Add ("Create Icon");
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
				if (GUI.Button(new Rect(1, 1 + 21 * i, ContextwindowRect.width - 2, 21), ContextMenuOptions[i]))
				{
					SelectedOption = ContextMenuOptions[i];
				}
			}
		}

		switch (SelectedOption)
		{
		case "Note":
			InformationType = "Note";
			CloseContextMenu();
			break;
		case "Grammer":
			InformationType = "Grammer";
			CloseContextMenu();
			break;
		case "Critical":
			InformationType = "Critical";
			CloseContextMenu();
			break;
		case "Bug":
			InformationType = "Bug";
			CloseContextMenu();
			break;
		case "Log":
			InformationType = "Log";
			CloseContextMenu();
			break;
		case "Other":
			InformationType = "Other";
			CloseContextMenu();
			break;
		}
	}

	void CloseContextMenu()
	{
		ContextMenuOptions.RemoveRange(0, ContextMenuOptions.Count);
		SelectedOption = "";
		ShowContext = false;
	}

	void CreateFile()
	{
		Date = System.DateTime.Now.ToString("dd-MMMM-yyyy");
		Time = System.DateTime.Now.ToString("HH-mm");
		WrittenTime = System.DateTime.Now.ToString("HH:mm dd MMMM, yyyy");

		if (!Directory.Exists(Application.dataPath + "/QA/" + GameControl.control.ProfileName + "/" + Date + "/" + InformationType))
		{
			Directory.CreateDirectory(Application.dataPath + "/QA/" + GameControl.control.ProfileName + "/" + Date + "/" + InformationType);
		}

		TextWriter tw = new StreamWriter(Application.dataPath + "/QA/" + GameControl.control.ProfileName + "/" + Date + "/" + InformationType + "/" + Subject + " " + Time + ".txt");
		sh.path = Application.dataPath + "/QA/" + GameControl.control.ProfileName + "/" + Date + "/" + InformationType;
		sh.Bug = true;
		sh.TakeShot();
		tw.Write("Reported Date: " + WrittenTime + "\n" + "\n" + "Reported By: " + GameControl.control.ProfileName + "\n" + "\n" + GameControl.control.GameVersion[0] + "(" + GameControl.control.GameVersion[2] + ")" + "\n" + GameControl.control.GameVersion[1] + "\n" + ActualVersion + "\n" + "\n" + "Subject: " + Subject + "\n" + "\n" + "Report Type: " + InformationType + "\n" + "\n" + "Report Information: " + "\n" + Content + "\n" + "\n" + "Current OS: " + GameControl.control.SelectedOS.Name);
		tw.Close();

		Subject = "";
		InformationType = "";
		Content = "";
		
	}
}
