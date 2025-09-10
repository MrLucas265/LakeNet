using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateSystem
{
    public float Seconds;
    public float Miniutes;
    public float Hours;
    public float TwelveHours;
    public float TimeMulti;
    public int Day;
    public int Month;
    public int Year;
    public int LeapYearCount;
    public string MonthName;
    public bool IsLeapYear;
    public string DayName;
    public int DayNumber;
    public int EndDay;
    public int StartDay;
    public bool USADate;
    public string TodaysDate;
    public string CurrentTime;
    public string CurrentTwTime;
    public string FullDate;
    public bool AM;
    public bool SetTime;


    public DateSystem(float seconds,float miniutes, float hours,float twelvehours,int day,int month,int year,int leapyearcount, string monthname,bool isleapyear,string dayname,int daynumber,int endday,int startday,bool usadate,string todaysdate,string currenttime, string currenttwtime, string fulldate,bool am)
    {
        Seconds = seconds;
        Miniutes = miniutes;
        Hours = hours;
        TwelveHours = twelvehours;
        Day = day;
        Month = month;
        Year = year;
        LeapYearCount = leapyearcount;
        MonthName = monthname;
        IsLeapYear = isleapyear;
        DayName = dayname;
        DayNumber = daynumber;
        EndDay = endday;
        StartDay = startday;
        USADate = usadate;
        TodaysDate = todaysdate;
        CurrentTime = currenttime;
        CurrentTwTime = currenttwtime;
        FullDate = fulldate;
        AM = am;
    }
}