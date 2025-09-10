using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Portfolio : MonoBehaviour
{
	public List<string> ListOfCompaniesNames = new List<string>();


	public List<string> ListOfSavedCompanies = new List<string>();

	public List<StockExchangeShareSystem> Exchange = new List<StockExchangeShareSystem>();

	public List<int> StockPrice = new List<int>();

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	private GameObject AppSoftware;
	private GameObject SysSoftware;

	private Clock clk;
	private InternetBrowser ib;
	private Defalt def;

	public float cd;
	public float Cooldown;

	public bool showSellMenu;

	public int Index;

	public List<Color> Colors = new List<Color>();
	public Color32 rgb1 = new Color32(0,0,0,0);
	public Color32 ButtonColor = new Color32(0,0,0,0);
	public Color32 FontColor = new Color32(0,0,0,0);

	public int ColorSelect;

	public bool EnableTime;

	public float Count;
	public float SLCount;

	public string ExchangeName;

	public int SelectedCompany;
	public int Amount;
	public string ShareQTY;
	public float CurrentSharePrice;

	// Use this for initialization
	void Start () 
	{
		ExchangeName = "Memes Exchange";
		AppSoftware = GameObject.Find("Applications");
		SysSoftware = GameObject.Find("System");
		WebSearch();
		LoadPresetColors();
		EnableTime = true;
		Cooldown = 2;
	}

	void WebSearch()
	{
		ib = AppSoftware.GetComponent<InternetBrowser>();
		def = SysSoftware.GetComponent<Defalt>();
	}
	void LoadPresetColors()
	{
        rgb1.r = 100;
        rgb1.g = 100;
        rgb1.b = 100;
        rgb1.a = 255;

        ButtonColor.r = 50;
        ButtonColor.g = 50;
        ButtonColor.b = 50;
        ButtonColor.a = 255;

        FontColor.r = 128;
        FontColor.g = 128;
        FontColor.b = 128;
        FontColor.a = 255;
    }


	public void RenderSite()
	{
		GUI.backgroundColor = ButtonColor;
		GUI.contentColor = FontColor;
		//GUI.color = rgb1;
		RenderReady();
	}

	void SellShares()
	{
		float cost = CurrentSharePrice * Amount;

		for (int i = 0; i < GameControl.control.BankData.Count; i++)
		{
			for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
			{
				if (GameControl.control.BankData[i].Accounts[j].Primary == true)
				{
					GameControl.control.BankData[i].Accounts[j].AccountBalance += cost;
				}
			}
		}

        GameControl.control.TransactionHistory.Add(new StockPortfolioSystem(GameControl.control.Portfolio[SelectedCompany].Exchange, "S", GameControl.control.Portfolio[SelectedCompany].Company, "",PersonController.control.Global.DateTime.FullDate, CurrentSharePrice, Amount));
        GameControl.control.Portfolio.RemoveAt (SelectedCompany);
		Amount = 0;
		showSellMenu = false;
	}

	void RenderReady()
	{
		if (showSellMenu == true) 
		{
			for (int i = 0; i < GameControl.control.Exchanges.Count; i++)
			{
				if (GameControl.control.Exchanges [i].Exchange == GameControl.control.Portfolio [SelectedCompany].Exchange)
				{
					if (GameControl.control.Exchanges [i].Company == GameControl.control.Portfolio [SelectedCompany].Company)
					{
						CurrentSharePrice = GameControl.control.Exchanges [i].CurPrice; 
					}
				}
			}

			if(GUI.Button(new Rect(50, 200, 60, 21), "Cancel"))
			{
				showSellMenu = false;
			}

			GUI.Label (new Rect(5,40,300,300),"" + GameControl.control.Portfolio [SelectedCompany].Company);

			GUI.Label (new Rect(5,60,300,300),"Type the qty of shares you want to sell.");
			ShareQTY = GUI.TextField(new Rect (5, 80, 50, 20),"" + ShareQTY);
			ShareQTY = Regex.Replace(ShareQTY, @"[^0-9]", "");
			if (ShareQTY != "") 
			{
				Amount = int.Parse(ShareQTY);	
			}
			GUI.Label (new Rect(5,100,300,300),"" + Amount + " @ " + GameControl.control.Portfolio [SelectedCompany].Price);

			GUI.Label (new Rect(5,115,300,300),"Total Paid Price: " + Amount * GameControl.control.Portfolio [SelectedCompany].Price);

			float cost = CurrentSharePrice * Amount;

			GUI.Label (new Rect(5,130,300,300),"" + Amount + " @ " + CurrentSharePrice);

			GUI.Label (new Rect(5,145,300,300),"Total Price: " + cost);

			if(GUI.Button(new Rect(100, 80, 35, 21), "All"))
			{
				ShareQTY = "" + GameControl.control.Portfolio [SelectedCompany].Ammount;
			}

			if(GUI.Button(new Rect(250, 200, 35, 21), "Sell"))
			{
				if (Amount > 0)
				{
					SellShares();
				}
			}
		} 
		else
		{
			GUI.Button(new Rect(2, 25, 174, 21), "Company Name");

			GUI.Button(new Rect(177, 25, 100, 21), "Purchase Date");

			GUI.Button(new Rect(278, 25, 80, 21), "Price");

			GUI.Button (new Rect (359, 25, 60, 21), "QTY");

			GUI.Button (new Rect (420, 25, 60, 21), "Sell");

			//GUI.Label(new Rect(205,55,100,100),"Selected: " + GStockSave.stocks.SelectedCompanyIndex);

			GUI.contentColor = Color.white;

			if (GameControl.control.Portfolio.Count > 0)
			{
				scrollpos = GUI.BeginScrollView(new Rect(0, 47, 499, 196), scrollpos, new Rect(0, 0, 0, scrollsize*22));
				for (scrollsize = 0; scrollsize < GameControl.control.Portfolio.Count; scrollsize++)
				{

					GUI.contentColor = Color.white;

					if(GUI.Button(new Rect(2, scrollsize * 22, 174, 21), "" + GameControl.control.Portfolio[scrollsize].Company))
					{

					}

					if(GUI.Button(new Rect(177, scrollsize * 22, 100, 21), "" + GameControl.control.Portfolio[scrollsize].PDate))
					{

					}

					if(GUI.Button(new Rect(278, scrollsize * 22, 80, 21), "$" + GameControl.control.Portfolio[scrollsize].Price))
					{

					}

					if(GUI.Button(new Rect(359, scrollsize * 22, 60, 21), "" + GameControl.control.Portfolio[scrollsize].Ammount))
					{

					}

					if(GUI.Button(new Rect(420, scrollsize * 22, 60, 21), "[S]"))
					{
						SelectedCompany = scrollsize;
						showSellMenu = true;
					}
				}
				GUI.EndScrollView();
			}
		}	
	}
}
