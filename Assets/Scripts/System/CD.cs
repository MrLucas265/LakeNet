using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using System.IO;

public class CD : MonoBehaviour
{
	[DllImport("winmm.dll", EntryPoint = "mciSendString")]
	public static extern int mciSendStringA(string lpstrCommand, string lpstrReturnString, 
		int uReturnLength, int hwndCallback);

	public string driveLetter;
	public string returnString;

	public bool open;
	public bool close;

	public int Index;

	public string[] CustomDrives;
	// Use this for initialization
	void Start ()
	{
		driveLetter = CustomDrives[Index];
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (open == true)
		{
			Open();
		}

		if (close == true)
		{
			Close();
		}
	}

	void Open()
	{
		for (Index = 0; Index < CustomDrives.Length; Index++)
		{
			driveLetter = CustomDrives[Index];
			mciSendStringA("open " + CustomDrives[Index] + ": type CDaudio alias drive" + driveLetter, returnString, 0, 0);
			mciSendStringA("set drive" + CustomDrives[Index] + " door open", returnString, 0, 0);
		}
		open = false;
	}

	void Close()
	{
		for (Index = 0; Index < CustomDrives.Length; Index++)
		{
		mciSendStringA("open " + CustomDrives[Index] + ": type CDaudio alias drive" + CustomDrives[Index], returnString, 0, 0);
		mciSendStringA("set drive" + CustomDrives[Index] + " door closed", returnString, 0, 0);
		}
		close = false;
	}
}
