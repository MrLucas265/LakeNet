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

    //public string Answer;
    //public float Var1;
    //public float Var2;

    // Use this for initialization
    void Start()
    {
        ep = GetComponent<ErrorProm>();
        sdp = GetComponent<ShutdownProm>();
        dp = GetComponent<DeleteProm>();
        accset = GetComponent<AccSetup>();

        ProfileController.procon.Load();

        SelectedBackground = Random.Range(0, BackgroundPics.Count - 1);

        //		if (GameControl.control.SelectedOS == null)
        //		{
        //			GameControl.control.SelectedOS = GameControl.control.OSName[0];
        //		}

    }

    // Update is called once per frame
    void Update()
    {
        //var x = Var1;
        //var factor = Var2;
        //var divs = x / factor;
        //var remainder = x % factor;
        //Answer = (string.Format("{0} / {1} = {2} // {0} % {1} = {3}", x, factor, divs, remainder));

        //if (TypedPass[0] != ProfileController.procon.ProfilePassWord[Select])
        //{
        //    Correctpass = false;
        //}

        Counting();
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
		GameControl.control.ProfilePicID = ProfileController.procon.ProfileID[Select];
		GameControl.control.ProfileID = Select;
		Customize.cust.ProfileName = ProfileController.procon.Profiles[Select];
		HardwareController.hdcon.ProfileName = ProfileController.procon.Profiles[Select];
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

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = Skin;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundPics[SelectedBackground]);

		//GUI.Label(new Rect(695, Screen.height-30, 100, 30),"Turn off system");

//		if(GUI.Button(new Rect(100, Screen.height-31, 110, 22), "Create Account"))
//		{
//			Select = 0;
//			GameControl.control.Load();
//			StartSetup();
//		}

        if(ProfileController.procon.Profiles.Count == 2)
        {
            Select = 1;
            GameControl.control.ProfileName = ProfileController.procon.Profiles[Select];
            Customize.cust.ProfileName = ProfileController.procon.Profiles[Select];
            HardwareController.hdcon.ProfileName = ProfileController.procon.Profiles[Select];
            GameControl.control.Load();
            Customize.cust.Load();
            HardwareController.hdcon.Load();
        }

		if (Select > 0) 
		{
			//GUI.Label (new Rect (400, 100, 150, 22), "Account Name: " + GameControl.control.ProfileName);
			//if (GameControl.control.MyBankDetails.Count > 0)
			//{
			//	GUI.Label(new Rect(400, 120, 150, 22), "Bank Balance: " + "$" + GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance);
			//}
			//else
			//{
			//	GUI.Label(new Rect(400, 120, 150, 22), "Bank Balance: " + "$" + 0);
			//}
			//GUI.Label (new Rect (400, 140, 150, 22), "Reputation: " + GameControl.control.Rep);
			//GUI.Label (new Rect (400, 160, 150, 22), "Current Contracts: " + GameControl.control.Contracts.Count);
			//GUI.Label (new Rect (400, 180, 150, 22), "IP: " + GameControl.control.fullip);
			//GUI.Label (new Rect (400, 200, 150, 22), "Time: " + ProfileController.procon.Hour.ToString("F0") + ":" + ProfileController.procon.Min.ToString("F0"));
			//GUI.Label (new Rect (400, 220, 150, 22), "Date: " + ProfileController.procon.Day + "/" + ProfileController.procon.Month + "/" + ProfileController.procon.CurYear);
			//if (ProfileController.procon.SelectedOS != null)
			//{
			//	GUI.Label (new Rect (400, 240, 300, 22), "Selected OS: " + ProfileController.procon.SelectedOS[Select].Name);
			//}

			if (Correctpass == false)
			{
				PasswordLogin ();
			}
			else
			{
				SigningIn = true;
			}

			//if (GUI.Button(new Rect(0, Select * ScrollSizeHolder, 32, 32), "Del"))
			//{
			//	ProfileController.procon.SelectedProfile = Select;
			//	GameControl.control.ProfileName = ProfileController.procon.Profiles[Select];
			//	Customize.cust.ProfileName = ProfileController.procon.Profiles[Select];
			//	HardwareController.hdcon.ProfileName = ProfileController.procon.Profiles[Select];
			//	GameControl.control.DeleteFile();
			//	Customize.cust.DeleteFile();
			//	HardwareController.hdcon.DeleteFile();
			//	ProfileController.procon.DeleteProfile();
			//	Select = -1;
			//	ProfileController.procon.SelectedProfile = Select;
			//}

			//			if (GameControl.control.OSName.Count > 1)
			//			{
			//				OSscrollpos = GUI.BeginScrollView(new Rect(50, 265, 170, 150), OSscrollpos, new Rect(0, 0, 0, OSscrollsize*23));
			//				for (OSscrollsize = 0; OSscrollsize < GameControl.control.OSName.Count; OSscrollsize++)
			//				{
			//					if (GUI.Button (new Rect (0, 23 * OSscrollsize, 150, 22),"" + GameControl.control.OSName[OSscrollsize].Name)) 
			//					{
			//						GameControl.control.SelectedOS = GameControl.control.OSName[OSscrollsize];
			//					}
			//				}
			//				GUI.EndScrollView();
			//			}
		}

		if (SigningIn == false)
		{
			if(GUI.Button(new Rect(Screen.width-33, Screen.height-33, 32, 32), ShutdownPic))
			{
				Application.Quit();
			}



			if (ProfileController.procon.Profiles.Count >= 1) 
			{
				scrollpos = GUI.BeginScrollView(new Rect(5, 5, 350, 410), scrollpos, new Rect(0, 0, 0, scrollsize*ScrollSizeHolder));
				for (scrollsize = 0; scrollsize < ProfileController.procon.Profiles.Count; scrollsize++)
				{
					StyleFunc();
					if(GUI.Button(new Rect(2, scrollsize * ScrollSizeHolder, 200, 32),ProfileController.procon.Profiles[scrollsize],style))
					{
						Select = scrollsize;
						if (Select > 0) 
						{
							GameControl.control.ProfileName = ProfileController.procon.Profiles [Select];
							Customize.cust.ProfileName = ProfileController.procon.Profiles[Select];
							HardwareController.hdcon.ProfileName = ProfileController.procon.Profiles[Select];
							GameControl.control.Load ();
							Customize.cust.Load();
							HardwareController.hdcon.Load();
						}
						else 
						{
							Select = 0;
							GameControl.control.ProfileName = ProfileController.procon.Profiles [Select];
							Customize.cust.ProfileName = ProfileController.procon.Profiles[Select];
							GameControl.control.Load();
							Customize.cust.Load();
							HardwareController.hdcon.Load();
							StartSetup();
						}
					}
					GUI.Label(new Rect(-33, scrollsize * ScrollSizeHolder, 32, 32),GameControl.control.UserPic[ProfileController.procon.ProfilePic[scrollsize]], style);
                    if (scrollsize == 0)
                    {
                        GUI.Label(new Rect(-33, scrollsize * ScrollSizeHolder, 32, 32), Plus, style);
                    }
                    //				GUI.DrawTexture (new Rect (5, scrollsize * ScrollSizeHolder + 5f, 24, 24),GameControl.control.UserPic[ProfileController.procon.ProfileID [scrollsize]]);
                }

				GUI.EndScrollView();
			}
		}

		if (SigningIn == true)
		{
			GUI.Label(new Rect(Screen.width/2.5f, Screen.height/2, 100, 24),"Signing In please wait.", style1);
			SignIn();
		}
	}
}
