using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class ProfileController : MonoBehaviour 
{
	public static ProfileController procon;

	public string FilePath;
	public string Folder;
	public int VersionNumber;
	public List<string> Profiles = new List<string>();
	public List<string> ProfilePassWord = new List<string>();
	public List<string> PasswordHint = new List<string>();
	public List<int> ProfileID = new List<int>();
	public List<int> ProfilePic = new List<int>();
	public bool ShowTOS;
	public string ActualFilePath;
    //public List<float> VersionNumber = new List<float>();

    public int SelectedProfile;

	public List<OperatingSystems> SelectedOS = new List<OperatingSystems>();

	//Hardware
	public List<MotherboardSystem> Motherboard = new List<MotherboardSystem>();

	public bool ResetAccount;

	void Awake ()
	{
		Awake1();
	}

	void Awake1()
	{
		if (!Directory.Exists (Application.dataPath + "/saves/"+VersionNumber+"/profiles")) 
		{
			if (FilePath != "") 
			{
				Directory.CreateDirectory(Application.dataPath + "/saves/"+VersionNumber+"/profiles");
			}
		}

		if (!Directory.Exists (Application.dataPath + "/screenshots")) 
		{
			Directory.CreateDirectory(Application.dataPath + "/screenshots");
		}

		if (!Directory.Exists (Application.dataPath + "/saves/" + VersionNumber + "/custom")) 
		{
			Directory.CreateDirectory(Application.dataPath + "/saves/" +VersionNumber + "/custom");
		}

        if (!Directory.Exists(Application.dataPath + "/saves/" + VersionNumber + "/people"))
        {
            Directory.CreateDirectory(Application.dataPath + "/saves/" + VersionNumber + "/people");
        }

        if (!Directory.Exists (Application.dataPath + "/saves/" + VersionNumber + "/hardware")) 
		{
			Directory.CreateDirectory(Application.dataPath + "/saves/" +VersionNumber + "/hardware");
		}

		if(procon == null)
		{
			DontDestroyOnLoad(gameObject);
			procon = this;
		}
		else if(procon != this)
		{
			Destroy(gameObject);
		}
	}
	// Use this for initialization
	void Start () 
	{
		ActualFilePath = Application.dataPath + "/saves/" + VersionNumber + "/profiles/" + "Profiles";
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void DeleteProfile(int Selected)
	{
		Profiles.RemoveAt(Selected);
		ProfilePassWord.RemoveAt(Selected);
		PasswordHint.RemoveAt(Selected);
		ProfileID.RemoveAt(Selected);
		ProfilePic.RemoveAt(Selected);
		SelectedOS.RemoveAt(Selected);
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(ActualFilePath + ".dat");
		ProfileData data = new ProfileData ();

		data.Profiles = Profiles;
		data.ProfileID = ProfileID;
		data.ProfilePassWord = ProfilePassWord;
		data.ProfilePic = ProfilePic;
		data.ShowTOS = ShowTOS;
		data.VersionNumber = VersionNumber;
		data.PasswordHint = PasswordHint;
		data.SelectedOS = SelectedOS;
		data.Motherboard = Motherboard;
		data.ResetAccount = ResetAccount;

		bf.Serialize (file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (ActualFilePath + ".dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (ActualFilePath + ".dat", FileMode.Open);
			ProfileData data = (ProfileData)bf.Deserialize (file);
			file.Close ();

			Profiles = data.Profiles;
			ProfileID = data.ProfileID;
			ProfilePassWord = data.ProfilePassWord;
			ProfilePic = data.ProfilePic;
			ShowTOS = data.ShowTOS;
			VersionNumber = data.VersionNumber;
			PasswordHint = data.PasswordHint;
			SelectedOS = data.SelectedOS;
			Motherboard = data.Motherboard;
			ResetAccount = data.ResetAccount;
		}
	}

	[Serializable]
	class ProfileData
	{
		public List<string> Profiles = new List<string>();
		public List<int> ProfileID = new List<int>();
		public List<string> ProfilePassWord = new List<string>();
		public List<string> PasswordHint = new List<string>();
		public List<int> ProfilePic = new List<int>();
		public bool ShowTOS;

		//public List<float> VersionNumber = new List<float>();
		public int VersionNumber;

		public string GatewayLocation;

		public List<OperatingSystems> SelectedOS = new List<OperatingSystems>();

		//Hardware
		public List<MotherboardSystem> Motherboard = new List<MotherboardSystem>();

		public bool ResetAccount;
	}
}
