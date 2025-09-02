using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PasswordMan : MonoBehaviour
{
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Select;

	private ProfileUI pui;

	public string Password;

	// Use this for initialization
	void Start () 
	{
		pui = GetComponent<ProfileUI>();
		//SceneManager.LoadScene ("Game", LoadSceneMode.Additive);
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

		if(pui.ShowPass == true)
		{
			windowRect = GUI.Window(2,windowRect,DoMyWindow,"");
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.DragWindow (new Rect (5, 5, 180, 21));
		GUI.Box (new Rect (5, 5, 180, 21), "Password Manager");

		Password = GUI.TextField(new Rect(5, 40, 190, 20),Password);
		if(GUI.Button(new Rect(5, 75, 100, 20),"Set Password"))
		{
			if (Password != "") 
			{
				ProfileController.procon.ProfilePassWord.Add(Password);
				ProfileController.procon.Profiles.Add(pui.ProfileName);
				ProfileController.procon.ProfileID.Add(pui.ProfilePicID);
				Password = "";
				pui.ProfileName = "";
				pui.show = false;
				pui.ShowPass = false;
				ProfileController.procon.Save();
				Select = ProfileController.procon.Profiles.Count-1;
				GameControl.control.ProfileName = ProfileController.procon.Profiles[Select];
				GameControl.control.ProfilePicID = pui.ProfilePicID;
				Customize.cust.ProfileName = ProfileController.procon.Profiles[Select];
				Customize.cust.Load();
				Application.LoadLevel("Game");
			}
		}
	}
}
