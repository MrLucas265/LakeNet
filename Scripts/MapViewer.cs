using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapViewer : MonoBehaviour
{
    public bool show;
    private Computer com;
    public int windowID;
    public Rect windowRect = new Rect(100, 100, 200, 200);
    public float native_width = 1920;
    public float native_height = 1080;
    public bool Drag;
    public Texture2D texture;
    public List<Vector2> Lines = new List<Vector2>();
    public bool Line;
    public string col;
    public int Index;
    public List<bool> LineBool = new List<bool>();
    public int Bounce;
    public Vector2 lastVec = new Vector2(0,0);

    // When added to an object, draws colored rays from the
    // transform position.
    public int lineCount = 100;
    public float radius = 3.0f;

    static Material lineMaterial;
    static void CreateLineMaterial ()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find ("Hidden/Internal-Colored");
            lineMaterial = new Material (shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt ("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt ("_ZWrite", 0);
        }
    }

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
        GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1));

        if(show == true)
        {
            windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
        }
    }

    void DoMyWindow(int WindowID)
    {
        GUI.DragWindow(new Rect(5,5,570,21));
        GUI.Box(new Rect(5,5,570,21), "Map Viewer");
        GUI.DrawTexture(new Rect(5,30,560,260), texture);

        if(GUI.Button(new Rect(575,5,21,21),"X"))
        {
            show = false;
        }
        //515,225
//        GUI.Box(new Rect(GameControl.control.GatewayPos.x, GameControl.control.GatewayPos.y, 21, 21), "GW");
//        if(Lines.Contains(GameControl.control.GatewayPos))
//        {
//            
//        }
//        else
//        {
//          //  Lines.Add(GameControl.control.GatewayPos);
//        }



        if(GUI.Button(new Rect(100, 50, 21, 21), "JD"))
        {
            lastVec = new Vector2(100, 50);
            //Drawing.DrawLine(LinePos[1], LinePos[1],Color.white, 5, true);
            if(LineBool[1] == false && !Lines.Contains((new Vector2(100,50))))
            {
                Lines.Add(new Vector2(100,50));
            }
            else
            {
                Lines.Remove(new Vector2(100,50));
            }
        }

        if(GUI.Button(new Rect(300, 150, 21, 21), "DS"))
        {
            lastVec = new Vector2(300, 150);
            //Drawing.DrawLine(LinePos[1], LinePos[1],Color.white, 5, true);
            if (LineBool[2] == false && !Lines.Contains((new Vector2(300,150))))
            {
                Lines.Add(new Vector2(300,150));
            }
            else
            {
                Lines.Remove(new Vector2(300,150));
            }
        }

        if(GUI.Button(new Rect(150, 150, 21, 21), "AS"))
        {
            lastVec = new Vector2(150, 150);
            //Drawing.DrawLine(LinePos[1], LinePos[1],Color.white, 5, true);
            if (LineBool[3] == false && !Lines.Contains((new Vector2(150, 150))))
            {
                Lines.Add(new Vector2(150, 150));
            }
            else
            {
                Lines.Remove(new Vector2(150, 150));
            }
        }

        if(GUI.Button(new Rect(300, 50, 21, 21), "UC"))
        {
            lastVec = new Vector2(300, 50);
            //Drawing.DrawLine(LinePos[1], LinePos[1],Color.white, 5, true);
            if (LineBool[4] == false && !Lines.Contains((new Vector2(300, 50))))
            {
                Lines.Add(new Vector2(300, 50));
            }
            else
            {
                Lines.Remove(new Vector2(300, 50));
            }
        }

//        for (Index = 0; Index < Lines.Count; Index++)
//        {
//            Drawing.DrawLine(Lines[Index], Lines[Index],Color.white, 5, true);
//        }

        if(show == true)
        {
          /*  Drawing.DrawLine(Lines[0], Lines[1],Color.white, 5, true);
            Drawing.DrawLine(Lines[1], Lines[2],Color.white, 5, true);
            Drawing.DrawLine(Lines[2], Lines[3],Color.white, 5, true);
            Drawing.DrawLine(Lines[3], Lines[4],Color.white, 5, true);
            Drawing.DrawLine(Lines[4], Lines[5],Color.white, 5, true);
            Drawing.DrawLine(Lines[5], Lines[6],Color.white, 5, true); */

            if(Lines.Count > 1)
            {
                Drawing.DrawLine(Lines[Lines.Count - 1], lastVec, Color.white, 5, true);
            }
    
            int curIdx = 0;
            for(int x = 0; x < Lines.Count; x++)
            {
                if (curIdx >= 1)
                {
                    StartCoroutine(lineLerp(5.0f, x, curIdx));
                    curIdx = 0;
                }
                curIdx++;
            }
        }
    }

    IEnumerator lineLerp(float lSpeed, int x, int curIdx)
    {
        Vector2 vecL = Vector2.Lerp(Lines[x - curIdx], Lines[x], lSpeed * Time.deltaTime);
        Drawing.DrawLine(vecL, Lines[x], Color.white, 5, true);
        yield return new WaitForSeconds(2);
    }

/*

    public void OnPostRender()
    {
        Debug.Log("renderrrr");

        CreateLineMaterial();
     //   GL.PushMatrix();

        GL.LoadOrtho();
        lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);

        GL.Vertex3(Lines[0].x, Lines[0].y, 0);
        GL.Vertex3(Lines[1].x, Lines[1].y, 0);

        GL.End();

     //   GL.PopMatrix();
   //     GL.PopMatrix();
    }*/
}
