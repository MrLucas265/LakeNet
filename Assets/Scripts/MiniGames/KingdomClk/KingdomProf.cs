using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class KingdomProf : MonoBehaviour 
{
	public static KingdomProf kingprof;

	public string ProfilePath;
	public int ProfileID;

	public List<string> ProfileName = new List<string>();
	public List<string> ProfilePass = new List<string>();
	public List<bool> NewAccount = new List<bool>();

	void Awake ()
	{
		ProfilePath = "C:/ProgramData/FakeNet/Minigames/KingdomClicker";
		if (!Directory.Exists (ProfilePath)) 
		{
			if (ProfilePath != "") 
			{
				Directory.CreateDirectory(ProfilePath);
			}
		}
		Awake1();
	}

	void Awake1()
	{
		if(kingprof == null)
		{
			DontDestroyOnLoad(gameObject);
			kingprof = this;
		}
		else if(kingprof != this)
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
		FileStream file = File.Create (ProfilePath + "/" + "KingdomClickerProfiles" + ".dat");
		KingdomProfileData data = new KingdomProfileData ();

		data.ProfilePath = ProfilePath;
		data.ProfileID = ProfileID;
		data.ProfileName = ProfileName;
		data.ProfilePass = ProfilePass;
		data.NewAccount = NewAccount;

		bf.Serialize (file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (ProfilePath + "/" + "KingdomClickerProfiles" + ".dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (ProfilePath + "/" + "KingdomClickerProfiles" + ".dat", FileMode.Open);
			KingdomProfileData data = (KingdomProfileData)bf.Deserialize (file);
			file.Close ();

			ProfilePath = data.ProfilePath;
			ProfileID = data.ProfileID;
			ProfileName = data.ProfileName;
			ProfilePass = data.ProfilePass;
			NewAccount = data.NewAccount;
		}
	}

	[Serializable]
	class KingdomProfileData
	{
		public string ProfilePath;
		public int ProfileID;

		public List<string> ProfileName = new List<string>();
		public List<string> ProfilePass = new List<string>();
		public List<bool> NewAccount = new List<bool>();
	}
}