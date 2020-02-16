using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour 
{
	public float autosave;

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;

	public float multi;

	private Computer com;

    public bool LastDay;

    private NotfiPrompt noti;
    private GameObject Prompt;

	void Start ()
	{
        Prompt = GameObject.Find("Prompts");
        noti = Prompt.GetComponent<NotfiPrompt>();

        com = GetComponent<Computer>();
		multi = 1;
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
        CurrentDate();
        UpdateDayNames();
        UpdateMonths();
        FullDate();
    }

    void Update()
    {
        GameControl.control.Time.Miniutes += multi * Time.deltaTime;
        autosave += 1 * Time.deltaTime;


        CurrentTime();

        if (GameControl.control.Time.Miniutes >= 59.5f)
        {
            UpdateMiniutes();
        }

        switch (GameControl.control.TimeMulti)
        {
            case 0:
                GameControl.control.TimeMulti = 1;
                break;
            case 1:
                Time.timeScale = 1;
                break;
            case 2:
                Time.timeScale = 2;
                break;
            case 3:
                Time.timeScale = 3;
                break;
            case 4:
                Time.timeScale = 4;
                break;
        }

        if (autosave >= 1)
        {
            Screen.fullScreen = Customize.cust.FullScreen;
            QualitySettings.antiAliasing = Customize.cust.AA;
            QualitySettings.vSyncCount = Customize.cust.VSync;
            SaveFunction();
            autosave = 0;
            //GameControl.control.Balance [GameControl.control.SelectedBank] += 1;
        }
    }

    void SaveFunction()
	{
		GameControl.control.Save();
		ProfileController.procon.Save();
		Customize.cust.Save();
		HardwareController.hdcon.Save();
	}

	void TimeFormat()
	{
		switch (Customize.cust.TimeFormat)
		{
		case"":
			break;
		}
	}

    void MonthlyStuff()
    {
        GameControl.control.Time.StartDay = GameControl.control.Time.DayNumber;
        UpdateMonths();
    }

    void PlanCheck()
    {
        if (GameControl.control.Plans.Count > 0)
        {
            for (int i = 0; i < GameControl.control.Plans.Count; i++)
            {
                if(GameControl.control.Plans[i].Due.Day == GameControl.control.Time.Day)
                {
                    GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance -= GameControl.control.Plans[i].Price;

                    GameControl.control.EmailData.Add(new EmailSystem("Monthly Plan",
    GameControl.control.Plans[i].Company, GameControl.control.Time.FullDate,
    GameControl.control.Plans[i].Name + " " + GameControl.control.Plans[i].Price + "Has been redacted from your account.", 0, 1, 1, false,
    EmailSystem.EmailType.New));

                    noti.NewNotification("New Mail", GameControl.control.Plans[i].Company, "Monthly Plan");
                    GameControl.control.Plans[i].Due.Month++;
                    GameControl.control.Plans[i].Due.TodaysDate = "" + GameControl.control.Plans[i].Due.Day.ToString("00") + "/" + GameControl.control.Plans[i].Due.Month.ToString("00") + "/" + GameControl.control.Plans[i].Due.Year.ToString("0000");
                    if (GameControl.control.Plans[i].Due.Month>12)
                    {
                        GameControl.control.Plans[i].Due.Month = 1;
                    }
                }
            }
        }
    }

    void UpdateMiniutes()
    {
        GameControl.control.Time.Miniutes++;

        if (GameControl.control.Time.Miniutes >= 59.5f)
        {
            UpdateHours();
        }
    }

    void UpdateHours()
    {
        GameControl.control.Time.Hours++;
        GameControl.control.Time.Miniutes = 0;

        if (GameControl.control.Time.Hours >= 24)
        {
            UpdateDays();
        }
    }

    void UpdateDayNames()
    {
        switch (GameControl.control.Time.DayNumber)
        {
            case 1:
                GameControl.control.Time.DayName = "Sunday";
                break;
            case 2:
                GameControl.control.Time.DayName = "Monday";
                break;
            case 3:
                GameControl.control.Time.DayName = "Tuesday";
                break;
            case 4:
                GameControl.control.Time.DayName = "Wensday";
                break;
            case 5:
                GameControl.control.Time.DayName = "Thursday";
                break;
            case 6:
                GameControl.control.Time.DayName = "Friday";
                break;
            case 7:
                GameControl.control.Time.DayName = "Saturday";
                break;
        }
    }

    void UpdateDays()
    {
        GameControl.control.Time.Hours = 0;
        GameControl.control.Time.Day++;
        GameControl.control.Time.DayNumber++;

        PlanCheck();

        if (GameControl.control.Time.DayNumber > 7)
        {
            GameControl.control.Time.DayNumber = 1;
        }

        UpdateDayNames();

        UpdateMonths();

        CurrentDate();
    }

    void UpdateMonths()
    {
        switch (GameControl.control.Time.Month)
        {
        case 1:
            GameControl.control.Time.MonthName = "January";
            GameControl.control.Time.EndDay = 31;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 2:
            GameControl.control.Time.MonthName = "Febuary";
            if(!GameControl.control.Time.IsLeapYear)
            {
                GameControl.control.Time.EndDay = 28;
            }
            else
            {
                GameControl.control.Time.EndDay = 29;
            }
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay && !GameControl.control.Time.IsLeapYear)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay && GameControl.control.Time.IsLeapYear)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                GameControl.control.Time.LeapYearCount = 0;
                MonthlyStuff();

            }
            break;

        case 3:
            GameControl.control.Time.MonthName = "March";
            GameControl.control.Time.EndDay = 31;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 4:
            GameControl.control.Time.MonthName = "April";
            GameControl.control.Time.EndDay = 30;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 5:
            GameControl.control.Time.MonthName = "May";
            GameControl.control.Time.EndDay = 31;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 6:
            GameControl.control.Time.MonthName = "June";
            GameControl.control.Time.EndDay = 30;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 7:
            GameControl.control.Time.MonthName = "July";
            GameControl.control.Time.EndDay = 31;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 8:
            GameControl.control.Time.MonthName = "August";
            GameControl.control.Time.EndDay = 31;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 9:
            GameControl.control.Time.MonthName = "September";
            GameControl.control.Time.EndDay = 30;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 10:
            GameControl.control.Time.MonthName = "October";
            GameControl.control.Time.EndDay = 31;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 11:
            GameControl.control.Time.MonthName = "Novmeber";
            GameControl.control.Time.EndDay = 30;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month++;
                GameControl.control.Time.Day = 1;
                MonthlyStuff();
            }
            break;

        case 12:
            GameControl.control.Time.MonthName = "December";
            GameControl.control.Time.EndDay = 31;
            if (GameControl.control.Time.Day > GameControl.control.Time.EndDay)
            {
                GameControl.control.Time.Month = 1;
                GameControl.control.Time.Day = 1;
                GameControl.control.Time.Year++;
                UpdateLeapYear();
                MonthlyStuff();
            }
            break;
        }
    }

    void UpdateLeapYear()
    {
        GameControl.control.Time.LeapYearCount++;

        if (GameControl.control.Time.LeapYearCount == 4)
        {
            GameControl.control.Time.IsLeapYear = true;
        }
        else
        {
            GameControl.control.Time.IsLeapYear = false;
        }
    }

    void CurrentTime()
    {
        GameControl.control.Time.CurrentTime = GameControl.control.Time.Hours.ToString("00") + ":" + GameControl.control.Time.Miniutes.ToString("00");
        FullDate();
    }

    void CurrentDate()
    {
        GameControl.control.Time.TodaysDate = GameControl.control.Time.Day.ToString("00") + "" + "/" + GameControl.control.Time.Month.ToString("00") + "/" + GameControl.control.Time.Year.ToString("0000");
    }

    void FullDate()
    {
        GameControl.control.Time.FullDate = GameControl.control.Time.TodaysDate + " " + GameControl.control.Time.CurrentTime;
    }
}