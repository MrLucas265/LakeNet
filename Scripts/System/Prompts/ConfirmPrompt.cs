using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfirmPrompt : MonoBehaviour
{
	private GameObject Puter;

	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public string ErrorMsg;
	public string ErrorTitle;

	public AudioSource AS;

	public bool show;

	public bool playsound;

	public int SoundSelect;

	private Computer com;
	private Defalt def;
	private SoundControl sc;

	private Rect CloseButton;

	public int InitalWindowID;
	void Start () 
	{
		Puter = GameObject.Find("System");
		com = Puter.GetComponent<Computer>();
		def = Puter.GetComponent<Defalt>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		CloseButton = new Rect (378, 1, 21, 21);
		windowRect = new Rect(100, 100, 400, 150);
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = com.Skin[GameControl.control.GUIID];

		if (playsound == true)
		{
			playsound = false;
			sc.SoundSelect = SoundSelect;
			sc.PlaySound();
		}

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
			GUI.FocusWindow (windowID);
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				show = false;
				enabled = false;
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

		GUI.DragWindow (new Rect (1, 1, 370, 21));
		GUI.Box (new Rect (1, 1, 377, 21), ErrorTitle);

		GUI.TextArea((new Rect (5, 30, 385, 90)),ErrorMsg);

		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return)
		{
			com.DeleteFile();
			ErrorTitle = "";
			ErrorMsg = "";
			enabled = false;
			show = false;
			GUI.FocusWindow (InitalWindowID);
		}

		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Escape || Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Backspace)
		{
			//com.DeleteFile();
			ErrorTitle = "";
			ErrorMsg = "";
			enabled = false;
			show = false;
			GUI.FocusWindow (InitalWindowID);
		}

		if(GUI.Button(new Rect(100, 125, 50, 20),"Yes"))
		{
			com.DeleteFile();
			ErrorTitle = "";
			ErrorMsg = "";
			enabled = false;
			show = false;
			GUI.FocusWindow (InitalWindowID);
		}

		if(GUI.Button(new Rect(200, 125, 50, 20),"No"))
		{
			enabled = false;
			show = false;
			GUI.FocusWindow (InitalWindowID);
		}
	}
}

