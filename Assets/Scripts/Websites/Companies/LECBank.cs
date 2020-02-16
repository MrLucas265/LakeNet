using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class LECBank : MonoBehaviour 
{
	private GameObject Computer;
	private InternetBrowser ib;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public bool RemeberMe;

	public string UsrName;
	public string password;
	public string SiteAdminPass;

	public int Index;

	public bool logged;

	public List<string> AccountName = new List<string>();
	public List<string> AccountNumbers = new List<string>();
	public List<string> AccountPass = new List<string>();
	public List<float> AccountBalance = new List<float>();
	public List<float> CreditRating = new List<float>();
	public List<float> Loan = new List<float>();

	public float LoanIntrest;
	public float DepoIntrest;

	public float Ammount;
	public int AccNoFrom;
	public int AccNoTo;

	public int SelectedAccount;

	// Use this for initialization
	void Start () 
	{
		Computer = GameObject.Find("Applications");
		ib = Computer.GetComponent<InternetBrowser>();
		//WebSearch();
		//PlayerInfo();

	}

	// Update is called once per frame
	void Update () 
	{

	}

//	void PlayerInfo()
//	{
////		AccountName.Add(GameControl.control.ProfileName);
////		AccountNumbers.Add("0");
////		AccountPass.Add(GameControl.control.LoginPass[0]);
////		AccountBalance.Add(0);
////		CreditRating.Add(0);
////		Loan.Add(0);
//		GameControl.control.AccountNumber.Add("184678");
//		GameControl.control.Bank.Add("192.168.178.91");
//		GameControl.control.Balance.Add(0);
//
//		GameControl.control.AccountNumber.Add("184678");
//		GameControl.control.Bank.Add("192.168.178.91");
//		GameControl.control.Balance.Add(0);
//
//		GameControl.control.AccountNumber.Add("184678");
//		GameControl.control.Bank.Add("192.168.178.91");
//		GameControl.control.Balance.Add(0);
//
//		GameControl.control.AccountNumber.Add("184678");
//		GameControl.control.Bank.Add("192.168.178.91");
//		GameControl.control.Balance.Add(0);
//	}

	void WebSearch()
	{
		ib = Computer.GetComponent<InternetBrowser>();
	}

	void AccountCheck()
	{
		for (int i = 0; i < GameControl.control.MyBankDetails.Count; i++) 
		{
			if (GameControl.control.MyBankDetails [i].BankName == "LEC Bank")
			{
				if(UsrName == GameControl.control.MyBankDetails[i].AccountNumber && password == GameControl.control.MyBankDetails[i].AccountPass)
				{
					SelectedAccount = i;
					logged = true;

					ib.showAddressBar = false;
					ib.AddressBar = "www.lecbank.com/account";
				}
			}

			if (i >= GameControl.control.MyBankDetails.Count) 
			{
				GUI.Label(new Rect(3, 100, 500, 500), "ERROR: Account details do not match please try again.");
			}
		}
	}

	public void RenderSite()
	{
		switch(ib.AddressBar)
		{
		case "www.lecbank.com": 
			if(GUI.Button(new Rect(10,75,100,20),"Public"))
			{
				ib.AddressBar = "www.lecbank.com/public";
			}  
			if(GUI.Button(new Rect(10,120,100,20),"Sign in"))
			{
				ib.AddressBar = "www.lecbank.com/accountlogin";
			}    
			break;

		case "www.lecbank.com/accountlogin": 
			UsrName = GUI.TextField(new Rect(115, 55, 200, 20), UsrName, 500);
			password = GUI.TextField(new Rect(115, 75, 200, 20), password, 500);
			GUI.Label(new Rect(3, 55, 500, 500), "Account Number: ");
			GUI.Label(new Rect(3, 75, 500, 500), "Password: ");

//			if(AccountNumbers.Contains(UsrName) && AccountPass.Contains(password))
//			{
//				if(GUI.Button(new Rect(10,125,100,20),"Login"))
//				{
//					ib.showAddressBar = false;
//					logged = true;
//					ib.AddressBar = "www.lecbank.com/account";
//					//log.JDLog.Add(GameControl.control.fullip);
//					GameControl.control.Sites.Add("www.lecbank.com");
//					Index = AccountNumbers.IndexOf(UsrName);
//				}
//			}

			if(GUI.Button(new Rect(10,125,120,20),"Login"))
			{
				AccountCheck();
			}

			if(GUI.Button(new Rect(10,150,120,20),"Auto Login " + RemeberMe))
			{
				RemeberMe =! RemeberMe;
			}

			if(RemeberMe == true)
			{
				UsrName = GameControl.control.MyBankDetails [SelectedAccount].AccountNumber;
				password = GameControl.control.MyBankDetails [SelectedAccount].AccountPass;
			}


			if (GUI.Button(new Rect(245,30,50,20), "Back"))
			{
				ib.AddressBar = "www.lecbank.com";
			} 
			break;

		case "www.lecbank.com/account":
			if (GUI.Button (new Rect (10, 40, 100, 20), "Accounts"))
			{
				ib.AddressBar = "www.lecbank.com/accountinfo";
			}

			//if (GUI.Button (new Rect (10, 60, 100, 20), "Transactions"))
			//{
			//	ib.AddressBar = "www.lecbank.com/transactions";
			//}

			if (GUI.Button (new Rect (10, 80, 100, 20), "Loans"))
			{
				ib.AddressBar = "www.lecbank.com/loans";
			}

			if (GUI.Button (new Rect (10, 100, 100, 20), "Transfer"))
			{
				ib.AddressBar = "www.lecbank.com/transfer";
			}

			if (GUI.Button (new Rect (10, 120, 100, 20), "Log Out"))
			{
				ib.AddressBar = "www.lecbank.com/accountlogin";
				ib.showAddressBar = true;
			}
			break;

		case "www.lecbank.com/accountinfo":
			if (GUI.Button(new Rect(245,30,50,20), "Back"))
			{
				ib.AddressBar = "www.lecbank.com/account";
			} 

			GUI.TextField(new Rect(5,60,300,20),"Account Name: " + GameControl.control.MyBankDetails[SelectedAccount].AccountName);
			GUI.TextField(new Rect(5,80,300,20),"Account Number: " + GameControl.control.MyBankDetails[SelectedAccount].AccountNumber);
			GUI.TextField(new Rect(5,100,300,20),"Balance: " + GameControl.control.MyBankDetails[SelectedAccount].AccountBalance);
			GUI.TextField(new Rect(5,120,300,20),"Loans: " + GameControl.control.MyBankDetails[SelectedAccount].Loan);
			GUI.TextField(new Rect(5,140,300,20),"Credit Rating: " + GameControl.control.MyBankDetails[SelectedAccount].CreditRating);
			break;

		case "www.lecbank.com/loans":
			if (GUI.Button (new Rect (245, 30, 50, 20), "Back")) {
				ib.AddressBar = "www.lecbank.com/account";
			}

			GUI.TextField (new Rect (5, 100, 300, 20), "Balance: " + GameControl.control.MyBankDetails[SelectedAccount].AccountBalance);
			GUI.TextField (new Rect (5, 120, 300, 20), "Loans: " + GameControl.control.MyBankDetails[SelectedAccount].Loan);
			GUI.TextField (new Rect (5, 140, 300, 20), "Rates: " + GameControl.control.MyBankDetails[SelectedAccount].LoanIntrest);;

			string AmmountString = "";

			AmmountString = GUI.TextField (new Rect (5, 180, 50, 20), Ammount.ToString ());
			AmmountString = Regex.Replace (AmmountString, @"[^0-9 ]", "");
			Ammount = float.Parse (AmmountString);

			if (Ammount >= 2147483647)
			{
				AmmountString = "2147483647";
				Ammount = 2147483647;
			}

//			if (string.IsNullOrEmpty (AmmountString)) 
//			{
//				return 0;
//			}
//			else 
//			{
//				return System.Convert.ToInt32 (AmmountString);
//			}

			break;

		case "www.lecbank.com/transfer":
			if (GUI.Button (new Rect (245, 30, 50, 20), "Back")) 
			{
				ib.AddressBar = "www.lecbank.com/account";
			}

			//Ammount = GUI.TextField(new Rect(5,60,100,20),Ammount);
			//AccNoFrom = GUI.TextField(new Rect(5,80,100,20),AccNoFrom);
			//AccNoTo = GUI.TextField(new Rect(5,100,100,20),AccNoTo);
			break;
		}
	}
}
