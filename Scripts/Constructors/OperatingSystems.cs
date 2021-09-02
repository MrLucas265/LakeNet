using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OperatingSystems
{
	public OSName Name;
	public string Title;
	public ColorSystem Colour;
	public OSFPCSystem FPC;
	public bool DisableColourOption;
	public int SelectedBackground;
	public bool GridMode;

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
		SafeMode,
		EthelOS
    }

	public OperatingSystems(OSName name,string title, ColorSystem colour,OSFPCSystem fpc,bool disablecolouroption,int selectedbackground, bool gridmode) //,Texture2D icon)
	{
		Name = name;
		Title = title;
		Colour = colour;
		FPC = fpc;
		DisableColourOption = disablecolouroption;
		SelectedBackground = selectedbackground;
		GridMode = gridmode;
	}
}
