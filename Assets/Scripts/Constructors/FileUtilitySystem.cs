using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FileUtilitySystem
{
	public string Name;
	public string FileName;
	public string Location;
	public string Target;
	public string FileTarget;
	public string FileType;
	public string FileContent;
	public bool Minimize;
	public bool Start;
	public bool Show;
	public bool Done;
	public float FileVersion;
	public float Percentage;
	public float TimeRemainSeconds;
	public float TimeRemainUISeconds;
	public float TimeRemainMin;
	public float TimeRemainHour;
	public float ItemRemain;
	public float OurFileSize;
	public float FileSize;
	public float Count;
	public float Timer;
	public float StartTime;
	public ProgramType Type;
	//public Texture2D Icon;

	public enum ProgramType
	{
		Upload,
		RemoteDelete,
		LocalDelete,
		LocalFolderDelete,
		Download,
        DownloadProgram,
        Installer,
		Paste
	}

	public FileUtilitySystem (string name,string filename,string location,string target,string filetarget,string filecontent,string filetype,bool minimize,bool start,bool show,bool done,float fileversion,float percentage,float timeremainseconds,float timeremainuiseconds, float timeremainmin, float timeremainhour, float itemtemain, float ourfilesize, float filesize,float count,float timer,float starttime, ProgramType type) //,Texture2D icon)
	{
		Name = name;
		FileName = filename;
		Location = location;
		Target = target;
		FileTarget = filetarget;
		FileContent = filecontent;
		FileType = filetype;
		Minimize = minimize;
		Start = start;
		Show = show;
		Done = done;
		FileVersion = fileversion;
		Percentage = percentage;
		TimeRemainSeconds = timeremainseconds;
		TimeRemainUISeconds = timeremainuiseconds;
		TimeRemainMin = timeremainmin;
		TimeRemainHour = timeremainhour;
		ItemRemain = itemtemain;
		OurFileSize = ourfilesize;
		FileSize = filesize;
		Count = count;
		Timer = timer;
		StartTime = starttime;
		Type = type;
	}
}