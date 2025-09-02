using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventViewer : MonoBehaviour
{

    public GameObject SysSoftware;
    public GameObject Applications;
    public bool show;
    private Computer com;
    public int windowID;
    public Rect windowRect;
    public float native_width = 1920;
    public float native_height = 1080;

    private AppMan appman;

    public int SelectedEvent;

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;

    public Rect TextAreaRect;

    public string SelectedMenu;

    private Rect CloseButton;

    public int ContextMenuID;
    public Rect ContextwindowRect = new Rect(100, 100, 100, 200);
    public bool ShowContext;
    public List<string> ContextMenuOptions = new List<string>();
    public string SelectedOption;
    public Vector2 Scroll;

    //STUFF
    public ReminderSystem Reminder;
    private CalendarV2 calendarv2;

    public Texture2D CalendarIcon;

    void Start()
    {
        SysSoftware = GameObject.Find("System");
        Applications = GameObject.Find("Applications");
        com = SysSoftware.GetComponent<Computer>();
        appman = SysSoftware.GetComponent<AppMan>();
        calendarv2 = Applications.GetComponent<CalendarV2>();

        PosCheck();

        native_height = Customize.cust.native_height;
        native_width = Customize.cust.native_width;

        windowRect.width = 185;
        windowRect.height = 200;

        ContextwindowRect.width = 100;

        CloseButton = new Rect(windowRect.width - 23, 2, 21, 21);

        TextAreaRect = new Rect(2, 50, windowRect.width - 4, windowRect.height - 53);

        SelectedMenu = "Main Menu";
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

        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        //{
        //    if (!new Rect(windowRect).Contains(Event.current.mousePosition))
        //    {
        //        ShowContext = false;
        //    }
        //}

        if (show == true)
        {
            GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
            // windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
        }

        //if (ShowContext == true)
        //{
        //    ContextwindowRect.height = 21 * ContextMenuOptions.Count + 2;
        //    GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
        //    GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
        //    ContextwindowRect = WindowClamp.ClampToScreen(GUI.Window(ContextMenuID, ContextwindowRect, DoMyContextWindow, ""));
        //}
    }

    void DoMyWindow(int windowID)
    {
        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
            {
                appman.SelectedApp = "Event Viewer";
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

        GUI.DragWindow(new Rect(2, 2, CloseButton.x - 4, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x - 3, 21), "" + "Event Viewer");


        switch (SelectedMenu)
        {
            case "Main Menu":
                MainUI();
                break;
            case "Event View":
                ViewUI();
                break;
            case "Event Creator":
                CreatorUI();
                break;
        }
    }

    void MainUI()
    {
        if (GUI.Button(new Rect(2, CloseButton.height+3, 100, 21), "Add Reminder"))
        {
            SelectedMenu = "Event Creator";
        }

        if (GameControl.control.Reminders.Count > 0)
        {
            scrollpos = GUI.BeginScrollView(new Rect(TextAreaRect), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
            for (scrollsize = 0; scrollsize < GameControl.control.Reminders.Count; scrollsize++)
            {
                if (GUI.Button(new Rect(0, 21 * scrollsize, TextAreaRect.width - 20, 20), "" + GameControl.control.Reminders[scrollsize].Title))
                {
                    SelectedEvent = scrollsize;
                    SelectedMenu = "Event View";
                }
            }
            GUI.EndScrollView();
        }
    }

    void ViewUI()
    {
        if (GUI.Button(new Rect(2, CloseButton.height + 3, 50, 21), "Back"))
        {
            SelectedMenu = "Main Menu";
        }

        GUI.Label(new Rect(2, CloseButton.height + 26, 100, 22), "Title: ");
        GUI.TextField(new Rect(74, CloseButton.height + 26, 102, 21), GameControl.control.Reminders[SelectedEvent].Title);

        GUI.Label(new Rect(2, CloseButton.height + 48, 100, 22), "Sub-Title: ");
        GUI.TextField(new Rect(74, CloseButton.height + 48, 102, 21), GameControl.control.Reminders[SelectedEvent].Subtitle);

        GUI.Label(new Rect(2, CloseButton.height + 70, 100, 22), "Start: ");
        GUI.TextField(new Rect(74, CloseButton.height + 70, 80, 21), "" + GameControl.control.Reminders[SelectedEvent].EventStart.Day + "/" + GameControl.control.Reminders[SelectedEvent].EventStart.Month + "/" + GameControl.control.Reminders[SelectedEvent].EventStart.Year);

        GUI.Label(new Rect(2, CloseButton.height + 92, 100, 22), "End: ");
        GUI.TextField(new Rect(74, CloseButton.height + 92, 80, 21), "" + GameControl.control.Reminders[SelectedEvent].EventEnd.Day + "/" + GameControl.control.Reminders[SelectedEvent].EventEnd.Month + "/" + GameControl.control.Reminders[SelectedEvent].EventEnd.Year);

        GUI.Label(new Rect(2, CloseButton.height + 108, 100, 22), "Message ");
        GUI.TextArea(new Rect(2, CloseButton.height + 128, windowRect.width - 4, 50), GameControl.control.Reminders[SelectedEvent].Message);
    }

    void CreatorUI()
    {
        if (GUI.Button(new Rect(2, CloseButton.height + 3, 50, 21), "Back"))
        {
            SelectedMenu = "Main Menu";
        }

        if (GUI.Button(new Rect(72, CloseButton.height + 3, 50, 21), "Add"))
        {
            //Reminder.CreatedTime = GameControl.control.Time;
            GameControl.control.Reminders.Add(Reminder);
            SelectedMenu = "Main Menu";
        }
        GUI.Label(new Rect(2, CloseButton.height + 26, 100, 22), "Title: ");
        Reminder.Title = GUI.TextField(new Rect(74, CloseButton.height + 26, 102, 21), Reminder.Title);

        GUI.Label(new Rect(2, CloseButton.height + 48, 100, 22), "Sub-Title: ");
        Reminder.Subtitle = GUI.TextField(new Rect(74, CloseButton.height + 48, 102, 21), Reminder.Subtitle);

        GUI.Label(new Rect(2, CloseButton.height + 70, 100, 22), "Start: ");
        GUI.TextField(new Rect(74, CloseButton.height + 70, 80, 21),"" + Reminder.EventStart.Day + "/" + Reminder.EventStart.Month + "/" + Reminder.EventStart.Year);
        if (GUI.Button(new Rect(155, CloseButton.height + 70, 21, 21), CalendarIcon))
        {
            calendarv2.Menu = "Months";
            appman.SelectedApp = "Calendar v2";
            calendarv2.DatePicker = true;
            calendarv2.EventStart = true;
            calendarv2.Menu = "Months";
        }

        GUI.Label(new Rect(2, CloseButton.height + 92, 100, 22), "End: ");
        GUI.TextField(new Rect(74, CloseButton.height + 92, 80, 21), "" + Reminder.EventEnd.Day + "/" + Reminder.EventEnd.Month + "/" + Reminder.EventEnd.Year);
        if (GUI.Button(new Rect(155, CloseButton.height + 92, 21, 21), CalendarIcon))
        {
            calendarv2.Menu = "Months";
            appman.SelectedApp = "Calendar v2";
            calendarv2.DatePicker = true;
            calendarv2.EventStart = false;
            calendarv2.Menu = "Months";
        }

        GUI.Label(new Rect(2, CloseButton.height + 114, 100, 22), "Message: ");
        Reminder.Message = GUI.TextArea(new Rect(2, CloseButton.height + 134, windowRect.width - 4, 40), Reminder.Message);


        //if (GUI.Button(new Rect(102, CloseButton.height + 26 + 43, 50, 21), "📅 📆 �"))
        //{
        //    appman.SelectedApp = "Calendar v2";
        //    calendarv2.DatePicker = true;
        //    calendarv2.Menu = "Months";
        //}
    }
}
