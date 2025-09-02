using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OperatingSystems
{
	public string Title;
	public OSName Name;
	public string SerialKey;
	public OSColorSystem Colour = new OSColorSystem();
	public OSFPCSystem FPC = new OSFPCSystem();
	public OSOptionsSystem Options = new OSOptionsSystem();
	public List<DiskPartSystem> Partitions = new List<DiskPartSystem>();
	public enum OSName
	{
		FluidicIceOS,
		TreeOS,
		UplinkOS,
		HackNetSystems,
		NOS,
		WinDoor68,
		AppatureOS,
		CSOSV1,
		HackLink,
        QuantinitumOS,
		RizaRami,
		SafeMode,
		EthelOS
    }

	public OperatingSystems(string title, OSName name) //,Texture2D icon)
	{
		Title = title;
		Name = name;
	}
	public OperatingSystems(string title, OSName name,OSOptionsSystem options) //,Texture2D icon)
	{
		Title = title;
		Name = name;
		Options = options;
	}
	public OperatingSystems() //,Texture2D icon)
	{

	}
}
