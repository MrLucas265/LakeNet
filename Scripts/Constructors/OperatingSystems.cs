using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OperatingSystems
{
	public OSName Name;

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
        QuantinitumOS
    }

	public OperatingSystems(OSName name) //,Texture2D icon)
	{
		Name = name;
	}
}
