using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Desktop : MonoBehaviour 
{
	private GameObject Desktops;

	private AppatureDesktop appaDesktop;
    	//private AppatureDesktop appaDesktop;
	private TreeOSDesktop treeDesktop;
	private IceOSDesktop iceDesktop;
	private	CmdSysOSDesktop cmdsysosDesktop;
	private EthelOSDesktop ethelosDesktop;

	private Boot boot;

	public Texture2D GatewayIcon;
	public Texture2D ApplicationsIcon;
	public Texture2D InternetIcon;
	public Texture2D EmailIcon;
	public Texture2D InfoIcon;
	public Texture2D LogoutIcon;
	public Texture2D CMDIcon;
	public Texture2D HardIcon;
	public Texture2D MapIcon;
	public Texture2D HUDAnaIcon;
	public Texture2D SearchIcon;
	public Texture2D SettingsIcon;
    public Texture2D SpeakerIcon;
    public Texture2D[] SpeakerIconArray;

	private AppMan appman;

	// Use this for initialization
	void Start () 
    {
		Desktops = GameObject.Find("Desktops");
		//Desktop Enviros
		appaDesktop = Desktops.GetComponent<AppatureDesktop>();
		treeDesktop = Desktops.GetComponent<TreeOSDesktop>();
		iceDesktop = Desktops.GetComponent<IceOSDesktop>();
		cmdsysosDesktop = Desktops.GetComponent<CmdSysOSDesktop>();
		ethelosDesktop = Desktops.GetComponent<EthelOSDesktop>();

		appman = GetComponent<AppMan>();

		boot = GetComponent<Boot>();

		if (GameControl.control.GatewayStatus.Terminal == true)
		{
			this.enabled = false;
		}
		else
		{
			DesktopCheck();
		}
	}

	void DesktopCheck()
	{
		switch (boot.SelectedOS.Name) 
		{
		case OperatingSystems.OSName.AppatureOS:
			appaDesktop.enabled = true;
			this.enabled = false;
			break;
        case OperatingSystems.OSName.QuantinitumOS:
            appaDesktop.enabled = true;
            this.enabled = false;
            break;
        case OperatingSystems.OSName.TreeOS:
		    treeDesktop.enabled = true;
		    this.enabled = false;
		    break;
		case OperatingSystems.OSName.FluidicIceOS:
			iceDesktop.enabled = true;
			this.enabled = false;
			break;
		case OperatingSystems.OSName.CSOSV1:
			cmdsysosDesktop.enabled = true;
			this.enabled = false;
			break;
		case OperatingSystems.OSName.EthelOS:
			ethelosDesktop.enabled = true;
			this.enabled = false;
			break;
		}
	}
}