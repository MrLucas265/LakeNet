using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ProfileUI : MonoBehaviour
{
    public float x;
    public float y;

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;

    public Vector2 OSscrollpos = Vector2.zero;
    public int OSscrollsize;

    public GUIStyle style;
    public GUIStyle style1;
    public GUISkin Skin;

    public int Select;
    public int CurSelect;

    public Rect windowRect = new Rect(100, 100, 200, 200);
    public float native_width = 1920;
    public float native_height = 1080;
    public int windowID;

    public int ProfilePicID;
    public string ProfileName;
    public List<string> PicName = new List<string>();

    public Texture2D Plus;

    public List<Texture2D> BackgroundPics = new List<Texture2D>();

    public Texture2D ShutdownPic;
    public Texture2D loginArrow;

    public bool show;
    public bool ShowPics;
    public bool ShowPass;

    public bool shutdown;

    public AudioSource AS;

    private ErrorProm ep;
    private ShutdownProm sdp;
    private DeleteProm dp;
    private AccSetup accset;

    public bool male;
    public bool female;

    public float Modify;
    public float ScrollSizeHolder;

    //Password Stuff
    public string[] TypedPass;
    public bool Correctpass;
    public string InputtedPass;

    public int Counted;

    public bool Focused;
    public bool EnterLogin;

    public bool CreateNewAccount;

    public bool ShowSelectedAccount;

    public int CharLength;
    public float CharLengthMath;
    public float mod;

    public bool SigningIn;

    public int SelectedBackground;

	public bool ShowDeleteInfo;
	public bool ConfirmedAccountDeletion;
	public int DeleteSelectedAccount;
	public int CorrectCount;

	public bool showAccountsList;

	public string CurrentTime;

	public GUIStyle test;
	public Rect Black_Box;
	public Texture2D BlackBox;

	void Start()
    {
        ep = GetComponent<ErrorProm>();
        sdp = GetComponent<ShutdownProm>();
        dp = GetComponent<DeleteProm>();
        accset = GetComponent<AccSetup>();

		ProfileController.procon.Load();

		showAccountsList = true;

		LoadPics();
	}


	void LoadPics()
	{
		for (int PhotoNumber = 0; PhotoNumber < 131; PhotoNumber++)
		{
			BackgroundPics.Add(Resources.Load<Texture2D>("DesktopBackgrounds/" + PhotoNumber));
		}

		SelectedBackground = Random.Range(0, BackgroundPics.Count - 1);
	}

	void Update()
    {
        Counting();

		CurrentTime = "" + System.DateTime.Now;

	}

    void Counting()
    {
        Counted = ProfileController.procon.Profiles.Count;

        if (Counted <= 1)
        {
            StartSetup();
        }
    }

    void StartSetup()
    {
        this.enabled = false;
        accset.enabled = true;
    }

    void StyleFunc()
    {
        style.normal.background = Skin.button.normal.background;
        style.hover.background = Skin.button.hover.background;
        style.active.background = Skin.button.active.background;
        style.normal.background = Skin.button.normal.background;
        style.hover.background = Skin.button.hover.background;
        style.active.background = Skin.button.active.background;

        style.normal.textColor = Skin.button.normal.textColor;
        style.hover.textColor = Skin.button.hover.textColor;
        style.active.textColor = Skin.button.active.textColor;
        style.normal.textColor = Skin.button.normal.textColor;
        style.hover.textColor = Skin.button.hover.textColor;
        style.active.textColor = Skin.button.active.textColor;

        style1.normal.textColor = Skin.button.normal.textColor;
        style1.hover.textColor = Skin.button.hover.textColor;
        style1.active.textColor = Skin.button.active.textColor;
        style1.normal.textColor = Skin.button.normal.textColor;
        style1.hover.textColor = Skin.button.hover.textColor;
        style1.active.textColor = Skin.button.active.textColor;

        style1.fontSize = 36;

        Counting();
    }

	void SignIn()
	{
		GameControl.control.ProfileName = ProfileController.procon.Profiles[Select];
		GameControl.control.ProfilePicID = ProfileController.procon.ProfilePic[Select];
		GameControl.control.ProfileID = Select;
		Customize.cust.ProfileName = ProfileController.procon.Profiles[Select];
		PersonController.control.ProfileName = ProfileController.procon.Profiles[Select];
		Application.LoadLevel(1);
	}

	void PasswordLogin()
	{
		GUI.DrawTexture(new Rect(400, 100, 128, 128),GameControl.control.UserPic[ProfileController.procon.ProfilePic[Select]]);

		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return) 
		{
			GameControl.control.SelectedOS.Name = ProfileController.procon.SelectedOS [Select].Name;
			TypedPass[0] = InputtedPass;
		}

		GUI.SetNextControlName("Text1");
		InputtedPass = GUI.PasswordField(new Rect (375, 300, 175, 24), InputtedPass,"*"[0]);

		CharLength = GameControl.control.ProfileName.Length;
		mod = CharLength * 0.4f;
		CharLengthMath = CharLength * mod;
		GUI.Label(new Rect(425-CharLengthMath, 240, 100, 24), GameControl.control.ProfileName, style1);

		GUI.Label(new Rect(300, 300, 100, 24),"Password: ");

		if (GUI.Button (new Rect (551, 300, 24, 24), ">"))
		{
			GameControl.control.SelectedOS.Name = ProfileController.procon.SelectedOS [Select].Name;
			TypedPass[0] = InputtedPass;
		}

		GUI.Label(new Rect(300,325, 250,150),"Hint: " + ProfileController.procon.PasswordHint[Select]);

		if (TypedPass [0] == ProfileController.procon.ProfilePassWord [Select]) 
		{
			Correctpass = true;
			if (EnterLogin == true) 
			{
				SigningIn = true;
			}
		} 
		else 
		{
			Correctpass = false;
		}

		if (!Focused) 
		{
			GUI.FocusControl("Text1");
			Focused = true;
		}
	}

	void DeleteAccountUI()
	{
		GUI.DrawTexture(new Rect(400, 100, 128, 128), GameControl.control.UserPic[ProfileController.procon.ProfilePic[DeleteSelectedAccount]]);

		CharLength = ProfileController.procon.Profiles[DeleteSelectedAccount].Length;
		mod = CharLength * 0.4f;
		CharLengthMath = CharLength * mod;
		GUI.Label(new Rect(425 - CharLengthMath, 240, 100, 24), "Delete Account: " + ProfileController.procon.Profiles[DeleteSelectedAccount], style1);

		switch(CorrectCount)
		{
			case 0:
				GUI.Label(new Rect(300, 325, 250, 150), "Press enter or click DEL to confirm the deletion of the account.");
				break;
			case 1:
				GUI.Label(new Rect(300, 325, 250, 150), "Are you sure you wish to remove this account.");
				break;
			case 2:
				GUI.Label(new Rect(300, 325, 250, 150), "Confirm once more for permenant removal.");
				break;
		}

		//GUI.Label(new Rect(300, 325, 250, 150), "Press enter or click DEL to confirm the deletion of the account.");

		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return || Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Delete)
		{
			if (TypedPass[1] == ProfileController.procon.ProfilePassWord[DeleteSelectedAccount])
			{
				CorrectCount++;
			}
			else
			{
				CorrectCount = 0;
			}

			if (CorrectCount >= 2)
			{
				ConfirmedAccountDeletion = true;
				TypedPass[1] = "";
				InputtedPass = "";
				CorrectCount = 0;
			}

			TypedPass[1] = InputtedPass;
		}

		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Backspace)
		{
			ShowDeleteInfo = false;
			TypedPass[1] = "";
			CorrectCount = 0;

		}

		if (GUI.Button(new Rect(278, 300, 24, 24), "X"))
		{
			ShowDeleteInfo = false;
			TypedPass[1] = "";
			CorrectCount = 0;
		}

		GUI.SetNextControlName("Text1");
		InputtedPass = GUI.PasswordField(new Rect(375, 300, 175, 24), InputtedPass, "*"[0]);

		GUI.Label(new Rect(300, 300, 100, 24), "Password: ");

		if (GUI.Button(new Rect(551, 300, 24, 24), "DEL"))
		{
			if (TypedPass[1] == ProfileController.procon.ProfilePassWord[DeleteSelectedAccount])
			{
				CorrectCount++;
			}
			else
			{
				CorrectCount = 0;
			}

			if (CorrectCount>=2)
			{
				ConfirmedAccountDeletion = true;
				TypedPass[1] = "";
				InputtedPass = "";
				CorrectCount = 0;
			}

			TypedPass[1] = InputtedPass;
		}

		if (!Focused)
		{
			GUI.FocusControl("Text1");
			Focused = true;
		}
	}

	void AccountSelection()
	{
		if (ProfileController.procon.Profiles.Count >= 1 && showAccountsList == true)
		{
			scrollpos = GUI.BeginScrollView(new Rect(5, 5, 260, 410), scrollpos, new Rect(0, 0, 0, scrollsize * ScrollSizeHolder));
			for (scrollsize = 0; scrollsize < ProfileController.procon.Profiles.Count; scrollsize++)
			{
				StyleFunc();
				if (GUI.Button(new Rect(2, scrollsize * ScrollSizeHolder, 200, 32), ProfileController.procon.Profiles[scrollsize], style))
				{
					ShowDeleteInfo = false;
					Select = scrollsize;
					InputtedPass = "";
					Correctpass = false;
					CorrectCount = 0;
					if (Select > 0)
					{
						LoadProfileData(Select);
					}
					else
					{
						Select = 0;
						LoadProfileData(Select);
						StartSetup();
					}
				}

				GUI.Label(new Rect(-33, scrollsize * ScrollSizeHolder, 32, 32), GameControl.control.UserPic[ProfileController.procon.ProfilePic[scrollsize]], style);
				if (scrollsize == 0)
				{
					GUI.Label(new Rect(-33, scrollsize * ScrollSizeHolder, 32, 32), Plus, style);
				}
				else
				{
					if (GUI.Button(new Rect(204, scrollsize * ScrollSizeHolder, 32, 32), "DEL"))
					{
						DeleteSelectedAccount = scrollsize;
						InputtedPass = "";
						Correctpass = false;
						CorrectCount = 0;
						if (DeleteSelectedAccount > 0)
						{
							ShowDeleteInfo = true;
						}
						else
						{
							ShowDeleteInfo = false;
						}
					}
				}
			}

			GUI.EndScrollView();
		}
	}

	void DeleteAccount()
	{
		LoadProfileData(DeleteSelectedAccount);

		GameControl.control.DeleteFile();
		Customize.cust.DeleteFile();

		ProfileController.procon.DeleteProfile(DeleteSelectedAccount);

		ProfileController.procon.Save();

		DeleteSelectedAccount = 0;
		Select = 0;
		CorrectCount = 0;

		LoadProfileData(DeleteSelectedAccount);
	}

	void LoadProfileData(int Selected)
	{
		GameControl.control.ProfileName = ProfileController.procon.Profiles[Selected];
		Customize.cust.ProfileName = ProfileController.procon.Profiles[Selected];
		PersonController.control.ProfileName = ProfileController.procon.Profiles[Selected];
		GameControl.control.Load();
		Customize.cust.Load();
		PersonController.control.Load();
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = Skin;


		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundPics[SelectedBackground]);

        if(ProfileController.procon.Profiles.Count == 2 && Select != 1)
        {
            Select = 1;
			LoadProfileData(Select);
		}

		if(ShowDeleteInfo == true)
		{
			DeleteAccountUI();

			if (ConfirmedAccountDeletion == true)
			{
				DeleteAccount();
				ShowDeleteInfo = false;
				ConfirmedAccountDeletion = false;
				TypedPass[1] = "";
				Select = 0;
				DeleteSelectedAccount = 0;
				CorrectCount = 0;
			}
		}
		else
		{
			if (Select > 0)
			{

				if (Correctpass == false)
				{
					PasswordLogin();
				}
				else
				{
					SigningIn = true;
				}
			}
		}

		if (SigningIn == false)
		{
			if (GUI.Button(new Rect(Screen.width - 33, Screen.height - 33, 32, 32), ShutdownPic))
			{
				Application.Quit();
			}
		}

		AccountSelection();

		if (SigningIn == true)
		{
			showAccountsList = false;
			GUI.Label(new Rect(Screen.width/2.5f, Screen.height/2, 100, 24),"Signing In please wait.", style1);
			SignIn();
		}

		GUI.Label(new Rect(Screen.width / 2, Screen.height-200, Black_Box.width, 200), BlackBox);
		GUI.Label(new Rect(Screen.width / 2, Screen.height-200, 500, 500), CurrentTime, test);
	}
}
