using UnityEngine;
using System.Collections;

public class RezPrompt : MonoBehaviour
{
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public string ErrorMsg;
	public string ErrorTitle;

	public AudioSource AS;

	public bool show;

	public bool playsound;

	public bool Game;

	public bool Restart;

	public int SoundSelect;

	private Computer com;
	private Defalt def;
	private SoundControl sc;

	private Rect CloseButton;

	// Use this for initialization
	void Start () 
	{
		com = GetComponent<Computer>();
		def = GetComponent<Defalt>();
		sc = GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		CloseButton = new Rect (375, 5, 21, 21);
	}

	// Update is called once per frame
	void Update () 
	{
		if (playsound == true)
		{
			playsound = false;
			sc.SoundSelect = SoundSelect;
			sc.PlaySound();
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if (Game == true)
		{
			GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
		}

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
		GUI.Box (new Rect (5, 5, 370, 21), ErrorTitle);

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [0])) 
			{
				Restart = false;
			}
		} 
		else
		{
			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
			if (GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [1])) 
			{
				Restart = false;
			}
		}

		GUI.TextArea((new Rect (5, 30, 385, 90)),ErrorMsg);

		if (Restart == false) 
		{
			if(GUI.Button(new Rect(150, 125, 50, 20),"Ok"))
			{
			}
		} 
		else
		{
			if(GUI.Button(new Rect(100, 125, 50, 20),"Restart Now"))
			{
				GameControl.control.GatewayStatus.Booted = false;
				//Application.LoadLevel (1);
			}

			if(GUI.Button(new Rect(200, 125, 50, 20),"Cancel"))
			{
				Restart = false;
			}
		}
	}
}
