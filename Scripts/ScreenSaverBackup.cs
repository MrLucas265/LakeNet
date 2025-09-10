using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenSaverBackup : MonoBehaviour
{
	public float autosave;

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);

	public float multi;

	public float Timer;
	public float CoolDown;

	public bool show;

	public bool SSEnabled;

	private Computer com;

	public GUIStyle ClockFont;

	public Texture2D ScreensaverBackGround;
	public Texture2D ScreensaverBackGroundDefalt;
	public Texture2D ScreensaverPicture;
	public Texture2D ScreensaverBackGroundBlack;
	public Texture2D ScreensaverBackGroundWhite;

	public bool Up;
	public bool Right;

	public float posX;
	public float posY;
	public float speed;

	public bool UseRealTime;

	public float MTF;
	public string MTS;

	public int ModX;
	public int ModY;

	public GameObject Desktops;

	public List<string> ScreenSaverTypes = new List<string>();

	void Start()
	{
		windowRect.width = Screen.width;
		windowRect.height = Screen.height;
		Desktops = GameObject.Find("Desktops");
		com = GetComponent<Computer>();
		multi = 1;
		Timer = CoolDown;
		AddTypes();
	}


	void Update()
	{
		CoolDown = Customize.cust.SSActiveTime;
		if (Customize.cust.ScreenSaverEnabled == false)
		{
			this.enabled = false;
		}

		if (show == false && SSEnabled == true)
		{
			Timer -= multi * Time.deltaTime;
		}

		if (Input.anyKey)
		{
			show = false;
			Desktops.SetActive(true);
			Timer = CoolDown;
		}

		if (Timer <= 0)
		{
			show = true;
			Desktops.SetActive(false);
		}

		switch (Customize.cust.ScreenSaverType)
		{
			case "Time":
				if (Right == true)
				{
					posX += speed * Time.deltaTime;
				}
				else
				{
					posX -= speed * Time.deltaTime;
				}


				if (Up == true)
				{
					posY += speed * Time.deltaTime;
				}
				else
				{
					posY -= speed * Time.deltaTime;
				}


				if (posY <= 0)
				{
					Up = true;
				}
				if (posY >= Screen.height - ModY)
				{
					Up = false;
				}

				if (posX <= 0)
				{
					Right = true;
				}
				if (posX >= Screen.width - ModX)
				{
					Right = false;
				}
				break;

			case "Picture":
				if (Right == true)
				{
					posX += speed * Time.deltaTime;
				}
				else
				{
					posX -= speed * Time.deltaTime;
				}


				if (Up == true)
				{
					posY += speed * Time.deltaTime;
				}
				else
				{
					posY -= speed * Time.deltaTime;
				}


				if (posY <= 0)
				{
					Up = true;
				}
				if (posY >= Screen.height - ScreensaverPicture.height / 4)
				{
					Up = false;
				}

				if (posX <= 0)
				{
					Right = true;
				}
				if (posX >= Screen.width - ScreensaverPicture.width / 4)
				{
					Right = false;
				}
				break;
		}
	}

	void AddTypes()
	{
		ScreenSaverTypes.Add("Blank");
		ScreenSaverTypes.Add("Time");
		ScreenSaverTypes.Add("Picture");
		//ScreenSaverTypes.Add("Blank Rainbow");
	}


	void OnGUI()
	{
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		if (show == true)
		{
			windowRect = GUI.Window(windowID, windowRect, DoMyWindow, "");
		}
	}

	void DoMyWindow(int WindowID)
	{
		switch (Customize.cust.ScreenSaverType)
		{
			case "Blank":
				DisplayBlank();
				break;
			case "Time":
				DisplayTime();
				break;
			case "Picture":
				DisplayPicture();
				break;
			case "Blank Rainbow":
				break;
		}
	}

	void DisplayBlankRainbow()
	{

	}

	void DisplayBlank()
	{
		if (Customize.cust.CustomTexFileNames[5] == "")
		{
			GUI.DrawTexture(new Rect(0, 0, windowRect.width, windowRect.height), ScreensaverBackGroundDefalt);
		}
		else
		{
			GUI.DrawTexture(new Rect(0, 0, windowRect.width, windowRect.height), ScreensaverBackGround);
		}
	}

	void DisplayPicture()
	{
		ModX = 0;
		ModY = 0;

		if (Customize.cust.CustomTexFileNames[5] == "")
		{
			GUI.DrawTexture(new Rect(0, 0, windowRect.width, windowRect.height), ScreensaverBackGroundDefalt);
		}
		else
		{
			GUI.DrawTexture(new Rect(0, 0, windowRect.width, windowRect.height), ScreensaverBackGround);
		}

		GUI.Label(new Rect(posX, posY, ScreensaverPicture.width / 4, ScreensaverPicture.height / 4), ScreensaverPicture);
	}

	void DisplayTime()
	{
		ModX = 130;
		ModY = 40;

		if (Customize.cust.CustomTexFileNames[5] == "")
		{
			GUI.DrawTexture(new Rect(0, 0, windowRect.width, windowRect.height), ScreensaverBackGroundDefalt);
		}
		else
		{
			GUI.DrawTexture(new Rect(0, 0, windowRect.width, windowRect.height), ScreensaverBackGround);
		}

		if (UseRealTime == false)
		{
			GUI.Label(new Rect(posX, posY, 200, 200), "" + PersonController.control.Global.DateTime.CurrentTime, ClockFont);
		}
		else
		{
			if (System.DateTime.Now.Hour >= 12)
			{
				MTS = " PM";
			}
			else
			{
				MTS = " AM";
			}

			if (System.DateTime.Now.Hour < 13)
			{
				GUI.Label(new Rect(posX, posY, 100, 100), "" + System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00") + MTS, ClockFont);
			}
			if (System.DateTime.Now.Hour >= 13)
			{
				MTF = System.DateTime.Now.Hour;
				MTF -= 12;
				GUI.Label(new Rect(posX, posY, 100, 100), "" + MTF.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00") + MTS, ClockFont);
			}
		}
	}
}