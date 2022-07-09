using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BankAccountsSystem
{
	public string AccIP;
	public string BankName;
	public string AccountName;
	public string AccountNumber;
	public string AccountPass;
	public float AccountBalance;
	public float CreditRating;
	public float Loan;
	public float MaxLoan;
	public float LoanIntrest;
	public float AccountIntrest;
	public bool Primary;
	public bool PlayerKnown;
	public List<BankLogsSystem> Logs = new List<BankLogsSystem>();

	public BankAccountsSystem(string accip, string bankname, string accountname, string accountnumber, string accountpass, float accountbalance, float creditrating, float loan,float maxloan, float loanintrest, float accountintrest,bool primary,bool playerknown, List<BankLogsSystem> logs)
	{
		AccIP = accip;
		BankName = bankname;
		AccountName = accountname;
		AccountNumber = accountnumber;
		AccountPass = accountpass;
		AccountBalance = accountbalance;
		CreditRating = creditrating;
		Loan = loan;
		MaxLoan = maxloan;
		LoanIntrest = loanintrest;
		AccountIntrest = accountintrest;
		Primary = primary;
		PlayerKnown = playerknown;
		Logs = logs;
	}
}