using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BankSystem
{
	public string Name;
	public string IP;
	public List<BankAccountsSystem> Accounts = new List<BankAccountsSystem>();


	public BankSystem(string name, string ip, List<BankAccountsSystem> accounts)
	{
		Name = name;
		IP = ip;
		Accounts = accounts;
	}
}