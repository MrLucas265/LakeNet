using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DeleteProm : MonoBehaviour
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

	private Computer com;
	private ErrorProm ep;
	private ProfileUI pui;

	public string Select;
	public int Selected;

	// Use this for initialization
	void Start () 
	{
		com = GetComponent<Computer>();
		pui = GetComponent<ProfileUI>();
		ep = GetComponent<ErrorProm>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (playsound == true)
		{
			if (AS.isPlaying == false)
			{
				AS.PlayOneShot(AS.clip);
				playsound = false;
			}
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
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;

		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(show == true)
		{
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.DragWindow (new Rect (5, 5, 370, 21));
		GUI.Box (new Rect (5, 5, 370, 21), "Confirm Account Deletion");
		if(GUI.Button(new Rect(375, 5, 21, 21),"X"))
		{
			show = false;
			Select = "";
		}

		GUI.TextArea((new Rect (5, 30, 385, 90)),"Are you sure you want to delete " + Select);

//		if(GUI.Button(new Rect(30, 125, 50, 20),"Yes"))
//		{
//			GameControl.control.ProfileName = ProfileController.procon.Profiles[Select];
//			ProfileController.procon.Profiles.RemoveAt(Select);
//			ProfileController.procon.ProfileID.RemoveAt(Select);
//			ProfileController.procon.ProfilePassWord.RemoveAt(Select);
//			File.Delete(GameControl.control.ProfilePath + "/" + GameControl.control.ProfileName + " Fakenet Profile" +".dat");
//			Select = 0;
//			GameControl.control.ProfileName = ProfileController.procon.Profiles[Select];
//			GameControl.control.Load();
//			ProfileController.procon.Save();
//			show = false;
//		}

		if(GUI.Button(new Rect(30, 125, 50, 20),"Yes"))
		{
			if (Select == GameControl.control.ProfileName) 
			{
				ProfileController.procon.Profiles.RemoveAt(Selected);
				ProfileController.procon.ProfileID.RemoveAt(Selected);
				ProfileController.procon.ProfilePassWord.RemoveAt(Selected);
				File.Delete (GameControl.control.ProfilePath + "/" + Select + " Fakenet Profile" + ".dat");
				ProfileController.procon.Save ();
				show = false;
				Select = "";
				pui.Select = 0;
				GameControl.control.ProfileName = ProfileController.procon.Profiles[pui.Select];
				GameControl.control.Load();
			} 
			else 
			{
				ep.playsound = true;
				ep.show = true;
				ep.ErrorMsg = "Selected account does not match delete account";
				ep.ErrorTitle = "Account Error - 932";
			}
		}

		if(GUI.Button(new Rect(90, 125, 50, 20),"No"))
		{
			Select = "";
			show = false;
		}
	}
}
