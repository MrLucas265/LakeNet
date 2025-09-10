using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class NotfiPrompt : MonoBehaviour
{
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public string Notification;

	public AudioSource AS;

	public bool show;

	public bool playsound;

	private GameObject Puter;
    private GameObject NotiSnd;
    private Computer com;
	private Defalt def;
	private SoundControl sc;

	public float y;
	public float x;

	public float DisplayTime;

	public bool ShowNoti;
	public bool Moving;
	public bool MoveUp;
	public bool MoveDown;
	public bool startTimer;
	public float Timer;

	public bool ForcedMusicSetting;
	public bool ForcedMusicOption;

	// Use this for initialization
	void Start () 
	{
		Puter = GameObject.Find("System");
        NotiSnd = GameObject.Find("Notification");
        com = Puter.GetComponent<Computer>();
		def = Puter.GetComponent<Defalt>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		SetWindowPos();
	}

	void SetWindowPos()
	{
		if (Customize.cust.SideNoti == true)
		{
			windowRect = new Rect(Screen.width - x, Screen.height - 100, 200 * Customize.cust.UIScale, 50 * Customize.cust.UIScale);
		} 
		else 
		{
			windowRect = new Rect(Screen.width - 315, Screen.height - y, 200 * Customize.cust.UIScale, 50 * Customize.cust.UIScale);
		}
	}

	public void NewNotification(string Title,string SubTitle,string Message)
	{
		if (SubTitle == "OS Kernal Panic")
		{
			GameControl.control.Notifications.Add(new NotificationSystem(Title, SubTitle, Message, PersonController.control.Global.DateTime.CurrentTime, PersonController.control.Global.DateTime.TodaysDate, NotificationSystem.NotificationType.System));
			ShowNoti = false;
		}
		else
		{
			ShowNoti = true;
			Notification = SubTitle;
			if (ForcedMusicSetting == true)
			{
				if (ForcedMusicOption == true)
				{
					playsound = true;
				}
				else
				{
					playsound = false;
				}

			}
			else
			{
				playsound = Customize.cust.PlayNotiSound;
			}
			DisplayTime = 4;
			GameControl.control.Notifications.Add(new NotificationSystem(Title, SubTitle, Message, PersonController.control.Global.DateTime.CurrentTime, PersonController.control.Global.DateTime.TodaysDate, NotificationSystem.NotificationType.System));
		}
	}

	public void DisplayNotification(string Title, string SubTitle, string Message,bool playsnd,float Timer)
	{
		ShowNoti = true;
		DisplayTime = Timer;
		Notification = Message;
		playsound = playsnd;
		GameControl.control.Notifications.Add(new NotificationSystem(Title, SubTitle, Message, PersonController.control.Global.DateTime.CurrentTime, PersonController.control.Global.DateTime.TodaysDate, NotificationSystem.NotificationType.System));
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

        if (GameControl.control.LCDPage > 0 && GameControl.control.LCDPage  < 50)
        {
            LogitechGSDK.LogiLcdColorSetTitle("Notification", 0, 255, 0);
            LogitechGSDK.LogiLcdColorSetText(0, Notification, 0, 255, 0);
        }

        SetWindowPos ();

		if(show == true)
		{
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.BringWindowToFront(windowID);
		GUI.TextArea((new Rect (2, 2, 196, 46)),Notification);

		if (ShowNoti == true) 
		{
			Moving = true;
			MoveUp = true;
			ShowNoti = false;
		}

        if(MoveUp && MoveDown == true)
        {
            MoveDown = false;
        }

		if (Moving == true)
		{
			if (MoveUp == true && Customize.cust.SideNoti == false) 
			{
				if (y < 51) 
				{
					y += Time.deltaTime * 32;
				}
				if (y >= 51) 
				{
					MoveUp = false;
					startTimer = true;
				}
			}

			if (MoveUp == true && Customize.cust.SideNoti == true) 
			{
				if (x < 200) 
				{
					x += Time.deltaTime * 32;
				}
				if (x >= 200) 
				{
					MoveUp = false;
					startTimer = true;
				}
			}

			if (startTimer == true)
			{
				Timer += Time.deltaTime;

				if (playsound == true && Customize.cust.PlayNotiSound)
				{
					playsound = false;
					sc.SoundSelect = Customize.cust.SelectedNotiSound;
					sc.PlaySound();
                }

				if (Timer >= DisplayTime)
				{
					Timer = 0;
					startTimer = false;
					MoveDown = true;
				}
			}

			if (MoveDown == true && Customize.cust.SideNoti == true) 
			{
				if (x > 0) 
				{
					x -= Time.deltaTime * 32;
				}
				if (x <= 0) 
				{
					MoveDown = false;
					Moving = false;
				}
			}

			if (MoveDown == true && Customize.cust.SideNoti == false) 
			{
				if (y > 0) 
				{
					y -= Time.deltaTime * 32;
				}
				if (y <= 0) 
				{
					MoveDown = false;
					Moving = false;
				}
			}
		}
	}
}
