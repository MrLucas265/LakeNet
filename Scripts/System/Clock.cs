using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour 
{
	public float autosave;

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;

	private Computer com;

    public bool LastDay;

    private NotfiPrompt noti;
    private GameObject Prompt;

	void Start ()
	{
        Prompt = GameObject.Find("Prompts");
        noti = Prompt.GetComponent<NotfiPrompt>();

        com = GetComponent<Computer>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
        CurrentDate();
        UpdateDayNames();
        UpdateMonths();
        FullDate();
    }

    void Update()
    {
        GlobalClock();
        GameControl.control.Time.Miniutes += GameControl.control.TimeMulti * Time.deltaTime;

        if(Customize.cust.EnableAutoSave)
        {
            autosave += 1 * Time.deltaTime;
        }

        CurrentTime();

        if (GameControl.control.Time.Miniutes >= 59)
        {
            UpdateMiniutes();
        }

        if (autosave >= Customize.cust.AutoSaveTime)
        {
            Screen.fullScreen = Customize.cust.FullScreen;
            QualitySettings.antiAliasing = Customize.cust.AA;
            QualitySettings.vSyncCount = Customize.cust.VSync;
            SaveFunction();
            autosave = 0;
            //GameControl.control.Balance [GameControl.control.SelectedBank] += 1;
        }
    }

    void GlobalClock()
    {
        if (PersonController.control.People.Count > 0)
        {
            for (int i = 0; i < PersonController.control.People.Count; i++)
            {
                if (PersonController.control.People[i].Gateway.Status.On == true)
                {
                    if (PersonController.control.People[i].Gateway.Timer.TimeRemain <= 0)
                    {
                        PersonController.control.People[i].Gateway.Timer.TimeRemain = PersonController.control.People[i].Gateway.Timer.InitalTimer;
                    }
                    else
                    {
                        PersonController.control.People[i].Gateway.Timer.TimeRemain -= Time.deltaTime * GameControl.control.TimeMulti;
                    }
                }
            }
        }
    }

    void SaveFunction()
	{
		GameControl.control.Save();
		ProfileController.procon.Save();
		Customize.cust.Save();
        PersonController.control.Save();
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
                    for (int j = 0; j < GameControl.control.BankData.Count; j++)
                    {
                        for (int k = 0; k < GameControl.control.BankData[j].Accounts.Count; k++)
                        {
                            if (GameControl.control.BankData[j].Accounts[k].Primary == true)
                            {
                                GameControl.control.BankData[j].Accounts[k].AccountBalance -= GameControl.control.Plans[i].Price;
                            }
                        }
                    }

                    GameControl.control.EmailData.Add(new EmailSystem("Monthly Plan",
    GameControl.control.Plans[i].Company, GameControl.control.Time.FullDate,
    GameControl.control.Plans[i].Name + " " + GameControl.control.Plans[i].Price + "Has been redacted from your account.", null, 0, 1, 1, false,
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

        if (GameControl.control.Time.Miniutes >= 59)
        {
            if(GameControl.control.Time.Hours == 11)
            {
                GameControl.control.Time.AM = false;
            }
            if (GameControl.control.Time.Hours == 23)
            {
                GameControl.control.Time.AM = true;
            }
            UpdateHours();
        }
    }

    void UpdateHours()
    {
        GameControl.control.Time.Hours++;
        GameControl.control.Time.Miniutes = 0;

        if (GameControl.control.Time.Hours > 12)
        {
            GameControl.control.Time.TwelveHours = GameControl.control.Time.Hours - 12;
        }
        else
        {
            GameControl.control.Time.TwelveHours = GameControl.control.Time.Hours;
        }

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
        if(GameControl.control.Time.AM == true)
        {
            GameControl.control.Time.CurrentTwTime = GameControl.control.Time.TwelveHours.ToString("00") + ":" + GameControl.control.Time.Miniutes.ToString("00") + " AM";
        }
        else
        {
            GameControl.control.Time.CurrentTwTime = GameControl.control.Time.TwelveHours.ToString("00") + ":" + GameControl.control.Time.Miniutes.ToString("00") + " PM";
        }
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