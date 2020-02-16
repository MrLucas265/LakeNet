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

        GUI.skin = com.Skin[GameControl.control.GUIID];

        if(show == true)
        {
            GUI.color = com.colors[Customize.cust.WindowColorInt];
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
		if (GameControl.control.MyBankDetails.Count >= 1)
		{
			GUI.Button(new Rect(2,25,60, 20),"AccNo");
			GUI.Button(new Rect(63,25,120, 20),"Bank");
			GUI.Button(new Rect(184,25,70, 20),"Balance");

			scrollpos = GUI.BeginScrollView(new Rect(1, 46, 295, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
			for (scrollsize = 0; scrollsize < GameControl.control.MyBankDetails.Count; scrollsize++)
			{
				if (GUI.Button (new Rect (1, scrollsize * 21, 60, 20), "" + GameControl.control.MyBankDetails [scrollsize].AccountNumber)) 
				{

				}

				if (GUI.Button (new Rect (62, scrollsize * 21, 120, 20),"" + GameControl.control.MyBankDetails[scrollsize].BankName))
				{

				}

				if (GUI.Button (new Rect (183, scrollsize * 21, 70, 20),"" + GameControl.control.MyBankDetails[scrollsize].AccountBalance)) 
				{

				}

				if (GameControl.control.SelectedBank == scrollsize)
				{
					if (GUI.Button (new Rect (254, scrollsize * 21, 20, 20),"☒")) 
					{
						GameControl.control.SelectedBank = scrollsize;
					}
				}
				else
				{
					if (GUI.Button (new Rect (254, scrollsize * 21, 20, 20),"☐")) 
					{
						GameControl.control.SelectedBank = scrollsize;
					}
				}
			}
			GUI.EndScrollView();
		}
	}

	void DisplayLogins()
	{
		if (SelectedLogin == "") 
		{
			GUI.Button(new Rect(2,25,150, 20),"Name");

			if (GameControl.control.StoredLogins.Count >= 1)
			{
				scrollpos = GUI.BeginScrollView(new Rect(1, 46, 295, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
				for (scrollsize = 0; scrollsize < GameControl.control.StoredLogins.Count; scrollsize++)
				{
					if (GUI.Button (new Rect (1, scrollsize * 21, 150, 20), "" + GameControl.control.StoredLogins [scrollsize].Name)) 
					{
						SelectedLogin = GameControl.control.StoredLogins[scrollsize].Name;
					}
				}
				GUI.EndScrollView();
			}
		} 
		else 
		{
			GUI.Button(new Rect(2,25,125, 20),"Usr");
			GUI.Button(new Rect(128,25,125, 20),"Pass");

			Logins.RemoveRange (0, Logins.Count);


			for (int i = 0; i < GameControl.control.StoredLogins.Count; i++)
			{
				if (GameControl.control.StoredLogins [i].Name == SelectedLogin)
				{
					Logins.Add (GameControl.control.StoredLogins[i]);
				}
			}

			if (Logins.Count > 0)
			{
				scrollpos = GUI.BeginScrollView(new Rect(1, 46, 295, 150), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
				for (scrollsize = 0; scrollsize < Logins.Count; scrollsize++)
				{
					if (GUI.Button (new Rect (1, scrollsize * 21, 125, 20),"" + Logins[scrollsize].Username))
					{

					}

					if (GUI.Button (new Rect (127, scrollsize * 21, 125, 20),"" + Logins[scrollsize].Password)) 
					{

					}
				}
				GUI.EndScrollView();
			}
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
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				appman.SelectedApp = "Accounts";
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1]);
		}

		if (MiniButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		}

		if (Menu != "Main") 
		{
			DefaltBoxSetting = new Rect (2 + 24, 2, MiniButton.x - 24 - 3, 21);
			if (GUI.Button (new Rect (2, 2, DefaltBoxSetting.x - 3, 21), "<",com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				if (SelectedLogin != "")
				{
					SelectedLogin = "";
				} 
				else
				{
					Menu = "Main";
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
