using UnityEngine;
using System.Collections;

public class Desktop1 : MonoBehaviour 
{
	private GameObject SysSoftware;

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Select;

	public bool show;

	public bool playsound;
	public int SoundSelect;

	public Texture2D[] pic;
	public int Index;


	private Computer com;
	private SoundControl sc;

	// Use this for initialization
	void Start ()
	{
		SysSoftware = GameObject.Find("System");

		com = SysSoftware.GetComponent<Computer>();
		sc = SysSoftware.GetComponent<SoundControl>();

		windowRect.width = Screen.width;
		windowRect.height = Screen.height;
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

		if (playsound == true)
		{
			playsound = false;
			sc.SoundSelect = SoundSelect;
			sc.PlaySound();
		}

		if(show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		
	}
}
