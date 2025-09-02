//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Threading;
//using System;

//public class ThreadPool : MonoBehaviour 
//{
//	public static List<Action> Threads = new List<Action>();

//	public static Thread NewThread;

//	public int numberOfThreads;

//	// Use this for initialization
//	void Start()
//	{
//		numberOfThreads = (SystemInfo.processorCount * 2) - 4;


//		Parallel.For(0, numberOfThreads, i =>
//		{
//			if(Threads.Count <= numberOfThreads)
//            {

//			}
//		});
//	}

//	// Update is called once per frame
//	void Update ()
//	{
		
//	}
//}
