using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BankLogsSystem
{
	public string FromBankIP;
	public string FromAccountNumber;
	public string ToBankIP;
	public string ToAccountNumber;
	public string TransactionType;
	public float TransfferAmount;
	public string Date;

	public BankLogsSystem(string frombankip, string fromaccountnumber, string tobankip, string toaccountnumber, string transactiontype, float transfferamount,string date)
	{
		FromBankIP = frombankip;
		FromAccountNumber = fromaccountnumber;
		ToBankIP = tobankip;
		ToAccountNumber = toaccountnumber;
		TransactionType = transactiontype;
		TransfferAmount = transfferamount;
		Date = date;
	}
}