//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GlobalTimer : MonoBehaviour {
//	public bool swap;
//	// Use this for initialization
//	void Start () 
//	{
		
//	}

//	public static void Running()
//    {
//		if (PersonController.control.People.Count > 0)
//		{
//			for (int i = 0; i < PersonController.control.People.Count; i++)
//			{
//				if (PersonController.control.People[i].Gateway.Status.On == true)
//				{
//					if (PersonController.control.People[i].Gateway.Timer.TimeRemain <= 0)
//					{
//						PersonController.control.People[i].Gateway.Timer.TimeRemain = PersonController.control.People[i].Gateway.Timer.InitalTimer;
//					}
//					else
//					{
//						PersonController.control.People[i].Gateway.Timer.TimeRemain -= Time.deltaTime * PersonController.control.Global.DateTime.TimeMulti;
//					}
//				}
//			}
//		}
//	}
	
//	// Update is called once per frame
//	void Update ()
//	{
//		if(swap)
//        {
//			QThread.MakeThread(Running);
//		}
//		else
//        {
//			Running();
//		}
//	}
//}
