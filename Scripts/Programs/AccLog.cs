using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AccLog : MonoBehaviour 
{
	private GameObject System;
    public float native_width = 1920;
    public float native_height = 1080;
    public Rect windowRect = new Rect(100, 100, 200, 200);
    public int windowID;
    public Vector2 scrollpos = Vector2.zero;
    public bool Drag;
    public bool show;
    public int scrollsize;
    public int Select;
    public string Username;
    public string Password;
    public string FNL;
    public bool ShowLogins;
    public bool ShowAquiredLogins;
    public bool ShowWallets;

    private Computer com;
	private Defalt def;
	private AppMan appman;

	public string Selected;
	public string UnSelected;

	public bool minimize;
	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	public string Menu;
	public string SelectedLogin;

	public List<LoginSystem> Logins = new List<LoginSystem>();

	public string SelectedBank;

    //private Files files;
    // Use this for initialization
    void Start () 
    {
		System = GameObject.Find("System");
		com = System.GetComponent<Computer>();
		def = System.GetComponent<Defalt>();
		appman = System.GetComponent<AppMan>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		PosCheck();
		Menu = "Main";
		SelectedLogin = "";
		SelectedBank = "";
    }

    // Update is called once per frame
    void Update () 
    {

    }

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0) 
		{
			if (Customize.cust.windowy[windowID] == 0) 
			{
				Customize.cust.windowx [windowID] = Screen.width / 2;
				Customize.cust.windowy [windowID] = Screen.height / 2;
			}
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		SetPos();
	}

	void SetPos()
	{
		CloseButton = new Rect (windowRect.width-23,2,21,21);
		MiniButton = new Rect (CloseButton.x-22,2,21,21);
		DefaltSetting = new Rect (2,2,300,200);
		DefaltBoxSetting = new Rect (2,2,MiniButton.x-2,21);
	}

	void Minimize()
	{
		if (minimize == true) 
		{
			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,23));
		}
		else
		{
			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,DefaltSetting.height));
		}
	}

    void OnGUI()
    {
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

        GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

        if(show == true)
        {
            GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
        }
    }

	public void DisplayMainMenu()
	{
		if (GUI.Button (new Rect (2, 25, 100, 21), "Bank Accounts")) 
		{
			Menu = "Bank";
		}

		if (GUI.Button (new Rect (2, 50, 100, 21), "Logins")) 
		{
			Menu = "Login";
		}

		if (GUI.Button (new Rect (2, 75, 100, 21), "Reputation")) 
		{
			Menu = "Rep";
		}
	}

	void DisplayBankAccounts()
	{
		if (GameControl.control.BankData.Count >= 1)
		{
			if (SelectedBank != "")
			{
				GUI.Button(new Rect(2, 25, 60, 20), "AccNo");
				GUI.Button(new Rect(63, 25, 120, 20), "Account IP");
				GUI.Button(new Rect(184, 25, 70, 20), "Balance");

				scrollpos = GUI.BeginScrollView(new Rect(1, 46, 295, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
				for (int i = 0; i < GameControl.control.BankData.Count; i++)
				{
					for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
					{
						if(GameControl.control.BankData[i].Accounts[j].PlayerKnown == true)
						{
							scrollsize = j;
							if (GUI.Button(new Rect(1, scrollsize * 21, 60, 20), "" + GameControl.control.BankData[i].Accounts[j].AccountNumber))
							{
								GUIUtility.systemCopyBuffer = GameControl.control.BankData[i].Accounts[j].AccountNumber;
							}

							if (GUI.Button(new Rect(62, scrollsize * 21, 120, 20), "" + GameControl.control.BankData[i].Accounts[j].AccIP))
							{
								GUIUtility.systemCopyBuffer = GameControl.control.BankData[i].Accounts[j].AccIP;
							}

							if (GUI.Button(new Rect(183, scrollsize * 21, 70, 20), "" + GameControl.control.BankData[i].Accounts[j].AccountBalance))
							{

							}

							if (GameControl.control.BankData[i].Accounts[j].Primary == true)
							{
								if (GUI.Button(new Rect(254, scrollsize * 21, 20, 20), "☒"))
								{
									GameControl.control.BankData[i].Accounts[j].Primary = false;
								}
							}
							else
							{
								if (GUI.Button(new Rect(254, scrollsize * 21, 20, 20), "☐"))
								{
									GameControl.control.BankData[i].Accounts[j].Primary = true;
								}
							}
						}
					}
				}
				GUI.EndScrollView();
			}
			else
			{
				scrollpos = GUI.BeginScrollView(new Rect(1, 25, 295, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
				for (int i = 0; i < GameControl.control.BankData.Count; i++)
				{
					for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
					{
						if(GameControl.control.BankData[i].Accounts[j].PlayerKnown)
						{
							if (GUI.Button(new Rect(1, scrollsize * 21, 200, 20), "" + GameControl.control.BankData[i].Name))
							{
								int Select = i;
								SelectedBank = GameControl.control.BankData[Select].Name;
							}
						}
					}
				}
				GUI.EndScrollView();
			}
		}
	}

	void DisplayLogins()
	{
		if (SelectedLogin != "")
		{
			GUI.Button(new Rect(2, 25, 60, 20), "User");
			GUI.Button(new Rect(63, 25, 120, 20), "Pass");

			for (int i = 0; i < GameControl.control.StoredLogins.Count; i++)
			{
				if (GameControl.control.StoredLogins[i].Name == SelectedLogin)
				{
					if(!Logins.Contains(GameControl.control.StoredLogins[i]))
					{
						Logins.Add(GameControl.control.StoredLogins[i]);
					}
				}
			}


			scrollpos = GUI.BeginScrollView(new Rect(1, 46, 295, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (int i = 0; i < Logins.Count; i++)
			{
				scrollsize = i;
				if (GUI.Button(new Rect(1, scrollsize * 21, 60, 20), "" + Logins[i].Username))
				{
					GUIUtility.systemCopyBuffer = Logins[i].Username;
				}

				if (GUI.Button(new Rect(62, scrollsize * 21, 120, 20), "" + Logins[i].Password))
				{
					GUIUtility.systemCopyBuffer = Logins[i].Password;
				}
			}
			GUI.EndScrollView();
		}
		else
		{
			for (int i = 0; i < GameControl.control.StoredLogins.Count; i++)
			{
				if (!Logins.Contains(GameControl.control.StoredLogins[i]))
				{
					Logins.Add(GameControl.control.StoredLogins[i]);
				}
			}

			scrollpos = GUI.BeginScrollView(new Rect(1, 25, 295, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (int i = 0; i < Logins.Count; i++)
			{
				scrollsize = i;
				if (GUI.Button(new Rect(1, i * 21, 200, 20), "" + Logins[scrollsize].Name))
				{
					int Select = i;
					SelectedLogin = Logins[Select].Name;
					Logins.RemoveRange(0, Logins.Count);
				}
			}
			GUI.EndScrollView();
		}
	}

	void DisplayRep()
	{
		if (GameControl.control.Rep.Count >= 1)
		{
			GUI.Button(new Rect(2,25,100, 20),"Name");
			GUI.Button(new Rect(103,25,60, 20),"Current");
			GUI.Button(new Rect(164,25,60, 20),"Next");
			GUI.Button(new Rect(225,25,60, 20),"Level");

			scrollpos = GUI.BeginScrollView(new Rect(1, 46, 295, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < GameControl.control.Rep.Count; scrollsize++)
			{
				if (GUI.Button (new Rect (1, scrollsize * 21, 100, 20), "" + GameControl.control.Rep [scrollsize].Name)) 
				{

				}

				if (GUI.Button (new Rect (102, scrollsize * 21, 60, 20),"" + GameControl.control.Rep[scrollsize].CurrentRep))
				{

				}

				if (GUI.Button (new Rect (163, scrollsize * 21, 60, 20),"" + GameControl.control.Rep[scrollsize].RepLevelRequirement)) 
				{

				}

				if (GUI.Button (new Rect (224, scrollsize * 21, 60, 20),"" + GameControl.control.Rep[scrollsize].RepLevel)) 
				{

				}
			}
			GUI.EndScrollView();
		}
	}

    void DoMyWindow(int WindowID)
    {
        if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [0])) 
			{
				appman.SelectedApp = "Account Tracker";
			}
		} 
		else
		{
			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
			GUI.Button (new Rect (CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [1]);
		}

		if (MiniButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (MiniButton), "-",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		} 
		else
		{
			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
			if (GUI.Button (new Rect (MiniButton), "-",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		}

		if (Menu != "Main") 
		{
			DefaltBoxSetting = new Rect (2 + 24, 2, MiniButton.x - 24 - 3, 21);
			if (GUI.Button (new Rect (2, 2, DefaltBoxSetting.x - 3, 21), "<",GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles [2])) 
			{
				if (SelectedLogin != "")
				{
					SelectedLogin = "";
				} 
				else
				{
					if(Menu == "Bank")
					{
						if(SelectedBank != "")
						{
							SelectedBank = "";
						}
						else
						{
							Menu = "Main";
						}
					}
					else if (Menu == "Login")
					{
						if (SelectedLogin != "")
						{
							SelectedLogin = "";
							Logins.RemoveRange(0, Logins.Count);
						}
						else
						{
							Menu = "Main";
							Logins.RemoveRange(0, Logins.Count);
						}
					}
					else
					{
						Menu = "Main";
					}
				}
			}
		} 
		else
		{
			DefaltBoxSetting = new Rect (2,2,MiniButton.x-3,21);
		}

		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting),"Account Tracker");

        if(show == true)
        {
			switch (Menu)
			{
			case "Main":
				DisplayMainMenu ();
				break;

			case "Bank":
				DisplayBankAccounts ();
				break;

			case "Login":
				DisplayLogins ();
				break;

			case "Rep":
				DisplayRep ();
				break;
			}
        }
    }
}
