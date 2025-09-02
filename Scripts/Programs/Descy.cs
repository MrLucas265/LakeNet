using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Descy : MonoBehaviour 
{
    public bool show;
    private Computer com;
   // private HomePage hp;
    public int windowID;
    public Rect windowRect = new Rect(100, 100, 200, 200);
    public float native_width = 1920;
    public float native_height = 1080;
    public bool Drag;
    public List<int> Cypher = new List<int>(); 
    public bool done;
    public bool running;
    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;
    public float coolDown;
    public float cd;
    public bool valid;

	// Use this for initialization
    void Start () 
    {
        com = GetComponent<Computer>();
       // hp = GetComponent<HomePage>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
        Random10();
        coolDown = 15;
    }

	
	// Update is called once per frame
	void Update () 
    {
        if(done == true)
        {
            running = false;
            Done();
        }
        if(done == false)
        {
            cd = coolDown;
        }

//        switch(hp.Address)
//        {
//            case "www.academicstudies.com/database":
//                valid = true;
//                break;
//        }
	}

    void OnGUI()
    {
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
        GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
        //set up scaling
        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        //  GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0, 1, 0)), new Vector3(FloatXSize, FloatYSize, 1))

        GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

        //now create your GUI normally, as if you were in your native resolution
        //The GUI.matrix will scale everything automatically.
        //example
        if(show == true)
        {
            windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
        }
    }

    void Random10()
    {
        int i;
        for (i = 0; i < Cypher.Count; i++)
        {
            Cypher[i] = Random.Range(0,2);
        }
    }

    void Done()
    {
//        switch(hp.Address)
//        {
//            case "www.academicstudies.com/database":
//                
//
//                hp.Cyphed[1] = true;
//                cd -= Time.deltaTime;
//                if(cd <= 0)
//                {
//                    hp.Cyphed[1] = false;
//                    Random10();
//                    done = false;
//                }
//                break;
//        }
    }

    void DoMyWindow(int WindowID)
    {
        if (Drag == false)
        {
            if (GUI.Button(new Rect(5, 5, 170, 20), "Descy"))
            {
                Drag = true;
            }
        }

        if (Drag == true)
        {
            if (GUI.Button(new Rect(5, 5, 170, 20), "Descy - Dragging"))
            {
                Drag = false;
            }
            GUI.DragWindow();
        }

        if (GUI.Button(new Rect(175, 5, 20, 20), "X"))
        {
            show = false;
        }

        if (GUI.Button(new Rect(100, 60, 100, 20), "Start Cypher"))
        {
            if(valid == true)
            {
                running = true;
            }
        }

        if(running == true)
        {
            scrollpos = GUI.BeginScrollView(new Rect(3, 60, 40, 100), scrollpos, new Rect(0, 0, scrollsize*20, 0));
            for (scrollsize = 0; scrollsize < Cypher.Count; scrollsize++)
            {
                //GUI.Button(new Rect(83, scrollsize * 20, 20, 20), "" + files.FileSize[scrollsize]);
                // if (GUI.Button(new Rect(3, scrollsize * 20, 80, 20), "" + "mi.missionName[scrollsize]"))
                if (GUI.Button(new Rect(3, scrollsize * 20, 20, 20),"" + Cypher[scrollsize]))
                {
                    Cypher[scrollsize] = 1;
                }

                if(Cypher.Contains(0))
                {

                }
                else
                {
                    Done();
                    done = true;
                }
            }
            GUI.EndScrollView();
        }
    }
}
