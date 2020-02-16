using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class KingClkSAL : MonoBehaviour 
{
	public static KingClkSAL kingsal;

	public string ProfilePath;
	public int ProfileID;

	//public List<string> ProfileName = new List<string>();
	public string ProfileName;
	public List<string> ProfilePass = new List<string>();
	public List<bool> NewAccount = new List<bool>();
	public List<float> Tools = new List<float>();
	public List<float> Health = new List<float>();
	public List<float> MaxHealth = new List<float>();
	public List<float> Progress = new List<float>();
	public List<string> CurrencyName = new List<string>();
	public List<float> CurrencyAmt = new List<float>();
	public List<string> BuildingName = new List<string>();
	public List<float> BuildingUpgrades = new List<float>();
	public List<string> KingdomNames = new List<string>();
	public List<float> KingdomTrades = new List<float>();
	public List<string> MineName = new List<string>();
	public List<float> MineAmmount = new List<float>();

	public bool NewPlayer;


	void Awake ()
	{
		ProfilePath = "C:/ProgramData/FakeNet/Minigames/KingdomClicker";
		//ProfileName = "kcsave";
		Awake1();
	}

	void Awake1()
	{
		if(kingsal == null)
		{
			DontDestroyOnLoad(gameObject);
			kingsal = this;
		}
		else if(kingsal != this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (ProfilePath + "/" + ProfileName + ".dat");
		KingdomProfileData data = new KingdomProfileData ();

		data.ProfilePath = ProfilePath;
		data.ProfileID = ProfileID;
		data.ProfileName = ProfileName;
		data.ProfilePass = ProfilePass;
		data.NewAccount = NewAccount;
		data.Health = Health;
		data.MaxHealth = MaxHealth;
		data.Progress = Progress;
		data.Tools = Tools;
		data.CurrencyName = CurrencyName;
		data.CurrencyAmt = CurrencyAmt;
		data.BuildingName = BuildingName;
		data.BuildingUpgrades = BuildingUpgrades;
		data.KingdomNames = KingdomNames;
		data.KingdomTrades = KingdomTrades;
		data.MineName = MineName;
		data.MineAmmount = MineAmmount;
		data.NewPlayer = NewPlayer;

		bf.Serialize (file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (ProfilePath + "/" + ProfileName + ".dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (ProfilePath + "/" + ProfileName + ".dat", FileMode.Open);
			KingdomProfileData data = (KingdomProfileData)bf.Deserialize (file);
			file.Close ();

			ProfilePath = data.ProfilePath;
			ProfileID = data.ProfileID;
			ProfileName = data.ProfileName;
			ProfilePass = data.ProfilePass;
			NewAccount = data.NewAccount;
			Health = data.Health;
			MaxHealth = data.MaxHealth;
			Progress = data.Progress;
			Tools = data.Tools;
			CurrencyName = data.CurrencyName;
			CurrencyAmt = data.CurrencyAmt;
			BuildingName = data.BuildingName;
			BuildingUpgrades = data.BuildingUpgrades;
			KingdomNames = data.KingdomNames;
			KingdomTrades = data.KingdomTrades;
			MineName = data.MineName;
			MineAmmount = data.MineAmmount;
			NewPlayer = data.NewPlayer;
		}
	}

	[Serializable]
	class KingdomProfileData
	{
		public string ProfilePath;
		public int ProfileID;

		//public List<string> ProfileName = new List<string>();
		public string ProfileName;
		public List<string> ProfilePass = new List<string>();
		public List<bool> NewAccount = new List<bool>();

		public List<float> Tools = new List<float>();
		public List<float> Health = new List<float>();
		public List<float> MaxHealth = new List<float>();
		public List<float> Progress = new List<float>();
		public List<string> CurrencyName = new List<string>();
		public List<float> CurrencyAmt = new List<float>();
		public List<string> BuildingName = new List<string>();
		public List<float> BuildingUpgrades = new List<float>();
		public List<string> KingdomNames = new List<string>();
		public List<float> KingdomTrades = new List<float>();
		public List<string> MineName = new List<string>();
		public List<float> MineAmmount = new List<float>();
		public bool NewPlayer;
	}
}