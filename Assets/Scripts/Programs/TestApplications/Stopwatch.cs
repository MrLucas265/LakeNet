using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Stopwatch : MonoBehaviour
{
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	private GameObject Puter;
	private Computer com;

	public bool show;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect ListButton;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Page;

	public bool CDPlaying;
	public bool CDReset;

	public bool SWPlaying;
	public bool SWReset;

	public float CountdownStartTime;

	public float CountdownTimer;
	public float StopwatchTimer;

	public float SWSec;
	public float SWMin;
	public float SWHour;


	void Start()
	{
		//being able to test in unity
		//if (Application.isEditor) absolutePath = "Assets/";
		Puter = GameObject.Find("System");
		com = Puter.GetComponent<Computer>();

		ListButton = new Rect (133,2,21,21);
		MiniButton = new Rect (155,2,21,21);
		CloseButton = new Rect (177,2,21,21);
	}

	void Update()
	{
		if(CDPlaying == true)
		{
			if (CountdownTimer > 0)
			{
				CountdownTimer -= Time.deltaTime * 1;
			}
			if (CountdownTimer <= 0)
			{
				CountdownTimer = 0;
				CDPlaying = false;
			}
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = com.Skin[GameControl.control.GUIID];

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	void TitleBar()
	{
		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				show = false;
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1]);
		}
	}

	void DoMyWindow(int WindowID)
	{
		TitleBar();

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow(new Rect(2,2,130,21));
		GUI.Box(new Rect(2,2,130,21), "Timers");

		int min = Mathf.FloorToInt(CountdownTimer / 60);
		int sec = Mathf.FloorToInt(CountdownTimer % 60);
		GUI.Label (new Rect (2, 50, 100, 25),"" + min.ToString("00") + ":" + sec.ToString("00"));

		if (CDPlaying == false)
		{
			if (GUI.Button (new Rect (2, 75, 40, 25),"Start")) 
			{
				if (CountdownTimer <= 0) 
				{
					CountdownTimer = CountdownStartTime;
				}
				CDPlaying = true;
			}
		} 
		else 
		{
			if (GUI.Button (new Rect (2, 75, 40, 25),"Stop")) 
			{
				CDPlaying = false;
			}
		}

		if (GUI.Button (new Rect (55, 75, 50, 25),"Reset")) 
		{
			CountdownTimer = CountdownStartTime;
		}
	}
}