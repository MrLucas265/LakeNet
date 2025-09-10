using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class LECBank : MonoBehaviour
{
	private GameObject Computer;
	private GameObject System;
	private InternetBrowser ib;
	private CLICommandsV2 clic;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public bool RemeberMe;

	public string UsrName;
	public string password;
	public string SiteAdminPass;

	public string BankIPTo;

	public int Index;

	public bool logged;

	public BankAccountsSystem LoggedInAs;

	public float LoanIntrest;
	public float DepoIntrest;

	public float Ammount;
	public float Ammount1;
	public string AccNoFrom;
	public string AccNoTo;

	public int SelectedAccount;
	public int SelectedBank;

	public int CompanyID;

	public Color32 buttonColor = new Color32(0, 0, 0, 0);
	public Color32 fontColor = new Color32(0, 0, 0, 0);

	public List<RemoteFileSystem> PageFile1 = new List<RemoteFileSystem>();

	// Use this for initialization
	void Start()
	{
		Computer = GameObject.Find("Applications");
		System = GameObject.Find("System");
		ib = Computer.GetComponent<InternetBrowser>();
		clic = System.GetComponent<CLICommandsV2>();
		BankIPTo = "";
		CompanyID = -1;
		LoadPresetColors();
		//WebSearch();
		//PlayerInfo();

	}

	// Update is called once per frame
	void Update()
	{
		if (CompanyID == -1)
		{
			for (int i = 0; i < GameControl.control.CompanyServerData.Count; i++)
			{
				if (GameControl.control.CompanyServerData[i].Name == "LEC Bank")
				{
					CompanyID = i;
				}
			}
		}
	}

	void LoadPresetColors()
	{
		//rgb1.r = 255;
		//rgb1.g = 255;
		//rgb1.b = 255;
		//rgb1.a = 255;

		buttonColor.r = 255;
		buttonColor.g = 255;
		buttonColor.b = 255;
		buttonColor.a = 255;

		fontColor.r = 255;
		fontColor.g = 255;
		fontColor.b = 255;
		fontColor.a = 255;
	}

	public void RenderSite()
	{
		GUI.backgroundColor = buttonColor;
		GUI.contentColor = fontColor;
		NewWebsiteStuff();
	}

	void NewWebsiteStuff()
	{
		RefreshPage();
		if (PageFile1.Count > 0)
		{
			if (ib.Request == true)
			{
				Request();
			}


			for (int i = 0; i < PageFile1.Count; i++)
			{
				if (GUI.Button(new Rect(10, 35 + 30 * i, 100, 22), PageFile1[i].Name))
				{
					ib.AddressBar = PageFile1[i].Target;
				}
			}
		}
		WebsiteStuff();
	}

	void RefreshPage()
	{
		PageFile1.RemoveRange(0, PageFile1.Count);
		for (int i = 0; i < GameControl.control.CompanyServerData[CompanyID].WebPages.Count; i++)
		{
			if (GameControl.control.CompanyServerData[CompanyID].WebPages[i].Location == ib.AddressBar)
			{
				if (!PageFile1.Contains(GameControl.control.CompanyServerData[CompanyID].WebPages[i]))
				{
					PageFile1.Add(GameControl.control.CompanyServerData[CompanyID].WebPages[i]);
				}
			}
		}
	}

	void Request()
	{
		if (PageFile1.Count > 0)
		{
			for (int i = 0; i < PageFile1.Count; i++)
			{
				clic.PastCommands.Add("#" + i + " " + PageFile1[i].Name +  " | " + PageFile1[i].Target);
			}
		}

		ib.Request = false;
	}

	//void AddPlayerBank()
	//{
	//	GameControl.control.StoredLogins.Add(new LoginSystem("LEC Bank", StringGenerator.RandomNumberChar(6, 6), StringGenerator.RandomMixedChar(15, 15), 0));
	//	StartingBank.Add(new BankAccountsSystem(IPAddress, "LEC Bank", GameControl.control.StoredLogins[0].Username, GameControl.control.StoredLogins[0].Username, GameControl.control.StoredLogins[0].Password, 0, 1, 0, 0, 0, 1, true, true, BlankBankLogs));
	//	GameControl.control.BankData.Add(new BankSystem("LEC Bank", "192.168.56.91", StartingBank));
	//	string EmailSender = "14K3N37 VMB";
	//	string EmailSubject = "LEC Bank Account Details";
	//	string EmailContent = "Due to being a new member of 14K3N37 VMB we have created a new LEC Account for you as you complete contracts funds will automatically go there." +
	//		"\n" + "URL: www.lecbank.com" +
	//		//"\n" + "Account Name: " + GameControl.control.StoredLogins[0].Username +
	//		"\n" + "Account Number: " + GameControl.control.StoredLogins[0].Username +
	//		"\n" + "Account Password: " + GameControl.control.StoredLogins[0].Password;

	//	GameControl.control.EmailData.Add(new EmailSystem(EmailSubject, EmailSender, InitalPlanDate.TodaysDate, EmailContent, null, 0, 0, 0, false, EmailSystem.EmailType.New));
	//}

	void AccountCheck()
	{
		for (int j = 0; j < GameControl.control.CompanyServerData[CompanyID].BankDetails.Count; j++)
		{
			//if (UsrName == GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountName && password == GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountPass && GameControl.control.Gateway.InstalledModem[0].ModemIP == GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccIP)
			//{
			//	SelectedBank = CompanyID;
			//	SelectedAccount = j;
			//	logged = true;
			//	LoggedInAs = GameControl.control.CompanyServerData[CompanyID].BankDetails[j];
			//	ib.showAddressBar = false;
			//	ib.AddressBar = "www.lecbank.com/account";
			//}


			if (j >= GameControl.control.CompanyServerData[CompanyID].BankDetails.Count)
			{
				GUI.Label(new Rect(3, 100, 500, 500), "ERROR: Account details do not match please try again.");
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

	public void WebsiteStuff()
	{
		switch (ib.AddressBar)
		{
			case "www.lecbank.com":
				if (GUI.Button(new Rect(10, 200, 100, 20), "Store Account"))
				{
					//GameControl.control.CompanyServerData[CompanyID].BankDetails.Add(new BankAccountsSystem(GameControl.control.Gateway.InstalledModem[0].ModemIP, "LEC Bank", UsrName, StringGenerator.RandomNumberChar(6, 6), password, 0, 1, 0, 0, 0, 1, true, true, null));
					ib.AddressBar = "www.lecbank.com/accountlogin";
				}
				break;

			case "www.lecbank.com/createaccount":

				UsrName = GUI.TextField(new Rect(115, 55, 200, 20), UsrName, 500);
				password = GUI.PasswordField(new Rect(115, 75, 200, 20), password, "*"[0], 500);

				if (GUI.Button(new Rect(10, 120, 100, 20), "Create Account"))
				{
					//GameControl.control.CompanyServerData[CompanyID].BankDetails.Add(new BankAccountsSystem(GameControl.control.Gateway.InstalledModem[0].ModemIP, "LEC Bank", UsrName, StringGenerator.RandomNumberChar(6, 6), password, 0, 1, 0, 0, 0, 1, true, true, null));
					ib.AddressBar = "www.lecbank.com/accountlogin";
				}
				break;

			case "www.lecbank.com/accountlogin":
				UsrName = GUI.TextField(new Rect(115, 55, 200, 20), UsrName, 500);
				password = GUI.PasswordField(new Rect(115, 75, 200, 20), password, "*"[0], 500);
				GUI.Label(new Rect(3, 55, 500, 500), "Account Number: ");
				GUI.Label(new Rect(3, 75, 500, 500), "Password: ");

				if (CompanyID != -1)
				{
					if (GUI.Button(new Rect(10, 125, 120, 20), "Login"))
					{
						AccountCheck();
					}

					if (GUI.Button(new Rect(10, 150, 120, 20), "Auto Login " + RemeberMe))
					{
						for (int j = 0; j < GameControl.control.CompanyServerData[CompanyID].BankDetails.Count; j++)
						{
							if (UsrName == GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountNumber)
							{
								password = GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountPass;
							}
						}
					}
				}
				break;

			case "www.lecbank.com/account":
				if (GUI.Button(new Rect(10, 120, 100, 20), "Log Out"))
				{
					BankIPTo = "";
					AccNoTo = "";
					Ammount1 = 0;
					ib.AddressBar = "www.lecbank.com/accountlogin";
					ib.showAddressBar = true;
				}
				break;

			case "www.lecbank.com/accountinfo":

				GUI.TextField(new Rect(5, 60, 300, 20), "Account Name: " + LoggedInAs.AccountName);
				GUI.TextField(new Rect(5, 80, 300, 20), "Account Number: " + LoggedInAs.AccountNumber);
				GUI.TextField(new Rect(5, 100, 300, 20), "Balance: " + LoggedInAs.AccountBalance);
				GUI.TextField(new Rect(5, 120, 300, 20), "Loans: " + LoggedInAs.Loan);
				GUI.TextField(new Rect(5, 140, 300, 20), "Credit Rating: " + LoggedInAs.CreditRating);
				break;

			case "www.lecbank.com/loans":

				GUI.TextField(new Rect(5, 100, 300, 20), "Balance: " + LoggedInAs.AccountBalance);
				GUI.TextField(new Rect(5, 120, 300, 20), "Loans: " + LoggedInAs.Loan);
				GUI.TextField(new Rect(5, 140, 300, 20), "Rates: " + LoggedInAs.LoanIntrest);

				string AmmountString = "";

				AmmountString = GUI.TextField(new Rect(5, 180, 150, 20), Ammount.ToString());
				AmmountString = Regex.Replace(AmmountString, @"[^0-9 ]", "");
				Ammount = float.Parse(AmmountString);

				if (Ammount >= 2147483647)
				{
					AmmountString = "2147483647";
					Ammount = 2147483647;
				}

				if (GUI.Button(new Rect(2, 220, 50, 20), "Take Out Loan"))
				{
					if (Ammount > 0)
					{
						for (int j = 0; j < GameControl.control.CompanyServerData[CompanyID].Accounts.Count; j++)
						{
							if (GameControl.control.CompanyServerData[CompanyID].IP == LoggedInAs.AccIP)
							{
								if (GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountNumber == LoggedInAs.AccountNumber)
								{
									if (GameControl.control.CompanyServerData[CompanyID].BankDetails[j].Loan < GameControl.control.CompanyServerData[CompanyID].BankDetails[j].MaxLoan)
									{
										GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountBalance += Ammount;
										GameControl.control.CompanyServerData[CompanyID].BankDetails[j].Loan += Ammount;

										string DateTime = PersonController.control.Global.DateTime.CurrentTime + " " + PersonController.control.Global.DateTime.TodaysDate;
										string FromBankIP = GameControl.control.BankData[SelectedBank].IP;
										string FromBankAccountNumber = GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountNumber;

										GameControl.control.CompanyServerData[CompanyID].BankDetails[j].Logs.Add(new BankLogsSystem("LEC Bank Credit", "", LoggedInAs.AccIP, LoggedInAs.AccountName, "Loan Deposit", Ammount, DateTime));
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
						for (int j = 0; j < GameControl.control.CompanyServerData[CompanyID].BankDetails.Count; j++)
						{
							if (GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountNumber == LoggedInAs.AccountNumber)
							{
								if (GameControl.control.CompanyServerData[CompanyID].BankDetails[j].Loan > 0)
								{
									GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountBalance -= Ammount;
									GameControl.control.CompanyServerData[CompanyID].BankDetails[j].Loan -= Ammount;
									string DateTime = PersonController.control.Global.DateTime.CurrentTime + " " + PersonController.control.Global.DateTime.TodaysDate;
									string FromBankIP = GameControl.control.CompanyServerData[CompanyID].IP;
									string FromBankAccountNumber = GameControl.control.CompanyServerData[CompanyID].BankDetails[j].AccountNumber;
									GameControl.control.CompanyServerData[CompanyID].BankDetails[j].Logs.Add(new BankLogsSystem("LEC Bank Credit", "", LoggedInAs.AccIP, LoggedInAs.AccountName, "Loan Deposit", Ammount, DateTime));
								}
							}
						}
					}
				}

				break;

			case "www.lecbank.com/transfer":


				string AmountString = "";


				GUI.TextField(new Rect(5, 60, 90, 20), "Acc # (From):");
				GUI.TextField(new Rect(100, 60, 100, 20), LoggedInAs.AccountNumber);

				GUI.TextField(new Rect(5, 100, 90, 20), "Bank IP (To):");
				BankIPTo = GUI.TextField(new Rect(100, 100, 100, 20), BankIPTo);

				GUI.TextField(new Rect(5, 130, 90, 20), "Acc # (To):");
				AccNoTo = GUI.TextField(new Rect(100, 130, 100, 20), AccNoTo);

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
					if (Ammount1 > 0)
					{
						for (int i = 0; i < GameControl.control.CompanyServerData.Count; i++)
						{
							for (int j = 0; j < GameControl.control.CompanyServerData[i].BankDetails.Count; j++)
							{
								if (GameControl.control.CompanyServerData[i].IP == BankIPTo)
								{
									if (GameControl.control.CompanyServerData[i].BankDetails[j].AccountNumber == AccNoTo)
									{
										if (LoggedInAs.AccountBalance >= Ammount1)
										{
											GameControl.control.CompanyServerData[i].BankDetails[j].AccountBalance += Ammount1;
											GameControl.control.CompanyServerData[SelectedBank].BankDetails[SelectedAccount].AccountBalance -= Ammount1;

											string DateTime = PersonController.control.Global.DateTime.CurrentTime + " " + PersonController.control.Global.DateTime.TodaysDate;
											string FromBankIP = GameControl.control.CompanyServerData[SelectedBank].IP;
											string FromBankAccountNumber = GameControl.control.CompanyServerData[SelectedBank].BankDetails[SelectedAccount].AccountNumber;

											GameControl.control.CompanyServerData[SelectedBank].BankDetails[SelectedAccount].Logs.Add(new BankLogsSystem(FromBankIP, FromBankAccountNumber, BankIPTo, AccNoTo, "Withdraw", Ammount1, DateTime));
											GameControl.control.CompanyServerData[i].BankDetails[j].Logs.Add(new BankLogsSystem(FromBankIP, FromBankAccountNumber, BankIPTo, AccNoTo, "Deposit", Ammount1, DateTime));
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
