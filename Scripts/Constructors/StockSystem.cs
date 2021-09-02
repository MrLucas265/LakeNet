using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StockSystem
{
	public List<StockPortfolioSystem> TransactionHistory = new List<StockPortfolioSystem>();
	public List<StockPortfolioSystem> Portfolio = new List<StockPortfolioSystem>();

	public StockSystem(List<StockPortfolioSystem> transactionhistory, List<StockPortfolioSystem> portfolio)
	{
		TransactionHistory = transactionhistory;
		Portfolio = portfolio;
	}
}