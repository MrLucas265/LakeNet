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

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GameControl.control.Gateway.InstalledStorageDevice.Count > 0)
		{
			for (int i = 0; i < GameControl.control.Gateway.InstalledStorageDevice.Count; i++)
			{
				if (GameControl.control.Gateway.InstalledStorageDevice[i].CurrentHealth > 0)
				{
					GameControl.control.Gateway.InstalledStorageDevice[i].Timer -= Time.deltaTime * GameControl.control.TimeMulti;
					if (GameControl.control.Gateway.InstalledStorageDevice[i].Timer <= 0)
					{
						GameControl.control.Gateway.InstalledStorageDevice[i].CurrentHealth -= GameControl.control.Gateway.InstalledStorageDevice[i].DegradationRate;
						GameControl.control.Gateway.InstalledStorageDevice[i].HealthPercentage = GameControl.control.Gateway.InstalledStorageDevice[i].CurrentHealth / GameControl.control.Gateway.InstalledStorageDevice[i].MaxHealth * 100;
						GameControl.control.Gateway.InstalledStorageDevice[i].Timer = GameControl.control.Gateway.InstalledStorageDevice[i].InitalTimer;
					}
				}
				else
				{
					GameControl.control.Gateway.InstalledStorageDevice[i].CurrentHealth = 0;
				}
			}
		}
	}
}
