using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
	
	public List<Color> Colors = new List<Color>();
	public Color32 rgb1 = new Color32(0,0,0,0);
	public Color32 rgb2 = new Color32(0,0,0,0);
	public Color32 rgb3 = new Color32(0,0,0,0);

	// Use this for initialization
	void Start ()
	{
		LoadPresetColors();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void LoadPresetColors()
	{
		rgb1.r = 0;
		rgb1.g = 0;
		rgb1.b = 0;
		rgb1.a = 255;

		rgb2.r = 148;
		rgb2.g = 0;
		rgb2.b = 211;
		rgb2.a = 255;

		rgb3.r = 0;
		rgb3.g = 255;
		rgb3.b = 255;
		rgb3.a = 255;
	}


	public void RenderSite()
	{
		GUI.backgroundColor = rgb2;
		GUI.contentColor = rgb3;

		if(GUI.Button(new Rect(50,60,400,30),"COMPANY NAME: JAIL DEW"))
		{

		}  

		if(GUI.Button(new Rect(50,90,300,100),"A BRAND NEW WEBSITE AND PRODUCT"))
		{

		}  

		if(GUI.Button(new Rect(50,200,100,21),"Home"))
		{

		}  

		if(GUI.Button(new Rect(150,200,100,21),"Products"))
		{

		}  

		if(GUI.Button(new Rect(250,200,100,21),"Contact US"))
		{

		}  
			
		GUI.TextArea (new Rect (50, 230, 300, 60), "Jaildew is a mountian dew paradoy :p");
		GUI.TextArea (new Rect (350,90,100,200), "DUDE THIS PRODUCT IS EPIC I CANT WAIT FOR THIS TO COME OUT ");
	}
}
