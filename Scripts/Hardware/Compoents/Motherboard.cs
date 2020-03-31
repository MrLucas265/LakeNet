using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motherboard : MonoBehaviour 
{
	public string MotherboardName;
	public string MotherboardSize;

	public int CPUSlots;
	public int CPUSocket;

	public int RAMSlots;
	public int MaxRAMAmt;

	public int GPUSlots;

	public int maxDrivesAmt;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void HardwareCheck()
	{
		switch (HardwareController.hdcon.Motherboard[0]) 
		{
		case "Basic":
			CPUSocket = 775;
			CPUSlots = 1;
			RAMSlots = 2;
			MaxRAMAmt = 8;
			GPUSlots = 1;
			maxDrivesAmt = 2;
			break;
		}
	}
}
