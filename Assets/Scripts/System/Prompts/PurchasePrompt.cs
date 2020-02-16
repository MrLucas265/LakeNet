using UnityEngine;
using System.Collections;

public class PurchasePrompt : MonoBehaviour
{
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public string ConfirmMsg;
	public string ConfirmTitle;

	public AudioSource AS;

	public bool show;

	public bool playsound;

	private Defalt def;
	private Computer com;
	private Upgrade upg;

	public bool Bought;

	// Use this for initialization
	void Start () 
	{
		com = GetComponent<Computer>();
		def = GetComponent<Defalt>();
		upg = GetComponent<Upgrade>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
	}

	// Update is called once per frame
	void Update () 
	{
		if (playsound == true)
		{
			if (AS.isPlaying == false)
			{
				AS.PlayOneShot(AS.clip);
				playsound = false;
			}
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = com.Skin[GameControl.control.GUIID];

		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;

		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1));

		if(show == true)
		{
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
			GUI.FocusWindow(windowID);
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.DragWindow (new Rect (5, 5, 370, 21));
		GUI.Box (new Rect (5, 5, 370, 21), ConfirmTitle);
		GUI.TextArea((new Rect (5, 30, 385, 90)),ConfirmMsg);

		if(GUI.Button(new Rect(100, 125, 50, 20),"Buy"))
		{
			if(GameControl.control.Balance[GameControl.control.SelectedBank] >= upg.Cost)
			{
				GameControl.control.Balance[GameControl.control.SelectedBank] -= upg.Cost;
				Bought = true;
				playsound = true;
			}
			else
			{
				ConfirmTitle = "Error-269 Transaction Error";
				ConfirmMsg = "Transaction Could not complete due to insuffcient funds.";
			}
		}

		if(GUI.Button(new Rect(300, 125, 50, 20),"Cancel"))
		{

		}
	}
}
