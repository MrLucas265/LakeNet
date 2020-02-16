using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCrysis : MonoBehaviour 
{
	public string MenuSelector;

	public float PowerUnit;
	public float Cash;
	public float DisplayCash;
	public string Suffix;
	public float Bank;
	public int DisplayNumberType;

	public List<ECSystems> Buildings = new List<ECSystems>();

	// Use this for initialization
	void Start () 
	{
		AddBuildings();
	}


	void AddBuildings()
	{
		Buildings.Add (new ECSystems ("Power Plant", "A basic power plant", 1, 0.01f, 10, 0, 100, 1,0));
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void Timers()
	{
		for (int i = 0; i < Buildings.Count; i++)
		{
			Buildings [i].Timer -= Time.deltaTime;
			if (Buildings [i].Timer <= 0)
			{
				PowerUnit += Buildings[i].Production * Buildings[i].Owned;
				Buildings [i].Timer = Buildings [i].Cooldown;
			}
		}
	}

	void PriceCheck()
	{
		for (int i = 0; i < Buildings.Count; i++)
		{
			if (Buildings [i].Owned >= 1)
			{
				Buildings [i].Price = Buildings [i].Price * Buildings [i].Effeciency;
				Buildings[i].Price = Mathf.Round(Buildings[i].Price * 100f) / 100f;
			}
		}
	}

	void CashCheck()
	{
		Cash = Mathf.Round(Cash * 100f) / 100f;
	}

	public static string NumberFormat(double num)
	{
		double numStr;
		string suffix;

		if (num < 1000d)
		{
			numStr = num;
			suffix = "";
		}
		else if(num < 10000000d)
		{
			numStr = num / 1000d;
			suffix = "K";
		}
		else if(num < 10000000000d)
		{
			numStr = num / 1000000d;
			suffix = "M";
		}
		else
		{
			numStr = num / 1000000000d;
			suffix = "B";
		}
		return numStr.ToString () + suffix;
	}

	public void MiniGameRender()
	{
		Timers();
		CashCheck();
		switch (DisplayNumberType)
		{
		case 0:
			DisplayCash = Cash;
			GUI.Label (new Rect (25, 25, 1000, 22), "Cash: " + DisplayCash.ToString ("n2"));
			break;
		case 1:
			GUI.Label (new Rect (25, 25, 1000, 22), "Cash: " + NumberFormat(DisplayCash));
			break;
		}
		if(GUI.Button(new Rect(50,50,200,50),Buildings[0].Name + " " + Buildings[0].Price.ToString ("n2")))
		{
			if (Cash >= Buildings[0].Price) 
			{
				Buildings [0].Owned += 1;
				Cash -= Buildings [0].Price;
				Buildings [0].Effeciency += 0.0001f * Buildings[0].Owned;
				PriceCheck();
			}
		}
	}

	public void GameRender()
	{
		switch (MenuSelector)
		{
		case "Main Menu":
			MiniGameRender();
			break;
		}
	}
}
