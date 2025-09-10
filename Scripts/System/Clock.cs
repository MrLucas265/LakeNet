using UnityEngine;
using System.Collections;
using System;

public class Clock : MonoBehaviour 
{
	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;

	private Computer com;

    public bool LastDay;

    private NotfiPrompt noti;
    private GameObject Prompt;

    void Start()
    {
        Prompt = GameObject.Find("Prompts");
        noti = Prompt.GetComponent<NotfiPrompt>();

        PersonController.control.Global.Autosave.Enabled = true;

        if (PersonController.control.Global.Autosave.ResetTime == 0)
        {
            PersonController.control.Global.Autosave.ResetTime = 60;
        }

        com = GetComponent<Computer>();
        CurrentDate();
        UpdateDayNames();
        UpdateMonths();
        FullDate();
    }

    void Update()
    {
        GlobalClock();
        PersonController.control.Global.DateTime.Seconds += PersonController.control.Global.DateTime.TimeMulti * Time.deltaTime;
        PersonController.control.Global.Timer += 1 * Time.deltaTime;

        QThread.MakeThread(CurrentTime);

        if (PersonController.control.Global.Timer >= 1)
        {
            PersonController.control.Global.Timer = 0;
        }

        if (PersonController.control.Global.Autosave.Enabled == true)
        {
            PersonController.control.Global.Autosave.Timer += 1 * Time.deltaTime;
            //PersonController.control.Global.Autosave.Timer = PersonController.control.Global.Autosave.Timer + PersonController.control.Global.Timer;
        }

        if (PersonController.control.Global.DateTime.Seconds >= 60)
        {
            UpdateMiniutes();
        }

        if (PersonController.control.Global.Autosave.Timer >= PersonController.control.Global.Autosave.ResetTime)
        {
            Screen.fullScreen = Customize.cust.FullScreen;
            QualitySettings.antiAliasing = Customize.cust.AA;
            QualitySettings.vSyncCount = Customize.cust.VSync;
            QThread.MakeThread(SaveFunction);
            PersonController.control.Global.Autosave.Timer = 0;
            //SaveFunction();
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
                        QThread.MakeThread(SystemResourceManager.CalculateProgramUsage);
                        //QThread.MakeThread(HardDrives.CheckAllDrives);
                        HardDrives.CheckAllDrives();
                        FileUtilityFunc.CheckList();
                        for (int j = 0; j < PersonController.control.People[i].Gateway.RunningPrograms.Count; j++)
                        {
                            PersonController.control.People[i].Gateway.RunningPrograms[j].WPN = j;
                            //for (int j = 0; j < pwinman.RunningPrograms.Count; j++)
                            //{
                            //    if (pwinman.RunningPrograms[j].ProgramName == ProgramName)
                            //    {
                            //        pwinman.RunningPrograms[j].PID++;
                            //        pwinman.RunningPrograms[j].PID = pwinman.RunningPrograms[j].PID - 1;
                            //    }
                            //}
                        }
                        PersonController.control.People[i].Gateway.Timer.TimeRemain = PersonController.control.People[i].Gateway.Timer.InitalTimer;
                    }
                    else
                    {
                        PersonController.control.People[i].Gateway.Timer.TimeRemain -= Time.deltaTime * PersonController.control.Global.DateTime.TimeMulti;
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

    //void SaveFunction()
    //{
    //    QThread.MakeThread(GameControl.control.Save);
    //    QThread.MakeThread(ProfileController.procon.Save);
    //    QThread.MakeThread(PersonController.control.Save);
    //    QThread.MakeThread(Customize.cust.Save);
    //}

    void MonthlyStuff()
    {
        PersonController.control.Global.DateTime.StartDay = PersonController.control.Global.DateTime.DayNumber;
        UpdateMonths();
    }

    void PlanCheck()
    {
        if (GameControl.control.Plans.Count > 0)
        {
            for (int i = 0; i < GameControl.control.Plans.Count; i++)
            {
                if(GameControl.control.Plans[i].Due.Day == PersonController.control.Global.DateTime.Day)
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
    GameControl.control.Plans[i].Company, PersonController.control.Global.DateTime.FullDate,
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
        PersonController.control.Global.DateTime.Seconds = 0;
        PersonController.control.Global.DateTime.Miniutes++;

        if (PersonController.control.Global.DateTime.Miniutes > 59)
        {
            if(PersonController.control.Global.DateTime.Hours == 11)
            {
                PersonController.control.Global.DateTime.AM = false;
            }
            if (PersonController.control.Global.DateTime.Hours == 23)
            {
                PersonController.control.Global.DateTime.AM = true;
            }
            UpdateHours();
        }
    }

    void UpdateHours()
    {
        PersonController.control.Global.DateTime.Hours++;
        PersonController.control.Global.DateTime.Miniutes = 0;

        if (PersonController.control.Global.DateTime.Hours > 12)
        {
            PersonController.control.Global.DateTime.TwelveHours = PersonController.control.Global.DateTime.Hours - 12;
        }
        else
        {
            PersonController.control.Global.DateTime.TwelveHours = PersonController.control.Global.DateTime.Hours;
        }

        if (PersonController.control.Global.DateTime.Hours >= 24)
        {
            UpdateDays();
        }
    }

    void UpdateDayNames()
    {
        switch (PersonController.control.Global.DateTime.DayNumber)
        {
            case 1:
                PersonController.control.Global.DateTime.DayName = "Sunday";
                break;
            case 2:
                PersonController.control.Global.DateTime.DayName = "Monday";
                break;
            case 3:
                PersonController.control.Global.DateTime.DayName = "Tuesday";
                break;
            case 4:
                PersonController.control.Global.DateTime.DayName = "Wensday";
                break;
            case 5:
                PersonController.control.Global.DateTime.DayName = "Thursday";
                break;
            case 6:
                PersonController.control.Global.DateTime.DayName = "Friday";
                break;
            case 7:
                PersonController.control.Global.DateTime.DayName = "Saturday";
                break;
        }
    }

    void UpdateDays()
    {
        PersonController.control.Global.DateTime.Hours = 0;
        PersonController.control.Global.DateTime.Day++;
        PersonController.control.Global.DateTime.DayNumber++;

        PlanCheck();

        if (PersonController.control.Global.DateTime.DayNumber > 7)
        {
            PersonController.control.Global.DateTime.DayNumber = 1;
        }

        UpdateDayNames();

        UpdateMonths();

        CurrentDate();
    }

    void UpdateMonths()
    {
        switch (PersonController.control.Global.DateTime.Month)
        {
        case 1:
            PersonController.control.Global.DateTime.MonthName = "January";
            PersonController.control.Global.DateTime.EndDay = 31;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 2:
            PersonController.control.Global.DateTime.MonthName = "Febuary";
            if(!PersonController.control.Global.DateTime.IsLeapYear)
            {
                PersonController.control.Global.DateTime.EndDay = 28;
            }
            else
            {
                PersonController.control.Global.DateTime.EndDay = 29;
            }
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay && !PersonController.control.Global.DateTime.IsLeapYear)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay && PersonController.control.Global.DateTime.IsLeapYear)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                PersonController.control.Global.DateTime.LeapYearCount = 0;
                MonthlyStuff();

            }
            break;

        case 3:
            PersonController.control.Global.DateTime.MonthName = "March";
            PersonController.control.Global.DateTime.EndDay = 31;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 4:
            PersonController.control.Global.DateTime.MonthName = "April";
            PersonController.control.Global.DateTime.EndDay = 30;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 5:
            PersonController.control.Global.DateTime.MonthName = "May";
            PersonController.control.Global.DateTime.EndDay = 31;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 6:
            PersonController.control.Global.DateTime.MonthName = "June";
            PersonController.control.Global.DateTime.EndDay = 30;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 7:
            PersonController.control.Global.DateTime.MonthName = "July";
            PersonController.control.Global.DateTime.EndDay = 31;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 8:
            PersonController.control.Global.DateTime.MonthName = "August";
            PersonController.control.Global.DateTime.EndDay = 31;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 9:
            PersonController.control.Global.DateTime.MonthName = "September";
            PersonController.control.Global.DateTime.EndDay = 30;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 10:
            PersonController.control.Global.DateTime.MonthName = "October";
            PersonController.control.Global.DateTime.EndDay = 31;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 11:
            PersonController.control.Global.DateTime.MonthName = "Novmeber";
            PersonController.control.Global.DateTime.EndDay = 30;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month++;
                PersonController.control.Global.DateTime.Day = 1;
                MonthlyStuff();
            }
            break;

        case 12:
            PersonController.control.Global.DateTime.MonthName = "December";
            PersonController.control.Global.DateTime.EndDay = 31;
            if (PersonController.control.Global.DateTime.Day > PersonController.control.Global.DateTime.EndDay)
            {
                PersonController.control.Global.DateTime.Month = 1;
                PersonController.control.Global.DateTime.Day = 1;
                PersonController.control.Global.DateTime.Year++;
                UpdateLeapYear();
                MonthlyStuff();
            }
            break;
        }
    }

    void UpdateLeapYear()
    {
        PersonController.control.Global.DateTime.LeapYearCount++;

        if (PersonController.control.Global.DateTime.LeapYearCount == 4)
        {
            PersonController.control.Global.DateTime.IsLeapYear = true;
        }
        else
        {
            PersonController.control.Global.DateTime.IsLeapYear = false;
        }
    }

    void CurrentTime()
    {
        PersonController.control.Global.DateTime.CurrentTime = PersonController.control.Global.DateTime.Hours.ToString("00") + ":" + PersonController.control.Global.DateTime.Miniutes.ToString("00");
        if(PersonController.control.Global.DateTime.AM == true)
        {
            PersonController.control.Global.DateTime.CurrentTwTime = PersonController.control.Global.DateTime.TwelveHours.ToString("00") + ":" + PersonController.control.Global.DateTime.Miniutes.ToString("00") + " AM";
        }
        else
        {
            PersonController.control.Global.DateTime.CurrentTwTime = PersonController.control.Global.DateTime.TwelveHours.ToString("00") + ":" + PersonController.control.Global.DateTime.Miniutes.ToString("00") + " PM";
        }
        FullDate();
    }

    void CurrentDate()
    {
        PersonController.control.Global.DateTime.TodaysDate = PersonController.control.Global.DateTime.Day.ToString("00") + "" + "/" + PersonController.control.Global.DateTime.Month.ToString("00") + "/" + PersonController.control.Global.DateTime.Year.ToString("0000");
    }

    void FullDate()
    {
        PersonController.control.Global.DateTime.FullDate = PersonController.control.Global.DateTime.TodaysDate + " " + PersonController.control.Global.DateTime.CurrentTime;
    }
}