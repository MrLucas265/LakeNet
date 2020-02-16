using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerStatsController : MonoBehaviour 
{
	public static PlayerStatsController stats;

	public string ProfilePath;
	public string ProfileName;

	public int ATK;

	void Awake()
	{
		if (!Directory.Exists (ProfilePath)) 
		{
			if (ProfilePath != "") 
			{
				Directory.CreateDirectory(ProfilePath);
			}
		}

		if(stats == null)
		{
			DontDestroyOnLoad(gameObject);
			stats = this;
		}
		else if(stats != this)
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
		FileStream file = File.Create (Application.dataPath + "/saves" + "/custom/" + ProfileName + ".dat");
		CustomData data = new CustomData ();

		data.ATK = ATK;

		bf.Serialize (file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (Application.dataPath + "/saves" + "/custom/" + ProfileName + ".dat"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.dataPath + "/saves" + "/custom/" + ProfileName + ".dat",FileMode.Open);
			CustomData data = (CustomData)bf.Deserialize (file);
			file.Close ();

			ATK = data.ATK;
		}
	}

	[Serializable]
	class CustomData
	{
		public int ATK;
	}
}
