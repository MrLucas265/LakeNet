using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StockExchangeSystem
{
	public string Exchange;
	public string CurrentURL;
	public string URL;
	public bool RequiresSignin;

	public StockExchangeSystem(string exchange,string currenturl,string url,bool requiressignin)
	{
		Exchange = exchange;
		CurrentURL = currenturl;
		URL = url;
		RequiresSignin = requiressignin;
	}
}
