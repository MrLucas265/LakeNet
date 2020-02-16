using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIViewer : MonoBehaviour
{
	private GameObject ai;
	private GameObject go;
	private NPCGen npcg;
	private Computer com;
	private Defalt def;
	private SoundControl sc;

	private Rect CloseButton;

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public bool show;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	// Use this for initialization
	void Start () 
	{
		ai = GameObject.Find("NPCs");
		go = GameObject.Find("Computer");
		npcg = ai.GetComponent<NPCGen>();
		com = go.GetComponent<Computer>();
		def = go.GetComponent<Defalt>();
		sc = go.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		CloseButton = new Rect (375, 5, 21, 21);
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];

		//set up scaling
		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(show == true)
		{
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.DragWindow (new Rect (5, 5, 370, 21));
		GUI.Box (new Rect (5, 5, 370, 21), "AI Viewer");

		if (npcg.Selector >= npcg.MaxNPCs-1) 
		{
			npcg.Selector = npcg.MaxNPCs-1;
		}
		if (npcg.Selector <= 0) 
		{
			npcg.Selector = 0;
		}

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				show = false;
			}
		}
		else 
		{
			GUI.backgroundColor = com.colors [Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors [Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
			{
				show = false;
			}
		}

		scrollpos = GUI.BeginScrollView(new Rect(5, 30, 200, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		scrollsize = 5;
		GUI.Label (new Rect (0, 0, 500, 500),"Name " + npcg.FullName[npcg.Selector]);
		GUI.Label (new Rect (0, 20, 500, 500),"Balance " + npcg.BankBalance[npcg.Selector]);
		GUI.Label (new Rect (0, 40, 500, 500),"Phone Number " + npcg.PhoneNumber[npcg.Selector]);
		GUI.Label (new Rect (0, 60, 500, 500),"Acc No " + npcg.AccountNumber[npcg.Selector]);
		GUI.Label (new Rect (0, 80, 500, 500),"Acc Pass " + npcg.AccountPass[npcg.Selector]);
		GUI.EndScrollView();

		if (GUI.Button (new Rect (10, 170, 50, 20), "-1"))
		{
			npcg.Selector--;
		}

		GUI.Label (new Rect (75, 170, 100, 100),"" + npcg.Selector);

		if(GUI.Button (new Rect (100, 170, 50, 20), "+1"))
		{
			npcg.Selector++;
		}
	}
}
