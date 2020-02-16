using UnityEngine;
using System.Collections;

public class LoginBackground : MonoBehaviour 
{
	public Texture2D pic1;
	public Texture2D pic2;
	public Texture2D pic3;
	public Texture2D pic4;

	public float pic1x;
	public float pic1y;
	public float pic1w;
	public float pic1h;

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI()
	{
		//GameControl.control.windowx[windowID] = windowRect.x;
		//GameControl.control.windowy[windowID] = windowRect.y;
		//GUI.skin = com.Skin[GameControl.control.GUIID];
		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;

		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		GUI.depth = 100;
//		GUI.DrawTexture (new Rect (0, 0, 2000, 2000), pic3);
//		GUI.DrawTexture (new Rect (pic1x, pic1y, pic1w, pic1h), pic4);
//		GUI.DrawTexture (new Rect (0, 930, 2000, 150), pic2);
//      GUI.DrawTexture (new Rect (0, 0, 2000, 150), pic1);
	}
}
