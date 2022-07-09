using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StockInfoSys
{
	public string Company;
	public string Exchange;
	public string Abv;
	public string Ticket;
	public string Currency;
	public string Type;
	public string Industry;
	public float Low;
	public float High;
	public float PPrice;
	public float CurPrice;
	public float ChangeVal;
	public float ChangePercent;

	public StockInfoSys(string company, string exchange, string abv, string ticket, string currency, string type, string industry, float low, float high, float pprice, float curprice, float changeval, float changepercent)
	{
		Company = company;
		Exchange = exchange;
		Abv = abv;
		Ticket = ticket;
		Currency = currency;
		Type = type;
		Industry = industry;
		Low = low;
		High = high;
		PPrice = pprice;
		CurPrice = curprice;
		ChangeVal = changeval;
		ChangePercent = changepercent;
	}
}
