using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StockExchangeSystem
{
	public string Exchange;
	public string URL;

	public StockExchangeSystem(string exchange,string url)
	{
		Exchange = exchange;
		URL = url;
	}
}
