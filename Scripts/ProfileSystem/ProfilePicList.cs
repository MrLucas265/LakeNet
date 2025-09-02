using UnityEngine;
using System.Collections;

public class ProfilePicList : MonoBehaviour
{

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Select;

	private ProfileUI pui;

	// Use this for initialization
	void Start () 
	{
		pui = GetComponent<ProfileUI>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		//GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;

		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(pui.ShowPics == true)
		{
			windowRect = GUI.Window(2,windowRect,DoMyWindow1,"");
		}
	}

	void DoMyWindow1(int WindowID)
	{
		GUI.DragWindow (new Rect (5, 5, 100, 21));
		GUI.Box (new Rect (5, 5, 100, 21), "Profile Pics");

		scrollpos = GUI.BeginScrollView(new Rect(5, 30, 100, 160), scrollpos, new Rect(0, 0, 0, scrollsize*32));
		for (scrollsize = 0; scrollsize < GameControl.control.UserPic.Count; scrollsize++)
		{
			GUI.Label(new Rect(40, scrollsize * 32, 100, 32),pui.PicName[scrollsize]);
			if(GUI.Button(new Rect(0, scrollsize * 32, 32, 32),GameControl.control.UserPic[scrollsize]))
			{
				Select = scrollsize;
				pui.ProfilePicID = Select;
				pui.ShowPics = false;
				//ProfileController.procon.ProfileID[Select] = pui.ProfilePicID;
			}
		}
		GUI.EndScrollView();
	}
}
