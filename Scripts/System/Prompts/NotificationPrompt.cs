using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NotificationPrompt : MonoBehaviour
{
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;

	public AudioSource AS;

	public bool show;

	private GameObject Puter;
	private GameObject WindowHandel;

	private Computer com;
	private SoundControl sc;
	private WindowManager winman;

	public List<DisplayNotificationSystem> StackedNotifications = new List<DisplayNotificationSystem>();

	public int ProgramCount;
	public int SelectedWindowID;
	public int SelectedProgram;

	public Rect TextAreaRect;

	// Use this for initialization
	void Start()
	{
		Puter = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");

		winman = WindowHandel.GetComponent<WindowManager>();
		com = Puter.GetComponent<Computer>();
		sc = Puter.GetComponent<SoundControl>();
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		SetWindowPos();
	}

	void SetWindowPos()
	{
		if (Customize.cust.SideNoti == true)
		{
			windowRect = new Rect(Screen.width, Screen.height - 100, 200 * Customize.cust.UIScale, 50 * Customize.cust.UIScale);
		}
		else
		{
			windowRect = new Rect(Screen.width - 315, Screen.height, 200 * Customize.cust.UIScale, 50 * Customize.cust.UIScale);
		}
	}

	void OnGUI()
	{
		if(show == true)
		{
			GUI.skin = com.Skin[GameControl.control.GUIID];

			ProgramCount = 0;

			if (winman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < winman.RunningPrograms.Count; i++)
				{
					if (winman.RunningPrograms[i].ProgramName == "StackedNotification")
					{
						if (ProgramCount > StackedNotifications.Count)
						{
							ProgramCount = 0;
						}
						else
						{
							ProgramCount++;
						}

						GUI.color = com.colors[Customize.cust.WindowColorInt];
						winman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(winman.RunningPrograms[i].WID, winman.RunningPrograms[i].windowRect, DoMyWindow, ""));
					}
				}
			}
		}
	}

	public void AddStackNotification(string Title, string SubTitle, string Message, bool playsnd, float Timer, bool AutoDismiss)
	{
		winman.ProgramName = "StackedNotification";
		winman.windowRect = new Rect(Screen.width - 315, Screen.height, 200 * Customize.cust.UIScale, 50 * Customize.cust.UIScale);
		winman.AddProgramWindow();
		StackedNotifications.Add(new DisplayNotificationSystem(Title, SubTitle, Message, playsnd, Timer, AutoDismiss));
		GameControl.control.Notifications.Add(new NotificationSystem(Title, SubTitle, Message, GameControl.control.Time.CurrentTime, GameControl.control.Time.TodaysDate, NotificationSystem.NotificationType.System));
		if (playsnd == true && Customize.cust.PlayNotiSound)
		{
			PlayNotificationSound();
		}
	}

	public void RemoveAllStackedNotfications()
	{
		StackedNotifications.RemoveRange(0, StackedNotifications.Count);
	}

	public void RemoveAStackedNotfications(int ID)
	{
		StackedNotifications.RemoveAt(ID);
	}

	void Close(int ID)
	{
		if (winman.RunningPrograms.Count > 0)
		{
			for (int i = 0; i < winman.RunningPrograms.Count; i++)
			{
				if (winman.RunningPrograms[i].ProgramName == "StackedNotification")
				{
					if (winman.RunningPrograms[i].WID == ID)
					{
						RemoveAStackedNotfications(winman.RunningPrograms[i].PID);
						winman.RunningPrograms.RemoveAt(i);
					}
				}
			}
		}

		ResetPIDs();
	}

	void ResetPIDs()
	{
		for (int i = 0; i < winman.RunningPrograms.Count; i++)
		{
			if (winman.RunningPrograms[i].ProgramName == "StackedNotification")
			{
				winman.RunningPrograms[i].PID = i-1;
			}
		}
	}

	void PlayNotificationSound()
	{
		sc.SoundSelect = Customize.cust.SelectedNotiSound;
		sc.PlaySound();
	}

	void DoMyWindow(int WindowID)
	{
		TextAreaRect = new Rect(2, 2, 196, 46);
		GUI.BringWindowToFront(WindowID);

		if (winman.RunningPrograms.Count > 0)
		{
			for (int i = 0; i < winman.RunningPrograms.Count; i++)
			{
				if (winman.RunningPrograms[i].ProgramName == "StackedNotification")
				{
					if (winman.RunningPrograms[i].WID == SelectedWindowID)
					{
						SelectedProgram = winman.RunningPrograms[i].PID;
					}

					if (WindowID == winman.RunningPrograms[i].WID)
					{
						winman.RunningPrograms[i].windowRect.y = Screen.height - 50 - winman.RunningPrograms[i].PID * 50;
						if (StackedNotifications.Count > 0)
						{
							for (int j = 0; j < StackedNotifications.Count; j++)
							{
								if (j == winman.RunningPrograms[i].PID)
								{
									GUI.TextArea(TextAreaRect, StackedNotifications[j].Message);

									if (StackedNotifications[j].AutoDismiss == true)
									{
										if (StackedNotifications[j].DisplayTime <= 0)
										{
											Close(WindowID);
										}
										else
										{
											StackedNotifications[j].DisplayTime -= 1 * Time.deltaTime;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		if (Input.GetMouseButtonDown(0) && TextAreaRect.Contains(Event.current.mousePosition))
		{
			SelectedWindowID = WindowID;
			Close(SelectedWindowID);
		}
	}
}