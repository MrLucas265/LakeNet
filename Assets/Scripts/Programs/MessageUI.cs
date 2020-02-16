using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageUI : MonoBehaviour
{
	private GameObject Puter;

	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public List<string> Messages = new List<string>();
	public List<string> ResponseOptions = new List<string>();
	public string Inputted;

	public List<string> GroupChats = new List<string>();
	public List<string> Contacts = new List<string>();

	public AudioSource AS;

	public bool show;

	public bool playsound;

	public int SoundSelect;

	private Computer com;
	private Defalt def;
	private SoundControl sc;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	private Rect CloseButton;
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
		Test();
	}

	void Test()
	{
		Contacts.Add ("Person 1");
		Contacts.Add ("Person 2");
		Contacts.Add ("Person 3");
		GroupChats.Add ("Group 1");
		GroupChats.Add ("Group 2");
		GroupChats.Add ("Group 3");
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
		GUI.Box (new Rect (1, 1, 377, 21), "Messenger");

		scrollpos = GUI.BeginScrollView(new Rect(2, 35, 344, 180), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < Messages.Count; scrollsize++)
		{
			GUI.Label (new Rect (2, scrollsize * 20, 325, 25), "" + Messages [scrollsize]);
		}
		GUI.EndScrollView();

		Inputted = GUI.TextField(new Rect(5, 225, 340, 20), Inputted, 500);
	}
}

