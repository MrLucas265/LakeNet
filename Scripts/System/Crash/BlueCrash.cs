using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCrash : MonoBehaviour 
{
	public string StopCodeNumber;
	public string StopCodeWord;
	public string CodeDetail;
	public string ExtraDetail;
	public float Timer;
	public Rect windowRect;
	public int windowID;
	public GUISkin crashskin;

	public Color32 Color1 = new Color32(0,0,0,0);

    // Use this for initialization
    void Start () 
	{
        windowRect.width = Screen.width;
		windowRect.height = Screen.height;
		LoadPresetColors ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Timers()
	{
		if (Timer > 0)
		{
			Timer -= 1 * Time.deltaTime;
		}

		if (Timer <= 0)
		{
			GameControl.control.Booted = false;
			Application.LoadLevel(1);
		}
	}

	void LoadPresetColors()
	{
		Color1.r = 0;
		Color1.g = 0;
		Color1.b = 255;
		Color1.a = 255;
	}

	void OnGUI()
	{
		//GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "");
		GUI.skin = crashskin;
		//GUI.color = Color1;
		GUI.backgroundColor = Color1;
		windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
	}

	void DoMyWindow(int WindowID)
	{
		GUI.backgroundColor = Color1;
		GUI.contentColor = Color.white;
		GUI.Box (new Rect (0, 0, windowRect.width,windowRect.height), "");
		GUI.Label (new Rect (0, 50,500,22),"The Gateway System has detected a fault and " + GameControl.control.SelectedOS.Name + " has shutdown to prevent furthur damage.");
		GUI.Label (new Rect (0, 75,500,22),"STOP: " + StopCodeNumber);
		GUI.Label (new Rect (0, 100,500,22),"STOP_CODE: " + StopCodeWord);
		GUI.Label (new Rect (0, 125,500,22),"Error: " + CodeDetail);
		GUI.Label (new Rect (0, 150,500,22),"Information: " + ExtraDetail);
		GUI.Label (new Rect (0, 175,500,22),"Automatic Restart in " + Timer.ToString("F0"));
	}
}
