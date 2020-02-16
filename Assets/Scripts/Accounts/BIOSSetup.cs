using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIOSSetup : MonoBehaviour
{
	public List<string> DiagonisticLogs = new List<string>();

	public bool RunTimers;
	public float timer;
	public float cooldown;
	public bool AddLine;

	// Use this for initialization
	void Start ()
	{
		cooldown = 10;
		DebugLogs();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (RunTimers == true)
		{
			Timers();
		}
	}

	void DebugLogs()
	{
		
	}

	void Timers()
	{
		timer += Time.deltaTime;

		if (timer >= cooldown) 
		{
			timer = 0;
		}

		if (timer <= 1) 
		{
			DiagonisticLogs.Add ("Start boot sequence");
		}

		if (timer >= 1) 
		{
			DiagonisticLogs.Add ("boot sequence Initlized");
		}
	}

	void OnGUI()
	{
		for(int i = 0; i < DiagonisticLogs.Count; i++)
		{
			GUI.Label (new Rect (100, 20 * i, 100, 100), DiagonisticLogs [i]);
		}
	}
}
