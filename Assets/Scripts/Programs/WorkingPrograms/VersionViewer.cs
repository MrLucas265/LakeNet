﻿using UnityEngine;
using System.Collections;

public class VersionViewer : MonoBehaviour 
{
	public bool show;
	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	public bool close;
	public bool execute;

	private Defalt defalt;
	private CLI cmd;
	private WebSec ws;
	private ErrorProm ep;
	private InternetBrowser ib;
	private CPU cpu;
	private Tracer trace;
	private Computer com;
	private VersionList pl;
	private SoundControl sc;

	private GameObject Hardware;
	private GameObject Prompts;
	private GameObject SysSoftware;
	private GameObject AppSoftware;
	private GameObject HackingSoftware;

	public float timer;
	public float startTime;

	public float percentage;
	public float StartingCount;
	public float CurrentCount;

	public string Password;
	public string CurrentWord;

	public bool Matched;

	public Rect CloseButton;
	public Rect ExecuteButton;

	public int WordCount;

	// Progtive is the one at a time sequential cracker
	// Use this for initialization
	void Start ()
	{
		Hardware = GameObject.Find ("Hardware");
		Prompts = GameObject.Find ("Prompts");
		SysSoftware = GameObject.Find ("System");
		HackingSoftware = GameObject.Find ("Hacking");
		AppSoftware = GameObject.Find ("Applications");

		ep = Prompts.GetComponent<ErrorProm>();
		com = SysSoftware.GetComponent<Computer>();
		trace = HackingSoftware.GetComponent<Tracer>();
		cmd = SysSoftware.GetComponent<CLI>();
		defalt = SysSoftware.GetComponent<Defalt>(); 
		ib = AppSoftware.GetComponent<InternetBrowser>(); 
		ws = AppSoftware.GetComponent<WebSec>();
		cpu = Hardware.GetComponent<CPU>();
		pl = SysSoftware.GetComponent<VersionList>();
		sc = SysSoftware.GetComponent<SoundControl>();

		windowRect = new Rect (100, 100, 300, 400);

		CloseButton = new Rect (windowRect.width-23, 2, 21, 21);
		ExecuteButton = new Rect (45, 100, 60, 24);

		StartingCount = pl.Words.Count;

		windowID = 99;
	}

	void Close()
	{
		show = false;
		this.enabled = false;
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

	void DoMyWindow(int WindowID)
	{

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				Close();
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1]);
		}

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow(new Rect(2, 2, windowRect.width-26, 21));
		GUI.Box (new Rect (2, 2, windowRect.width-26, 21), "Version");

		if (pl.VersionLines.Count == 0)
		{
			pl.AddPasswordsList ();
		}

		for(int i = 0; i < pl.VersionLines.Count; i++)
		{
			GUI.Label (new Rect (2, 40 + 20 * i, 300, 24), pl.VersionLines [i]);
		}
	}
}
