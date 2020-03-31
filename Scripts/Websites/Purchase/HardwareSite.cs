using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareSite : MonoBehaviour
{
	private GameObject Computer;
	private GameObject Prompts;
	private GameObject Applications;
	private GameObject Hacking;
	private GameObject System;
	private GameObject Hardware;

	private InternetBrowser ib;
	private CPU cpu;

	public List<Color> Colors = new List<Color>();
	public Color32 rgb1 = new Color32(0,0,0,0);
	public Color32 buttonColor = new Color32(0,0,0,0);
	public Color32 fontColor = new Color32(0,0,0,0);

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;


	//Transitions
	public bool FadeIn;
	public bool FadeOut;
	public bool EnableTransition;
	public string GotoSite;
	public float Fadetimer;
	public float Fadecooldown;

	//Banner
	public List<string> BannerInfo = new List<string>();

	//Search
	public string SearchBarText;
	public Texture2D SearchIcon;
	public string SearchSites;
	public string Searched;
	public string Inputted;
	public bool SearchDone;
	public int SearchCount;
	public bool UpdateSearchUI;

	//Checkout
	public List<string> CheckoutLists = new List<string>();

	//Products
	public List<string> ListOfProductName = new List<string>();
	public List<string> ButtonList = new List<string>();
	public List<string> CPUList = new List<string>();
	public List<string> GPUList = new List<string>();
	public List<string> SSDList = new List<string>();
	public List<string> HDDList = new List<string>();
	public List<string> RAMList = new List<string>();
	public List<string> PSUList = new List<string>();
	public string SelectedProduct;
	public int Selected;
	public string SelectedPage;


	// Use this for initialization
	void Start () 
	{
		Applications = GameObject.Find("Applications");
		Hardware = GameObject.Find("Hardware");
		GetComp();
		LoadPresetColors();
		SetTimers();
		UpdateProductList();
		UpdateButtonList();
	}

	void SetTimers()
	{
		Fadecooldown = 0f;
		Fadetimer = Fadecooldown;
		//SearchSites = "Search for products here";
	}

	void GetComp()
	{
		ib = Applications.GetComponent<InternetBrowser>();
		cpu = Hardware.GetComponent<CPU>();
	}

	void LoadPresetColors()
	{
		buttonColor.r = 25;
		buttonColor.g = 25;
		buttonColor.b = 25;
		buttonColor.a = 255;

		fontColor.r = 255;
		fontColor.g = 255;
		fontColor.b = 255;
		fontColor.a = 255;
	}

	void UpdateProductList()
	{
		CPUList.Add ("CPU1");
		CPUList.Add ("CPU2");
		GPUList.Add ("GPU1");
		GPUList.Add ("GPU2");
		RAMList.Add ("RAM1");
		RAMList.Add ("RAM2");
		HDDList.Add ("HDD1");
		HDDList.Add ("HDD2");
	}

	void UpdateButtonList()
	{
		ButtonList.Add ("CPU");
		ButtonList.Add ("CPUP1");
		ButtonList.Add ("GPU");
		ButtonList.Add ("RAM");
		ButtonList.Add ("HDD");
		ButtonList.Add ("SSD");
		ButtonList.Add ("PSU");
	}

	void SwitchPage()
	{
		switch (SelectedPage)
		{
		case "CPU":
			ib.AddressBar = "test2/cpu";
			break;
		case "CPUP1":
			ib.AddressBar = "test2/cpu/page1";
			break;
		case "GPU":
			ib.AddressBar = "test2/gpu";
			break;
		case "RAM":
			ib.AddressBar = "test2/ram";
			break;
		}
	}

	void BackButton()
	{
		if (GUI.Button (new Rect (0, 50, 60, 21), "Back"))
		{
			ib.AddressBar = "test2/homepage";
		}
	}

	void HomePage()
	{
		scrollpos = GUI.BeginScrollView(new Rect(5, 100, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < ButtonList.Count; scrollsize++)
		{
			if(GUI.Button(new Rect(5,scrollsize * 20,100,20),ButtonList[scrollsize]))
			{
				SelectedPage = ButtonList[scrollsize].ToString();
				SwitchPage();
			}
		}
		GUI.EndScrollView();
	}

	void CPUUI()
	{
		BackButton();
		scrollpos = GUI.BeginScrollView(new Rect(5, 100, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < CPUList.Count; scrollsize++)
		{
			if(GUI.Button(new Rect(5,scrollsize * 20,100,20),CPUList[scrollsize]))
			{
				Searched = CPUList[scrollsize].ToString();
			}
		}
		GUI.EndScrollView();
	}

	void CPUUIP1()
	{
		BackButton();
		if (GUI.Button (new Rect (50, 100, 42, 42), "13")) 
		{
			//HardwareController.hdcon.CPU[0] = "Zion Z-13";
			//HardwareController.hdcon.AirFlow = 2;
			//HardwareController.hdcon.MaxCPUSpeed = 1f;
			//HardwareController.hdcon.Cores = 1;
			//HardwareController.hdcon.MaxTEMP = 70;
			//HardwareController.hdcon.PowerEff = 0.035f;
			//HardwareController.hdcon.ThrottleTEMP = 70;
			//HardwareController.hdcon.CPUEff = 10;
			//HardwareController.hdcon.CPUVoltage = 1;
			//HardwareController.hdcon.Save();
			//cpu.UpdateCPUStats();
		}

		if (GUI.Button (new Rect (100, 100, 42, 42), "12")) 
		{
			//HardwareController.hdcon.CPU[0] = "Zion Z-12";
			//HardwareController.hdcon.AirFlow = 2;
			//HardwareController.hdcon.MaxCPUSpeed = 1.2f;
			//HardwareController.hdcon.Cores = 4;
			//HardwareController.hdcon.MaxTEMP = 80;
			//HardwareController.hdcon.PowerEff = 0.15f;
			//HardwareController.hdcon.ThrottleTEMP = 70;
			//HardwareController.hdcon.CPUEff = 6;
			//HardwareController.hdcon.CPUVoltage = 1;
			//HardwareController.hdcon.Save();
			//cpu.UpdateCPUStats();
		}

		for (int x = 0; x < 5; x++)
		{
			for (int y = 0; y < 5; y++)
			{
				Rect slotRect = new Rect (x * 50, y * 50, 48, 48);
				GUI.Box (slotRect,"" + y);
			}
		}
	}

	void GPUUI()
	{
		BackButton();
		scrollpos = GUI.BeginScrollView(new Rect(5, 100, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < GPUList.Count; scrollsize++)
		{
			if(GUI.Button(new Rect(5,scrollsize * 20,100,20),GPUList[scrollsize]))
			{
				Searched = GPUList[scrollsize].ToString();
			}
		}
		GUI.EndScrollView();
	}

	void RAMUI()
	{
		BackButton();
		scrollpos = GUI.BeginScrollView(new Rect(5, 100, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < RAMList.Count; scrollsize++)
		{
			if(GUI.Button(new Rect(5,scrollsize * 20,100,20),RAMList[scrollsize]))
			{
				Searched = RAMList[scrollsize].ToString();
			}
		}
		GUI.EndScrollView();
	}

	public void RenderSite()
	{
		GUI.backgroundColor = buttonColor;
		GUI.contentColor = fontColor;
		//GUI.color = rgb1;

		if (EnableTransition == true) 
		{
			//Transition();
		}

		switch (ib.AddressBar)
		{
		case "test2":
			HomePage();
			break;
		case "test2/homepage":
			HomePage();
			break;
		case "test2/cpu":
			CPUUI();
			break;
		case "test2/cpu/page1":
			CPUUIP1();
			break;
		case "test2/gpu":
			GPUUI();
			break;
		case "test2/ram":
			RAMUI();
			break;
		}
	}
}
