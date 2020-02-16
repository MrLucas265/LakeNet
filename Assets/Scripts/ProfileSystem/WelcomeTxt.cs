using UnityEngine;
using System.Collections;

public class WelcomeTxt : MonoBehaviour 
{

	public string Welcome;

	public float pic1x;
	public float pic1y;
	public float pic1w;
	public float pic1h;

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 940;
	public float native_height = 560;
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
		GUI.color = Color.black;
		GUI.Label (new Rect (pic1x, pic1y, pic1w, pic1h), Welcome);
	}
}
