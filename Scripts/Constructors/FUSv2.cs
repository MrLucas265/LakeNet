using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FUSv2
{
	public string Name;
	public string Location;
	public string Target;
	public bool Minimize;
	public bool Start;
	public bool Show;
	public bool Done;
	public float Percentage;
	public float TimeRemainSeconds;
	public float TimeRemainUISeconds;
	public float TimeRemainMin;
	public float TimeRemainHour;
	public float ItemRemain;
	public float OurFileSize;
	public float Count;
	public float Timer;
	public float StartTime;
	public UtilityType Type;
	public ProgramSystem File;
	//public Texture2D Icon;

	public enum UtilityType
	{
		Upload,
		RemoteDelete,
		LocalDelete,
		LocalFolderDelete,
		Download,
		DownloadProgram,
		DownloadFile,
		Installer,
		Copy,
		Paste
	}

	public FUSv2(string name,string location,string target,bool minimize,bool start,bool show,bool done,float percentage,float timeremainseconds,float timeremainuiseconds,float timeremainmin,float timeremainhour,float itemtemain, float ourfilesize,float count,float timer,float starttime, UtilityType type,ProgramSystem file) //,Texture2D icon)
	{
		Name = name;
		Location = location;
		Target = target;
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
		File = file;
	}
}