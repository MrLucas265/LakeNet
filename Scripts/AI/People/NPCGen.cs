using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGen : MonoBehaviour 
{
	public List<string> FirstName = new List<string>();
	public List<string> MiddleName = new List<string>();
	public List<string> LastName = new List<string>();

	public List<string> FullName = new List<string>();

	public List<string> Proffesion = new List<string>();

	public List<string> AccountNumber = new List<string>();
	public List<string> AccountPass = new List<string>();
	public List<float> BankBalance = new List<float>();

	public List<string> PhoneNumber = new List<string>();

	public List<string> Address = new List<string>();

	public int Selector;

	public int MaxNPCs;

	private GameObject Computer;
	private Defalt def;
	private string Name;

	// Use this for initialization
	void Start () 
	{
		Computer = GameObject.Find("Computer");
		WebSearch();
		ProffesionGen();
		AddressGen();
	}
		
	void WebSearch()
	{
		def = Computer.GetComponent<Defalt>();
		FirstNameGen();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (FullName.Count < MaxNPCs)
		{
			CreateFullName();
		}
	}

	void FirstNameGen()
	{
		FirstName.Add("Jack");
		FirstName.Add ("Lake");
		FirstName.Add ("Daisy");
		FirstName.Add ("Kate");
		FirstName.Add ("Rebecca");
		FirstName.Add ("Crystal");
		FirstName.Add ("Krystal");
		FirstName.Add ("Kathryn");
		FirstName.Add ("Pike");
		FirstName.Add ("Willson");
		FirstName.Add ("Willy");
		FirstName.Add ("Wayne");
		FirstName.Add ("Frank");
		FirstName.Add ("Franky");
		FirstName.Add ("Heather");
		FirstName.Add ("April");
		FirstName.Add ("June");
		FirstName.Add ("Autum");
		FirstName.Add ("Debby");
		FirstName.Add ("Georgia");
		FirstName.Add ("Bill");
		FirstName.Add ("David");
		FirstName.Add ("Lucas");
		FirstName.Add ("Luke");
		FirstName.Add ("Sky");
		FirstName.Add ("Amber");
		FirstName.Add ("Jackson");
		LastNameGen();
	}

	void LastNameGen()
	{
		LastName.Add("Bell");
		LastName.Add ("Ball");
		LastName.Add ("Lake");
		LastName.Add ("Cullen");
		LastName.Add ("Hillry");
		LastName.Add ("Torrento");
		LastName.Add ("Tully");
		LastName.Add ("Reber");
		LastName.Add ("Piper");
		LastName.Add ("Ferro");
		LastName.Add ("Maskill");
		LastName.Add ("Winfrey");
		LastName.Add ("Stallio");
		LastName.Add ("Holli");
		LastName.Add ("Yuri");
		LastName.Add ("Tucker");
		LastName.Add ("Lory");
		LastName.Add ("Cory");
		LastName.Add ("Gabby");
		LastName.Add ("Allison");
		LastName.Add ("Dory");
		LastName.Add ("Jary");
	}


	void CreateFullName()
	{
		for(int i = 0; i < MaxNPCs; i++)
		{
			Name = FirstName[Random.Range(1,10)] + " " + LastName[Random.Range(1,10)];

			if (!FullName.Contains (Name))
			{
				FullName.Add (Name);
				CreateBankAccount();
				CreatePhoneNumber();
			}
		}
	}

	void CreateBankAccount()
	{
		AccountNumber.Add (StringGenerator.RandomNumberChar(12,12));
		AccountPass.Add (StringGenerator.RandomMixedChar(4,12));
		BankBalance.Add(0);
	}

	void CreatePhoneNumber()
	{
		PhoneNumber.Add (StringGenerator.RandomNumberChar(8,8));
	}

	void AddressGen()
	{
		Address.Add("America");
		Address.Add("Euroupe");
		Address.Add("Australia");
		Address.Add("China");
		Address.Add("Japan");
		Address.Add("Africa");
		Address.Add("South America");
	}

	void ProffesionGen()
	{
		Proffesion.Add("Hacker");
		Proffesion.Add("Secuirty");
		Proffesion.Add("Worker");
		Proffesion.Add("Student");
		Proffesion.Add("Goverment");
		Proffesion.Add("Stock Broker");
	}
}
