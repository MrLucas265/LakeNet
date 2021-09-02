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

	public string BankIPTo;

	public int Index;

	public bool logged;

	public BankSystem LoggedInAs;

	public float LoanIntrest;
	public float DepoIntrest;

	public float Ammount;
	public float Ammount1;
	public string AccNoFrom;
	public string AccNoTo;

	public int SelectedAccount;
	public int SelectedBank;

	// Use this for initialization
	void Start () 
	{
		Computer = GameObject.Find("Applications");
		ib = Computer.GetComponent<InternetBrowser>();
		BankIPTo = "";
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
		for(int i = 0; i < GameControl.control.BankData.Count; i++)
		{
			if(GameControl.control.BankData[i].Name == "LEC Bank")
			{
				SelectedBank = i;
				for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
				{
					if (UsrName == GameControl.control.BankData[i].Accounts[j].AccountName && password == GameControl.control.BankData[i].Accounts[j].AccountPass && GameControl.control.Gateway.InstalledModem[0].ModemIP == GameControl.control.BankData[i].Accounts[j].AccIP)
					{
						SelectedAccount = j;
						logged = true;
						LoggedInAs = GameControl.control.BankData[SelectedBank];
						ib.showAddressBar = false;
						ib.AddressBar = "www.lecbank.com/account";
					}


					if (j >= GameControl.control.BankData.Count)
					{
						GUI.Label(new Rect(3, 100, 500, 500), "ERROR: Account details do not match please try again.");
					}
				}
			}
		}
	}

	//void AccountCheckAI()
	//{
	//	for (int i = 0; i < PersonController.control.People.Count; i++)
	//	{
	//		for (int j = 0; j < PersonController.control.People[i].BankDetails.Accounts.Count; j++)
	//		{
	//			if (PersonController.control.People[i].BankDetails.Accounts[j].BankName == "LEC Bank")
	//			{
	//				SelectedBank = j;
	//				if (UsrName == PersonController.control.People[i].BankDetails.Accounts[j].AccountName && password == PersonController.control.People[i].BankDetails.Accounts[j].AccountPass && GameControl.control.Gateway.InstalledModem[0].ModemIP == PersonController.control.People[i].BankDetails.Accounts[j].AccIP)
	//				{
	//					SelectedAccount = j;
	//					logged = true;
	//					LoggedInAs = GameControl.control.BankData[SelectedBank];
	//					ib.showAddressBar = false;
	//					ib.AddressBar = "www.lecbank.com/account";
	//				}
	//			}
	//		}
	//	}
	//}

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
			password = GUI.PasswordField(new Rect(115, 75, 200, 20), password,"*"[0],500);
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
				for (int i = 0; i < GameControl.control.BankData.Count; i++)
				{
					if(GameControl.control.BankData[i].Name == "LEC Bank")
					{
						for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
						{
							if (UsrName == GameControl.control.BankData[i].Accounts[j].AccountNumber)
							{
								password = GameControl.control.BankData[i].Accounts[j].AccountPass;
							}
						}
					}
				}
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
				BankIPTo = "";
				AccNoTo = "";
				Ammount1 = 0;
				ib.AddressBar = "www.lecbank.com/accountlogin";
				ib.showAddressBar = true;
			}
			break;

		case "www.lecbank.com/accountinfo":
			if (GUI.Button(new Rect(245,30,50,20), "Back"))
			{
				ib.AddressBar = "www.lecbank.com/account";
			} 

			GUI.TextField(new Rect(5,60,300,20),"Account Name: " + LoggedInAs.Accounts[SelectedAccount].AccountName);
			GUI.TextField(new Rect(5,80,300,20),"Account Number: " + LoggedInAs.Accounts[SelectedAccount].AccountNumber);
				GUI.TextField(new Rect(5,100,300,20),"Balance: " + LoggedInAs.Accounts[SelectedAccount].AccountBalance);
				GUI.TextField(new Rect(5,120,300,20),"Loans: " + LoggedInAs.Accounts[SelectedAccount].Loan);
				GUI.TextField(new Rect(5,140,300,20),"Credit Rating: " + LoggedInAs.Accounts[SelectedAccount].CreditRating);
				break;

		case "www.lecbank.com/loans":
			if (GUI.Button (new Rect (245, 30, 50, 20), "Back")) {
				ib.AddressBar = "www.lecbank.com/account";
			}

			GUI.TextField (new Rect (5, 100, 300, 20), "Balance: " + LoggedInAs.Accounts[SelectedAccount].AccountBalance);
			GUI.TextField (new Rect (5, 120, 300, 20), "Loans: " + LoggedInAs.Accounts[SelectedAccount].Loan);
			GUI.TextField (new Rect (5, 140, 300, 20), "Rates: " + LoggedInAs.Accounts[SelectedAccount].LoanIntrest);

			string AmmountString = "";

			AmmountString = GUI.TextField (new Rect (5, 180, 150, 20), Ammount.ToString ());
			AmmountString = Regex.Replace (AmmountString, @"[^0-9 ]", "");
			Ammount = float.Parse (AmmountString);

			if (Ammount >= 2147483647)
			{
				AmmountString = "2147483647";
				Ammount = 2147483647;
			}

			if (GUI.Button(new Rect(2, 220, 50, 20), "Take Out Loan"))
			{
				if (Ammount > 0)
				{
					for (int i = 0; i < GameControl.control.BankData.Count; i++)
					{
						for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
						{
							if (GameControl.control.BankData[i].IP == LoggedInAs.IP)
							{
								if (GameControl.control.BankData[i].Accounts[j].AccountNumber == LoggedInAs.Accounts[0].AccountNumber)
								{
									if (GameControl.control.BankData[i].Accounts[j].Loan < GameControl.control.BankData[i].Accounts[j].MaxLoan)
									{
										GameControl.control.BankData[i].Accounts[j].AccountBalance += Ammount;
										GameControl.control.BankData[i].Accounts[j].Loan += Ammount;
										string DateTime = GameControl.control.Time.CurrentTime + " " + GameControl.control.Time.TodaysDate;
										string FromBankIP = GameControl.control.BankData[SelectedBank].IP;
										string FromBankAccountNumber = GameControl.control.BankData[i].Accounts[j].AccountNumber;
										GameControl.control.BankData[i].Accounts[j].Logs.Add(new BankLogsSystem("LEC Bank Credit", "", LoggedInAs.IP, LoggedInAs.Name, "Loan Deposit", Ammount, DateTime));
									}
								}
							}
						}
					}
				}
			}

			if (GUI.Button(new Rect(102, 220, 75, 20), "Payback Loan"))
			{
				if (Ammount > 0)
				{
					for (int i = 0; i < GameControl.control.BankData.Count; i++)
					{
						for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
						{
							if (GameControl.control.BankData[i].IP == LoggedInAs.IP)
							{
								if (GameControl.control.BankData[i].Accounts[j].AccountNumber == LoggedInAs.Accounts[0].AccountNumber)
								{
									if (GameControl.control.BankData[i].Accounts[j].Loan > 0)
									{
										GameControl.control.BankData[i].Accounts[j].AccountBalance -= Ammount;
										GameControl.control.BankData[i].Accounts[j].Loan -= Ammount;
										string DateTime = GameControl.control.Time.CurrentTime + " " + GameControl.control.Time.TodaysDate;
										string FromBankIP = GameControl.control.BankData[SelectedBank].IP;
										string FromBankAccountNumber = GameControl.control.BankData[i].Accounts[j].AccountNumber;
										GameControl.control.BankData[i].Accounts[j].Logs.Add(new BankLogsSystem("LEC Bank Credit", "", LoggedInAs.IP, LoggedInAs.Name, "Loan Deposit", Ammount, DateTime));
									}
								}
							}
						}
					}
				}
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


			string AmountString = "";

				
			GUI.TextField(new Rect(5, 40, 90, 20), "Acc # (From):");
			GUI.TextField(new Rect(100, 40, 100, 20), LoggedInAs.Accounts[SelectedAccount].AccountNumber);

			GUI.TextField(new Rect(5, 80, 90, 20), "Bank IP (To):");
			BankIPTo = GUI.TextField(new Rect(100, 80, 100, 20), BankIPTo);

			GUI.TextField(new Rect(5, 100, 90, 20), "Acc # (To):");
			AccNoTo = GUI.TextField(new Rect(100, 100, 100, 20), AccNoTo);

			GUI.TextField(new Rect(5, 180, 90, 20), "Amount: ");
			AmountString = GUI.TextField(new Rect(100, 180, 150, 20), Ammount1.ToString());
			AmountString = Regex.Replace(AmountString, @"[^0-9 ]", "");
			Ammount1 = float.Parse(AmountString);

			if (Ammount1 >= 2147483647)
			{
				AmountString = "2147483647";
				Ammount1 = 2147483647;
			}

			if (GUI.Button(new Rect(2, 220, 50, 20), "Send"))
			{
				if(Ammount1 > 0)
				{
					for (int i = 0; i < GameControl.control.BankData.Count; i++)
					{
						for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
						{
							if (GameControl.control.BankData[i].IP == BankIPTo)
							{
								if (GameControl.control.BankData[i].Accounts[j].AccountNumber == AccNoTo)
								{
									if(GameControl.control.BankData[SelectedBank].Accounts[SelectedAccount].AccountBalance >= Ammount1)
									{
										GameControl.control.BankData[i].Accounts[j].AccountBalance += Ammount1;
										GameControl.control.BankData[SelectedBank].Accounts[SelectedAccount].AccountBalance -= Ammount1;
										string DateTime = GameControl.control.Time.CurrentTime + " " + GameControl.control.Time.TodaysDate;
										string FromBankIP = GameControl.control.BankData[SelectedBank].IP;
										string FromBankAccountNumber = GameControl.control.BankData[SelectedBank].Accounts[SelectedAccount].AccountNumber;
										GameControl.control.BankData[SelectedBank].Accounts[SelectedAccount].Logs.Add(new BankLogsSystem(FromBankIP, FromBankAccountNumber, BankIPTo, AccNoTo,"Withdraw", Ammount1,DateTime));
										GameControl.control.BankData[i].Accounts[j].Logs.Add(new BankLogsSystem(FromBankIP, FromBankAccountNumber, BankIPTo, AccNoTo, "Deposit", Ammount1, DateTime));
									}
								}
							}
						}
					}
				}
			}
			break;
		}
	}
}
