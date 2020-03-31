using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardDrives : MonoBehaviour 
{
	public float PowerUsage;
	public float DriveSpeed;

	public bool check;

	public float UsedSpace;

	public float CustomSpeed;

    public float Timer;

	// Use this for initialization
	void Start ()
	{
		//if (HardwareController.hdcon.HDDSSD.Count > 0)
		//{
		//	HardwareCheck();
		//}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (HardwareController.hdcon.HDDCheck == true) 
		{
			HardwareCheck();
			HardwareController.hdcon.HDDCheck = false;
		}

        Timer += Time.deltaTime;

        if (Timer > 1)
        {
            HealthDegredationMath();
        }

        MathCheck();
	}

	public void HardwareCheck()
	{
		//switch (HardwareController.hdcon.HDDSSD[0])
		//{
		//case "Custom":
		//	GameControl.control.BootTime = 0.01f;
		//	HardwareController.hdcon.HDDMaxSpace = 1000;
		//	HardwareController.hdcon.HardDriveSpeed = 1;
		//	PowerUsage = 5;
		//	break;

		//case "Slow":
		//	GameControl.control.BootTime = CustomSpeed;
		//	HardwareController.hdcon.HDDMaxSpace = 1000;
		//	HardwareController.hdcon.HardDriveSpeed = 1;
		//	PowerUsage = 5;
		//	break;
		//case "Med":
		//	GameControl.control.BootTime = CustomSpeed;
		//	HardwareController.hdcon.HDDMaxSpace = 1000;
		//	HardwareController.hdcon.HardDriveSpeed = 1;
		//	PowerUsage = 5;
		//	break;
		//case "Fast":
		//	GameControl.control.BootTime = 0.08f;
		//	HardwareController.hdcon.HDDMaxSpace = 1000;
		//	HardwareController.hdcon.HardDriveSpeed = 1;
		//	PowerUsage = 5;
		//	break;


		//case "SnowDisk 10TF":
		//	GameControl.control.BootTime = 0.08f;
		//	HardwareController.hdcon.HDDMaxSpace = 10;
		//	HardwareController.hdcon.HardDriveSpeed = 0.125f;
		//	PowerUsage = 5;
		//	break;
		//case "SnowDisk 20TF":
		//	GameControl.control.BootTime = 0.08f;
		//	HardwareController.hdcon.HDDMaxSpace = 20;
		//	HardwareController.hdcon.HardDriveSpeed = 0.125f;
		//	PowerUsage = 15;
		//	break;
		//case "SnowDisk 50TF":
		//	GameControl.control.BootTime = 0.08f;
		//	HardwareController.hdcon.HDDMaxSpace = 50;
		//	HardwareController.hdcon.HardDriveSpeed = 0.125f;
		//	PowerUsage = 25;
		//	break;
		//case "SnowDisk 100TF":
		//	GameControl.control.BootTime = 0.08f;
		//	HardwareController.hdcon.HDDMaxSpace = 100;
		//	HardwareController.hdcon.HardDriveSpeed = 0.125f;
		//	PowerUsage = 50;
		//	break;
		//case "SnowDisk 200TF":
		//	GameControl.control.BootTime = 0.08f;
		//	HardwareController.hdcon.HDDMaxSpace = 200;
		//	HardwareController.hdcon.HardDriveSpeed = 0.125f;
		//	PowerUsage = 100;
		//	break;
		//case "Accel Drives 15TF":
		//	GameControl.control.BootTime = 0.06f;
		//	HardwareController.hdcon.HDDMaxSpace = 15;
		//	HardwareController.hdcon.HardDriveSpeed = 0.256f;
		//	PowerUsage = 10;
		//	break;
		//case "Entra Server Drives 250TF":
		//	GameControl.control.BootTime = 0.12f;
		//	HardwareController.hdcon.HDDMaxSpace = 250;
		//	HardwareController.hdcon.HardDriveSpeed = 0.074f;
		//	PowerUsage = 100;
		//	break;
		//case "Entra Server Drives 500TF":
		//	GameControl.control.BootTime = 0.12f;
		//	HardwareController.hdcon.HDDMaxSpace = 500;
		//	HardwareController.hdcon.HardDriveSpeed = 0.074f;
		//	PowerUsage = 200;
		//	break;
		//case "East Virtual Solid Drives 50TF":
		//	GameControl.control.BootTime = 0.04f;
		//	HardwareController.hdcon.HDDMaxSpace = 50;
		//	HardwareController.hdcon.HardDriveSpeed = 0.512f;
		//	PowerUsage = 15;
		//	break;
		//}
	}

	void MathCheck()
	{
        for (int i = 0; i < GameControl.control.Gateway.InstalledStorageDevice.Count; i++)
        {
            GameControl.control.Gateway.InstalledStorageDevice[i].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[i].Capacity - GameControl.control.Gateway.InstalledStorageDevice[i].UsedSpace;
        }

        GameControl.control.BootTime = GameControl.control.Gateway.InstalledStorageDevice[0].BootTime;
	}

    public void HealthDegredationMath()
    {
        for (int i = 0; i < GameControl.control.Gateway.InstalledStorageDevice.Count; i++)
        {
            GameControl.control.Gateway.InstalledStorageDevice[i].CurrentHealth -= GameControl.control.Gateway.InstalledStorageDevice[i].DegradationRate;
            GameControl.control.Gateway.InstalledStorageDevice[i].HealthPercentage = GameControl.control.Gateway.InstalledStorageDevice[i].CurrentHealth / GameControl.control.Gateway.InstalledStorageDevice[i].MaxHealth * 100;
        }
        Timer = 0;
    }
}
