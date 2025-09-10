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
			windowRect = new Rect(Screen.width, Screen.height - 100, 200 * Customize.cust.UIScale, 75 * Customize.cust.UIScale);
		}
		else
		{
			windowRect = new Rect(Screen.width - 315, Screen.height, 200 * Customize.cust.UIScale, 75 * Customize.cust.UIScale);
		}
	}

	void OnGUI()
	{
		if(show == true)
		{
			GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

			ProgramCount = 0;

			for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
			{
				var pwinman = PersonController.control.People[PersonCount].Gateway;

				if (pwinman.RunningPrograms.Count > 0)
				{
					for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
					{
						if (pwinman.RunningPrograms[i].ProgramName == "StackedNotification")
						{
							if (ProgramCount > StackedNotifications.Count)
							{
								ProgramCount = 0;
							}
							else
							{
								ProgramCount++;
							}

							GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
							pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));
						}
					}
				}
			}
		}
	}

	public void AddStackNotification(string Title, string SubTitle, string Message, bool playsnd, float Timer, bool AutoDismiss)
	{
		winman.ProgramName = "StackedNotification";
		winman.windowRect = new Rect(Screen.width - 315, Screen.height, 200 * Customize.cust.UIScale, 75 * Customize.cust.UIScale);
		winman.AddProgramWindow();
		StackedNotifications.Add(new DisplayNotificationSystem(Title, SubTitle, Message, playsnd, Timer, AutoDismiss));
		GameControl.control.Notifications.Add(new NotificationSystem(Title, SubTitle, Message, PersonController.control.Global.DateTime.CurrentTime, PersonController.control.Global.DateTime.TodaysDate, NotificationSystem.NotificationType.System));
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
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == "StackedNotification")
					{
						if (pwinman.RunningPrograms[i].WID == ID)
						{
							RemoveAStackedNotfications(pwinman.RunningPrograms[i].PID);
							pwinman.RunningPrograms.RemoveAt(i);
						}
					}
				}
			}
		}

		ResetPIDs();
	}

	void ResetPIDs()
	{
		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
			{
				if (pwinman.RunningPrograms[i].ProgramName == "StackedNotification")
				{
					pwinman.RunningPrograms[i].PID = i - 1;
				}
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
		TextAreaRect = new Rect(2, 2, 196, 70);
		GUI.BringWindowToFront(WindowID);

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == "StackedNotification")
					{
						if (pwinman.RunningPrograms[i].WID == SelectedWindowID)
						{
							SelectedProgram = pwinman.RunningPrograms[i].PID;
						}

						if (WindowID == pwinman.RunningPrograms[i].WID)
						{
							pwinman.RunningPrograms[i].windowRect.y = Screen.height - 75 - pwinman.RunningPrograms[i].PID * 75;
							if (StackedNotifications.Count > 0)
							{
								for (int j = 0; j < StackedNotifications.Count; j++)
								{
									if (j == pwinman.RunningPrograms[i].PID)
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
		}

		if (Input.GetMouseButtonDown(0) && TextAreaRect.Contains(Event.current.mousePosition))
		{
			SelectedWindowID = WindowID;
			Close(SelectedWindowID);
		}
	}
}