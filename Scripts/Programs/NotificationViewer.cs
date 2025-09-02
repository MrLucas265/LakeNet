using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationViewer : MonoBehaviour
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

    public int SelectedNotification;

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

    private Rect CloseButton;

    public int ContextMenuID;
    public Rect ContextwindowRect = new Rect(100, 100, 100, 200);
    public bool ShowContext;
    public List<string> ContextMenuOptions = new List<string>();
    public string SelectedOption;
    public Vector2 Scroll;

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

        ContextwindowRect.width = 100;

        CloseButton = new Rect(windowRect.width - 23, 2, 21, 21);
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

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (!new Rect(windowRect).Contains(Event.current.mousePosition))
            {
                ShowContext = false;
            }
        }

        if (show == true)
        {
            GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
            // windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
        }

        if (ShowContext == true)
        {
            ContextwindowRect.height = 21 * ContextMenuOptions.Count + 2;
            GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
            GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
            ContextwindowRect = WindowClamp.ClampToScreen(GUI.Window(ContextMenuID, ContextwindowRect, DoMyContextWindow, ""));
        }
    }

    void AddContextOptions()
    {
		if (SelectedMenu != 0) 
		{
			ContextMenuOptions.Add ("Back");	
		}
		else 
		{
			ContextMenuOptions.Add("View");
		}
        ContextMenuOptions.Add("Dismiss");
    }

    void DoMyContextWindow(int WindowID)
    {
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
                if (GUI.Button(new Rect(1, 1 + 21 * i, ContextwindowRect.width - 2, 21), ContextMenuOptions[i]))
                {
                    SelectedOption = ContextMenuOptions[i];
                }
            }
        }

        switch (SelectedOption)
        {
		case "View":
			SelectedMenu = 2;
			CloseContextMenu();
			break;

		case "Back":
			SelectedMenu = 0;
			CloseContextMenu();
			break;

		case "Dismiss":
			GameControl.control.Notifications.RemoveAt (SelectedNotification);
			SelectedMenu = 0;
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

    void DoMyWindow(int windowID)
    {
        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
            {
                appman.SelectedApp = "Notification Viewer";
            }

            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
        }
        else
        {
            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

            GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]);
        }

        GUI.DragWindow(new Rect(40, 2, 236, 21));
        GUI.Box(new Rect(40, 2, 236, 21), "" + "Notification Viewer");

        switch (SelectedMenu)
        {
            case 0:
                TextAreaRect = new Rect(2, 25, windowRect.width - 4, windowRect.height - 27);

				if (SelectedNotification <= 0)
				{
					SelectedNotification = 0;
				}

				if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.RightArrow) 
				{
					SelectedMenu = 2;
				}

                if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow) 
				{
					if (SelectedNotification >= 1)
					{
						scrollpos.y -= 22;
						SelectedNotification--;
					}

					if (scrollpos.y < SelectedNotification*22)
					{
						scrollpos.y = SelectedNotification*22;
					}

					if (scrollpos.y > SelectedNotification*22)
					{
						scrollpos.y = SelectedNotification*22;
					}
				}

				if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow) 
				{
					if (SelectedNotification < GameControl.control.Notifications.Count-1)
					{
						scrollpos.y += 22;
						SelectedNotification++;
					}

					if (scrollpos.y < SelectedNotification*22)
					{
						scrollpos.y = SelectedNotification*22;
					}

					if (scrollpos.y > SelectedNotification*22)
					{
						scrollpos.y = SelectedNotification*22;
					}
				}

                if(GameControl.control.Notifications.Count > 0)
                {
				
					if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) 
					{
						SelectedMenu = 2;
					}

					if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Delete) 
					{
						GameControl.control.Notifications.RemoveAt (SelectedNotification);
					}

                    scrollpos = GUI.BeginScrollView(new Rect(TextAreaRect), scrollpos, new Rect(0, 0, 0, scrollsize * 22));
                    for (scrollsize = 0; scrollsize < GameControl.control.Notifications.Count; scrollsize++)
                    {
						if (SelectedNotification == scrollsize)
						{
							if (GUI.Button(new Rect(0, 22 * scrollsize, TextAreaRect.width-20, 21), "☒" + GameControl.control.Notifications[scrollsize].Title))
							{
								SelectedNotification = scrollsize;
							}
						}
						else
						{
							if (GUI.Button(new Rect(0, 22 * scrollsize, TextAreaRect.width-20, 21), "☐" + GameControl.control.Notifications[scrollsize].Title))
							{
								SelectedNotification = scrollsize;
							}
						}
                    }
                    GUI.EndScrollView();
                }
                break;
            case 1:
                TextAreaRect = new Rect(115, 25, 150, 128);
                break;
            case 2:
				if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace) 
				{
					SelectedMenu = 0;
				}

				if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Delete) 
				{
					GameControl.control.Notifications.RemoveAt (SelectedNotification);
					SelectedMenu = 0;
				}

				if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftArrow) 
				{
					SelectedMenu = 0;
				}

				TextAreaRect = new Rect(2, 47, windowRect.width-4, windowRect.height - 48);
				TypedText = GUI.TextArea(new Rect(TextAreaRect), "Title: " + GameControl.control.Notifications[SelectedNotification].Title  + "\n" + "SubTitle: " + GameControl.control.Notifications[SelectedNotification].Subtitle + "\n" + "Message: " + GameControl.control.Notifications[SelectedNotification].Message + "\n" + "Time: " + GameControl.control.Notifications[SelectedNotification].Time + "\n" + "Date: " + GameControl.control.Notifications[SelectedNotification].Date, 500);
                break;
            case 3:
                TextAreaRect = new Rect(115, 25, 150, 128);
                break;
        }

        if (ShowFileNameMaker == true)
        {
           // SaveLocation = fp.SelectedFolderLocation;

            GUI.Label(new Rect(5, 50, 150, 21), "File Name");
            TypedTitle = GUI.TextField(new Rect(5, 100, 140, 21), TypedTitle);

            GUI.Label(new Rect(5, 150, 150, 21), "File Location");
            SaveLocation = GUI.TextField(new Rect(5, 200, 140, 21), SaveLocation);
        }

		if (GUI.Button(new Rect(2, 2, 37, 21), "[---]"))
		{
			if (new Rect(2, 2, 37, 21).Contains(Event.current.mousePosition))
			{
				if (GameControl.control.Notifications.Count > 0)
				{
					ContextwindowRect.x = Input.mousePosition.x;
					ContextwindowRect.y = Screen.height - Input.mousePosition.y;
					ShowContext = true;
					GUI.BringWindowToFront(ContextMenuID);
				}
			}
		}
    }
}
