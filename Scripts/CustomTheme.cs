using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomTheme : MonoBehaviour
{
	private GameObject System;
	public List<Texture2D> tex1 = new List<Texture2D>();
	public int Select;

	public int scrollsize;

	private Computer com;
	private ScreenSaver ss;
	private SystemPanel sp;
	private OS os;
	private Mouse mouse;

	public bool PicLoad;
	public bool Once;

	// Use this for initialization
	void Start ()
	{
		System = GameObject.Find("System");
		com = GetComponent<Computer>();
		ss = System.GetComponent<ScreenSaver>();
		sp = System.GetComponent<SystemPanel>();
		os = System.GetComponent<OS>();
		mouse = System.GetComponent<Mouse>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (tex1.Count == Customize.cust.CustomTexFileNames.Count && Once == false) 
		{
			PicLoad = true;
		}

		//if (Once == true)
		//{
		//	this.enabled = false;
		//}

		if (PicLoad == true)
		{
			LoadPics();
		}
	}

	public void UpdatePics()
	{
		if (tex1.Count >= 5)
		{
			tex1.Clear ();
			for (scrollsize = 0; scrollsize < Customize.cust.CustomTexFileNames.Count; scrollsize++) 
			{
				if(scrollsize == 4)
				{
					tex1.Add(LoadPNG(GameControl.control.SelectedOS.FPC.BackgroundAddress));
				}
				else
				{
					tex1.Add(LoadPNG(Customize.cust.CustomTexFileNames[scrollsize]));
				}
			}
		} 
		else
		{
			for (scrollsize = 0; scrollsize < Customize.cust.CustomTexFileNames.Count; scrollsize++)
			{
				if (scrollsize == 4)
				{
					tex1.Add(LoadPNG(GameControl.control.SelectedOS.FPC.BackgroundAddress));
				}
				else
				{
					tex1.Add(LoadPNG(Customize.cust.CustomTexFileNames[scrollsize]));
				}
			}
			//tex1[4] = LoadPNG(GameControl.control.SelectedOS.FPC.BackgroundAddress);
		}
	}

	public void LoadPics()
	{
		//com.Skin[10].button.normal.background = tex1[0];
		//com.Skin[10].button.hover.background = tex1[1];
		//com.Skin[10].button.active.background = tex1[2];
		//com.Skin[10].window.normal.background = tex1[3];
		//com.Skin[10].window.onNormal.background = tex1[4];
		ss.ScreensaverBackGround = tex1[5];
		ss.ScreensaverPicture = tex1[6];
		os.pic[3] = tex1[4];
		mouse.cursorImage = tex1[3];

		Once = true;
	}

	public static Texture2D LoadPNG(string filePath)
	{
		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath))   
		{
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
        return tex;
	}
}