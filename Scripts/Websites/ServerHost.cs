using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHost : MonoBehaviour 
{
	public int MenuSelect;
	public int ButtonWidth = 200;



	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void RenderSite()
	{
		switch (MenuSelect)
		{
		case 0:

			if(GUI.Button(new Rect(5,80,ButtonWidth,20),"Bitcoin Servers"))
			{
				MenuSelect = 1;
			}

			if(GUI.Button(new Rect(5,100,ButtonWidth,20),"File Servers"))
			{
				MenuSelect = 2;
			}

			break;

		case 1:

			GUI.Label (new Rect (5, 60, ButtonWidth, 20), "Bitcoin Servers");

			if(GUI.Button(new Rect(5,80,ButtonWidth,20),"Back"))
			{
				MenuSelect = 0;
			}

			if(GUI.Button(new Rect(5,100,ButtonWidth,20),"Basic Bitcoin Server"))
			{
				//GameControl.control.Balance[GameControl.control.SelectedBank] -= 0;
			}
			break;

		case 2:

			GUI.Label (new Rect (5, 60, ButtonWidth, 20), "File Servers");

			if(GUI.Button(new Rect(5,80,ButtonWidth,20),"Back"))
			{
				MenuSelect = 0;
			}

			if(GUI.Button(new Rect(5,100,ButtonWidth,20),"Basic File Server"))
			{

			}
			break;
		}
	}
}
