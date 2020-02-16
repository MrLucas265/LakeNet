//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class VoiceCMDS : MonoBehaviour {
//	private DictationScript dcs;
//	private AppMan appman;
//	public string[] InputArray;
//	public string ModdedText;
//	public string ResultText;
//	// Use this for initialization
//	void Start () 
//	{
//		dcs = GetComponent<DictationScript>();
//		appman = GetComponent<AppMan>();
//	}

//	void Update()
//	{
//		ResultText = dcs.ResultText;
//		InputArray = ResultText.Split(' ');
//		if (InputArray.Length > 0)
//		{
//			if (InputArray[0] == "open" || InputArray[0] == "close" || InputArray[0] == "run" )
//			{
//				ProgramExecute();
//			}
//		}
//	}

//	void ProgramExecute()
//	{
//		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
//		{
//			ModdedText = GameControl.control.ProgramFiles[i].Name.ToLower();
//			if (ModdedText == InputArray[1]) 
//			{
//				appman.enabled = true;
//				appman.SelectedApp = GameControl.control.ProgramFiles[i].Target;
//				dcs.ResultText = "";
//			}
//		}
//	}
//}

////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class VoiceCMDS : MonoBehaviour {
////	private DictationScript dcs;
////	private AppMan appman;
////	public string[] InputArray;
////	public string ModdedText;
////	public string ResultText;
////	public int currentword;
////	public string totalword;
////	public bool wordCheck;
////	// Use this for initialization
////	void Start () 
////	{
////		dcs = GetComponent<DictationScript>();
////		appman = GetComponent<AppMan>();
////	}

////	void Update()
////	{
////		ResultText = dcs.ResultText;
////		InputArray = ResultText.Split(' ');
////		if (InputArray.Length > 0)
////		{
////			if (InputArray[0] == "open" || InputArray[0] == "close" || InputArray[0] == "run" )
////			{
////				int wordcount = InputArray.Length;
////				if (wordcount > 0)
////				{
////					if (currentword < wordcount)
////					{
////						currentword++;
////					}
////					if (currentword > 0 && currentword < wordcount)
////					{
////						totalword += InputArray[currentword] + " ";
////					}
////				}
////				if (currentword == wordcount)
////				{
////					ProgramExecute();
////					wordCheck = false;
////				}
////			}
////		}
////	}

////	public void ResetWords()
////	{
////		dcs.ResultText = "";
////		InputArray[0] = "";
////		currentword = 0;
////		totalword = "";
////	}

////	void ProgramExecute()
////	{
////		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
////		{
////			ModdedText = GameControl.control.ProgramFiles[i].Name.ToLower();
////			if (ModdedText == totalword) 
////			{
////				totalword = "";
////				appman.enabled = true;
////				appman.SelectedApp = GameControl.control.ProgramFiles[i].Target;
////				currentword = 0;
////			}
////		}
////	}
////}

