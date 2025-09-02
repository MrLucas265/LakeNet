using UnityEngine;
using System.Collections;

public class WebSecViewer : MonoBehaviour
{
	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;
	private Computer com;
	private MonitorBypass mb;
	private WebSec ws;
	private Defalt def;
	public bool show;
	public Texture2D LockedIconL;
	public Texture2D LockedIcon;
	public Texture2D UnLockedIcon;
	// Use this for initialization
	void Start ()
	{
		com = GetComponent<Computer>();
		ws = GetComponent<WebSec>();
		mb = GetComponent<MonitorBypass>();
		def = GetComponent<Defalt>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI()
	{
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

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
		GUI.DragWindow(new Rect(5,5,170,21));
		GUI.Box(new Rect(5,5,170,21), "WebSec Viewer");

		if(GUI.Button(new Rect(175,5,21,21),"X"))
		{
		}

//		if (hp.SiteID >= 0)
//		{
//			if (ws.Monitor == true && ws.MonitorLevel <= GameControl.control.SoftwareVersion [7])
//			{
//				if (GameControl.control.MyPrograms.Contains ("Monitor Bypass"))
//				{
//					if(GUI.Button (new Rect (10, 50, 32, 32), LockedIcon))
//					{
//						mb.Active = true;
//						Debug.Log("active = true");
//					}
//				}
//			}
//			if (ws.Monitor == false && ws.MonitorLevel <= GameControl.control.SoftwareVersion [7])
//			{
//				if (GameControl.control.MyPrograms.Contains ("Monitor Bypass"))
//				{
//					if(GUI.Button (new Rect (10, 50, 32, 32), UnLockedIcon))
//					{
//						mb.Active = false;
//						Debug.Log("active = false");
//					}
//				}
//			}
//
//			if(ws.MonitorLevel > GameControl.control.SoftwareVersion [7])
//			{
//				mb.Active = false;
//				GUI.Button (new Rect (10, 50, 32, 32), LockedIconL);
//			}
//
//			GUI.Label (new Rect (7, 80, 500, 500), "Monitor " + "V" + ws.MonitorLevel);
//		} 
//		else
//		{
//			GUI.Label(new Rect (5, 80, 500, 500),"ERROR: Cant analyize secuirty");
//		}
	}
}
