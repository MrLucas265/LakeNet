using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUG : MonoBehaviour 
{
	private GameObject AppsSoftware;
	private InternetBrowser ib;

	public string Title;
	public Rect TitlePos;
	public List<Color> Colors = new List<Color>();
	public Color32 rgb1 = new Color32(0,0,0,0);
	public Color32 rgb2 = new Color32(0,0,0,0);
	public Color32 rgb3 = new Color32(0,0,0,0);

	public int ColorSelect;

	public float SelectedSpeed;

    public int MonthlyPrice;

	public float Timer;
	public float TimerStart = 5;

	public int BannerMsg;
	public string BannerContent;
	public Rect BannerRect;
	public bool SwitchBanner;
	public bool MoveBanner;
	public bool MoveBannerRight;
	public bool MoveBannerLeft;

	public string Username;
	public string Password;

	public string CurrentAccount;
	public string CurrentPass;

	public bool LoggedIn;

	public bool DisplayNotfications;
	public string NotificationMsg;

	public string SelectedPlanInfo;
    public string SelectedPlanName;

	// Use this for initialization
	void Start () 
	{
		Timer = TimerStart;
		LoadPresetColors();
		BannerRect = new Rect (0, 100, 500, 50);

		AppsSoftware = GameObject.Find("Applications");
		ib = AppsSoftware.GetComponent<InternetBrowser>();
	}

	void BannerTimers()
	{
		if (Timer <= 0) 
		{
			Timer = TimerStart;
		} 
		if(Timer > 0) 
		{
			Timer -= Time.deltaTime;
		}
	}

	void BannerSystem()
	{

		if (SwitchBanner == false && MoveBanner == false)
		{
			BannerTimers();
		}

		if (BannerMsg >= 3 && Timer <= 0) 
		{
			BannerMsg = 0;
			SwitchBanner = true;
		} 

		if(Timer <= 0 && SwitchBanner == false && MoveBanner == false) 
		{
			SwitchBanner = true;
			MoveBanner = true;
			MoveBannerRight = true;
		}

		if (SwitchBanner == true) 
		{
			if (MoveBannerRight == true)
			{
				BannerRect.x += 5 * Time.deltaTime * 25;
			}

			if (BannerRect.x >= 505) 
			{
				BannerMsg++;
				if (BannerMsg > 3) 
				{
					BannerMsg = 0;
				} 
				MoveBannerRight = false;
				MoveBannerLeft = true;
			}

			if (MoveBannerLeft == true) 
			{
				BannerRect.x -= 5 * Time.deltaTime * 25;
			}

			if (BannerRect.x <= 0)
			{
				MoveBannerLeft = false;
				MoveBanner = false;
				SwitchBanner = false;
			}
		}
	}

	void LoadPresetColors()
	{
		rgb1.r = 0;
		rgb1.g = 0;
		rgb1.b = 0;
		rgb1.a = 255;

		rgb2.r = 148;
		rgb2.g = 0;
		rgb2.b = 211;
		rgb2.a = 255;

		rgb3.r = 0;
		rgb3.g = 255;
		rgb3.b = 255;
		rgb3.a = 255;
	}

	public void HomePage()
	{
		BannerSystem();

		GUI.Box(new Rect (BannerRect), BannerContent);

		switch (BannerMsg)
		{
		case 0:
			BannerContent = "Get ADSL+2 50GQs for 79.99 a months.";
			break;
		case 1:
			BannerContent = "Get ADSL+1 Unlimited for 59.99 a months.";
			break;
		case 2:
			BannerContent = "Get Dial-Up for free";
			break;
		case 3:
			BannerContent = "Get 200GQs of Cable for 150 a month";
			break;
		}

		if(GUI.Button(new Rect(0,75,100,21),"Plans"))
		{
			if (LoggedIn == true) 
			{
				ib.AddressBar = "www.tugs.com/plans";
			}
		}

		if(GUI.Button(new Rect(100,75,100,21),"Account"))
		{
			ib.AddressBar = "www.tugs.com/login";
		}
	}

	void Plans()
	{
		//GUI.Label (new Rect (TitlePos), Title);

		if(GUI.Button(new Rect(2,40,150,20),"Set Network Speed"))
		{
            GameControl.control.Gateway.InstalledModem[0].MaxSpeed = SelectedSpeed;
            GameControl.control.Gateway.InstalledModem[0].CurrentSpeed = SelectedSpeed;
            
            DateSystem InitalPlanDate = new DateSystem(GameControl.control.Time.Seconds, GameControl.control.Time.Miniutes, GameControl.control.Time.Hours, GameControl.control.Time.Day, GameControl.control.Time.Month, GameControl.control.Time.Year, 0, "", false, "", 0, 0, 0, false, "" + GameControl.control.Time.Day.ToString("00") + "" + "/" + GameControl.control.Time.Month.ToString("00") + "/" + GameControl.control.Time.Year.ToString("0000"), "", "");
            int MonthMath = InitalPlanDate.Month + 1;
            int DDay = InitalPlanDate.Day;
            int DYear = InitalPlanDate.Year;
            string DString = "" + DDay.ToString("00") + "/" + MonthMath.ToString("00") + "/" + DYear.ToString("0000");
            DateSystem DuePlanDate = new DateSystem(InitalPlanDate.Seconds, InitalPlanDate.Miniutes, InitalPlanDate.Hours, InitalPlanDate.Day, MonthMath, InitalPlanDate.Year, 0, "", false, "", 0, 0, 0, false,DString, "", "");

            for (int i = 0; i < GameControl.control.Plans.Count; i++)
            {
                if(GameControl.control.Plans[i].Company == "TUG")
                {
                    if (!GameControl.control.Plans[i].Name.Contains("Modem"))
                    {
                       // GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance -= GameControl.control.Plans[i].Price;
                        GameControl.control.Plans.RemoveAt(i);
                    }
                }
            }

            GameControl.control.Plans.Add(new PlanSystem("TUG", "www.tugs.com", SelectedPlanName, SelectedPlanInfo, MonthlyPrice, InitalPlanDate, DuePlanDate));
           // GameControl.control.MyBankDetails[GameControl.control.SelectedBank].AccountBalance -= MonthlyPrice;
        }

        GUI.Label(new Rect(315, 60, 300, 20), "Current Max Speed: " + GameControl.control.Gateway.InstalledModem[0].MaxSpeed);
        GUI.Label(new Rect(315, 80, 300, 20), "Current Speed: " + GameControl.control.Gateway.InstalledModem[0].CurrentSpeed);

        if (GUI.Button(new Rect(5,80,100,20),"Dial Up"))
		{
            SelectedPlanName = "DialUp";
            SelectedPlanInfo = "Dial up is the slowest plan how ever it is free and unlimited good for basic networking soloutions.";
			SelectedSpeed = 0.056f;
            MonthlyPrice = 0;
        }
		if(GUI.Button(new Rect(5,100,100,20),"ADSLG1"))
		{
            SelectedPlanName = "ADSLG1";
            SelectedPlanInfo = "ADSLG1 is the next step 5x faster then dial up how ever you have to pay and it has diffrent options.";
			SelectedSpeed = 0.256f;
            MonthlyPrice = 500;
        }
		if(GUI.Button(new Rect(5,120,100,20),"ADSLG2"))
		{
            SelectedPlanName = "ADSLG2";
            SelectedPlanInfo = "ADSLG2 is double the speed of ADSLG1 but it costs a lil bit more and has plantaful options.";
			SelectedSpeed = 0.512f;
            MonthlyPrice = 1500;
        }
        if (GUI.Button(new Rect(5, 140, 100, 20), "ADSLG3"))
        {
            SelectedPlanName = "ADSLG3";
            SelectedPlanInfo = "ADSLG3 is double the speed of ADSLG2 but it costs a lil bit more and has plantaful options.";
            SelectedSpeed = 1;
            MonthlyPrice = 3500;
        }
        if (GUI.Button(new Rect(5,160,100,20),"ADSLG4"))
		{
            SelectedPlanName = "ADSLG4";
            SelectedPlanInfo = "ADSLG4 is double the speed of ADSLG3 but it costs a lil bit more and has plantaful options.";
			SelectedSpeed = 2f;
            MonthlyPrice = 8000;
        }
        if (GUI.Button(new Rect(5, 180, 100, 20), "ADSLG5"))
        {
            SelectedPlanName = "ADSLG5";
            SelectedPlanInfo = "ADSLG5 is double the speed of ADSLG4 but it costs a lil bit more and has plantaful options.";
            SelectedSpeed = 4f;
            MonthlyPrice = 20000;
        }
  //      if (GUI.Button(new Rect(5,160,100,20),"Naked"))
		//{
		//	SelectedSpeed = 10f;
		//}

		GUI.TextArea (new Rect (110,100,200,200), SelectedPlanInfo);
	}

	void Login()
	{
		Username = GUI.TextField(new Rect(85, 55, 120, 20), Username, 500);
		Password = GUI.TextField(new Rect(85, 75, 120, 20), Password, 500);
		GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
		GUI.Label(new Rect(3, 75, 500, 500), "Password: ");

		if (DisplayNotfications == true) 
		{
			if (GUI.Button (new Rect (50, 120, 300, 21), "Incorrect information please retry"))
			{
				DisplayNotfications = false;
			}
		}

		//GUI.Label(new Rect(3, 75, 500, 500), "Incorrect Password");

		if(GUI.Button(new Rect(250,250,100,21),"Login"))
		{
			if (Username != CurrentAccount || Password != CurrentPass)
			{
				DisplayNotfications = true;
			}

			if (Username == CurrentAccount && Password == CurrentPass) 
			{
				DisplayNotfications = false;
				ib.AddressBar = "www.tugs.com/plans";
				LoggedIn = true;
			}
		}
	}

	public void RenderSite()
	{
		GUI.backgroundColor = rgb2;
		GUI.contentColor = rgb3;

		switch (ib.AddressBar) 
		{
		case "www.tugs.com":
			HomePage();
			break;
		case "www.tugs.com/homepage":
			HomePage();
			break;
		case "www.tugs.com/login":
			Login();
			break;
		case "www.tugs.com/plans":
			Plans();
			break;
		}
	}
}
