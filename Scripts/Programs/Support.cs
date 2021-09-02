using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{

	private GameObject System;
	public float native_width = 1920;
	public float native_height = 1080;
	public Rect windowRect = new Rect(100, 100, 600, 400);
	public int windowID;
	public Vector2 scrollpos = Vector2.zero;
	public bool show;
	public int scrollsize;
	public int Select;

	private Computer com;
	private Defalt def;
	private AppMan appman;

	public string Title;

	public bool minimize;
	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	public string Menu;


	// Use this for initialization
	void Start()
	{
		System = GameObject.Find("System");
		com = System.GetComponent<Computer>();
		def = System.GetComponent<Defalt>();
		appman = System.GetComponent<AppMan>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		windowRect.width = 150;
		windowRect.height = 200;

		PosCheck();
		Menu = "Main";

		windowID = 49;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0)
		{
			if (Customize.cust.windowy[windowID] == 0)
			{
				Customize.cust.windowx[windowID] = Screen.width / 2;
				Customize.cust.windowy[windowID] = Screen.height / 2;
			}
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		SetPos();
	}

	void SetPos()
	{
		CloseButton = new Rect(windowRect.width - 23, 2, 21, 21);
		MiniButton = new Rect(CloseButton.x - 22, 2, 21, 21);
		DefaltSetting = new Rect(2, 2, 300, 200);
		DefaltBoxSetting = new Rect(2, 2, MiniButton.x - 2, 21);
	}

	void Minimize()
	{
		if (minimize == true)
		{
			windowRect = (new Rect(windowRect.x, windowRect.y, DefaltSetting.width, 23));
		}
		else
		{
			windowRect = (new Rect(windowRect.x, windowRect.y, DefaltSetting.width, DefaltSetting.height));
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = com.Skin[GameControl.control.GUIID];

		if (show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
			{
				//appman.SelectedApp = "Account Tracker";
				show = false;
				this.enabled = false;
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
		}

		if (MiniButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				minimize = !minimize;
				Minimize();
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				minimize = !minimize;
				Minimize();
			}
		}

		if (Menu != "Main")
		{
			DefaltBoxSetting = new Rect(2 + 24, 2, MiniButton.x - 24 - 3, 21);
			if (GUI.Button(new Rect(2, 2, DefaltBoxSetting.x - 3, 21), "<", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{

			}
		}
		else
		{
			DefaltBoxSetting = new Rect(2, 2, MiniButton.x - 3, 21);
		}

		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting), Title);
	}

	void HardwareInstallationMenu()
	{
		
	}

	void SoftwareInstallationMenu()
	{

	}

	void FactoryReset()
	{

	}

	void FAQ()
	{

	}

	void Background()
	{
		GUI.color = Color.black;
		GUI.Box(new Rect(0, 0, windowRect.width, windowRect.height), "");
		GUI.color = Color.white;
	}
}
