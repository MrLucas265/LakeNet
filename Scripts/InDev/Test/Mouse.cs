using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class Mouse : MonoBehaviour
{
	[DllImport("user32.dll")]
	static extern bool SetCursorPos(int X, int Y);

	public int X;
	public int Y;


	public Texture2D cursorImage;

	public int cursorWidth;
	public int cursorHeight;

	public bool DevMode;
	public bool ShowMouse;
	private Desktop desktop;

	void Start()
	{
		desktop = GetComponent<Desktop>();
		if (Customize.cust.CustomCursorSize == false && Customize.cust.CursorSize == 0) 
		{
			Customize.cust.CursorSize = 16;
		}
		//Cursor.lockState = CursorLockMode.Confined;
		//Cursor.SetCursor (cursorImage, new Vector2 (0, 0), CursorMode.Auto);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}


	void OnGUI()
	{
		AssignMouseStuff();
	}

	void AssignMouseStuff()
	{
		if (ShowMouse == true) 
		{
			//			Cursor.lockState = CursorLockMode.None;
			//			Cursor.lockState = CursorLockMode.Confined;
			if (Customize.cust.UsingCustomCursor == true) 
			{
				Cursor.visible = false;
				GUI.depth = -100;
				if(cursorImage != null)
					GUI.DrawTexture (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, Customize.cust.CursorSize, Customize.cust.CursorSize), cursorImage);
			} 
			else 
			{
				Cursor.visible = true;
			}
		} 
		else
		{
			//			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}