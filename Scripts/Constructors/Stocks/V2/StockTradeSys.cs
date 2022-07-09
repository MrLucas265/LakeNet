using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StockTradeSys
{
	public string ExchangeName;
	public List<UACStockSystem> TradeAccounts = new List<UACStockSystem>();
	public List<StockInfoSys> Stocks = new List<StockInfoSys>();

	public StockTradeSys(string exchangename, List<UACStockSystem> tradeaccounts, List<StockInfoSys> stocks)
	{
		Stocks = stocks;
		TradeAccounts = tradeaccounts;
		ExchangeName = exchangename;
	}
}

