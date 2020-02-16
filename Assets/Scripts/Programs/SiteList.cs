using UnityEngine;
using System.Collections;

public class SiteList : MonoBehaviour 
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
    private Progtive pro;

	// Use this for initialization
	void Start ()
    {
        com = GetComponent<Computer>();
        pro = GetComponent<Progtive>();
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
//        if(show == true && hp.showAddress == true)
//        {
//            windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
//        }
    }

    void DoMyWindow(int WindowID)
    {
		GUI.DragWindow(new Rect(5,5,170,21));
		GUI.Box(new Rect(5,5,170,21), "History");

		if(GUI.Button(new Rect(175,5,21,21),"X"))
        {
            show = false;
        }

        scrollpos = GUI.BeginScrollView(new Rect(5, 30, 200, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
        for (scrollsize = 0; scrollsize < GameControl.control.Sites.Count; scrollsize++)
        {
            //GUI.Button(new Rect(83, scrollsize * 20, 20, 20), "" + files.FileSize[scrollsize]);
            // if (GUI.Button(new Rect(3, scrollsize * 20, 80, 20), "" + "mi.missionName[scrollsize]"))
            if (GUI.Button(new Rect(3, scrollsize * 20, 175, 20), GameControl.control.Sites[scrollsize]))
            {
                Select = scrollsize;
                //hp.Address = GameControl.control.Sites[Select];
//                hp.UsrName[hp.SiteID] = "Admin";
//                hp.password[hp.SiteID] = pro.Password;
            }
        }
        GUI.EndScrollView();
    }
}
