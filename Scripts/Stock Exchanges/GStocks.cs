﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GStocks : MonoBehaviour
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

	public bool showBuyMenu;

	public int Index;

	public int BoughtShareAmmount;
	public int BuyingOrSeling;

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
	public string ShareQTY;
	public int Amount;

	// Use this for initialization
	void Start () 
	{
		ExchangeName = "Memes Exchange";
		AppSoftware = GameObject.Find("Applications");
		SysSoftware = GameObject.Find("System");
		WebSearch();
		AddCorps();
		LoadPresetColors();
		EnableTime = true;
		Cooldown = 60;
	}

	// Update is called once per frame
	void Update () 
	{
		Timers ();
	}

	void WebSearch()
	{
		ib = AppSoftware.GetComponent<InternetBrowser>();
		def = SysSoftware.GetComponent<Defalt>();
	}

	void AddCorps()
	{
		ListOfCompaniesNames.Add("Dat Boii Pepe");
		ListOfCompaniesNames.Add("Nyan Cat");
		ListOfCompaniesNames.Add("Trolol");
		ListOfCompaniesNames.Add("Darude Sandstorm");
		ListOfCompaniesNames.Add("MLG");
		ListOfCompaniesNames.Add("Vine");
		ListOfCompaniesNames.Add("Star Wars Kid");
		ListOfCompaniesNames.Add("Clippy");
		ListOfCompaniesNames.Add("Cheeki Breeki");
		ListOfCompaniesNames.Add("Remove Kebab");
		ListOfCompaniesNames.Add("Air Horn Remixes");
		ListOfCompaniesNames.Add("Harambe");
		ListOfCompaniesNames.Add("DANK");
		ListOfCompaniesNames.Add("EMOJI");
		ListOfCompaniesNames.Add("Doge");
		ListOfCompaniesNames.Add("Cats");
		ListOfCompaniesNames.Add("Leave Birtany Alone");
		ListOfCompaniesNames.Add("Ennuya Guy");
		ListOfCompaniesNames.Add("Hey Hey Hey");
		ListOfCompaniesNames.Add("Overwatch Memes");
		ListOfCompaniesNames.Add("Lemme Smash");


		if (GameControl.control.Exchanges.Count > 0)
		{
			for (int i = 0; i < GameControl.control.Exchanges.Count; i++)
			{
				if (GameControl.control.Exchanges [i].Exchange == ExchangeName)
				{
					Count++;
				}	
			}

			if (Count < ListOfCompaniesNames.Count) 
			{
				for (int i = 0; i < ListOfCompaniesNames.Count; i++) 
				{
					GameControl.control.Exchanges.Add (new StockExchangeShareSystem (ListOfCompaniesNames[i],ExchangeName,"","","","","",Random.Range(1,300),Random.Range(1,300),Random.Range(1,300),Random.Range(1,300),0,0));
				}
			}	

		} 
		else 
		{
			if (GameControl.control.Exchanges.Count < ListOfCompaniesNames.Count) 
			{
				for (int i = 0; i < ListOfCompaniesNames.Count; i++) 
				{
					GameControl.control.Exchanges.Add (new StockExchangeShareSystem (ListOfCompaniesNames[i],ExchangeName,"","","","","",Random.Range(1,300),Random.Range(1,300),Random.Range(1,300),Random.Range(1,300),0,0));
				}
			}	
		}
	}

	void Timers()
	{
		if (EnableTime == true) 
		{
			cd += Time.deltaTime;
		}

		if (cd >= Cooldown) 
		{
			Cal();
			cd = 0;
		}
		//
		//		if (ProfileController.procon.Hour == 2 && ProfileController.procon.Min == 0)
		//		{
		//			Cal();
		//		}
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

	void Math()
	{
		Exchange.RemoveRange (0, Exchange.Count);

		if (GameControl.control.Exchanges.Count > 0)
		{
			for (int i = 0; i < GameControl.control.Exchanges.Count; i++)
			{
				if (GameControl.control.Exchanges [i].Exchange == ExchangeName)
				{
					BuyingOrSeling = Random.Range(1, 3);

					if (GameControl.control.Exchanges [i].CurPrice < Random.Range (10, 50))
					{
						BuyingOrSeling = 1;
						BoughtShareAmmount = Random.Range(25, 50);
					}

					if (GameControl.control.Exchanges [i].CurPrice > Random.Range (300, 500))
					{
						BuyingOrSeling = 2;
						BoughtShareAmmount = Random.Range(25, 50);
					}

					if (BuyingOrSeling == 1) 
					{
						//Buying
						BoughtShareAmmount = Random.Range(1, 25);
						//GStockSave.stocks.CurrentShareVolume[i] -= BoughtShareAmmount;
						GameControl.control.Exchanges[i].CurPrice += 1 * BoughtShareAmmount;
					}

					if (BuyingOrSeling == 2) 
					{
						//Selling
						BoughtShareAmmount = Random.Range(1, 25);
						//GStockSave.stocks.CurrentShareVolume[i] += BoughtShareAmmount;
						GameControl.control.Exchanges[i].CurPrice -= 1 * BoughtShareAmmount;
					}

					//PercentValue[i] = GStockSave.stocks.CurrentSharePrice[i] - GStockSave.stocks.PastSharePrice[i];

					GameControl.control.Exchanges [i].ChangeVal = GameControl.control.Exchanges [i].CurPrice - GameControl.control.Exchanges [i].PPrice;

					float PercentMath = (GameControl.control.Exchanges [i].CurPrice - GameControl.control.Exchanges [i].PPrice) / GameControl.control.Exchanges [i].PPrice * 100;
					GameControl.control.Exchanges [i].ChangePercent = PercentMath;

					Exchange.Add (GameControl.control.Exchanges [i]);
				}
			}
		}
	}

	void Cal()
	{
		if (GameControl.control.Exchanges.Count > 0)
		{
			for (int i = 0; i < GameControl.control.Exchanges.Count; i++)
			{
				if (GameControl.control.Exchanges [i].Exchange == ExchangeName)
				{
					GameControl.control.Exchanges [i].PPrice = GameControl.control.Exchanges [i].CurPrice;
				}
			}

			Math();
		}
	}

	void PurchaseShares()
	{
		float cost = Exchange [SelectedCompany].CurPrice * Amount;
		GameControl.control.MyBankDetails [GameControl.control.SelectedBank].AccountBalance -= cost;
		GameControl.control.Portfolio.Add (new StockPortfolioSystem (Exchange [SelectedCompany].Exchange, "", Exchange [SelectedCompany].Company, "", GameControl.control.Time.FullDate, Exchange [SelectedCompany].CurPrice, Amount));
        GameControl.control.TransactionHistory.Add(new StockPortfolioSystem(Exchange[SelectedCompany].Exchange, "B", Exchange[SelectedCompany].Company, "", GameControl.control.Time.FullDate, Exchange[SelectedCompany].CurPrice, Amount));
        Amount = 0;
		showBuyMenu = false;
	}

	public void RenderSite()
	{
		GUI.backgroundColor = ButtonColor;
		GUI.contentColor = FontColor;
		//GUI.color = rgb1;
		RenderReady();
	}

	void RenderReady()
	{
		if (showBuyMenu == true)
		{
			if(GUI.Button(new Rect(50, 200, 60, 21), "Cancel"))
			{
				showBuyMenu = false;
			}

			GUI.Label (new Rect(5,40,300,300),"" + Exchange[SelectedCompany].Company);

			GUI.Label (new Rect(5,60,300,300),"Type the qty of shares you want to purchase.");
			ShareQTY = GUI.TextField(new Rect (5, 80, 50, 20),"" + ShareQTY);
			ShareQTY = Regex.Replace(ShareQTY, @"[^0-9]", "");
			if (ShareQTY != "") 
			{
				Amount = int.Parse(ShareQTY);	
			}
			GUI.Label (new Rect(5,100,300,300),"" + Amount + " @ " + Exchange[SelectedCompany].CurPrice);

			float cost = Exchange [SelectedCompany].CurPrice * Amount;
			GUI.Label (new Rect(5,130,300,300),"Total Cost: " + cost);

			if(GUI.Button(new Rect(250, 200, 35, 21), "Buy"))
			{
				if (Amount > 0)
				{
					if (GameControl.control.MyBankDetails [GameControl.control.SelectedBank].AccountBalance >= cost)
					{
						PurchaseShares ();
					}
				}
			}
		}
		else
		{
			GUI.Button(new Rect(2, 25, 174, 21), "Company Name");

			GUI.Button(new Rect(177, 25, 100, 21), "Change %");

			GUI.Button(new Rect(278, 25, 80, 21), "Price");

			GUI.Button (new Rect (359, 25, 60, 21), "Change");

			GUI.Button (new Rect (420, 25, 40, 21), "Buy");

			//GUI.Label(new Rect(205,55,100,100),"Selected: " + GStockSave.stocks.SelectedCompanyIndex);

			if (Exchange.Count > 0)
			{
				scrollpos = GUI.BeginScrollView(new Rect(0, 47, 499, 240), scrollpos, new Rect(0, 0, 0, scrollsize*22));
				for (scrollsize = 0; scrollsize < Exchange.Count; scrollsize++)
				{
					if(Exchange[scrollsize].ChangeVal < 0)
					{
						GUI.contentColor = Color.red;
					}

					if (Exchange[scrollsize].ChangeVal > 0)
					{
						GUI.contentColor = Color.green;
					}

					if(GUI.Button(new Rect(359, scrollsize * 22, 60, 21), "" + Exchange[scrollsize].ChangeVal))
					{

					}

					GUI.contentColor = Color.white;

					//				if(GUI.Button(new Rect(0, scrollsize * 22, 42, 21), "" + scrollsize))
					//				{
					//
					//				}

					if(GUI.Button(new Rect(2, scrollsize * 22, 174, 21), "" + Exchange[scrollsize].Company))
					{
						//GStockSave.stocks.SelectedCompanyIndex = scrollsize;
						//GStockSave.stocks.SelectedCompanyName = ListOfCompaniesNames[GStockSave.stocks.SelectedCompanyIndex];
						//GStockSave.stocks.SelectedPrice = GStockSave.stocks.CurrentSharePrice[GStockSave.stocks.SelectedCompanyIndex];
					}

					if(Exchange[scrollsize].ChangePercent < 0)
					{
						GUI.contentColor = Color.red;
					}

					if (Exchange[scrollsize].ChangePercent > 0)
					{
						GUI.contentColor = Color.green;
					}

					if(GUI.Button(new Rect(177, scrollsize * 22, 100, 21), "" + Exchange[scrollsize].ChangePercent.ToString("0.00") + "%"))
					{

					}

					GUI.contentColor = Color.white;

					if(GUI.Button(new Rect(278, scrollsize * 22, 80, 21), "$" + Exchange[scrollsize].CurPrice))
					{

					}

					if(GUI.Button(new Rect(420, scrollsize * 22, 40, 21), "[B]"))
					{
						SelectedCompany = scrollsize;
						showBuyMenu = true;
					}
				}
				GUI.EndScrollView();
			}	
		}
	}
}
