using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSU : MonoBehaviour
{
	public string PowerSupplyName;
	public float MaxPowerOutput;
	public float RemainingPower;
	public float UsedPower;


	void Start ()
	{
	}

	void Update () 
	{
		Math();
	}

	void Math()
	{
        for (int i = 0; i < GameControl.control.Gateway.InstalledPSU.Count; i++)
        {
            GameControl.control.Gateway.InstalledPSU[i].Remaining = GameControl.control.Gateway.InstalledPSU[i].Max - GameControl.control.Gateway.InstalledPSU[i].Used;
            GameControl.control.Gateway.InstalledPSU[i].Used = 0;
        }

        for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
        {
            GameControl.control.Gateway.InstalledPSU[0].Used = GameControl.control.Gateway.InstalledPSU[0].Used + GameControl.control.Gateway.InstalledCPU[i].PowerDraw;
        }

        for (int i = 0; i < GameControl.control.Gateway.InstalledRAM.Count; i++)
        {
            GameControl.control.Gateway.InstalledPSU[0].Used = GameControl.control.Gateway.InstalledPSU[0].Used + GameControl.control.Gateway.InstalledRAM[i].PowerUsage;
        }

        for (int i = 0; i < GameControl.control.Gateway.InstalledStorageDevice.Count; i++)
        {
            GameControl.control.Gateway.InstalledPSU[0].Used = GameControl.control.Gateway.InstalledPSU[0].Used + GameControl.control.Gateway.InstalledStorageDevice[i].PowerUsage;
        }


    }

	//public void HardwareCheck()
	//{
	//	switch (HardwareController.hdcon.PSU[0])
	//	{
	//	case "StrongPower-WeakED":
	//		MaxPowerOutput = 50;
	//		break;
	//	case "StrongPower-2Pack":
	//		MaxPowerOutput = 100;
	//		break;
	//	case "StrongPower-4Pack":
	//		MaxPowerOutput = 200;
	//		break;
	//	case "StrongPower-6Pack":
	//		MaxPowerOutput = 400;
	//		break;
	//	}
	//}
}
