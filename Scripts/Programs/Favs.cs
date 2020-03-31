using UnityEngine;
using System.Collections;

public class Favs : MonoBehaviour 
{
	public float native_width = 1920;
	public float native_height = 1080;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public int windowID;
	public Vector2 scrollpos = Vector2.zero;
	public bool Drag;
	public bool show;
	public int scrollsize;
	public int Select;
	private Computer com;
	public string WebSite;

	// Use this for initialization
	void Start ()
	{
		com = GetComponent<Computer>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];
		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		//  GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 1, 0)), new Vector3(FloatXSize, FloatYSize, 1))

		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		//now create your GUI normally, as if you were in your native resolution
		//The GUI.matrix will scale everything automatically.
		//example
//		if(show == true && hp.showAddress == true)
//		{
//			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
//		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.DragWindow(new Rect(5,5,170,21));
		GUI.Box(new Rect(5,5,170,21), "Favourites");

		if(GUI.Button(new Rect(175,5,21,21),"X"))
		{
			show = false;
		}

		if (GUI.Button(new Rect(120, 175, 80, 20), "Bookmark") && !GameControl.control.FavSites.Contains(WebSite) && WebSite!="")
		{
			GameControl.control.FavSites.Add(WebSite);
		}

		//if (GUI.Button(new Rect(5, 150, 80, 20), "Connect") && hp.show == true)
		//{
			//hp.Address = GameControl.control.FavSites[Select];
		//}

		if (GUI.Button(new Rect(95, 150, 80, 20), "Remove"))
		{
			GameControl.control.FavSites.Remove(WebSite);
			WebSite = "";
		}

		WebSite = GUI.TextField(new Rect(5, 175, 115, 20), WebSite, 30);

		if (GameControl.control.FavSites.Count > 0) 
		{
			scrollpos = GUI.BeginScrollView(new Rect(5, 30, 140, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
			for (scrollsize = 0; scrollsize < GameControl.control.FavSites.Count; scrollsize++)
			{
				if (GUI.Button(new Rect(3, scrollsize * 20, 130, 20), GameControl.control.FavSites[scrollsize]))
				{
					Select = scrollsize;
				}
			}
			GUI.EndScrollView();
		}
	}
}
