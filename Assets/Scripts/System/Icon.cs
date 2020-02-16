using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour 
{
	public GameObject SysSoftware;
	public bool show; 
	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	private Defalt defalt;
	private Computer com;
	private OS os;

	public Rect Pos;
	public Texture2D Pic;

	void Start ()
	{
		windowID = 50;
		SysSoftware = GameObject.Find("System");
		com = SysSoftware.GetComponent<Computer>();
		defalt = SysSoftware.GetComponent<Defalt>();
		os = SysSoftware.GetComponent<OS>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		windowRect = new Rect(500,350,200,200);
		Pos.width = 50;
		Pos.height = 50;
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];

		if(show == true)
		{
			//GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}
	}

	void DoMyWindow(int windowID)
	{
		GUI.Box(Pos, Pic);
		if (Pos.Contains (Event.current.mousePosition))
		{
			if (Input.GetMouseButton (0))
			{
				if (Pos.x > 1 && Pos.x < windowRect.width - 1) 
				{
					if (Pos.y > 1 && Pos.x < windowRect.height - 1)
					{
						Pos.x = Event.current.mousePosition.x - Pos.width / 2;
						Pos.y = Event.current.mousePosition.y - Pos.height / 2;
					}
				} 
				else
				{
					Pos.x = 2;
					Pos.y = 2;
				}
			}
		}
		//GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		//GUI.contentColor = com.colors[Customize.cust.FontColorInt];
	}
}
