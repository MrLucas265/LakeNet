using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StockTransactionHistorySystem
{
    public string Exchange;
    public string Abv;
    public string Company;
    public string Ticket;
    public string PDate;
    public float Price;
    public int Ammount;

    public StockTransactionHistorySystem(string exchange, string abv, string company, string ticket, string pdate, float price, int ammount)
    {
        Exchange = exchange;
        Abv = abv;
        Company = company;
        Ticket = ticket;
        PDate = pdate;
        Price = price;
        Ammount = ammount;
    }
}
