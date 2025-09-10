using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour {

    public GameObject SysSoftware;
    public bool show;
    private Computer com;
    public int windowID;
    public Rect windowRect;
    public float native_width = 1920;
    public float native_height = 1080;
    public bool Drag;

    //private FileExplorer fp;

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

    public int day;

    public int StartingDay;

    public int StartingPos;

    public int TotalGrid;

    public List<string> Days = new List<string>();

    public bool GridUpdated;

    public DateSystem SelectedTime;
    public DateSystem DefaltTime;
    public DateSystem CurrentTime;

    public int MathDay;

    public bool IncreaseDays;
    public bool DecreaseDays;

    void Start()
    {
        SysSoftware = GameObject.Find("System");
        com = SysSoftware.GetComponent<Computer>();
        defalt = SysSoftware.GetComponent<Defalt>();
        //fp = SysSoftware.GetComponent<FileExplorer>();
        appman = SysSoftware.GetComponent<AppMan>();

        PosCheck();

        native_height = Customize.cust.native_height;
        native_width = Customize.cust.native_width;

        windowRect.width = 200;
        windowRect.height = 200;

        ContextwindowRect.width = 100;

        CloseButton = new Rect(windowRect.width - 23, 2, 21, 21);

        StartingDay = 1;


        DefaltTime.Day = 1;
        DefaltTime.DayNumber = 5;
        DefaltTime.Month = 1;
        DefaltTime.LeapYearCount = 2;
        DefaltTime.Year = 1970;
        DefaltTime.StartDay = 5;

        CurrentTimeUpdate();
    }

    void CurrentTimeUpdate()
    {
        CurrentTime.Year = PersonController.control.Global.DateTime.Year;
        CurrentTime.Month = PersonController.control.Global.DateTime.Month;
        CurrentTime.MonthName = PersonController.control.Global.DateTime.MonthName;
        CurrentTime.Day = PersonController.control.Global.DateTime.Day;
        CurrentTime.DayNumber = PersonController.control.Global.DateTime.DayNumber;
        CurrentTime.DayName = PersonController.control.Global.DateTime.DayName;
        CurrentTime.StartDay = PersonController.control.Global.DateTime.StartDay;
        CurrentTime.EndDay = PersonController.control.Global.DateTime.EndDay;
        CurrentTime.LeapYearCount = PersonController.control.Global.DateTime.LeapYearCount;

        SelectedTime = CurrentTime;

        UpdateGrid();
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

    void Update()
    {
        if (IncreaseDays == true)
        {
            IncreaseValue();
        }

        if (DecreaseDays == true)
        {
            DecreaseValue();
        }
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

    void DateReset()
    {
        SelectedTime.Day = 1;
        SelectedTime.DayNumber = 5;
        SelectedTime.Month = 1;
        SelectedTime.LeapYearCount = 2;
        SelectedTime.Year = 1970;
    }

    void IncreaseValue()
    {
        //SelectedTime.DayNumber = SelectedTime.StartDay;
        //SelectedTime.Day = 1;

        for (int i = 1; SelectedTime.Day < SelectedTime.EndDay; i++)
        {
            UpdateDays(1);
        }

        if(SelectedTime.Day >= SelectedTime.EndDay)
        {
            UpdateDays(1);
            Days.RemoveRange(0, Days.Count);
            UpdateGrid();
            IncreaseDays = false;
        }
    }

    void DecreaseValue()
    {
        //SelectedTime.DayNumber = SelectedTime.StartDay;
        //SelectedTime.Day = 1;

        for (int i = 1; SelectedTime.Day < SelectedTime.EndDay; i--)
        {
            UpdateDays(-1);
        }

        if (SelectedTime.Day >= SelectedTime.EndDay)
        {
            UpdateDays(-1);
            Days.RemoveRange(0, Days.Count);
            UpdateGrid();
            DecreaseDays = false;
        }
    }

    void UpdateGrid()
    {
        StartingDay = SelectedTime.StartDay;

        if (Days.Count <= SelectedTime.EndDay)
        {
            for (int i = 0; i < SelectedTime.EndDay+1; i++)
            {
                if(i > 0)
                {
                    Days.Add("" + i);
                }
            }
        }

        for (int i = 0; i < StartingDay; i++)
        {
            if(i > 0)
            {
                Days.Insert(0, "");
            }
        }

        GridUpdated = true;
    }

    void DoMyWindow(int windowID)
    {
        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
            {
                appman.SelectedApp = "Calendar";
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

        GUI.DragWindow(new Rect(2, 2, CloseButton.x-4, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x-3, 21), "" + "Calendar");

        int rows = 0;
        float x = 0;
        float y = 0;

        day = 0;

        if(SelectedTime.Day == 1 && GridUpdated == false)
        {
            Days.RemoveRange(0, Days.Count);
            UpdateGrid();
        }

        if (SelectedTime.Day == SelectedTime.EndDay)
        {
            GridUpdated = false;
        }

        GUI.Label(new Rect(2, windowRect.height - 178, 200, 22), SelectedTime.Day + " " + SelectedTime.MonthName + " " + SelectedTime.Year + "  " + SelectedTime.DayName);
        //GUI.Label(new Rect(2, windowRect.height - 160, 200, 21), SelectedTime.DayName);

        //if(GUI.Button(new Rect(100, windowRect.height - 160, 200, 21), "Reset"))
        //{
        //    DateReset();
        //}

        GUI.Box(new Rect(2, windowRect.height - 155, 21, 21), "S");
        GUI.Box(new Rect(24, windowRect.height - 155, 21, 21), "M");
        GUI.Box(new Rect(46, windowRect.height - 155, 21, 21), "T");
        GUI.Box(new Rect(68, windowRect.height - 155, 21, 21), "W");
        GUI.Box(new Rect(90, windowRect.height - 155, 21, 21), "T");
        GUI.Box(new Rect(112, windowRect.height - 155, 21, 21), "F");
        GUI.Box(new Rect(134, windowRect.height - 155, 21, 21), "S");

        if (GUI.Button(new Rect(windowRect.width - 44, windowRect.height - 90, 22, 21), "JUMP"))
        {
            Days.RemoveRange(0, Days.Count);
            CurrentTimeUpdate();
        }

        if (GUI.Button(new Rect(windowRect.width-44, windowRect.height - 60, 22, 21), "<M"))
        {
            ///Days.RemoveRange(0, Days.Count);
            //System.DateTime dt = new System.DateTime(SelectedTime.Year, SelectedTime.Month, 1);
            //Days.Add(dt.da);
        }
        if (GUI.Button(new Rect(windowRect.width - 21, windowRect.height - 60, 18, 21), ">"))
        {
            IncreaseDays = true;
        }

        if (GUI.Button(new Rect(windowRect.width - 44, windowRect.height - 30, 22, 21), "<Y"))
        {

        }
        if (GUI.Button(new Rect(windowRect.width - 21, windowRect.height - 30, 18, 21), ">"))
        {

        }

        for (int i = 0; i < Days.Count; i++)
        {
            if(Days[i] != "")
            {
                day = day + 1;
                if(GUI.Button(new Rect(2 + x, windowRect.height - 154 + y + 21, 21, 21), Days[i]))
                {
                    SelectedTime.Day = day;
                    System.DateTime dt = new System.DateTime(SelectedTime.Year, SelectedTime.Month, SelectedTime.Day);
                    SelectedTime.DayName = "" + dt.DayOfWeek;
                    // UpdateDayNames();
                }
            }


            rows++;
            x += 21 + 1;
            if (rows == 7)
            {
                rows = 0;
                x = 0;
                y += 21 + 1;
            }
        }
    }

    void MonthlyStuff()
    {
        SelectedTime.StartDay = SelectedTime.DayNumber;
        UpdateMonths();
    }

    void UpdateDayNames()
    {
        switch (SelectedTime.DayNumber)
        {
            case 1:
                SelectedTime.DayName = "Sunday";
                break;
            case 2:
                SelectedTime.DayName = "Monday";
                break;
            case 3:
                SelectedTime.DayName = "Tuesday";
                break;
            case 4:
                SelectedTime.DayName = "Wensday";
                break;
            case 5:
                SelectedTime.DayName = "Thursday";
                break;
            case 6:
                SelectedTime.DayName = "Friday";
                break;
            case 7:
                SelectedTime.DayName = "Saturday";
                break;
        }
    }

    void UpdateDays(int Amt)
    {
        if(Amt < 0)
        {
            SelectedTime.Day -= Amt;
            SelectedTime.DayNumber -= Amt;

            if (SelectedTime.DayNumber < 0)
            {
                SelectedTime.DayNumber = 7;
            }

            UpdateDayNames();

            UpdateMonthsReverse();
        }
        else
        {
            SelectedTime.Day += Amt;
            SelectedTime.DayNumber += Amt;

            if (SelectedTime.DayNumber > 7)
            {
                SelectedTime.DayNumber = 1;
            }

            UpdateDayNames();

            UpdateMonths();
        }
    }

    void UpdateMonthsReverse()
    {
        switch (SelectedTime.Month)
        {
            case 1:
                SelectedTime.MonthName = "January";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 2:
                SelectedTime.MonthName = "Febuary";
                if (!SelectedTime.IsLeapYear)
                {
                    SelectedTime.EndDay = 28;
                }
                else
                {
                    SelectedTime.EndDay = 29;
                }
                if (SelectedTime.Day > SelectedTime.EndDay && !SelectedTime.IsLeapYear)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                if (SelectedTime.Day > SelectedTime.EndDay && SelectedTime.IsLeapYear)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    SelectedTime.LeapYearCount = 0;
                    MonthlyStuff();

                }
                break;

            case 3:
                SelectedTime.MonthName = "March";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 4:
                SelectedTime.MonthName = "April";
                SelectedTime.EndDay = 30;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 5:
                SelectedTime.MonthName = "May";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 6:
                SelectedTime.MonthName = "June";
                SelectedTime.EndDay = 30;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 7:
                SelectedTime.MonthName = "July";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 8:
                SelectedTime.MonthName = "August";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 9:
                SelectedTime.MonthName = "September";
                SelectedTime.EndDay = 30;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 10:
                SelectedTime.MonthName = "October";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 11:
                SelectedTime.MonthName = "Novemeber";
                SelectedTime.EndDay = 30;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month--;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 12:
                SelectedTime.MonthName = "December";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month = 1;
                    SelectedTime.Day = 1;
                    SelectedTime.Year--;
                    UpdateLeapYear(-1);
                    MonthlyStuff();
                }
                break;
        }
    }

    void UpdateMonths()
    {
        switch (SelectedTime.Month)
        {
            case 1:
                SelectedTime.MonthName = "January";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 2:
                SelectedTime.MonthName = "Febuary";
                if (!SelectedTime.IsLeapYear)
                {
                    SelectedTime.EndDay = 28;
                }
                else
                {
                    SelectedTime.EndDay = 29;
                }
                if (SelectedTime.Day > SelectedTime.EndDay && !SelectedTime.IsLeapYear)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                if (SelectedTime.Day > SelectedTime.EndDay && SelectedTime.IsLeapYear)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    SelectedTime.LeapYearCount = 0;
                    MonthlyStuff();

                }
                break;

            case 3:
                SelectedTime.MonthName = "March";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 4:
                SelectedTime.MonthName = "April";
                SelectedTime.EndDay = 30;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 5:
                SelectedTime.MonthName = "May";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 6:
                SelectedTime.MonthName = "June";
                SelectedTime.EndDay = 30;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 7:
                SelectedTime.MonthName = "July";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 8:
                SelectedTime.MonthName = "August";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 9:
                SelectedTime.MonthName = "September";
                SelectedTime.EndDay = 30;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 10:
                SelectedTime.MonthName = "October";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 11:
                SelectedTime.MonthName = "Novemeber";
                SelectedTime.EndDay = 30;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month++;
                    SelectedTime.Day = 1;
                    MonthlyStuff();
                }
                break;

            case 12:
                SelectedTime.MonthName = "December";
                SelectedTime.EndDay = 31;
                if (SelectedTime.Day > SelectedTime.EndDay)
                {
                    SelectedTime.Month = 1;
                    SelectedTime.Day = 1;
                    SelectedTime.Year++;
                    UpdateLeapYear(1);
                    MonthlyStuff();
                }
                break;
        }
    }

    void UpdateLeapYear(int amt)
    {
        SelectedTime.LeapYearCount+=amt;

        if (SelectedTime.LeapYearCount == 4)
        {
            SelectedTime.IsLeapYear = true;
        }
        else
        {
            SelectedTime.IsLeapYear = false;
        }
    }
}
