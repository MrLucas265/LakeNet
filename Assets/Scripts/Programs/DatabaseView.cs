using UnityEngine;
using System.Collections;

public class DatabaseView : MonoBehaviour 
{
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;
	public bool show;
	public bool Game;
	private Computer com;

	public string Name;
	public bool Search;
	public bool DisplayInfo;
	public int i;

	public string DegreeEdit;
	public string NameEdit;

	public string Test;

	// Use this for initialization
	void Start () 
	{
		com = GetComponent<Computer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Search == true)
		{
			SearchDatabase();
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if (Game == true)
		{
			GUI.skin = com.Skin[GameControl.control.GUIID];
		}

		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;

		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(show == true)
		{
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
		}
	}

	void SearchDatabase()
	{
		if (Search == true) 
		{
			int MaxRange = 0;
			MaxRange = GameControl.control.AcaName.IndexOf(Name);
			{
				for(i = 0; i < MaxRange; i++) 
				{
					
				}
			}
			if (GameControl.control.AcaName[i] == Name)
			{
				DisplayInfo = true;
				Search = false;
				NameEdit = GameControl.control.AcaName[i];
				DegreeEdit = GameControl.control.AcaDegree[i];
			}
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.DragWindow (new Rect (5, 5, 370, 21));
		GUI.Box (new Rect (5, 5, 370, 21), "Database View - Test");
		if(GUI.Button(new Rect(375, 5, 21, 21),"X"))
		{
			show = false;
		}

		if (Search == true) 
		{
			int MaxRange = 0;
			MaxRange = GameControl.control.AcaName.IndexOf(Name);
			{
				for(i = 0; i < MaxRange; i++) 
				{

				}
			}
			if (GameControl.control.AcaName[i] == Name)
			{
				DisplayInfo = true;
				Search = false;
				NameEdit = GameControl.control.AcaName[i];
				DegreeEdit = GameControl.control.AcaDegree[i];
			}
		}

		if (DisplayInfo == false)
		{
			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return) 
			{
				Search = true;
			}

			Name = GUI.TextField(new Rect(50, 40, 250, 25),Name);

			if(GUI.Button(new Rect(50, 150, 100, 20),"Search"))
			{
				Search = true;
			}
		}

		if (DisplayInfo == true) 
		{
			if(GUI.Button(new Rect(50, 150, 100, 20),"Apply Changes"))
			{
				
				GameControl.control.AcaName[i] = NameEdit;
				GameControl.control.AcaDegree[i] = DegreeEdit;
				Search = false;
				DisplayInfo = false;
				Name = "";

			}
		}

		if(GUI.Button(new Rect(200, 150, 100, 20),"Cancel"))
		{
			Search = false;
			DisplayInfo = false;
			Name = "";
		}

		if (DisplayInfo == true) 
		{
			NameEdit = GUI.TextField(new Rect(50, 40, 250, 25),NameEdit);
			DegreeEdit = GUI.TextArea(new Rect(50, 70, 250, 70),DegreeEdit);
		}
	}
}
