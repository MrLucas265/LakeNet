using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class HardwareController : MonoBehaviour 
{
	public static HardwareController hdcon;

	public string ProfilePath;
	public int ProfileID;
	public string ProfileName;
	//public List<float> VersionNumber = new List<float>();

	//public List<string> HDDSSD = new List<string>();
	//public List<string> GPU = new List<string>();
	public List<string> Ram = new List<string>();
	public List<string> PSU = new List<string>();
	public List<string> Motherboard = new List<string>();
	public List<string> Modem = new List<string>();

	public float networkspeed;
	public float Maxsnetworkspeed;
	public float GPUVoltage;
	public float RAMVoltage;


	public float HDDMaxSpace;
	public float HDDFreeSpace;
	public float HDDUsedSpace;

	public float HardDriveSpeed;


	public bool CPUCheck;
	public bool GPUCheck;
	public bool PSUCheck;
	public bool RAMCheck;
	public bool HDDCheck;
	public bool MBCheck;
	public bool ModemCheck;

//	public List<string> CPU = new List<string>();
	public float MaxCPUSpeed;
	public int CPUEff;
	public float MaxTEMP;
	public float ThrottleTEMP;
	public float PowerEff;
	public int Cores;
	public float AirFlow;
	public float CPUVoltage;


	void Awake ()
	{
		Awake1();
	}

	void Awake1()
	{
		if(hdcon == null)
		{
			DontDestroyOnLoad(gameObject);
			hdcon = this;
		}
		else if(hdcon != this)
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

	public void DeleteFile ()
	{
		File.Delete(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber+ "/hardware/" + ProfileName + ".dat");
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber+ "/hardware/" + ProfileName + ".dat");
		ProfileData data = new ProfileData ();

		data.ProfilePath = ProfilePath;
		data.ProfileID = ProfileID;
		data.ProfileName = ProfileName;
		//data.HDDSSD = HDDSSD;
		//data.GPU = GPU;
		data.Ram = Ram;
		data.Motherboard = Motherboard;
		data.PSU = PSU;
		data.networkspeed = networkspeed;
		data.Modem = Modem;
		data.HDDUsedSpace = HDDUsedSpace;
		data.GPUVoltage = GPUVoltage;
		data.RAMVoltage = RAMVoltage;
		//CPU
		//data.CPU = CPU;
		data.CPUVoltage = CPUVoltage;
		data.MaxCPUSpeed = MaxCPUSpeed;
		data.CPUEff = CPUEff;
		data.MaxTEMP = MaxTEMP;
		data.ThrottleTEMP = ThrottleTEMP;
		data.PowerEff = PowerEff;
		data.Cores = Cores;
		data.AirFlow = AirFlow;

		bf.Serialize (file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber+ "/hardware/" + ProfileName + ".dat"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber+ "/hardware/" + ProfileName + ".dat", FileMode.Open);
			ProfileData data = (ProfileData)bf.Deserialize (file);
			file.Close ();

			ProfilePath = data.ProfilePath;
			ProfileID = data.ProfileID;
			ProfileName = data.ProfileName;
			//HDDSSD = data.HDDSSD;
			//GPU = data.GPU;
			Ram = data.Ram;
			Motherboard = data.Motherboard;
			PSU = data.PSU;
			networkspeed = data.networkspeed;
			Modem = data.Modem;
			HDDUsedSpace = data.HDDUsedSpace;
			GPUVoltage = data.GPUVoltage;
			RAMVoltage = data.RAMVoltage;
			//CPU
			//CPU = data.CPU;
			CPUVoltage = data.CPUVoltage;
			MaxCPUSpeed = data.MaxCPUSpeed;
			CPUEff = data.CPUEff;
			MaxTEMP = data.MaxTEMP;
			ThrottleTEMP = data.ThrottleTEMP;
			PowerEff = data.PowerEff;
			Cores = data.Cores;
			AirFlow = data.AirFlow;

		}
	}

	[Serializable]
	class ProfileData
	{
		public string ProfilePath;
		public int ProfileID;
		public string ProfileName;

		//public List<string> HDDSSD = new List<string>();
		//public List<string> GPU = new List<string>();
		public List<string> Ram = new List<string>();
		public List<string> Motherboard = new List<string>();
		public List<string> PSU = new List<string>();
		public List<string> Modem = new List<string>();
		public float networkspeed;

		public float GPUVoltage;
		public float RAMVoltage;

		public float HDDUsedSpace;

		//public List<string> CPU = new List<string>();
		public float MaxCPUSpeed;
		public int CPUEff;
		public float MaxTEMP;
		public float ThrottleTEMP;
		public float PowerEff;
		public int Cores;
		public float AirFlow;
		public float CPUVoltage;
	}
}
