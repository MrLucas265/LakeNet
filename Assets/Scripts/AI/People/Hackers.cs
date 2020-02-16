//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Hackers : MonoBehaviour 
//{
//	public List<string> MissionListName = new List<string>();
//	public List<int> MissionRewardCash = new List<int>();
//	public List<int> MissionRewardRep = new List<int>();
//	public List<string> MissionFile = new List<string>();
//	public List<string> MissionTarget = new List<string>();
//	public List<string> MissionAddress = new List<string>();
//	public List<string> MissionType = new List<string>();
//
//	public GameObject go;
//	public GameObject db;
//	private MissionGen misgen;
//	private JailDew jd;
//	private Becas bc;
//	private Unicom uc;
//
//	public string IP;
//
//	public string SiteName;
//
//	public bool Connected;
//
//	public int Select;
//
//	public bool SetFileSizes;
//
//	public float Remaining;
//	public int Index;
//
//	public float InternetSpeed;
//	public float CPUPower;
//
//	public bool RunTimers;
//
//	public float cd;
//	public float CoolDown;
//	public float Modifier;
//
//	public float passcd;
//	public float passCoolDown;
//	public float passModifier;
//
//	public bool MissionComplete;
//
//	public bool PasswordBreaking;
//	public bool PasswordBroke;
//	public bool LogDeleting;
//
//	public bool DevCom;
//	// Use this for initialization
//	void Start () 
//	{
//		go = GameObject.Find("Missions");
//		db = GameObject.Find("Database");
//		AfterStart();
//	}
//
//	void AfterStart()
//	{
//		misgen = go.GetComponent<MissionGen>();
//		jd = db.GetComponent<JailDew>();
//		uc = db.GetComponent<Unicom>();
//		bc = db.GetComponent<Becas>();
//
//		Connected = false;
//
//		IP = "AI Test 127.0.0.1";
//	}
//	
//	// Update is called once per frame
//	void Update ()
//	{
//		if (misgen.MissionListName.Count >= 30 && MissionListName.Count <= 0) 
//		{
//			MissionSelect();
//		}
//
//		if (RunTimers == true) 
//		{
//			Timers();
//		}
//	}
//
//	void Timers()
//	{
//		CoolDown = 1 * Modifier;
//		if (cd > 0) 
//		{
//			cd -= Time.deltaTime;
//		}
//		if (cd <= 0) 
//		{
//			Remaining -= InternetSpeed;
//			cd = CoolDown;
//		}
//		if (Remaining <= 0) 
//		{
//			if (PasswordBreaking == false && LogDeleting == false) 
//			{
//				Remaining = 0;
//				MissionObjectiveCompletion();
//			}
//		}
//	}
//
//	void PasswordBreakingTimer()
//	{
//		passCoolDown = 1 * passModifier;
//		if (passcd > 0) 
//		{
//			passcd -= Time.deltaTime;
//		}
//		if (cd <= 0) 
//		{
//			passcd = CoolDown;
//			PasswordBreaking = false;
//			PasswordBroke = true;
//		}
//	}
//
//	void PasswordBreak()
//	{
//		switch (SiteName)
//		{
//		case "JailDew":
//			if (PasswordBroke == true)
//			{
//				
//			} 
//			else 
//			{
//				PasswordBreakingTimer ();
//			}
//			break;
//		}
//	}
//
//	void TraceSystem()
//	{
//
//	}
//
//	void LogDeleter()
//	{
//
//	}
//
//	void FileManager()
//	{
//		switch (MissionType [0])
//		{
//		case "JDelete":
//			if (SetFileSizes == true && PasswordBreaking == false) 
//			{
//				Index = GameControl.control.JaildewFileSystem.IndexOf (MissionFile [0]);
//				Remaining = GameControl.control.JaildewFileSystem [Index];
//				SetFileSizes = false;
//				RunTimers = true;
//			}
//			break;
//
//		case "JCopy":
//			if (SetFileSizes == true  && PasswordBreaking == false) 
//			{
//				Index = GameControl.control.JaildewFileSystem.IndexOf(MissionFile[0]);
//				Remaining = GameControl.control.JaildewFileSystem[Index];
//				SetFileSizes = false;
//				RunTimers = true;
//			}
//			break;
//
//		case "PJDelete":
//			if (SetFileSizes == true) 
//			{
//				Index = GameControl.control.JaildewFileSystem.IndexOf(MissionFile[0]);
//				Remaining = GameControl.control.JaildewFileSystem[Index];
//				SetFileSizes = false;
//				RunTimers = true;
//			}
//			break;
//
//		case "PJCopy":
//			if (SetFileSizes == true) 
//			{
//				Index = GameControl.control.JaildewFileSystem.IndexOf(MissionFile[0]);
//				Remaining = GameControl.control.JaildewFileSystem[Index];
//				SetFileSizes = false;
//				RunTimers = true;
//			}
//			break;
//
//		case "UDelete":
//			if (SetFileSizes == true  && PasswordBreaking == false) 
//			{
//				Index = GameControl.control.UniconFileName.IndexOf(MissionFile[0]);
//				Remaining = GameControl.control.UniconFileSize[Index];
//				SetFileSizes = false;
//				RunTimers = true;
//			}
//			break;
//
//		case "PUDelete":
//			if (SetFileSizes == true) 
//			{
//				Index = GameControl.control.UniconPublicFileName.IndexOf(MissionFile[0]);
//				Remaining = GameControl.control.UniconPublicFileSize[Index];
//				SetFileSizes = false;
//				RunTimers = true;
//			}
//			break;
//		}
//	}
//
//	void ConnectionSystem()
//	{
//		switch (MissionTarget[0])
//		{
//		case "www.jaildew.com":
//			SiteName = "JailDew";
//			jd.Logs.Add ("Connection " + IP + " " + ProfileController.procon.Hour + " " + ProfileController.procon.Min);
//			Connected = true;
//			SetFileSizes = true;
//			FileManager();
//			break;
//		case "www.unicom.com":
//			SiteName = "Unicom";
//			uc.Logs.Add("Connection " + IP + " " + ProfileController.procon.Hour + " " + ProfileController.procon.Min);
//			Connected = true;
//			SetFileSizes = true;
//			FileManager ();
//			break;
//		case "www.becassystems.com":
//			SiteName = "Becas";
//			//bc.Logs.Add("Connection " + IP + " " + ProfileController.procon.Hour + " " + ProfileController.procon.Min);
//			Connected = true;
//			SetFileSizes = true;
//			FileManager ();
//			break;
//		}
//	}
//
//	void MissionSelect()
//	{
//		Select = Random.Range(1, misgen.MissionListName.Count);
//		MissionComplete = false;
//		AcceptMission();
//	}
//
//	void AcceptMission()
//	{
//		MissionListName.Add(misgen.MissionListName[Select]);
//		misgen.MissionListName.Remove(misgen.MissionListName[Select]);
//
//		MissionRewardCash.Add(misgen.MissionRewardCash[Select]);
//		misgen.MissionRewardCash.Remove(misgen.MissionRewardCash[Select]);
//
//		MissionRewardRep.Add(misgen.MissionRewardRep[Select]);
//		misgen.MissionRewardRep.Remove(misgen.MissionRewardRep[Select]);
//
//		MissionAddress.Add(misgen.MissionAddress[Select]);
//		misgen.MissionAddress.Remove(misgen.MissionAddress[Select]);
//
//		MissionFile.Add(misgen.MissionFile[Select]);
//		misgen.MissionFile.Remove(misgen.MissionFile[Select]);
//
//		MissionType.Add(misgen.MissionType[Select]);
//		misgen.MissionType.Remove(misgen.MissionType[Select]);
//
//		misgen.MissionDesc.Remove(misgen.MissionDesc[Select]);
//
//		MissionTarget.Add(misgen.MissionTarget[Select]);
//		misgen.MissionTarget.Remove(misgen.MissionTarget[Select]);
//
//		if (Connected == false)
//		{
//			ConnectionSystem();
//		}
//	}
//
//	void MissionObjectiveCompletion()
//	{
//		RunTimers = false;
//
//		switch (MissionType [0])
//		{
//		case "JDelete":
//			
//			GameControl.control.JaildewFileSystem.RemoveAt (Index);
//			GameControl.control.JaildewFileSystem.RemoveAt (Index);
//
//			MissionCompletion();
//
//			break;
//
//		case "JCopy":
//			MissionCompletion();
//			break;
//
//		case "PJDelete":
//			
//			GameControl.control.JailDewPublicFileName.RemoveAt (Index);
//			GameControl.control.JailDewPublicFileSize.RemoveAt (Index);
//
//			MissionCompletion();
//
//			break;
//
//		case "PJCopy":
//			MissionCompletion();
//			break;
//
//		case "UDelete":
//			
//			GameControl.control.UniconFileName.RemoveAt (Index);
//			GameControl.control.UniconFileSize.RemoveAt (Index);
//
//			MissionCompletion();
//
//			break;
//
//		case "PUDelete":
//			
//			GameControl.control.UniconPublicFileName.RemoveAt (Index);
//			GameControl.control.UniconPublicFileSize.RemoveAt (Index);
//
//			MissionCompletion();
//
//			break;
//
//		case "AEdit":
//			MissionCompletion();
//
//			break;
//		}
//	}
//
//	void MissionCompletion()
//	{
//		MissionListName.RemoveAt (0);
//		MissionRewardCash.RemoveAt (0);
//		MissionRewardRep.RemoveAt (0);
//		MissionAddress.RemoveAt (0);
//		MissionFile.RemoveAt (0);
//		MissionType.RemoveAt (0);
//		MissionTarget.RemoveAt (0);
//
//		Connected = false;
//		MissionComplete = true;
//	}
//}
