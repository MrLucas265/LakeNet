using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TOA : MonoBehaviour
{

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Select;

	private ProfileUI pui;

	public List<string> TOAText = new List<string>();

	// Use this for initialization
	void Start () 
	{
		pui = GetComponent<ProfileUI>();
		ProfileController.procon.Load();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
//		GameControl.control.windowx[windowID] = windowRect.x;
//		GameControl.control.windowy[windowID] = windowRect.y;
		//GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;

		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if (ProfileController.procon.ShowTOS == true)
		{
			windowRect = GUI.Window (0, windowRect, DoMyWindow1, "");
		}
		else 
		{
            SceneManager.LoadScene("Game");
		}
	}

	void DoMyWindow1(int WindowID)
	{
		GUI.DragWindow (new Rect (5, 5, 385, 21));
		GUI.Box (new Rect (5, 5, 385, 21), "Terms Of Service");

		scrollpos = GUI.BeginScrollView(new Rect(5, 30, 380, 300), scrollpos, new Rect(0, 0, 0, scrollsize*32));
		for (scrollsize = 0; scrollsize < TOAText.Count; scrollsize++)
		{
			GUI.Label(new Rect(40, scrollsize * 32, 280, 25),TOAText[scrollsize]);
		}
		GUI.EndScrollView();

		if (GUI.Button (new Rect (5, 350, 100, 20),"Accept")) 
		{
			SceneManager.LoadScene("Game");
		}

		if (GUI.Button (new Rect (115, 350, 100, 20),"Decline")) 
		{
			Application.Quit();
		}
	}
}
