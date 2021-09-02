using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCheck : MonoBehaviour 
{


	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool show;
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;


	public GUISkin POSTSkin;
	public GUISkin BIOSSkin;

	public List<string> BootInfo = new List<string>();
	public List<ProgramSystem> BootableOS = new List<ProgramSystem>();

	private Boot boot;
	private OS os;

	public bool ChangeOS;

	public string SelectedOS;


	// Use this for initialization
	void Start ()
	{
		boot = GetComponent<Boot>();
		os = GetComponent<OS>();

		windowRect = new Rect(0, 0, Customize.cust.RezX, Customize.cust.RezY);

		if (Application.isEditor == true) 
		{
			windowRect.width = Screen.width;
			windowRect.height = Screen.height;
		} 
		else 
		{
			windowRect.width = Customize.cust.RezX;
			windowRect.height = Customize.cust.RezY;
		}
	}

	void OnGUI()
	{
		GUI.depth = -30;
		GUI.skin = POSTSkin;
			
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if(show == true)
		{
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}
	}

	void DoMyWindow(int WindowID)
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.OS)
			{
				if(!BootableOS.Contains(GameControl.control.ProgramFiles[i]))
				{
					//if(GameControl.control.ProgramFiles[i].Location != "Reserved")
					//{
					//	BootableOS.Add(GameControl.control.ProgramFiles[i]);
					//}
					BootableOS.Add(GameControl.control.ProgramFiles[i]);
				}
			}
		}

		if (ChangeOS == true)
		{
			if (BootableOS.Count <= 0)
			{
				if (GameControl.control.SelectedOS.Name == OperatingSystems.OSName.SafeMode)
				{
					boot.enabled = true;
					this.enabled = false;
					GameControl.control.Gateway.Status.Terminal = true;
					ChangeOS = false;
					show = false;
				}
				else
				{
					GUI.Label(new Rect(10, Screen.height - 25, 500, 20), "No boot device found. Press any key to restart and boot into safe mode.");

					if (Input.anyKeyDown)
					{
						GameControl.control.SelectedOS.Name = OperatingSystems.OSName.SafeMode;
						GameControl.control.Gateway.Status.Booted = false;
						GameControl.control.Gateway.Status.SafeMode = true;
						Application.LoadLevel(1);
					}
				}
			}
			else
			{
				GameControl.control.Gateway.Status.Terminal = false;

				scrollpos = GUI.BeginScrollView(new Rect(100, 100, 920, 540), scrollpos, new Rect(0, 0, 0, scrollsize * 22));
				for (scrollsize = 0; scrollsize < BootableOS.Count; scrollsize++)
				{
					if (GUI.Button(new Rect(0, scrollsize * 22, 200, 21), "" + BootableOS[scrollsize].Name))
					{
						SelectedOS = BootableOS[scrollsize].Name;
					}
				}
				GUI.EndScrollView();

				for (int i = 0; i < GameControl.control.OSName.Count; i++)
				{
					if (GameControl.control.OSName[i].Title == SelectedOS)
					{
						if(SelectedOS == "Kernal-Sanders")
						{
							GameControl.control.Gateway.Status.Terminal = true;
						}
						GameControl.control.SelectedOS = GameControl.control.OSName[i];
						GameControl.control.SelectedOS.Colour = GameControl.control.OSName[i].Colour;
						ChangeOS = false;
					}
				}
			}
		}
		else
		{
			if(BootableOS.Count <= 0)
			{
				if (GameControl.control.SelectedOS.Name == OperatingSystems.OSName.SafeMode)
				{
					boot.enabled = true;
					this.enabled = false;
					GameControl.control.Gateway.Status.Terminal = true;
					ChangeOS = false;
					show = false;
				}
				else
				{
					GUI.Label(new Rect(10, Screen.height - 25, 500, 20), "No boot device found. Press any key to restart and boot into safe mode.");

					if (Input.anyKeyDown)
					{
						GameControl.control.SelectedOS.Name = OperatingSystems.OSName.SafeMode;
						GameControl.control.Gateway.Status.Booted = false;
						Application.LoadLevel(1);
					}
				}
			}
			else
			{
				if (BootableOS.Count == 0)
				{
					this.enabled = false;
					boot.enabled = true;
					GameControl.control.Gateway.Status.Terminal = true;
				}
				else
				{
					this.enabled = false;
					boot.enabled = true;
				}
			}
		}
		scrollpos = GUI.BeginScrollView(new Rect(5, 5, 920, 540), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < BootInfo.Count; scrollsize++)
		{
			GUI.Label (new Rect (10, scrollsize * 20, 300, 21), "" + BootInfo[scrollsize]);
		}
		GUI.EndScrollView();
	}
}
