using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
	public bool Trees;
	public bool Rocks;
	public bool Grass;
	public bool Water;

	public bool show;
	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	private Computer com;
	private Defalt def;
	private AppMan appman;

	private GameObject Hardware;
	private GameObject Prompts;
	private GameObject SysSoftware;
	private GameObject AppSoftware;
	private GameObject HackingSoftware;

	public Rect DefaltBoxSetting;

	public List<Texture2D> Texture = new List<Texture2D>();
	public List<Rect> TexturePos = new List<Rect>();

	public Texture2D aTree;

	void Start () 
	{
		Hardware = GameObject.Find("Hardware");
		Prompts = GameObject.Find("Prompts");
		SysSoftware = GameObject.Find("System");
		HackingSoftware = GameObject.Find("Hacking");
		AppSoftware = GameObject.Find("Applications");

		com = SysSoftware.GetComponent<Computer>();
		def = SysSoftware.GetComponent<Defalt>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		if(show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}


	void DoMyWindow(int WindowID)
	{
		GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box (new Rect (DefaltBoxSetting), "Map Maker");

		if(GUI.Button(new Rect(3,25,100,20),"Trees"))
		{
			Trees = true;
		}

		if (Input.GetMouseButton(0))
		{
			if (windowRect.Contains (Event.current.mousePosition)) 
			{
				if (Trees == true)
				{
					for (int i = 0; i < 1; i++) 
					{
						if (Event.current.mousePosition.x != TexturePos [i].x+TexturePos [i].width && Event.current.mousePosition.y != TexturePos [i].y+TexturePos [i].height) 
						{
							TexturePos.Add (new Rect (Event.current.mousePosition.x,Event.current.mousePosition.y,21,21));
							Texture.Add(aTree);
						}
					}
				}
			}
		}

		for (int i = 0; i < TexturePos.Count; i++) 
		{
			GUI.Box (TexturePos [i], Texture [i]);
		}
	}

	void Update()
	{
		
	}
}