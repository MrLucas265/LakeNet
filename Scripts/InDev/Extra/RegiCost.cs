using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class RegiCost : MonoBehaviour 
{

	private GameObject ai;
	private GameObject go;
	private Computer com;
	private Defalt def;
	private SoundControl sc;

	public ErrorProm ep;

	private Rect CloseButton;

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public bool show;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Count;
	public int Cost;

	public string Ammount;

	// Use this for initialization
	void Start () 
	{
		windowID = 33;
		ai = GameObject.Find("NPCs");
		go = GameObject.Find("Computer");
		com = go.GetComponent<Computer>();
		def = go.GetComponent<Defalt>();
		sc = go.GetComponent<SoundControl>();
		ep = go.GetComponent<ErrorProm>();
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
		GUI.Box (new Rect (5, 5, 370, 21), "New York Registry");

		Ammount = GUI.TextField(new Rect (5, 60, 100, 20), Count.ToString());
		Ammount = Regex.Replace(Ammount, @"[^a-zA-Z0-9 ]", "");
		Count = int.Parse(Ammount);

		if (GUI.Button (new Rect (5, 30, 100, 20), "Calculate"))
		{
			if (Count < 10)
			{
				ep.ErrorTitle = "Registration Error";
				ep.ErrorMsg = "You need to have more then to register and calucate a cost";
			} 

			if(Count >= 10 && Count < 20)
			{
				Cost = 500 * Count;
			}
			if(Count >= 20 && Count < 30)
			{
				Cost = 450 * Count;
			}
			if(Count >= 30)
			{
				Cost = 400 * Count;
			}
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
	}


}
