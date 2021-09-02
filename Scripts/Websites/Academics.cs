//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class Academics : MonoBehaviour 
//{
//	public int StartCount;
//	//public int DG;
//	//public int LG;
//	//public bool Check;

//	void Start()
//	{
//		fileGenPrivate ();
//		if(GameControl.control.AcaName[0] == "")
//		{
//			GameControl.control.AcaName.Add("Lucas Cullen");
//			GameControl.control.AcaName.Add("Daisy Ball");
//			GameControl.control.AcaName.Add("John Smith");
//			GameControl.control.AcaName.Add("Jackson Lake");
//			GameControl.control.AcaName.Add("Leo Anderson");
//		}
//	}

//	void Update()
//	{
//		if (GameControl.control.AcaDegree.Count < 15) 
//		{
//			fileGenPrivate ();
//		}
//	}

//	public void fileGenPrivate()
//	{
//		GameControl.control.AcaDegree.Add("Mathmatics, Class 1");
//		GameControl.control.AcaDegree.Add("Mathmatics, Class 2");
//		GameControl.control.AcaDegree.Add("Mathmatics, Class 3");
//		GameControl.control.AcaDegree.Add("Science, Class 1");
//		GameControl.control.AcaDegree.Add("Science, Class 2");
//		GameControl.control.AcaDegree.Add("Science, Class 3");
//		GameControl.control.AcaDegree.Add("Physics, Class 1");
//		GameControl.control.AcaDegree.Add("Physics, Class 2");
//		GameControl.control.AcaDegree.Add("Physics, Class 3");
//		GameControl.control.AcaDegree.Add("Technology, Class 1");
//		GameControl.control.AcaDegree.Add("Technology, Class 2");
//		GameControl.control.AcaDegree.Add("Technology, Class 3");
//		GameControl.control.AcaDegree.Add("Engineering, Class 1");
//		GameControl.control.AcaDegree.Add("Engineering, Class 2");
//		GameControl.control.AcaDegree.Add("Engineering, Class 3");
//	}
//}