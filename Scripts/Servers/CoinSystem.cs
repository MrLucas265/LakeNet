using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour 
{
	public List<string> BitServerName = new List<string>();

	public List<float> ServerCPUSpeed = new List<float>();
	public List<float> ServerCPUCores = new List<float>();
	public List<string> ServerCPUName = new List<string>();

	public List<float> ServerGPUSpeed = new List<float>();
	public List<string> ServerGPUName = new List<string>();

	public float CoinValue;
	public float CurrentCoins;
	public float Power;

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public bool show;

	private Computer com;

	public float cd;
	public float cooldown;
	public float Mod;

	public List<string> ConsoleList = new List<string>();
	public List<string> InputList = new List<string>();
	string input;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int LastInt;

	public int TempValue;

	// Use this for initialization
	void Start ()
	{
		Hardware();
		cooldown = 60;
		Mod = 1;
		ConsoleList.Add ("type help to get the list of commands");
		ConsoleList.Add("");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Timers();
	}

	void Coins()
	{
		Power = ServerCPUSpeed [0] * ServerCPUCores[0];
		CurrentCoins += 0.001f * Power;
	}

	void Timers()
	{
		if (cd <= 0)
		{
			cd = cooldown;
			Coins();
		}

		if (cd > 0)
		{
			cd-=Time.deltaTime*Mod;
		}
	}

	void Hardware()
	{
		switch (BitServerName[0]) 
		{
		case "Basic":
			ServerCPUCores.Add(1);
			ServerCPUSpeed.Add(2.6f);
			break;
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

//		GUI.skin = com.Skin[GameControl.control.GUIID];

		//set up scaling
		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(show == true)
		{
//			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
		}
	}

	void DoMyWindow(int WindowID)
	{
		ConsoleSystem();
	}

	void ConsoleSystem()
	{
		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return) 
		{
			TempValue = ConsoleList.Count;
			TempValue -= 1;
			ConsoleList.Add(ConsoleList[scrollsize]);
		}

		scrollpos = GUI.BeginScrollView(new Rect(2, 35, 344, 180), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < ConsoleList.Count; scrollsize++)
		{
			GUI.TextField (new Rect (2, scrollsize * 20, 325, 20), "" + ConsoleList[scrollsize]);
			if (scrollsize == TempValue) 
			{
				ConsoleList[scrollsize] = GUI.TextField (new Rect (2, scrollsize * 20 + 20, 325, 20), "" + ConsoleList[scrollsize]);
			}

			//TempValue = scrollsize * 20;
		}
		GUI.EndScrollView();
	}
}
