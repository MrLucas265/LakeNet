using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkSystem
{
	public string InputAddress;
	public string IPAddress;
	public string DisplayName;
	public string Domain;
	public string InternalIPAddress;
	public string Port;
	public string Command;
	public string Status;

	public NetworkSystem(string inputaddress,string command)
	{
		InputAddress = inputaddress;
		Command = command;
	}

    public NetworkSystem()
    {
    }
}