using UnityEngine;
using System.Collections;

public class DirSearch : MonoBehaviour 
{
    public string FNL;
    public string Filetype;
    public string FileName;
    public string Output;
    private JailDew jd;
    private Unicom uc;

	public bool show;
	private Computer com;    
	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	// Use this for initialization
	void Start ()
	{
		com = GetComponent<Computer>();
		jd = GetComponent<JailDew>();
		uc = GetComponent<Unicom>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}


	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];

		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1));

		if(show == true)
		{
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}
	}

	void DoMyWindow(int windowID)
	{
		GUI.DragWindow(new Rect(3,3,271,21));
		GUI.Box(new Rect(3,3,271,21), "DirSearch");
		GUI.Label(new Rect(5,30,300,300), "FNL:");
		GUI.Label(new Rect(5,60,300,300), "File Name:");
		GUI.Label(new Rect(5,90,300,300), "File Type:");
		GUI.Label(new Rect(5,120,300,300), "" + Output);
		FileName = GUI.TextField(new Rect(80, 60, 120, 20), FileName, 500);
		Filetype = GUI.TextField(new Rect(80, 90, 120, 20), Filetype, 500);
			 FNL = GUI.TextField(new Rect(80, 30, 120, 20), FNL, 500);
		if(GUI.Button(new Rect(277,3,21,21),"X"))
		{
			show = false;
		}
	}
}