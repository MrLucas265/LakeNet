using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIOSSelect : MonoBehaviour 
{
	private BIOS1 bios1;
	// Use this for initialization
	void Start () 
	{
		bios1 = GetComponent<BIOS1>();
		//GameControl.control.Load();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnGUI()
	{
		
	}

	public void RenderBios()
	{
		switch (GameControl.control.Gateway.Brand)
		{

			case "":
				bios1.enabled = true;
				bios1.BIOSGUI();
				break;

			case "Basic":
				bios1.enabled = true;
				bios1.BIOSGUI();
				break;

			case "NOVA 8A21C-1280RD":
				bios1.enabled = true;
				bios1.BIOSGUI();
				break;

			case "2":
				break;
		}
	}
}
