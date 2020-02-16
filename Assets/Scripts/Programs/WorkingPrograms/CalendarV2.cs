using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarV2 : MonoBehaviour {

    public GameObject SysSoftware;
    public GameObject Applications;
    public bool show;
    private Computer com;
    public int windowID;
    public Rect windowRect;
    public float native_width = 1920;
    public float native_height = 1080;

    private AppMan appman;
    private EventViewer eventview;

    public int SelectedNotification;

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;

    public Rect TextAreaRect;

    public int SelectedMenu;

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

    public List<string> Months = new List<string>();

    public bool GridUpdated;

    public DateSystem SelectedTime;
    public DateSystem DefaltTime;
    public DateSystem CurrentTime;

    private System.DateTime date = new System.DateTime(1970, 1, 1);

    public string Menu;
    public int Math;
    public int SelectedMonth;
    public int SelectedYear;

    public Rect DateString;

    public bool DatePicker;
    public bool EventStart;

    void Start()
    {
        Applications = GameObject.Find("Applications");
        SysSoftware = GameObject.Find("System");
        com = SysSoftware.GetComponent<Computer>();
        appman = SysSoftware.GetComponent<AppMan>();
        eventview = Applications.GetComponent<EventViewer>();

        PosCheck();

        native_height = Customize.cust.native_height;
        native_width = Customize.cust.native_width;

        windowRect.width = 185;
        windowRect.height = 200;

        ContextwindowRect.width = 100;

        CloseButton = new Rect(windowRect.width - 23, 2, 21, 21);
        DateString = new Rect(2, windowRect.height - 178, 200, 22);

        StartingDay = 1;


        DefaltTime.Day = 1;
        DefaltTime.DayNumber = 5;
        DefaltTime.Month = 1;
        DefaltTime.LeapYearCount = 2;
        DefaltTime.Year = 1970;
        DefaltTime.StartDay = 5;

        CurrentTimeUpdate();

        Menu = "Dates";

        Months.Add("");
        Months.Add("Jan");
        Months.Add("Feb");
        Months.Add("Mar");
        Months.Add("Apr");
        Months.Add("May");
        Months.Add("Jun");
        Months.Add("Jul");
        Months.Add("Aug");
        Months.Add("Sep");
        Months.Add("Oct");
        Months.Add("Nov");
        Months.Add("Dec");
    }

    void CurrentTimeUpdate()
    {
        CurrentTime.Year = GameControl.control.Time.Year;
        CurrentTime.Month = GameControl.control.Time.Month;
        CurrentTime.MonthName = GameControl.control.Time.MonthName;
        CurrentTime.Day = GameControl.control.Time.Day;
        CurrentTime.DayNumber = GameControl.control.Time.DayNumber;
        CurrentTime.DayName = GameControl.control.Time.DayName;
        CurrentTime.StartDay = GameControl.control.Time.StartDay;
        CurrentTime.EndDay = GameControl.control.Time.EndDay;
        CurrentTime.LeapYearCount = GameControl.control.Time.LeapYearCount;

        SelectedTime = CurrentTime;

        date = new System.DateTime(CurrentTime.Year, CurrentTime.Month, CurrentTime.Day);

        SelectedTime.Day = date.Day;

        UpdateGrid();
    }

    void SwitchMonth(int Direction)
    {
        if(Direction < 0)
        {
            date = date.AddMonths(-1);
        }
        else
        {
            date = date.AddMonths(1);
        }
    }

    void SwitchYear(int Direction)
    {
        if (Direction < 0)
        {
            date = date.AddYears(-1);
        }
        else
        {
            date = date.AddYears(1);
        }
        SelectedYear = date.Year;
    }

    int GetTotalDays(int Year, int Month)
    {
        return System.DateTime.DaysInMonth(Year,Month);
    }

    int GetMonthStartDay(int Year, int Month)
    {
        System.DateTime temp = new System.DateTime(Year, Month, 1);

        return (int)temp.DayOfWeek + 1;
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
        GUI.skin = com.Skin[GameControl.control.GUIID];

        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        //{
        //    if (!new Rect(windowRect).Contains(Event.current.mousePosition))
        //    {
        //        ShowContext = false;
        //    }
        //}

        if (show == true)
        {
            GUI.color = com.colors[Customize.cust.WindowColorInt];
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
            // windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
        }

        //if (ShowContext == true)
        //{
        //    ContextwindowRect.height = 21 * ContextMenuOptions.Count + 2;
        //    GUI.skin = com.Skin[GameControl.control.GUIID];
        //    GUI.color = com.colors[Customize.cust.WindowColorInt];
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

    void UpdateCalendar()
    {

    }

    void UpdateGrid()
    {
        StartingDay = GetMonthStartDay(date.Year, date.Month);
        int EndDay = GetTotalDays(date.Year, date.Month);
        System.DateTime dt = new System.DateTime(date.Year, date.Month, SelectedTime.Day);
        SelectedTime.DayName = "" + dt.DayOfWeek;

        if (Days.Count <= EndDay)
        {
            for (int i = 0; i < EndDay+1; i++)
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
    }

    void DoMyWindow(int windowID)
    {
        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
            {
                appman.SelectedApp = "Calendar v2";
            }

            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
            GUI.contentColor = com.colors[Customize.cust.FontColorInt];
        }
        else
        {
            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
            GUI.contentColor = com.colors[Customize.cust.FontColorInt];

            GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
        }

        GUI.DragWindow(new Rect(2, 2, CloseButton.x-4, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x-3, 21), "" + "Calendar v2");

        int rows = 0;
        float x = 0;
        float y = 0;
        day = 0;

        //GUI.Label(new Rect(2, windowRect.height - 160, 200, 21), SelectedTime.DayName);

        //if(GUI.Button(new Rect(100, windowRect.height - 160, 200, 21), "Reset"))
        //{
        //    DateReset();
        //}

        if(Menu == "Dates")
        {

            GUI.Label(new Rect(DateString), SelectedTime.Day + " " + date.ToString("MMMM") + " " + date.Year + " " + SelectedTime.DayName);

            if (DateString.Contains(Event.current.mousePosition))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Menu = "Months";
                }
            }

            //if (GUI.Button(new Rect(windowRect.width - 44, windowRect.height - 60, 22, 21), "<M"))
            //{
            //    SwitchMonth(-1);
            //}
            //if (GUI.Button(new Rect(windowRect.width - 21, windowRect.height - 60, 18, 21), ">"))
            //{
            //    SwitchMonth(1);
            //}

            GUI.Box(new Rect(2, windowRect.height - 155, 21, 21), "S");
            GUI.Box(new Rect(24, windowRect.height - 155, 21, 21), "M");
            GUI.Box(new Rect(46, windowRect.height - 155, 21, 21), "T");
            GUI.Box(new Rect(68, windowRect.height - 155, 21, 21), "W");
            GUI.Box(new Rect(90, windowRect.height - 155, 21, 21), "T");
            GUI.Box(new Rect(112, windowRect.height - 155, 21, 21), "F");
            GUI.Box(new Rect(134, windowRect.height - 155, 21, 21), "S");

            for (int i = 0; i < Days.Count; i++)
            {
                if (Days[i] != "")
                {
                    day = day + 1;
                    if (GUI.Button(new Rect(2 + x, windowRect.height - 154 + y + 21, 21, 21), Days[i]))
                    {
                        SelectedTime.Day = day;
                        System.DateTime dt = new System.DateTime(date.Year, date.Month, SelectedTime.Day);
                        SelectedTime.DayName = "" + dt.DayOfWeek;
                        if(DatePicker == true)
                        {
                            if (EventStart == true)
                            {
                                eventview.Reminder.EventStart.Day = SelectedTime.Day;
                                eventview.Reminder.EventStart.Month = date.Month;
                                eventview.Reminder.EventStart.Year = date.Year;
                            }
                            else
                            {
                                eventview.Reminder.EventEnd.Day = SelectedTime.Day;
                                eventview.Reminder.EventEnd.Month = date.Month;
                                eventview.Reminder.EventEnd.Year = date.Year;
                            }
                            appman.SelectedApp = "Calendar v2";
                        }
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
        if (Menu == "Months")
        {
            GUI.Label(new Rect(windowRect.width/2-14, windowRect.height - 165, 200, 22),"" + date.Year);

            //GUI.Label(new Rect(windowRect.width / 2, windowRect.height - 165, 200, 22), "|");

            if (GUI.Button(new Rect(windowRect.width / 2 - 35, windowRect.height - 165, 21, 21), "<"))
            {
                SwitchYear(-1);
            }
            if (GUI.Button(new Rect(windowRect.width / 2 + 20, windowRect.height - 165, 21, 21), ">"))
            {
                SwitchYear(1);
            }

            if (GUI.Button(new Rect(windowRect.width-42, windowRect.height - 165, 40, 21), "RSET"))
            {
                Days.RemoveRange(0, Days.Count);
                CurrentTimeUpdate();
                Menu = "Dates";
            }

            //if (GUI.Button(new Rect(windowRect.width - 66, windowRect.height - 120, 22, 21), "DAT"))
            //{
            //    Menu = "Dates";
            //}

            for (int i = 1; i < 13; i++)
            {
                if (GUI.Button(new Rect(2 + x, windowRect.height - 154 + y + 21, 42, 42),"" + Months[i]))
                {
                    SelectedTime.Month = i;
                    SelectedTime.Day = 1;
                    date = new System.DateTime(date.Year, SelectedTime.Month, SelectedTime.Day);
                    SelectedTime.DayName = "" + date.DayOfWeek;
                    Days.RemoveRange(0, Days.Count);
                    UpdateGrid();
                    Menu = "Dates";
                }


                rows++;
                x += 42 + 1;
                if (rows == 4)
                {
                    rows = 0;
                    x = 0;
                    y += 42 + 1;
                }
            }
        }
        if (Menu == "Years")
        {
            GUI.Label(new Rect(2, windowRect.height - 178, 200, 22), "" + date.Year);

            if (GUI.Button(new Rect(windowRect.width - 66, windowRect.height - 120, 22, 21), "DAT"))
            {
                Menu = "Dates";
            }

            for (int i = 1; i < 10; i++)
            {
                if(i < 4)
                {
                    if (GUI.Button(new Rect(2 + x, windowRect.height - 122 + y + 21, 32, 32), "" + i))
                    {
                        SelectedYear = i;
                        Menu = "Month";
                    }
                }


                rows++;
                x += 32 + 1;
                if (rows == 3)
                {
                    rows = 0;
                    x = 0;
                    y += 32 + 1;
                }
            }
        }
    }
}
