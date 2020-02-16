using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BankSystem
{
	public string BankIP;
	public string BankName;
	public string AccountName;
	public string AccountNumber;
	public string AccountPass;
	public float AccountBalance;
	public float CreditRating;
	public float Loan;
	public float LoanIntrest;
	public float AccountIntrest;

	public BankSystem (string bankip, string bankname, string accountname, string accountnumber, string accountpass, float accountbalance, float creditrating, float loan, float loanintrest, float accountintrest)
	{
		BankIP = bankip;
		BankName = bankname;
		AccountName = accountname;
		AccountNumber = accountnumber;
		AccountPass = accountpass;
		AccountBalance = accountbalance;
		CreditRating = creditrating;
		Loan = loan;
		LoanIntrest = loanintrest;
		AccountIntrest = accountintrest;
	}
}