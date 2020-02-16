using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kingdomgame : MonoBehaviour 
{
	public string MenuSelection;
	// ACCOUNT INFO
	public bool DisplayNotfications;
	public string Username;
	public string Password;
	public bool LoggedIn;
	public int ProfileIndex;
	public bool ShowLoginButton;
	public bool ShowCreateAccount;
	//Timer
	public float Timer = 1;
	public float CoolDown = 1;

	void Start()
	{
		
	}

	void Timers()
	{
		if (Timer >= 0)
		{
			Timer -= Time.deltaTime;
		}

		if (Timer < 0)
		{
			KingClkSAL.kingsal.Save();
			Timer = CoolDown;
		}
	}

	void CurrencyCheck()
	{
		if (KingClkSAL.kingsal.NewPlayer == true) 
		{
			KingClkSAL.kingsal.CurrencyName.Add("Bronze Coins");
			KingClkSAL.kingsal.CurrencyName.Add("Silver Coins");
			KingClkSAL.kingsal.CurrencyName.Add("Gold Coins");
			KingClkSAL.kingsal.CurrencyName.Add("Wood");
			KingClkSAL.kingsal.CurrencyName.Add("Rock");
			KingClkSAL.kingsal.CurrencyName.Add("Wool");
			KingClkSAL.kingsal.CurrencyName.Add("Planks");
			KingClkSAL.kingsal.CurrencyName.Add("Stone");
			KingClkSAL.kingsal.CurrencyName.Add("Cloth");
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
			KingClkSAL.kingsal.CurrencyAmt.Add(0);
		}
	}

	void Lumber()
	{
		int CutWood = 0;
		if (GUI.Button (new Rect (100, 100, 50, 50), "Tree")) 
		{
			CutWood = KingClkSAL.kingsal.CurrencyName.IndexOf ("Wood");
			KingClkSAL.kingsal.CurrencyAmt[CutWood] += 1;
		}
	}

	void MainMenu()
	{
		if (GUI.Button (new Rect (0, 100, 50, 50), "Lumberyard")) 
		{
			MenuSelection = "Lumberyard";
		}

		if (GUI.Button (new Rect (200, 100, 50, 50), "Save")) 
		{
			KingClkSAL.kingsal.Save();
		}

		if (GUI.Button (new Rect (150, 100, 50, 50), "Load")) 
		{
			KingClkSAL.kingsal.Load();
		}
	}

	public void GameRender()
	{
		if (MenuSelection == "") 
		{
			MenuSelection = "Main Menu";
		}

		switch (MenuSelection) 
		{
		case "Main Menu":
			MainMenu();
			break;

		case "Lumberyard":
			Lumber();
			break;
		}
	}
}
