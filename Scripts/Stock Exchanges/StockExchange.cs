﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockExchange : MonoBehaviour
{
	public List<StockExchangeSystem> ListOfExchanges = new List<StockExchangeSystem>();

	public List<StockExchangeSystem> CurrentListOfExchanges = new List<StockExchangeSystem>();

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	private GameObject AppSoftware;
	private GameObject SysSoftware;

	private Clock clk;
	private StockExchangeBrowser seb;
	private Defalt def;

	public int Index;

	public List<Color> Colors = new List<Color>();
	public Color32 rgb1 = new Color32(0,0,0,0);
	public Color32 ButtonColor = new Color32(0,0,0,0);
	public Color32 FontColor = new Color32(0,0,0,0);

	public int ColorSelect;

	public string ExchangeName;

	// Use this for initialization
	void Start () 
	{
		ExchangeName = "";
		AppSoftware = GameObject.Find("Applications");
		SysSoftware = GameObject.Find("System");
		WebSearch();
		AddExchanges();
		LoadPresetColors();
	}

	void WebSearch()
	{
		seb = AppSoftware.GetComponent<StockExchangeBrowser>();
		def = SysSoftware.GetComponent<Defalt>();
	}

	void AddExchanges()
	{
		ListOfExchanges.Add(new StockExchangeSystem("Create Account","www.stockexchange.com", "www.stockexchange.com/createaccount",false));
		ListOfExchanges.Add(new StockExchangeSystem("Sign In", "www.stockexchange.com", "www.stockexchange.com/signin", false));
		ListOfExchanges.Add (new StockExchangeSystem ("Memes", "www.stockexchange.com/exchanges", "www.stockexchange.com/exchanges/meme", false));
		ListOfExchanges.Add (new StockExchangeSystem ("Proper", "www.stockexchange.com/exchanges", "www.stockexchange.com/exchanges/proper", false));
		ListOfExchanges.Add(new StockExchangeSystem("Exchanges", "www.stockexchange.com", "www.stockexchange.com/exchanges",false));
		ListOfExchanges.Add (new StockExchangeSystem ("Portfolio", "www.stockexchange.com/account", "www.stockexchange.com/account/portfolio",true));
        ListOfExchanges.Add(new StockExchangeSystem("Trade History", "www.stockexchange.com/account", "www.stockexchange.com/account/history",true));
    }

	void LoadPresetColors()
	{
		rgb1.r = 100;
		rgb1.g = 100;
		rgb1.b = 100;
		rgb1.a = 255;

		ButtonColor.r = 75;
		ButtonColor.g = 75;
		ButtonColor.b = 75;
		ButtonColor.a = 255;

		FontColor.r = 255;
		FontColor.g = 255;
		FontColor.b = 255;
		FontColor.a = 255;
	}

	public void UpdateCurrentList(int i)
	{
		if (seb.Inputted == ListOfExchanges[i].CurrentURL)
		{
			CurrentListOfExchanges.Add(ListOfExchanges[i]);
		}
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
		if (ListOfExchanges.Count > 0)
		{
			int rows = 0;
			float x = 0;
			float y = 0;

			CurrentListOfExchanges.RemoveRange(0, CurrentListOfExchanges.Count);

			for (int i = 0; i < ListOfExchanges.Count; i++)
			{
				UpdateCurrentList(i);
			}

			if (CurrentListOfExchanges.Count > 0)
			{
				for (int j = 0; j < CurrentListOfExchanges.Count; j++)
				{
					if (GUI.Button(new Rect(x + 2, y + 48, 150, 22), CurrentListOfExchanges[j].Exchange))
					{
						seb.Inputted = CurrentListOfExchanges[j].URL;
					}


					rows++;
					x += 150 + 1;
					if (rows == 3)
					{
						rows = 0;
						x = 0;
						y += 22 + 1;
					}
				}
			}
		}	
	}
}
