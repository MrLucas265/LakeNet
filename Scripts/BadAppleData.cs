//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;

//public class BadAppleData : MonoBehaviour {
//	public bool TurnOn;
//	public bool run;
//	public List<string> Line = new List<string>();
//	public string CurrentLine;
//	public string[] characters;

//	public int CurrentLineID;

//	public int IndexString;

//	public int StringCurrentCount;

//	public int CurrentCharacter;

//	public float Timer;
//	public float Cooldown;
//	// Use this for initialization
//	public float Timer1;
//	public float Cooldown1;
//	public bool launched;
//	public bool launching;

//	public float Timer2;
//	public float Cooldown2;


//	void Awake()
//	{
//		GameControl.control.DesktopIconList.RemoveRange(0, GameControl.control.DesktopIconList.Count);
//	}
//	void Start () 
//	{
//		Cooldown = 0.06f;
//		Timer = Cooldown;
//		Cooldown1 = 0f;
//		Timer1 = Cooldown1;
//		Timer2 = Cooldown2;
//		Cooldown2 = 0.015f;
//	}

//	// Update is called once per frame
//	void Update () 
//	{
//		if(Timer <=0)
//		{
//			TurnOn = true;
//		}
//		else
//		{
//			Timer -= 1 * Time.deltaTime;
//		}
//		if(TurnOn == true)
//		{
//			DataListResource();
//		}

//		if(launching == true)
//		{
//			if(launched == false)
//			{
//				System.Diagnostics.Process.Start("E:/Videos/Viddly YouTube Downloader/badapple2.mp4");
//				launched = true;
//			}
//		}

//		if (Timer1 <= 0)
//		{
//			launching = true;
//		}
//		else
//		{
//			Timer1 -= 1 * Time.deltaTime;
//		}

//		if (GameControl.control.DesktopIconList.Count < 65)
//		{
//			GameControl.control.DesktopIconList.Add(new ProgramSystem("Frame", "", "", "", "", "", "", "", "", "", ProgramSystem.FileExtension.White, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, null, null));
//		}

//		if (Line.Count >= 12945)
//		{
//			TurnOn = false;
//			run = true;
//		}

//		if (run == true)
//		{
//			Running();
//		}
//	}

//    public void DataListResource()
//    {
//		TextAsset txt = (TextAsset)Resources.Load("badappledata", typeof(TextAsset));
//        Line = new List<string>(txt.text.Split('\n'));
//    }

//	public void Running()
//	{
//		CurrentLine = Line[CurrentLineID];
//		characters = new string[CurrentLine.Length];
//		for (int i = 0; i < CurrentLine.Length; i++)
//		{
//			characters[i] = System.Convert.ToString(CurrentLine[i]);
//		}

//		for (int i = 0; i < characters.Length; i++)
//		{
//			if (characters[i] == "1")
//			{
//				GameControl.control.DesktopIconList[i].Extension = ProgramSystem.FileExtension.White;
//			}
//			else
//			{
//				GameControl.control.DesktopIconList[i].Extension = ProgramSystem.FileExtension.Black;
//			}
//			if(CurrentCharacter < characters.Length)
//			{
//				CurrentCharacter++;
//			}
//			else
//			{
//				ChangeLine();
//			}
//		}
//	}

//	void ChangeLine()
//	{
//		if(Timer2 > 0)
//		{
//			Timer2 -= 1 * Time.deltaTime;
//		}
//		else
//		{
//			CurrentLineID++;
//			CurrentCharacter = 0;
//			Timer2 = Cooldown2;
//		}
//	}
//}