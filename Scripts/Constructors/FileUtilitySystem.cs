using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class FileUtilitySystem
{
	public string Name;
	public string DestinationPath;
	public bool Minimize;
	public bool Start;
	public bool Show;
	public bool Done;
	public float Percentage;
	public float TimeRemainSeconds;
	public float TimeRemainUISeconds;
	public float TimeRemainMin;
	public float TimeRemainHour;
    public float TimeRemainDays;
    public float ItemRemain;
	public float OurFileSize;
	public float Count;
	public float Timer;
	public float StartTime;
	public float Speed;

    public double SpeedDbl;
    public double OurFileSizeDbl;

    public ProgramType Type;
	public int RequestedPersonID;
	public int DestinationPersonID;
	public ProgramSystemv2 ProgramFile;

	public enum ProgramType
	{
		Null,
		Save,
		Upload,
		RemoteDelete,
		RemoteDelete1,
		LocalDelete,
		LocalFolderDelete,
		Download,
		Download1,
        DownloadProgram,
        Installer,
		Paste
	}

    public FileUtilitySystem(string name, string destinationpath,
		ProgramType type, ProgramSystemv2 programfile)
    {
        Name = name;
        DestinationPath = destinationpath;
        Type = type;
        ProgramFile = programfile;
    }

    public FileUtilitySystem (string name,string destinationpath,bool minimize,bool start,bool show,bool done,float percentage,float timeremainseconds,
		float timeremainuiseconds, float timeremainmin, float timeremainhour, float itemtemain, float ourfilesize,float count,float timer,
		float starttime, ProgramType type,int requestedpersonid,int destinationpersonid,ProgramSystemv2 programfile)
	{
		Name = name;
		DestinationPath = destinationpath;
		Minimize = minimize;
		Start = start;
		Show = show;
		Done = done;
		Percentage = percentage;
		TimeRemainSeconds = timeremainseconds;
		TimeRemainUISeconds = timeremainuiseconds;
		TimeRemainMin = timeremainmin;
		TimeRemainHour = timeremainhour;
		ItemRemain = itemtemain;
		OurFileSize = ourfilesize;
		Count = count;
		Timer = timer;
		StartTime = starttime;
		Type = type;
		RequestedPersonID = requestedpersonid;
		DestinationPersonID = destinationpersonid;
		ProgramFile = programfile;
	}
}