using UnityEngine;
using System.Collections;

public class WebSec : MonoBehaviour 
{
	public float SecLevel;

	public int MonitorLevel;
	public bool Monitor;

	public int ProxyLevel;
	public bool Proxy;

	public int FirewallLevel;
	public bool Firewall;

	private InternetBrowser ib;

	public bool UpdateSecCheck;

	public void Start()
	{
		ib = GetComponent<InternetBrowser> ();

        if(GameControl.control.WebsiteSecurity.Count == 0)
        {
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.REVATest, "Password Verification",1,"Enabled",1440,1440,WebSecSystem.SecType.UAC));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.REVATest, "Intrusion Detector", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.IDS));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.REVATest, "Proxy System", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.Proxy));

            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.Jaildew, "Password Verification", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.UAC));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.Jaildew, "Intrusion Detector", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.IDS));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.Jaildew, "Proxy", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.Proxy));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.Jaildew, "Logs", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.LogManagement));

            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.BecasSystem, "Password Verification", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.UAC));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.BecasSystem, "Intrusion Detector", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.IDS));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.BecasSystem, "Proxy", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.Proxy));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.BecasSystem, "Logs", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.LogManagement));

            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.Unicom, "Password Verification", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.UAC));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.Unicom, "Intrusion Detector", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.IDS));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.Unicom, "Proxy", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.Proxy));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.Unicom, "Logs", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.LogManagement));

            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.ISD, "Password Verification", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.UAC));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.ISD, "Intrusion Detector", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.IDS));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.ISD, "Proxy", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.Proxy));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.ISD, "User Account Controller", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.Firewall));
            GameControl.control.WebsiteSecurity.Add(new WebSecSystem(WebSecSystem.Server.ISD, "Logs", 1, "Enabled", 1440, 1440, WebSecSystem.SecType.LogManagement));
        }
	}

	void Update()
	{
		if (UpdateSecCheck == true) 
		{
			SecCheck();
		}
	}

	public void SecCheck()
	{
		switch(ib.SiteName)
		{
		case "Becas":
			Monitor = true;
			Proxy = false;
			Firewall = false;
			SecLevel = 1;
			break;
		case "Reva Test":
			Monitor = false;
			Proxy = false;
			Firewall = false;
			SecLevel = 1;
			break;
		case "Jaildew":
            for(int i = 0; i < GameControl.control.WebsiteSecurity.Count; i++)
            {
                if(GameControl.control.WebsiteSecurity[i].ServerName == WebSecSystem.Server.Jaildew)
                {
                    if (!ib.CurrentSecurity.Contains(GameControl.control.WebsiteSecurity[i]))
                    {
                            ib.CurrentSecurity.Add(GameControl.control.WebsiteSecurity[i]);
                    }
                }
            }
			break;
		case "Unicom":
			Monitor = true;
			Proxy = false;
			Firewall = false;
			SecLevel = 1;
            break;
		}
	}
}