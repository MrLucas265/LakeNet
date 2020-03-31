using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VMDesigner : MonoBehaviour 
{
	public GameObject SysSoftware;
	private Computer com;
	public List<Rect> windowRect = new List<Rect>();
	public List<int> ID = new List<int>();
	public List<bool> show = new List<bool>();
	public int windowID;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	private Defalt defalt;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public Rect ResizeButton;
	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	public GUISkin Skin;

	private SoundControl sc;

	public bool minimize;

	public string ProgramTitle;

	public Color32 backgroundColor = new Color32(0,0,0,0);
	public Color32 buttonColor = new Color32(0,0,0,0);
	public Color32 fontColor = new Color32(0,0,0,0);

	public int selectedID;
	public float w;
	public float h;



	//SYSTEM VARS
	bool showStartMenu = false;
	bool showAppMenu = false;

	void Start ()
	{
		SysSoftware = GameObject.Find("System");
		com = SysSoftware.GetComponent<Computer>();
		defalt = SysSoftware.GetComponent<Defalt>();
		AfterStart();
	}

	void SetColor()
	{
		GUI.backgroundColor = buttonColor;
		GUI.contentColor = fontColor;

		backgroundColor.a = 255;
		fontColor.a = 255;
		buttonColor.a = 255;
		buttonColor.g = 255;
		fontColor.g = 255;
	}

	void AfterStart()
	{
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		ProgramTitle = "Virtual Machine Designer";

		for (int i = 0; i < windowRect.Count; i++) 
		{
			ID.Add (defalt.OpenwindowID.Count + i);
			DefaltSetting = new Rect(Customize.cust.windowx[windowID],Customize.cust.windowy[windowID],357,200);
			windowRect[i] = DefaltSetting;
		}
	}

	void OnGUI()
	{
		GUI.skin = Skin;

		if (windowRect.Count > 0) 
		{
			for (int i = 0; i < windowRect.Count; i++) 
			{
				ResizeButton = new Rect (windowRect[i].width-21, windowRect[i].height-21, 21, 21);
				CloseButton = new Rect(windowRect[i].width-22,1,21,21);
				MiniButton = new Rect(CloseButton.x-21,1,21,21);

				if(show[i] == true)
				{
					GUI.color = backgroundColor;
					windowRect[i] = WindowClamp.ClampToScreen(GUI.Window(ID[i],windowRect[i],DoMyWindow,""));
				}
			}
		}
		DefaltBoxSetting  = new Rect(1,1,MiniButton.x-1,21);
	}

	void DoMyWindow(int windowID)
	{
		if (CloseButton.Contains (Event.current.mousePosition))
		{
			if (GUI.Button (new Rect (CloseButton), "X"))
			{
				show [ID.IndexOf (windowID)] = false;;
			}
		} 
		else 
		{
			SetColor();
			GUI.Button (new Rect (CloseButton), "X",Skin.customStyles[1]);
		}

		SetColor();

		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box (new Rect (DefaltBoxSetting), ProgramTitle);
		for (int i = 0; i < windowRect.Count; i++)
		{
			if (GUI.Button (new Rect (ResizeButton), "[S]")) 
			{
				System.Diagnostics.Process.Start ("notepad.exe");
				//w += Event.current.delta.x;
				//windowRect[windowID].width += Event.current.delta.y;
			}
		}
	}
}
